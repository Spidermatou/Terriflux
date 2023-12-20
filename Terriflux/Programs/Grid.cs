using Godot;
using System;
using System.Collections.Generic;

namespace Terriflux.Programs;
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
    /// <typeparam name="T">Class derived from Cell and having a constructor without parameters.</typeparam>
    public void FillWith<T>() where T : Cell, new()
    {
        for (int line = 0; line < this.cells.GetLength(0); line++)
        {
            for (int column = 0; column < this.cells.GetLength(1); column++)
            {
                SetAt(new Vector2I(line, column), new T(), false);
            }
        }

        // update
        UpdateDisplay();
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

    public void SetAt(Vector2I coordinates, ICell cell, bool callForUpdate = true)
    {
        // secu
        VerifyCoordinates(coordinates);

        cells[coordinates.X, coordinates.Y] = cell;

        // update now?
        if (callForUpdate)
        {
            UpdateDisplay();
        }
    }

    public int DistanceBewteen(Vector2I position1, Vector2I position2)  
    {
        // secu
        VerifyCoordinates(position1);
        VerifyCoordinates(position2);

        // Calcul de la distance de Manhattan
        int distanceX = Math.Abs(position1.X - position2.X);
        int distanceY = Math.Abs(position1.Y - position2.Y);

        return distanceX + distanceY;
    }

    public Building[] GetInactiveBuildings()    
    {
        List<Building> inactiveBuildings = new();
        foreach (Building building in GetAllOfType<Building>())
        {
            if (!building.IsActive())
            {
                inactiveBuildings.Add(building);
            }
        }
        return inactiveBuildings.ToArray();
    }

    private void VerifyCoordinates(Vector2 coordinates)
    {
        if (coordinates.X > dimensions.X || coordinates.Y > dimensions.Y)
        {
            throw new ArgumentException("Coordinates given exceed grid size!", nameof(coordinates));
        }
    }

    public T[] GetAllOfType<T>() where T : ICell
    {
        List<T> found = new();
        foreach (ICell cell in cells)
        {
            if (cell is T cellT)
            {
                found.Add(cellT);
            }
        }
        return found.ToArray();
    }

    /// <summary>
    /// Updates grid display.
    /// </summary>
    private void UpdateDisplay()
    {
        // Reset all
        ResetDisplay();

        // (re) Add new children
        Vector2 oneCellSize = this.cells[0,0].GetDimensions();
        Vector2 oneCellScale = ((Cell)this.cells[0, 0]).Scale;
        GD.Print($"GLOBAL x={oneCellSize.X}, y={oneCellSize.Y}");      // test
        for (int line = 0; line < dimensions.X; line++)
        {
            for (int column = 0; column < dimensions.Y; column++)
            {
                Node2D visualDraft = RawNode.Instantiate(this.cells[line, column].GetType().Name);
                visualDraft.Position = new Vector2(line * oneCellSize.X * oneCellScale.X, column * oneCellSize.Y * oneCellScale.Y);
                this.AddChild(visualDraft);    // instantiate 
                GD.Print($"act x={visualDraft.Position.X}, act y={visualDraft.Position.Y}");      // test
            }
        }
        GD.Print($"nb child= {this.GetChildren().Count}");      // test

    }

    private void ResetDisplay()
    {
        this.GetChildren().Clear();
    }
}
