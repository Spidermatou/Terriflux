using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model.Cell;
using Terriflux.Programs.Model.Placeables;
using Terriflux.Programs.Observers;

namespace Terriflux.Programs.Model.Grid
{
	/// <summary>
	/// A grid of cell that can accommodate any Placeable element.
	/// (Note: it is impossible to modify the size of a grid once it has been created).
	/// </summary>
	public partial class GridModel : IVerbosable
	{
		private readonly CellModel[,] cells;
		private readonly int size;
		private readonly Dictionary<Vector2I, IPlaceable> placementTable = new();    // refers to which Placeable is stored where in the grid
		private readonly List<IGridObserver> observers = new();
		private readonly Dictionary<Vector2I, Warehouse> warehouses = new();

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
			return size;
		}
		
		public Dictionary<Vector2I, Warehouse> GetWarehouses()
		{
			return this.warehouses;
		}
		// Cells
		/// <summary>
		/// Modifies the grass model in the grid
		/// </summary>
		/// <param name="grass"></param>
		/// <param name="line"></param>
		/// <param name="column"></param>
		/// <param name="callForUpdate">If set to True, processes the display directly for the player (more ergonomic). 
		///     Otherwise, processes it at the next Notify (more optimized).</param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public void PlaceAt(GrassModel grass, int line, int column, bool callForUpdate = false)
		{
			// security (avoid out of range)
			if (size < line || size < column)
			{
				throw new ArgumentOutOfRangeException($"Try to access a cell outside the grid limits: " +
					$"{this}'s size={size} but accessed coordinates are {line},{column}");
			}

			// set
			cells[line, column] = grass;

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
		public void PlaceAt(IPlaceable placeable, int line, int column, bool callForUpdate = false)
		{
			Vector2I coordinates = new(line, column);

			// is the cell free?
			if (!placementTable.ContainsKey(coordinates))
			{
				// place the placeable
				placementTable.Add(coordinates, placeable);
			}

			// update model
			cells[line, column] = placeable.GetComposition();

			// Update view now, if wanted
			if (callForUpdate)
			{
				NotifyGridChanged();
			}

			// If the player places a warehouse, his neighbours were added
			if (placeable.GetName() == "Warehouse")
			{
				Warehouse warehouse = new Warehouse(placeable.GetName());
				int[] warehouseCoordinate = new int[]{line,column};
				//Search of different placeables on the grid
				foreach(KeyValuePair <Vector2I, IPlaceable> kvp in this.GetAllPlacements()){
					//Ignore the current placeable
					if(kvp.Key.X != line && kvp.Key.Y != column){
						int[] placeableCoordinates = new int[]{kvp.Key.X,kvp.Key.Y};
					
						if (DistanceBetween(warehouseCoordinate,placeableCoordinates) <=2){
							warehouse.AddNeighbour((BuildingModel)kvp.Value);
						}
					}
				}
				warehouses.Add(coordinates,warehouse);
			}
			else
			{
				if(warehouses.Count > 0){
					int[] placeableCoordinates = new int[]{line,column};
					foreach (KeyValuePair <Vector2I, Warehouse> kvp in warehouses)
					{
						int[] warehouseCoordinate = new int[]{kvp.Key.X,kvp.Key.Y};
						if(DistanceBetween(warehouseCoordinate,placeableCoordinates)<=2)
						{
							kvp.Value.AddNeighbour((BuildingModel)placeable);
						}
					}
				}
			}
		}

		public IPlaceable GetPlaceableAt(int line, int column)
		{
			Vector2I coordinates = new(line, column);

			// is in the grid?
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
			return placementTable.ToDictionary(entry => entry.Key, entry => entry.Value);  // copy
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
		private void NotifyGridChanged()
		{
			foreach (IGridObserver observer in observers)
			{
				observer.UpdateMap(this);
			}
		}

		public void ForceUpdate() { this.NotifyGridChanged(); }
		
		public int DistanceBetween(int[] coordinate1, int[] coordinate2)
		{
			int distance = Convert.ToInt32(Math.Sqrt((coordinate1[0] - coordinate2[0])*(coordinate1[0] - coordinate2[0]) + (coordinate1[1] - coordinate2[1])*(coordinate1[1] - coordinate2[1])));
			return distance;
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
