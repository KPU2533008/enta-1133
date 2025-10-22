using GD14_1133_DiceGame_Peskoff_Rob.game.combat;
using GD14_1133_DiceGame_Peskoff_Rob.game.@object;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.item.weapon {
	internal abstract class Weapon(Dice dice) : Item(dice) {

		protected string criticalRollFlavorText = "The attack lands with expert precision";

		protected void BasicAttack(Combatant aggressor, Combatant victim) {
			int damage = Roll();

			Game.dialogWindow.ShowDialog($"{aggressor.Name} attacks {victim.Name} with {Name}!");
			victim.TakeDamage(damage);

			if ( dice.IsCriticalRoll ) {
				Game.dialogWindow.ShowDialog($"CRITICAL STRIKE! {criticalRollFlavorText} and {victim.Name} receives {damage} HP of damage!");
			} else if ( dice.IsCriticalFail ) {
				Game.dialogWindow.ShowDialog($"CRITICAL FAIL! {victim.Name} evades the attack with finesse and grace and receives only {damage} HP of damage!");
			} else {
				Game.dialogWindow.ShowDialog($"{victim.Name} receives {damage} HP of damage!");
			}
		}

		public override void OnUse(Combatant user, Combatant target) {
			BasicAttack(user, target);
		}

	}
}
