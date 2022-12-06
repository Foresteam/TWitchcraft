using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;

namespace TWitchery;
#nullable enable
interface ICrafting<TAction, TInventory>
	where TAction : System.Enum
	where TInventory : StackedInventory {
	public TInventory Inventory { get; }
	public abstract TAction Interract(int i, int j, Player ply, Item[] inv, int slot);
	public abstract WitcheryRecipe.Result? Craft(int i, int j);
	public abstract void Flush(int i, int j);
	public abstract void GiveResult(WitcheryRecipe.Result? result, Point16 tile, Player ply, TileEntity source);
}