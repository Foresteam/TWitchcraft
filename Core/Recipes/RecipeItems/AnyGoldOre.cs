using Terraria.ID;

namespace TWitchery.Recipes.RecipeItems;
class AnyGoldOre : RecipeItemPool {
	public AnyGoldOre(int stack = 1) : base(ItemID.GoldOre, stack) {
		Pool = new int[] {
			ItemID.GoldOre,
			ItemID.PlatinumOre
		};
	}
}