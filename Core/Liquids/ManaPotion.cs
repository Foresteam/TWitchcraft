using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class ManaPotion : WeakManaPotion {
	public override string Name => "Mana potion";
	public override Color Color => (Color)base.ColorSecondary;
	public override Color? ColorSecondary => new Color(93, 127, 255);
	public ManaPotion(float volume) : base(volume) {}
}