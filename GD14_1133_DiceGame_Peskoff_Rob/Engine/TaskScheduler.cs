using GD14_1133_DiceGame_Peskoff_Rob.engine.instance;
using GD14_1133_DiceGame_Peskoff_Rob.engine.input;
using GD14_1133_DiceGame_Peskoff_Rob.engine.render;

namespace GD14_1133_DiceGame_Peskoff_Rob.engine {
	internal class TaskScheduler : ILifecycle {

		public static readonly int FPS_TARGET = 60;

		private Renderer renderer = new();
		private AsyncTask? engineLoop = null;

		private long lastTick = 0;

		public Signal<float> PreRender = new();

		private void FrameSleep(float frameLength) {
			float targetFrameTime = 1.0f / FPS_TARGET;
			Thread.Sleep(Math.Max(0, (int)( targetFrameTime - frameLength )));
		}

		private AsyncTask StartEngineLoop() {
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
					float frameLengthMs = ( frameEnd - frameStart ) / ( TimeSpan.TicksPerMillisecond * 1000.0f );
					lastTick = frameStart;

					FrameSleep(frameLengthMs);
				}
			});
		}

		public void Init() { }

		public void Start() {
			engineLoop = StartEngineLoop();
		}

		public void Stop() {
			engineLoop?.Cancel();
		}

	}
}
