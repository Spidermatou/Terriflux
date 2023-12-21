using Godot;
using System;
using Terriflux.Programs;

public partial class Round : RawNode, IRound, IPlaceMediator
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
        int next = (int.Parse(number.Text) + 1);

        // victory?
        if (next >= MAX_NB_TURN)
        {
            Alert alert = (Alert) Instantiate("Alert");
            this.AddChild(alert);
            alert.Say("Victoire ! Vous avez su gerer votre territoire malgre les defis. " +
                "Votre ville est prospere et attractive, sans etre trop polluante. Bien joue !!");
        }
        number.Text = next.ToString();

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
