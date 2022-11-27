using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace TWitchery.Items.Placeables {
	public class Altar : ModItem {
		public const string DisplayNameText = "Witchery Altar";
		public const string TooltipText = "Used for rituals";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault(DisplayNameText);
			Tooltip.SetDefault(TooltipText);
		}

		public override void SetDefaults() {
			Item.width = 48;
			Item.height = 44;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 150;
			Item.createTile = ModContent.TileType<Tiles.Altar>();
		}
		public override void AddRecipes() {
			CreateRecipe()
			.AddIngredient(ItemID.DirtBlock, 1)
			.Register();
		}
	}
}