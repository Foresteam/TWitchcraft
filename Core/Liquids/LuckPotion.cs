using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class LuckPotion : Liquid {
	public override string Name => "Luck potion";
	public override Color Color => new Color(18, 27, 42);
	public override Color? ColorSecondary => new Color(74, 93, 101);
	public LuckPotion(float volume) : base(volume) {}
}