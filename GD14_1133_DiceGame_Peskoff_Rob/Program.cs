using GD14_1133_DiceGame_Peskoff_Rob.Game;
using GD14_1133_DiceGame_Peskoff_Rob.Game.Util;
using GD14_1133_DiceGame_Peskoff_Rob.Menu;

namespace GD14_1133_DiceGame_Peskoff_Rob {

	internal class Program {

		static void Main(string[] args) {
			Console.Title = "Rob Peskoff's Dice Game";
			Display.PrintLn($"Welcome to Rob Peskoff's Dice Game!\nIt is {DateTime.Now}\n");

			IMenu mainMenu = new MainMenu();
			MenuManager menuManager = new(mainMenu);
			menuManager.Start();
		}

	}

}
