using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class SwiftnessPotion : Liquid {
	public override Color Color => new Color(132, 197, 38);
	public override Color? ColorSecondary => new Color(161, 198, 112);
	public SwiftnessPotion(float volume) : base(volume) {}
}