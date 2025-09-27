using GD14_1133_DiceGame_Peskoff_Rob.Game;

namespace GD14_1133_DiceGame_Peskoff_Rob.Menu {
	internal class GameMenu : IMenu {

		public override string ToString() {
			return "Dice Game";
		}

		public string GetMenuId() {
			return "GAME_MENU";
		}

		public bool ShouldDisplayMenuName() {
			return true;
		}

		public IMenu? GetPreviousMenu() {
			return new MainMenu();
		}

		public List<IMenuOption> GetMenuOptions(MenuManager menuManager) {
			return new();
		}

		public void RenderMenu() {
			DiceGameRunner gameRunner = new();
			gameRunner.RunGame();
		}

	}
}
