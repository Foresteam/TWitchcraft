using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class CratePotion : Liquid {
	public override string Name => "Crate potion";
	public override Color Color => new Color(132, 79, 26);
	public override Color? ColorSecondary => new Color(230, 172, 125);
	public CratePotion(float volume) : base(volume) {}
}