using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class WeakHealingPotion : Liquid {
	public override string Name => "Weak healing poton";
	public override Color Color => new Color(150, 0, 0);
	public WeakHealingPotion(float volume) : base(volume) {}
}