using Godot;
using System;

namespace Terriflux.Programs.Model
{
    /// <summary>
    /// Provided with different grids to play several different types of scenario, 
    /// such as a virgin territory to territorialize, 
    /// or a more or less rich and complex city to make prosper.
    /// </summary>
    public static partial class GridFactory
    {
        public static GridModel CreateFullGrassLand(int size)
        {
            GridModel g = new(size);
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    GrassModel grass = new();
                    grass.SetPlacement(x * grass.GetExactDimensions(),
                        y * grass.GetExactDimensions());
                    g.SetAt(grass, x, y, true);
                }
            }
            return g;
        }
    }
}