using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TWitchery;
partial class WitcheryRecipe {
	private float _failedWorkedChance, _matchThreshold;
	private List<RecipeItem> _itemIngredients;
	private List<Liquid> _liquidIngredients;
	private Item _catalyst;
	private Result _result;
	public delegate void ResultGetter(RecipeItem[] ritems, int? xAmount, Item[] items, Item catalyst, Liquid[] liquids, ref Result result);
	public readonly ResultGetter GetResult;
	/// <param name="resultGetter">An alternative (and advanced) way to define recipe results.</param>
	public WitcheryRecipe(float energyCost, float failedWorkedChance = 0, float matchThreshold = .75f, ResultGetter resultGetter = null) {
		_failedWorkedChance = failedWorkedChance;
		_matchThreshold = matchThreshold;
		_catalyst = new Item();

		_itemIngredients = new List<RecipeItem>();
		_liquidIngredients = new List<Liquid>();
		_result = new Result(energyCost);

		GetResult = resultGetter != null ? resultGetter : GetDefaultResult;
	}

	public WitcheryRecipe AddIngredient(Item ingredient) {
		_itemIngredients.Add(new RecipeItem(ingredient));
		return this;
	}
	public WitcheryRecipe AddIngredient(RecipeItem ingredient) {
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
		foreach (Item item in items)
			foreach (RecipeItem ing in _itemIngredients)
				if (ing.Match(item, ref xAmount)) {
					if (xAmount == null)
						xAmount = item.stack / ing.Stack;
					match++;
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
	public static void GetDefaultResult(RecipeItem[] ritems, int? xAmount, Item[] items, Item catalyst, Liquid[] liquids, ref Result result) {
		if (xAmount != null)
			foreach (var item in result.items)
				item.self.stack *= (int)xAmount;
	}
	/// Attempt to combine ingredients into the recipe
	#nullable enable
	public Result? Craft(Item[] items, Item catalyst, Liquid[] liquids) {
		int? xAmount;
		float match = Match(items, catalyst, liquids, out xAmount);
		Main.NewText(match, Color.Cyan);
		if (match < _matchThreshold || match < 1 && Main.rand.NextFloat() < match * _failedWorkedChance)
			return null;
		
		Result result = _result.Clone();
		GetDefaultResult(_itemIngredients.ToArray(), xAmount, items, catalyst, liquids, ref result);
		// the same for liquids
		if (match < 1)
			result.energyCost /= _failedWorkedChance / match;
		return result;
	}
}