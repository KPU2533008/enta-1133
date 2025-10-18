namespace GD14_1133_DiceGame_Peskoff_Rob.game.item.consumable {
	internal class RevivalFruit : HealthRestoreItem {

		public RevivalFruit() : base(new(1, 5)) {
			Name = "Fruit of Life";
			Description = "A mystical fruit that restores life to those who have fallen.";
			TargetMortality = @enum.Mortality.Dead;
		}

	}
}
