using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TWitchery;
using Liquids;

class EnergyDrainer {
	// private Tile _tile;
	private float _yetToDrain;
	private float _amountDrainFlora;
	private int _x,
			_y;

	public EnergyDrainer(float amountDrainFlora = 15) {
		_amountDrainFlora = amountDrainFlora;
	}

	/// <returns>Amount of energy left to drain. Can go negative (took more than needed)</returns>
	public float DrainAltar(float amount, Player ply, int i, int j) {
		HelpMe.GetTileOrigin(ref i, ref j);
		_x = i;
		_y = j;

		_yetToDrain = amount;

		foreach (
				var step in new Action[]
				{
								() => DrainPlayer(ply),
								DrainBiome,
								DrainFlora,
								DrainBlocks,
								DrainLivingForms
				}
		)
			;

		Main.NewText(_yetToDrain);
		return _yetToDrain;
	}

	/// <returns>Amount of energy left to drain. Can go negative (took more than needed)</returns>
	public float DrainCauldron(float amount, Player ply, LiquidInventory liquids) {
		_yetToDrain = amount;

		foreach (var step in new Action[] { () => DrainPlayer(ply), () => DrainLiquid(liquids) })
			if (_yetToDrain > 0)
				step();
			else
				break;
		return _yetToDrain;
	}

	private void DrainPlayer(Player ply) =>
			ply.GetModPlayer<TWitcheryPlayer>()
					.TakeMana((int)_yetToDrain, out _yetToDrain, useDeplition: false);

	private void DrainLiquid(LiquidInventory liquids) {
		if (_yetToDrain == 0)
			return;
		List<Liquid> remove = new();
		foreach (Liquid liquid in liquids)
			if (Tables.Common.energyLiquids.ContainsKey(liquid.GetType())) {
				var mpu = Tables.Common.energyLiquids[liquid.GetType()];
				if (liquid.Volume < _yetToDrain / mpu) {
					_yetToDrain -= liquid.Volume * mpu;
					remove.Add(liquid);
					continue;
				}
				liquid.Volume -= _yetToDrain / mpu;
				return;
			}
		foreach (var toRemove in remove)
			liquids.Take(toRemove);
	}

	private void DrainFlora() {
		int radius = 10;
		for (int t = _x - radius + 1; t < _x + radius; t++)
			for (int o = _y - radius + 2; o < _y + radius; o++) {
				//Main.NewText(Main.tile[t, o].TileType);
				if (Main.tile[t, o].TileType == 2) {
					//Main.NewText("Есть: "+ Main.tile[t, o].TileType);
					_yetToDrain -= _amountDrainFlora;
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

	private void DrainBiome() {
		int radius = 10;
		for (int t = _x - radius + 1; t < _x + radius; t++) {
			for (int o = _y - radius + 2; o < _y + radius; o++) {
				//Main.NewText(Main.tile[t, o].TileType);
				if (Main.tile[t, o].TileType == 3) {
					//Main.NewText("Есть: "+ Main.tile[t, o].TileType);
					_yetToDrain -= 25;
					WorldGen.KillTile(t, o);
				}
				if (_yetToDrain < 0) {
					break;
				}
			}
		}
	}

	private void DrainLivingForms() {
		Vector2 pos;
		pos = new Vector2(_x, _y);

		float maxDetectDistance = 400f;
		NPC closestNPC = null;

		// Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
		float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

		// Loop through all NPCs(max always 200)
		for (int k = 0; k < Main.maxNPCs; k++) {
			float maxDetectRadius = 1000f;
			NPC target = Main.npc[k];
			// Check if NPC able to be targeted. It means that NPC is
			// 1. active (alive)
			// 2. chaseable (e.g. not a cultist archer)
			// 3. max life bigger than 5 (e.g. not a critter)
			// 4. can take damage (e.g. moonlord core after all it's parts are downed)
			// 5. hostile (!friendly)
			// 6. not immortal (e.g. not a target dummy)
			if (target.CountsAsACritter) {
				float sqrDistanceToTarget = Vector2.DistanceSquared(
						new Vector2(
								target.Center.ToTileCoordinates().X,
								target.Center.ToTileCoordinates().Y
						),
						pos
				);
				//Main.NewText("Центр мир: " + target.Center);
				//Main.NewText("Центр экран?: " + target.Center.ToTileCoordinates());

				// Check if it is within the radius
				if (sqrDistanceToTarget < sqrMaxDetectDistance) {
					Main.NewText("Есть монстр: ");
					sqrMaxDetectDistance = sqrDistanceToTarget;
					closestNPC = target;
					//target.life = 0;
					target.StrikeNPC(999, 0, 0);
					//target.CountsAsACritter
				}
			}
			if (target.CanBeChasedBy()) {
				float sqrDistanceToTarget = Vector2.DistanceSquared(
						new Vector2(
								target.Center.ToTileCoordinates().X,
								target.Center.ToTileCoordinates().Y
						),
						pos
				);

				// Check if it is within the radius
				if (sqrDistanceToTarget < sqrMaxDetectDistance) {
					sqrMaxDetectDistance = sqrDistanceToTarget;
					closestNPC = target;
					//target.life = 0;
					target.StrikeNPC(999, 0, 0);
					_yetToDrain -= 40;
					//target.CountsAsACritter
				}
			}
		}
	}
}
