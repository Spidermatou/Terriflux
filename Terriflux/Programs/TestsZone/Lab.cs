using Godot;
using System;
using Terriflux.Programs.Model;
using Terriflux.Programs.TestsZone;
using Terriflux.Programs.View;

namespace Terriflux.Programs.TestsZone
{
    public partial class Lab : Node2D
    {
        private Marker2D _bigObjectSartPoint;

        public override void _Ready()
        {
            // child
            _bigObjectSartPoint = GetNode<Marker2D>("BigObjectSartPoint");

            TestsProvider tp = new(this);

            // Models
            GD.Print("--Test cell model--");
            tp.TCellModel();

            GD.Print("--Test grass model--");
            tp.TGrassModel();

            GD.Print("--Test building part model--");
            tp.TBuildingPart();

            GD.Print("--Test building model--");
            tp.TBuildingModel();

            GD.Print("--Test grid model--");
            tp.GridModel();

            // Views
            GD.Print("--Test cell view--");
            tp.TCellView();

            GD.Print("--Test grass view--");
            tp.TGrassView();

            GD.Print("--Test building part view--");
            tp.TBuildingPartView();

            GD.Print("--Test building view--");
            tp.TBuildingView();

            // Factories
            GD.Print("--Test building factory--");
            tp.TBuildingFactory();

            GD.Print("--Test building factory - version: no texture provided--");
            tp.TBuildingFactory_WithUnprovidedTexture();

            GD.Print("--Test grid factory - version: grass only--");
            tp.TGridFactory_GrassOnly(this._bigObjectSartPoint.Position);

            GD.Print("--Test grid factory - version: buildings classic--");
            tp.TGridFactory_WithBuildings(this._bigObjectSartPoint.Position, true);
            
        }
    }
}