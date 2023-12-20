using Godot;
using System;
using System.Collections.Generic;
using Terriflux.Programs;

public partial class Grid : RawNode, IGrid
{
    private readonly Vector2I dimensions;
    private readonly ICell[,] cells;

    public Grid(Vector2I dimensions)
    {
        this.dimensions = dimensions;
    }

    public override void _Ready()
    {
        base._Ready();
    }

    public int DistanceBewteen(Vector2I position1, Vector2I position2)  // TODO
    {
        throw new NotImplementedException();
    }

    public ICell[,] GetAll()
    {
        return cells;
    }

    public ICell GetAt(Vector2I coordinates)
    {
        // secu
        VerifyCoordinates(coordinates);

        return cells[coordinates.X, coordinates.Y];                
    }

    public Building[] GetInactiveBuildings()
    {
        throw new NotImplementedException();
    }

    public Vector2I GetDimensions()
    {
        return this.dimensions;
    }

    public void SetAt(Vector2I coordinates, ICell cell)
    {
        cells[coordinates.X, coordinates.Y] = cell;
    }

    private void VerifyCoordinates(Vector2 coordinates)
    {
        if (coordinates.X > dimensions.X || coordinates.Y > dimensions.Y)
        {
            throw new ArgumentException("Coordinates given exceed grid size!", nameof(coordinates));
        }
    }
}
