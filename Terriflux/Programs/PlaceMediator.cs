using Godot;
using System;
using System.Collections.Generic;
namespace Terriflux.Programs;

public partial class PlaceMediator : IPlaceMediator
{
    // guides the following classes:
    private IRound round;
    private IGrid grid;
    private IImpacts impacts;
    private PlacementList placeList;

    // info about placement status
    private int maxPerTurn;
    private int buildedThisTurn;            
    private Vector2I wantedCoordinates;     // where to place the cell
    private ICell wantedCell;               // what it will places             

    public void Notify(IPlaceMediator sender)   // TODO
    {

    }


    public int GetMaxPlacementPerTurn() { return maxPerTurn; }
    public int GetPlacedThisTurn() { return buildedThisTurn; }
    private void ReactOnPlacementList() { wantedCell = placeList.GetSelectedBuilding(); }
    private void ReactOnGrid() { wantedCoordinates =  }
    private void SetWantedCell(cell : ICell)
    private void SetWantedCoordinates(coordinates : Vector2I)
    private void NextTurn()

    /// <summary>
    /// Confirm the placement and update the grid. <br></br>
    /// If it's a building: adds it to the nearest warehouse, if it's in its area of effect. <br></br>
    /// If it's a warehouse: searches for buildings in its zone and adds them.  
    /// </summary>
    private void Place()



}

