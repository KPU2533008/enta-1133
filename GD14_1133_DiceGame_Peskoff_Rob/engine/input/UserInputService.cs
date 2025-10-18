using GD14_1133_DiceGame_Peskoff_Rob.engine.@object;

namespace GD14_1133_DiceGame_Peskoff_Rob.engine.input {
	internal class UserInputService {

		public static Signal<ConsoleKey> keyPressed = new();
		public static Signal<string> phraseEntered = new();
		public static string inProgressPhrase { get; private set; } = "";
		public delegate T InputProcessFn<T>(string input, out bool valid);

		private static void EnterPhrase() {
			phraseEntered.Fire(inProgressPhrase);
			inProgressPhrase = "";
		}

		public static void GetValidInput<T>(InputProcessFn<T> callback, out T output) {
			bool isValid = false;
			T outputInternal = default;

			Connection inputConn = phraseEntered.Connect((string phrase) => {
				outputInternal = callback(phrase, out isValid);
			});

			while ( !isValid ) {
				phraseEntered.Wait();
			}

			inputConn.Disconnect();
			output = outputInternal;
		}

		public static void PollInput() {
			while ( Console.KeyAvailable ) {
				ConsoleKeyInfo keyInfo = Console.ReadKey(true);
				ConsoleKey key = keyInfo.Key;

				keyPressed.Fire(key);

				if ( key == ConsoleKey.Enter ) {
					EnterPhrase();
				} else if ( key == ConsoleKey.Backspace ) {
					if ( inProgressPhrase.Length > 0 ) {
						inProgressPhrase = inProgressPhrase.Substring(0, inProgressPhrase.Length - 1);
					}
				} else {
					inProgressPhrase += keyInfo.KeyChar;
				}
			}
		}

	}
}
