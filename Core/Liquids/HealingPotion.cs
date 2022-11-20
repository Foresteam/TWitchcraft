using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class HealingPotion : WeakHealingPotion {
	public override string Name => "Healing potion";
	public override Color Color => new Color(230, 10, 57);
	public HealingPotion(float volume) : base(volume) {}
}