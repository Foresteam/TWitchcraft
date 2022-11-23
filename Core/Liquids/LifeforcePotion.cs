using Microsoft.Xna.Framework;

// The "secret" potion. Should this be used for something else?
namespace TWitchery.Liquids;
class LifeforcePotion : Liquid {
	public override string Name => "Lifeforce potion";
	public override Color Color => new Color(33, 3, 55);
	public override Color? ColorSecondary => new Color(255, 98, 178);
	public LifeforcePotion(float volume = 0) : base(volume) {}
}