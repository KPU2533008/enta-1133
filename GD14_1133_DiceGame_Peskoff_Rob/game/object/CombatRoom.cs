using GD14_1133_DiceGame_Peskoff_Rob.game.util;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.@object {
	internal class CombatRoom : DungeonRoom {
		public override string GetRoomDescription() {
			return "Combat Room";
		}

		public override void OnEntered() {
			bool didVisit = isVisited;
			base.OnEntered();

			Sugar.Wait(3);
			if ( didVisit ) {
				Typewriter.Play(Game.dialogText, $"You recall the great and vicious battle that you had here.");
			} else {
				Typewriter.Play(Game.dialogText, $"Suddenly, a horde of enemies appears before you!");
				Sugar.Wait(2);

				DiceGameRunner diceGame = new();
				diceGame.RunGame();
				Typewriter.Play(Game.dialogText, $"You breathe a sigh of relief as the battle comes to an end.");
			}
		}

		public override void OnSearched() {
			base.OnSearched();
		}
	}
}
