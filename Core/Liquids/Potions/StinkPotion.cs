using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class StinkPotion : Liquid {
	public override Color Color => new Color(50, 76, 0);
	public override Color? ColorSecondary => new Color(135, 184, 24);
	public StinkPotion(float volume = 0) : base(volume) {}
}