namespace GD14_1133_DiceGame_Peskoff_Rob {
	internal class GameSettings {

		// This version no longer has any flavor text
		internal static Dictionary<string, bool> SETTINGS = new Dictionary<string, bool>() {
			//["FlavorText"] = true,
			["TypewriterEffect"] = true,
		};

		internal static bool GetSetting(string settingName) {
			return SETTINGS[settingName];
		}

		internal static void SetSetting(string settingName, bool value) {
			SETTINGS[settingName] = value;
		}

	}
}
