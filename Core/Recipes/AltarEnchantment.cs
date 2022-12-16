using Terraria;
using Terraria.ModLoader;

namespace TWitchery.Recipes;
/// <summary>No actual difference. Just fits best</summary>
class AltarEnchantment : WitcheryRecipe {
	public AltarEnchantment(float energyCost, float failedWorkedChance = 0, float matchThreshold = .75f) : base(energyCost, failedWorkedChance, matchThreshold) { }
	public AltarEnchantment SetTarget(Item target) => (AltarEnchantment)base.SetCatalyst(target);
	public override AltarEnchantment AddIngredient(Item ingredient) => (AltarEnchantment)base.AddIngredient(ingredient);
	public AltarEnchantment BuffedResult(float damage = 0, float knockback = 0, float useTime = 0, float scale = 0, float shootSpeed = 0, float mana = 0, int critBonus = 0) {
		Item enchanted = new Item(_catalyst.Type);
		Enchantment.SetBuffs(damage, knockback, useTime, scale, shootSpeed, mana, critBonus);
		Enchantment.ApplyEnchantment<Enchantment>(ref enchanted);
		AddResult(enchanted);
		return this;
	}
}