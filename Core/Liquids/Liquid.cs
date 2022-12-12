using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text.Json;

namespace TWitchery.Liquids;
abstract class Liquid {
	private float _volume;
	public virtual string Name => Regex.Replace(GetType().Name, "(\\B[A-Z])", " $1");
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
		float totalVolume = liquids.Select(l => l.Volume).Sum();
		float accumulated = liquids.First().Volume;
		foreach (var liquid in liquids) {
			Color? other = colorGetter(liquid);
			if (other == null || other == color)
				continue;
			else {
				color = HelpMe.Blend((Color)color, (Color)other, accumulated / totalVolume, liquid.Volume / totalVolume);
				accumulated += liquid.Volume;
			}
		}
		return color;
	}

	/// <exception cref="ArgumentException">Types don't strictly match</exception>
	public static Liquid operator +(Liquid a, Liquid b) {
		if (a.GetType() != b.GetType())
			throw new ArgumentException("Types don't strictly match");
		Liquid rs = (Liquid)a.Clone();
		rs.Volume += b.Volume;
		return rs;
	}
	public static Liquid operator *(Liquid a, float b) {
		Liquid result = (Liquid)a.Clone();
		result.Volume *= b;
		return result;
	}
	public Liquid Clone() => (Liquid)MemberwiseClone();
	public string JDump() => '{' + $"\"id\": {HelpMe.GetLiquidsTable().First(p => p.Value == GetType()).Key}, \"volume\": {Volume}" + '}';
	public string Dump(bool json = false) => !json ? ($"{Name} x{Volume}") : JDump();
	public string? FullType => GetType().FullName;
	public string Serialize() => JsonSerializer.Serialize(this);
	public static Liquid? Deserialize(string data) {
		try {
			var json = JsonDocument.Parse(data);
			var stype = json.RootElement.GetProperty("FullType").GetString();
			if (stype == null)
				throw new Exception();
			var type = Type.GetType(stype);
			if (type == null)
				throw new Exception();
			return (Liquid?)Activator.CreateInstance(type, 0);
		}
		catch {
			return null;
		}
	}
}