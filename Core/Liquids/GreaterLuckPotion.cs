using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class GreaterLuckPotion : Liquid {
	public override string Name => "Greater luck potion";
	public override Color Color => new Color(197, 39, 170);
	public override Color? ColorSecondary => new Color(159, 30, 133);
	public GreaterLuckPotion(float volume) : base(volume) {}
}