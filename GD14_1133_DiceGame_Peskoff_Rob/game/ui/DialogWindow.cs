using GD14_1133_DiceGame_Peskoff_Rob.engine.datatype;
using GD14_1133_DiceGame_Peskoff_Rob.engine.@enum;
using GD14_1133_DiceGame_Peskoff_Rob.engine.input;
using GD14_1133_DiceGame_Peskoff_Rob.engine.instance.gui;
using GD14_1133_DiceGame_Peskoff_Rob.engine.@object;
using GD14_1133_DiceGame_Peskoff_Rob.game.util;

namespace GD14_1133_DiceGame_Peskoff_Rob.game.ui {
	internal class DialogWindow {

		private readonly LabeledWindow dialogBox = new(new(0, 0, 1, -12), new(0.5f, 0, 0, 5), "GAME LOG");
		private readonly TextLabel dialogText = new();

		private readonly LabeledWindow dialogOptions = new(new(0, 0, 1, -7), new(0.5f, 0, 0, 6), "OPTIONS");
		private readonly TextLabel optionsList = new();

		private string[] lastOptions = [];
		public readonly Signal<int> optionChosen = new();

		public DialogWindow() {
			dialogText.size = UDim2.FromScale(1, 1);
			dialogText.text = "";
			dialogText.textXAlignment = TextXAlignment.Left;
			dialogText.textYAlignment = TextYAlignment.Center;
			dialogText.textWrapped = true;
			dialogText.Parent = dialogBox.container;

			optionsList.size = UDim2.FromScale(1, 1);
			optionsList.Parent = dialogOptions.container;
			optionsList.textXAlignment = TextXAlignment.Left;

			UserInputService.phraseEntered.Connect((string phrase) => {
				if ( lastOptions.Length == 0 || phrase.Length == 0 ) { return; }

				for ( int i = 0; i < lastOptions.Length; i++ ) {
					if ( phrase == ( i + 1 ).ToString() ) {
						optionChosen.Fire(i);
						return;
					}

					string option = lastOptions[i];
					string[] allowedInputs = option.Split(' ');

					for ( int j = 0; j < allowedInputs.Length; j++ ) {
						string allowedInput = allowedInputs[j].ToLower();
						if ( phrase.ToLower() == allowedInput ) {
							optionChosen.Fire(i);
							return;
						}
					}
				}
			});

			optionChosen.Connect((int optionNum) => {
				optionsList.text = "";
			});
		}

		public void ClearText() {
			dialogText.text = "";
		}

		public void ClearOptions() {
			optionsList.text = "";
			lastOptions = [];
		}

		public void ShowDialog(string dialog, bool awaitEnter = true) {
			Typewriter.Play(dialogText, dialog);
			if ( awaitEnter ) {
				UserInputService.phraseEntered.Wait();
			}
		}

		public void ShowDialog(string dialog, string[] options) {
			Typewriter.Play(dialogText, dialog);
			Sugar.Wait(0.5f);
			ShowOptions(options);
		}

		public void ShowOptions(string[] options) {
			lastOptions = options.ToArray();

			string optionsText = $"1) {options[0]}";
			for ( int i = 1; i < options.Length; i++ ) {
				optionsText += $"\n{i + 1}) {options[i]}";
			}

			Typewriter.Play(optionsList, optionsText);
		}

		public string[] GetLastOptions() {
			return lastOptions.ToArray();
		}

	}
}
