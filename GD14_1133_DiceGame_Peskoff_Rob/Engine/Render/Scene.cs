namespace GD14_1133_DiceGame_Peskoff_Rob.engine.render {
	internal class Scene {

		private static List<IDrawable> objects = new();

		internal static void Load(IDrawable[] objArr) {
			objects = objArr.ToList();
		}

		internal static bool Add(IDrawable obj) {
			if ( objects.Contains(obj) ) {
				return false;
			}
			objects.Add(obj);
			return true;
		}

		internal static bool Remove(IDrawable obj) {
			return objects.Remove(obj);
		}

		internal static void Clear() {
			objects.Clear();
		}

		internal static IDrawable[] GetObjects() {
			return objects.ToArray();
		}

	}
}
