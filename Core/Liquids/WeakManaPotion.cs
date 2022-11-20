using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class WeakManaPotion : Liquid {
	public override string Name => "Weak mana potion";
	public override Color Color => new Color(16, 45, 152);
	public override Color? ColorSecondary => new Color(11, 61, 245);
	public WeakManaPotion(float volume) : base(volume) {}
}
//93, 127, 255