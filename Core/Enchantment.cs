using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using System.IO;
using System;
using Microsoft.Xna.Framework;
using System.Text.Json;

namespace TWitchery;
#nullable enable
class EnchantmentData {
	public float Damage { get; set; }
	public float Knockback { get; set; }
	public float Crit { get; set; }
	public EnchantmentData(float damage = -1f, float knockback = -1f, float crit = -1f) {
		this.Damage = damage;
		this.Knockback = knockback;
		this.Crit = crit;
	}
	public static EnchantmentData operator *(EnchantmentData a, EnchantmentData b) {
		EnchantmentData t = (EnchantmentData)a.MemberwiseClone();
		if (b.Damage != -1f) t.Damage = b.Damage;
		if (b.Knockback != -1f) t.Knockback = b.Knockback;
		if (b.Crit != -1f) t.Crit = b.Crit;
		return t;
	}
}
class Enchantment : GlobalItem {
	private EnchantmentData _data = new(1, 1, 1);
	public void Apply(EnchantmentData data) {
		_data *= data;
		Main.NewText($"Apply {_data.Damage} {_data.Knockback} {_data.Crit}");
	}
	public void Apply(Item other) {
		var ench = other.GetGlobalItem<Enchantment>();
		Main.NewText($"ApplyI {ench._data.Damage} {ench._data.Knockback} {ench._data.Crit}");
		Apply(ench._data);
	}
	public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage) {
		damage *= _data.Damage;
	}

	public override void ModifyWeaponKnockback(Item item, Player player, ref StatModifier knockback) {
		knockback *= _data.Knockback;
	}

	public override void ModifyWeaponCrit(Item item, Player player, ref float crit) {
		crit *= _data.Crit;
	}

	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
		if (_data.Damage == 1f && _data.Knockback == 1f && _data.Crit == 1f)
			return;

		tooltips.Add(new TooltipLine(Mod, $"TitleEnchantment{_data.Damage}", "Applied enchantments: ") { OverrideColor = Color.PowderBlue });
		if (_data.Damage != 1f) 
			tooltips.Add(new TooltipLine(Mod, $"DamageEnchantment{_data.Damage}", $"{StatString(_data.Damage)} Damage") { OverrideColor = ColorStat(_data.Damage) });
		if (_data.Knockback != 1f)
			tooltips.Add(new TooltipLine(Mod, $"KnockbackEnchantment{_data.Knockback}", $"{StatString(_data.Knockback)} Knockback") { OverrideColor = ColorStat(_data.Knockback) });
		if (_data.Crit != 1f)
			tooltips.Add(new TooltipLine(Mod, $"CritEnchantment{_data.Crit}", $"{StatString(_data.Crit)} Crit Chance") { OverrideColor = ColorStat(_data.Crit) });
	}

	private string StatString(float stat) {
		return (int)(stat * 100) + "%";
	}

	private Color ColorStat(float stat) {
		return stat < 1 ? Color.IndianRed : Color.LimeGreen;	
	}

	public override void SaveData(Item item, TagCompound tag) {
		tag.Add("TWenchantmentData", JsonSerializer.Serialize(_data));
	}

	public override void LoadData(Item item, TagCompound tag) {
		var s = tag.GetString("TWenchantmentData");
		if (s.Length < 2)
			return;
		var data = JsonSerializer.Deserialize<EnchantmentData?>(s);
		if (data != null)
			_data = (EnchantmentData)data;
	}

	public override void NetSend(Item item, BinaryWriter writer) {
		writer.Write(JsonSerializer.Serialize(_data));
	}

	public override void NetReceive(Item item, BinaryReader reader) {
		var data = JsonSerializer.Deserialize<EnchantmentData?>(reader.ReadString());
		if (data != null)
			_data = (EnchantmentData)data;
	}

	public override bool InstancePerEntity => true;
}

