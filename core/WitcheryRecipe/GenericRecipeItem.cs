using Terraria;

namespace TWitchery;
class GenericRecipeItem {
	public delegate bool MatchFunction(Item match, ref int? xAmount);
	public readonly MatchFunction Match;
	public GenericRecipeItem(MatchFunction match = null) {
		Match = match != null ? match : MatchDefault;
	}
	public virtual bool MatchDefault(Item match, ref int? xAmount) => false;
}