using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using Terriflux.Programs.Model;
using Terriflux.Programs.View;

namespace Terriflux.Programs.GameContext
{
    public partial class TestsProvider
    {
        private readonly Node scene;

        public TestsProvider(Node scene)
        {
            this.scene = scene;
        }

        public void ImageToolsProvider()
        {
            string path = "Ressources/Textures/grass.png";


            GD.Print($"--Test_ImageToolsProvider OnGrid--");
            GD.Print($"Actual parent scene child count: {scene.GetChildren().Count}");

            Texture2D texture = GameContext.ImageToolsProvider.LoadTexture(path);
            GD.Print($"Texture loaded? {texture != null}");

            // create image to show the slice-texture's results
            const int CUT_WANTED = 2;
            double gap = 1;
            Texture2D[] slicedTextureParts = GameContext.ImageToolsProvider.SliceImageTexture(path, CUT_WANTED);
            GD.Print($"Number of parts provided: {slicedTextureParts.Length}, awaited: {CUT_WANTED}.");

            // show on screen
            foreach (Texture2D individualTexture in slicedTextureParts)
            {
                Sprite2D sprite = new();
                sprite.Texture = individualTexture;

                CellModel cm = new("Field", CellKind.BUILDING);
                CellView cv = CellView.Design();
                cm.AddObserver(cv);
                cm.SetGlobalSize(128 / 2, 1);
                cm.SetPlacement(new Vector2((float) gap, 0));
                scene.AddChild(cv);

                cv.ChangeSkin(sprite);
                gap += CellModel.GetGlobalSize();
            }

            GD.Print($"New parent scene child count: {scene.GetChildren().Count}");
        }

        public void ImageToolsProvider_OnGrid()   // TODO - URGENT - not yet conclusive!
        {
            GD.Print($"--Test_ImageToolsProvider OnGrid--");
            GD.Print($"Actual child into grid view: {scene.GetChildren().Count}");

            GridModel gridModel = new(10, true);
            GridView gridView = GridView.Design();
            gridModel.AddObserver(gridView);

            GD.Print($"GridView children number: {gridView.GetChildren().Count}");

            // instantiate into test zone
            scene.AddChild(gridView);

            // Modify grid
            gridModel.PlaceAt(BuildingModel.CreateFromName("field"), new Vector2I(5, 5));
            gridModel.PlaceAt(BuildingModel.CreateFromName("field"), new Vector2I(3, 7));
            gridModel.PlaceAt(BuildingModel.CreateFromName("field"), new Vector2I(0, 0));

            // Call a update of view
            gridModel.CallForUpdate();

            gridModel.Verbose();
            GD.Print($"GridView children number: {gridView.GetChildren().Count}");

            GD.Print($"Actual child into grid view: {scene.GetChildren().Count}");
        }

        public void Cell()
        {
            string path = Paths.TEXTURES + "field.png";
            int CUT = 2;

            // via path
            CellModel model = new("field", CellKind.BUILDING);
            CellView view = CellView.Design();  // with texture path
            scene.AddChild(view);
            model.AddObserver(view);
            view.ChangeSkin(path);
            model.SetName("via path");

            // via load and cut
            CellModel model2 = new("field", CellKind.BUILDING);
            CellView view2 = CellView.Design();  // with texture path
            scene.AddChild(view2);
            model2.AddObserver(view2);
            Texture2D[] textures = GameContext.ImageToolsProvider.SliceImageTexture(path, CUT);
            Texture2D littleBit = textures[1];
            Sprite2D sprite = new Sprite2D();

            sprite.Name = "just the sprite";
            sprite.Position = new Vector2(184, -96);
            scene.AddChild(sprite);
            GD.PrintErr(textures.Length);


            sprite.Texture = littleBit;
            view2.ChangeSkin(sprite);
            model2.SetName("via load and cut");
            view2.Position = new Vector2(-288, -72);
        }

        public void Grass()
        {
            GrassModel grassModel = new GrassModel();
            CellView grassView = GrassView.Design();
            grassModel.AddObserver(grassView);
            scene.AddChild(grassView);
        }

        public void Grid()
        {
            GridModel grid_test = GridFactory.CreateFullGrassLand(10);
            GridView territory_view = GridView.Design();
            scene.AddChild(territory_view);
            grid_test.AddObserver(territory_view);
            territory_view.UpdateMap(grid_test);
        }

        public void EnumTranslation()
        {
            string what = "wAteR";
            what = what.Capitalize();
            GD.Print(GlobalTools.TranslateToFlowKind(what));

        }

        /// <summary>
        /// Design a building "field", who wild spawn at specified coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void BuildGeneration()
        {
            BuildingModel model = BuildingModel.CreateFromName("fIElD");
            BuildingView view = BuildingViewsFactory.DesignFromModel(model, scene);
            view.Position = new Vector2((float) CellModel.GetGlobalSize(),
                (float)CellModel.GetGlobalSize());

            // hierarchy
            GD.Print("scene children:" + scene.GetChildren().Count);
            foreach (Node2D child in scene.GetChildren())
            {
                GD.Print($"1- {child} '{child.Name}'");
                foreach (Node subChild in child.GetChildren())
                {
                    GD.Print($"\t2- {subChild} '{subChild.Name}'");
                }
            }
        }

        public void PlaceBuilding()
        {
            // Game basis creation
            GridModel grid = new(5);
            GridView view = GridView.Design();
            grid.AddObserver(view);
            scene.AddChild(view);


            // Building creation
            BuildingModel bmodel = BuildingModel.CreateFromName("field");
            grid.PlaceAt(bmodel, new Vector2I(1, 1), true);

        }
    }
}
