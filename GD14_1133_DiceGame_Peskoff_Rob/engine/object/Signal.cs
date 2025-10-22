using GD14_1133_DiceGame_Peskoff_Rob.engine.instance.@interface;

namespace GD14_1133_DiceGame_Peskoff_Rob.engine.@object {
	internal class Connection {

		private Action disconnectHandler;
		public bool Connected { get; private set; } = true;

		public Connection(Action disconnectHandler) {
			this.disconnectHandler = disconnectHandler;
		}

		public void Disconnect() {
			if ( Connected ) {
				Connected = false;
				disconnectHandler();
			}
		}

	}

	internal class Signal<T> : IDestroyable {
		public delegate void SignalEventHandler(T args);
		private event SignalEventHandler InternalEvent;
		private List<Connection> connections = new();

		public void Fire(T args) {
			InternalEvent?.Invoke(args);
		}

		public Connection Connect(SignalEventHandler subscriber) {
			SignalEventHandler asyncWrapper = (T args) => {
				Task.Run(() => {
					subscriber(args);
				});
			};

			InternalEvent += asyncWrapper;
			Connection conn = null;

			conn = new(() => {
				InternalEvent -= asyncWrapper;
				if ( conn != null && connections.Count > 0 && connections.Contains(conn) ) {
					connections.Remove(conn);
				}
			});

			connections.Add(conn);
			return conn;
		}

		public Connection Once(SignalEventHandler subscriber) {
			Connection conn = null;
			conn = Connect((args) => {
				subscriber(args);
				conn?.Disconnect();
			});
			return conn;
		}

		public void Wait() {
			bool yielding = true;

			Once((args) => {
				yielding = false;
			});

			while ( yielding ) {
				Thread.Sleep(1);
			}
		}

		public void DisconnectAll() {
			for ( int i = 0; i < connections.Count; i++ ) {
				connections[i].Disconnect();
			}
		}

		public void Destroy() {
			DisconnectAll();
		}

	}
}
