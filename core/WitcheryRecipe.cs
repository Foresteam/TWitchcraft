using Terraria;
using Terraria.ObjectData;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TWitchery {
	class WitcheryRecipe {
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
				return (Result)this.MemberwiseClone();
			}
		}

		private float _failedWorkedChance, _matchThreshold;
		private List<Item> _itemIngredients;
		private List<Liquid> _liquidIngredients;
		private Item _catalyst;
		private Result _result;
		public WitcheryRecipe(float energyCost, float failedWorkedChance = 0, float matchThreshold = .75f) {
			_failedWorkedChance = failedWorkedChance;
			_matchThreshold = matchThreshold;
			_catalyst = new Item();

			_itemIngredients = new List<Item>();
			_liquidIngredients = new List<Liquid>();
			_result = new Result(energyCost);
		}

		public WitcheryRecipe AddIngredient(Item ingredient) {
			_itemIngredients.Add(ingredient);
			return this;
		}
		public WitcheryRecipe AddIngredient(Liquid ingredient) {
			_liquidIngredients.Add(ingredient);
			return this;
		}
		public WitcheryRecipe SetCatalyst(Item catalyst) {
			_catalyst = catalyst;
			return this;
		}
		public WitcheryRecipe AddResult(Result.ItemResult result) {
			_result.items.Add(result);
			return this;
		}
		public WitcheryRecipe AddResult(Result.LiquidResult result) {
			_result.liquids.Add(result);
			return this;
		}

		private float Match(Item[] _items, Item catalyst, Liquid[] liquids, out int? xAmount) {
			var items = new List<Item>(_items).FindAll(i => !i.IsAir);
			int total = liquids.Length + _itemIngredients.Count + 1;
			int match = 0;
			foreach (Liquid liquid in liquids)
				match += _liquidIngredients.Find(l => l.x == liquid.x) != null ? 1 : 0;
			// "multiply" the result
			xAmount = null;
			foreach (Item item in items) {
				var found = _itemIngredients.Find(i => i.type == item.type);
				if (found == null)
					continue;
				if (xAmount == null)
					xAmount = item.stack / found.stack;
				match += (xAmount >= 1 && item.stack % xAmount == 0) ? 1 : 0;
			}
			match += (_catalyst.type == catalyst.type && catalyst.stack % xAmount == 0) ? 1 : 0;
			Main.NewText($"total: {total}, items: {items.Count}, ctype: {catalyst.type} == {_catalyst.type}, {catalyst.stack}, {xAmount}");
			return (float)match / total;
		}
		private float Match(Item[] _items, Item catalyst, Liquid[] liquids) {
			int? xAmount;
			return Match(_items, catalyst, liquids, out xAmount);
		}
		public static WitcheryRecipe BestMatch(List<WitcheryRecipe> recipes, Item[] items, Item catalyst, Liquid[] liquids) {
			recipes.Sort((a, b) => {
				float am = a.Match(items, catalyst, liquids), bm = b.Match(items, catalyst, liquids);
				if (am > bm)
					return 1;
				if (am < bm)
					return -1;
				return 0;
			});
			return recipes[0];
		}
		/// Attempt to combine ingredients into the recipe
		public Result? Craft(Item[] items, Item catalyst, Liquid[] liquids) {
			int? xAmount;
			float match = Match(items, catalyst, liquids, out xAmount);
			Main.NewText(match, Color.Cyan);
			if (match < _matchThreshold || match < 1 && Main.rand.NextFloat() < match * _failedWorkedChance)
				return null;
			
			Result result = _result.Clone();
			if (xAmount != null)
				foreach (var item in result.items)
					item.self.stack *= (int)xAmount;
			// the same for liquids
			if (match < 1)
				result.energyCost /= _failedWorkedChance / match;
			return result;
		}
	}
}