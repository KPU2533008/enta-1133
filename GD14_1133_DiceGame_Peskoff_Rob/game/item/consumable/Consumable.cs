using GD14_1133_DiceGame_Peskoff_Rob.game.combat;
using GD14_1133_DiceGame_Peskoff_Rob.game.@object;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.item.consumable {
	internal abstract class Consumable(Dice dice) : Item(dice) {

		protected string usageText = "@USER uses a @ITEM on @TARGET!";

		public override void OnUse(Combatant user, Combatant target) {
			Game.dialogWindow.ShowDialog(usageText.Replace("@USER", user.GetFullName()).Replace("@ITEM", Name).Replace("@TARGET", ( user == target ? "themself" : target.GetFullName() )));
		}

	}
}
