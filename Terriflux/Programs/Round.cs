using Godot;
using System;
using Terriflux.Programs;
using static System.Net.Mime.MediaTypeNames;

public partial class Round : RawNode, IRound
{
    private Button _number;
    private PlaceMediator mediator;

    private const int MAX_NB_TURN = 10;

    private int number;

    public Round() { this.number = 1; }

    public int GetNumber()
    {
        return int.Parse(_number.Text);
    }

    public void Next()
    {
        int next = number + 1;

        // victory?
        if (next >= MAX_NB_TURN)
        {
            End.Victory();
        }
        number++;

        if (mediator == null) throw new Exception("Invalid Round configuration");
        mediator.Notify(this);
    }
    
    private void Previous() { number--; }
    
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
        Previous();
    }

    public override void _Ready()
	{
		base._Ready();

        _number = GetNode<Button>("Number");

	}

    public override void _Process(double delta)
    {
        base._Process(delta);

        _number.Text = this.number.ToString();
    }

    private void OnPressed()
    {
        Next();
    }
}
