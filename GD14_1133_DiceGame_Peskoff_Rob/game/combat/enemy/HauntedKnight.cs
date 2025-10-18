using GD14_1133_DiceGame_Peskoff_Rob.game.item.consumable;
using GD14_1133_DiceGame_Peskoff_Rob.game.item.weapon;
using GD14_1133_DiceGame_Peskoff_Rob.game.item.weapon.cpu;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.combat.enemy {
	internal class HauntedKnight : CpuCombatant {

		public HauntedKnight() : base(25) {
			Name = "Haunted Knight";
		}

		public override string GetDefeatMessage() {
			return $"{Name}'s armor shattered to pieces!";
		}

		public override string GetPassMessage() {
			return $"{Name} admires their sword as it glistens in the light.";
		}

		public override Consumable SelectConsumable() {
			return new SpoiledRations();
		}

		public override Weapon SelectWeapon() {
			return new ShadowSword();
		}

	}
}
