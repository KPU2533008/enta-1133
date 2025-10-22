using GD14_1133_DiceGame_Peskoff_Rob.engine;
using GD14_1133_DiceGame_Peskoff_Rob.engine.datatype;
using GD14_1133_DiceGame_Peskoff_Rob.engine.@enum;
using GD14_1133_DiceGame_Peskoff_Rob.engine.input;
using GD14_1133_DiceGame_Peskoff_Rob.engine.instance;
using GD14_1133_DiceGame_Peskoff_Rob.engine.instance.gui;
using GD14_1133_DiceGame_Peskoff_Rob.game.@object;
using GD14_1133_DiceGame_Peskoff_Rob.game.util;
using System.Numerics;
using System.Reflection;

namespace GD14_1133_DiceGame_Peskoff_Rob.game {
	internal class DiceGameRunner {

		private List<DiceGamePlayer> players = new List<DiceGamePlayer>();
		private List<DiceGamePlayer> finalRanking = new List<DiceGamePlayer>();

		private Frame gameWindow = new();
		private TextLabel roundDisplay = new();
		private TextLabel centerText = new();
		private List<TextLabel> playerCards = new();

		public DiceGameRunner() {
			gameWindow.size = UDim2.FromScale(1, 1);
			gameWindow.borderEnabled = false;
			gameWindow.Parent = Game.mainViewport;

			roundDisplay.position = new(0.5f - 0.1f, 0, 0, 1);
			roundDisplay.size = new(0.2f, 0, 0, 3);
			roundDisplay.text = $"{new String('-', 100)}\n{new String('-', 100)}[[ Game Setup ]]{new String('-', 100)}\n{new String('-', 100)}";
			roundDisplay.clipsDescendants = true;
			roundDisplay.Parent = gameWindow;

			centerText.position = UDim2.FromScale(0.5f - 0.3f, 0.5f - ( 0.65f / 2 ));
			centerText.size = UDim2.FromScale(0.6f, 0.65f);
			centerText.textWrapped = true;
			centerText.textXAlignment = TextXAlignment.Left;
			centerText.Parent = gameWindow;
		}

		private void ShowRules() {

			bool doShowRules = false;

			Game.dialogWindow.ShowDialog("Would you like to see the rules?", ["Yes", "No"]);
			Game.dialogWindow.optionChosen.Once((int optionNum) => {
				doShowRules = optionNum == 0;
			});
			Game.dialogWindow.optionChosen.Wait();
			Game.dialogWindow.ClearText();

			if ( doShowRules ) {
				Typewriter.Play(centerText, "\nEvery player begins the game with an identical collection of dice, each with a different number of faces." +
				"\n\nEach round, every player chooses one of their dice to roll. The player who rolls the highest number wins the round and keeps all dice thrown that round except for their own die, which is thrown away." +
				"\n\nWhen a player runs out of dice, they are eliminated from the game." +
				"\nThe game continues until only one player remains. That player is the winner!", 2);
				Sugar.Wait(2);
				Game.dialogWindow.ShowDialog("Press enter to begin the game...");
				centerText.text = "";
			}
		}

		private bool RegisterPlayers() {
			Game.dialogWindow.ShowDialog("This game supports 2-4 players. How many will play this time, including non-human players?", false);

			UserInputService.GetValidInput((string input, out bool isValid) => {
				isValid = true;
				bool success = int.TryParse(input, out int parsed);

				if ( !success ) {
					isValid = false;
				}

				if ( Math.Clamp(parsed, 2, 4) != parsed ) {
					isValid = false;
				}

				return parsed;
			}, out int numPlayers);

			for ( int i = 0; i < numPlayers; i++ ) {
				Game.dialogWindow.ShowDialog($"What is player {i + 1}'s name? (max 7 characters)", false);

				UserInputService.GetValidInput((string input, out bool isValid) => {
					isValid = input.Length > 0;
					return input.Substring(0, Math.Min(input.Length, 7));
				}, out string playerName);

				bool isCpu = false;
				Game.dialogWindow.ShowDialog($"Is {playerName} a CPU?", ["Yes", "No"]);
				Game.dialogWindow.optionChosen.Once((int optionNum) => {
					isCpu = optionNum == 0;
				});
				Game.dialogWindow.optionChosen.Wait();

				players.Add(new DiceGamePlayer(playerName, isCpu));

				// TODO: Create ui for player cards to display game information

				//int offset = 
				//TextLabel playerCard = new();
				// ( ( 0 - 2 + 1) / 2 )
				//playerCard.position = UDim2.FromScale(0.5f + ( ( i - numPlayers + 1 ) / 2.0f ) * ( 0.65f / numPlayers ), 0.4f);
				//playerCard.size = UDim2.FromScale(0.2f, 0.2f);
				//playerCard.text = playerName;
				//playerCard.Parent = gameWindow;
			}

			// TODO [STRETCH]: Ask if all players look correct or if they'd like to start again

			return true;
		}

