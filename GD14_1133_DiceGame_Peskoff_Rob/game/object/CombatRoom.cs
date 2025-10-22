using GD14_1133_DiceGame_Peskoff_Rob.game.combat;
using GD14_1133_DiceGame_Peskoff_Rob.game.combat.enemy;
using System.Diagnostics;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.@object {
	internal class CombatRoom : DungeonRoom {
		private static readonly char[] LETTERS = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N'];

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

		private Team AssembleEnemyTeam() {
			Random rng = new();
			int numEnemies = rng.Next(0, 2) + 1;

			Dictionary<Type, int> enemyCounts = [];
			List<Combatant> enemies = [];

			for ( int i = 0; i < numEnemies; i++ ) {
				CpuCombatant enemy = PickRandomCombatant();
				Type type = enemy.GetType();

				if ( !enemyCounts.ContainsKey(type) ) {
					enemyCounts[type] = 0;
				}

				enemyCounts[type]++;
				enemies.Add(enemy);
			}

			{
				Dictionary<Type, int> encountered = [];
				foreach ( Combatant enemy in enemies ) {
					Type type = enemy.GetType();

					if ( enemyCounts[type] < 2 )
						continue;

					if ( !encountered.ContainsKey(type) ) {
						encountered[type] = 0;
					}

					enemy.Suffix = LETTERS[encountered[type]];
					encountered[type]++;
				}
			}

			return new(enemies);
		}

		public override void OnEntered(DungeonGamePlayer player) {
			bool didVisit = isVisited;
			base.OnEntered(player);

			if ( didVisit ) {
				Game.dialogWindow.ShowDialog("You recall the great and vicious battle that you had here.");
			} else if ( player.Team != null ) {
				Game.dialogWindow.ShowDialog("Suddenly, a horde of enemies appears before you!");

				CombatEncounter battle = new(player.Team, AssembleEnemyTeam());
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
