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
class AltarEnegyDrain : EnergyDrainer
{
    public AltarEnegyDrain(float amountDrainFlora) : base(amountDrainFlora)
    {

    }

    public float Drain(float amount, Player ply, int i, int j, ref List<Item> slots)
    {
        return DrainAltar(amount,ply,i,j,ref slots);
    }

}

