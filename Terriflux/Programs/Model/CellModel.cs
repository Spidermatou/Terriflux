using Godot;
using System;
using System.Collections.Generic;
using Terriflux.Programs.Observers;

namespace Terriflux.Programs.Model
{
    public partial class CellModel : ICellObservable
    {
        protected const int DEFAULT_CELL_SIZE = 128; //px
        protected const int DEFAULT_SCALE = 1;

        private string cname = "Cell";
        private CellKind kind = CellKind.PRIMARY;
        private readonly List<ICellObserver> observers = new();
        private Vector2I placement;

        // CONSTRUCT
        public CellModel()
        {
            SetPlacement(0, 0);
        }

        public CellModel(int x, int y)
        {
            SetPlacement(x, y);
        }

        public CellModel(string name, CellKind kind, int x, int y)
        {
            SetCellName(name);
            SetPlacement(x, y);
            SetCellKind(kind);
        }

        // GET-SET
        public static int GetDefaultDimension()
        {
            return DEFAULT_CELL_SIZE * DEFAULT_SCALE;
        }

        public void SetCellName(string newName)
        {
            cname = newName;
            NotifyCellName();
        }

        public string GetCellName()
        {
            return cname;
        }

        public void SetCellKind(CellKind newKind)
        {
            kind = newKind;
            NotifyKind();
        }

        public CellKind GetCellKind()
        {
            return kind;
        }

        public void SetPlacement(int x, int y)
        {
            placement = new Vector2I(x, y);
            NotifyPlacement();
        }

        public void SetPlacement(Vector2I coordinates)
        {
            placement = coordinates;
            NotifyPlacement();
        }

        public Vector2I GetPlacement()
        {
            return placement;
        }

        // Observer
        public void AddObserver(ICellObserver observer)
        {
            observers.Add(observer);
        }

        public void NotifyPlacement()
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
                observer.UpdateCellName(cname);
            }
        }
        public ICellObserver[] GetObserver()
        {
            return observers.ToArray();
        }

        public void NotifyKind()
        {
            foreach (ICellObserver observer in observers)
            {
                observer.UpdateCellKind(kind);
            }
        }

        public void RemoveObserver(ICellObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        public void NotifyAlls()
        {
            NotifyPlacement();
            NotifyCellName();
            NotifyKind();
        }
    }
}