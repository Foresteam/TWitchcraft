using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using System.IO;
using System;
using Microsoft.Xna.Framework;

namespace TWitchery;    
class Enchantment : GlobalItem {
	private float _power = 1f, _knockback = 1f, _crit = 1f; 
	
	public void Apply(float power= -1f, float knockback = -1f, float crit = -1f) {
		if (power != -1f)	_power = power;				
		if (knockback != -1f)  _knockback = knockback; 
		if (crit != -1f) _crit = crit;
	}
	public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage) {
		damage *= _power;
	}

	public override void ModifyWeaponKnockback(Item item, Player player, ref StatModifier knockback) {
		knockback *= _knockback;
	}

	public override void ModifyWeaponCrit(Item item, Player player, ref float crit) {
		crit *= _crit;
	}

	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
		if (_power == 1f && _knockback == 1f && _crit == 1f)
			return;

		tooltips.Add(new TooltipLine(Mod, $"TitleEnchantment{_power}", "Applied enchantments: ") { OverrideColor = Color.PowderBlue });
		if (_power != 1f) 
			tooltips.Add(new TooltipLine(Mod, $"DamageEnchantment{_power}", StatString(_power) + " Damage") { OverrideColor = ColorStat(_power) });
		if (_knockback != 1f)
			tooltips.Add(new TooltipLine(Mod, $"KnockbackEnchantment{_knockback}", StatString(_knockback) + " Knockback") { OverrideColor = ColorStat(_knockback) });
		if (_crit != 1f)
			tooltips.Add(new TooltipLine(Mod, $"CritEnchantment{_crit}", StatString(_crit) + " Crit Chance") { OverrideColor = ColorStat(_crit) });
	}

	private string StatString(float stat) {
		return (stat >= 0 ? "+" : "") + (int)(stat) * 100 + "% ";
	}

	private Color ColorStat(float stat) {
		return stat < 1 ? Color.IndianRed : Color.LimeGreen;	
	}

	public override void SaveData(Item item, TagCompound tag) {
		tag.Add("_power", _power.ToString());
		tag.Add("_knockback", _knockback.ToString());
		tag.Add("_crit", _crit.ToString());
	}

	public override void LoadData(Item item, TagCompound tag) {
		Single.TryParse(tag.GetString("_power")?? _power.ToString(),out _power);
		Single.TryParse(tag.GetString("_knockback") ?? _knockback.ToString(), out _knockback);
		Single.TryParse(tag.GetString("_crit") ?? _crit.ToString(), out _crit);		
	}

	public override void NetSend(Item item, BinaryWriter writer) {
		writer.Write(_power);
		writer.Write(_knockback);
		writer.Write(_crit);
	}

	public override void NetReceive(Item item, BinaryReader reader) {
		_power = reader.ReadSingle();
		_knockback = reader.ReadSingle();
		_crit = reader.ReadSingle();
	}

	public override bool InstancePerEntity => true;
}

