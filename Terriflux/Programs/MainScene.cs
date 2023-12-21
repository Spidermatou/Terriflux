using Godot;
using System;
using Terriflux.Programs;

public partial class MainScene : RawNode
{
    Inventory inventory;

    public MainScene() : base() { }

	public override void _Ready()
	{
		base._Ready();

        // create alert
        this.AddChild(Alert.GetInstance());

        // create grid      (have to be generate via script)
        GridBuilder gridBuilder = new();
        gridBuilder.BuildWasteland(new(7, 7));
        Grid grid = gridBuilder.GetResult();
        grid.Position = GetNode<Marker2D>("GridMark").Position;
        grid.Scale = new Vector2((float)0.9, (float)0.9); 
        this.AddChild(grid);

        // get placement list
        PlacementList placementList = GetNode<PlacementList>("PlacementList");

        // get impacts
        Impacts impact = GetNode<Impacts>("Impacts");

        // get round
        Round round = GetNode<Round>("Round");

        // get inventory
        inventory = GetNode<Inventory>("Inventory");

        // create mediator
        PlaceMediator mediator = new(grid, placementList, impact, round, inventory);
        placementList.SetMediator(mediator);
        grid.SetMediator(mediator);
        round.SetMediator(mediator);

        // hiding
        inventory.Hide();
    }

    private void OnExitGamePressed()
    {
        GetTree().Quit();
    }

    private void OnOpenInventoryPressed()
    {
        if (inventory.Visible) inventory.Hide();
        else inventory.Show();
    }
}
