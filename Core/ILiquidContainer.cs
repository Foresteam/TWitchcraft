using TWitchery.Liquids;
using System.Collections.Generic;

namespace TWitchery;
interface ILiquidContainer {
	public bool Pour(Liquid liquid);
	public bool Draw<T>(float amount) where T : Liquid;
	#nullable enable
	public Liquid? Draw();
	public List<Liquid> GetLiquids();
	public void ClearLiquids();
}