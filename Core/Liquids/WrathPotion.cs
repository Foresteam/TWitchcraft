using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class WrathPotion : Liquid {
	public override Color Color => new Color(153, 58, 54);
	public override Color? ColorSecondary => new Color(192, 101, 98);
	public WrathPotion(float volume = 0) : base(volume) {}
}