using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class ObsidianSkin : Liquid {
	public override Color Color => new Color(54, 44, 104);
	public override Color? ColorSecondary => new Color(125, 110, 199);
	public ObsidianSkin(float volume = 0) : base(volume) {}
}