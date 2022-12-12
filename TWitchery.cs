using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.IO;
using System.Linq;
using static Terraria.ID.ItemID;

namespace TWitchery {
	public class TWitchery : Mod {}
	public class TWitcheryModSystem : ModSystem {
		private static readonly int[] _deletedPotions = new int[] {
			AmmoReservationPotion,
			ArcheryPotion,
			BattlePotion,
			BuilderPotion,
			CalmingPotion,
			CratePotion,
			TrapsightPotion,
			EndurancePotion,
			FeatherfallPotion,
			FishingPotion,
			GenderChangePotion,
			GillsPotion,
			GravitationPotion,
			GreaterHealingPotion,
			LuckPotionLesser,
			LuckPotion,
			LuckPotionGreater,
			LesserHealingPotion,
			HealingPotion,
			GreaterHealingPotion,
			HeartreachPotion,
			HunterPotion,
			InfernoPotion,
			InvisibilityPotion,
			IronskinPotion,
			LesserManaPotion,
			ManaPotion,
			GreaterManaPotion,
			SuperManaPotion,
			LifeforcePotion,
			LovePotion,
			MagicPowerPotion,
			ManaRegenerationPotion,
			MiningPotion,
			NightOwlPotion,
			ObsidianSkinPotion,
			RagePotion,
			RecallPotion,
			RegenerationPotion,
			RestorationPotion,
			PotionOfReturn,
			ShinePotion,
			SonarPotion,
			SpelunkerPotion,
			StinkPotion,
			SummoningPotion,
			SwiftnessPotion,
			TeleportationPotion,
			ThornsPotion,
			TitanPotion,
			WarmthPotion,
			WaterWalkingPotion,
			WormholePotion,
			WrathPotion
		};
		public override void AddRecipes() {
			// smelting the unspawned ores
			Recipe.Create(SilverBar)
				.AddIngredient(SilverCoin, 15)
				.AddTile(Terraria.ID.TileID.Furnaces)
				.Register();
		}
		public override void PostAddRecipes() {
			foreach (var result in _deletedPotions)
				foreach (var recipe in Main.recipe)
					if (recipe.HasResult(result))
						recipe.DisableRecipe();
		}
	}
	public class TWitcheryPlayer : ModPlayer {
		public int CalcDepletionLimits() {
			return (int)(.5f * Player.statManaMax);
		}
		// Cur: -100
		// Max: 200
		// Take: 150
		/// <returns>Amount that couldn't be taken. 0 or below means success</returns>
		public float TakeMana(int amount, out float took, bool useDeplition = false) {
			float yetToTake = amount;
			if (useDeplition)
				yetToTake -= CalcDepletionLimits();
			else
				amount = Math.Min(amount, Math.Max(Player.statMana, 0));
			yetToTake -= Player.statMana;
			Player.statMana = Math.Max(useDeplition ? -CalcDepletionLimits() : 0, Player.statMana - amount);
			took = amount - yetToTake;
			return yetToTake;
		}
		public float TakeMana(int amount, bool useDeplition = false) {
			float took;
			return TakeMana(amount, out took, useDeplition);
		}
	}
	public class DumpRecipesCommand : ModCommand {
		public override string Command => "tw";
		public override CommandType Type => CommandType.Chat;
		public override string Usage =>
			"/tw <dev|export> <recipes|items|liquids|vessels>" +
			"\n recipes — Dump all recipes to CSV tables" +
			"\n TWitchery mod commands";
		private int damn(System.Collections.Generic.KeyValuePair<int, Func<float, Liquids.Liquid>> p, System.Collections.Generic.Dictionary<int, Type> liquids) {
			if (p.Value(1) == null)
				return -1;
			return liquids.First(lt => lt.Value == p.Value(1).GetType()).Key;
		}
		public override void Action(CommandCaller caller, string input, string[] args) {
			bool dev = args.Length > 0 ? args[0] == "dev" : false;
			Directory.CreateDirectory("dump");
			var liquidsTable = HelpMe.GetLiquidsTable();
			switch (args.Length > 1 ? args[1] : "") {
				case "recipes":
					var recipes = Tiles.TECauldron.DumpRecipes(dev);
					if (dev)
						recipes = recipes.Replace("\"", "\"\"\"");
					File.WriteAllText($"dump/{(dev ? "cauldron_recipes" : "CauldronRecipes")}.csv", recipes);
					break;
				case "liquids":
					File.WriteAllText("dump/ids_liquids.csv", "ID;Name\n" + String.Join('\n', liquidsTable.Select(pair => pair.Key + ";" + pair.Value.Name)) + "\n-1;");
					break;
				case "items":
					File.WriteAllText("dump/ids_items.csv", "ID;Name\n" + String.Join('\n', HelpMe.GetItemsTable().Select(p => p.Key + ";" + p.Value)));
					break;
				case "vessels":
					File.WriteAllText("dump/vessels_vessels.csv", "Filled;Empty\n" + String.Join('\n', Tables.Vessels.vessels.Select(p => p.Key + ";" + p.Value)));
					File.WriteAllText("dump/vessels_liquids.csv", "Filled;Liquid\n" + String.Join('\n', 
						Tables.Vessels.vesselsLiquids
						.Select(p => p.Key + ";" + damn(p, liquidsTable))
					));
					File.WriteAllText("dump/vessels_volumes.csv", "ID;Volume\n" + String.Join('\n', 
						Tables.Vessels.vesselsVolumes
						.Select(p => p.Key + ";" + p.Value)
					));
					break;
				default:
					Main.NewText(Usage, Color.Red);
					return;
			}
			Main.NewText("Dumped successfully.");
		}
	}
	public class TestCommand : ModCommand {
		public override string Command => "tg";
		public override CommandType Type => CommandType.Chat;
		public override void Action(CommandCaller caller, string input, string[] args) {
			var ply = caller.Player;
			var item = Items.UniversalBottle.CreateFilled(new Liquids.Blood());
			ply.QuickSpawnClonedItem(null, item, 2);
		}
	}
}