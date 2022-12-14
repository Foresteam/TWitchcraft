using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using TWitchery.PedestalCore;

namespace TWitchery;
using Liquids;

abstract class EnergyDrainer {
	// private Tile _tile;
	protected float _yetToDrain;
	protected int _x, _y;
	private float _amountDrainFlora, _amountDrainBlocks, _amountDrainBiome, _amountDrainMonster, _amountDrainAnimal;
	public EnergyDrainer(float amountDrainFlora = 15,float amountDrainBlocks=5,float amountDrainBiome=25, float amountDrainMonster = 20, float amountDrainAnimal = 40) {
		_amountDrainFlora = amountDrainFlora;
		_amountDrainBlocks = amountDrainBlocks;
		_amountDrainBiome = amountDrainBiome;
		_amountDrainMonster = amountDrainMonster;
		_amountDrainAnimal = amountDrainAnimal;
	}

	protected void DrainManaPotions(List<Inventory> entrySlots)
    {
		for (int k = 0; k < entrySlots.Count; k++)
		{
			if (entrySlots[k].slots[0].healMana > 0 && entrySlots[k].slots[0].potion && entrySlots[k].slots[0].healLife == 0)
			{
				if ((_yetToDrain / entrySlots[k].slots[0].healMana) > entrySlots[k].slots[0].stack)
				{
					_yetToDrain -= entrySlots[k].slots[0].healMana * entrySlots[k].slots[0].stack;
					entrySlots[k].slots[0] = new Item();
				}
				else
				{
					entrySlots[k].slots[0].stack -= (int)(_yetToDrain / entrySlots[k].slots[0].healMana);
					_yetToDrain -= entrySlots[k].slots[0].healMana * (int)(_yetToDrain / entrySlots[k].slots[0].healMana);
					break;
				}
			}
		}
	}

	protected void DrainPlayer(Player ply) {
		float took;
		ply.GetModPlayer<TWitcheryPlayer>().TakeMana((int)_yetToDrain, out took, useDeplition: false);
		_yetToDrain -= took;
	}

	protected void DrainLiquid(LiquidInventory liquids) {
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
				_yetToDrain = 0;
				return;
			}
		foreach (var toRemove in remove)
			liquids.Take(toRemove);
	}

	protected void DrainFlora() {
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

	protected void DrainBlocks() {
		//tile.HasTile();
		int radius = 6;
		for (int o = _y - radius + 2; o < _y + radius; o++)
			for (int t = _x - radius + 1; t < _x + radius; t++) {
				if (Main.tile[t, o].TileType == TileID.Dirt && Main.tile[t, o].HasTile) {
					_yetToDrain -= _amountDrainBlocks;
					WorldGen.ReplaceTile(t, o, TileID.Stone, 0);
				}
				if (_yetToDrain < 0)
					break;
			}
	}

	protected void DrainBiome() {
		int radius = 10;
		for (int t = _x - radius + 1; t < _x + radius; t++) {
			for (int o = _y - radius + 2; o < _y + radius; o++) {
				//Main.NewText(Main.tile[t, o].TileType);
				if (Main.tile[t, o].TileType == 3) {
					//Main.NewText("Есть: "+ Main.tile[t, o].TileType);
					_yetToDrain -= _amountDrainBiome;
					WorldGen.KillTile(t, o);
				}
				if (_yetToDrain < 0) {
					break;
				}
			}
		}
	}

	protected void DrainLivingForms() {
		Vector2 pos;
		pos = new Vector2(_x, _y);

		float maxDetectDistance = 400f;
		NPC closestNPC = null;

		// Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
		float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

		// Loop through all NPCs(max always 200)
		for (int k = 0; k < Main.maxNPCs; k++) {	
			NPC target = Main.npc[k];
			// Check if NPC able to be targeted. It means that NPC is
			// 1. active (alive)
			// 2. chaseable (e.g. not a cultist archer)
			// 3. max life bigger than 5 (e.g. not a critter)
			// 4. can take damage (e.g. moonlord core after all it's parts are downed)
			// 5. hostile (!friendly)
			// 6. not immortal (e.g. not a target dummy)

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
				//Main.NewText("Есть монстр: ");
				sqrMaxDetectDistance = sqrDistanceToTarget;
				closestNPC = target;
				//target.life = 0;				
				if (target.CountsAsACritter) {
					//Monster in radius
					_yetToDrain -= _amountDrainMonster;
					//debuff?
				}
				else {
					//Animal in radius
					_yetToDrain -= _amountDrainAnimal;
				}
				target.StrikeNPC(999, 0, 0);
			}

			//if (target.CanBeChasedBy()) {
			//	float sqrDistanceToTarget = Vector2.DistanceSquared(
			//			new Vector2(
			//					target.Center.ToTileCoordinates().X,
			//					target.Center.ToTileCoordinates().Y
			//			),
			//			pos
			//	);

			//	// Check if it is within the radius
			//	if (sqrDistanceToTarget < sqrMaxDetectDistance) {
			//		sqrMaxDetectDistance = sqrDistanceToTarget;
			//		closestNPC = target;
			//		//target.life = 0;
			//		target.StrikeNPC(999, 0, 0);
			//		_yetToDrain -= 40;
			//		//target.CountsAsACritter
			//	}
			//}
		}
	}
}
