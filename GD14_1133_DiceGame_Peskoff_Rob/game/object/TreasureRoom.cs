using GD14_1133_DiceGame_Peskoff_Rob.game.util;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.@object {
	internal class TreasureRoom : DungeonRoom {

		private bool hasTreasure;
		private bool foundTreasure = false;

		public TreasureRoom() {
			Random rng = new Random();
			this.hasTreasure = rng.Next(0, 2) == 0;
		}

		public override string GetRoomDescription() {
			return "Treasure Room";
		}

		public override void OnSearched() {
			base.OnSearched();
			Sugar.Wait(3);
			if ( hasTreasure ) {
				if ( !foundTreasure ) {
					// TODO: Add die to inventory
					foundTreasure = true;
					Typewriter.Play(Game.dialogText, "You find a treasure chest with shiny loot inside!");
				} else {
					Typewriter.Play(Game.dialogText, "You rummage through the treasure chest again but come up empty handed. It seems you've already found everything here.");
				}
			} else {
				Typewriter.Play(Game.dialogText, "You search and search but come up empty handed. It seems the room is barren.");
			}
		}
	}
}
