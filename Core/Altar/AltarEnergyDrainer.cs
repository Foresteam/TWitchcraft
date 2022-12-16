using System;
using System.Collections.Generic;
using Terraria;
using TWitchery.PedestalCore;


namespace TWitchery.AltarCore;
class AltarEnergyDrainer : EnergyDrainer {
	/// <returns>Amount of energy left to drain. Can go negative (took more than needed)</returns>
	public float Drain(float amount, Player ply, int i, int j, List<Inventory> entrySlots) {
		HelpMe.GetTileTextureOrigin(ref i, ref j);
		_x = i;
		_y = j;

		_yetToDrain = amount;
		Main.NewText(amount);

		foreach (var step in new System.Action[] {
				// () => DrainManaPotions(entrySlots),
				() => DrainPlayer(ply),
				DrainBiome,
				DrainFlora,
				DrainBlocks,
				DrainLivingForms
		})
			if (_yetToDrain > 0)
				step();
			else
				break;

		return _yetToDrain;
	}
}

