using Terraria.ID;

namespace TWitchery.Recipes.RecipeItems;
class AnyGoldBar : RecipeItemPool {
	public AnyGoldBar(int stack = 1) : base(ItemID.GoldBar, stack) {
		Pool = new int[] {
			ItemID.GoldBar,
			ItemID.PlatinumBar
		};
	}
}