using Terraria;
using Terraria.ObjectData;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Test {
	class WitcheryRecipe {
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
			public object Clone() {
				return this.MemberwiseClone();
			}
		}

		private float _energyCost;
		private float _failedWorkedChance;
		private List<Item> _itemIngredients;
		private List<Liquid> _liquidIngredients;
		private Item _catalyst;
		private Result _result;
		public WitcheryRecipe(float energyCost, Item catalyst = null, float failedWorkedChance = 0) {
			_failedWorkedChance = failedWorkedChance;
			_energyCost = energyCost;
			_catalyst = catalyst != null ? catalyst : new Item();

			_itemIngredients = new List<Item>();
			_liquidIngredients = new List<Liquid>();
			_result = new Result(_energyCost);
		}

		public void AddIngredient(Item ingredient) {
			_itemIngredients.Add(ingredient);
		}
		public void AddIngredient(Liquid ingredient) {
			_liquidIngredients.Add(ingredient);
		}
		public void AddResult(Result.ItemResult result) {
			_result.items.Add(result);
		}
		public void AddResult(Result.LiquidResult result) {
			_result.liquids.Add(result);
		}

		private float Match(Liquid[] liquids, Item[] items, Item catalyst) {
			int total = liquids.Length + items.Length + 1;
			int match = 0;
			foreach (Liquid liquid in liquids)
				match += _liquidIngredients.Find(l => l.x == liquid.x) != null ? 1 : 0;
			foreach (Item item in items)
				match += _itemIngredients.Find(i => i.type == item.type) != null ? 1 : 0;
			match += _catalyst.type == catalyst.type ? 1 : 0;
			return (float)match / total;
		}
		public Result? Craft(Item[] items, Item catalyst, Liquid[] liquids) {
			float match = Match(liquids, items, catalyst);
			bool mismatch = match < .75f;
			if (mismatch || match < 1 && Main.rand.NextFloat() < match * _failedWorkedChance)
				return null;
			
			Result result = (Result)_result.Clone();
			if (mismatch)
				result.energyCost /= _failedWorkedChance;
			return result;
		}
	}
}