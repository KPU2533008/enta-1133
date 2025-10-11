using GD14_1133_DiceGame_Peskoff_Rob.engine.input;
using GD14_1133_DiceGame_Peskoff_Rob.game.@object;
using GD14_1133_DiceGame_Peskoff_Rob.game.util;

namespace GD14_1133_DiceGame_Peskoff_Rob.game {
	internal class DungeonGameRunner {

		private Dungeon dungeon;
		private DungeonRoom currentRoom;
		private Player player;

		private bool awaitingAction = false;
		private bool awaitingMove = false;

		private Dictionary<string, Action> actions;

		public DungeonGameRunner() {
			actions = new Dictionary<string, Action>() {
				{ "move", () => {
					AskForDirection();
				} },
				{ "search", () => {
					currentRoom?.OnSearched();
					Sugar.Wait(3);
					AskForAction();
				} },
			};
		}

		private bool GetNextRoom(string dir, out DungeonRoom? room) {
			if ( dir == "north" ) {
				room = currentRoom.north;
			} else if ( dir == "east" ) {
				room = currentRoom.east;
			} else if ( dir == "south" ) {
				room = currentRoom.south;
			} else if ( dir == "west" ) {
				room = currentRoom.west;
			} else {
				room = null;
				return false;
			}
			return true;
		}

		private void AskForAction() {
			awaitingAction = true;
			Typewriter.Play(Game.dialogText, "What would you like to do?");
			Sugar.Wait(0.5f);
			Typewriter.Play(Game.optionsList, "1) Move rooms (\"move\")\n2) Search current room (\"search\")");
		}

		private void AskForDirection() {
			awaitingMove = true;
			Typewriter.Play(Game.dialogText, "Which direction would you like to go?");
			Sugar.Wait(0.5f);
			Typewriter.Play(Game.optionsList, "1) North\n2) South\n3) East\n4) West");
		}

		private void MoveToRoom(DungeonRoom nextRoom) {
			currentRoom.OnExited();
			Sugar.Wait(3);
			currentRoom = nextRoom;
			currentRoom.OnEntered();
			Sugar.Wait(3);
			AskForAction();
		}

		public void RunGame() {
			Random rng = new();

			dungeon = new(new(3, 3));
			currentRoom = dungeon.GetRandomRoom();
			player = new("Player", false);

			currentRoom.OnEntered();
			Sugar.Wait(3);
			AskForAction();

			UserInputService.phraseEntered.Connect((string phrase) => {
				phrase = phrase.ToLower();

				if ( awaitingAction ) {
					if ( actions.ContainsKey(phrase) ) {
						awaitingAction = false;
						Game.optionsList.text = "";
						Task.Run(actions[phrase]);
					}
				} else if ( awaitingMove ) {
					bool validDir = GetNextRoom(phrase, out DungeonRoom? nextRoom);
					if ( validDir ) {
						awaitingMove = false;
						Game.optionsList.text = "";
						if ( nextRoom != null ) {
							Task.Run(() => {
								MoveToRoom(nextRoom);
							});
						} else {
							Task.Run(() => {
								Typewriter.Play(Game.dialogText, "You look, but there doesn't seem to be another room in this direction.");
								Sugar.Wait(3.5f);
								AskForAction();
							});
						}
					}
				}
			});
		}

	}
}
