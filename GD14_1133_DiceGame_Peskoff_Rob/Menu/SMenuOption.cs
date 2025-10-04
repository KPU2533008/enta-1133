namespace GD14_1133_DiceGame_Peskoff_Rob.Menu {
	internal struct SMenuOption {

		public string name;
		public string displayText;
		public Func<SMenuActionResult> onChosen;

		public SMenuOption(string name, string? displayText, Func<SMenuActionResult> onChosen) {
			this.name = name;
			this.displayText = displayText ?? name;
			this.onChosen = onChosen;
		}
	}
}
