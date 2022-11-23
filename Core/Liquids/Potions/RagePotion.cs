using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class RagePotion : Liquid {
	public override Color Color => new Color(218, 6, 7);
	public override Color? ColorSecondary => new Color(195, 133, 10);
	public RagePotion(float volume = 0) : base(volume) {}
}