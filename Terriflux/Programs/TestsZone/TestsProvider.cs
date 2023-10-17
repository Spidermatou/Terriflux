﻿using Godot;
using System.Collections.Generic;
using System.Text;
using Terriflux.Programs.Data.Management;
using Terriflux.Programs.Exceptions;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model;
using Terriflux.Programs.View;

namespace Terriflux.Programs.TestsZone
{
    public partial class TestsProvider
    {
        private readonly Node scene;

        public TestsProvider(Node scene)
        {
            this.scene = scene;
        }

        private void PrintChildrenCount()
        {
            StringBuilder sb = new();
            List<Node> children = new(scene.GetChildren());

            sb.Append(children[0].ToString());
            for (int i = 1; i < scene.GetChildren().Count; i++)
            {
                sb.Append(',')
                  .Append(children[i].ToString());
            }

            GD.Print($"Nb of children in this scene: {scene.GetChildren().Count} ({sb}).");
        }

        // Models
        public void TCellModel()
        {
            CellModel cm = new("Default", CellKind.WASTELAND);
            GD.Print($"Null? {cm == null}");
            GD.Print($"Attributes (2): ");
            GD.Print(cm.GetName());
            GD.Print(cm.GetKind());
        }

        public void TGrassModel()
        {
            GrassModel grass = new();
            GD.Print($"Null? {grass == null}");
            GD.Print($"Attributes (2): ");
            GD.Print(grass.GetName());
            GD.Print(grass.GetKind());
        }


        public void TBuildingModel()
        {
            // attributes
            string name = "Fork Factory";
            double[] impacts = new double[3] { 12.4, 23.0, 11.8 };
            InfluenceScale influence = InfluenceScale.NATIONAL;
            Dictionary<FlowKind, int> needs = new();
            needs.Add(FlowKind.Water, 3);
            needs.Add(FlowKind.Electricity, 7);
            needs.Add(FlowKind.Raw_materials, 4);
            Dictionary<FlowKind, int> productions = new();
            productions.Add(FlowKind.Merchandise_manufactured, 5);

            // create the building himself
            BuildingModel bm = new(name, impacts, influence, needs, productions);

            // print
            GD.Print($"Null? {bm == null}");
            GD.Print($"Attributes (6): ");
            GD.Print(bm.GetName());
            GD.Print(bm.GetInfluence());
            // needs
            GD.Print("Needs-");
            foreach (FlowKind flow in bm.GetNeedsKind())
            {
                GD.Print($"\t*Flow:{flow}, Qty:{bm.GetQuantityNeeded(flow)}");
            }
            // products
            GD.Print("Products-");
            foreach (FlowKind flow in bm.GetProductionKind())
            {
                GD.Print($"\t*Flow:{flow}, Qty:{bm.GetQuantityProduced(flow)}");
            }
        }

        public void GridModel()
        {
            int wantedSize = 10;
            GridModel grid = new(wantedSize);

            GD.Print($">> Grid full of grass' part <<");
            GD.Print($"Is null? {grid == null}");
            GD.Print($"Size: {grid.GetSize()}, expected {wantedSize}");

            for (int line = 0; line < grid.GetSize(); line++)
            {
                for (int column = 0; column < grid.GetSize(); column++)
                {
                    grid.PlaceAt(new GrassModel(), line, column);
                    GD.Print(grid.GetAt(line, column));
                }
            }

            // self
            GD.Print(grid.Verbose());

            // placeable
            string name = "Fork Factory";
            double[] impacts = new double[3] { 12.4, 23.0, 11.8 };
            InfluenceScale influence = InfluenceScale.NATIONAL;
            Dictionary<FlowKind, int> needs = new();
            needs.Add(FlowKind.Water, 3);
            needs.Add(FlowKind.Electricity, 7);
            needs.Add(FlowKind.Raw_materials, 4);
            Dictionary<FlowKind, int> productions = new();
            productions.Add(FlowKind.Merchandise_manufactured, 5);
            Vector2I coordinatesBuilding1 = new(2, 4);
            Vector2I coordinatesBuilding2 = new(6, 3);

            GD.Print($">> Grid with placeables part <<");
            grid.PlaceAt(new BuildingModel(name, impacts, influence, needs, productions),
                coordinatesBuilding1.X, coordinatesBuilding1.Y);    // add building in 2,4 (horizontal)
            grid.PlaceAt(new BuildingModel(name, impacts, influence, needs, productions),
                6, 3);        // add building in 6,3 (vertical)
            GD.Print($"Is a placeable in {coordinatesBuilding1.X},{coordinatesBuilding1.Y} and not null? " +
                $"{grid.GetAt(coordinatesBuilding1.X, coordinatesBuilding1.Y) == null}");
            GD.Print($"Is a placeable in {coordinatesBuilding2.X},{coordinatesBuilding2.Y} and not null? " +
                $"{grid.GetAt(coordinatesBuilding2.X, coordinatesBuilding2.Y) == null}");
            GD.Print($"What's repertoried (placeables)?");
            foreach (KeyValuePair<Vector2I, IPlaceable> kvp in grid.GetAllPlacements())
            {
                GD.Print($"Key=({kvp.Key.X},{kvp.Key.Y}) and Value={nameof(kvp.Value)}.");
            }
        }

