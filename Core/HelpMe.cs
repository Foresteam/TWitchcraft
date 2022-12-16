using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using static Terraria.ModLoader.ModContent;

#nullable enable
namespace TWitchery;
static partial class HelpMe {
	// f'd up. Fix this
	public static void GetTileTextureOrigin(ref int i, ref int j) {
		i -= Terraria.Main.tile[i, j].TileFrameX / 16;
		j -= Terraria.Main.tile[i, j].TileFrameY / 16;
	}
	public static Point16 GetTileTextureOrigin(Point16 point) {
		int i = point.X, j = point.Y;
		GetTileTextureOrigin(ref i, ref j);
		return new Point16(i, j);
	}
	public static T? GetTileEntity<T>(int i, int j) where T: TileEntity {
		GetTileTextureOrigin(ref i, ref j);

		TileEntity? te;
		if (!TileEntity.ByPosition.TryGetValue(GetTileTextureOrigin(new Point16(i, j)), out te) || te is not T)
			return null;
		return (T)te;
	}
	public static T? GetTileEntity<T>(Point16 p) where T: TileEntity => GetTileEntity<T>(p.X, p.Y);
	public static bool Consume(ref Terraria.Item item, int amount = 1) {
		if (item.stack < amount)
			return false;
		if (item.stack > amount)
			item.stack -= amount;
		else
			item = new Terraria.Item();
		return true;
	}
	public static void GiveItem(Terraria.Item item, Terraria.Player ply, int pid = -1) {
		if (pid == -1)
			pid = Terraria.Main.myPlayer;
		Terraria.Item taken = ply.GetItem(pid, item, Terraria.GetItemSettings.InventoryEntityToPlayerInventorySettings);
		if (!taken.IsAir)
			ply.QuickSpawnItem(null, taken);
	}
	public static void Swap<T>(ref T a, ref T b) {
		var t = a;
		a = b;
		b = t;
	}

	/// <summary>Blends the specified colors together.</summary>
	/// <param name="color">First color</param>
	/// <param name="color2">Second color</param>
	/// <param name="k1">Amount of <paramref name="color"/>, supposed to be less or equal than 1</param>
	/// <param name="k2">Amount of <paramref name="color2"/>, supposed to be less or equal than 1</param>
	/// <returns>The blended colors.</returns>
	public static Color Blend(Color color, Color color2, float k1 = .5f, float k2 = 0) {
		if (k2 == 0)
			k2 = 1 - k1;
		byte r = (byte)(color.R * k1 + color2.R * k2);
		byte g = (byte)(color.G * k1 + color2.G * k2);
		byte b = (byte)(color.B * k1 + color2.B * k2);
		return new Color(r, g, b);
	}

	public static string DumpItem(Item item, bool dev = false) {
		if (!dev)
			return $"{item.Name} x{item.stack}";
		else
			return '{' + $"\"id\": {item.type}, \"stack\": {item.stack}" + '}';
	}
	public static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace) =>
			assembly.GetTypes()
			.Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
			.ToArray();
	public static Dictionary<int, Type> GetLiquidsTable() {
		Dictionary<int, Type> result = new();
		int i = 0;
		foreach (var liquidType in HelpMe.GetTypesInNamespace(System.Reflection.Assembly.GetExecutingAssembly(), "TWitchery.Liquids")) {
			result[i] = liquidType;
			i++;
		}
		return result;
	}
	public static Dictionary<int, string> GetItemsTable() {
		Dictionary<int, string> result = new();
		int i = 0;
		foreach (var itemType in typeof(Terraria.ID.ItemID).GetFields()) {
			if (itemType.FieldType != typeof(System.Int16))
				continue;
			var itemID = (short)(itemType.GetValue(null) ?? -i - 1);
			result[itemID] = itemType.Name;
			i++;
		}
		return result;
	}

	public const float reduceHair = .85f; // -15%
	// Approximately :)
	public static bool IsOrange(Color color) => color.R > 150 && color.R - color.G > 100 && color.R - color.B > 150;
	public static void ReduceEnergyCost(ref Recipes.WitcheryRecipe.Result? result, Player ply, Item[]? inv, int wandSlot) {
		if (result == null)
			return;
		if (IsOrange(ply.GetHairColor(false)))
			result.energyCost *= reduceHair;
		var wand = inv?[wandSlot]?.ModItem;
		if (wand is Items.IMagicWand)
			result.energyCost *= ((Items.IMagicWand)wand).ReduceEnergyCost;
	} 
}