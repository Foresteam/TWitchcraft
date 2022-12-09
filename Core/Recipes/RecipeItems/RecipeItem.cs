using Terraria;

namespace TWitchery.Recipes.RecipeItems;
class RecipeItem {
	private int? _stack, _id;
	public int Stack => (int)_stack;
	public int Type => (int)_id;

	public RecipeItem(int id, int stack) {
		_id = id;
		_stack = stack;
	}
	public RecipeItem(Item item) : this(item.type, item.stack) {}

	public virtual bool Match(Item other, ref int? xAmount) => other.type == _id && (xAmount == null || other.stack / _stack == xAmount);
}