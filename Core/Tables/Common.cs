using System.Collections.Generic;
using Terraria.ID;

namespace TWitchery.Tables;
using Liquids;
class Common {
	public static readonly Dictionary<System.Type, float> energyLiquids = new() {
		{ new LesserManaPotion(1).GetType(), 50f * 4 },
		{ new ManaPotion(1).GetType(), 100f * 4 },
	};
	public static readonly List<int> manaPotions = new() {
		ItemID.ManaPotion,
		ItemID.LesserManaPotion,
		ItemID.GreaterManaPotion,
		ItemID.SuperManaPotion
	};
}