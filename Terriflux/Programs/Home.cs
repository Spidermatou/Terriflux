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
            Test_ImageToolsProvider();
            
            //Test_Cell();
            
            //Test_BuildGeneration();
        }

        private void Test_ImageToolsProvider()
        {
            GD.Print($"--Test_ImageToolsProvider--");
            GD.Print($"Actual parent scene child count: {this.GetChildren().Count}");

            Texture2D texture = ImageToolsProvider.LoadTexture("Ressources/Textures/grass.png"); 
            GD.Print($"Texture loaded? {texture != null}");
            Sprite2D origine = new();
            origine.Texture = texture;
            origine.Position = new Vector2(-100, -100);
            this.AddChild(origine );

            // create image to show the slice-texture's results
            const int CUT_WANTED = 2;
            int gap = 1;
            Texture2D[] slicedTextureParts = ImageToolsProvider.SliceImage(texture, CUT_WANTED);
            GD.Print($"Number of parts provided: {slicedTextureParts.Length}, waited: {CUT_WANTED}.");
            foreach (Texture2D individualTexture in slicedTextureParts)
            {
                Sprite2D sprite = new();
                sprite.Texture = individualTexture;
                sprite.Position = new Vector2(100 *  gap, 100 * gap);
                this.AddChild(sprite);
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
            Texture2D[] textures = ImageToolsProvider.SliceImage(ImageToolsProvider.LoadTexture(path), CUT);
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
            
            BuildingModel model = BuildingFactory.CreateFromName("fIElD");
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
            BuildingModel bmodel = BuildingFactory.CreateFromName("field");
            grid.PlaceAt(bmodel, new Vector2I(1,1), true);

        }
    }
}
