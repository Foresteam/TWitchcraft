using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.IO;

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
	public class DumpRecipesCommand : ModCommand {
		public override string Command => "twitchery";
		public override CommandType Type => CommandType.Chat;
		public override string Usage =>
			"/twitchery <recipes> [stack]" +
			"\n recipes — Dump all recipes to CSV tables" +
			"\n TWitchery mod commands";
		public override void Action(CommandCaller caller, string input, string[] args) {
			switch (args.Length > 0 ? args[0] : "") {
				case "recipes":
					File.WriteAllText("CauldronRecipes.csv", Tiles.TECauldron.DumpRecipes());
					Main.NewText("Dumped successfully.");
					return;
				default:
					Main.NewText(Usage, Color.Red);
					return;
			}
		}
	}
}