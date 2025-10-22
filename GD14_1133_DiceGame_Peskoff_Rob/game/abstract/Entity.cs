namespace GD14_1133_DiceGame_Peskoff_Rob.game.@abstract {
	internal abstract class Entity {
		public string Name { get; protected set; } = "Entity";

		public virtual void Destroy() { }
	}
}
