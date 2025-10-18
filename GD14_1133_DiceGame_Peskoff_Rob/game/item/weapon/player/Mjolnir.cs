namespace GD14_1133_DiceGame_Peskoff_Rob.game.item.weapon.player {
	internal class Mjolnir : Weapon {

		public Mjolnir() : base(new(5, 3)) {
			Name = "Mjolnir";
			Description = $"A hammer forged from godly metals, once wielded by the God of Thunder, Thor. Deals {dice.GetDieType()} HP of damage.";
			criticalRollFlavorText = "The mighty hammer shakes the ground, summoning fearsome lightning from the sky";
		}

	}
}
