using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using Terraria;
using Terraria.ID;

namespace TWitchery.Recipes;
using RecipeItems;
using Liquids;
using Terraria.ModLoader;

///<summary>Inherit this and redefine GetResult to customize results based on input ingredients</summary>
#nullable enable
partial class WitcheryRecipe {
	protected float _failedWorkedChance, _matchThreshold;
	protected List<RecipeItem> _itemIngredients;
	protected List<Liquid> _liquidIngredients;
	protected RecipeItem? _catalyst;
	//protected RecipeItem? _catalyst;
	protected Result _result;
	/// <param name="resultGetter">An alternative (and advanced) way to define recipe results.</param>
	public WitcheryRecipe(float energyCost, float failedWorkedChance = 0, float matchThreshold = .75f) {
		_failedWorkedChance = failedWorkedChance;
		_matchThreshold = matchThreshold;
		_catalyst = null;

		_itemIngredients = new List<RecipeItem>();
		_liquidIngredients = new List<Liquid>();
		_result = new Result(energyCost);
	}

	public virtual WitcheryRecipe AddIngredient(Item ingredient) {
		_itemIngredients.Add(new RecipeItem(ingredient));
		return this;
	}
	public virtual WitcheryRecipe AddIngredient(RecipeItem ingredient) {
		_itemIngredients.Add(ingredient);
		return this;
	}
	public virtual WitcheryRecipe AddIngredient(Liquid ingredient) {
		_liquidIngredients.Add(ingredient);
		return this;
	}
	public virtual WitcheryRecipe SetCatalyst(RecipeItem catalyst) {
		_catalyst = catalyst;
		return this;
	}
	public virtual WitcheryRecipe SetCatalyst(Item catalyst) {
		_catalyst = new RecipeItem(catalyst);
		return this;
	}
	public virtual WitcheryRecipe AddResult(Item result) {
		_result.items.Add(result);
		return this;
	}
	public virtual WitcheryRecipe AddResult(Liquid result) {
		_result.liquids.Add(result);
		return this;
	}
	
	private List<Liquid?> FilterEnergyOut(List<Liquid?> input) {
		return input.Select(liquid => !Tables.Common.energyLiquids.ContainsKey(liquid?.GetType()) ? liquid : null).ToList();
	}
	private float Match(Item[] _items, Item catalyst, List<Liquid> inputLiquids, out int? xAmount, bool fuck) {
		var items = new List<Item>(_items).FindAll(i => !i.IsAir);
		List<Liquid?> liquids = new(inputLiquids);
		if (_result.energyCost > 0)
			liquids = FilterEnergyOut(liquids);
		int 
			totalLiquids = Math.Max(liquids.Select(l => l == null ? 0 : 1).Sum(), _liquidIngredients.Count),
			totalItems = Math.Max(_itemIngredients.Count, _items.Select(i => i.type == 0 ? 0 : 1).Sum()),
			totalCatalyst = Math.Max(catalyst.type != 0 ? 1 : 0, (_catalyst?.Type ?? 0) != 0 ? 1 : 0);
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
		foreach (Liquid? liquid in liquids) {
			if (liquid == null)
				continue;
			var _xAmount = xAmount;
			Liquid? rliquid = null;
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
		if (fuck)
			Main.NewText($"{total} {totalItems} {totalCatalyst} {totalLiquids} {match}");
		if (totalCatalyst > 0 && _catalyst != null)
			match += _catalyst.Match(catalyst, ref xAmount) ? 1 : 0;
		if (fuck)
			Main.NewText($"{total} {totalItems} {totalCatalyst} {totalLiquids} {match}");
		// Main.NewText($"totalItems: {totalItems}, totalLiquids: {totalLiquids}, totalCatalyst: {totalCatalyst}, total: {total}, match: {match}, items: {items.Count}, ctype: {catalyst.type} == {_catalyst.type}, {catalyst.stack}, {xAmount}");
		return (float)match / total;
	}
	private float Match(Item[] _items, Item catalyst, List<Liquid> liquids) {
		int? xAmount;
		return Match(_items, catalyst, liquids, out xAmount, false);
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
	public virtual void GetResult(RecipeItem[] ritems, int? xAmount, Item[] items, Item catalyst, List<Liquid> liquids, ref Result result) {
		if (xAmount == null)
			return;
		foreach (var item in result.items)
			item.stack *= (int)xAmount;
		foreach (var liquid in result.liquids)
			liquid.Volume *= (int)xAmount;
		Main.NewText($"GetResult: {xAmount}");
		result.energyCost *= (int)xAmount;
	}
	/// Attempt to combine ingredients into the recipe
#nullable enable
	public Result? Craft(Item[] items, Item catalyst, List<Liquid>? liquids = null) {
		if (liquids == null)
			liquids = new List<Liquid>();
		int? xAmount;
		float match = Match(items, catalyst, liquids, out xAmount, true);
		// Main.NewText(match, Color.Cyan);
		if (match < _matchThreshold || match < 1 && Main.rand.NextFloat() > match * _failedWorkedChance)
			return null;
		
		Result result = _result.Clone();
		GetResult(_itemIngredients.ToArray(), xAmount, items, catalyst, liquids, ref result);
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
		rs.Add('[' + String.Join(", ", _liquidIngredients.Select(lq => lq.Dump(dev))) + ']');
		rs.Add((_catalyst?.Type ?? 0) != 0 ? HelpMe.DumpItem(_catalyst?.Item ?? new Item(), dev) : "");
		rs.Add(_result.energyCost.ToString());
		rs.Add('[' + String.Join(", ", _result.liquids.Select(lq => lq.Dump(dev))) + ']');
		rs.Add('[' + String.Join(", ", _result.items.Select(item => HelpMe.DumpItem(item, dev))) + ']');
		return String.Join(';', rs);
	}
}