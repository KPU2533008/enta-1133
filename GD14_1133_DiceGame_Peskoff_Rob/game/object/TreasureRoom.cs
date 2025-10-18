using GD14_1133_DiceGame_Peskoff_Rob.game.item;
using GD14_1133_DiceGame_Peskoff_Rob.game.item.consumable;
using GD14_1133_DiceGame_Peskoff_Rob.game.item.weapon.cpu;
using GD14_1133_DiceGame_Peskoff_Rob.game.item.weapon.player;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.@object {
	internal class TreasureRoom : DungeonRoom {

		private bool hasTreasure;
		private bool foundTreasure = false;

		private delegate Item LootItemConstructor();
		private readonly Dictionary<LootItemConstructor, int> LOOT_TABLE = new() {
			{ () => { return new Bandage(); }, 15 },
			{ () => { return new HealthPotion(); }, 6 },
			{ () => { return new ShadowSword(); }, 4 },
			{ () => { return new Mjolnir(); }, 1 },
			{ () => { return new Excalibur(); }, 1 },
		};

		public TreasureRoom() {
			Random rng = new Random();
			hasTreasure = rng.Next(0, 3) != 0;
		}

		private int SumLootTableWeights(Dictionary<LootItemConstructor, int> lootTable) {
			int totalWeight = 0;

			foreach ( KeyValuePair<LootItemConstructor, int> kvp in lootTable ) {
				totalWeight += kvp.Value;
			}

			return totalWeight;
		}

		private Item RollLootTable(Dictionary<LootItemConstructor, int> lootTable) {
			Item? item = null;
			Random rng = new();
			int remainingDistance = (int)( SumLootTableWeights(lootTable) * rng.NextSingle() );

			foreach ( KeyValuePair<LootItemConstructor, int> kvp in lootTable ) {
				remainingDistance -= kvp.Value;
				if ( remainingDistance < 0 ) {
					item = kvp.Key();
				}
			}

			// Shut up. It exists.
			return (Item)item;
		}

		public override string GetRoomDescription() {
			return "Treasure Room";
		}

		public override void OnSearched(DungeonGamePlayer player) {
			base.OnSearched(player);
			if ( hasTreasure ) {
				if ( !foundTreasure ) {
					foundTreasure = true;
					Item foundLoot = RollLootTable(LOOT_TABLE);
					Game.dialogWindow.ShowDialog($"You find a treasure chest with a {foundLoot.Name} inside!");
					player.GiveItem(foundLoot);
				} else {
					Game.dialogWindow.ShowDialog("You rummage through the treasure chest again but come up empty handed. It seems you've already found everything here.");
				}
			} else {
				Game.dialogWindow.ShowDialog("You search and search but come up empty handed. It seems the room is barren.");
			}
		}
	}
}
