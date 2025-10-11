namespace GD14_1133_DiceGame_Peskoff_Rob.engine.render {
	internal class Canvas {

		public readonly int width;
		public readonly int height;
		private char[,] pixels;
		//private Pixel[,] pixels;

		/*
		 * We need to keep resolution relatively low. We're running this
		 * completely serially on a single CPU thread, not in parallel on
		 * a fast and beefy GPU with vertex shaders and fragment shaders.
		 * 
		 * Maybe if we want to squeeze some extra perf out of this, we
		 * could explore multi-threading it and giving each row its own
		 * thread on the CPU.
		 */
		public Canvas(int width, int height) {
			this.width = width;
			this.height = height;
			pixels = new char[height, width];
			//pixels = new Pixel[height, width];

			WritePixels(0, 0, new String(' ', width * height), ConsoleColor.Black, ConsoleColor.Black, true);
		}

		public override string ToString() {
			string render = "";
			for ( int row = 0; row < pixels.GetLength(0); row++ ) {
				for ( int col = 0; col < pixels.GetLength(1); col++ ) {
					char px = pixels[row, col];
					render += px.ToString();
				}
				render += "\n";
			}
			return render;
		}

		//public Pixel[,] GetPixels() {
		//	return pixels;
		//}

		public void WritePixels(int startX, int startY, string str, ConsoleColor background, ConsoleColor foreground, bool wrap = false) {
			int x = startX;
			int y = startY;

			for ( int i = 0; i < str.Length; i++ ) {
				if ( x > pixels.GetLength(1) - 1 ) {
					if ( wrap ) {
						x = 0;
						y++;
					} else {
						return;
					}
				}

				if ( y > pixels.GetLength(0) - 1 ) {
					return;
				}

				if ( x >= 0 && y >= 0 && str[i] != '\n' && str[i] != '\t' ) {
					pixels[y, x] = str[i];
				}

				// Disabled color implementation
				//pixels[y, x] = new Pixel(str[i], background, foreground);
				x++;
			}
		}

	}
}
