
using GD14_1133_DiceGame_Peskoff_Rob.engine.input;
using GD14_1133_DiceGame_Peskoff_Rob.engine.@object;
using GD14_1133_DiceGame_Peskoff_Rob.game.combat;
using GD14_1133_DiceGame_Peskoff_Rob.game.@enum;
using GD14_1133_DiceGame_Peskoff_Rob.game.item;
using GD14_1133_DiceGame_Peskoff_Rob.game.item.consumable;
using GD14_1133_DiceGame_Peskoff_Rob.game.item.weapon;
using GD14_1133_DiceGame_Peskoff_Rob.util;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.@object {
	internal class DungeonGamePlayer : Combatant {

		private readonly int INVENTORY_SIZE = 10;
		private readonly List<Item> inventory = [];
		public readonly Signal<bool> inventoryChanged = new();

		public DungeonGamePlayer() : base(15) {
			Name = "Player";
		}

		public void SetName(string newName) {
			Name = newName;
		}

		/*
		 * Prompt the player to select an item in their inventory,
		 * optionally of a narrower type than just `Item`. For
		 * example, `Weapon` or `Consumable`.
		 */
		public Item? PromptSelectItem(Type itemType) {
			string itemNameString = (
				itemType == typeof(Weapon) ? "a weapon" :
				itemType == typeof(Consumable) ? "a consumable" :
				"an item"
			);

			Item? chosenItem = null;

			while ( true ) {
				Game.dialogWindow.ShowDialog($"Type the name of {itemNameString} (or its #), or \"back\" if you've changed your mind.", false);
				UserInputService.GetValidInput((string input, out bool isValid) => {
					if ( input.Equals("back", StringComparison.CurrentCultureIgnoreCase) ) {
						isValid = true;
						return null;
					}

					Item? chosenItem = null;
					bool isInt = int.TryParse(input, out int itemIdx);
					itemIdx--;

					if ( isInt && Math.Clamp(itemIdx, 0, inventory.Count - 1) == itemIdx ) {
						chosenItem = inventory[itemIdx];
					} else {
						foreach ( Item item in inventory ) {
							if ( input.Equals(item.Name, StringComparison.CurrentCultureIgnoreCase) )
								chosenItem = item;
						}
					}

					isValid = chosenItem != null;
					return chosenItem;
				}, out chosenItem);

				if ( chosenItem == null || chosenItem.GetType().IsSubclassOf(itemType) ) {
					break;
				}

				Game.dialogWindow.ShowDialog($"This is not {itemNameString}.", true);
			}

			return chosenItem;
		}

		public Item? PromptSelectItemForUse(Type itemType) {
			Item? selectedItem = PromptSelectItem(itemType);

			while ( selectedItem != null && !CanUseItem(selectedItem) ) {
				Game.dialogWindow.ShowDialog($"{selectedItem.Name} cannot be used right now.");
				selectedItem = PromptSelectItem(itemType);
			}

			return selectedItem;
		}

		/*
		 * Prompt the player to drop an item in their inventory.
		 * Do not allow them to drop the only weapon they have.
		 */
		public void PromptDropItem() {
			Item? chosenItem;

			while ( true ) {
				chosenItem = PromptSelectItem(typeof(Item));

				if ( chosenItem == null || !chosenItem.GetType().IsSubclassOf(typeof(Weapon)) ) {
					break;
				} else if ( chosenItem.GetType().IsSubclassOf(typeof(Weapon)) ) {
					int numWeapons = 0;

					foreach ( Item item in inventory ) {
						if ( item is Weapon )
							numWeapons++;
					}

					if ( numWeapons <= 1 )
						Game.dialogWindow.ShowDialog($"You can't throw away your only weapon!", true);
					else
						break;
				}

			}

			if ( chosenItem != null ) {
				TakeItem(chosenItem);
			}

			Game.dialogWindow.ClearText();
		}

		public int GiveItem(Item item) {
			if ( inventory.Contains(item) )
				return -1; // ??? hax or im bad

			/*
			 * We're nice and we give the user the option to throw
			 * away an item if their inventory is full.
			 */
			if ( inventory.Count >= INVENTORY_SIZE ) {
				Game.dialogWindow.ShowDialog($"Your inventory is full. Would you like to throw away an item to make space?", ["Yes", "No"]);
				bool doPromptDropItem = false;

				Game.dialogWindow.optionChosen.Once((int optionNum) => {
					doPromptDropItem = optionNum == 0;
				});
				Game.dialogWindow.optionChosen.Wait();

				if ( doPromptDropItem )
					PromptDropItem();

				if ( inventory.Count >= INVENTORY_SIZE )
					return -2;
			}

			inventory.Add(item);
			inventoryChanged.Fire(true);
			return 0;
		}

		public int TakeItem(Item item) {
			if ( !inventory.Contains(item) )
				return -1;

			inventory.Remove(item);
			inventoryChanged.Fire(false);
			return 0;
		}

		public Item[] GetInventoryItems() {
			return inventory.ToArray();
		}

		public override string GetDefeatMessage() {
			return $"{GetFullName()} sustains fatal injuries and meets their demise...";
		}

		public override string GetPassMessage() {
			return $"{GetFullName()} passes up their turn and bides their time.";
		}

		public override CombatAction SelectCombatAction() {
			CombatAction chosenAction = CombatAction.Pass;

			List<string> options = [];
			foreach ( CombatAction action in Enum.GetValues(typeof(CombatAction)) ) {
				options.Add(StringUtil.AddSpaces(action.ToString()));
			}

			Game.dialogWindow.ShowDialog($"{GetFullName()}, choose an action to take this turn!", [.. options]);
			Game.dialogWindow.optionChosen.Once((int optionNum) => {
				chosenAction = (CombatAction)optionNum;
			});
			Game.dialogWindow.optionChosen.Wait();

			return chosenAction;
		}

		public override Weapon SelectWeapon() {
			Weapon? selectedItem = PromptSelectItemForUse(typeof(Weapon)) as Weapon;
			return selectedItem ?? new PromptCombatActionAgainWeapon(); // The explanation for this dumbfuckery can be found in CombatEncounter.cs
		}

		public override Consumable SelectConsumable() {
			Consumable? selectedItem = PromptSelectItemForUse(typeof(Consumable)) as Consumable;

			if ( selectedItem != null ) {
				TakeItem(selectedItem);
			}

			return selectedItem ?? new PromptCombatActionAgainConsumable(); // The explanation for this dumbfuckery can be found in CombatEncounter.cs
		}

		public override Combatant SelectTarget(Combatant[] validTargets) {
			List<string> options = [];

			foreach ( Combatant target in validTargets ) {
				options.Add(target.GetFullName());
			}

			int chosenTargetNum = 0;

			Game.dialogWindow.ShowDialog($"Now choose a target for this action!", [.. options]);
			Game.dialogWindow.optionChosen.Once((int optionNum) => {
				chosenTargetNum = optionNum;
			});
			Game.dialogWindow.optionChosen.Wait();

			return validTargets[chosenTargetNum];
		}

		public override void Destroy() {
			base.Destroy();
			inventoryChanged.Destroy();
		}

	}
}
