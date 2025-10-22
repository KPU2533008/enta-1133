using GD14_1133_DiceGame_Peskoff_Rob.game.@abstract;
using GD14_1133_DiceGame_Peskoff_Rob.game.combat;
using GD14_1133_DiceGame_Peskoff_Rob.game.@enum;
using GD14_1133_DiceGame_Peskoff_Rob.game.@object;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.item {
	internal abstract class Item(Dice dice) : Entity {

		protected readonly Dice dice = dice;
		public int LastRoll => dice.GetLastRoll();

		public string Description { get; protected set; } = "A regular item. It doesn't do anything.";
		public Allegiance TargetAllegiance { get; protected set; } = Allegiance.Hostile;
		public Mortality TargetMortality { get; protected set; } = Mortality.Alive;

		public int Roll() {
			return dice.Roll();
		}

		public abstract void OnUse(Combatant user, Combatant target);

	}
}
