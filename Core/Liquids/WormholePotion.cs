using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class WormholePotion : Liquid {
	public override Color Color => new Color(45, 208, 255);
	public override Color? ColorSecondary => new Color(56, 168, 255);
	public WormholePotion(float volume) : base(volume) {}
}