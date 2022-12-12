using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class Blood : Liquid {
	public override Color Color => new Color(133, 4, 4);
	public override Color? ColorSecondary => new Color(120, 16, 16);
	public Blood(float volume = 0) : base(volume) { }
}