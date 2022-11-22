using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class SpelunkerPotion : Liquid {
	public override Color Color => new Color(160, 100, 50);
	public override Color? ColorSecondary => new Color(255, 237, 126);
	public SpelunkerPotion(float volume) : base(volume) {}
}