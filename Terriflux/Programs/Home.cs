using Godot;
using System;
using System.Linq;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model;
using Terriflux.Programs.View;

namespace Terriflux.Programs
{
    /// <summary>
    /// Start the game
    /// </summary>
    public partial class Home : Node2D
    {
        private Home() { }

        public override void _Ready()
        {
            Test_ImageToolsProvider_OnGrid();
            
            //Test_Cell();
            
            //Test_BuildGeneration();
        }

        // TODO - a class to regroup tests (TestsProvider)

        private void Test_ImageToolsProvider()
        {
            string path = "Ressources/Textures/grass.png";


            GD.Print($"--Test_ImageToolsProvider OnGrid--");
            GD.Print($"Actual parent scene child count: {this.GetChildren().Count}");

            Texture2D texture = ImageToolsProvider.LoadTexture(path);
            GD.Print($"Texture loaded? {texture != null}");

            // create image to show the slice-texture's results
            const int CUT_WANTED = 2;
            int gap = 1;
            Texture2D[] slicedTextureParts = ImageToolsProvider.SliceImageTexture(path, CUT_WANTED);
            GD.Print($"Number of parts provided: {slicedTextureParts.Length}, awaited: {CUT_WANTED}.");

            // show on screen
            foreach (Texture2D individualTexture in slicedTextureParts)
            {
                Sprite2D sprite = new();
                sprite.Texture = individualTexture;

                CellModel cm = new("Field", CellKind.BUILDING);
                CellView cv = CellView.Design();
                cm.AddObserver(cv);
                cm.SetDimensions(128 / 2, 1);
                cm.SetPlacement(new Vector2(gap, 0));
                this.AddChild(cv);

                cv.ChangeSkin(sprite);
                gap += cm.GetSize();
            }

            GD.Print($"New parent scene child count: {this.GetChildren().Count}");
        }

        private void Test_ImageToolsProvider_OnGrid()
        {
            string path = "Ressources/Textures/grass.png";
            GridModel gridModel = new(10, true);
            GridView gridView = GridView.Design();
            gridModel.SetObserver(gridView);
            this.AddChild(gridView);

            GD.Print($"--Test_ImageToolsProvider OnGrid--");
            GD.Print($"Actual parent scene child count: {this.GetChildren().Count}");

            Texture2D texture = ImageToolsProvider.LoadTexture(path);
            GD.Print($"Texture loaded? {texture != null}");

            // create image to show the slice-texture's results
            const int CUT_WANTED = 2;
            int gap = 1;
            Texture2D[] slicedTextureParts = ImageToolsProvider.SliceImageTexture(path, CUT_WANTED);
            GD.Print($"Number of parts provided: {slicedTextureParts.Length}, awaited: {CUT_WANTED}.");

            // show on screen
            foreach (Texture2D individualTexture in slicedTextureParts)
            {
                Sprite2D sprite = new();
                sprite.Texture = individualTexture;

                CellModel cm = new("Field", CellKind.BUILDING);
                cm.SetDimensions(128/2, 1);
                cm.SetPlacement(new Vector2(gap, 0));
                gridModel.SetAt(cm, gap, 0, true);
                gap++;
            }

           

            GD.Print($"New parent scene child count: {this.GetChildren().Count}");
        }

        private void Test_Cell()
        {
            string path = Paths.TEXTURES + "field.png";
            int CUT = 2;

            // via path
            CellModel model = new("field", CellKind.BUILDING);
            CellView view = CellView.Design();  // with texture path
            this.AddChild(view);
            model.AddObserver(view);
            view.ChangeSkin(path);
            model.SetCellName("via path");

            // via load and cut
            CellModel model2 = new("field", CellKind.BUILDING);
            CellView view2 = CellView.Design();  // with texture path
            this.AddChild(view2);
            model2.AddObserver(view2);
            Texture2D[] textures = ImageToolsProvider.SliceImageTexture(path, CUT);
            Texture2D littleBit = textures[1];
            Sprite2D sprite = new Sprite2D();

            sprite.Name = "just the sprite";
            sprite.Position = new Vector2(184, -96);
            this.AddChild(sprite);
            GD.PrintErr(textures.Length);


            sprite.Texture = littleBit;
            view2.ChangeSkin(sprite);
            model2.SetCellName("via load and cut");
            view2.Position = new Vector2(-288, -72);
        }

        private void Test_Grid()
        {
            // test grid
            GridModel grid_test = GridFactory.CreateFullGrassLand(10);
            GridView territory_view = GridView.Design();
            this.AddChild(territory_view);
            grid_test.SetObserver(territory_view);
            territory_view.UpdateMap(grid_test);
        }

        private void Test_EnumTranslation() 
        {
            // enum test
            GD.Print("--Enum test--");
            string what = "wAteR";
            what = what.Capitalize();
            GD.Print(GlobalTools.TranslateToFlowKind(what));

        }

        private void Test_BuildGeneration()
        {
            GD.Print("--Build generation test--");
            
            BuildingModel model = BuildingModel.CreateFromName("fIElD");
            BuildingView.Design(this, model);


            // hierarchy
            GD.Print("this children:" + this.GetChildren().Count);
            foreach (Node2D child in this.GetChildren())
            {
                GD.Print($"1- {child} '{child.Name}'");
                foreach (Node subChild in child.GetChildren())
                {
                    GD.Print($"\t2- {subChild} '{subChild.Name}'");
                }
            }
        }

        private void Test_PlaceBuilding()
        {
            // Game basis creation
            GridModel grid = new(5);
            GridView view = GridView.Design();
            grid.SetObserver(view);
            this.AddChild(view);


            // Building creation
            BuildingModel bmodel = BuildingModel.CreateFromName("field");
            grid.PlaceAt(bmodel, new Vector2I(1,1), true);

        }
    }
}
