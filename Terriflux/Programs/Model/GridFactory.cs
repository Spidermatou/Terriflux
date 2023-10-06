using Godot;
using System;

namespace Terriflux.Programs.Model
{
    public static partial class GridFactory
    {

        public static GridModel CreateFullBuildLand(int size)
        {
            /*
            Grid g = new(size);
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    CellModel grass = GrassModel.Create();
                    grass.SetPlacement(x * grass.GetExactDimensions(),
                        y * grass.GetExactDimensions());   
                    g.SetAt(grass, x, y);
                }
            }
            return g;
            */
            throw new NotImplementedException();
        }

        public static GridModel CreateFullGrassLand(int size)
        {
            GridModel g = new(size);
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    CellModel grass = GrassModel.Create();
                    grass.SetPlacement(x * grass.GetExactDimensions(),
                        y * grass.GetExactDimensions());
                    g.SetAt(grass, x, y, true);
                }
            }
            return g;
        }
    }
}