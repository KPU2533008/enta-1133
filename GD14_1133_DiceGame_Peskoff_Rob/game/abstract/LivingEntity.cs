using GD14_1133_DiceGame_Peskoff_Rob.engine.@object;
using GD14_1133_DiceGame_Peskoff_Rob.game.@enum;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.@abstract {
	internal abstract class LivingEntity(int maxHp) : Entity {

		public int MaxHP {
			get;
			private set {
				field = value;
				MaxHpChanged.Fire(HP);
				HP = Math.Clamp(HP, 0, field);
			}
		} = maxHp;

		public int HP {
			get;
			private set {
				field = Math.Clamp(value, 0, MaxHP);
				HpChanged.Fire(HP);
			}
		} = maxHp;

		public readonly Signal<int> HpChanged = new();
		public readonly Signal<int> MaxHpChanged = new();

		public bool IsAlive => HP > 0;
		public Mortality Mortality => IsAlive ? Mortality.Alive : Mortality.Dead;

		public bool TakeDamage(int damage) {
			HP -= damage;
			return HP > 0;
		}

		public void Heal(int health) {
			HP += health;
		}

		public override void Destroy() {
			base.Destroy();
			HpChanged.Destroy();
			MaxHpChanged.Destroy();
		}

	}
}
