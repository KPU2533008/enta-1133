using GD14_1133_DiceGame_Peskoff_Rob.engine.datatype;
using System.Diagnostics;

namespace GD14_1133_DiceGame_Peskoff_Rob.engine.render {
	internal class Renderer {

		// Dynamically adjust the render resolution of the game to match the console window size
		public static Vector2 RENDER_RESOLUTION {
			get {
				return new(Console.WindowWidth, Console.WindowHeight - 1);
			}
		}

		private static readonly Vector2 MIN_RESOLUTION = new(120, 40);
		private Canvas? lastCanvas = null;

		public Renderer() {
			Console.CursorVisible = false;
		}

		private void ClearScreen() {
			Console.Clear();
			Console.WriteLine("\x1b[3J");
			Console.Clear();
		}

		internal void DrawScreen() {
			/*
			 * If the console window has been shrunk, there will be text in the backflow buffer
			 * that has to be cleared out, so we clear the whole console. However, we don't want
			 * to do this every frame since clearing the console leads to very ugly flickering.
			 * We don't need to do this if the window has gotten larger, however, since we'll just
			 * overwrite all of the existing contents (because it's bigger!).
			 */
			if ( lastCanvas != null && ( lastCanvas.width > RENDER_RESOLUTION.X || lastCanvas.height > RENDER_RESOLUTION.Y ) ) {
				ClearScreen();
			}

			Canvas canvas = new(RENDER_RESOLUTION.X, RENDER_RESOLUTION.Y);

			// Enforce a minimum render resolution for the game
			if ( RENDER_RESOLUTION.X < MIN_RESOLUTION.X || RENDER_RESOLUTION.Y < MIN_RESOLUTION.Y ) {
				canvas.WritePixels(0, 0, "Game window too small to render game. Please increase window size.", ConsoleColor.Black, ConsoleColor.White, true);
			} else {
				// TODO: Sort by GuiBase2d ZIndex + parent-child hierarchy (child should render on top of parent)
				foreach ( IDrawable drawable in Scene.GetObjects() ) {
					if ( !drawable.CanDraw() )
						continue;
					drawable.Draw(canvas);
				}
			}

			Console.SetCursorPosition(0, 0);
			Console.Write(canvas.GetPixels());
			lastCanvas = canvas;
		}

	}
}
