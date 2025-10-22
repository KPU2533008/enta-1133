using GD14_1133_DiceGame_Peskoff_Rob.engine.datatype;
using GD14_1133_DiceGame_Peskoff_Rob.engine.instance.gui;
using GD14_1133_DiceGame_Peskoff_Rob.engine.@object;
using GD14_1133_DiceGame_Peskoff_Rob.game.combat;
using System.Drawing;
using TaskScheduler = GD14_1133_DiceGame_Peskoff_Rob.engine.TaskScheduler;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.ui {
	internal class CombatantInfoCard : LabeledWindow {

		private TextLabel hpLabel = new();
		private TextLabel combatantHealth = new();

		private Connection updateHealthConn;

		public CombatantInfoCard(Combatant combatant) : base(new(), UDim2.FromOffset(24, 3), combatant.GetFullName()) {
			main.Parent = Game.mainViewport;

			hpLabel.size = new(0, 2, 0, 1);
			hpLabel.position = UDim2.FromOffset(0, 0);
			hpLabel.text = "HP";
			hpLabel.textXAlignment = engine.@enum.TextXAlignment.Left;
			hpLabel.Parent = container;

			combatantHealth.size = new(1, -3, 0, 1);
			combatantHealth.position = UDim2.FromOffset(3, 0);
			combatantHealth.Parent = container;

			updateHealthConn = TaskScheduler.PreRender.Connect((float dt) => {
				UpdateHealthBar(combatant.HP, combatant.MaxHP);
			});
		}

		private void UpdateHealthBar(int hp, int maxHp) {
			int sizeX = combatantHealth.AbsoluteSize.X;
			float percent = (float)hp / maxHp;
			int filled = (int)( percent * sizeX );
			int empty = sizeX - filled;

			combatantHealth.text = $"{new String('█', filled)}{new String('░', empty)}";
		}

		public override void Destroy() {
			base.Destroy();
			updateHealthConn.Disconnect();
		}

	}
}
