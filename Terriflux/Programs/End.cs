using Godot;
using System;
using Terriflux.Programs;

public partial class End : RawNode
{
    private Sprite2D _background;
    private Button _exit;

    // himself
    private static readonly End singleton = (End)RawNode.Instantiate("End");

    private End() : base() { }

    public override void _Ready()
	{
		base._Ready();
		_background = GetNode<Sprite2D>("Background");
        _exit = GetNode<Button>("ExitGame");
        this.Hide();
        this.ZIndex = 110;
    }

    public static End GetInstance()
    {
        return singleton;
    }

    public static void Victory()
    {
        Alert.Say("Felicitations ! Vous avez reussi a equilibrer l'ecologie, l'economie et le social. " +
            "La region prospere grace a vos efforts acharnes. Vous etes un veritable champion de la reterritorialisation, " +
            "demontrant que l'harmonie entre ces trois piliers est la cle d'un avenir durable.");
        singleton.Show();
        singleton._background.Texture = GD.Load<Texture2D>(PATH_IMAGES + "victory.png");
        singleton._background.ZIndex = 1;
        singleton._exit.Show();
    }

    public static void Fail()
    {
        Alert.Say("Votre quete pour la reterritorialisation a pris fin. Bien que vous ayez eu un impact significatif sur la region, " +
            "la situation est critique. L'un des piliers essentiels - l'ecologie, l'economie ou le social - est maintenant en peril, " +
            "mettant en danger l'equilibre que vous avez tente de maintenir. Utilisez cette experience pour reflechir a de nouvelles " +
            "strategies, car meme des pas modestes dans la bonne direction peuvent avoir un effet profond et durable. C'est une lecon " +
            "precieuse pour vos futures entreprises. Continuez a batir sur ces fondations et a inspirer le changement positif.");
        singleton.Show();
        singleton._background.Texture = GD.Load<Texture2D>(PATH_IMAGES + "fail.png");
        singleton._background.ZIndex = 1;
        singleton._exit.Show();
    }

    private void OnExitPressed()
    {
        GetTree().Quit();
    }


}
