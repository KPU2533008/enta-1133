namespace GD14_1133_DiceGame_Peskoff_Rob.game.@object {
	internal class Dice {

		private readonly int faces;
		private readonly int numDice;
		private int lastRoll = -1;

		internal Dice(int faces = 6, int numDice = 1) {
			this.faces = faces;
			this.numDice = numDice;
		}

		public override string ToString() {
			return $"{numDice}{GetDieType()}[{lastRoll}]";
		}

		internal string GetDieType() {
			return "d" + faces;
		}

		internal int Roll() {
			Random rng = new Random();
			int roll = 0;

			for ( int i = 0; i < numDice; i++ ) {
				roll += rng.Next(0, faces) + 1;
			}

			lastRoll = roll;
			return roll;
		}

		internal int GetLastRoll() {
			return lastRoll;
		}
	}
}
