using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terriflux.Programs.Exceptions;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Observers;

namespace Terriflux.Programs.Model
{

    /// <summary>
    /// Design a cell grid that can accommodate any Placeable element.
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
            cells = new CellModel[this.size, this.size];

            if (autoFilling)
            {
                for (int line = 0; line < this.size; line++)
                {
                    for (int column = 0; column < this.size; column++)
                    {
                        cells[line, column] = new CellModel();
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
            cells[line, column] = cell;

            // update view?
            if (callForUpdate)
            {
                NotifyGridChanges();
            }
        }

        public CellModel GetAt(int line, int column)
        {
            if (size < line && size < column)
            {
                throw new IndexOutOfRangeException($"Tries to place a cell beyond the limits of the grid");
            }
            if (cells[line, column] == null)
            {
                throw new NullReferenceException($"No initialized cell at emplacement {line}, {column}!");
            }
            return cells[line, column];
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
            for (int i = 0; i < placeable.GetPartsNumber() && i < GetSize(); i++)
            {

                // look placeable-number-of-part cells to right
                if (direction == Direction2D.HORIZONTAL)
                {
                    CellModel actualCell = cells[originPositionPlacement.X + i, originPositionPlacement.Y]; // next cell on right

                    // is the size already used?
                    if (actualCell.GetKind() != CellKind.WASTELAND)
                    {
                        throw new UnplaceableException();
                    }
                }
                // look placeable-number-of-part cells below
                else if (direction == Direction2D.VERTICAL)
                {
                    CellModel actualCell = cells[originPositionPlacement.X, originPositionPlacement.Y + i]; // cell just below

                    // is the size already used?
                    if (actualCell.GetKind() == CellKind.WASTELAND)
                    {
                        throw new UnplaceableException();
                    }
                }
            }

            // First grid observation - place the cells
            List<CellModel> parts = placeable.GetComposition();
            int placed = 0;     // nb of already placed cells
            for (int i = 0; i < placeable.GetPartsNumber() && i < GetSize(); i++)
            {
                // look placeable-number-of-part cells to right
                if (direction == Direction2D.HORIZONTAL)
                {
                    SetAt(parts[placed],
                        originPositionPlacement.X + i,
                        originPositionPlacement.Y);
                    placed++;
                }
                // look placeable-number-of-part cells below
                else if (direction == Direction2D.VERTICAL)
                {
                    SetAt(parts[placed],
                        originPositionPlacement.X,
                        originPositionPlacement.Y + i);
                    placed++;
                }
            }

            // Indicates modification of element cell placement for saving
            placeable.ChangeOriginCoordinates(originPositionPlacement);

            // Update view now, if wanted
            if (callForUpdate)
            {
                NotifyGridChanges();
            }

            // Save placement
            placementTable.Add(new Tuple<int, int>(originPositionPlacement.X, originPositionPlacement.Y), placeable);
        }

        public IPlaceable GetPlaceableAt(int line, int column)
        {
            Tuple<int, int> coordinates = new(line, column);

            // Is in the grid?
            if (placementTable.ContainsKey(coordinates))
            {
                return placementTable[coordinates];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A clone of Dictionary(Coordinates,Placeable) wich represent all Placeable actually placed
        /// on the grid. </returns>
        public Dictionary<Tuple<int, int>, IPlaceable> GetPlaceablesInfos()
        {
            return placementTable.ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        // INFOS
        public int GetSize()
        {
            return size;
        }

        // OBSERVATION
        public void AddObserver(IGridObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void RemoveObserver(IGridObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
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
            foreach (IGridObserver observer in observers)
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
            foreach (IGridObserver observer in observers)
            {
                sb.Append(observer);
            }
            sb.Append('\n');

            // print of the actual grid content
            for (int line = 0; line < size; line++)
            {
                for (int column = 0; column < size; column++)
                {
                    if (cells[line, column] != null)
                    {
                        sb.Append(cells[line, column].GetName());
                    }
                    else
                    {
                        sb.Append("NULL");
                    }

                    if (column < size - 1)
                    {
                        sb.Append(',');
                    }
                }
                sb.Append('\n');
            }

            return sb.ToString();
        }
    }
}