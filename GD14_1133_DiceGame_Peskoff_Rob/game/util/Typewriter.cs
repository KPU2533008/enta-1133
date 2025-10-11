using GD14_1133_DiceGame_Peskoff_Rob.engine;
using GD14_1133_DiceGame_Peskoff_Rob.engine.instance;
using GD14_1133_DiceGame_Peskoff_Rob.engine.instance.gui;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.util {
	internal class Typewriter {

		private static Connection PlayInternal(TextLabel label, string text, int inc = 1) {
			label.text = text;
			label.maxVisibleGraphemes = GameSettings.GetSetting("TypewriterEffect") ? 0 : -1;

			Connection conn = null;
			conn = Engine.RunService.PreRender.Connect((float dt) => {
				if ( !GameSettings.GetSetting("TypewriterEffect") || label.maxVisibleGraphemes >= label.text.Length ) {
					conn?.Disconnect();
					return;
				}
				label.maxVisibleGraphemes += (int)Math.Round(inc * ( dt * 60 ));
			});

			return conn;
		}

		public static void Play(TextLabel label, string text, int inc = 1) {
			Connection conn = PlayInternal(label, text);
			while ( conn.Connected ) {
				Thread.Sleep(1);
			}
		}

		public static void PlayAsync(TextLabel label, string text, int inc = 1) {
			PlayInternal(label, text);
		}
	}
}
