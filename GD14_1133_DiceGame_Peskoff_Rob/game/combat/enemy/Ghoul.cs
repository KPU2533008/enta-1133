using GD14_1133_DiceGame_Peskoff_Rob.game.item;
using GD14_1133_DiceGame_Peskoff_Rob.game.item.consumable;
using GD14_1133_DiceGame_Peskoff_Rob.game.item.weapon;
using GD14_1133_DiceGame_Peskoff_Rob.game.item.weapon.cpu;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.combat.enemy {
	internal class Ghoul : CpuCombatant {

		public Ghoul() : base(11) {
			Name = "Ghoul";
		}

		public override string GetDefeatMessage() {
			return $"{Name} disappeared!";
		}

		public override string GetPassMessage() {
			return $"{Name} is floating around ominously.";
		}

		public override Consumable SelectConsumable() {
			Consumable item = new Bandage();
			return CanUseItem(item) ? item : new PromptCombatActionAgainConsumable();
		}

		public override Weapon SelectWeapon() {
			Weapon item = new IronFist();
			return CanUseItem(item) ? item : new PromptCombatActionAgainWeapon();
		}

	}
}
