using Godot;
using System;
using Terriflux.Programs;

public partial class MainScene : RawNode
{
    Inventory inventory;

    // children
    Label _money;

    public MainScene() : base() { }

	public override void _Ready()
	{
		base._Ready();

        // get children
        _money = GetNode<Label>("Money");

        // create alert
        this.AddChild(Alert.GetInstance());

        // create grid      (have to be generate via script)
        GridBuilder gridBuilder = new();
        gridBuilder.BuildWasteland(new(5, 5));
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

        // create end screen
        End end = End.GetInstance();
        end.Hide();
        this.AddChild(end);

        // hiding
        inventory.Hide();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        // show _money
        _money.Text = inventory.GetMoney().ToString();
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
