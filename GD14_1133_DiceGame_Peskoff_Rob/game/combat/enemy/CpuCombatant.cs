using GD14_1133_DiceGame_Peskoff_Rob.game.@enum;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.combat.enemy {
	internal abstract class CpuCombatant(int maxHp) : Combatant(maxHp) {
		public override CombatAction SelectCombatAction() {
			Random rng = new();
			int roll = rng.Next(0, 10);

			/*
			 * Attack - 70%
			 * UseItem - 20%
			 * Pass - 10%
			 */
			if ( roll < 1 ) {
				return CombatAction.Pass;
			} else if ( roll < 3 ) {
				return CombatAction.UseItem;
			} else {
				return CombatAction.Attack;
			}
		}

		public override Combatant SelectTarget(Combatant[] validTargets) {
			Random rng = new();
			return validTargets[rng.Next(0, validTargets.Length)];
		}
	}
}
