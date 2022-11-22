using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class TeleportationPotion : Liquid {
	public override Color Color => new Color(87, 2, 158);
	public override Color? ColorSecondary => new Color(216, 45, 255);
	public TeleportationPotion(float volume) : base(volume) {}
}