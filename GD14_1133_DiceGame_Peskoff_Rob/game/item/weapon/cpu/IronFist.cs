namespace GD14_1133_DiceGame_Peskoff_Rob.game.item.weapon.cpu {
	internal class IronFist : Weapon {

		public IronFist() : base(new(2, 2)) {
			Name = "an iron fist";
			Description = "UNOBTAINABLE -- CPU ONLY";
			criticalRollFlavorText = "It mercilessly beats out again and again with unwavering strength. Bones can be heard cracking";
		}

	}
}
