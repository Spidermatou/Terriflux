using Godot;
using System;

public partial class GridGenerator 
{
    public Grid GenerateGrid(int size)
    {
        Grid g = new Grid(size);
        CellModel model = new(); // for size and pos
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                CellModel cm = new CellModel(
                    model.GetCellSize() * x,
                    model.GetCellSize() * y
                    );
                g.SetAt(cm, x, y);
            }
        }
        return g;
    }
}
