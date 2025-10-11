namespace GD14_1133_DiceGame_Peskoff_Rob.menu {
	internal interface IMenu {

		internal string GetMenuId();
		internal bool ShouldDisplayHeader();
		internal IMenu? GetPreviousMenu();
		internal List<SMenuOption> GetMenuOptions(MenuManager menuManager);
		internal void Draw();

	}
}
