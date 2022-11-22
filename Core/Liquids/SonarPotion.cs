using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class SonarPotion : Liquid {
	public override Color Color => new Color(61, 111, 1);
	public override Color? ColorSecondary => new Color(144, 208, 39);
	public SonarPotion(float volume) : base(volume) {}
}