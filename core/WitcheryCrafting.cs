using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System;

namespace Test {
	class WitcheryCrafting {
		public enum Action {
			Nothing = 0,
			Take,
			Put,
			Craft
		}
		public Item[] slots;
		#nullable enable
		public Item? catalyst;
		private List<WitcheryRecipe> _recipes;
		public WitcheryCrafting(int size, bool catalyst) {
			slots = Enumerable.Repeat(new Item(), size).ToArray();
			this.catalyst = catalyst ? new Item() : null;
			_recipes = new List<WitcheryRecipe>();
		}

		public int SlotsUsed {
			get => slots.Sum(i => i.type == 0 ? 0 : 1) + ((catalyst != null && catalyst.type != 0) ? 1 : 0);
		}
		public bool Put(Item item) {
			for (int i = 0; i < slots.Length; i++)
				if (slots[i].type == 0) {
					slots[i] = item;
					return true;
				}
			if (catalyst != null && catalyst.type == 0)
				catalyst = item;
			return false;
		}
		public bool Put(Item[] inv, int slot) {
			if (!Put(inv[slot]))
				return false;
			Main.NewText("Two dicks in my ass is not enough...");
			Main.NewText(inv[slot].type);
			inv[slot] = new Item();
			return true;
		}

		#nullable enable
		public Item? Take(bool peek = false) {
			if (catalyst != null && catalyst.type != 0) {
				var t = catalyst;
				if (!peek)
					catalyst = new Item();
				return t;
			}
			for (int i = slots.Length - 1; i >= 0; i--)
				if (slots[i].type != 0) {
					var t = slots[i];
					if (!peek)
						slots[i] = new Item();
					return t;
				}
			return null;
		}
		public Item? Take(int i, int j, Player ply) {
			Item? heldItem = Take();
			var names = slots.Select(i => $"{i.Name}x{i.stack}").ToArray();
			Main.NewText($"Contained items: {String.Join(", ", names)}");
			ply.DropItem(null, new Vector2(i * 16, j * 16), ref heldItem);
			return heldItem;
		}

		public Action Interract(int i, int j, Player ply, Item[] inv, int slot) {
			// take item
			if (SlotsUsed > 0 && inv[slot].type == 0)
				return Action.Take;
			if (inv[slot].type == 0)
				return Action.Nothing;
			// put item
			return Action.Put;
		}
	}
}