using Godot;
using System;
using Terriflux.Programs;
using static System.Net.Mime.MediaTypeNames;

public partial class Round : RawNode, IRound
{
    private Button number;
    private PlaceMediator mediator;

    private const int MAX_NB_TURN = 10;

    public Round() { }

    public int GetNumber()
    {
        return int.Parse(number.Text);
    }

    public void Next()
    {
        int next = int.Parse(number.Text) + 1;

        // victory?
        if (next >= MAX_NB_TURN)
        {
            End end = new();
            Alert.Say(end.Victory());
        }
        number.Text = next.ToString();

        if (mediator == null) throw new Exception("Invalid Round configuration");
        mediator.Notify(this);
    }
    
    private void Previous() { number.Text = (int.Parse(number.Text) - 1).ToString(); }
    
    public void SetMediator(PlaceMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// If received, must cancel turn change.
    /// </summary>
    /// <param name="sender"></param>
    public void Notify(IPlaceMediator sender)
    {
        if (sender is PlaceMediator)
        {
            Previous();
        }
    }

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
