using Godot;
using System;

public static partial class GridFactory 
{
    public static Grid CreatePrimaryLand(int size)
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

    public static Grid CreateNoMansLand(int size)
    {
        Grid g = new Grid(size);
        CellModel model = new(); // for size and pos
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                CellModel grass = CellsFactory.CreateGrass(model.GetCellSize() * x,
                    model.GetCellSize() * y);
                grass.SetCellKind(CellKind.WASTELAND);
                g.SetAt(grass, x, y);
            }
        }
        return g;
    }
}
