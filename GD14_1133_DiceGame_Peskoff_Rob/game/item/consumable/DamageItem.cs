using GD14_1133_DiceGame_Peskoff_Rob.game.combat;
using GD14_1133_DiceGame_Peskoff_Rob.game.@object;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.item.consumable {
	public abstract class DamageItem : Consumable {

		public DamageItem(Dice dice) : base(dice) {
			TargetAllegiance = @enum.Allegiance.Hostile;
		}

		public override void OnUse(Combatant user, Combatant target) {
			base.OnUse(user, target);

			int damage = Roll();
			target.TakeDamage(damage);

			Game.dialogWindow.ShowDialog($"{target.GetFullName()} takes {damage} HP of damage!");
		}

	}
}
