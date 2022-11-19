using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;

namespace TWitchery;
public class EnergyDrainer<T> where T : ModTileEntity {
	// private Tile _tile;
	private float _yetToDrain;
	private int _x, _y;

	public EnergyDrainer(int i, int j, float amount) {
		Player player = Main.LocalPlayer;
		
		HelpMe.GetTileOrigin(ref i, ref j);
		_x = i; _y = j;

		_yetToDrain = amount;
	}

	public float Drain() {
		foreach (var step in new Action[] { DrainFlora, DrainBlocks })
			if (_yetToDrain > 0)
				step();
			else
				break;
		Main.NewText(_yetToDrain);
		return Math.Max(0, _yetToDrain);
	}

	private void DrainFlora() {
		int radius = 10;
		for (int t = _x - radius + 1; t < _x + radius; t++)
			for (int o = _y - radius + 2; o < _y + radius; o++) {
				//Main.NewText(Main.tile[t, o].TileType);
				if (Main.tile[t, o].TileType == 2) {
					//Main.NewText("Есть: "+ Main.tile[t, o].TileType);
					_yetToDrain -= 15;
					WorldGen.ReplaceTile(t, o, TileID.Dirt, 0);
				}
				if (_yetToDrain < 0)
					break;
			}
	}

	private void DrainBlocks() {
		//tile.HasTile();
		int radius = 6;
		for (int o = _y - radius + 2; o < _y + radius; o++)
			for (int t = _x - radius + 1; t < _x + radius; t++) {
				if (Main.tile[t, o].TileType == TileID.Dirt && Main.tile[t, o].HasTile) {
					_yetToDrain -= 5;
					WorldGen.ReplaceTile(t, o, TileID.Stone, 0);
				}
				if (_yetToDrain < 0)
					break;
			}
	}
}