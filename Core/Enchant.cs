using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using TWitchery.PedestalCore;

namespace TWitchery;
internal class Enchant : ModPrefix {
	public static float Power = 0.1f, KnockBack = 0.1f, Scale = 0.1f,UseTime = 0.1f,ShootSpeed = 0.1f,Mana = 0.1f;
	//public static float Power = 0f, KnockBack = 0f, Scale = 0f, UseTime = 0f, ShootSpeed = 0f, Mana = 0f;
	public static int CritBonus = 10;

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

	public static void setBuffs(float damage, float knockback, float useTime, float scale, float shootSpeed, float mana, int critBonus) {
		Power = damage;
		KnockBack = knockback;
		UseTime = useTime;
		Scale = scale;
		ShootSpeed = shootSpeed;
		Mana = mana;
		CritBonus = critBonus;		
	}

	public static List<double> getBuffs() {
		return new List<double>() { Power, KnockBack, UseTime, Scale, ShootSpeed, Mana, CritBonus};
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
		damageMult *= 1f + Power;
		knockbackMult *= 1f + KnockBack;
		useTimeMult *= 1f + UseTime;
		scaleMult *= 1f + Scale;
		shootSpeedMult *= 1f + ShootSpeed;
		critBonus += CritBonus;
		Power = 0f; KnockBack = 0f; Scale = 0f; UseTime = 0f; ShootSpeed = 0f; Mana = 0f;
		CritBonus = 0;
	}
	public static void ApplyPrefix(ref Item item, int prefix) {
		bool favorited = item.favorited;
		item.netDefaults(item.netID);
		item.Prefix(prefix);
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

