using GD14_1133_DiceGame_Peskoff_Rob.game.combat;
using GD14_1133_DiceGame_Peskoff_Rob.game.@object;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.item.consumable {
	internal abstract class RevivalItem : Consumable {

		public RevivalItem(Dice dice) : base(dice) {
			TargetAllegiance = @enum.Allegiance.Friendly;
			TargetMortality = @enum.Mortality.Dead;
		}

		public override void OnUse(Combatant user, Combatant target) {
			base.OnUse(user, target);

			int health = Roll();
			target.Heal(health);

			Game.dialogWindow.ShowDialog($"{target.Name} is revived with {health} HP!");
		}

	}
}
