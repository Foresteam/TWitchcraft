using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class SummoningPotion : Liquid {
	public override Color Color => new Color(116, 140, 20);
	public override Color? ColorSecondary => new Color(217, 244, 107);
	public SummoningPotion(float volume) : base(volume) {}
}