using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class LesserHealingPotion : Liquid {
	public override string Name => "Lesser healing potion";
	public override Color Color => new Color(164, 16, 47);
	public override Color? ColorSecondary => new Color(255, 95, 129);
	public LesserHealingPotion(float volume) : base(volume) {}
}