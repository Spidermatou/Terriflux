using Godot;
using System;

public partial class GridFactory 
{
    public Grid CreateTestLand(int size)
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

    public Grid CreateNoMansLand(int size)
    {
        Grid g = new Grid(size);
        CellModel model = new(); // for size and pos
        CellsFactory cf = new CellsFactory();
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                CellModel grass = cf.CreateGrass(model.GetCellSize() * x,
                    model.GetCellSize() * y);
                g.SetAt(grass, x, y);
            }
        }
        return g;
    }
}
