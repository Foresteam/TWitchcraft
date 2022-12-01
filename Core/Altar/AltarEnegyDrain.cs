using System;
using System.Collections.Generic;
using Terraria;

namespace TWitchery.Altar;
class AltarEnegyDrainer : EnergyDrainer {
	/// <returns>Amount of energy left to drain. Can go negative (took more than needed)</returns>
	public float Drain(float amount, Player ply, int i, int j, ref List<Item> slots) {
		HelpMe.GetTileTextureOrigin(ref i, ref j);
		_x = i;
		_y = j;

		_yetToDrain = amount;

		// POLNAYA PIZDA!!!
		int stack;
		for (int k = 0; k < slots.Count; k++) {
			stack = slots[k].stack;
			//int y = slots[k].stack;
			if (slots[k].healMana > 0 && slots[k].potion) {
				for (int t = 1; t < stack; t++) {
					_yetToDrain -= slots[k].healMana;
					if (_yetToDrain <= 0) {
						ply.QuickSpawnItem(null, slots[k], stack - t);
						slots[k] = new Item();
						break;
					}
				}
				slots[k] = new Item();
			}
			if (_yetToDrain <= 0) {
				break;
			}
		}

		foreach (
				var step in new Action[]
				{
								() => DrainPlayer(ply),
								DrainBiome,
								DrainFlora,
								DrainBlocks,
								DrainLivingForms
				}
		)
			;

		Main.NewText(_yetToDrain);
		return _yetToDrain;
	}
}

