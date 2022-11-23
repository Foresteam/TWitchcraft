using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class EvilExtract : Liquid {
	public override Color Color => new BattlePotion().Color;
	public override Color? ColorSecondary => new Color(30, 0, 30);
	public EvilExtract(float volume = 0) : base(volume) {}
}