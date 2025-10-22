namespace GD14_1133_DiceGame_Peskoff_Rob.game.item.weapon.player {
	internal class Excalibur : Weapon {

		public Excalibur() : base(new(3, 4)) {
			Name = "Excalibur";
			Description = $"The legendary sword said to be once wielded by King Arthur himself. Deals {dice.GetDieType()} HP of damage.";
			criticalRollFlavorText = "King Arthur's fury crashes down from the ruins of Avalon";
		}

	}
}
