using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class GreaterHealingPotion : HealingPotion {
	public override string Name => "Greater healing potion";
	public override Color Color => new Color(240, 25, 57);
	public GreaterHealingPotion(float volume = 0) : base(volume) {}
}