using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class RegenerationPotion : Liquid {
	public override Color Color => new Color(182, 28, 105);
	public override Color? ColorSecondary => new Color(205, 113, 158);
	public RegenerationPotion(float volume) : base(volume) {}
}