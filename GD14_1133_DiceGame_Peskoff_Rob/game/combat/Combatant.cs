using GD14_1133_DiceGame_Peskoff_Rob.game.@abstract;
using GD14_1133_DiceGame_Peskoff_Rob.game.@enum;
using GD14_1133_DiceGame_Peskoff_Rob.game.item;
using GD14_1133_DiceGame_Peskoff_Rob.game.item.consumable;
using GD14_1133_DiceGame_Peskoff_Rob.game.item.weapon;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.combat {
	internal abstract class Combatant : LivingEntity {

		public char Suffix { get; set; } = ' ';
		public Team? Team { get; private set; }

		public abstract CombatAction SelectCombatAction();
		public abstract Weapon SelectWeapon();
		public abstract Consumable SelectConsumable();
		public abstract Combatant SelectTarget(Combatant[] validTargets);

		public abstract string GetPassMessage();
		public abstract string GetDefeatMessage();

		public Combatant(int maxHp) : base(maxHp) {
			Name = "Combatant";
		}

		public string GetFullName() {
			return Name + ( Suffix == ' ' ? "" : $" {Suffix}" );
		}

		public bool CanAct() {
			return true;
		}

		protected bool CanUseItem(Item item) {
			if ( Team == null )
				return false;
			return ( Team.GetAllegiantMembers(item.TargetAllegiance, item.TargetMortality).Length > 0 );
		}

		public void SetTeam(Team? team) {
			if ( Team == team ) {
				return;
			}

			Team? lastTeam = Team;
			Team = team;

			lastTeam?.RemoveMember(this);
			team?.AddMember(this);
		}

	}
}
