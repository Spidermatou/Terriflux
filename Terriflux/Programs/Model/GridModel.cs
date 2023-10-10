using Godot;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Terriflux.Programs.Exceptions;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model;
using Terriflux.Programs.Observers;
using Terriflux.Programs.View;

/// <summary>
/// Create a cell grid that can accommodate any Placeable element.
/// (Note: it is impossible to modify the size of a grid once it has been created).
/// </summary>
public partial class GridModel : IVerbosable
{
    private readonly CellModel[,] cells;
    private readonly int size;
    private readonly List<IGridObserver> observers = new();
    private readonly Dictionary<Tuple<int, int>, IPlaceable> placementTable = new();    // refers to which Placeable is stored where in the grid

    /// <summary>
    /// Instantiates a square cell grid
    /// </summary>
    /// <param name="size">Length AND width</param>
    /// <param name="autoFilling">If true, fill in the primary cell grid. Otherwise, leave the grid with nulls.</param>
    public GridModel(int size, bool autoFilling = false)
    {
        this.size = size;
        this.cells = new CellModel[this.size, this.size] ;

        if (autoFilling)
        {
            for (int line  = 0; line < this.size; line++)
            {
                for (int column  = 0; column < this.size; column++)
                {
                    this.cells[line, column] = new CellModel();
                }
            }
        }
    }

    // CELLS
    /// <summary>
    /// Modifies the cell model in the grid
    /// </summary>
    /// <param name="cell"></param>
    /// <param name="line"></param>
    /// <param name="column"></param>
    /// <param name="callForUpdate">If set to True, processes the display directly for the player (more ergonomic). 
    ///     Otherwise, processes it at the next Notify (more optimized).</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void SetAt(CellModel cell, int line, int column, bool callForUpdate = false)
    {
        // security (avoid out of range)
        if (size < line || size < column)
        {
            throw new ArgumentOutOfRangeException($"Try to access a cell outside the grid limits: " +
                $"{this}'s size={size} but accessed coordinates are {line},{column}");
        }

        // set
        this.cells[line, column] = cell;

        // update view?
        if (callForUpdate) NotifyGridChanges();
    }

    public CellModel GetAt(int line, int column)
    {
        if (size < line && size < column)
        {
            throw new ArgumentOutOfRangeException("Tries to place a cell beyond the limits of the grid");
        }
        if (this.cells[line, column] == null)
        {
            throw new NullReferenceException($"No initialized cell at emplacement {line}, {column}!");
        }
        return this.cells[line, column];
    }

    // BUILDINGS Place a building on the grid 
    /// <summary>
    /// Place a placeable element on the grid, if specified emplacement is free
    /// </summary>
    /// <param name="placeable"></param>
    /// <param name="originPositionPlacement"></param>
    /// <param name="callForUpdate">If set to True, processes the display directly for the player(more ergonomic). 
    /// Otherwise, processes it at the next Notify (more optimized).</param>
    /// <exception cref="UnplaceableException"></exception>
    public void PlaceAt(IPlaceable placeable, Vector2I originPositionPlacement, bool callForUpdate = false)
    {
        Direction2D direction = placeable.GetDirection();

        // First grid observation - verify if the necessary placement is free
        for (int i = 0; i < placeable.GetPartsNumber() && i < this.GetSize(); i++)
        {

            // look placeable-number-of-part cells to right
            if (direction == Direction2D.HORIZONTAL)
            {
                CellModel actualCell = this.cells[originPositionPlacement.X + i, originPositionPlacement.Y]; // next cell on right

                // is the size already used?
                if (actualCell.GetCellKind() == CellKind.WASTELAND) 
                {
                    throw new UnplaceableException(); 
                }
            }
            // look placeable-number-of-part cells below
            else if (direction == Direction2D.VERTICAL)
            {
                CellModel actualCell = this.cells[originPositionPlacement.X, originPositionPlacement.Y + i]; // cell just below

                // is the size already used?
                if (actualCell.GetCellKind() == CellKind.WASTELAND)
                {
                    throw new UnplaceableException();
                }
            }
        }

        // First grid observation - place the cells
        List<CellModel> parts = placeable.GetComposition();
        int placed = 0;     // nb of already placed cells
        for (int i = 0; i < placeable.GetPartsNumber() && i < this.GetSize(); i++)
        {
            // look placeable-number-of-part cells to right
            if (direction == Direction2D.HORIZONTAL)
            {
                this.SetAt(parts[placed],
                    originPositionPlacement.X + i,
                    originPositionPlacement.Y);
                placed++;                
            }
            // look placeable-number-of-part cells below
            else if (direction == Direction2D.VERTICAL)
            {
                this.SetAt(parts[placed],
                    originPositionPlacement.X,
                    originPositionPlacement.Y + i);
                placed++;
            }
        }

        // Indicates modification of element cell placement for saving
        placeable.ChangeOriginCoordinates(originPositionPlacement);

        // Update view now, if wanted
        if (callForUpdate) NotifyGridChanges();

        // Save placement
        this.placementTable.Add(new Tuple<int, int>(originPositionPlacement.X, originPositionPlacement.Y), placeable);
    }

    public IPlaceable GetPlaceableAt(int line, int column)
    {
        Tuple<int, int> coordinates = new(line, column);

        // Is in the grid?
        if (this.placementTable.ContainsKey(coordinates))
        {
            return this.placementTable[coordinates];
        }
        else
        {
            return null;
        }
    }

    // INFOS
    public int GetSize()
    {
        return this.size;
    }

    // OBSERVATION
    public void AddObserver(IGridObserver observer) 
    {
        if (!this.observers.Contains(observer))
        {
            this.observers.Add(observer);
        }
    }

    public void RemoveObserver(IGridObserver observer) 
    {
        if (this.observers.Contains(observer))
        {
            this.observers.Remove(observer);
        }
    }

    /// <summary>
    /// Manually request a view update
    /// </summary>
    public void CallForUpdate()
    {
        NotifyGridChanges();
    }

    private void NotifyGridChanges()
    {
        foreach (IGridObserver observer in this.observers)
        {
            observer.UpdateMap(this);
        }
    }

    public string Verbose()
    {
        StringBuilder sb = new();

        // print some informations
        sb.Append($"Size={size}\n");

        // observers
        sb.Append($"Observers : ");
        foreach (IGridObserver observer in this.observers)
        {
            sb.Append(observer);
        }
        sb.Append('\n');

        // print of the actual grid content
        for (int line  = 0; line < this.size; line++)
        {
            for (int column = 0; column < this.size; column++)
            {
                if (this.cells[line, column] != null)
                {
                    sb.Append(this.cells[line, column].GetCellName());
                }
                else
                {
                    sb.Append("NULL");
                }

                if (column < this.size - 1) sb.Append(',');
            }
            sb.Append('\n');
        }

        return sb.ToString();
    }
}