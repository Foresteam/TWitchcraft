using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class NightOwlPotion : Liquid {
	public override Color Color => new Color(87, 143, 8);
	public override Color? ColorSecondary => new Color(149, 223, 25);
	public NightOwlPotion(float volume = 0) : base(volume) {}
}