using Terraria.ID;

namespace TWitchery.Recipes.RecipeItems;
class AnyEvilBlock : RecipeItemPool {
	public AnyEvilBlock(int stack = 1) : base(ItemID.EbonstoneBlock, stack) { }
	private readonly int[] _pool = new int[] {
		ItemID.EbonstoneBlock,
		ItemID.Ebonwood,
		ItemID.CorruptSandstone,
		ItemID.EbonsandBlock,
		ItemID.CrimstoneBlock,
		ItemID.Shadewood,
		ItemID.CrimsonSandstone,
		ItemID.CrimsandBlock
	};
	public override int[] Pool => _pool;
}