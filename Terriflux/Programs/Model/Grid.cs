using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

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

    public void SetAt(CellModel cell, int line, int column)
    {
        if (size < line && size < column)
        {
            throw new ArgumentOutOfRangeException();
        }
        this.cells[line, column] = cell;
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
        observer.UpdateMap(this);        
    }
}
