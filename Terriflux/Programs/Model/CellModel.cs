using Godot;
using System;
using System.Collections.Generic;
using Terriflux.Programs.Observers;
using Terriflux.Programs.View;

namespace Terriflux.Programs.Model
{
    /// <summary>
    /// Represent a cell.
    /// All cell have the exact same size. If one changes size, everything else adapts. You can access to these
    /// dimensions with Get/SetGlobalSize
    /// </summary>
    public partial class CellModel 
    {
        private static readonly double globalSize = 128;   //px

        private string name = "Cell";  // default
        private readonly CellKind kind = CellKind.PRIMARY;   // default
        private Vector2 placement = new(0,0);   // default
        private readonly List<ICellObserver> observers = new();

        // CONSTRUCT
        public CellModel() { }

        public CellModel(string name, CellKind kind)
        {
            SetName(name);
            this.kind = kind;
        }

        // Global dimensions
        public static double GetGlobalSize()
        {
            return globalSize;
        }

        // Own name
        public void SetName(string newName)
        {
            name = newName;
            NotifyCellName();
        }

        public string GetName()
        {
            return name;
        }

        // Own kind
        public CellKind GetKind()
        {
            return kind;
        }

        public void SetPlacement(float x, float y)
        {
            placement = new Vector2(x, y);
            NotifyPlacement();
        }

        public void SetPlacement(Vector2 coordinates)
        {
            placement = coordinates;
            NotifyPlacement();
        }

        public Vector2 GetPlacement()
        {
            return placement;
        }

        // Observer
        public void AddObserver(ICellObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(ICellObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        private void NotifyPlacement()
        {
            foreach (ICellObserver observer in observers)
            {
                observer.UpdatePlacement(placement);
            }
        }

        public void NotifyCellName()
        {
            foreach (ICellObserver observer in observers)
            {
                observer.UpdateCellName(name);
            }
        }     

        public void NotifyAlls()
        {
            NotifyPlacement();
            NotifyCellName();
        }
    }
}