        // Views
        public void TCellView(bool print = false)
        {
            CellView view = CellView.Design();
            scene.AddChild(view);
            PrintChildrenCount();

            if (print == false)
            {
                view.Hide();
            }
        }

        public void TGrassView(bool print = false)
        {
            GrassView view = GrassView.Design();
            scene.AddChild(view);
            PrintChildrenCount();

            if (print == false)
            {
                view.Hide();
            }
        }

        public void TBuildingView(bool print = false)
        {
            const string TNAME = "Default";
            string TPATH = OurPaths.TEXTURES + "default.png";

            // create the model
            BuildingModel tmodel = new(TNAME,
               new double[3],
               InfluenceScale.REGIONAL,
               new Dictionary<FlowKind, int>(),
               new Dictionary<FlowKind, int>());

            // create the view
            Texture2D texture = DataManager.LoadTexture(TPATH);
            BuildingView view = BuildingView.Design(tmodel, texture);

            // add to scene
            scene.AddChild(view);

            // print, etc
            PrintChildrenCount();

            if (print == false)
            {
                view.Hide();
            }
        }

        // Factories   
        public void TBuildingFactory(bool print = false)
        {
            BuildingModel model = BuildingFactory.LoadModel("field", InfluenceScale.NATIONAL);

            GD.Print($">> Building model part <<");
            GD.Print(model.Verbose());

            GD.Print($">> Grid with placeables part <<");
            BuildingView view = BuildingFactory.CreateView(model);
            scene.AddChild(view);
            PrintChildrenCount();

            if (print == false)
            {
                view.Hide();
            }
        }

        public void TBuildingFactory_WithUnprovidedTexture(bool print = false)
        {
            BuildingModel model = BuildingFactory.LoadModel("fork factory", InfluenceScale.NATIONAL);

            GD.Print($">> Building model part <<");
            GD.Print(model.Verbose());

            GD.Print($">> Grid with placeables part <<");
            BuildingView view = BuildingFactory.CreateView(model);
            scene.AddChild(view);
            PrintChildrenCount();

            if (print == false)
            {
                view.Hide();
            }
        }


        public void TGridFactory_GrassOnly(Vector2 startPosition, bool print = false)
        {
            GridModel model = GridFactory.CreateFullGrassLand(5);

            GD.Print($">> A grid full grass <<");
            GD.Print(model.Verbose());

            GD.Print($">> Grid with placeables part <<");
            GridView view = GridFactory.CreateGridView(model);
            view.Position = startPosition;
            scene.AddChild(view);

            if (print == false)
            {
                view.Hide();
            }
        }

        public void TGridFactory_WithBuildings(Vector2 startPosition, bool print = false)
        {
            GridModel model = GridFactory.CreateFullGrassLand(10);

            GD.Print($">> A grid with buildings <<");
            BuildingModel tbuildingModel1 = BuildingFactory.LoadModel("fork factory", InfluenceScale.NATIONAL);
            BuildingModel tbuildingModel2 = BuildingFactory.LoadModel("field", InfluenceScale.REGIONAL);
            model.PlaceAt(tbuildingModel1, 3, 5);
            model.PlaceAt(tbuildingModel2, 0, 0);
            GD.Print(model.Verbose());

            GD.Print($">> Grid with placeables part <<");
            GridView view = GridFactory.CreateGridView(model);
            view.Position = startPosition;
            view.Scale = new Vector2((float)0.5, (float)0.5);
            scene.AddChild(view);

            if (print == false)
            {
                view.Hide();
            }
        }

        public void TGridFactory_WithConflicts(Vector2 startPosition, bool print = false)
        {
            GridModel model = GridFactory.CreateFullGrassLand(5);

            GD.Print($">> A grid with buildings (and conflicts) <<");
            BuildingModel tbuildingModel = BuildingFactory.LoadModel("field", InfluenceScale.REGIONAL);
            model.PlaceAt(tbuildingModel, 0, 0);

            try { model.PlaceAt(tbuildingModel, 0, 0); } // try to place at the same case
            catch (UnplaceableException e) { GD.Print($"CATCHED: {e.Message}"); }

            GD.Print(model.Verbose());

            GD.Print($">> Grid with placeables part (and conflicts)  <<");
            GridView view = GridFactory.CreateGridView(model);
            view.Position = startPosition;
            scene.AddChild(view);

            if (print == false)
            {
                view.Hide();
            }
        }
    }
}