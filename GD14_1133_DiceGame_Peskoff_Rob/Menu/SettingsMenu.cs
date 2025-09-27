using GD14_1133_DiceGame_Peskoff_Rob.Game.Util;

namespace GD14_1133_DiceGame_Peskoff_Rob.Menu {
	internal class SettingsMenu : IMenu {

		public override string ToString() {
			return "Settings Menu";
		}

		private string FormatSettingState(bool state) {
			return state ? "enabled" : "disabled";
		}

		public string GetMenuId() {
			return "SETTINGS_MENU";
		}

		public bool ShouldDisplayMenuName() {
			return true;
		}

		public IMenu? GetPreviousMenu() {
			return new MainMenu();
		}

		public List<IMenuOption> GetMenuOptions(MenuManager menuManager) {
			List<IMenuOption> inputOptions = new();

			inputOptions.Add(new("FlavorText", $"Flavor text enabled\t\t\t{GameSettings.FLAVOR_TEXT_ENABLED}", () => {
				GameSettings.FLAVOR_TEXT_ENABLED = !GameSettings.FLAVOR_TEXT_ENABLED;
				Display.PrintLn($"Flavor text is now {FormatSettingState(GameSettings.FLAVOR_TEXT_ENABLED)}");
				return null;
			}));

			inputOptions.Add(new("Typewriter", $"Typewriter enabled\t\t\t{GameSettings.TYPEWRITER_ENABLED}", () => {
				GameSettings.TYPEWRITER_ENABLED = !GameSettings.TYPEWRITER_ENABLED;
				Display.PrintLn($"Typewriter is now {FormatSettingState(GameSettings.TYPEWRITER_ENABLED)}");
				return null;
			}));

			return inputOptions;
		}

		public void RenderMenu() {
			Display.PrintLn("Type the number of a setting to turn it on or off or \"back\" to go back.");
		}

	}
}
