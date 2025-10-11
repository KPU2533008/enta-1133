namespace GD14_1133_DiceGame_Peskoff_Rob.engine.datatype {
	internal class UDim {

		public readonly float scale = 0;
		public readonly int offset = 0;

		public UDim(float scale, int offset) {
			this.scale = scale;
			this.offset = offset;
		}

		public static UDim FromScale(float scale) {
			return new(scale, 0);
		}

		public static UDim FromOffset(int offset) {
			return new(0, offset);
		}

	}
}
