using Terraria.ID;

namespace TWitchery.Recipes.RecipeItems;
class AnyIronOre : RecipeItemPool {
	public AnyIronOre(int stack = 1) : base(ItemID.GoldOre, stack) { }
	private static readonly int[] _pool = new int[] {
		ItemID.IronOre,
		ItemID.LeadOre
	};
	public override int[] Pool => _pool;
}