using Godot;
using System;
using System.Collections.Generic;

public partial class CellModel : ICellObservable
{
    public const int DEFAULT_CELL_SIZE = 128; //px
    public const int DEFAULT_SCALE = 1;

    private string cname = "Cell";
    private CellKind kind = CellKind.PRIMARY;
    private List<ICellObserver> observers = new List<ICellObserver>();
    private Vector2I placement;
    private string skinExtension = ".png";

    // CONSTRUCT
    public CellModel()
    {
        this.SetPlacement(0, 0);
    }

    public CellModel(int x, int y)
    {
        this.SetPlacement(x, y);
    }

    public CellModel(string name, CellKind kind, int x, int y)
    {
        this.SetCellName(name);
        this.SetPlacement(x, y);
    }

    // GET-SET
    public int GetCellSize()
    {
        return DEFAULT_CELL_SIZE * DEFAULT_SCALE;
    }

    public void SetCellName(string newName)
    {
        this.cname = newName;
        NotifyCellName();
    }

    public string GetCellName()
    {
        return this.cname;
    }

    public void SetCellKind(CellKind newKind)
    {
        this.kind = newKind;
        NotifyKind();
    }

    public CellKind GetCellKind()
    {
        return this.kind;
    }

    public void SetPlacement(int x, int y)
    {
        this.placement = new Vector2I(x, y);
        this.NotifyPlacement();
        NotifyPlacement();
    }

    public Vector2I GetPlacement()
    {
        return this.placement;
    }

    public string GetSkinExtension()
    {
        return this.skinExtension;
    }

    /// <summary>
    /// Set extension (png, jpeg, etc.) of the skin to the specified new extension
    /// </summary>
    /// <param name="extension"></param>
    public void SetSkinExtension(string extension)
    {
        if (!extension.Contains('.'))
        {
            extension = "." + extension;
        }
        this.skinExtension = extension;
    }

    // Observer
    public void AddObserver(ICellObserver observer)
    {
        this.observers.Add(observer);
    }

    public void NotifyPlacement()
    {
        foreach (ICellObserver observer in this.observers)
        {
            observer.UpdatePlacement(this.placement);
        }
    }

    public void NotifyCellName()
    {
        foreach (ICellObserver observer in this.observers)
        {
            observer.UpdateCellName(this.cname);
        }
    }
    public ICellObserver[] GetObserver()
    {
        return this.observers.ToArray();
    }

    public void NotifyKind()
    {
        foreach (ICellObserver observer in this.observers)
        {
            observer.UpdateCellKind(this.kind);
        }
    }

    public void RemoveObserver(ICellObserver observer)
    {
        if (this.observers.Contains(observer))
        {
            this.observers.Remove(observer);
        }
    }

    public void NotifyAllCellsInfos()
    {
        NotifyPlacement();
        NotifyCellName();
        NotifyKind();
    }
}
