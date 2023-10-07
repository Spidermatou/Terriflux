using Godot;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using Terriflux.Programs.Exceptions;
using Terriflux.Programs.Model;
using Terriflux.Programs.Observers;
using Terriflux.Programs.View;

/// <summary>
/// Create a cell grid that can accommodate any Placeable element.
/// (Note: it is impossible to modify the size of a grid once it has been created).
/// </summary>
public partial class GridModel : IGridObservable
{
    private CellModel[,] cells;
    private int size;
    private IGridObserver observer;

    public GridModel(int size)
    {
        this.size = size;
        this.cells = new CellModel[this.size, this.size] ;
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
        if (size < line && size < column)
        {
            throw new ArgumentOutOfRangeException();
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
            throw new ArgumentOutOfRangeException();
        }
        if (this.cells[line, column] == null)
        {
            throw new NullReferenceException();
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
    }

    // INFOS
    public int GetSize()
    {
        return this.size;
    }

    // OBSERVATION
    public void SetObserver(IGridObserver observer)
    {
        this.observer = observer;
    }

    /// <summary>
    /// Manually request a view update
    /// </summary>
    public void CallForUpdate()
    {
        NotifyGridChanges();
    }

    public void NotifyGridChanges()
    {
        if (this.observer != null)
        {
            observer.UpdateMap(this);
        }
    }
}