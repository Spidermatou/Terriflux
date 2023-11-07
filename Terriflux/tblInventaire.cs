using Godot;
using System;

public partial class tblInventaire : PanelContainer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		private var fluxEau = GetNode<Label>("fluxEau");
		private var fluxBois = GetNode<Label>("fluxBois");
		private var fluxCereales = GetNode<Label>("fluxCereales");
		private var fluxMedicaments = GetNode<Label>("fluxMedicaments");
		private var fluxEnergie = GetNode<Label>("fluxEnergie");
		private var fluxPain = GetNode<Label>("fluxPain");
		private var fluxMarchandise = GetNode<Label>("fluxMarchandise");
		private var fluxMatieresPrem = GetNode<Label>("fluxMatieresPrem");
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
