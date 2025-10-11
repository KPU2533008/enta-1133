using GD14_1133_DiceGame_Peskoff_Rob.engine;
using GD14_1133_DiceGame_Peskoff_Rob.engine.datatype;
using GD14_1133_DiceGame_Peskoff_Rob.engine.input;
using GD14_1133_DiceGame_Peskoff_Rob.engine.render;
using GD14_1133_DiceGame_Peskoff_Rob.engine.@enum;
using GD14_1133_DiceGame_Peskoff_Rob.engine.instance.gui;

namespace GD14_1133_DiceGame_Peskoff_Rob.game {
	internal class Game {

		public static readonly Frame mainViewport = new Frame();

		public static readonly Frame dialogBox = new Frame();
		public static readonly TextLabel dialogText = new TextLabel();

		private static readonly Frame dialogOptions = new Frame();
		public static readonly TextLabel optionsList = new TextLabel();

		//private static readonly Frame worldMap = new Frame();
		private static readonly Frame inventory = new Frame();
		private static readonly Frame inventoryPadding = new Frame();
		private static readonly TextLabel inventoryLabel = new TextLabel();


		private static readonly TextLabel inputDisplay = new TextLabel();

		private static readonly TextLabel gameResolution = new TextLabel();


		public static void Run() {
			Console.Title = GameSettings.GAME_NAME;

			mainViewport.borderEnabled = true;
			mainViewport.size = new(1, 0, 1, -12);

			gameResolution.position = UDim2.FromOffset(1, 1);
			gameResolution.size = UDim2.FromOffset(30, 1);
			gameResolution.textXAlignment = TextXAlignment.Left;
			gameResolution.Parent = mainViewport;

			dialogBox.borderEnabled = true;
			dialogBox.position = new(0, 0, 1, -12);
			dialogBox.size = new(0.5f, 0, 0, 5);

			dialogText.position = UDim2.FromOffset(2, 1);
			dialogText.size = new(1, -4, 0, 3);
			dialogText.text = "";
			dialogText.textXAlignment = TextXAlignment.Left;
			dialogText.textYAlignment = TextYAlignment.Center;
			dialogText.textWrapped = true;
			dialogText.Parent = dialogBox;

			dialogOptions.position = new(0, 0, 1, -7);
			dialogOptions.size = new(0.5f, 0, 0, 6);
			dialogOptions.borderEnabled = true;

			optionsList.position = UDim2.FromOffset(2, 1);
			optionsList.size = new(1, -4, 1, -2);
			optionsList.Parent = dialogOptions;
			optionsList.textXAlignment = TextXAlignment.Left;

			inventory.position = new(0.5f, 0, 1, -12);
			inventory.size = new(0.5f, 0, 0, 12);
			inventory.borderEnabled = true;

			inventoryPadding.position = UDim2.FromOffset(2, 1);
			inventoryPadding.size = new(1, -4, 1, -2);
			inventoryPadding.Parent = inventory;

			inventoryLabel.size = new(1, 0, 0, 1);
			inventoryLabel.text = "INVENTORY";
			inventoryLabel.textXAlignment = TextXAlignment.Left;
			inventoryLabel.Parent = inventoryPadding;

			inputDisplay.position = new(0, 0, 1, -1);
			inputDisplay.size = new(0.5f, 0, 0, 1);
			inputDisplay.clipsDescendants = true;
			inputDisplay.textXAlignment = TextXAlignment.Left;

			Engine.RunService.PreRender.Connect((float dt) => {
				gameResolution.text = $"RESOLUTION: {Renderer.RENDER_RESOLUTION.ToString()}";
				inputDisplay.text = $"> {UserInputService.inProgressPhrase}";
			});

			//DiceGameRunner gameRunner = new();
			//gameRunner.RunGame();

			DungeonGameRunner gameRunner = new();
			gameRunner.RunGame();

			while ( true ) {
				Thread.Sleep(1);
			}
		}

	}
}
