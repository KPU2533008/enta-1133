namespace GD14_1133_DiceGame_Peskoff_Rob.Game.Util {
	internal class Display {

		private static float TYPEWRITER_DEFAULT = 1.0f / 60.0f;

		// Typing Console.WriteLine over and over is really annoying
		public static void Print(string msg = "") {
			Console.Write(msg);
		}

		public static void PrintLn(string msg = "") {
			Console.WriteLine(msg);
		}

		public static void PrintTypewriter(string msg, float delay = 1.0f / 60.0f, bool trailingWait = true) {
			for ( int i = 0; i < msg.Length; i++ ) {
				Print(msg.Substring(i, 1));

				if ( !GameSettings.TYPEWRITER_ENABLED )
					continue;
				if ( msg.Substring(i, 1) == "\n" )
					continue;
				if ( i >= msg.Length - 1 && !trailingWait )
					continue;

				Sugar.Wait(delay);
			}
		}

		public static void PrintTypewriterLn(string msg, float delay = 1.0f / 60.0f, bool trailingWait = true) {
			PrintTypewriter(msg, delay, trailingWait);
			Print("\n");
		}

	}
}
