namespace GD14_1133_DiceGame_Peskoff_Rob.engine.render {
	internal interface IDrawable {

		public bool IsCulled() { return false; }
		public void Draw(Canvas canvas);

	}
}
