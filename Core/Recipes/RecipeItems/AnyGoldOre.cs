using Terraria.ID;

namespace TWitchery.Recipes.RecipeItems;
class AnyGoldOre : RecipeItemPool {
	public AnyGoldOre(int stack = 1) : base(ItemID.GoldOre, stack) {}
	private static readonly int[] _pool = new int[] {
		ItemID.GoldOre,
		ItemID.PlatinumOre
	};
	public override int[] Pool => _pool;
}