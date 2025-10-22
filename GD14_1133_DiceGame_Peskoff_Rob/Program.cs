using GD14_1133_DiceGame_Peskoff_Rob.engine;
using GD14_1133_DiceGame_Peskoff_Rob.game;

namespace GD14_1133_DiceGame_Peskoff_Rob {
	internal class Program {

		static void Main(string[] args) {
			Engine.Run();
			Game.Run();

			//IMenu mainMenu = new MainMenu();
			//MenuManager menuManager = new(mainMenu);
			//menuManager.Start();
		}

	}
}
