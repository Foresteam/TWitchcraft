using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class ShinePotion : Liquid {
	public override Color Color => new Color(255, 255, 126);
	public override Color? ColorSecondary => new Color(162, 168, 7);
	public ShinePotion(float volume) : base(volume) {}
}