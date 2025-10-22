namespace GD14_1133_DiceGame_Peskoff_Rob.engine.datatype {
	internal class Vector2 {

		public readonly int X, Y;

		public Vector2(int x = 0, int y = 0) {
			X = x;
			Y = y;
		}

		public override string ToString() {
			return $"{X}, {Y}";
		}

		public static Vector2 operator -(Vector2 v) {
			return new(-v.X, -v.Y);
		}

		public static Vector2 operator +(Vector2 a, Vector2 b) {
			return new(a.X + b.X, a.Y + b.Y);
		}

		public static Vector2 operator -(Vector2 a, Vector2 b) {
			return new(a.X - b.X, a.Y - b.Y);
		}

		public static Vector2 operator *(Vector2 a, Vector2 b) {
			return new(a.X * b.X, a.Y * b.Y);
		}

	}
}
