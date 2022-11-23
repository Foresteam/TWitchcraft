using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class RecallPotion : Liquid {
	public override Color Color => new Color(1, 98, 117);
	public override Color? ColorSecondary => new Color(97, 194, 214);
	public RecallPotion(float volume = 0) : base(volume) {}
}