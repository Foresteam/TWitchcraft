using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TWitchery.Items;
public class EbonWand : ModItem {
	public override void UpdateInventory(Player player) {
		var hairColor = player.GetHairColor(false);
		base.UpdateInventory(player);
	}
	public override void SetStaticDefaults() {
		DisplayName.SetDefault("Ebonwood Wand"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
		Tooltip.SetDefault(
			"[I]You selected the best parts of ebonwood, the cores, and opposed them to the purification power of silver\n" +
			"to prevent the concentrated evil from tainting your mind.[/I]\n" +
			"A tool to control the destructing force of magic."
		);
	}
	
	public override void SetDefaults() {
		Item.damage = 50;
		Item.DamageType = DamageClass.Magic;
		Item.width = 64;
		Item.height = 64;
		Item.useTime = 20;
		Item.useAnimation = 20;
		Item.useStyle = 1;
		Item.knockBack = 6;
		Item.value = 1000;
		Item.rare = 2;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.shoot = ProjectileID.SlimeGun;
	}

	public override void AddRecipes() {
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.Ebonwood, 60);
		recipe.AddIngredient(ItemID.SilverBar, 10);
		recipe.AddTile(TileID.WorkBenches);
		recipe.AddTile(TileID.Sawmill);
		recipe.Register();
		recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.Shadewood, 60);
		recipe.AddIngredient(ItemID.SilverBar, 10);
		recipe.AddTile(TileID.WorkBenches);
		recipe.AddTile(TileID.Sawmill);
		recipe.Register();
	}
}