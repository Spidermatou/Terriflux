using Godot;
using System;
using Terriflux.Programs;

public partial class PlacementList : RawNode
{
	// children
	private ItemList _buildingsList;

	public PlacementList() : base() { }

    public override void _Ready()
	{
		this._buildingsList = GetNode<ItemList>("BuildingsList");

		// add one model of all type of buildings avaible
		BuildingFactory buildingFactory = new();
        Add(buildingFactory.CreateEnergySupplier());
		Add(buildingFactory.CreateShaft());
		Add(buildingFactory.CreateField());
		Add(buildingFactory.CreateFactory());
		Add(buildingFactory.CreateBakery());
		Add(buildingFactory.CreateGrocery());
    }

    public void Add(Building building)
	{
		this._buildingsList.AddItem(building.Name, building.GetIcon(), true);
	}

	private void OnItemSelected(int index)	// TODO
	{
		// notify

		// reset focus
		this._buildingsList.DeselectAll();
    }
}
