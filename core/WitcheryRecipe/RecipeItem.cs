using Terraria;

namespace TWitchery;
class RecipeItem : GenericRecipeItem {
	private int? _stack, _id;
	public int Stack => (int)_stack;
	public int Type => (int)_id;

	public RecipeItem(int id, int stack, MatchFunction match = null) : base(match) {
		_id = id;
		_stack = stack;
	}
	public RecipeItem(Item item, MatchFunction match = null) : this(item.type, item.stack, match) {}

	public override bool MatchDefault(Item another, ref int? xAmount) => another.type == _id && (xAmount == null || another.stack / _stack == xAmount);
}