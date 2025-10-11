namespace GD14_1133_DiceGame_Peskoff_Rob.menu {
	internal struct SMenuActionResult {
		public readonly bool? result = null;
		public readonly string message = "";

		public SMenuActionResult(bool? result = null, string message = "") {
			this.result = result;
			this.message = message;
		}
	}
}
