using Godot;
using System;
using System.Collections.Generic;
using Terriflux.Programs;

public partial class PlacementList : RawNode
{
    private readonly Texture2D helpIcon;

    private readonly List<Building> drafts;      // to see needs and production 

    // children
    private ItemList _buildingsList;
	private ItemList _helpList;


    public PlacementList() : base() 
    {
        this.drafts = new();
        this.helpIcon = GD.Load<Texture2D>(PATH_IMAGES + "Help.png"); 
    }

    public override void _Ready()
	{
        this._buildingsList = GetNode<ItemList>("BuildingsList");
		this._helpList = GetNode<ItemList>("HelpList");

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
        this._helpList.AddItem("", helpIcon, true);
        this.drafts.Add(building);      
    }

    private void OnItemSelected(int index)	// TODO
	{
		// notify

		// reset focus
		this._buildingsList.DeselectAll();
    }

    private void OnHelpSelected(int index)
    {
        // say the building's manual
        Alert alert = (Alert) Instantiate("Alert");
        alert.Position = new Vector2(-225, 350);
        this.AddChild(alert);
        alert.Say(this.drafts[index].GetHelp());

        // reset focus
        this._helpList.DeselectAll();
    }
}
