using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Terriflux.Programs.View;

namespace Terriflux.Programs.Model
{
    public interface IPlaceable
    {
        /// <summary>
        /// Return all cells wich must be placed if this object is
        /// </summary>
        /// <returns></returns>
        List<CellModel> GetComposition();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The orientation (horizontal or vertical) where the placeable element is supposed to be oriented </returns>
        Direction2D GetDirection();

        /// <summary>
        ///  
        /// </summary>
        /// <returns>Number of cell wich compose the placeable element </returns>
        int GetPartsNumber();

        /// <summary>
        /// Modify the placement data for the first cell wich compose the element, 
        /// then deduces and modifies the rest of the cells' placement, 
        /// according to the element orientation.
        /// </summary>
        void ChangeOriginCoordinates(Vector2I newCoordinates);
    }
}
