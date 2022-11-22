using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class GillsPotion : Liquid {
	public override string Name => "Gills potion";
	public override Color Color => new Color(8, 50, 90);
	public override Color? ColorSecondary => new Color(5, 87, 184);
	public GillsPotion(float volume) : base(volume) {}
}