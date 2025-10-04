using GD14_1133_DiceGame_Peskoff_Rob.Menu;

namespace GD14_1133_DiceGame_Peskoff_Rob {
	internal class Program {

		static void Main(string[] args) {
			Console.Title = "Rob Peskoff's DIRTY DICE";
			IMenu mainMenu = new MainMenu();
			MenuManager menuManager = new(mainMenu);
			menuManager.Start();
		}

	}
}
