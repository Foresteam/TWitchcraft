using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class BuilderPotion : Liquid {
	public override string Name => "Builder potion";
	public override Color Color => new Color(120, 82, 63);
	public override Color? ColorSecondary => new Color(219, 184, 164);
	public BuilderPotion(float volume = 0) : base(volume) {}
}