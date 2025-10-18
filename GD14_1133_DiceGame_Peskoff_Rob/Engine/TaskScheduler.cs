using GD14_1133_DiceGame_Peskoff_Rob.engine.input;
using GD14_1133_DiceGame_Peskoff_Rob.engine.render;
using GD14_1133_DiceGame_Peskoff_Rob.engine.@object;

namespace GD14_1133_DiceGame_Peskoff_Rob.engine {
	internal static class TaskScheduler {

		public static readonly int FPS_TARGET = 60;

		private static Renderer renderer = new();
		private static AsyncTask? engineLoop = null;

		private static long lastTick = 0;

		public static Signal<float> PreRender = new();

		private static List<float> frameLengthLog = new();

		private static void FrameSleep(float frameLength) {
			float targetFrameTime = 1.0f / FPS_TARGET;
			Thread.Sleep(Math.Max(0, (int)( targetFrameTime - frameLength )));
		}

		private static AsyncTask StartEngineLoop() {
			lastTick = DateTime.Now.Ticks;
			return new((token) => {
				while ( true ) {
					if ( token.IsCancellationRequested ) {
						break;
					}

					long frameStart = DateTime.Now.Ticks;
					long deltaTick = frameStart - lastTick;
					float deltaTime = deltaTick / ( TimeSpan.TicksPerMillisecond * 1000.0f );

					UserInputService.PollInput();
					PreRender.Fire(deltaTime);
					renderer.DrawScreen();

					long frameEnd = DateTime.Now.Ticks;
					float frameLength = ( frameEnd - frameStart ) / ( TimeSpan.TicksPerMillisecond * 1000.0f );
					lastTick = frameStart;

					frameLengthLog.Add(frameLength);
					if ( frameLengthLog.Count > 10 ) {
						frameLengthLog.RemoveAt(0);
					}

					FrameSleep(frameLength);
				}
			});
		}

		public static float GetAvgFrameLength() {
			float sum = 0.0f;
			for ( int i = 0; i < frameLengthLog.Count; i++ ) {
				sum += frameLengthLog[i];
			}
			return sum / frameLengthLog.Count;
		}

		public static void Init() { }

		public static void Start() {
			engineLoop = StartEngineLoop();
		}

		public static void Stop() {
			engineLoop?.Cancel();
		}

	}
}
