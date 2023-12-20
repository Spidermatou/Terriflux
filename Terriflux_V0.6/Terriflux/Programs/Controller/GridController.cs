using Godot;
using System;
using System.Text;
using Terriflux.Programs.Gauges;
using Terriflux.Programs.Model.Cell;
using Terriflux.Programs.Model.Grid;
using Terriflux.Programs.Model.Placeables;
using Terriflux.Programs.Model.Round;
using Terriflux.Programs.View;

namespace Terriflux.Programs.Controller
{
	public static class GridController
	{
		private static GridModel grid;  // controlled grid
		private static RoundModel rounds;     // rounds
		private static Impacts impacts;    // impacts
		private static IPlaceableView lastCaller;   // last selected IPlaceable


		private static IPlaceable wantToPlace; // model of cell requested to be placed by the player
		private static readonly Vector2I NULL_SELECTED_COORDINATES = new(-1, -1);    // default, invalid, says it's unselected (Vector2I cannot be null)
		private static Vector2I selectedCoordinates = NULL_SELECTED_COORDINATES;    // coordinates of the cell to be modified by player action

		/* ***************************
		* Verbose
		************ */
		public static string Verbose()
		{
			StringBuilder sb = new();
			sb.AppendLine($"- Grid configured? {grid != null} ;");
			sb.AppendLine($"- Rounds configured? {rounds != null} ;");
			sb.AppendLine($"- Impacts configured? {impacts != null}.");
			return sb.ToString();
		}

		/* ***************************
		* Building placement
		************ */
		public static void SetGrid(GridModel grid)
		{
			GridController.grid = grid;
		}

		public static void SetRoundManager(RoundModel roundModel)
		{
			rounds = roundModel;
		}

		/// <summary>
		/// Indicates which cell on the grid (visible) the player wants to modify (her position in the grid model is calculated automatically).
		/// </summary>
		/// <param name="viewCoordinates">The true coordinates (on screen) of the grid view.</param>
		public static void SetSelectedCoordinates(Vector2 viewCoordinates, IPlaceableView caller)
		{
			if (grid != null)
			{
				// already have selected coordinates?
				if (selectedCoordinates != NULL_SELECTED_COORDINATES && caller != null)
				{
					// reset last caller aspect (he is no longer selected)
					lastCaller.ResetTexture();
				}

				// grid placement calculation
				Vector2I inGridCoordinates = new((int)(viewCoordinates.X / CellView.GetGlobalSize()),
				   (int)(viewCoordinates.Y / CellView.GetGlobalSize()));

				// save her coordinates to place something later
				selectedCoordinates = inGridCoordinates;

				// save caller
				lastCaller = caller;
			}
			else
			{
				caller.ResetTexture();
			}
		}

		/// <summary>
		/// Indicates which cell the player wants to place.
		/// </summary>
		/// <param name="cellModel"></param>
		public static void SetModelRequested(CellModel cellModel)
		{
			wantToPlace = cellModel;
		}

		/* ***************************
		* Impacts
		************ */
		public static void SetControledImpacts(Impacts newControledImpacts)
		{
			impacts = newControledImpacts;
		}

		/* ***************************
		* Placement
		************ */
		/// <summary>
		/// Requests replacement of the cell at the location previously selected 
		/// via GridController.SetSelectedCoordinates() by the cell previously selected 
		/// via GridController.SetModelRequested().
		/// </summary>
		public static void StartPlacement()
		{
			// grid assigned?
			if (grid != null && rounds != null && impacts != null)
			{
				// does the player have chosen the location to modify AND the new cell he wants?
				if (wantToPlace != null && selectedCoordinates != NULL_SELECTED_COORDINATES)
				{
					// maximum of builds reached for this turn?
					if (rounds.GetThisTurn() >= rounds.GetMaxPerTurn())
					{
						PopUp.Say("Maximum construction reached for this round!");
						lastCaller.ResetTexture();
					}
					// is space free?
					else if (grid.GetAllPlacements().ContainsKey(selectedCoordinates))
					{
						PopUp.Say("The selected slot is not empty!");
						lastCaller.ResetTexture();
					}
					// all ok?
					else
					{
						// place the wanted build at wanted coordinates
						grid.PlaceAt(wantToPlace, selectedCoordinates.X, selectedCoordinates.Y, true);

						// remove one possibility of building 
						rounds.PlusOneBuilded();

						// updates impacts
						if (wantToPlace is BuildingModel wantedBuilding)
						{
							impacts.AddEconomy(wantedBuilding.GetImpacts()[0]); // previous + newPlaced's impacts
							impacts.AddEcology(wantedBuilding.GetImpacts()[1]);
							impacts.AddSocial(wantedBuilding.GetImpacts()[2]);
						}
					}

					// reset for next
					wantToPlace = null;
					selectedCoordinates = NULL_SELECTED_COORDINATES;
				}
			}
			else
			{
				throw new Exception("GridController not set correctly!");
			}
		}

	}
}
