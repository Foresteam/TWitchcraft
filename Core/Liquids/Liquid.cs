using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
abstract class Liquid {
	private float _volume;
	public virtual string Name => "Generic Liquid";
	public virtual Color Color => Color.White;
	public virtual Color? ColorSecondary => null;
	public float Volume {
		get => _volume;
		set => _volume = (int)(Math.Max(0, value) * .1e+3) / .1e+3f;
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

#nullable enable
	public static Color? Blend(IEnumerable<Liquid> liquids, Func<Liquid, Color?> colorGetter) {
		if (liquids.Count() == 0)
			return null;
		Color? color = colorGetter(liquids.First());
		if (color == null)
			return null;
		float accumulated = liquids.First().Volume;
		foreach (var liquid in liquids) {
			Color? other = colorGetter(liquid);
			if (other == null || other == color)
				continue;
			else {
				color = HelpMe.Blend((Color)other, (Color)color, accumulated / liquid.Volume / (accumulated == liquids.First().Volume ? 2 : 1));
				accumulated += liquid.Volume;
			}
		}
		return color;
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
	public Liquid Clone() => (Liquid)MemberwiseClone();
}