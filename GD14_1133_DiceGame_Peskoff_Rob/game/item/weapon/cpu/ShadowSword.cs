namespace GD14_1133_DiceGame_Peskoff_Rob.game.item.weapon.cpu {
	internal class ShadowSword : Weapon {

		public ShadowSword() : base(new(2, 3)) {
			Name = "Shadow Sword";
			Description = $"A sharp, blackened sword that swallows all light around it. Deals {dice.GetDieType()} HP of damage.";
			criticalRollFlavorText = "It slices like butter";
		}

	}
}
