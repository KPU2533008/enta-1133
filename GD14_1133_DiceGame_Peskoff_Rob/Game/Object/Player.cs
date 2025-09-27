using GD14_1133_DiceGame_Peskoff_Rob.Game.Util;

namespace GD14_1133_DiceGame_Peskoff_Rob.Game.Object {
	internal class Player {

		internal string playerName;
		internal bool isHuman = true;
		internal int score = 0;
		internal List<Dice> dice = [new(6), new(8), new(12), new(20)];

		public Player() {
			Display.PrintTypewriterLn("PLEASE ENTER YOUR NAME...");
			string input = Console.ReadLine() ?? "";
			playerName = input == "" ? "Rule Breaker" : input;
		}

		public Player(string name) {
			playerName = name;
			isHuman = false;
		}

		public override string ToString() {
			return playerName;
		}

		internal void AddToScore(int amount) {
			score += amount;
		}

	}
}
