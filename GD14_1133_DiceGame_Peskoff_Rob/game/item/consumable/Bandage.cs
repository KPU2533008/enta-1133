namespace GD14_1133_DiceGame_Peskoff_Rob.game.item.consumable {
	internal class Bandage : HealthRestoreItem {

		public Bandage() : base(new(2, 3)) {
			Name = "Bandage";
			Description = $"A bandage that restores {dice.GetDieType()} HP to a target.";
		}

	}
}
