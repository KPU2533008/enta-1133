namespace GD14_1133_DiceGame_Peskoff_Rob.game.item.consumable {
	internal class HealthPotion : HealthRestoreItem {

		public HealthPotion() : base(new(2, 4)) {
			Name = "Health Potion";
			Description = $"A ruby red healing elixir in a pristine glass bottle. Restores {dice.GetDieType()} HP when used.";
		}

	}
}
