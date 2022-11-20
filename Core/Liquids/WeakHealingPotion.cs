using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class WeakHealingPotion : Liquid {
	public override string Name => "Weak healing potion";
	public override Color Color => new Color(164, 16, 47);
	public override Color? ColorSecondary => new Color(255, 95, 129);
	public WeakHealingPotion(float volume) : base(volume) {}
}