using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class Water : Liquid {
	public override string Name => "Water";
	public override Color Color => new Color(50, 80, 255);
	public Water(float volume = 0) : base(volume) {}
}