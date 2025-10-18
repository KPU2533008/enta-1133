namespace GD14_1133_DiceGame_Peskoff_Rob.engine.render {
	internal interface IDrawable {

		public bool CanDraw() { return true; }
		public void Draw(Canvas canvas);

	}
}
