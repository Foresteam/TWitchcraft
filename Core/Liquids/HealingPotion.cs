using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class HealingPotion : Liquid {
	public override string Name => "Healing poton";
	public override Color Color => new Color(200, 15, 0);
	public HealingPotion(float volume) : base(volume) {}
}