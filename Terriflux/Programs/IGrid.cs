using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terriflux.Programs
{
    public interface IGrid
    {
        /// <returns>The size of the grid.</returns>
        Vector2I GetDimensions();

        /// <summary>
        /// Retrieves the reference of all grid cells.
        /// </summary>
        /// <returns> Reference of all grid cells.</returns>
        IDictionary<ICell, Vector2I> GetAll();

        /// <summary>
        /// Retrieves the cell reference at the specified coordinates.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        ICell GetAt(Vector2I coordinates);

        /// <summary>
        /// Replaces the cell at the specified coordinates.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="cell"></param>
        void SetAt(Vector2I coordinates, ICell cell);

        int DistanceBewteen(Vector2I position1, Vector2I position2);

        /// <returns>In particular, buildings that cannot produce resources this turn.</returns>
        Building[] GetInactiveBuildings();
       
    }
}