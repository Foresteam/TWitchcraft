using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class WarmthPotion : Liquid {
	public override Color Color => new Color(205, 100, 30);
	public override Color? ColorSecondary => new Color(255, 250, 119);
	public WarmthPotion(float volume) : base(volume) {}
}