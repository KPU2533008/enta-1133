namespace GD14_1133_DiceGame_Peskoff_Rob {
	internal class GameSettings {

		internal const string GAME_NAME = "Rob Peskoff's Dungeons & Dinguses";

		// This version no longer has any flavor text
		internal static readonly Dictionary<string, bool> SETTINGS = new Dictionary<string, bool>() {
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
