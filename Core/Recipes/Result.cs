using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace TWitchery.Recipes;
using Liquids;
using Terraria.ModLoader;

partial class WitcheryRecipe {
	#nullable enable
	public class Result {
		public List<Item> items;
		public List<Liquid> liquids;
		public float energyCost;
		public static float Power = 0f, KnockBack = 0f, Scale = 0f, UseTime = 0f, ShootSpeed = 0f, Mana = 0f;
		public static int CritBonus = 0;
		public Result(float energyCost) {
			items = new List<Item>();
			liquids = new List<Liquid>();
			this.energyCost = energyCost;
		}
		public Result Clone() {
			var copy = new Result(energyCost);
			foreach (var ir in items) {
				Item it = new Item(ir.type, ir.stack);
				if (ir.prefix != 0) {
					Enchant.setBuffs(Power, KnockBack, UseTime, Scale, ShootSpeed, Mana, CritBonus);
					Enchant.ApplyPrefix(ref it, ModContent.PrefixType<Enchant>());
				}
				copy.items.Add(it);
			}
			foreach (var lr in liquids)
				copy.liquids.Add(lr.Clone());
			return copy;
		}

        public static void setBuffs(float damage, float knockback, float useTime, float scale, float shootSpeed, float mana, int critBonus) {
			Power = damage;
			KnockBack = knockback;
			UseTime = useTime;
			Scale = scale;
			ShootSpeed = shootSpeed;
			Mana = mana;
			CritBonus = critBonus;
		}
    }
}