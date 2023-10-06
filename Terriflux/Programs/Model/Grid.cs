using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using Terriflux.Programs.Exceptions;
using Terriflux.Programs.Model;
using Terriflux.Programs.Observers;
using Terriflux.Programs.View;

public partial class Grid : IGridObservable
{
    private CellModel[,] cells;
    private int size;
    private IGridObserver observer;

    public Grid(int size)
    {
        this.size = size;
        this.cells = new CellModel[this.size, this.size] ;
    }

    // CELLS
    public void SetAt(CellModel cell, int line, int column)
    {
        if (size < line && size < column)
        {
            throw new ArgumentOutOfRangeException();
        }
        this.cells[line, column] = cell;
            Notify();
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

    // BUILDINGS
    /// <summary>
    /// Place a building on the grid
    /// </summary>
    public void PlaceAt(IPlaceable placeable, Vector2I originPositionPlacement)
    {
        List<CellView> parts = placeable.GetComposition();
        Direction2D direction = placeable.GetDirection();

        // is necessary placement free?
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


        // Place the cell - TODO


    }

    // INFOS
    public int GetSize()
    {
        return this.size;
    }

    public void SetObserver(IGridObserver observer)
    {
        this.observer = observer;
    }

    public void Notify()
    {
        if (this.observer != null)
        {
            observer.UpdateMap(this);
        }
    }

}
