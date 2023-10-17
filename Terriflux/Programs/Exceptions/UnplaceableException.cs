using Godot;
using System;
using Terriflux.Programs.Model;

namespace Terriflux.Programs.Exceptions
{
    public class UnplaceableException : Exception
    {
        /// <summary>
        /// Exception for impossible placement issue, with message "Unable to place item on grid!".
        /// </summary>
        public UnplaceableException() : base("Unable to place item on grid!") { }

        /// <summary>
        /// Exception for impossible placement issue, with message "Unable to place item on grid! At this coordinates, 
        /// the placeable will it will exceed the grid size. Coordinates_X+Placeable_size={xPlusSize} and 
        /// Coordinates_X+Placeable_size={yPlusSize}"
        /// </summary>
        /// <param name="xPlusSize"></param>
        /// <param name="yPlusSize"></param>
        public UnplaceableException(int grid_size, int xPlusSize, int yPlusSize) : base($"Unable to place item on grid! At this coordinates, the placeable will " +
            $"exceed the grid size. " +
            $"Expected = {grid_size}, but coordinates-X more placeable-size = {xPlusSize} and " +
            $"coordinates-X more placeable-size = {yPlusSize}.") { }


        /// <summary>
        /// Exception for impossible placement issue, 
        /// with message "Unable to place item on grid, necesary is '{necessary}' but actual is '{actual}'!".
        /// </summary>
        public UnplaceableException(CellKind actual, CellKind necessary) : base($"Unable to place item on grid, necesary is '{necessary}' but actual is '{actual}'!") { }

        /// <summary>
        /// Exception if you attempts to place one placeable on top of another (i.e. coordinates conflicts), 
        /// with message Unable to place item on grid at ({coordinates.X},{coordinates.Y}), 
        /// there is already something here who can not be replaced! 
        /// </summary>
        /// <param name="coordinates"></param>
        public UnplaceableException(Vector2I coordinates) : base($"Unable to place item on grid at ({coordinates.X},{coordinates.Y}), there is already something here who can not be replaced!") { }
    }
}
