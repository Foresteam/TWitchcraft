using Terraria.ID;

namespace TWitchery.Recipes.RecipeItems;
class AnyMediumAnimal : RecipeItemPool {
	public AnyMediumAnimal(int stack = 1) : base(ItemID.Bunny, stack) {
		Pool = new int[] {
			ItemID.Bunny,
			ItemID.Owl,
			ItemID.Squirrel
		};
	}
}