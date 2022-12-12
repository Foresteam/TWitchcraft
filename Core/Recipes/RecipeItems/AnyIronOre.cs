using Terraria.ID;

namespace TWitchery.Recipes.RecipeItems;
class AnyIronOre : RecipeItemPool {
	public AnyIronOre(int stack = 1) : base(ItemID.IronOre, stack) {
		Pool = new int[] {
			ItemID.IronOre,
			ItemID.LeadOre
		};
	}
}