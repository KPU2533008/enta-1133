using GD14_1133_DiceGame_Peskoff_Rob.engine.datatype;
using GD14_1133_DiceGame_Peskoff_Rob.engine.render;

namespace GD14_1133_DiceGame_Peskoff_Rob.engine.instance.gui {
	internal abstract class LayerCollector : GuiBase2d {

		public override Vector2 AbsolutePosition {
			get {
				return new();
			}
		}

		public override Vector2 AbsoluteSize {
			get {
				return Renderer.RENDER_RESOLUTION;
			}
		}

		public bool enabled = true;

	}
}
