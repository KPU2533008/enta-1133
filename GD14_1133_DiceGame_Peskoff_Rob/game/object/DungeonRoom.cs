using GD14_1133_DiceGame_Peskoff_Rob.game.util;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.@object {
	internal abstract class DungeonRoom {

		protected bool isVisited = false;
		public DungeonRoom? north = null;
		public DungeonRoom? south = null;
		public DungeonRoom? east = null;
		public DungeonRoom? west = null;

		public virtual string GetRoomDescription() {
			return "Room";
		}

		public virtual void OnEntered() {
			Typewriter.Play(Game.dialogText, $"You enter a room with a layout that's {( isVisited ? "" : "un" )}familiar to you. You've {( isVisited ? "" : "not" )} seen this place before...");
			isVisited = true;
		}

		public virtual void OnSearched() {
			Typewriter.Play(Game.dialogText, $"You scour the room thoroughly, hoping to find something useful...");
		}

		public virtual void OnExited() {
			Typewriter.Play(Game.dialogText, $"You leave the room, content with what you've seen, and press onward.");
		}

	}
}
