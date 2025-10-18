using GD14_1133_DiceGame_Peskoff_Rob.engine.@enum;
using GD14_1133_DiceGame_Peskoff_Rob.engine.render;

namespace GD14_1133_DiceGame_Peskoff_Rob.engine.instance.gui {
	internal class TextLabel : GuiObject {

		public string text = "";
		public ConsoleColor textColor = ConsoleColor.White; // UNUSED, COLOR NOT SUPPORTED
		public TextXAlignment textXAlignment = TextXAlignment.Center;
		public TextYAlignment textYAlignment = TextYAlignment.Center;
		public bool textWrapped = false;
		public int maxVisibleGraphemes = -1;

		public bool TextFits { get; private set; } = false;

		public override bool CanDraw() {
			return text.Length > 0 && base.CanDraw();
		}

		public override void Draw(Canvas canvas) {
			if ( text.Length == 0 )
				return;

			string renderText = text.Replace('\t', ' ');

			/*
			 * In order to wrap the text, we split the text up into substrings every
			 * `AbsoluteSize.X` number of characters and add them to a list. Later on
			 * we iterate through the list and draw those strings to the canvas.
			 * 
			 * The nice thing is that this approach works even if we don't want to
			 * wrap the text. We can just throw the whole string into a single entry
			 * in the list and the text layout logic will just display it on a single
			 * line, aligned in the proper way.
			 */

			List<string> textLines = new List<string>();

			for ( int y = 0; y < AbsoluteSize.Y; y++ ) {
				if ( renderText.Substring(0, 1) == " " ) {
					renderText = renderText.Substring(1, renderText.Length - 1);
				}

				int newlinePos = renderText.IndexOf('\n');
				int lineLength = Math.Min(renderText.Length, textWrapped ? AbsoluteSize.X : renderText.Length);
				string lineText = renderText.Substring(0, lineLength);

				if ( newlinePos != -1 && newlinePos < lineLength ) {
					lineLength = newlinePos + 1;
					lineText = renderText.Substring(0, newlinePos);
				}

				renderText = renderText.Substring(lineLength, renderText.Length - lineLength);
				textLines.Add(lineText);

				if ( renderText.Length == 0 )
					break;
			}

			TextFits = renderText.Length == 0;

			/* 
			 * `maxVisibleGraphemes` is very useful for doing typewriter effects, since
			 * the text layout logic will lay everything out as if all the text were to
			 * be drawn and then stop writing to the canvas if it runs out of graphemes.
			 * This prevents text from rearranging itself in awkward and undesirable ways
			 * while being animated.
			 */

			int graphemesRemaining = maxVisibleGraphemes >= 0 ? maxVisibleGraphemes : text.Length;

			for ( int i = 0; i < textLines.Count; i++ ) {
				string line = textLines[i];

				if ( clipsDescendants && line.Length > AbsoluteSize.X ) {
					int extraChars = ( line.Length - AbsoluteSize.X );

					if ( textXAlignment == TextXAlignment.Left ) {
						line = line.Substring(0, AbsoluteSize.X);
					} else if ( textXAlignment == TextXAlignment.Center ) {
						line = line.Substring(extraChars / 2, line.Length - extraChars);
					} else {
						line = line.Substring(extraChars, line.Length - extraChars);
					}
				}

				int x =
					  textXAlignment == TextXAlignment.Left ? AbsolutePosition.X
					: textXAlignment == TextXAlignment.Right ? AbsolutePosition.X + AbsoluteSize.X - line.Length
					: AbsolutePosition.X + AbsoluteSize.X / 2 - line.Length / 2;

				int y =
					  textYAlignment == TextYAlignment.Top ? AbsolutePosition.Y + i
					: textYAlignment == TextYAlignment.Bottom ? AbsolutePosition.Y + AbsoluteSize.Y - textLines.Count + i
					: AbsolutePosition.Y + AbsoluteSize.Y / 2 - textLines.Count / 2 + i;

				int length = Math.Min(graphemesRemaining, line.Length);
				canvas.WritePixels(x, y, line.Substring(0, length), backgroundColor, textColor, false);
				graphemesRemaining = Math.Max(graphemesRemaining - length, 0);
			}
		}
	}
}
