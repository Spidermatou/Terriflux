using Godot;
using System;
using System.Collections.Generic;
using Terriflux.Programs;

public partial class Grid : RawNode, IGrid
{
    private readonly Vector2I dimensions;

    public Grid(Vector2I dimensions)
    {
        this.dimensions = dimensions;
    }

    public int DistanceBewteen(Vector2I position1, Vector2I position2)  // TODO
    {
        throw new NotImplementedException();
    }

    public IDictionary<ICell, Vector2I> GetAll()
    {
        throw new NotImplementedException();
    }

    public ICell GetAt(Vector2I coordinates)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }
}
