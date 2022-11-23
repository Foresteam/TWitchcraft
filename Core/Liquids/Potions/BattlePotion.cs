using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class BattlePotion : Liquid {
	public override string Name => "Battle potion";
	public override Color Color => new Color(74, 55, 116);
	public override Color? ColorSecondary => new Color(151, 131, 199);
	public BattlePotion(float volume = 0) : base(volume) {}
}