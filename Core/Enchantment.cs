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
	private float _power = 1f; 
	
	public void Apply(float power) {
		_power = power;		
	}
	public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage) {
		damage *= _power;
	}

	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
		if (_power == 1f)
			return;
		tooltips.Add(new TooltipLine(Mod, $"DamageEnchantment{_power}", "Enchantment Damage: "+ _power) { OverrideColor = Color.Red });
	}

	public override void SaveData(Item item, TagCompound tag) {
		tag.Add("_power", _power.ToString());
	}

	public override void LoadData(Item item, TagCompound tag) {
		_power = float.Parse(tag.GetString("_power"));
	}

	public override void NetSend(Item item, BinaryWriter writer) {
		writer.Write(_power);
	}

	public override void NetReceive(Item item, BinaryReader reader) {
		_power = reader.ReadSingle();
	}

	public override bool InstancePerEntity => true;
}

