using GD14_1133_DiceGame_Peskoff_Rob.game.combat;
using GD14_1133_DiceGame_Peskoff_Rob.game.combat.enemy;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.@object {
	internal class CombatRoom : DungeonRoom {
		public override string GetRoomDescription() {
			return "Combat Room";
		}

		private CpuCombatant PickRandomCombatant() {
			Random rng = new();
			int roll = rng.Next(0, 15);

			if ( roll <= 3 ) {
				return new HauntedKnight();
			} else {
				return new Ghoul();
			}
		}

		public override void OnEntered(DungeonGamePlayer player) {
			bool didVisit = isVisited;
			base.OnEntered(player);

			if ( didVisit ) {
				Game.dialogWindow.ShowDialog("You recall the great and vicious battle that you had here.");
			} else if ( player.Team != null ) {
				Game.dialogWindow.ShowDialog("Suddenly, a horde of enemies appears before you!");

				Random rng = new();
				int numEnemies = rng.Next(0, 2) + 1;

				List<Combatant> enemies = [];
				for ( int i = 0; i < numEnemies; i++ ) {
					enemies.Add(PickRandomCombatant());
				}

				Team enemyTeam = new(enemies);
				CombatEncounter battle = new(player.Team, enemyTeam);
				battle.RunCombat();

				if ( player.IsAlive ) {
					Game.dialogWindow.ShowDialog("You breathe a sigh of relief as the battle comes to an end.");
				}
			}
		}

		public override void OnSearched(DungeonGamePlayer player) {
			base.OnSearched(player);
		}
	}
}
