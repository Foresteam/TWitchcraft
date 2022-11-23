using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class Lava : Liquid {
	public override string Name => "Lava";
	public override Color Color => new Color(217, 82, 32);
	public Lava(float volume = 0) : base(volume) {}
}