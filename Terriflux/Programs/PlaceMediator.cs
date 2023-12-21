using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Terriflux.Programs;

/// <summary>
/// guides the following classes: IRound,IGrid, IImpacts, PlacementList
/// </summary>
public partial class PlaceMediator : IPlaceMediator
{
    public static readonly Vector2I UNVALID_COORDINATES = new(-1, -1);

    private readonly IGrid grid;
    private readonly PlacementList placementList;
    private readonly IImpacts impacts;
    private readonly IRound round;

    // info about placement status
    private Vector2I wantedCoordinates;     // where to place the cell
    private ICell wantedCell;               // what it will places
       

    public PlaceMediator(IGrid grid, PlacementList placementList, IImpacts impacts, IRound round) 
    {
        this.grid = grid;
        this.placementList = placementList;
        this.round = round;
        this.impacts = impacts;
        this.wantedCoordinates = UNVALID_COORDINATES;
        this.wantedCell = null;
    }

    public void Notify(IPlaceMediator sender)
    {
        // react
        switch (sender)
        {
            case PlacementList:
                wantedCell = placementList.GetSelectedItem();
                break;
            case IGrid:
                wantedCoordinates = grid.GetSelectedCoordinates();
                break;
            default:
                break;
        }

        // try place
        Place();
    }

    private void ResetWants()
    {
        wantedCell = null;
        wantedCoordinates = UNVALID_COORDINATES;
    }

    /// <summary>
    /// Confirm the placement and update the grid. <br></br>
    /// If it's a building: adds it to the nearest warehouse, if it's in its area of effect. <br></br>
    /// If it's a warehouse: searches for buildings in its zone and adds them.  
    /// </summary>
    private void Place() {
        if (wantedCell != null && wantedCoordinates != UNVALID_COORDINATES)
        {
            grid.SetAt(wantedCoordinates, wantedCell, true);
            ResetWants();

            // notify colleague
            grid.Notify(this);
            placementList.Notify(this);

            /*   // TODO - end of turn
            // manage impacts
            if (wantedCell is Building wantedBuilding)    
            {
                double[] addedValues = wantedBuilding.GetImpacts();
                impacts.IncrementsSocial(addedValues[0]);
                impacts.IncrementsEconomy(addedValues[1]);
                impacts.IncrementsEcology(addedValues[2]);

                GD.Print($"Add: {addedValues[0]}, {addedValues[1]}, {addedValues[2]}");

            }
            */
        }
    }   



}

