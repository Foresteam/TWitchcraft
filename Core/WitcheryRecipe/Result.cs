using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace TWitchery;
using Liquids;
partial class WitcheryRecipe {
	#nullable enable
	public class Result {
		public struct ItemResult {
			public Item self;
			public Vector2 pos;
			public ItemResult(Item self, Vector2 pos = new Vector2()) {
				this.self = self;
				this.pos = pos;
			}
		}
		public struct LiquidResult {
			public Liquid self;
			public LiquidResult(Liquid self) {
				this.self = self;
			}
		}
		public List<ItemResult> items;
		public List<LiquidResult> liquids;
		public float energyCost;
		public Result(float energyCost) {
			items = new List<ItemResult>();
			liquids = new List<LiquidResult>();
			this.energyCost = energyCost;
		}
		public Result Clone() {
			var copy = new Result(energyCost);
			foreach (var ir in items)
				copy.items.Add(new ItemResult(new Item(ir.self.type, ir.self.stack)));
			foreach (var lr in liquids)
				copy.liquids.Add(new LiquidResult(lr.self.Clone()));
			return copy;
		}
	}
}