using System.Collections.Generic;

namespace TWitchery.Tables;
using Liquids;
class Common {
	public static readonly Dictionary<System.Type, float> energyLiquids = new() {
		{ new LesserManaPotion(1).GetType(), 50f * 4 },
		{ new ManaPotion(1).GetType(), 100f * 4 },
	};
}