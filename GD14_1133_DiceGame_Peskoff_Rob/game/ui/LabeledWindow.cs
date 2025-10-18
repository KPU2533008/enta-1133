using GD14_1133_DiceGame_Peskoff_Rob.engine.datatype;
using GD14_1133_DiceGame_Peskoff_Rob.engine.@enum;
using GD14_1133_DiceGame_Peskoff_Rob.engine.instance.gui;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.ui {
	internal class LabeledWindow {

		protected readonly Frame main;
		protected readonly TextLabel label;
		public readonly Frame container;

		public LabeledWindow(UDim2 position, UDim2 size, string labelText) {
			main = new();
			main.position = position;
			main.size = size;
			main.borderEnabled = true;

			container = new();
			container.position = UDim2.FromOffset(2, 1);
			container.size = new(1, -4, 1, -2);
			container.Parent = main;

			label = new();
			label.position = UDim2.FromOffset(2, 0);
			label.size = new(1, 0, 0, 1);
			label.text = labelText;
			label.textXAlignment = TextXAlignment.Left;
			label.Parent = main;
		}

		public void SetPosition(UDim2 pos) {
			main.position = pos;
		}

		public virtual void Destroy() {
			main.Destroy();
		}

	}
}
