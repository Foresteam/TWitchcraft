namespace TWitchery.Cauldron;
partial class Crafting {
	public enum Action {
		Nothing = StackedInventory.Action.Nothing,
		Take = StackedInventory.Action.Take,
		Put = StackedInventory.Action.Put,
		PutCatalyst,
		Pour,
		Draw,
		Craft
	}
}