namespace GD14_1133_DiceGame_Peskoff_Rob.input {
	internal class InputHandler {

		private static int LOG_SIZE = 4;
		private static List<string> inputLog = new List<string>(LOG_SIZE);

		public static string ToString() {
			string str = "";

			for ( int i = 0; i < inputLog.Count; i++ ) {
				str += "> " + inputLog[i] + "\n";
			}

			return str;
		}

		public static string ReadInput() {
			string input = Console.ReadLine() ?? "";

			inputLog.Add(input);
			if ( inputLog.Count > LOG_SIZE ) {
				inputLog.RemoveAt(0);
			}

			return input;
		}

	}
}
