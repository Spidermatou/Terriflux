using Godot;
using System;

public partial class TerritoryView : Node2D
{

    public override void _Ready()
    {
		// test
		GenerateMap(10, 10);
    }


    // all in model, no value stored here, just map generation
    private void GenerateMap(int lines, int columns)
	{
		Cell model = new Cell();
		for (int x = 0; x < lines; x++)
		{
			for (int y = 0; y < columns; y++)
			{
				PackedScene cellScene = GD.Load<PackedScene>("Nodes/Cell.tscn");
				Node2D cellNode = (Node2D) cellScene.Instantiate();
				// move
				cellNode.Position = new Vector2(model.GetCellSize() * x,
                                                model.GetCellSize() * y);
				// print
				this.AddChild(cellNode);
			}
		}
		
	}
}
