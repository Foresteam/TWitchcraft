using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class LesserLuckPotion : Liquid {
	public override string Name => "Lesser luck potion";
	public override Color Color => new Color(110, 116, 166);
	public override Color? ColorSecondary => new Color(171, 191, 223);
	public LesserLuckPotion(float volume = 0) : base(volume) {}
}