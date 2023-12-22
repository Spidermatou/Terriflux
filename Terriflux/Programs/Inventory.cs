using Godot;
using System;
using System.Collections.Generic;


namespace Terriflux.Programs;
public partial class Inventory : RawNode, IInventory
{
	// prices
	private static readonly Dictionary<FlowKind, double> PRICES = new()		// per unity
	{
		{FlowKind.Water, 1.0},
		{FlowKind.Energy, 0.15},
		{FlowKind.Cereal, 4.0},
		{FlowKind.Bread, 20.0},
		{FlowKind.Composant, 6.0},
		{FlowKind.Machine, 100.0}
	};

    // quantities management
    private readonly Dictionary<FlowKind, int> quantitiesProduced = new();    // all quantities produced in one turn
	private double money;

    // children
    private readonly Dictionary<FlowKind, Sprite2D> _arrows = new(); // quantities arrows visible for user
	private readonly Dictionary<FlowKind, Label> _qtyLabels = new();    // quantities label visible for user
    private readonly Dictionary<FlowKind, LineEdit> _importLines = new();
    private readonly Dictionary<FlowKind, LineEdit> _exportLines = new();
    private readonly Dictionary<string, Label> _titles = new();

    // static textures
    private static readonly Texture2D _constArrow = GD.Load<Texture2D>(PATH_IMAGES + "const.png");
	private static readonly Texture2D _upArrow = GD.Load<Texture2D>(PATH_IMAGES + "up.png");
	private static readonly Texture2D _downArrow = GD.Load<Texture2D>(PATH_IMAGES + "down.png");

    // CONSTRUCT
    public Inventory() { this.money = 250; }

    // GODOT
    public override void _Ready()
    {
		// get children
		foreach (Node child in GetChildren())
		{
			if (child is Label label)
			{
				this._titles.Add(label.Name, label);
			}
		}

		List<FlowKind> allFlows = new();
		allFlows.AddRange((IEnumerable<FlowKind>)Enum.GetValues(typeof(FlowKind)));

		// design
		const int GAP = 55; //px
		Vector2I VECGAP;
        Vector2I RIGHTGAP = new(20, 0);
        Vector2 ARROWGAP = new((float)25, (float)28);
        Vector2 LINEGAP = new((float)0, (float)10);
        Vector2 QTYGAP = new((float)0, (float)5);
        Vector2 LABSCALE = new((float)1.5, (float)1.5);
        Vector2 LINESCALE = new((float)1.2, (float)1.2);
		Vector2 ARROWSCALE = new((float)1, (float)1);
		const string LABFONT_NAME = "Born2bSportyV2";
        Font LABFONT = GD.Load<Font>(PATH_FONT + LABFONT_NAME + ".ttf");

        for (int i = 0 ; i < allFlows.Count; i++)
        {
            VECGAP = new(0, ((i + 1) * GAP));

            // add reference for production
            this.quantitiesProduced.Add(allFlows[i], 0);

			// add type labels
			Label type = new();
			type.Position = _titles["Type"].Position + VECGAP; // place below his title
			type.Text = allFlows[i].ToString();
            type.Modulate = Color.FromHtml("#010E12");
			type.Scale = LABSCALE;
			type.HorizontalAlignment = HorizontalAlignment.Center;
			type.AddThemeFontOverride(LABFONT_NAME, LABFONT);
			this.AddChild(type);

            // add references for arrows
            Sprite2D arrow = new();
			arrow.Position = _titles["Variations"].Position + VECGAP + ARROWGAP; // place below his title
            arrow.Texture = _constArrow;
			arrow.Scale = ARROWSCALE;
            this.AddChild(arrow);
			this._arrows.Add(allFlows[i], arrow);

            // add references for quantities
            Label qty = new();
            qty.Position = _titles["Qte"].Position + VECGAP + RIGHTGAP + QTYGAP; // place below his title
            qty.Text = "0";
            qty.Modulate = Color.FromHtml("#010E12");
            qty.Scale = LABSCALE;
            qty.AddThemeFontOverride(LABFONT_NAME, LABFONT);
            this.AddChild(qty);
            this._qtyLabels.Add(allFlows[i], qty);

            // add references for import
            LineEdit import = new();
            import.Position = _titles["Import"].Position + VECGAP + LINEGAP; // place below his title
			import.Text = "0";
            import.Modulate = Color.FromHtml("#010E12");
            import.Scale = LINESCALE;
            import.TextChanged += LineEditTextChanged;
            import.AddThemeFontOverride(LABFONT_NAME, LABFONT);
            this.AddChild(import);
            this._importLines.Add(allFlows[i], import);

            // add references for export
            LineEdit export = new();
            export.Position = _titles["Export"].Position + VECGAP + LINEGAP; // place below his title
            export.Text = "0";
			export.Scale = LINESCALE;
            export.Modulate = Color.FromHtml("#010E12");
            export.TextChanged += LineEditTextChanged;
            export.AddThemeFontOverride(LABFONT_NAME, LABFONT);
            this.AddChild(export);
            this._exportLines.Add(allFlows[i], export);
        }
    }

