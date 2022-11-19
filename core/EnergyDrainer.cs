using Terraria;
using Terraria.ID;
using System;

namespace EnergyDrain;
public class EnergyDrainer {
	// private Tile _tile;
	private float _amountLeft;
	private int _x, _y;

	public EnergyDrainer(int i,int j, float amount) {
		Player player = Main.LocalPlayer;
		Tile tile = Main.tile[i, j];

		_x = i; _y = j;

		_amountLeft = amount;

		if (tile.TileFrameX < 16)
			_x++;
		if (tile.TileFrameY < 16)
			_y++;
		if (tile.TileFrameX > 16)
			_x--;
		if (tile.TileFrameY > 16)
			_y--;
	}

	public void Drain() {
		foreach (var step in new Action[] { DrainFlora, DrainBlocks })
			if (_amountLeft > 0)
				step();
			else
				break;
		Main.NewText(_amountLeft);
	}

	private void DrainFlora() {
		int radius = 10;
		for (int t = _x - radius + 1; t < _x + radius; t++)
			for (int o = _y - radius + 2; o < _y + radius; o++) {
				//Main.NewText(Main.tile[t, o].TileType);
				if (Main.tile[t, o].TileType == 2) {
					//Main.NewText("Есть: "+ Main.tile[t, o].TileType);
					_amountLeft -= 15;
					WorldGen.ReplaceTile(t, o, TileID.Dirt, 0);
				}
				if (_amountLeft < 0)
					break;
			}
	}

	private void DrainBlocks() {
		//tile.HasTile();
		int radius = 6;
		for (int o = _y - radius + 2; o < _y + radius; o++)
			for (int t = _x - radius + 1; t < _x + radius; t++) {
				if (Main.tile[t, o].TileType == TileID.Dirt && Main.tile[t, o].HasTile) {
					_amountLeft -= 5;
					WorldGen.ReplaceTile(t, o, TileID.Stone, 0);
				}
				if (_amountLeft < 0)
					break;
			}
	}
}