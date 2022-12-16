using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace TWitchery.Recipes;
/// <summary>No actual difference. Just fits best</summary>
class AltarEnchantment : WitcheryRecipe {
	public AltarEnchantment(float energyCost, float failedWorkedChance = 0, float matchThreshold = .75f) : base(energyCost, failedWorkedChance, matchThreshold) { }
	public AltarEnchantment SetTarget(Item target) => (AltarEnchantment)base.SetCatalyst(target);
	public override AltarEnchantment AddIngredient(Item ingredient) => (AltarEnchantment)base.AddIngredient(ingredient);
	public AltarEnchantment Enchant(float power = 1f) {
		_result.items.Last().GetGlobalItem<Enchantment>().Apply(power);
		return this;
	}
	public AltarEnchantment AddResult() => (AltarEnchantment)AddResult(new Item(_catalyst.Type, _catalyst.Stack));
}