using GD14_1133_DiceGame_Peskoff_Rob.engine.datatype;
using GD14_1133_DiceGame_Peskoff_Rob.engine.render;

namespace GD14_1133_DiceGame_Peskoff_Rob.engine.instance.gui {
	public abstract class GuiObject : GuiBase2d, IDrawable {

		public ConsoleColor backgroundColor = ConsoleColor.Black; // UNUSED, COLOR NOT SUPPORTED
		public UDim2 position = new();
		public UDim2 size = new();
		public bool visible = true;
		public int zIndex = 1;
		public bool clipsDescendants = false;

		public override Vector2 AbsolutePosition {
			get {
				Vector2 parentPos = new();
				Vector2 parentSize = Renderer.RENDER_RESOLUTION;
				GuiBase2d? ancestor = FindFirstAncestorWhichIsA(typeof(GuiBase2d)) as GuiBase2d;

				if ( ancestor != null ) {
					parentPos = ancestor.AbsolutePosition;
					parentSize = ancestor.AbsoluteSize;
				}

				Vector2 scale = new((int)( parentSize.X * position.X.scale ), (int)( parentSize.Y * position.Y.scale ));
				Vector2 offset = new(position.X.offset, position.Y.offset);

				return parentPos + scale + offset;
			}
		}

		public override Vector2 AbsoluteSize {
			get {
				Vector2 parentSize = Renderer.RENDER_RESOLUTION;
				GuiBase2d? ancestor = FindFirstAncestorWhichIsA(typeof(GuiBase2d)) as GuiBase2d;

				if ( ancestor != null ) {
					parentSize = ancestor.AbsoluteSize;
				}

				Vector2 scale = new((int)( parentSize.X * size.X.scale ), (int)( parentSize.Y * size.Y.scale ));
				Vector2 offset = new(size.X.offset, size.Y.offset);

				return scale + offset;
			}
		}

		public GuiObject() {
			Scene.Add(this);
		}

		public virtual bool CanDraw() {
			return visible;
		}

		public abstract void Draw(Canvas canvas);

		public override void Destroy() {
			Scene.Remove(this);
			base.Destroy();
		}

	}
}
