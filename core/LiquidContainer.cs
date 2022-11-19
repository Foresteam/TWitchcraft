using System;
using System.Collections.Generic;
using System.Linq;
using TWitchery.Liquids;

namespace TWitchery;
class LiquidContainer {
	private List<Liquid> _contained;
	public readonly float volume;
	public float VolumeLeft => volume - _contained.Select(liquid => liquid.Volume).Sum();

	/// <returns>Whether the liquid was added</returns>
	public bool Add<T>(float amount) where T : Liquid, new() {
		if (amount < 0)
			amount = 0;
		if (amount > VolumeLeft)
			return false;
		Liquid lq = null;
		foreach (var liquid in _contained)
			if (liquid is T) {
				lq = liquid;
				break;
			}
		if (lq == null) {
			lq = new T();
			_contained.Add(lq);
		}
		lq.Volume += amount;
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
		Liquid lq = _contained.Last();
		if (lq.Volume < amount)
			return null;
		if (lq.Volume == amount) {
			_contained.Remove(lq);
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
	public void Clear() => _contained.Clear();
}