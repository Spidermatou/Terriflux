using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Terriflux.Programs;

public partial class PlacementList : RawNode, IPlaceMediator
{
    private readonly Texture2D helpIcon;
    private readonly List<Building> drafts;      // to see needs and production 

    private PlaceMediator mediator;

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

    public void SetMediator(PlaceMediator mediator)
    {
        this.mediator = mediator;
    }

    public void Add(Building building)
	{
        this._buildingsList.AddItem(building.Name, building.GetIcon(), true);
        this._helpList.AddItem("", helpIcon, true);
        this.drafts.Add(building);      
    }

    public ICell GetItemAt(int index)
    {
        if (index > this._buildingsList.ItemCount)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        return (Building)Instantiate(this._buildingsList.GetItemText(index));
    }

    public ICell GetSelectedItem()
    {
        return GetItemAt(this._buildingsList.GetSelectedItems()[0]);
    }

    private void OnItemSelected(int index)	
	{
        // dialog
        if (mediator == null) throw new Exception("Invalid configuration for PlacementList");
        mediator.Notify(this); // can only retrieve name or image (with itemlist), so must create instante via name:
    }

    private void OnHelpSelected(int index)
    {
        // say the building's manual
        Alert.Say(this.drafts[index].GetHelp());

        // reset focus
        this._helpList.DeselectAll();
    }

    public void Notify(IPlaceMediator sender)
    {
        if (sender is PlaceMediator)
        {
            // reset focus
            this._buildingsList.DeselectAll();
        }
    }
}
