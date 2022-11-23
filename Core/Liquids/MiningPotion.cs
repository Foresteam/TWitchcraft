using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class MiningPotion : Liquid {
	public override Color Color => new Color(49, 90, 90);
	public override Color? ColorSecondary => new Color(166, 206, 209);
	public MiningPotion(float volume = 0) : base(volume) {}
}