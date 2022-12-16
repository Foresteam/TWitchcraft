using Terraria.ID;

namespace TWitchery.Recipes.RecipeItems;
class AnyEvilWood : RecipeItemPool {
	public AnyEvilWood(int stack = 1) : base(ItemID.Ebonwood, stack) {
		Pool = new int[] {
			ItemID.Ebonwood,
			ItemID.Shadewood
		};
	}
}