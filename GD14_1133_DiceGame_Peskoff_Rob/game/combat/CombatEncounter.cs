using GD14_1133_DiceGame_Peskoff_Rob.game.@enum;
using GD14_1133_DiceGame_Peskoff_Rob.game.item;
using GD14_1133_DiceGame_Peskoff_Rob.game.ui;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.combat {
	internal class CombatEncounter {

		/*
		 * THE PLAN:
		 *
		 * Combat takes place between two "teams", each with one or more `Combatant`s
		 * 
		 * Each team takes turns, with each `Combatant` on a team taking their turn before the next team takes theirs.
		 * 
		 * When a `Combatant` takes their turn, they will choose one `CombatAction` to perform:
		 *		- Attack:
		 *			They will choose a `Weapon` currently in their inventory and then a valid `Combatant` target.
		 *			The `Weapon` will Roll() and the target will take that amount of damage. The weapon will also
		 *			apply any side effects.
		 *		- UseItem
		 *			They will choose a `Consumable` currently in their inventory and then a valid `Combatant` target.
		 *			The `Consumable` will apply its side effects to the target `Combatant`. The `Consumable` will be
		 *			removed from their inventory.
		 *		- Flee
		 *			They will flee from the combat encounter and be removed from their team.
		 *			
		 * The combat encounter continues until all `Combatant`s on a given team have their HP reduced to zero ("defeated").
		 * 
		 */
		private List<Team> teams;
		private List<CombatantInfoCard> infoCards = [];

		public CombatEncounter(Team teamA, Team teamB) {
			teams = [teamA, teamB];
		}

		private Team[] GetUndefeatedTeams() {
			return [.. ( from team in teams where !team.IsDefeated select team )];
		}

		private void AssignAllegiances() {
			foreach ( Team team in teams ) {
				team.ResetAllegiances();
				foreach ( Team otherTeam in teams ) {
					if ( team == otherTeam )
						continue;
					team.SetTeamAllegiance(otherTeam, Allegiance.Hostile);
				}
			}
		}

		private void CreateInfoCards() {
			Combatant[] enemies = teams[1].GetMembers(Mortality.Any);
			for ( int i = 0; i < enemies.Length; i++ ) {
				Combatant enemy = enemies[i];
				CombatantInfoCard infoCard = new(enemy);
				int offset = -( infoCard.container.AbsoluteSize.X + 2 ) / 2;

				// Shut up, I'm hardcoding this for now because I can't figure out the math
				if ( enemies.Length == 1 ) {
					infoCard.SetPosition(new(0.5f, offset, 0.4f, 0));
				} else if ( enemies.Length == 2 ) {
					infoCard.SetPosition(new(0.5f, offset + ( i == 0 ? -20 : 20 ), 0.4f, 0));
				} else if ( enemies.Length == 3 ) {
					infoCard.SetPosition(new(0.5f, offset + ( i == 1 ? 0 : i == 0 ? -32 : 32 ), 0.4f, 0));
				}

				infoCards.Add(infoCard);
			}
		}

		private void RunSingleRound() {
			foreach ( Team team in teams ) {
				if ( team.IsDefeated )
					continue;

				/*
				 * We always want iterate throgh all combatants, dead or alive, in case a
				 * combatant is revived by a combatant who takes their turn first.
				 */
				foreach ( Combatant combatant in team.GetMembers(Mortality.Any) ) {
					if ( !combatant.IsAlive )
						continue;

					// TODO: Maybe display a special message here? ("X is stunned and cannot move!")
					if ( !combatant.CanAct() )
						continue;

					/*
					 * So, turns out, players can choose to type "back" to cancel an item
					 * selection. That's no bueno for combat because if they do that then
					 * we have to ask them for another action. Now, the only way to know
					 * when to do this is to check if `chosenItem` is null, since if they
					 * canceled their item selection then it will be. However, the two
					 * methods `SelectWeapon` and `SelectConsumable` are not allowed to
					 * return null! Soooo instead of complicating the logic, I just made
					 * two new special items:
					 * 
					 * `PromptCombatActionAgainWeapon` and `PromptCombatActionAgainConsumable`
					 * 
					 * which are each respectively returned from the player's `SelectWeapon`
					 * or `SelectConsumable` methods if the selection is canceled...
					 * 
					 * ...And then we keep looping, asking for a new action each time,
					 * until the chosen item isn't either of them.
					 * 
					 * Yay game development! Trains are just hats.
					 */
					Item? chosenItem = null;
					CombatAction combatantAction;

					do {
						combatantAction = combatant.SelectCombatAction();
						chosenItem = null;

						if ( combatantAction == CombatAction.Attack ) {
							chosenItem = combatant.SelectWeapon();
						} else if ( combatantAction == CombatAction.UseItem ) {
							chosenItem = combatant.SelectConsumable();
						} else if ( combatantAction == CombatAction.Flee ) {
							// TODO: Make this actually do something
							Game.dialogWindow.ShowDialog($"{combatant.GetFullName()} tries to flee from the battle...\n\n...but can't!");
						}
					} while ( chosenItem != null && ( chosenItem.GetType() == typeof(PromptCombatActionAgainWeapon) || chosenItem.GetType() == typeof(PromptCombatActionAgainConsumable) ) );

					if ( chosenItem != null ) {
						Combatant[] validItemTargets = team.GetAllegiantMembers(chosenItem.TargetAllegiance, chosenItem.TargetMortality);
						Combatant target = combatant.SelectTarget(validItemTargets);
						bool wasTargetAlive = target.IsAlive;

						chosenItem.OnUse(combatant, target);

						if ( !target.IsAlive && wasTargetAlive ) {
							Game.dialogWindow.ShowDialog(target.GetDefeatMessage());
						}
					} else if ( combatantAction == CombatAction.Pass ) {
						Game.dialogWindow.ShowDialog(combatant.GetPassMessage());
					}
				}
			}
		}

		private void RunCombatRoundLoop() {
			while ( GetUndefeatedTeams().Length > 1 ) {
				RunSingleRound();
			}
		}

		public void RunCombat() {
			CreateInfoCards();
			AssignAllegiances();
			RunCombatRoundLoop();

			foreach ( Team team in teams ) {
				team.ResetAllegiances();
			}

			foreach ( CombatantInfoCard infoCard in infoCards ) {
				infoCard.Destroy();
			}
		}

	}
}
