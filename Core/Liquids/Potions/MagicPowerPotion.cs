using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class MagicPowerPotion : Liquid {
	public override Color Color => new Color(61, 7, 139);
	public override Color? ColorSecondary => new Color(223, 169, 255);
	public MagicPowerPotion(float volume = 0) : base(volume) {}
}