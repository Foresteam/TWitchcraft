using Terraria.ID;

namespace TWitchery.Recipes.RecipeItems;
class AnyMediumAnimal : RecipeItemPool {
	public AnyMediumAnimal(int stack = 1) : base(ItemID.Bunny, stack) { }
	private static readonly int[] _pool = new int[] {
		ItemID.Bunny,
		ItemID.Owl,
		ItemID.Squirrel
	};
	public override int[] Pool => _pool;
}