using GD14_1133_DiceGame_Peskoff_Rob.game.@enum;
using GD14_1133_DiceGame_Peskoff_Rob.game.util;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.@object {
	public abstract class DungeonRoom {

		protected bool isVisited = false;
		public DungeonRoom? north = null;
		public DungeonRoom? south = null;
		public DungeonRoom? east = null;
		public DungeonRoom? west = null;

		public DungeonRoom? GetNextRoom(MoveDirection dir) {
			switch ( dir ) {
				case MoveDirection.North:
					return north;
				case MoveDirection.East:
					return east;
				case MoveDirection.South:
					return south;
				case MoveDirection.West:
					return west;
			}
			return null;
		}

		public virtual string GetRoomDescription() {
			return "Room";
		}

		public virtual void OnEntered(DungeonGamePlayer player) {
			Game.dialogWindow.ShowDialog($"You find yourself in a room with a layout that's {( isVisited ? "" : "un" )}familiar to you. You've {( isVisited ? "" : "not" )} seen this place before...");
			isVisited = true;
		}

		public virtual void OnSearched(DungeonGamePlayer player) {
			Game.dialogWindow.ShowDialog("You scour the room thoroughly, hoping to find something useful...");
		}

		public virtual void OnExited(DungeonGamePlayer player) {
			Game.dialogWindow.ShowDialog("You leave the room, content with what you've seen, and press onward.");
		}

	}
}
