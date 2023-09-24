using Godot;
using System;

public partial class CellModel : ICellObservable
{
    private string cname = "Cell";
    private int size = 128; // px
    private CellKind kind = CellKind.PRIMARY;
    private ICellObserver observer;
    private Vector2I placement;
    private string skinExtension = ".png"; 

    // CONSTRUCT
    public CellModel() { this.SetPlacement(0,0); }

    public CellModel(int x, int y)
    {
        this.SetPlacement(x, y);
    }

    public CellModel(string name, int x, int y) { 
        this.SetCellName(name);
        this.SetPlacement(x, y);
    }

    // GET-SET
    public int GetCellSize()
    {
        return this.size;
    }

    public void SetCellSize(int newSellSize)
    {
        this.size = newSellSize;
    }

    public void SetCellName(string newName)
    {
        this.cname = newName;
    }

    public string GetCellName()
    {
        return this.cname;
    }

    public void SetCellKind(CellKind newKind)
    {
        this.kind = newKind;
    }

    public CellKind GetCellKind()
    {
        return this.kind;
    }

    public void SetPlacement(int x, int y)
    {
        this.placement = new Vector2I(x, y);
        this.NotifyPlacement();
    }

    public Vector2I GetPlacement()
    {
        return this.placement;
    }

    public string GetSkinExtension() // TODO please add to uml
    {
        return this.skinExtension;
    }

    /// <summary>
    /// Set extension (png, jpeg, etc.) of the skin to the specified new extension
    /// </summary>
    /// <param name="extension"></param>
    public void SetSkinExtension(string extension) // TODO please add to uml
    {
        if (!extension.Contains('.'))
        {
            extension = "." + extension;
        }
        this.skinExtension = extension;
    }

    // Observer
    public void SetObserver(ICellObserver observer)
    {
        this.observer = observer;
    }

    public void NotifyPlacement()
    {
        if (observer != null)
        {
            this.observer.UpdatePlacement(this.placement);
        }
    }

    public void NotifyCellName()
    {
        this.observer.UpdateCellName(cname);
    }
    public ICellObserver GetObserver()
    {
        return this.observer;
    }
}
