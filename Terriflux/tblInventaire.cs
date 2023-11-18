using Godot;
using System;
using Terriflux.Programs.Model.Placeables;
using System.Collections.Generic;

public partial class tblInventaire : VBoxContainer
{
	private Label fluxEau;
	private Label fluxBois;
	private Label fluxCereales;
	private Label fluxEnergie;
	private Label fluxPain;
	private Label fluxMarchandise;
	private Label fluxMatieresPrem;
	
	private Sprite2D variation1;
	private Sprite2D variation2;
	private Sprite2D variation3;
	private Sprite2D variation4;
	private Sprite2D variation5;
	private Sprite2D variation6;
	private Sprite2D variation7;
	private Sprite2D variation8;
	
	Dictionary <string,int> quantFlux = new Dictionary <string,int>();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		fluxEau = GetNode<Label>("fluxEau");
		fluxBois = GetNode<Label>("fluxBois");
		fluxCereales = GetNode<Label>("fluxCereales");
		fluxEnergie = GetNode<Label>("fluxEnergie");
		fluxPain = GetNode<Label>("fluxPain");
		fluxMarchandise = GetNode<Label>("fluxMarchandise");
		fluxMatieresPrem = GetNode<Label>("fluxMatieresPrem");
		
		variation1 = GetNode<Sprite2D>("fluxEau/Var1");
		variation2 = GetNode<Sprite2D>("fluxBois/Var2");
		variation3 = GetNode<Sprite2D>("fluxPain/Var3");
		variation4 = GetNode<Sprite2D>("fluxEnergie/Var4");
		variation5 = GetNode<Sprite2D>("fluxCereales/Var5");
		variation6 = GetNode<Sprite2D>("fluxMarchandise/Var6");
		variation7 = GetNode<Sprite2D>("fluxMatieresPrem/Var7");
		
		foreach(Label lbl in this.GetChildren()){
			quantFlux.Add(lbl.Text,0);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionPressed("ui_right"))
		{
			//variation1.Scale = new Vector2((float)0.5,(float)0.5);
			variation1.Texture = (Texture2D)GD.Load<Texture>("res://Ressources/Textures/up.png");
		}
	}
	
	public void _Show(){
		//this.Show();
		this.Visible=true;
	}
	
	public void _Hide(){
		//this.Hide();
		this.Visible=false;
	}
	
	public void Add(FlowKind flow, int amount){
		//this.GetChildren()[amount].Show();
		quantFlux[flow.ToString()] += amount;
	}
	
	public void Remove(FlowKind flow, int amount){
		//this.GetChildren()[amount].Hide();
		quantFlux[flow.ToString()] -= amount;
	}
	
	public bool Contains(FlowKind flow){
		foreach(Label lbl in this.GetChildren()){
			if(lbl.Text.ToUpper() == flow.ToString().ToUpper()){
				return true;
			}
		}
		return false;
	}
	
	public bool ContainsEnough(FlowKind flow){
		if (quantFlux[flow.ToString()] > 0)
		{
			return true;
		}
		return false;
	}
}
