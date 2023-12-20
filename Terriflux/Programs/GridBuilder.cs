using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terriflux.Programs
{
    /// <summary>
    /// Use to create a specific grid more easily.
    /// Design pattern: Builder
    /// </summary>
    public class GridBuilder
    {
        private Grid grid = new();

        public GridBuilder() { }

        public void BuildWasteland(Vector2I dimensions)
        {
            grid.SetDimensions(dimensions);
            grid.FillWith<Grass>();
        }

        public void Reset()
        {
            grid = new Grid();
        }

        public Grid GetResult() { return grid; }
    }
}
