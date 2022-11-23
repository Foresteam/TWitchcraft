using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class ManaPotion : LesserManaPotion {
	public override string Name => "Mana potion";
	public override Color Color => (Color)base.ColorSecondary;
	public override Color? ColorSecondary => new Color(93, 127, 255);
	public ManaPotion(float volume = 0) : base(volume) {}
}