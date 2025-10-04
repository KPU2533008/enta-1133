namespace GD14_1133_DiceGame_Peskoff_Rob.Menu {
	internal interface IMenu {

		internal string GetMenuId();
		internal bool ShouldDisplayMenuName();
		internal IMenu? GetPreviousMenu();
		internal List<IMenuOption> GetMenuOptions(MenuManager menuManager);
		internal void RenderMenu();

	}
}
