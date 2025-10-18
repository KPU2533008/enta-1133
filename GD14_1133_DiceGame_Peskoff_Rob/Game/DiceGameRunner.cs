using GD14_1133_DiceGame_Peskoff_Rob.Game.Object;
using GD14_1133_DiceGame_Peskoff_Rob.Game.Util;

namespace GD14_1133_DiceGame_Peskoff_Rob.Game {
	internal class DiceGameRunner {

		private List<Player> players = new List<Player>();
		private List<Player> finalRanking = new List<Player>();

		// This is a bit funky but it's better than checking each of these individually each time
		private string[] ALLOWED_CONFIRM_STRINGS = ["yes", "confirm", "true", "y"];
		private string[] ALLOWED_REJECT_STRINGS = ["no", "reject", "false", "n"];

		internal void ShowRules() {
			bool doShowRules = false;

			while ( true ) {
				Display.PrintTypewriter("\nWould you like to see the rules? (y/n)\n");
				string showRulesInput = ( Console.ReadLine() ?? "" ).ToLower();
				if ( ALLOWED_CONFIRM_STRINGS.Contains(showRulesInput) || ALLOWED_REJECT_STRINGS.Contains(showRulesInput) ) {
					doShowRules = ALLOWED_CONFIRM_STRINGS.Contains(showRulesInput);
					break;
				}
			}

			if ( doShowRules ) {
				Display.PrintLn();
				Display.PrintHeader(2, "Rules");
				Display.PrintTypewriter("\nEvery player begins the game with a collection of dice with different numbers of faces.");
				Display.PrintTypewriter("\n\nEach round, every player chooses one of their dice to roll. The player who rolls the");
				Display.PrintTypewriter("\nhighest number wins the round and keeps all dice thrown that round except for their");
				Display.PrintTypewriter("\nown die, which is thrown away.");
				Display.PrintTypewriter("\n\nWhen a player runs out of dice, they are eliminated from the game.");
				Display.PrintTypewriter("\nThe game continues until only one player remains. That player is the winner!");
				Sugar.Wait(3);
				Display.PrintTypewriter("\n\nPress enter to begin the game...");
				Console.ReadLine();
			}
		}

		private bool RegisterPlayers() {
			int numPlayers = -1;

			while ( true ) {
				Display.PrintTypewriterLn("\nHow many players will play this game? (including non-human players)");
				string input = Console.ReadLine() ?? "";
				bool success = int.TryParse(input, out int parsed);

				if ( success ) {
					if ( parsed > 1 && parsed <= 4 ) {
						numPlayers = parsed;
						break;
					} else {
						Display.PrintTypewriter("This game only supports 2 - 4 players!\n");
					}
				} else {
					Display.PrintTypewriter("Please enter a valid number!\n");
				}

				Sugar.Wait(0.5f);
			}

			for ( int i = 0; i < numPlayers; i++ ) {
				Display.PrintTypewriter($"\nPlease enter player {i + 1}'s details:");
				Display.PrintTypewriter("\nName (max 7 chars): ");

				string nameInput = Console.ReadLine() ?? "";
				string name = nameInput == "" ? $"Player{i + 1}" : nameInput;

				if ( name.Length > 7 ) {
					name = name.Substring(0, 7);
				}

				bool isCpu = false;

				while ( true ) {
					Display.PrintTypewriter("Is CPU (y/n): ");
					string isHumanInput = ( Console.ReadLine() ?? "" ).ToLower();
					if ( ALLOWED_CONFIRM_STRINGS.Contains(isHumanInput) || ALLOWED_REJECT_STRINGS.Contains(isHumanInput) ) {
						isCpu = ALLOWED_CONFIRM_STRINGS.Contains(isHumanInput);
						break;
					}
				}

				players.Add(new Player(name, isCpu));
			}

			// TODO [STRETCH]: Ask if all players look correct or if they'd like to start again

			return true;
		}

