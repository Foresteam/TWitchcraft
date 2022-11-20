using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;

namespace TWitchery;
using Liquids;
class LiquidInventory : IEnumerable<Liquid> {
	private List<Liquid> _contained;
	public readonly float volume;
	public float VolumeLeft => volume - _contained.Select(liquid => liquid.Volume).Sum();

	public LiquidInventory(float volume) {
		_contained = new();
		this.volume = volume;
	}

	/// <returns>Whether the liquid was added</returns>
	public bool Add(Liquid nLiquid) {
		if (nLiquid.Volume > VolumeLeft)
			return false;
		Liquid lq = null;
		foreach (var t in _contained)
			if (t.GetType() == nLiquid.GetType()) {
				lq = t;
				break;
			}
		if (lq == null) {
			lq = nLiquid;
			_contained.Add(lq);
			return true;
		}
		lq.Volume += nLiquid.Volume;
		return true;
	}
	
	/// <returns>Volume taken</returns>
	public float Take<T>(float amount = 0) {
		if (amount < 0)
			amount = 0;
		Liquid lq = null;
		foreach (var liquid in _contained)
			if (liquid is T) {
				lq = liquid;
				break;
			}
		if (lq == null)
			return 0;
		if (amount == 0) {
			var vol = lq.Volume;
			lq.Volume = 0;
			return vol;
		}
		if (lq.Volume < amount)
			return 0;
		lq.Volume -= amount;
		return lq.Volume;
	}
	#nullable enable
	public Liquid? Take(float amount) {
		if (amount > volume || _contained.Count == 0)
			return null;
		Liquid? lq = null;
		for (int i = _contained.Count - 1; i >= 0; i--)
			if (_contained[i].Volume >= amount) {
				lq = _contained[i];
				break;
			}
		if (lq == null)
			return null;
		if (Math.Abs(lq.Volume - amount) < .01f) {
			_contained.Remove(lq);
			lq.Volume = amount;
			return lq;
		}
		return lq.TakePart(amount);
	}

	#nullable enable
	public Liquid? Get<T>() where T : Liquid {
		foreach (var liquid in _contained)
			if (liquid is T)
				return liquid;
		return null;
	}
	#nullable enable
	public Liquid? Get() => _contained.Count > 0 ? _contained.Last() : null;

	public List<Liquid> GetAll() => new List<Liquid>(_contained);
	public void Flush() => _contained.Clear();

	public void Apply(ref Item item, Player ply) {
		if (!HelpMe.Vessel.IsVessel(item))
			return;
		// draw
		if (HelpMe.Vessel.IsEmpty(item)) {
			Liquid? liquidToTake = Get();
			if (liquidToTake == null)
				return;
			int itemID = HelpMe.Vessel.GetFilledWith(item, liquidToTake);
			if (itemID == 0)
				return;
			Liquid? taken = Take(HelpMe.Vessel.GetVolume(item));
			// the order should be preserved!
			HelpMe.GiveItem(new Item(itemID), ply);
			HelpMe.Consume(ref item);
			return;
		}
		// fill
		if (!HelpMe.vesselsLiquids.ContainsKey(item.type))
			return;
		var vol = HelpMe.Vessel.GetVolume(item);
		if (!Add(HelpMe.vesselsLiquids[item.type](HelpMe.Vessel.GetVolume(item))))
			return;
		if (HelpMe.vessels[item.type] == ItemID.EmptyBucket)
			HelpMe.GiveItem(new Item(ItemID.EmptyBucket), ply);
		HelpMe.Consume(ref item);
	}

	public IEnumerator<Liquid> GetEnumerator() {
		return GetAll().GetEnumerator();
	}
	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}