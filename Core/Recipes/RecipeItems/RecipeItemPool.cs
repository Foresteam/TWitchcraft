using System.Linq;
using Terraria;

namespace TWitchery.Recipes.RecipeItems;
class RecipeItemPool : RecipeItem {
	public RecipeItemPool(int baseItemID, int stack = 1) : base(baseItemID, stack) { }
	public virtual int[] Pool => new int[0];
	public override bool Match(Item other, ref int? xAmount) => Pool.Contains(other.type) && (xAmount == null || other.stack / Stack == xAmount);
}