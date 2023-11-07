using Godot;
using System;
using System.Threading.Tasks;

public partial class Home : StandardWindow
{
    private Label _loginLabel;
    private Label _passwordLabel;

    private bool loginAnim_finished = false;
    private bool loginAnim_working = false;
    private bool passwordAnim_finished = false;
    private bool passwordAnim_working = false;

    public Home() { }

    public override void _Ready()
    {
        base._Ready();

        _loginLabel = GetNode<Label>("LoginBackground/LoginType");
        _passwordLabel = GetNode<Label>("PasswordBackground/PasswordType");
    }

    public override void _Process(double delta)
    {
        if (!loginAnim_finished && !loginAnim_working)
        {
            LoginAnim();
            loginAnim_working = true;
        }
        else if (loginAnim_finished && !passwordAnim_finished && !passwordAnim_working)
        {
            PasswordAnim();
            passwordAnim_working = true;
        }

    }

    private async void LoginAnim()
    {
        _loginLabel.Text = "M";

        await Task.Delay(200); // Attendre 0.2 secondes

        _loginLabel.Text += "a";
        await Task.Delay(200);
        _loginLabel.Text += "y";
        await Task.Delay(200);
        _loginLabel.Text += "o";
        await Task.Delay(200);
        _loginLabel.Text += "r";
        await Task.Delay(200);
        _loginLabel.Text += " x";
        await Task.Delay(200);
        _loginLabel.Text += "x";
        await Task.Delay(200);
        _loginLabel.Text += "x";

        this.loginAnim_finished = true;
    }

    private async void PasswordAnim()
    {
        for (int i = 0; i < 5; i++)
        {
            _passwordLabel.Text += "*";
            await Task.Delay(200);
        }

        this.passwordAnim_finished = true;
    }


    private void OnStartPressed()
    {

    }

    private void OnExitGamePressed()
    {

    }


    
}
