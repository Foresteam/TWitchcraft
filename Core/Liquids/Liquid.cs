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

	/// <exception cref="ArgumentException">Types don't strictly match</exception>
	public static Liquid operator +(Liquid a, Liquid b) {
		if (a.GetType() != b.GetType())
			throw new ArgumentException("Types don't strictly match");
		Liquid rs = (Liquid)a.MemberwiseClone();
		rs.Volume += b.Volume;
		return rs;
	}
	public static Liquid operator *(Liquid a, float b) {
		Liquid result = (Liquid)a.MemberwiseClone();
		result.Volume *= b;
		return result;
	}
}