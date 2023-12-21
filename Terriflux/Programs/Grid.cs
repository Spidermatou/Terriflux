using Godot;
using System;
using System.Collections.Generic;

namespace Terriflux.Programs;
public partial class Grid : RawNode, IGrid
{
    private Vector2I dimensions;
    private ICell[,] cells;

    private Vector2I selectedCoordinates;
    private PlaceMediator mediator;

    private ICell lastSelectedCell;

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

    public void SetMediator(PlaceMediator mediator)
    {
        this.mediator = mediator;
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
                SetAt(new Vector2I(line, column), (Cell) Instantiate(new T().GetType().Name), false);
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

        // add as observer
        cell.AddObserver(this);

        // add into the grid
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
        Vector2 oneCellSize = this.cells[0, 0].GetDimensions();
        for (int line = 0; line < dimensions.X; line++)
        {
            for (int column = 0; column < dimensions.Y; column++)
            {
                ((Cell) this.cells[line, column]).Position = new Vector2(line * this.cells[line, column].GetDimensions().X, 
                                                                    column * this.cells[line, column].GetDimensions().Y);
                this.AddChild((Cell) this.cells[line, column]);    // instantiate 
            }
        }
    }

    private void ResetDisplay()
    {
        foreach (Node2D child in GetChildren())
        {
            // remove children
            this.RemoveChild(child);

            // destroys items no longer intended for storage
            // TODO
        }
    }

    public Vector2I GetCoordinatesOf(ICell cell)
    {
        if (cell == null) return PlaceMediator.UNVALID_COORDINATES;
        for (int line = 0; line < this.cells.GetLength(0); line++)
        {
            for (int column = 0; column < this.cells.GetLength(1); column++)
            {
                if (cell == this.cells[line, column])
                {
                    return new Vector2I(line, column);
                }
            }
        }
        return PlaceMediator.UNVALID_COORDINATES;
    }

    public Vector2I GetSelectedCoordinates()   
    {
        return GetCoordinatesOf(lastSelectedCell);
    }

    public void Update(ICell sender)
    {
        lastSelectedCell?.Unselect();
        lastSelectedCell = sender;

        if (mediator == null) throw new Exception("Incorrect grid configuration");
        mediator.Notify(this);
    }

    public void Notify(IPlaceMediator sender)
    {
        if (sender is PlaceMediator && lastSelectedCell != null)
        {
            lastSelectedCell.Unselect();
        }
    }
}
