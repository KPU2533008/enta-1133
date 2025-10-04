namespace GD14_1133_DiceGame_Peskoff_Rob.Game.Util {
	internal class Sugar {

		public static void Wait(float time) {
			if ( !GameSettings.TYPEWRITER_ENABLED )
				return;
			Thread.Sleep((int)( time * 1000 ));
		}

	}
}
