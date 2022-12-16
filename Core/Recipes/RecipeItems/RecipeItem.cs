using Terraria;

namespace TWitchery.Recipes.RecipeItems;
#nullable enable
class RecipeItem {
	private int _stack, _id;
	private Item? _item;
	public virtual int Stack => (int)_stack;
	public virtual int Type => (int)_id;
	public virtual Item Item => _item ?? new Item(Type, Stack);

	public RecipeItem(int id, int stack) {
		_id = id;
		_stack = stack;
		_item = null;
	}
	public RecipeItem(Item item) : this(item.type, item.stack) {
		_item = item;
	}

	public virtual bool Match(Item other, ref int? xAmount) => other.type == Type && (xAmount == null || other.stack / Stack == xAmount);
}