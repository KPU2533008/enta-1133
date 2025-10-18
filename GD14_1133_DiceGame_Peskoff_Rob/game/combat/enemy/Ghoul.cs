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
			return new Bandage();
		}

		public override Weapon SelectWeapon() {
			return new IronFist();
		}

	}
}
