using GD14_1133_DiceGame_Peskoff_Rob.Game.Object;
using GD14_1133_DiceGame_Peskoff_Rob.Game.Util;
using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;

namespace GD14_1133_DiceGame_Peskoff_Rob.Game {
	internal class DiceGameRunner {

		private static string[] badListener = [
			"WOW, OUR CHALLENGER DIDN'T FOLLOW THE INSTRUCTIONS AT ALL!!! GO AHEAD AND TRY THAT AGAIN FOR ME...!!!",
			"OUR NEWEST CHALLENGER NEEDS TO GET THEIR EARS CHECKED, BECAUSE THEY HAVE TROUBLE LISTENING...!!!",
			"IT MUST BE OPPOSITE DAY BECAUSE OUR CHALLENGER DID THE POLAR OPPOSITE OF WHAT THEY WERE ASKED!! LET'S TRY IT AGAIN...!",
			"*ALREADY* OUR CHALLENGER IS USING MIND GAMES TO TRY AND THROW OFF THE COMPETITION!!! BUT I NEED YOU TO FOLLOW INSTRUCTIONS, SO GIVE IT ANOTHER SHOT!!!"
		];

		internal void ShowIntroText(string playerName) {
			Sugar.Wait(0.3f);
			Display.PrintTypewriter($"\nEVERYONE PLEASE WELCOME OUR NEWEST CHALLENGER");
			Display.PrintTypewriter("...", 1);
			Display.PrintTypewriter($" {playerName}", 1.0f / 120);
			Display.PrintTypewriter("!!!!!!!!!!!!!!!!", 0.1f);

			if ( GameSettings.FLAVOR_TEXT_ENABLED ) {
				Sugar.Wait(1);
				Display.PrintTypewriter($"\nTODAY, ON THIS BEAUTIFUL {Strings.UCase(DateTime.Now.ToString("MMMM"))} DAY, YOU'LL BE GOING HEAD TO HEAD AGAINST OUR REIGNING CHAMP");
				Display.PrintTypewriter("...", 1);
				Sugar.Wait(1);
				Display.PrintTypewriter("\n\nCHRISTINA PERSEPHONE UMBELTON OF THE APOSTLES OF MAGIC DICE");
				Display.PrintTypewriter("!!!!!!!", 0.1f);
				Sugar.Wait(1);
				Display.PrintTypewriter("\n\nAKA...");
				Sugar.Wait(1);
				Display.PrintTypewriter("\n\nTHE NOTORIOUS", 1.0f / 120);
				Sugar.Wait(1);
				Display.PrintTypewriter("\nCPU", 1, true);
				Display.Print(" OF ");
				Sugar.Wait(1);
				Display.PrintTypewriter("AMD", 1, false);
				Display.PrintTypewriter("!!!!!!!", 0.1f);
			}

			Sugar.Wait(2);
		}

		internal string PromptCoinFaceCall(string playerName) {
			Random rng = new();
			string coinCall = "";

			Display.PrintTypewriter("\n\nBUT FIRST, LET'S DECIDE TURN ORDER WITH A COIN FLIP!!!");
			Sugar.Wait(1);
			Display.PrintTypewriter($"\n{playerName}, GO AHEAD AND CALL HEADS OR TAILS!!!\n");
			Sugar.Wait(1);

			while ( true ) {
				string? input = Console.ReadLine();
				Display.PrintLn("");
				coinCall = Strings.LCase(input ?? "");

				if ( coinCall == "heads" || coinCall == "tails" ) {
					break;
				}

				Display.PrintTypewriter($"{badListener[rng.Next(0, badListener.Length)]}", 1.0f / 120);
				Sugar.Wait(1);
				Display.PrintTypewriter($"\n{playerName}, GO AHEAD AND CALL HEADS OR TAILS!!!\n");
			}

			return coinCall;
		}