		private Dice PromptPlayerDieSelection(Player player) {
			Sugar.Wait(0.5f);

			Dice chosenDie = player.SelectDieForTurn();
			Display.PrintTypewriter($"\n{player.ToString()} has chosen their {chosenDie.GetDieType()}!");

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
			Display.Print("\n\n");
			Display.PrintHeader(2, $"ROUND {roundNum}");

			List<Player> remainingPlayers = GetRemainingPlayers();
			Dictionary<Player, Dice> rolledDice = new();

			for ( int i = 0; i < remainingPlayers.Count; i++ ) {
				Player player = remainingPlayers[i];
				Display.PrintLn();
				Display.PrintHeader(3, $"{player.ToString()}'s Turn");
				Display.PrintTypewriter($"\n{player.ToString()}, you're up! Choose a die to throw this round!");
				Dice chosenDice = PromptPlayerDieSelection(player);

				Sugar.Wait(1);
				Display.PrintTypewriter("\nAlright, let's roll that die!");
				Sugar.Wait(1);
				Display.PrintTypewriter("\nAnd the result is...");
				Sugar.Wait(2);
				chosenDice.Roll();
				rolledDice.Add(player, chosenDice);
				player.totalRoll += chosenDice.GetLastRoll();
				Display.PrintTypewriter($"\n{rolledDice[player].GetLastRoll()}!");
				Sugar.Wait(1);
				Display.PrintLn();
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

			Display.PrintTypewriter($"\n{highestRoll.Key.ToString()} wins Round {roundNum} and collects all other players' dice!");
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
					Display.PrintTypewriter($"\n\n{player.ToString().ToUpper()}, YOU ARE OUT OF DICE! You have been ELIMINATED!");
					Sugar.Wait(1);
				}
			}

			Sugar.Wait(2);
		}

		private void RunGameLoop() {
			Display.PrintTypewriter("\nLet's begin!");
			Sugar.Wait(1);

			int roundNum = 1;

			while ( GetRemainingPlayers().Count > 1 ) {
				RunSingleRound(roundNum);
				roundNum++;
			}

			List<Player> remainingPlayers = GetRemainingPlayers();
			Player winner = remainingPlayers[0];

			Display.PrintTypewriter($"\n\nAfter {roundNum} rounds, we have a winner!");
			Sugar.Wait(1);
			Display.PrintTypewriter("\nAnd our winner is...");
			Sugar.Wait(1);
			Display.PrintTypewriter($"\n{winner.ToString().ToUpper()}!!!");
			finalRanking.Add(winner);
		}

		private void ShowFinalGameStats() {
			Display.PrintLn();
			Display.PrintHeader(2, "Final Stats");
			Display.PrintTypewriter("\nHere are your final game stats:");

			Display.Print("\n\nPLAYER\t\tWINS\t\tLOSSES\t\tTOTAL ROLL\t\tAVG. ROLL\t\tDICE COLLECTED");

			for ( int i = finalRanking.Count - 1; i >= 0; i-- ) {
				Player player = finalRanking[i];
				string playerStats = $"\n{( finalRanking.Count - i )}) {player.ToString()}\t\t{player.roundsWon}\t\t{player.roundsLost}\t\t{player.totalRoll}\t\t{player.totalRoll / ( player.roundsWon + player.roundsLost )}\t\t{player.totalDiceCollected}";
				Display.Print(playerStats);
			}
		}

		internal void RunGame() {
			Console.WriteLine("");
			Display.PrintTypewriterLn("Welcome to DIRTY DICE!\n");

			Sugar.Wait(1);

			Display.PrintHeader(2, "Game Setup");
			RegisterPlayers();
			Sugar.Wait(1);

			ShowRules();

			RunGameLoop();

			Sugar.Wait(2);

			ShowFinalGameStats();

			Sugar.Wait(1);

			Display.PrintTypewriter("\n\nPress enter to return to the main menu...\n");
			Console.ReadLine();
		}

	}
}
