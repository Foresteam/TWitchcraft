using Terraria;

namespace TWitchery.Recipes.RecipeItems;
class RecipeItem {
	private int? _stack, _id;
	public virtual int Stack => (int)_stack;
	public virtual int Type => (int)_id;
	public virtual Item Item => new Item(Type, Stack);

	public RecipeItem(int id, int stack) {
		_id = id;
		_stack = stack;
	}
	public RecipeItem(Item item) : this(item.type, item.stack) {}

	public virtual bool Match(Item other, ref int? xAmount) => other.type == Type && (xAmount == null || other.stack / Stack == xAmount);
}