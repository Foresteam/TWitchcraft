using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class IronskinPotion : Liquid {
	public override string Name => "Ironskin potion";
	public override Color Color => new Color(167, 163, 32);
	public override Color? ColorSecondary => new Color(255, 248, 172);
	public IronskinPotion(float volume = 0) : base(volume) {}
}