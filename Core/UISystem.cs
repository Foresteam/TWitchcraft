using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TWitchery.Cauldron;

namespace TWitchery;
class UISystem : ModSystem {
	public static UISystem instance;
	private UserInterface _cauldronHoverUI;

	public override void Load() {
		instance = this;

		if (!Main.dedServ)
			_cauldronHoverUI = new UserInterface();

		base.Load();
	}
	public override void Unload() {
		instance = null;
		_cauldronHoverUI = null;
		base.Unload();
	}


	private GameTime _lastUpdateUiGameTime;
	public override void UpdateUI(GameTime gameTime) {
		_lastUpdateUiGameTime = gameTime;

		if (!Main.tile[Main.MouseWorld.ToTileCoordinates()].HasTile)
			CloseCauldronHoverUI();

		if (_cauldronHoverUI.CurrentState != null)
			_cauldronHoverUI.Update(gameTime);
	}

	public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
		int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
		if (mouseTextIndex == -1) return;

		layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
			"TWitchery: UI",
			() => {
				if (_cauldronHoverUI.CurrentState != null)
					_cauldronHoverUI.Draw(Main.spriteBatch, _lastUpdateUiGameTime);

				return true;
			},
			InterfaceScaleType.UI)
		);
	}

	public static void OpenCauldronHoverUI(StackedInventory inventory) {
		// don't open the same ChestHoverUI again
		if ((instance._cauldronHoverUI.CurrentState as UIStates.CauldronHoverUI)?.inventory == inventory)
			return;

		instance._cauldronHoverUI.SetState(new UIStates.CauldronHoverUI((CauldronInventory)inventory));
	}
	public static void CloseCauldronHoverUI() {
		// don't always close the chest
		if (instance._cauldronHoverUI.CurrentState == null)
			return;

		instance._cauldronHoverUI.SetState(null);
	}
}