		private Dice PromptPlayerDieSelection(DiceGamePlayer player) {
			Dice chosenDie = player.SelectDieForTurn();
			Game.dialogWindow.ShowDialog($"\n{player.ToString()} has chosen their {chosenDie.GetDieType()}!");

			return chosenDie;
		}

		private List<DiceGamePlayer> GetRemainingPlayers() {
			List<DiceGamePlayer> remainingPlayers = new List<DiceGamePlayer>();
			foreach ( DiceGamePlayer player in players ) {
				if ( !player.IsEliminated() ) {
					remainingPlayers.Add(player);
				}
			}
			return remainingPlayers;
		}

		private void RunSingleRound(int roundNum) {
			roundDisplay.text = $"{new String('-', 100)}\n{new String('-', 100)}[[ ROUND {roundNum} ]]{new String('-', 100)}\n{new String('-', 100)}";

			List<DiceGamePlayer> remainingPlayers = GetRemainingPlayers();
			Dictionary<DiceGamePlayer, Dice> rolledDice = new();

			for ( int i = 0; i < remainingPlayers.Count; i++ ) {
				DiceGamePlayer player = remainingPlayers[i];

				Game.dialogWindow.ShowDialog($"[ {player.ToString()}'s Turn ]\n{player.ToString()}, you're up! Choose a die to throw this round!");
				Dice chosenDice = PromptPlayerDieSelection(player);

				Game.dialogWindow.ShowDialog($"Alright, let's roll that die!");
				Game.dialogWindow.ShowDialog($"And the result is...");
				chosenDice.Roll();
				rolledDice.Add(player, chosenDice);
				player.totalRoll += chosenDice.GetLastRoll();
				Game.dialogWindow.ShowDialog($"{rolledDice[player].GetLastRoll()}!");
			}

			KeyValuePair<DiceGamePlayer, int> highestRoll = KeyValuePair.Create(remainingPlayers[0], rolledDice[remainingPlayers[0]].GetLastRoll());

			foreach ( KeyValuePair<DiceGamePlayer, Dice> rolledDie in rolledDice ) {
				if ( rolledDie.Key == highestRoll.Key )
					continue;

				int dieRoll = rolledDie.Value.GetLastRoll();

				if ( dieRoll > highestRoll.Value ) {
					highestRoll = KeyValuePair.Create(rolledDie.Key, dieRoll);
				}
			}

			Game.dialogWindow.ShowDialog($"{highestRoll.Key.ToString()} wins Round {roundNum} and collects all other players' dice!");
			foreach ( KeyValuePair<DiceGamePlayer, Dice> rolledDie in rolledDice ) {
				if ( rolledDie.Key == highestRoll.Key )
					continue;
				highestRoll.Key.AddDice(rolledDie.Value);
			}

			for ( int i = 0; i < remainingPlayers.Count; i++ ) {
				DiceGamePlayer player = remainingPlayers[i];

				if ( player == highestRoll.Key ) {
					player.roundsWon++;
				} else {
					player.roundsLost++;
				}

				if ( player.IsEliminated() ) {
					finalRanking.Add(player);
					Game.dialogWindow.ShowDialog($"{player.ToString().ToUpper()}, YOU ARE OUT OF DICE! You have been ELIMINATED!");
				}
			}
		}

		private void RunGameLoop() {
			Game.dialogWindow.ShowDialog("Let's begin!");

			int roundNum = 1;

			while ( GetRemainingPlayers().Count > 1 ) {
				RunSingleRound(roundNum);
				roundNum++;
			}

			List<DiceGamePlayer> remainingPlayers = GetRemainingPlayers();
			DiceGamePlayer winner = remainingPlayers[0];

			Game.dialogWindow.ShowDialog($"After {roundNum} rounds, we have a winner!");
			Game.dialogWindow.ShowDialog($"And our winner is...");
			Game.dialogWindow.ShowDialog($"{winner.ToString().ToUpper()}!!!");
			finalRanking.Add(winner);
		}

		// TODO: Convert this to use the new UI system

		private void ShowFinalGameStats() {
			//Display.PrintLn();
			//Display.PrintHeader(2, "Final Stats");
			//Display.PrintTypewriter("\nHere are your final game stats:");

			//Display.Print("\n\nPLAYER\t\tWINS\t\tLOSSES\t\tTOTAL ROLL\t\tAVG. ROLL\t\tDICE COLLECTED");

			//for ( int i = finalRanking.Count - 1; i >= 0; i-- ) {
			//	Player player = finalRanking[i];
			//	string playerStats = $"\n{( finalRanking.Count - i )}) {player.ToString()}\t\t{player.roundsWon}\t\t{player.roundsLost}\t\t{player.totalRoll}\t\t{player.totalRoll / ( player.roundsWon + player.roundsLost )}\t\t{player.totalDiceCollected}";
			//	Display.Print(playerStats);
			//}
		}

		internal void RunGame() {

			RegisterPlayers();

			ShowRules();

			RunGameLoop();

			ShowFinalGameStats();

			Game.dialogWindow.ShowDialog($"Press enter to continue...");
			gameWindow.Destroy();
			Game.dialogWindow.ClearText();
		}


	}
}
