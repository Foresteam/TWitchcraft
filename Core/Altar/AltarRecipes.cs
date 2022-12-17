using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TWitchery.AltarCore;
using Recipes;
using Recipes.RecipeItems;
class AltarRecipes : ModSystem {
public static List<WitcheryRecipe> self = new();
	public override void AddRecipes() {
		self.Clear();
		self.AddRange(new WitcheryRecipe[] {
			new AltarRecipe(energyCost: 100)
				.AddIngredient(new Item(ItemID.DirtBlock, 5))
				.SetCatalyst(new Item(ItemID.Wood))
				.AddResult(new Item(ItemID.WoodenSword)),
			new AltarRecipe(800)
				.AddIngredient(new AnyGoldBar(10))
				.AddIngredient(Items.UniversalBottle.CreateFilled(new Liquids.Blood(), 12))
				.SetTarget(new AnyEvilWood(60))
				.AddResult(new Item(ModContent.ItemType<Items.AdvancedEbonWand>())),
			new AltarEnchantment(0)
				.AddIngredient(new Item(ItemID.DirtBlock, 5))
				.SetTarget(new Item(ItemID.WoodenSword))
				.AddResult()
				.Enchant(new EnchantmentData(damage: 5f)),
			new AltarEnchantment(energyCost: 50)
				.AddIngredient(new Item(ItemID.StoneBlock))
				.SetTarget(new Item(ItemID.WoodenSword))
				.AddResult()
				.Enchant(new EnchantmentData(crit: 1.2f)),
			new AltarEnchantment(0)
				.AddIngredient(new Item(ItemID.DirtBlock))
				.SetTarget(new Item(ItemID.WoodenSword))
				.AddResult()
				.Enchant(new EnchantmentData(knockback: -3f)),
		});
	}
}