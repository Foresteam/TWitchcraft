using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace TWitchery;
using Liquids;
partial class WitcheryRecipe {
	#nullable enable
	public class Result {
		public List<Item> items;
		public List<Liquid> liquids;
		public float energyCost;
		public Result(float energyCost) {
			items = new List<Item>();
			liquids = new List<Liquid>();
			this.energyCost = energyCost;
		}
		public Result Clone() {
			var copy = new Result(energyCost);
			foreach (var ir in items)
				copy.items.Add(new Item(ir.type, ir.stack));
			foreach (var lr in liquids)
				copy.liquids.Add(lr.Clone());
			return copy;
		}
	}
}