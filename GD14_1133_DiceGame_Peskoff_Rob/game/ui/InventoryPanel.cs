using GD14_1133_DiceGame_Peskoff_Rob.engine.datatype;
using GD14_1133_DiceGame_Peskoff_Rob.engine.instance.gui;
using GD14_1133_DiceGame_Peskoff_Rob.game.item;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.ui {
	internal class InventoryPanel {

		private readonly LabeledWindow panel = new(new(0.5f, 0, 1, -12), new(0.25f, 0, 0, 12), "INVENTORY");
		private readonly TextLabel itemsList = new();

		public InventoryPanel() {
			itemsList.size = UDim2.FromScale(1, 1);
			itemsList.textXAlignment = engine.@enum.TextXAlignment.Left;
			itemsList.textYAlignment = engine.@enum.TextYAlignment.Top;
			itemsList.Parent = panel.container;
		}

		public void Clear() {
			itemsList.text = "";
		}

		public void UpdateItems(Item[] list) {
			if ( list.Length == 0 ) {
				Clear();
				return;
			}

			string text = $"1) {list[0].Name}";

			if ( list.Length > 1 ) {
				for ( int i = 1; i < list.Length; i++ ) {
					text += $"\n{i + 1}) {list[i].Name}";
				}
			}

			itemsList.text = text;
		}
	}
}