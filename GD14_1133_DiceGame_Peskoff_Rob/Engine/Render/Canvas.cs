namespace GD14_1133_DiceGame_Peskoff_Rob.engine.render {
	internal class Canvas {

		public readonly int width;
		public readonly int height;
		private char[] pixels;

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
			pixels = new char[height * width];

			WritePixels(0, 0, new String(' ', width * height), ConsoleColor.Black, ConsoleColor.Black, true);
		}

		public char[] GetPixels() {
			return pixels;
		}

		public void WritePixels(int startX, int startY, string str, ConsoleColor background, ConsoleColor foreground, bool wrap = false) {
			int canvasIndex = startY * width + startX;

			for ( int i = 0; i < str.Length; i++ ) {
				if ( canvasIndex / width != startY && !wrap ) {
					return;
				}

				if ( canvasIndex < 0 || canvasIndex > pixels.Length - 1 ) {
					continue;
				}

				pixels[canvasIndex] = str[i];
				canvasIndex++;
			}
		}

	}
}
