using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class Grid 
{
    private CellModel[,] cells;
    private int size;

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
}
