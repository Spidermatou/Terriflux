using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terriflux.Programs;

/// <summary>
/// Abstract node
/// </summary>
public partial class Building : Cell
{
    private static readonly double[] maluses = new double[]{-1, -1, -1};    // maluses if inactive

    private readonly double[] impacts;
    private readonly Dictionary<FlowKind, int> needs;
    private readonly Dictionary<FlowKind, int> minimalProduction;

    private readonly Color dotColor;

    // children
    private Sprite2D _buildingSprite;
    private Polygon2D _dot;

    public Building(Texture2D texture, double[] impacts, Dictionary<FlowKind, int> needs, Dictionary<FlowKind, int> minimalProduction, Color colorOfDot) 
    {
        if (impacts.Length != 3) throw new ArgumentException(nameof(impacts));

        this._buildingSprite.Texture = texture;   
        this.dotColor = colorOfDot;

        this.impacts = impacts;
        this.needs = needs;
        this.minimalProduction = minimalProduction;
    }


    public override void _Ready()
    {
        base._Ready();
        _buildingSprite = GetNode<Sprite2D>("BuildingSprite");
        _dot = GetNode<Polygon2D>("Dot");
        _dot.Color = dotColor;
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