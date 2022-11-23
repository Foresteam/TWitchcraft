using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class EssenseOfChaos : Liquid {
	public override Color Color => new LuckPotion().Color;
	public override Color? ColorSecondary => new Color(20, 20, 20);
	public EssenseOfChaos(float volume = 0) : base(volume) {}
}