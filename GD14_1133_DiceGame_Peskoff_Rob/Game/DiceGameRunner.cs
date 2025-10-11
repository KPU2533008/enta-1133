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

		private List<Player> players = new List<Player>();
		private List<Player> finalRanking = new List<Player>();

		// This is a bit funky but it's better than checking each of these individually each time
		private string[] ALLOWED_CONFIRM_STRINGS = ["yes", "confirm", "true", "y"];
		private string[] ALLOWED_REJECT_STRINGS = ["no", "reject", "false", "n"];

		private Frame gameWindow;
		private TextLabel roundDisplay;
		private TextLabel centerText;
		private List<TextLabel> playerCards = new();

		public DiceGameRunner() {
			gameWindow = new();
			gameWindow.size = UDim2.FromScale(1, 1);
			gameWindow.borderEnabled = false;
			gameWindow.Parent = Game.mainViewport;

			roundDisplay = new();
			roundDisplay.position = new(0.5f - 0.1f, 0, 0, 1);
			roundDisplay.size = new(0.2f, 0, 0, 3);
			roundDisplay.text = $"{new String('-', 100)}\n{new String('-', 100)}[[ Game Setup ]]{new String('-', 100)}\n{new String('-', 100)}";
			roundDisplay.clipsDescendants = true;
			roundDisplay.Parent = gameWindow;

			centerText = new();
			centerText.position = UDim2.FromScale(0.5f - 0.3f, 0.5f - ( 0.65f / 2 ));
			centerText.size = UDim2.FromScale(0.6f, 0.65f);
			centerText.textWrapped = true;
			centerText.textXAlignment = TextXAlignment.Left;
			centerText.Parent = gameWindow;
		}

		private void ShowRules() {

			Typewriter.Play(Game.dialogText, "Would you like to see the rules? (y/n)");
			UserInputService.GetValidInput((string input, out bool isValid) => {
				isValid = ALLOWED_CONFIRM_STRINGS.Contains(input) || ALLOWED_REJECT_STRINGS.Contains(input);
				return ALLOWED_CONFIRM_STRINGS.Contains(input.ToLower());
			}, out bool doShowRules);

			Game.dialogText.text = "";

			if ( doShowRules ) {
				Typewriter.Play(centerText, "\nEvery player begins the game with an identical collection of dice, each with a different number of faces." +
				"\n\nEach round, every player chooses one of their dice to roll. The player who rolls the highest number wins the round and keeps all dice thrown that round except for their own die, which is thrown away." +
				"\n\nWhen a player runs out of dice, they are eliminated from the game." +
				"\nThe game continues until only one player remains. That player is the winner!", 2);
				Sugar.Wait(3);
				Typewriter.Play(Game.dialogText, "Press enter to begin the game...");
				UserInputService.phraseEntered.Wait();
				centerText.text = "";
			}
		}

		private bool RegisterPlayers() {
			Typewriter.Play(Game.dialogText, "This game supports 2-4 players. How many will play this time, including non-human players?");

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
				Typewriter.Play(Game.dialogText, $"What is player {i + 1}'s name? (max 7 characters)");

				UserInputService.GetValidInput((string input, out bool isValid) => {
					isValid = input.Length > 0;
					return input.Substring(0, Math.Min(input.Length, 7));
				}, out string playerName);

				Typewriter.Play(Game.dialogText, $"Is {playerName} a CPU? (y/n)");

				UserInputService.GetValidInput((string input, out bool isValid) => {
					isValid = ALLOWED_CONFIRM_STRINGS.Contains(input) || ALLOWED_REJECT_STRINGS.Contains(input);
					return ALLOWED_CONFIRM_STRINGS.Contains(input.ToLower());
				}, out bool isCpu);

				players.Add(new Player(playerName, isCpu));

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

		private Dice PromptPlayerDieSelection(Player player) {
			Sugar.Wait(0.5f);

			Dice chosenDie = player.SelectDieForTurn();
			Typewriter.Play(Game.dialogText, $"\n{player.ToString()} has chosen their {chosenDie.GetDieType()}!");

			return chosenDie;
		}

		private List<Player> GetRemainingPlayers() {
			List<Player> remainingPlayers = new List<Player>();
			foreach ( Player player in players ) {
				if ( !player.IsEliminated() ) {
					remainingPlayers.Add(player);
				}
			}
			return remainingPlayers;
		}

		private void RunSingleRound(int roundNum) {
			roundDisplay.text = $"{new String('-', 100)}\n{new String('-', 100)}[[ ROUND {roundNum} ]]{new String('-', 100)}\n{new String('-', 100)}";

			List<Player> remainingPlayers = GetRemainingPlayers();
			Dictionary<Player, Dice> rolledDice = new();

			for ( int i = 0; i < remainingPlayers.Count; i++ ) {
				Player player = remainingPlayers[i];

				Typewriter.Play(Game.dialogText, $"[ {player.ToString()}'s Turn ]\n{player.ToString()}, you're up! Choose a die to throw this round!");
				Sugar.Wait(3);
				Dice chosenDice = PromptPlayerDieSelection(player);

				Sugar.Wait(2.5f);
				Typewriter.Play(Game.dialogText, $"Alright, let's roll that die!");
				Sugar.Wait(2);
				Typewriter.Play(Game.dialogText, $"And the result is...");
				Sugar.Wait(2);
				chosenDice.Roll();
				rolledDice.Add(player, chosenDice);
				player.totalRoll += chosenDice.GetLastRoll();
				Typewriter.Play(Game.dialogText, $"{rolledDice[player].GetLastRoll()}!");
				Sugar.Wait(2.5f);
			}

			KeyValuePair<Player, int> highestRoll = KeyValuePair.Create(remainingPlayers[0], rolledDice[remainingPlayers[0]].GetLastRoll());

			foreach ( KeyValuePair<Player, Dice> rolledDie in rolledDice ) {
				if ( rolledDie.Key == highestRoll.Key )
					continue;

				int dieRoll = rolledDie.Value.GetLastRoll();

				if ( dieRoll > highestRoll.Value ) {
					highestRoll = KeyValuePair.Create(rolledDie.Key, dieRoll);
				}
			}

			Typewriter.Play(Game.dialogText, $"{highestRoll.Key.ToString()} wins Round {roundNum} and collects all other players' dice!");
			Sugar.Wait(3);
			foreach ( KeyValuePair<Player, Dice> rolledDie in rolledDice ) {
				if ( rolledDie.Key == highestRoll.Key )
					continue;
				highestRoll.Key.AddDice(rolledDie.Value);
			}

			for ( int i = 0; i < remainingPlayers.Count; i++ ) {
				Player player = remainingPlayers[i];

				if ( player == highestRoll.Key ) {
					player.roundsWon++;
				} else {
					player.roundsLost++;
				}

				if ( player.IsEliminated() ) {
					finalRanking.Add(player);
					Typewriter.Play(Game.dialogText, $"{player.ToString().ToUpper()}, YOU ARE OUT OF DICE! You have been ELIMINATED!");
					Sugar.Wait(3);
				}
			}

			Sugar.Wait(1);
		}

		private void RunGameLoop() {
			Typewriter.Play(Game.dialogText, "Let's begin!");
			Sugar.Wait(1);

			int roundNum = 1;

			while ( GetRemainingPlayers().Count > 1 ) {
				RunSingleRound(roundNum);
				roundNum++;
			}

			List<Player> remainingPlayers = GetRemainingPlayers();
			Player winner = remainingPlayers[0];

			Sugar.Wait(2.5f);
			Typewriter.Play(Game.dialogText, $"After {roundNum} rounds, we have a winner!");
			Sugar.Wait(2.5f);
			Typewriter.Play(Game.dialogText, $"And our winner is...");
			Sugar.Wait(2);
			Typewriter.Play(Game.dialogText, $"{winner.ToString().ToUpper()}!!!");
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
			Sugar.Wait(1);

			RegisterPlayers();
			Sugar.Wait(1);

			ShowRules();

			RunGameLoop();

			Sugar.Wait(2);

			ShowFinalGameStats();

			Sugar.Wait(1);

			Typewriter.Play(Game.dialogText, $"Press enter to continue...");
			UserInputService.phraseEntered.Wait();
			gameWindow?.Destroy();
			Game.dialogText.text = "";
		}


	}
}
