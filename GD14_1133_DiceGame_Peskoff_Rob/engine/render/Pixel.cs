namespace GD14_1133_DiceGame_Peskoff_Rob.engine.render {
	internal struct Pixel {

		public readonly char content;
		public readonly ConsoleColor background;
		public readonly ConsoleColor foreground;

		public Pixel(char c = ' ', ConsoleColor bg = ConsoleColor.Black, ConsoleColor fg = ConsoleColor.White) {
			content = c;
			background = bg;
			foreground = fg;
		}

	}
}
