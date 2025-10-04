using GD14_1133_DiceGame_Peskoff_Rob.Game.Util;
using System.Net.Sockets;

namespace GD14_1133_DiceGame_Peskoff_Rob.Menu {
	internal class MenuManager {

		private int separatorLength = 48;
		private IMenu? currentMenu;

		internal MenuManager(IMenu startMenu) {
			currentMenu = startMenu;
		}

		private void RunMenu(IMenu menu) {
			// TODO: Include everything before the while loop in the loop along with a Console.Clear() at the start in order to create really nice dynamically updating menus
			// For now we will leave it as is so that the output is clear
			if ( menu.ShouldDisplayMenuName() ) {
				string topLineDashes = new String('-', ( separatorLength - menu.ToString().Length ) / 2 - 2);
				Display.PrintLn($"{topLineDashes}[ {menu} ]{topLineDashes}\n{new String('-', separatorLength)}");
			}

			menu.RenderMenu();
			Display.PrintLn("");

			IMenu? previousMenu = menu.GetPreviousMenu();
			List<IMenuOption> options = menu.GetMenuOptions(this);

			if ( options.Count == 0 && previousMenu != null ) {
				this.GoToMenu(previousMenu);
				return;
			}

			for ( int i = 0; i < options.Count; i++ ) {
				IMenuOption option = options[i];
				Display.PrintLn($"{i + 1}. {option.displayText}");
			}

			Display.PrintLn("");

			while ( true ) {
				string input = ( Console.ReadLine() ?? "" ).ToLower();
				bool? inputProcessResult = false;

				if ( input == "back" && previousMenu != null ) {
					this.GoToMenu(previousMenu);
					break;
				}

				for ( int i = 0; i < options.Count; i++ ) {
					IMenuOption option = options[i];

					if ( input != option.name.ToLower() && input != ( i + 1 ).ToString() ) {
						continue;
					}

					inputProcessResult = option.onChosen();
					break;
				}

				if ( inputProcessResult == true ) {
					break;
				} else if ( inputProcessResult == false ) {
					Display.PrintLn($"\"{input}\" is not valid input for this menu");
				}
			}
		}

		internal void GoToMenu(IMenu? menu) {
			currentMenu = menu;
		}

		internal void Start() {
			while ( currentMenu != null ) {
				RunMenu(currentMenu);
			}
		}

	}
}
