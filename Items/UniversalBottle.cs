using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ModLoader.IO;
using ReLogic.Content;
using System.IO;

namespace TWitchery.Items;
using Liquids;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#nullable enable
class UniversalBottle : ModItem {
	public Liquid? storedLiquid = null;
	private static Asset<Texture2D>? _liquidTexture;
	public override void SetStaticDefaults() {
		DisplayName.SetDefault("Empty bottle");
		Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

		CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
		_liquidTexture = ModContent.Request<Texture2D>("TWitchery/Assets/UniversalBottleLiquid");
	}

	public override void SetDefaults() {
		Item.maxStack = 999;
		Item.consumable = false;
		Item.width = 20;
		Item.height = 26;
		Item.rare = ItemRarityID.White;
	}

	public override bool CanStack(Item item2) {
		var otherItem = item2.ModItem as UniversalBottle;

		return storedLiquid?.GetType() == otherItem?.storedLiquid?.GetType();
	}

	public override void SaveData(TagCompound tag) {
		tag.Add("storedLiquid", storedLiquid?.Serialize());
	}

	public override void LoadData(TagCompound tag) {
		storedLiquid = Liquid.Deserialize(tag.GetString("storedLiquid"));
	}

	public override void NetSend(BinaryWriter writer) {
		writer.Write(storedLiquid?.Serialize() ?? "");
	}

	public override void NetReceive(BinaryReader reader) {
		storedLiquid = Liquid.Deserialize(reader.ReadString());
	}

	public static Item CreateFilled(Liquid with) {
		Item self = new Item(ModContent.ItemType<UniversalBottle>());
		var bottle = self.ModItem as UniversalBottle;
		if (bottle == null)
			throw new System.Exception("Somehow, the bottle is no bottle");
		bottle.storedLiquid = with.Clone();
		return self;
	}

	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) {
		if (_liquidTexture == null || storedLiquid == null)
			return;
		spriteBatch.Draw(_liquidTexture.Value, position, frame, storedLiquid.Color, 0, origin, scale, SpriteEffects.None, 0);
	}
}