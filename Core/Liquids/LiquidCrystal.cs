using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class LiquidCrystal : Liquid {
	public override string Name => "Liquid crystal";
	public override Color Color => new Color(83, 98, 184);
	public override Color? ColorSecondary => new Color(115, 54, 180);
	public LiquidCrystal(float volume = 0) : base(volume) {}
}