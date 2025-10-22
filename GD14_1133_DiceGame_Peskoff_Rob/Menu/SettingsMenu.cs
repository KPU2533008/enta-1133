using GD14_1133_DiceGame_Peskoff_Rob.game.util;
using GD14_1133_DiceGame_Peskoff_Rob.util;

namespace GD14_1133_DiceGame_Peskoff_Rob.menu {
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

		public bool ShouldDisplayHeader() {
			return true;
		}

		public IMenu? GetPreviousMenu() {
			return new MainMenu();
		}

		public List<SMenuOption> GetMenuOptions(MenuManager menuManager) {
			List<SMenuOption> inputOptions = new();

			foreach ( KeyValuePair<string, bool> setting in GameSettings.SETTINGS ) {
				string settingName = setting.Key;
				string displayText = StringUtil.AddSpaces(settingName);
				bool isEnabled = setting.Value;

				inputOptions.Add(new(settingName, $"{displayText} enabled{new String(' ', 30 - displayText.Length)}{isEnabled}", () => {
					GameSettings.SetSetting(settingName, !isEnabled);
					return new(null, $"{displayText} is now {FormatSettingState(!isEnabled)}");
				}));
			}

			return inputOptions;
		}

		public void Draw() {
			Display.PrintLn("Type the number of a setting to turn it on or off or \"back\" to go back.");
		}

	}
}
