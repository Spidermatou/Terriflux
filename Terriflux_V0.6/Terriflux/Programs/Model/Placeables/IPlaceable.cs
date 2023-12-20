using Terriflux.Programs.Model.Cell;

namespace Terriflux.Programs.Model.Placeables
{
	/// <summary>
	/// A placeable element
	/// </summary>
	public interface IPlaceable
	{
		/// <returns>The name of the element.</returns>
		string GetName();

		/// <returns>All cells wich must be placed if this object is.</returns>
		CellModel GetComposition();
	}
}
