using System.Diagnostics;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.@object {
	internal class DiceGamePlayer {

		private string playerName;
		private bool isHuman;
		private List<Dice> inventory = [new(6), new(8), new(12), new(20)];

		internal int roundsWon = 0;
		internal int roundsLost = 0;
		internal int totalRoll = 0;
		internal int totalDiceCollected = 0;

		public DiceGamePlayer(string name, bool isCpu = true) {
			playerName = name;
			isHuman = !isCpu;
		}

		public override string ToString() {
			return playerName;
		}

		internal bool IsEliminated() {
			return inventory.Count == 0;
		}

		internal void AddDice(Dice dice) {
			totalDiceCollected++;
			inventory.Add(dice);
		}

		internal void RemoveDieAt(int index) {
			inventory.RemoveAt(index);
		}

		private int PromptHumanDiceSelection() {
			string[] options = new string[inventory.Count];
			for ( int i = 0; i < inventory.Count; i++ ) {
				options[i] = inventory[i].GetDieType();
			}

			int chosenDie = -1;

			Game.dialogWindow.ShowOptions(options);
			Game.dialogWindow.optionChosen.Once((int optionNum) => {
				chosenDie = optionNum;
			});
			Game.dialogWindow.optionChosen.Wait();

			Debug.WriteLine(chosenDie);
			return chosenDie;
		}

		internal Dice SelectDieForTurn() {
			int selectedDieNum = -1;

			if ( isHuman ) {
				selectedDieNum = PromptHumanDiceSelection();
			} else {
				Random rng = new();
				selectedDieNum = rng.Next(1, inventory.Count) - 1;
			}

			Dice selectedDie = inventory[selectedDieNum];
			RemoveDieAt(selectedDieNum);

			return selectedDie;
		}

	}
}
