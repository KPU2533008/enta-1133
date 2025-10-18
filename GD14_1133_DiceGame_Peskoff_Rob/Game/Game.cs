using GD14_1133_DiceGame_Peskoff_Rob.engine.datatype;
using GD14_1133_DiceGame_Peskoff_Rob.engine.input;
using GD14_1133_DiceGame_Peskoff_Rob.engine.render;
using GD14_1133_DiceGame_Peskoff_Rob.engine.@enum;
using GD14_1133_DiceGame_Peskoff_Rob.engine.instance.gui;
using TaskScheduler = GD14_1133_DiceGame_Peskoff_Rob.engine.TaskScheduler;
using GD14_1133_DiceGame_Peskoff_Rob.game.ui;

namespace GD14_1133_DiceGame_Peskoff_Rob.game {
	internal class Game {

		public static readonly Frame mainViewport = new();

		public static readonly DialogWindow dialogWindow = new();

		public static readonly InventoryPanel inventory = new();

		public static readonly LabeledWindow dungeonMap = new(new(0.75f, 0, 1, -12), new(0.25f, 0, 0, 12), "MAP");

		private static readonly TextLabel inputDisplay = new();
		private static readonly TextLabel gameResolution = new();

		public static void Run() {
			Console.Title = GameSettings.GAME_NAME;

			mainViewport.borderEnabled = true;
			mainViewport.size = new(1, 0, 1, -12);

			gameResolution.position = UDim2.FromOffset(1, 1);
			gameResolution.size = UDim2.FromOffset(30, 1);
			gameResolution.textXAlignment = TextXAlignment.Left;
			gameResolution.visible = false;
			gameResolution.Parent = mainViewport;

			inputDisplay.position = new(0, 0, 1, -1);
			inputDisplay.size = new(0.5f, 0, 0, 1);
			inputDisplay.clipsDescendants = true;
			inputDisplay.textXAlignment = TextXAlignment.Left;

			TaskScheduler.PreRender.Connect((float dt) => {
				gameResolution.text = $"{Renderer.RENDER_RESOLUTION.ToString()} @ {TaskScheduler.GetAvgFrameLength() * 1000}ms";
				inputDisplay.text = $"> {UserInputService.inProgressPhrase}";
			});

			DungeonGameRunner gameRunner = new();
			gameRunner.RunGame();

			while ( true ) {
				Thread.Sleep(1);
			}
		}

	}
}
