using GD14_1133_DiceGame_Peskoff_Rob.engine.datatype;

namespace GD14_1133_DiceGame_Peskoff_Rob.engine.instance.gui {
	internal abstract class GuiBase2d : Instance {

		public abstract Vector2 AbsolutePosition { get; }
		public abstract Vector2 AbsoluteSize { get; }

	}
}
