using Godot;
using System.Collections.Generic;

namespace Terriflux.Programs.Model
{
    /// <summary>
    /// A placeable element
    /// </summary>
    public interface IPlaceable     // Reworked
    {
        /// <returns>The name of the element.</returns>
        string GetName();

        /// <returns>All cells wich must be placed if this object is.</returns>
        CellModel[] GetComposition();
    }
}