		internal Dice PromptPlayerDieSelection(Player player) {
			Sugar.Wait(0.5f);

			int chosenDieNum = -1;

			if ( player.isHuman ) {
				Display.PrintTypewriter($"\nHERE ARE YOUR AVAILABLE DICE:");
				for ( int i = 0; i < player.dice.Count; i++ ) {
					Display.PrintTypewriter($"\n{i + 1}) {player.dice[i].GetDieType()}");
				}

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
				while ( true ) {
					Display.Print("\n");
					string input = Console.ReadLine() ?? "";
					int parsed;
					bool success = int.TryParse(input, out parsed);

					if ( success ) {
						if ( Math.Clamp(chosenDieNum, 1, player.dice.Count) == chosenDieNum ) {
							chosenDieNum = parsed - 1;
							break;
						}
					} else {
						for ( int i = 0; i < player.dice.Count; i++ ) {
							if ( player.dice[i].GetDieType() == input ) {
								chosenDieNum = i;
								break;
							}
						}
					}

					if ( chosenDieNum != -1 )
						break;

					Display.PrintTypewriter($"\nPLEASE CHOOSE ONE OF THE DICE ABOVE!");
				}
			} else {
				Sugar.Wait(1);
				Random rng = new();
				chosenDieNum = rng.Next(0, player.dice.Count);
				Display.Print("\n");
			}

			Dice chosenDie = player.dice[chosenDieNum];
			player.dice.RemoveAt(chosenDieNum);
			Display.PrintTypewriter($"\n{player.ToString().ToUpper()} HAS CHOSEN THEIR {chosenDie.GetDieType().ToUpper()}!!!!!");

			return chosenDie;
		}

		internal void RunGame() {
			Display.PrintTypewriter("\nWELCOME TO THE DICE GAME!!!");
			Sugar.Wait(1);
			Display.Print("\n\n");

			Random rng = new();
			Player player1 = new();
			Player player2 = new("CPU");
			List<Player> playerTurnOrder = [];

			string uppercaseP1 = Strings.UCase(player1.ToString());

			ShowIntroText(uppercaseP1);
			string coinCall = PromptCoinFaceCall(uppercaseP1);

			Display.PrintTypewriter($"THANKS YOU VERY MUCH!!! OUR CHALLENGER HAS CALLED {Strings.UCase(coinCall)}!!! LET'S FLIP THAT COIN!!!");
			Sugar.Wait(1);
			Display.PrintTypewriter("\nAND THE RESULT IS...");

			Sugar.Wait(1);
			string coinFlipResult = rng.Next(0, 2) == 0 ? "heads" : "tails";
			Display.PrintTypewriter($"\n\n{Strings.UCase(coinFlipResult)}!!!");
			Sugar.Wait(1.25f);

			if ( coinFlipResult == coinCall ) {
				Display.PrintTypewriter("\n\nCONGRATULATIONS CHALLENGER, YOU GET TO GO FIRST!!!");
				playerTurnOrder.Add(player1);
				playerTurnOrder.Add(player2);
			} else {
				Display.PrintTypewriter("\n\nOUCH... TOO BAD CHALLENGER, LOOKS LIKE THE REIGNING CHAMP IS UP FIRST!!!");
				playerTurnOrder.Add(player2);
				playerTurnOrder.Add(player1);
			}

			Sugar.Wait(1);

			// TODO: Put this all into a loop
			Dictionary<Player, int> rolls = new();

			for ( int i = 0; i < playerTurnOrder.Count; i++ ) {
				Player player = playerTurnOrder[i];
				Display.PrintTypewriter($"\n\n{player.ToString().ToUpper()}, IT'S YOUR ROLL! CHOOSE A DIE TO THROW!");
				Dice chosenDice = PromptPlayerDieSelection(player);

				Sugar.Wait(1);
				Display.PrintTypewriter("\nALRIGHT, LET'S ROLL THAT DIE!!!!");
				Sugar.Wait(1);
				Display.PrintTypewriter("\nAND THE RESULT IS...");
				Sugar.Wait(1);
				rolls.Add(player, chosenDice.Roll());
				Display.PrintTypewriter($"\n\n{rolls[player]}!!!!!!!");
				Sugar.Wait(1);
			}

			// Highest roll is automatically assigned to the player who won the coin toss.
			// We do this so that in the case of a tie, the coin toss winner wins the round.
			KeyValuePair<Player, int> highestRoll = KeyValuePair.Create(playerTurnOrder[0], rolls[playerTurnOrder[0]]);

			foreach ( KeyValuePair<Player, int> roll in rolls ) {
				if ( roll.Key == highestRoll.Key )
					continue;

				if ( roll.Value > highestRoll.Value ) {
					highestRoll = roll;
				} else if ( roll.Value == highestRoll.Value ) {
					Display.PrintTypewriter($"\n\nIT APPEARS AS THOUGH THERE IS A TIE... THEREFORE THIS ROUND GOES TO THE WINNER OF THE COIN TOSS...");
				}
			}

			Display.PrintTypewriter($"\nTHE WINNER IS... {highestRoll.Key}!!!!!!!");
		}

	}
}
