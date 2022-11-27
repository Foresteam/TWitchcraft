using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using Terraria;
using Terraria.ID;

namespace TWitchery;
using Liquids;
partial class WitcheryRecipe {
	private float _failedWorkedChance, _matchThreshold;
	private List<RecipeItem> _itemIngredients;
	private List<Liquid> _liquidIngredients;
	private Item _catalyst;
	private Result _result;
	public delegate void ResultGetter(RecipeItem[] ritems, int? xAmount, Item[] items, Item catalyst, List<Liquid> liquids, ref Result result);
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
	public WitcheryRecipe AddResult(Item result) {
		_result.items.Add(new Result.ItemResult(result));
		return this;
	}
	public WitcheryRecipe AddResult(Result.LiquidResult result) {
		_result.liquids.Add(result);
		return this;
	}
	public WitcheryRecipe AddResult(Liquid result) {
		_result.liquids.Add(new Result.LiquidResult(result));
		return this;
	}

	private List<Liquid> FilterEnergyOut(List<Liquid> input) {
		return input.Select(liquid => !Tables.Common.energyLiquids.ContainsKey(liquid.GetType()) ? liquid : null).ToList();
	}
	private float Match(Item[] _items, Item catalyst, List<Liquid> inputLiquids, out int? xAmount) {
		var items = new List<Item>(_items).FindAll(i => !i.IsAir);
		var liquids = inputLiquids;
		if (_result.energyCost > 0)
			liquids = FilterEnergyOut(liquids);
		int 
			totalLiquids = Math.Max(liquids.Select(l => l == null ? 0 : 1).Sum(), _liquidIngredients.Count),
			totalItems = Math.Max(_itemIngredients.Count, _items.Select(i => i.type == 0 ? 0 : 1).Sum()),
			totalCatalyst = Math.Max(catalyst.type != 0 ? 1 : 0, _catalyst.type != 0 ? 1 : 0);
		int total = totalLiquids + totalItems + totalCatalyst;
		int match = 0;
		// "multiply" the result
		xAmount = null;
		foreach (Item item in items)
			foreach (RecipeItem ing in _itemIngredients)
				if (ing.Match(item, ref xAmount)) {
					if (xAmount == null)
						xAmount = item.stack / ing.Stack;
					match++;
				}
		foreach (Liquid liquid in liquids) {
			if (liquid == null)
				continue;
			var _xAmount = xAmount;
			Liquid rliquid = null;
			foreach (var tliquid in _liquidIngredients)
				if (tliquid.GetType() == liquid.GetType() && (_xAmount == null || liquid.Volume / _xAmount == tliquid.Volume)) {
					rliquid = tliquid;
					break;
				}
			if (rliquid == null)
				continue;
			if (_xAmount == null)
				xAmount = (int)(liquid.Volume / rliquid.Volume);
			match += 1;
		}
		if (totalCatalyst > 0)
			match += (_catalyst.type == catalyst.type && catalyst.stack % xAmount == 0) ? 1 : 0;
		// Main.NewText($"totalItems: {totalItems}, totalLiquids: {totalLiquids}, totalCatalyst: {totalCatalyst}, total: {total}, match: {match}, items: {items.Count}, ctype: {catalyst.type} == {_catalyst.type}, {catalyst.stack}, {xAmount}");
		return (float)match / total;
	}
	private float Match(Item[] _items, Item catalyst, List<Liquid> liquids) {
		int? xAmount;
		return Match(_items, catalyst, liquids, out xAmount);
	}
#nullable enable
	public static WitcheryRecipe BestMatch(List<WitcheryRecipe> recipes, Item[] items, Item catalyst, List<Liquid>? liquids = null) {
		if (liquids == null)
			liquids = new List<Liquid>();
		recipes.Sort((a, b) => {
			float am = a.Match(items, catalyst, liquids), bm = b.Match(items, catalyst, liquids);
			if (am > bm)
				return 1;
			if (am < bm)
				return -1;
			return 0;
		});
		if (recipes.Count == 0)
			throw new ArgumentException("The recipe list is empty!");
		return recipes.Last();
	}
	public static void GetDefaultResult(RecipeItem[] ritems, int? xAmount, Item[] items, Item catalyst, List<Liquid> liquids, ref Result result) {
		if (xAmount == null)
			return;
		foreach (var item in result.items)
			item.self.stack *= (int)xAmount;
		foreach (var liquid in result.liquids)
			liquid.self.Volume *= (int)xAmount;
		result.energyCost *= (int)xAmount;
	}
	/// Attempt to combine ingredients into the recipe
#nullable enable
	public Result? Craft(Item[] items, Item catalyst, List<Liquid>? liquids = null) {
		if (liquids == null)
			liquids = new List<Liquid>();
		int? xAmount;
		float match = Match(items, catalyst, liquids, out xAmount);
		// Main.NewText(match, Color.Cyan);
		if (match < _matchThreshold || match < 1 && Main.rand.NextFloat() < match * _failedWorkedChance)
			return null;
		
		Result result = _result.Clone();
		GetDefaultResult(_itemIngredients.ToArray(), xAmount, items, catalyst, liquids, ref result);
		// the same for liquids
		if (match < 1)
			result.energyCost /= _failedWorkedChance / match;
		return result;
	}

	public static string DumpHeader => "Solid ingredients;Liquid ingredients;Catalyst;Energy cost;Out liquids;Out items";
	public string Dump(bool dev = false, int ningredients = 0) {
		List<string> rs = new();
		var ingredients = _itemIngredients.Select(ri => HelpMe.DumpItem(new Item(ri.Type, ri.Stack), dev)).ToList();
		// if (dev)
		// 	while (ingredients.Count < ningredients)
		// 		ingredients.Add("");
		var singredients = String.Join(",", ingredients);
		if (dev)
			singredients = $"[{singredients}]";
		rs.Add(singredients);
		rs.Add(String.Join(", ", _liquidIngredients.Select(lq => lq.Dump(dev))));
		rs.Add(_catalyst.type != 0 ? HelpMe.DumpItem(_catalyst) : "");
		rs.Add(_result.energyCost.ToString());
		rs.Add(String.Join(", ", _result.liquids.Select(lq => lq.self.Dump(dev))));
		rs.Add(String.Join(", ", _result.items.Select(item => HelpMe.DumpItem(item.self, dev))));
		return String.Join(';', rs);
	}
}