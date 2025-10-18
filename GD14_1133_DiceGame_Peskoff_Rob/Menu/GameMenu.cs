using GD14_1133_DiceGame_Peskoff_Rob.Game;

namespace GD14_1133_DiceGame_Peskoff_Rob.Menu {
	internal class GameMenu : IMenu {

		public override string ToString() {
			return "Dirty Dice";
		}

		public string GetMenuId() {
			return "GAME_MENU";
		}

		public bool ShouldDisplayHeader() {
			return true;
		}

		public IMenu? GetPreviousMenu() {
			return new MainMenu();
		}

		public List<SMenuOption> GetMenuOptions(MenuManager menuManager) {
			return new();
		}

		public void Draw() {
			DiceGameRunner gameRunner = new();
			gameRunner.RunGame();
		}

	}
}
