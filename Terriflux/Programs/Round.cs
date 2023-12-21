using Godot;
using System;
using Terriflux.Programs;

public partial class Round : RawNode, IRound, IPlaceMediator
{
    private Button number;
    private PlaceMediator mediator;

    public Round() { }

    public int GetNumber()
    {
        return int.Parse(number.Text);
    }

    public void Next()
    {
        number.Text = (int.Parse(number.Text) + 1).ToString();

        if (mediator == null) throw new Exception("Invalid Round configuration");
        mediator.Notify(this);
    }
    
    public void SetMediator(PlaceMediator mediator)
    {
        this.mediator = mediator;
    }

    // does nothing special
    public void Notify(IPlaceMediator sender) { }

    public override void _Ready()
	{
		base._Ready();

        number = GetNode<Button>("Number");

        number.Text = "1";
	}

	private void OnPressed()
    {
        Next();
    }
}
