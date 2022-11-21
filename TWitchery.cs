using Terraria.ModLoader;

namespace TWitchery {
	public class TWitchery : Mod {
		
	}
	public class TWitcheryPlayer : ModPlayer {
		public void TakeMana(int amount) {
			this.Player.statMana -= amount;
		}
	}
}