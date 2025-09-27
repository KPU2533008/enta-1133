namespace GD14_1133_DiceGame_Peskoff_Rob.Menu {
	internal struct IMenuOption {

		public string name;
		public string displayText;
		public Func<bool?> onChosen;

		public IMenuOption(string name, string? displayText, Func<bool?> onChosen) {
			this.name = name;
			this.displayText = displayText ?? name;
			this.onChosen = onChosen;
		}
	}
}
