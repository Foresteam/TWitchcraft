using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ModLoader.IO;
using ReLogic.Content;
using System.IO;

namespace TWitchery.Items;

using System.Collections.Generic;
using Liquids;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#nullable enable
class UniversalBottle : ModItem {
	public static readonly Dictionary<System.Type, string> descriptions = new() {
		{ new Glass().GetType(), "How did you even come to this?" },
		{ new Blood().GetType(), "Was that truly necessary?" },
		{ new Water().GetType(), "You shouldn't have this" },
		{ new Lava().GetType(), "Hold on!" },
		{ new EvilExtract().GetType(), "Mmm, nasty and stinking" },
		{ new AquaticEssense().GetType(), "\"I remember years ago...\"" },
		{ new LifeEssense().GetType(), "Don't you dare drink" },
		{ new AlienEssense().GetType(), "Did it just move?" },
		{ new EssenseOfChaos().GetType(), "Don't mess with it" },
		{ new EssenseOfForce().GetType(), "The force awakens" },
		{ new EssenseOfAwareness().GetType(), "I know you know" },
		{ new EssenseOfAvarice().GetType(), "\"I spent gold on this!\"" },
		// left for the knife
		// { new Blood().GetType(), "There will be blood" }
	};
	public Liquid? storedLiquid = null;
	private static Asset<Texture2D>? _liquidTexture;
	public override void SetStaticDefaults() {
		DisplayName.SetDefault("Empty bottle");
		Tooltip.SetDefault("Can hold any type of liquid");

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
	public override void ModifyTooltips(List<TooltipLine> tooltips) {
		if (storedLiquid == null || !descriptions.ContainsKey(storedLiquid.GetType()))
			return;
		tooltips.Add(new TooltipLine(Mod, $"BottleOf{storedLiquid.Name}", descriptions[storedLiquid.GetType()]));
	}
	public override void UpdateInventory(Player player) {
		if (storedLiquid == null)
			Item.ClearNameOverride();
		else
			Item.SetNameOverride($"Bottle of {storedLiquid.Name}");
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

	// can't figure out a way to make it work in world, though
	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) {
		if (_liquidTexture == null || storedLiquid == null)
			return;
		spriteBatch.Draw(_liquidTexture.Value, position, frame, storedLiquid.Color, 0, origin, scale, SpriteEffects.None, 0);
	}

	public static Item CreateFilled(Liquid with) {
		Item self = new Item(ModContent.ItemType<UniversalBottle>());
		var bottle = self.ModItem as UniversalBottle;
		if (bottle == null)
			throw new System.Exception("The bottle is no bottle");
		bottle.storedLiquid = with.Clone();
		return self;
	}
}