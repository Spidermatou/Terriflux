using Godot;
using System.Runtime.CompilerServices;
using Terriflux.Programs.Model.Cell;
using Terriflux.Programs.Model.Grid;
using Terriflux.Programs.Model.Placeables;
using Terriflux.Programs.Model.Round;
using Terriflux.Programs.View;

namespace Terriflux.Programs.Controller
{
    public static class GridController
    {
        private static GridModel controlGrid;  // controlled grid
        private static RoundModel roundManager;     // rounds


        /* ***************************
        * Building placement
        ************ */
        // coordinates of the cell to be modified by player action
        private readonly static Vector2I NULL_SELECTED_COORDINATES = new(-1, -1);    // default, invalid, says it's unselected (Vector2I cannot be null)
        private static Vector2I selectedCoordinates = NULL_SELECTED_COORDINATES;

        // model of cell requested to be placed by the player
        private static IPlaceable wantToPlace;

        public static void SetGridControl(GridModel grid)
        {
            controlGrid = grid;
        }

        public static void SetRoundManager(RoundModel roundModel)
        {
            roundManager = roundModel;
        }

        /// <summary>
        /// Indicates which cell on the grid (visible) the player wants to modify (her position in the grid model is calculated automatically).
        /// </summary>
        /// <param name="viewCoordinates">The true coordinates (on screen) of the grid view.</param>
        public static void SetSelectedCoordinates(Vector2 viewCoordinates)
        {
            if (controlGrid != null)
            {
                // grid placement calculation
                Vector2I inGridCoordinates = new((int)(viewCoordinates.X / CellView.GetGlobalSize()),
                   (int)(viewCoordinates.Y / CellView.GetGlobalSize()));

                // save her coordinates to place something later
                selectedCoordinates = inGridCoordinates;
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

        /// <summary>
        /// Requests replacement of the cell at the location previously selected 
        /// via GridController.SetSelectedCoordinates() by the cell previously selected 
        /// via GridController.SetModelRequested().
        /// </summary>
        public static void StartPlacement()
        {
            // grid assigned?
            if (controlGrid != null && roundManager != null)
            {
                // does the player have chosen the location to modify AND the new cell he wants?
                if (wantToPlace != null && selectedCoordinates != NULL_SELECTED_COORDINATES)
                {
                    // maximum of builds reached for this turn?
                    if (roundManager.GetThisTurn() >= roundManager.GetMaxPerTurn())
                    {
                        PopUp.Say("Maximum construction reached for this round!");
                    }
                    // is space free?
                    else if (controlGrid.GetAllPlacements().ContainsKey(selectedCoordinates))
                    {
                        PopUp.Say("The selected slot is not empty!");
                    }
                    // all ok?
                    else
                    {
                        // place the wanted build at wanted coordinates
                        controlGrid.PlaceAt(wantToPlace, selectedCoordinates.X, selectedCoordinates.Y, true);

                        // remove one possibility of building 
                        roundManager.PlusOneBuilded();
                    }

                    // reset for next
                    wantToPlace = null;
                    selectedCoordinates = NULL_SELECTED_COORDINATES;
                }
            }
        }
    }
}
