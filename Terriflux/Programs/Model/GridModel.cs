using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Terriflux.Programs.Exceptions;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Observers;

namespace Terriflux.Programs.Model
{
    /// <summary>
    /// A grid of cell that can accommodate any Placeable element.
    /// (Note: it is impossible to modify the size of a grid once it has been created).
    /// </summary>
    public partial class GridModel : IVerbosable    // Reworked
    {
        private readonly CellModel[,] cells;
        private readonly int size;
        private readonly Dictionary<Vector2I, IPlaceable> placementTable = new();    // refers to which Placeable is stored where in the grid
        private readonly Dictionary<Vector2I, Orientation2D> directions = new();    // refers Placeables directions to key coordinates
        private readonly List<Vector2I> blockedEmplacements = new();    // refers to know non-empty cells

        private readonly List<IGridObserver> observers = new();

        // CONSTRUCT
        /// <summary>
        /// Instantiates a empty cells grid (square).
        /// </summary>
        /// <param name="size">Length AND width</param>
        public GridModel(int size)
        {
            // security
            if (size <= 0)
            {
                throw new Exception("Cannot a grid with null or negative size.");
            }

            // creation
            this.size = size;
            cells = new CellModel[this.size, this.size];
        }

        // Infos
        public int GetSize()
        {
            return this.size;
        }

        // Cells
        /// <summary>
        /// Modifies the replacementCell model in the grid
        /// </summary>
        /// <param name="replacementCell"></param>
        /// <param name="line"></param>
        /// <param name="column"></param>
        /// <param name="callForUpdate">If set to True, processes the display directly for the player (more ergonomic). 
        ///     Otherwise, processes it at the next Notify (more optimized).</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void ReplaceAt(CellModel replacementCell, int line, int column, bool callForUpdate = false)
        {
            // security (avoid out of range)
            if (size < line || size < column)
            {
                throw new ArgumentOutOfRangeException($"Try to access a cell outside the grid limits: " +
                    $"{this}'s size={size} but accessed coordinates are {line},{column}");
            }

            // set
            cells[line, column] = replacementCell;

            // update view?
            if (callForUpdate)
            {
                NotifyGridChanged();
            }
        }

        public CellModel GetCellAt(int line, int column)
        {
            if (size < line && size < column)
            {
                throw new IndexOutOfRangeException($"Tries to place a cell beyond the limits of the grid.");
            }
            else if (cells[line, column] == null)
            {
                throw new NullReferenceException($"Empty emplacement at {line}, {column}.");
            }
            else
            {
                return cells[line, column];
            }
        }

        // Placeables

        public void PlaceAt(IPlaceable placeable, int line, int column, Orientation2D direction, bool callForUpdate = false)
        {
            // security (placeable is to big for the grid, starting at this specified coordinates)
            if (placeable.GetComposition().Length+line > this.GetSize()
                || placeable.GetComposition().Length + column > this.GetSize())
            {
                throw new UnplaceableException(this.size, placeable.GetComposition().Length + line,
                    placeable.GetComposition().Length + column);
            }

            // security: is there a placeable here?
            for (int i = 0; i < placeable.GetComposition().Length; i++)
            {
                if (direction == Orientation2D.HORIZONTAL)
                {
                    Vector2I nextCoordinates = new(line + i, column);
                    if (this.blockedEmplacements.Contains(nextCoordinates))
                    {
                        throw new UnplaceableException(nextCoordinates);
                    }
                }
                else
                {
                    Vector2I nextCoordinates = new(line, column + i);
                    if (this.blockedEmplacements.Contains(nextCoordinates))
                    {
                        throw new UnplaceableException(nextCoordinates);
                    }
                }
            }

            // First grid observation - verify if the necessary placement is free
            for (int i = 0; i < placeable.GetComposition().Length && i < this.GetSize(); i++)
            {

                // look placeable-number-of-part cells to right
                if (direction == Orientation2D.HORIZONTAL)
                {
                    CellModel actualCell = cells[line + i, column]; // next replacementCell on right

                    // is the size already used?
                    if (actualCell.GetKind() != CellKind.WASTELAND)
                    {
                        throw new UnplaceableException();
                    }
                }
                // look placeable-number-of-part cells below
                else if (direction == Orientation2D.VERTICAL)
                {
                    CellModel actualCell = cells[line, column + i]; // replacementCell just below

                    // is the size already used?
                    if (actualCell.GetKind() != CellKind.WASTELAND)
                    {
                        throw new UnplaceableException();
                    }
                }
            }

            // Second grid observation - place the cells
            int placed = 0;     // nb of already placed cells
            CellModel[] parts = placeable.GetComposition();
            for (int i = 0; i < parts.Length && i < this.GetSize(); i++)
            {
                // look placeable-number-of-part cells to right
                if (direction == Orientation2D.HORIZONTAL)
                {
                    ReplaceAt(parts[placed], line + i, column);
                    placed++;
                }
                // look placeable-number-of-part cells below
                else if (direction == Orientation2D.VERTICAL)
                {
                    ReplaceAt(parts[placed], line, column + i);
                    placed++;
                }
            }

            // Update view now, if wanted
            if (callForUpdate)
            {
                NotifyGridChanged();
            }

            // Save placement
            placementTable.Add(new Vector2I(line, column), placeable);

            // Save direction
            this.directions.Add(new Vector2I(line, column), direction);

            // Block the used cells
            for (int i = 0; i < placeable.GetComposition().Length; i++)
            {
                if (direction == Orientation2D.HORIZONTAL)
                {
                    this.blockedEmplacements.Add(new Vector2I(line + i, column));   // block next cell on the right
                }
                else
                {
                    this.blockedEmplacements.Add(new Vector2I(line, column + i));   // block next cell below
                }
            }
        }

        public IPlaceable GetAt(int line, int column)
        {
            Vector2I coordinates = new(line, column);

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

        public Dictionary<Vector2I, IPlaceable> GetAllPlacements()
        {
            return this.placementTable.ToDictionary(entry => entry.Key, entry => entry.Value);  // copy
        }

        public Dictionary<Vector2I, Orientation2D> GetAllPlacementsDirections()
        {
            return this.directions.ToDictionary(entry => entry.Key, entry => entry.Value);  // copy
        }

        public List<Vector2I> GetBlockedEmplacements()
        {
            return this.blockedEmplacements;
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
        public void NotifyGridChanged()
        {
            foreach (IGridObserver observer in observers)
            {
                observer.UpdateMap(this);
            }
        }

        // Verbose
        public string Verbose()
        {
            StringBuilder sb = new();

            // print some informations
            sb.Append($"Size={size}\n");

            // observers
            sb.Append($"Observers ({observers.Count}): ");
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