using GD14_1133_DiceGame_Peskoff_Rob.game.util;

namespace GD14_1133_DiceGame_Peskoff_Rob.menu {
	internal class MainMenu : IMenu {

		public override string ToString() {
			return "Main Menu";
		}

		public string GetMenuId() {
			return "MAIN_MENU";
		}

		public bool ShouldDisplayHeader() {
			return true;
		}

		public IMenu? GetPreviousMenu() {
			return null;
		}

		public List<SMenuOption> GetMenuOptions(MenuManager menuManager) {
			List<SMenuOption> inputOptions = new();

			inputOptions.Add(new("Play", null, () => {
				menuManager.GoToMenu(new GameMenu());
				return new(true);
			}));

			inputOptions.Add(new("Settings", null, () => {
				menuManager.GoToMenu(new SettingsMenu());
				return new(true);
			}));

			inputOptions.Add(new("Quit", null, () => {
				Environment.Exit(0);
				return new(true);
			}));

			return inputOptions;
		}

		public void Draw() {
			Display.PrintLn("Select an option:");
		}

	}
}
