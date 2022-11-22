using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class WaterWalkingPotion : Liquid {
	public override Color Color => new Color(0, 65, 145);
	public override Color? ColorSecondary => new Color(105, 163, 253);
	public WaterWalkingPotion(float volume) : base(volume) {}
}