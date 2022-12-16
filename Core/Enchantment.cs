using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace TWitchery;
internal class Enchantment : ModPrefix {
	public static float power = 0.1f, knockback = 0.1f, scale = 0.1f, useTime = 0.1f, shootSpeed = 0.1f, mana = 0.1f;
	//public static float Power = 0f, KnockBack = 0f, Scale = 0f, UseTime = 0f, ShootSpeed = 0f, Mana = 0f;
	public static int critBonus = 10;

	// Change your category this way, defaults to PrefixCategory.Custom. Affects which items can get this prefix.
	public override PrefixCategory Category => PrefixCategory.AnyWeapon;

	//public Enchant(float damage, float knockback, float useTime, float scale, float shootSpeed, float mana, int critBonus) {
	//	this.Power = damage;
	//	this.KnockBack = knockback;
	//	this.UseTime = useTime;
	//	this.Scale = scale;
	//	this.ShootSpeed = shootSpeed;
	//	this.Mana = mana;
	//	this.CritBonus = critBonus;
	//	SetStats(ref Power, ref KnockBack, ref UseTime, ref Scale, ref ShootSpeed, ref Mana, ref CritBonus);
	//}

	public static void SetBuffs(float damage = 0, float knockback = 0, float useTime = 0, float scale = 0, float shootSpeed = 0, float mana = 0, int critBonus = 0) {
		power = damage;
		Enchantment.knockback = knockback;
		Enchantment.useTime = useTime;
		Enchantment.scale = scale;
		Enchantment.shootSpeed = shootSpeed;
		Enchantment.mana = mana;
		Enchantment.critBonus = critBonus;
	}

	public static List<double> GetBuffs() {
		return new List<double>() { power, knockback, useTime, scale, shootSpeed, mana, critBonus };
	}

	// Determines if it can roll at all.
	// Use this to control if a prefix can be rolled or not.
	public override bool CanRoll(Item item) {
		return false;
	}
	public static void ClearEnchantments(ref Item item) {
		UnapplyPrefix(ref item);
	}

	// Use this function to modify these stats for items which have this prefix:
	// Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
	public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus) {
		damageMult *= 1f + power;
		knockbackMult *= 1f + knockback;
		useTimeMult *= 1f + useTime;
		scaleMult *= 1f + scale;
		shootSpeedMult *= 1f + shootSpeed;
		critBonus += Enchantment.critBonus;
		power = 0f; knockback = 0f; scale = 0f; useTime = 0f; shootSpeed = 0f; mana = 0f;
		Enchantment.critBonus = 0;
	}
	public static void ApplyEnchantment<Prefix>(ref Item item) where Prefix : ModPrefix {
		bool favorited = item.favorited;
		item.netDefaults(item.netID);
		item.Prefix(ModContent.PrefixType<Prefix>());
		item.position = Main.LocalPlayer.Center;
		item.favorited = favorited;
		PopupText.NewText(PopupTextContext.ItemReforge, item, item.stack, true);
	}

	public static void UnapplyPrefix(ref Item item) {
		bool favorited = item.favorited;
		item.netDefaults(item.netID);
		item.position = Main.LocalPlayer.Center;
		item.favorited = favorited;
		PopupText.NewText(PopupTextContext.ItemReforge, item, item.stack, true);
	}
}

