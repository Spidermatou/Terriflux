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
    private readonly IInventory inventory;

    // info about placement status
    private Vector2I wantedCoordinates;     // where to place the cell
    private Building wantedBuild;               // what it will places
    private readonly List<Building> builtThisTurn;
       

    public PlaceMediator(IGrid grid, PlacementList placementList, IImpacts impacts, IRound round, IInventory inventory) 
    {
        this.grid = grid;
        this.placementList = placementList;
        this.round = round;
        this.impacts = impacts;
        this.inventory = inventory;
        this.wantedCoordinates = UNVALID_COORDINATES;
        this.wantedBuild = null;
        this.builtThisTurn = new();
    }

    public void Notify(IPlaceMediator sender)
    {
        // react
        switch (sender)
        {
            case PlacementList:
                wantedBuild = (Building)placementList.GetSelectedItem();
                break;
            case IGrid:
                wantedCoordinates = grid.GetSelectedCoordinates();
                break;
            case Round:
                ReactOnNextRound();
                break;
            default:
                break;
        }

        // try place
        Place();
    }

    private void ResetWants()
    {
        wantedBuild = null;
        wantedCoordinates = UNVALID_COORDINATES;
    }

    private void ReactOnNextRound()
    {
        // possible change of turn?
        bool isOk = inventory.TryImportExport();

        if (isOk == false)  // non
        {
            GD.Print("Pas assez d'argent pour importer ces marchandises ou pas assez de marchandises pour exporter ces quantités !");  
            // TODO
            //Alert alert = (Alert) RawNode.Instantiate("Alert");
            //alert.Say("Pas assez d'argent pour importer ces marchandises ou pas assez de marchandises pour exporter ces quantités !");
            round.Notify(this);
        }
        else
        {
            // update inventory
            foreach (Building building in builtThisTurn)
            {
                double[] addedValues = building.GetImpacts();
                impacts.IncrementsSocial(addedValues[0]);
                impacts.IncrementsEconomy(addedValues[1]);
                impacts.IncrementsEcology(addedValues[2]);
            }
        }
    }

    /// <summary>
    /// Confirm the placement and update the grid. <br></br>
    /// If it's a building: adds it to the nearest warehouse, if it's in its area of effect. <br></br>
    /// If it's a warehouse: searches for buildings in its zone and adds them.  
    /// </summary>
    private void Place() {
        if (wantedBuild != null && wantedCoordinates != UNVALID_COORDINATES)
        {
            grid.SetAt(wantedCoordinates, wantedBuild, true);

            // notify colleague
            grid.Notify(this);
            placementList.Notify(this);

            // save a clone has builtThisTurn
            builtThisTurn.Add((Building) RawNode.Instantiate(wantedBuild.GetType().Name));

            // reset
            ResetWants();
        }
    }   



}

