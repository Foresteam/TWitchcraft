using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class CalmingPotion : Liquid {
	public override string Name => "Calming potion";
	public override Color Color => new Color(41, 51, 159);
	public override Color? ColorSecondary => new Color(131, 147, 189);
	public CalmingPotion(float volume) : base(volume) {}
}