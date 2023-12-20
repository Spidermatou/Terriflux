using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terriflux.Programs;

namespace Terriflux.Programs;
/// <summary>
/// Abstract node
/// </summary>
public partial class Building : Cell
{
    protected static readonly double[] maluses = new double[]{-1, -1, -1};    // maluses if inactive

    protected readonly double[] impacts;
    protected readonly Dictionary<FlowKind, int> needs;
    protected readonly Dictionary<FlowKind, int> minimalProduction;

    // children
    protected Color colorOfDot; 
    protected Sprite2D _buildingSprite;
    protected Polygon2D _dot;

    public Building(double[] impacts, Dictionary<FlowKind, int> needs, Dictionary<FlowKind, int> minimalProduction, Color colorOfDot) : base()
    {
        if (impacts.Length != 3) throw new ArgumentException(nameof(impacts));

        this.impacts = impacts;
        this.needs = needs;
        this.minimalProduction = minimalProduction;

        this.colorOfDot = colorOfDot;
    }

    public override void _Ready()
    {
        base._Ready();
        _buildingSprite = GetNode<Sprite2D>("BuildingSprite");
        this._buildingSprite.Texture = GD.Load<Texture2D>(PATH_IMAGES + GetType().Name.ToLower() + ".png");
        _dot = GetNode<Polygon2D>("Dot");
        this._dot.Color = colorOfDot;
    }

    public double[] GetMaluses()
    {
        return maluses.ToArray();
    }

    public double[] GetImpacts()
    {
        return impacts.ToArray();
    }

    public FlowKind[] GetNeeds()
    {
        return this.needs.Keys.ToArray();
    }
    
    public FlowKind[] GetProduction()
    {
        return this.minimalProduction.Keys.ToArray();
    }

    public int GetNeedOf(FlowKind flow)
    {
        return this.needs[flow];
    }
    
    /// <param name="flow"></param>
    /// <returns>Minimal production of </returns>
    public int GetProductOf(FlowKind flow)
    {
        return this.needs[flow];
    }

    public string GetHelp()
    {
        StringBuilder sb = new();
        // impacts
        sb.Append("Impacts: ");
        foreach (int impact in impacts)
        {
            sb.Append($"{impact} ");
        }
        // needs
        sb.AppendLine("Needs: ");
        foreach (KeyValuePair<FlowKind, int> kvp in needs)
        {
            sb.AppendLine("\t" + kvp.Key.ToString() + ": " + kvp.Value.ToString());
        }
        // product
        sb.AppendLine("Products (minimal): ");
        foreach (KeyValuePair<FlowKind, int> kvp in minimalProduction)
        {
            sb.AppendLine("\t" + kvp.Key.ToString() + ": " + kvp.Value.ToString());
        }
        return sb.ToString();
    }

    public override string Verbose()
    {
        return base.Verbose() + "\n" + GetHelp();
    }
}