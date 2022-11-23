using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class Glass : Liquid {
	public override string Name => "Liquid glass";
	public override Color Color => new Color(200, 246, 254);
	public override Color? ColorSecondary => new Color(62, 121, 148);
	public Glass(float volume = 0) : base(volume) {}
}