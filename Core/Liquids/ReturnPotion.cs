using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class ReturnPotion : Liquid {
	public override Color Color => new Color(80, 68, 197);
	public override Color? ColorSecondary => new Color(249, 217, 255);
	public ReturnPotion(float volume = 0) : base(volume) {}
}