namespace GD14_1133_DiceGame_Peskoff_Rob.game.item.weapon.player {
	internal class RustySword : Weapon {

		public RustySword() : base(new(1, 6)) {
			Name = "Rusty Sword";
			Description = $"An old and rusty sword. Its blade is dull and isn't particularly effective, but it deals {dice.GetDieType()} HP of damage.";
		}

	}
}
