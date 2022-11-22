using System;
using Terraria;
using Terraria.ModLoader;

namespace TWitchery {
	public class TWitchery : Mod {
		
	}
	public class TWitcheryPlayer : ModPlayer {
		public int CalcDepletionLimits() {
			return (int)(.5f * Player.statManaMax);
		}
		// Cur: -100
		// Max: 200
		// Take: 150
		/// <returns>Amount that couldn't be taken. 0 or below means success</returns>
		public float TakeMana(int amount, bool useDeplition = false) {
			float yetToTake = amount;
			if (useDeplition)
				yetToTake -= CalcDepletionLimits();
			else
				amount = Math.Min(amount, Math.Max(Player.statMana, 0));
			yetToTake -= Player.statMana;
			Player.statMana = Math.Max(useDeplition ? -CalcDepletionLimits() : 0, Player.statMana - amount);
			return yetToTake;
		}
	}
}