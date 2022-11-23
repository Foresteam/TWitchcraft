using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class MagicRestorationPotion : Liquid {
	public override Color Color => new Color(131, 11, 79);
	public override Color? ColorSecondary => new Color(255, 96, 204);
	public MagicRestorationPotion(float volume = 0) : base(volume) {}
}