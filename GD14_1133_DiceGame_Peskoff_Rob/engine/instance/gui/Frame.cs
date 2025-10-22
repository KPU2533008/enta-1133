using GD14_1133_DiceGame_Peskoff_Rob.engine.render;

namespace GD14_1133_DiceGame_Peskoff_Rob.engine.instance.gui {
	internal class Frame : GuiObject {

		public bool borderEnabled = false;

		public override void Draw(Canvas canvas) {
			if ( borderEnabled ) {
				for ( int y = AbsolutePosition.Y; y < AbsolutePosition.Y + AbsoluteSize.Y - 1; y++ ) {
					canvas.WritePixels(AbsolutePosition.X, y, "║", backgroundColor, backgroundColor, false);
					canvas.WritePixels(AbsolutePosition.X + AbsoluteSize.X - 1, y, "║", backgroundColor, backgroundColor, false);
				}

				canvas.WritePixels(AbsolutePosition.X, AbsolutePosition.Y, new string('═', AbsoluteSize.X), backgroundColor, backgroundColor, false);
				canvas.WritePixels(AbsolutePosition.X, AbsolutePosition.Y + AbsoluteSize.Y - 1, new string('═', AbsoluteSize.X), backgroundColor, backgroundColor, false);

				canvas.WritePixels(AbsolutePosition.X, AbsolutePosition.Y, "╔", backgroundColor, backgroundColor, false);
				canvas.WritePixels(AbsolutePosition.X + AbsoluteSize.X - 1, AbsolutePosition.Y, "╗", backgroundColor, backgroundColor, false);
				canvas.WritePixels(AbsolutePosition.X, AbsolutePosition.Y + AbsoluteSize.Y - 1, "╚", backgroundColor, backgroundColor, false);
				canvas.WritePixels(AbsolutePosition.X + AbsoluteSize.X - 1, AbsolutePosition.Y + AbsoluteSize.Y - 1, "╝", backgroundColor, backgroundColor, false);
			}
		}
	}
}
