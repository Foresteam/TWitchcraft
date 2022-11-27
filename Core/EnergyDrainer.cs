using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections.Generic;
using System.Linq;
using System;

namespace EnergyDrain
{
    public class EnergyDrainer
    {
        private Tile tile;
        private float amountLeft;
        private int x;
        private int y;
        Vector2 pos;

        public EnergyDrainer(int i, int j, float amount)
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

            pos = new Vector2(x, y);
        }

        public void Drain()
        {

            if (amountLeft > 0)
            {
                drainLivingForms();
            }
            if (amountLeft > 0)
            {
                drainBiome();
            }
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

        private void drainBiome()
        {
            int radius = 10;
            for (int t = x - radius + 1; t < x + radius; t++)
            {
                for (int o = y - radius + 2; o < y + radius; o++)
                {
                    //Main.NewText(Main.tile[t, o].TileType);
                    if (Main.tile[t, o].TileType == 3)
                    {
                        //Main.NewText("Есть: "+ Main.tile[t, o].TileType);
                        amountLeft -= 25;
                        WorldGen.KillTile(t, o);
                    }
                    if (amountLeft < 0)
                    {
                        break;
                    }
                }
            }
        }

        private void drainLivingForms()
        {
            float maxDetectDistance = 400f;
            NPC closestNPC = null;

            // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            // Loop through all NPCs(max always 200)
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                float maxDetectRadius = 1000f;
                NPC target = Main.npc[k];
                // Check if NPC able to be targeted. It means that NPC is
                // 1. active (alive)
                // 2. chaseable (e.g. not a cultist archer)
                // 3. max life bigger than 5 (e.g. not a critter)
                // 4. can take damage (e.g. moonlord core after all it's parts are downed)
                // 5. hostile (!friendly)
                // 6. not immortal (e.g. not a target dummy)
                if (target.CountsAsACritter)
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(
                        new Vector2(target.Center.ToTileCoordinates().X, target.Center.ToTileCoordinates().Y),
                        pos);
                    //Main.NewText("Центр мир: " + target.Center);
                    //Main.NewText("Центр экран?: " + target.Center.ToTileCoordinates());

                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        Main.NewText("Есть монстр: ");
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                        //target.life = 0;
                        target.StrikeNPC(999, 0, 0);
                        //target.CountsAsACritter
                    }
                }
                if (target.CanBeChasedBy())
                {

                    // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                    float sqrDistanceToTarget = Vector2.DistanceSquared(
                        new Vector2(target.Center.ToTileCoordinates().X, target.Center.ToTileCoordinates().Y),
                        pos);
                    //Main.NewText("Центр мир: " + target.Center);
                    //Main.NewText("Центр экран?: " + target.Center.ToTileCoordinates());

                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                        //target.life = 0;
                        target.StrikeNPC(999, 0, 0);
                        //target.CountsAsACritter
                    }
                }
            }
        }

        //private void drainMonsters()
        //{

        //}

    }
}
