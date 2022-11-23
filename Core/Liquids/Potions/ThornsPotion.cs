using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class ThornsPotion : Liquid {
	public override Color Color => new Color(142, 173, 10);
	public override Color? ColorSecondary => new Color(121, 150, 4);
	public ThornsPotion(float volume = 0) : base(volume) {}
}