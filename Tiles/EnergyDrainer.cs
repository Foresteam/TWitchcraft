using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections.Generic;
using System.Linq;
using System;

namespace EnergyDrain { 
	public class EnergyDrainer
	{
		private Tile tile;
		private float amountLeft;
		private int x;
		private int y;

		public EnergyDrainer(int i,int j, float amount)
		{
           
            Player player = Main.LocalPlayer;
            Tile tile = Main.tile[i, j];

            x = i; y = j;

            amountLeft = amount;

            if (tile.TileFrameX < 16)
            {
                x++;
            }

            if (tile.TileFrameY < 16)
            {
                y++;
            }

            if (tile.TileFrameX > 16)
            {
                x--;
            }

            if (tile.TileFrameY > 16)
            {
                y--;
            }

              
        }

        public void Drain()
        {
            if (amountLeft > 0)
            {
                drainFlora();
            }
            if (amountLeft > 0)
            {
                drainBlocks();
            }
            Main.NewText(amountLeft);
        }

        private void drainFlora()
        {
            int radius = 10;
            for (int t = x - radius + 1; t < x + radius; t++)
            {
                for (int o = y - radius + 2; o < y + radius; o++)
                {
                    //Main.NewText(Main.tile[t, o].TileType);
                    if (Main.tile[t, o].TileType == 2)
                    {
                        //Main.NewText("Есть: "+ Main.tile[t, o].TileType);
                        amountLeft -= 15;
                        WorldGen.ReplaceTile(t, o, TileID.Dirt, 0);
                    }
                    if (amountLeft < 0)
                    {
                        break;
                    }
                }
            }
        }

        private void drainBlocks()
        {
            //tile.HasTile();
            int radius = 6;
            for (int o = y - radius + 2; o < y + radius; o++)
            {
                for (int t = x - radius + 1; t < x + radius; t++)
                {
                    if (Main.tile[t, o].TileType == TileID.Dirt && Main.tile[t, o].HasTile)
                    {
                        amountLeft -= 5;
                        WorldGen.ReplaceTile(t, o, TileID.Stone, 0);
                    }
                    if (amountLeft < 0)
                    {
                        break;
                    }
                }
            }
        }
    }

}
