using Godot;
using System;
using System.Collections.Generic;
using Terriflux.Programs.Observers;

namespace Terriflux.Programs.Model
{
    public partial class CellModel 
    {
        protected const int DEFAULT_CELL_SIZE = 1024;   //px
        protected const float DEFAULT_SCALE = (float) 0.1;

        // Own infos
        private int size = DEFAULT_CELL_SIZE;
        private float scale = DEFAULT_SCALE;

        private string cname = "Cell";  // default
        private CellKind kind = CellKind.PRIMARY;   // default
        private Vector2 placement = new(0,0);   // default
        private readonly List<ICellObserver> observers = new();

        // CONSTRUCT
        public CellModel() { }

        public CellModel(string name, CellKind kind)
        {
            SetCellName(name);
            SetCellKind(kind);
        }

        // Global dimensions
        public static double GetDefaultDimension()
        {
            return DEFAULT_CELL_SIZE * DEFAULT_SCALE;
        }

        public static double GetDefaultSize()
        {
            return DEFAULT_CELL_SIZE;
        }

        public static double GetDefaultScale()
        {
            return DEFAULT_SCALE;
        }

        // Own dimensions
        /// <summary>
        /// Equivalent of GetScale() * GetSize();
        /// </summary>
        /// <returns></returns>
        public float GetExactDimensions()
        {
            return this.GetScale() * this.GetSize();
        }

        public void SetDimensions(int size, int scale)
        {
            this.size = size;
            this.scale = scale;
        }

        public float GetScale()
        {
            return this.scale;
        }

        public int GetSize()
        {
            return this.size;
        }

        // Own name
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