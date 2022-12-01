using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TWitchery;
using Liquids;
class CauldronEnegyDrain : EnergyDrainer
{
    public CauldronEnegyDrain(float amountDrainFlora) : base(amountDrainFlora)
    {

    }

    public float Drain(float amount, Player ply, LiquidInventory liquids)
    {
        return DrainCauldron(amount, ply, liquids);
    }

}
