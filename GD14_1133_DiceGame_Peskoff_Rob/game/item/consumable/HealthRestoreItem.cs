using GD14_1133_DiceGame_Peskoff_Rob.game.combat;
using GD14_1133_DiceGame_Peskoff_Rob.game.@object;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.item.consumable {
	internal abstract class HealthRestoreItem : Consumable {

		public HealthRestoreItem(Dice dice) : base(dice) {
			TargetAllegiance = @enum.Allegiance.Friendly;
		}

		public override void OnUse(Combatant user, Combatant target) {
			base.OnUse(user, target);

			int health = Roll();
			target.Heal(health);

			Game.dialogWindow.ShowDialog($"{target.GetFullName()} is healed for {health} HP!");
		}

	}
}
