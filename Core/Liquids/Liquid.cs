using System;
using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
abstract class Liquid {
	private float _volume;
	public virtual string Name => "Generic Liquid";
	public virtual Color Color => new Color(255, 255, 255, 255);
	public float Volume {
		get => _volume;
		set => _volume = Math.Max(0, value);
	}

	public Liquid(float volume = 0) {
		Volume = volume;
	}

	#nullable enable
	public Liquid? TakePart(float volume) {
		if (Volume < volume)
			return null;
		var lq = (Liquid)MemberwiseClone();
		lq.Volume = Volume - volume;
		Volume -= volume;
		return lq;
	}
}