	public void Add(FlowKind flow, int amount)
	{
		quantitiesProduced[flow] += amount;

		// update hud
		UpdateDisplay(flow);
	}

	public void Remove(FlowKind flow, int amount)
	{
		quantitiesProduced[flow] -= amount;

		// update hud
		UpdateDisplay(flow);
	}

	public bool Contains(FlowKind flow)
	{
		return quantitiesProduced.ContainsKey(flow);
	}

    public int GetQuantityOf(FlowKind flow)
    {
        return this.quantitiesProduced[flow];
    }
    
	private void UpdateDisplay(FlowKind flow)
	{
		int variation = this.quantitiesProduced[flow];
        Vector2 CONSTCALE = new((float)1, (float)1);
        Vector2 ARROWSCALE = new((float)0.5, (float)0.5);


        // overproduction?
        if (variation > 0)
		{
			this._arrows[flow].Texture = _upArrow; // arrow to up
            this._arrows[flow].Scale = ARROWSCALE;
            this._qtyLabels[flow].Text = variation.ToString();
			this._qtyLabels[flow].Modulate = Color.FromHtml("#229954");  
        }
		// perfect balance between product and necessity?
		else if (variation == 0)
		{
			this._arrows[flow].Texture = _constArrow; // vertical bar
            this._arrows[flow].Scale = CONSTCALE;
            this._qtyLabels[flow].Text = "0";
			this._qtyLabels[flow].Modulate = Color.FromHtml("#010E12");
        }
        // underproduction?
        else
		{
			this._arrows[flow].Texture = _downArrow; //  arrow to down
            this._arrows[flow].Scale = ARROWSCALE;
            this._qtyLabels[flow].Text = variation.ToString();
            this._qtyLabels[flow].Modulate = Color.FromHtml("#E74C3C"); 
		}
	}

    /// <summary>
    /// Verify that the player have enough _money and qty to import/export everything he wants
    /// </summary>
    /// <returns>True if transaction a is possible, false otherwise (not yet performed!).</returns>
    public bool TryImportExport()
	{
        // verify quantities
        foreach (KeyValuePair<FlowKind,LineEdit> export in _exportLines)
        {
			// player want to export more than he have?
			if (GetQuantityOf(export.Key) <int.Parse(export.Value.Text))
            {
                Alert.Say("Vous tentez d'exporter des ressources que vous ne possedez pas !");
                return false;
            }
        }

        // price calculation
        double totalPrice = 0;
		foreach (KeyValuePair<FlowKind, LineEdit> import in _importLines)
		{
			totalPrice -= PRICES[import.Key] * int.Parse(import.Value.Text);
		}

        foreach (KeyValuePair<FlowKind, LineEdit> export in _exportLines)
        {
            totalPrice += PRICES[export.Key] * int.Parse(export.Value.Text);
        }

        // enough _money?
        bool isNotOk = money + totalPrice < 0;
        if (isNotOk)
        {
            Alert.Say("Vous n'avez pas assez d'argent pour importer tout ca !");
        }
        else
        {
            money += totalPrice;
            ApplyImportExport();
        }
        
        return !isNotOk;
    }

    public double GetMoney()
    {
        return money;
    }

	private void ApplyImportExport()
	{
        foreach (KeyValuePair<FlowKind, LineEdit> import in _importLines)
        {
            this.Add(import.Key, int.Parse(import.Value.Text));
        }

        foreach (KeyValuePair<FlowKind, LineEdit> export in _exportLines)
        {
            this.Remove(export.Key, int.Parse(export.Value.Text));
        }

        ResetImportExport();
    }

    private void ResetImportExport()
    {
        foreach (LineEdit import in _importLines.Values)
        {
            import.Text = "0";
        }

        foreach (LineEdit export in _exportLines.Values)
        {
            export.Text = "0";
        }
    }

    private void LineEditTextChanged(string new_text)
    {
        new_text = new_text.Trim();
        foreach (Char chara in new_text)
        {
            if (!Char.IsNumber(chara))
            {
                Alert.Say("Valeur invalide !");
                ResetImportExport();
            }
        }
    }
}

