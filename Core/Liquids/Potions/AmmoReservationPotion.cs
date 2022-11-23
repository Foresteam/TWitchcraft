using Microsoft.Xna.Framework;

namespace TWitchery.Liquids;
class AmmoReservationPotion : Liquid {
	public override Color Color => new Color(133, 137, 136);
	public override Color? ColorSecondary => new Color(213, 129, 22);
	public AmmoReservationPotion(float volume = 0) : base(volume) {}
}