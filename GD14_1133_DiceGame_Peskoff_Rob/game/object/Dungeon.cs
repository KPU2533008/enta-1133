using GD14_1133_DiceGame_Peskoff_Rob.engine.datatype;
using GD14_1133_DiceGame_Peskoff_Rob.game.@enum;
using System.Data;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.@object {
	internal class Dungeon {

		private DungeonRoom[,] dungeonMap;

		public Dungeon(Vector2 size) {
			dungeonMap = new DungeonRoom[size.X, size.Y];
			Generate();
		}

		private DungeonRoom? TryGetRoom(int row, int col) {
			if ( row < 0 || row > dungeonMap.GetLength(0) - 1 || col < 0 || col > dungeonMap.GetLength(1) - 1 )
				return null;
			return dungeonMap[row, col];
		}

		private void LinkRoom(DungeonRoom room, int roomX, int roomY) {
			Vector2 north = new(roomX, roomY - 1);
			Vector2 east = new(roomX + 1, roomY);
			Vector2 south = new(roomX, roomY + 1);
			Vector2 west = new(roomX - 1, roomY);

			DungeonRoom? northRoom = TryGetRoom(north.Y, north.X);
			DungeonRoom? eastRoom = TryGetRoom(east.Y, east.X);
			DungeonRoom? southRoom = TryGetRoom(south.Y, south.X);
			DungeonRoom? westRoom = TryGetRoom(west.Y, west.X);

			if ( northRoom != null ) {
				room.north = northRoom;
			}

			if ( eastRoom != null ) {
				room.east = eastRoom;
			}

			if ( southRoom != null ) {
				room.south = southRoom;
			}

			if ( westRoom != null ) {
				room.west = westRoom;
			}
		}

		public void Generate() {
			Random rng = new();

			// First we will generate all the rooms and populate the dungeon map
			for ( int row = 0; row < dungeonMap.GetLength(0); row++ ) {
				for ( int col = 0; col < dungeonMap.GetLength(1); col++ ) {
					int roomType = rng.Next(0, 2);
					DungeonRoom room = roomType == 0 ? new TreasureRoom() : new CombatRoom();
					dungeonMap[row, col] = room;
				}
			}

			/*
			 * Next we will go through the now populated map and wire up all the
			 * rooms to each other, ensuring we do not go out of bounds.
			 * 
			 * This is gross and ugly but whatever.
			 */
			for ( int row = 0; row < dungeonMap.GetLength(0); row++ ) {
				for ( int col = 0; col < dungeonMap.GetLength(1); col++ ) {
					DungeonRoom currentRoom = dungeonMap[row, col];
					LinkRoom(currentRoom, col, row);
				}
			}
		}

		public DungeonRoom GetRandomRoom() {
			Random rng = new();
			return dungeonMap[rng.Next(0, dungeonMap.GetLength(0)), rng.Next(0, dungeonMap.GetLength(1))];
		}

		public DungeonRoom? GetRoomAt(int col, int row) {
			if ( row > dungeonMap.GetLength(0) || row < 0 || col < 0 || col > dungeonMap.GetLength(1) )
				return null;
			return dungeonMap[row, col];
		}

	}
}
