using GD14_1133_DiceGame_Peskoff_Rob.game.item;
using GD14_1133_DiceGame_Peskoff_Rob.game.item.consumable;
using GD14_1133_DiceGame_Peskoff_Rob.game.item.weapon;
using GD14_1133_DiceGame_Peskoff_Rob.game.item.weapon.cpu;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.combat.enemy {
	internal class HauntedKnight : CpuCombatant {

		public HauntedKnight() : base(25) {
			Name = "Haunted Knight";
		}

		public override string GetDefeatMessage() {
			return $"{GetFullName()}'s armor shattered to pieces!";
		}

		public override string GetPassMessage() {
			return $"{GetFullName()} admires their sword as it glistens in the light.";
		}

		public override Consumable SelectConsumable() {
			Consumable item = new SpoiledRations();
			return CanUseItem(item) ? item : new PromptCombatActionAgainConsumable();
		}

		public override Weapon SelectWeapon() {
			Weapon item = new ShadowSword();
			return CanUseItem(item) ? item : new PromptCombatActionAgainWeapon();
		}

	}
}
