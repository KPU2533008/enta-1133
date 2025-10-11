namespace GD14_1133_DiceGame_Peskoff_Rob.engine.datatype {
	internal class UDim2 {

		public readonly UDim X = new(0, 0);
		public readonly UDim Y = new(0, 0);

		public UDim2(float xScale = 0, int xOffset = 0, float yScale = 0, int yOffset = 0) {
			X = new(xScale, xOffset);
			Y = new(yScale, yOffset);
		}

		public static UDim2 FromScale(float xScale, float yScale) {
			return new(xScale, 0, yScale, 0);
		}

		public static UDim2 FromOffset(int xOffset, int yOffset) {
			return new(0, xOffset, 0, yOffset);
		}

	}
}
