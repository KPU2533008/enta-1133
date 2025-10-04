using GD14_1133_DiceGame_Peskoff_Rob.Game.Util;
using GD14_1133_DiceGame_Peskoff_Rob.Input;

namespace GD14_1133_DiceGame_Peskoff_Rob.Menu {
	internal class MenuManager {

		private IMenu? currentMenu;
		private string lastOut = "";

		internal MenuManager(IMenu startMenu) {
			currentMenu = startMenu;
		}

		private void DrawMenu(IMenu menu) {
			lastOut = "";

			while ( true ) {
				// Console.Clear() does not clear the scrollback buffer
				// This appears to fix the issue
				// https://stackoverflow.com/questions/75471607/console-clear-doesnt-clean-up-the-whole-console
				Console.Clear();
				Console.WriteLine("\x1b[3J");
				Console.Clear();

				if ( menu.ShouldDisplayHeader() ) {
					Display.PrintHeader(1, menu.ToString());
				}

				menu.Draw();
				Display.PrintLn("");

				IMenu? previousMenu = menu.GetPreviousMenu();
				List<SMenuOption> options = menu.GetMenuOptions(this);

				if ( options.Count == 0 && previousMenu != null ) {
					this.GoToMenu(previousMenu);
					return;
				}

				for ( int i = 0; i < options.Count; i++ ) {
					SMenuOption option = options[i];
					Display.PrintLn($"{i + 1}. {option.displayText}");
				}

				Display.Print($"\n{new String('-', Display.GetHeaderLength(1))}\n{lastOut}\n{new String('-', Display.GetHeaderLength(1))}\nInput Log:\n{InputHandler.ToString()}");

				string input = InputHandler.ReadInput();
				SMenuActionResult menuActionResult = new();

				if ( input == "back" && previousMenu != null ) {
					this.GoToMenu(previousMenu);
					break;
				}

				for ( int i = 0; i < options.Count; i++ ) {
					SMenuOption option = options[i];

					if ( input != option.name.ToLower() && input != ( i + 1 ).ToString() ) {
						continue;
					}

					menuActionResult = option.onChosen();
					lastOut = menuActionResult.message;
					break;
				}

				if ( menuActionResult.result == true ) {
					break;
				} else if ( menuActionResult.result == false ) {
					lastOut = $"\"{input}\" is not valid input for this menu";
				}
			}
		}

		internal void GoToMenu(IMenu? menu) {
			currentMenu = menu;
		}

		internal void Start() {
			while ( currentMenu != null ) {
				DrawMenu(currentMenu);
			}
		}

	}
}
