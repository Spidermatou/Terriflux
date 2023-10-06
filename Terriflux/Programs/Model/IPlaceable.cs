using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terriflux.Programs.View;

namespace Terriflux.Programs.Model
{
    public interface IPlaceable
    {
        /// <summary>
        /// Return all cells wich must be placed if this object is
        /// </summary>
        /// <returns></returns>
        List<CellView> GetComposition();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The direction (horizontal or vertical) where the placeable element is supposed to be oriented </returns>
        Direction2D GetDirection();

        /// <summary>
        ///  
        /// </summary>
        /// <returns>Number of cell wich compose the placeable element </returns>
        int GetPartsNumber();
    }
}
