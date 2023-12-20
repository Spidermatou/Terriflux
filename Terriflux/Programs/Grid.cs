using Godot;
using System;
using System.Collections.Generic;
using Terriflux.Programs;

public partial class Grid : RawNode, IGrid
{
    private Vector2I dimensions;
    private ICell[,] cells;

    /// <summary>
    /// Create a Grid.
    /// <br></br>
    /// NB : Use instantiation via GridBuilder instead.
    /// </summary>
    public Grid() : base() 
    {
        this.dimensions = new Vector2I(50, 50);    
        this.cells = new ICell[dimensions.X, dimensions.Y];
    } 

    public void SetDimensions(Vector2I dimensions)
    {
        this.dimensions = dimensions;
        this.cells = new ICell[dimensions.X, dimensions.Y];
    }

    /// <summary>
    /// Fills the grid with instances of the specified type.
    /// </summary>
    /// <typeparam name="T">Class derived from ICell and having a constructor without parameters.</typeparam>
    public void FillWith<T>() where T : ICell, new()
    {
        for (int line = 0; line < this.cells.GetLength(0); line++)
        {
            for (int column = 0; column < this.cells.GetLength(1); column++)
            {
                cells[line, column] = new T();
            }
        }
    }

    public override void _Ready()
    {
        base._Ready();
    }

    public Vector2I GetDimensions()
    {
        return this.dimensions;
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

    public void SetAt(Vector2I coordinates, ICell cell)
    {
        // secu
        VerifyCoordinates(coordinates);

        cells[coordinates.X, coordinates.Y] = cell;
    }

    public int DistanceBewteen(Vector2I position1, Vector2I position2)  // TODO
    {
        // secu
        VerifyCoordinates(position1);
        VerifyCoordinates(position2);

        return 0;        
    }

    public Building[] GetInactiveBuildings()    // TODO
    {
        throw new NotImplementedException();
    }

    private void VerifyCoordinates(Vector2 coordinates)
    {
        if (coordinates.X > dimensions.X || coordinates.Y > dimensions.Y)
        {
            throw new ArgumentException("Coordinates given exceed grid size!", nameof(coordinates));
        }
    }
}
