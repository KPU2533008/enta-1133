using GD14_1133_DiceGame_Peskoff_Rob.game.@abstract;
using GD14_1133_DiceGame_Peskoff_Rob.game.@enum;
using GD14_1133_DiceGame_Peskoff_Rob.game.item;
using GD14_1133_DiceGame_Peskoff_Rob.game.item.consumable;
using GD14_1133_DiceGame_Peskoff_Rob.game.item.weapon;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.combat {
	public abstract class Combatant : LivingEntity {

		public char Suffix { get; set; } = ' ';
		public Team? Team { get; private set; }

		public int XP {
			get;
			set {
				int sign = Math.Sign(value);
				int lvlIncrease = Math.Min(sign, 0);

				while ( ( value >= GetMaxXpForLevel(Level + lvlIncrease) || value < 0 ) && sign == Math.Sign(value) ) {
					value = value - GetMaxXpForLevel(Level + lvlIncrease) * sign;
					lvlIncrease += sign;
				}

				lvlIncrease -= Math.Min(sign, 0);
				field = value;
				if ( lvlIncrease != 0 )
					Level += lvlIncrease;
			}
		} = 0;

		public int MaxXP => GetMaxXpForLevel(Level);

		public int Level {
			get;
			set {
				value = Math.Max(value, 1);
				field = value;
				if ( Math.Min(XP, MaxXP - 1) != XP )
					XP = Math.Min(XP, MaxXP - 1);
			}
		} = 1;

		public abstract CombatAction SelectCombatAction();
		public abstract Weapon SelectWeapon();
		public abstract Consumable SelectConsumable();
		public abstract Combatant SelectTarget(Combatant[] validTargets);

		public abstract string GetPassMessage();
		public abstract string GetDefeatMessage();

		private static int GetMaxXpForLevel(int level) {
			return 10 + (int)( Math.Pow(5 * ( level - 1 ), 1.5f) );
		}

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
