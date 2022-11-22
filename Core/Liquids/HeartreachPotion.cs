using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class HeartreachPotion : Liquid {
	public override string Name => "Heartreach potion";
	public override Color Color => new Color(95, 8, 108);
	public override Color? ColorSecondary => new Color(139, 35, 129);
	public HeartreachPotion(float volume) : base(volume) {}
}