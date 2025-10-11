using GD14_1133_DiceGame_Peskoff_Rob.engine.input;
using GD14_1133_DiceGame_Peskoff_Rob.game.util;
using System.Numerics;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.@object {
	internal class Player {

		private string playerName;
		private bool isHuman;
		private List<Dice> inventory = [new(6), new(8), new(12), new(20)];

		internal int roundsWon = 0;
		internal int roundsLost = 0;
		internal int totalRoll = 0;
		internal int totalDiceCollected = 0;

		public Player(string name, bool isCpu = true) {
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

		private void ShowAvailableDice() {
			string availableDice = $"Here are your available dice:\n(1) {inventory[0].GetDieType()}";
			for ( int i = 1; i < inventory.Count; i++ ) {
				availableDice += $", ({i + 1}) {inventory[i].GetDieType()}";
			}
			Typewriter.Play(Game.dialogText, availableDice);
		}

		private int PromptHumanDiceSelection() {
			/*
			 * Input validation:
			 * Continually loop, grabbing player input. If they enter something that parses to an int,
			 * check to make sure that there's actually a die in that position in the list. For example,
			 * if they enter -1, we shouldn't accept that even though it's a valid int. Conversely, if
			 * their input doesn't parse to int, run through all the dice in their list of available dice
			 * and see if what they entered matches that die's type. If it does, we'll select that die.
			 * 
			 * Once we have a valid input in-hand and a die chosen, break the loop.
			 */
			UserInputService.GetValidInput((string input, out bool isValid) => {
				int selectedDieNum = -1;
				isValid = false;

				bool success = int.TryParse(input, out int parsed);

				if ( success ) {
					if ( Math.Clamp(parsed, 1, inventory.Count) == parsed ) {
						isValid = true;
						selectedDieNum = parsed - 1;
					}
				} else {
					for ( int i = 0; i < inventory.Count; i++ ) {
						if ( inventory[i].GetDieType() == input ) {
							isValid = true;
							selectedDieNum = i;
						}
					}
				}

				return selectedDieNum;
			}, out int selectedDieNum);

			return selectedDieNum;
		}

		internal Dice SelectDieForTurn() {
			int selectedDieNum = -1;

			if ( isHuman ) {
				ShowAvailableDice();
				selectedDieNum = PromptHumanDiceSelection();
			} else {
				Random rng = new();
				selectedDieNum = rng.Next(1, inventory.Count) - 1;
				Sugar.Wait(1);
			}

			Dice selectedDie = inventory[selectedDieNum];
			RemoveDieAt(selectedDieNum);

			return selectedDie;
		}

	}
}
