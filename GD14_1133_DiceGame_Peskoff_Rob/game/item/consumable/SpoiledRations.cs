namespace GD14_1133_DiceGame_Peskoff_Rob.game.item.consumable {
	internal class SpoiledRations : HealthRestoreItem {

		public SpoiledRations() : base(new(2, 3)) {
			Name = "Spoiled Rations";
			Description = "UNOBTAINABLE -- CPU ONLY";
			usageText = "@USER takes out some @ITEM and feeds @TARGET.";
		}

	}
}
