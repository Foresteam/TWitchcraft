using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class TitanPotion : Liquid {
	public override Color Color => new Color(71, 129, 39);
	public override Color? ColorSecondary => new Color(130, 190, 85);
	public TitanPotion(float volume = 0) : base(volume) {}
}