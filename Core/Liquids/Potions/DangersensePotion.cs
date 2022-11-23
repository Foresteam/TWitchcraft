using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class DangersensePotion : Liquid {
	public override string Name => "Dangersense potion";
	public override Color Color => new Color(243, 120, 65);
	public override Color? ColorSecondary => new Color(140, 32, 0);
	public DangersensePotion(float volume = 0) : base(volume) {}
}