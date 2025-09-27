using GD14_1133_DiceGame_Peskoff_Rob.Game.Util;

namespace GD14_1133_DiceGame_Peskoff_Rob.Menu {
	internal class MainMenu : IMenu {

		public override string ToString() {
			return "Main Menu";
		}

		public string GetMenuId() {
			return "MAIN_MENU";
		}

		public bool ShouldDisplayMenuName() {
			return true;
		}

		public IMenu? GetPreviousMenu() {
			return null;
		}

		public List<IMenuOption> GetMenuOptions(MenuManager menuManager) {
			List<IMenuOption> inputOptions = new();

			inputOptions.Add(new("Play", null, () => {
				menuManager.GoToMenu(new GameMenu());
				return true;
			}));

			inputOptions.Add(new("Settings", null, () => {
				menuManager.GoToMenu(new SettingsMenu());
				return true;
			}));

			inputOptions.Add(new("Quit", null, () => {
				Environment.Exit(0);
				return true;
			}));

			return inputOptions;
		}

		public void RenderMenu() {
			Display.PrintLn("Select an option:");
		}

	}
}
