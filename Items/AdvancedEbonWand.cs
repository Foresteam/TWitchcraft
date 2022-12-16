using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TWitchery.Items;
public class AdvancedEbonWand : ModItem {
	public override void UpdateInventory(Player player) {
		var hairColor = player.GetHairColor(false);
		base.UpdateInventory(player);
	}
	public override void SetStaticDefaults() {
		DisplayName.SetDefault("Advanced Ebonwood Wand"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
		Tooltip.SetDefault(
			"[I]With your newly acquired powers, you managed to create a more polish,\n" +
			"better wand with gold tips using magic infusion.[/I]\n" +
			"A better tool to control the destructing force of magic."
		);
	}

	public override void SetDefaults() {
		Item.damage = 1;
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
	}
}