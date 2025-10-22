namespace GD14_1133_DiceGame_Peskoff_Rob.engine {
	internal class AsyncTask {

		private CancellationTokenSource tokenSource;
		private CancellationToken token;

		public AsyncTask(Action<CancellationToken> runner) {
			tokenSource = new();
			token = tokenSource.Token;

			Task.Run(() => {
				runner(token);
			}, token);
		}

		public void Cancel() {
			tokenSource.Cancel();
		}

	}
}
