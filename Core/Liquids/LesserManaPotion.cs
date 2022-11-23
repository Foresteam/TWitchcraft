using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class LesserManaPotion : Liquid {
	public override string Name => "Lesser mana potion";
	public override Color Color => new Color(16, 45, 152);
	public override Color? ColorSecondary => new Color(11, 61, 245);
	public LesserManaPotion(float volume = 0) : base(volume) {}
}
//93, 127, 255