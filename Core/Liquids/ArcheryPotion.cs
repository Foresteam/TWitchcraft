using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class ArcheryPotion : Liquid {
	public override string Name => "Archery potion";
	public override Color Color => new Color(255, 186, 91);
	public override Color? ColorSecondary => new Color(165, 95, 7);
	public ArcheryPotion(float volume = 0) : base(volume) {}
}