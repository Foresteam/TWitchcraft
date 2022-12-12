using System;
using Terraria;

namespace TWitchery.CauldronCore;
class CauldronEnegyDrainer : EnergyDrainer {
	public float Drain(float amount, Player ply, LiquidInventory liquids) {
		_yetToDrain = amount;

		foreach (var step in new Action[] { () => DrainLiquid(liquids), () => DrainPlayer(ply) })
			if (_yetToDrain > 0)
				step();
			else
				break;
		return _yetToDrain;
	}
}
