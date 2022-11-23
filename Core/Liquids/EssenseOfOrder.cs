using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class EssenseOfOrder : Liquid {
	public override Color Color => new LesserLuckPotion().Color;
	public override Color? ColorSecondary => new Color(0, 0, 0);
	public EssenseOfOrder(float volume = 0) : base(volume) {}
}