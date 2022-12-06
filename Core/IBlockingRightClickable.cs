using System.Threading.Tasks;

namespace TWitchery.Tiles;
interface IBlockingRightClickable {
	public Task<bool> RightClick(int i, int j);
}