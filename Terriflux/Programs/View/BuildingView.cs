using Godot;
using System.Collections.Generic;
using System.IO;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model;
using Terriflux.Programs.Observers;

namespace Terriflux.Programs.View
{
    /// <summary>
    /// Creates a building visible to the player (IMMUABLE!)
    /// </summary>
    public partial class BuildingView : Node2D, IBuildingObserver 
    {
        /// <summary>
        /// Simple class construction not allowed. Please use the associated Design() function.
        /// </summary>
        private BuildingView() : base() { }

        // TODO - where the horizontal and vertical version will be stored, so as not to have to recreate a building every time the player changes orientation)?

        /// <param name="buildingName"></param>
        /// <returns>The created BuildingView node </returns>
        /// <exception cref="FileNotFoundException"></exception>
        private static BuildingView Create()
        {
            return (BuildingView)GD.Load<PackedScene>(Paths.VIEW_NODES + "BuildingView" + Paths.GDEXT)
                    .Instantiate();
        }

        /// <summary>
        /// Instantiates a building in the specified scene.
        /// </summary>
        /// <param name="parent">Scene in which this will be instantiated </param>
        /// <param name="model">Model on which this is based, from which this retrieves its information</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="FileLoadException"></exception>
        public static BuildingView Design(Node parent, BuildingModel model) 
        {
            GD.Print($"Parent={parent.Name}; Nb-child-before={parent.GetChildren().Count}; model={model.GetName()}"); // test

            BuildingView buildingView = BuildingView.Create();
            parent.AddChild(buildingView); // instantiate this and his children

            // observers and update
            model.AddObserver(buildingView);

            /* Configuration */
            string texturePath = Paths.TEXTURES + model.GetName() + ".png";
            int expectedNbOfParts = model.GetPartsNumber();

            // security : texture into path exists?
            if (!File.Exists(texturePath))
            {
                throw new FileNotFoundException(texturePath + " file doesn't found.");
            }

            // load and cut the texture
            Texture2D[] allPartsTextures = ImageToolsProvider.SliceImageTexture(texturePath,expectedNbOfParts);

            // security: have as many different textures as different cells?
            if (allPartsTextures.Length != expectedNbOfParts)
            {
                throw new FileLoadException($"Number of textures loaded ({allPartsTextures.Length}) " +
                    $"is not the same as number of necessary textures ({expectedNbOfParts})");
            }

            // create enough cell view to store the parts' skins
            CellView[] allCellsViews = new CellView[expectedNbOfParts]; 
            for (int i = 0; i < allPartsTextures.Length; i++)
            {
                CellView cellView = CellView.Design();      // create child
                allCellsViews[i] = cellView;    // add to futur obseervers list
                buildingView.AddChild(cellView);    // instantiate into scene
            }

            // add the new composition views to building's composition observers list
            model.AddCompositionObserver(allCellsViews);

            // set up each cells with correct appearance
            for (int i = 0; i < allPartsTextures.Length; i++)
            {
                Sprite2D skin = new();
                skin.Texture = allPartsTextures[i];
                allCellsViews[i].ChangeSkin(skin);

            }

            // test:
            if (model.GetCompObser().Count >= model.GetComposition().Count)
            {

                for (int i = 0; i < model.GetComposition().Count; i++)  // test
                {
                    GD.Print($"{model.GetComposition()[i]}, aka '{model.GetComposition()[i].GetCellName()}', have observer: {model.GetCompObser()[i]}"); // test
                }
            }
            else
            {
                GD.Print($"{model.GetCompObser().Count} lined, but {model.GetComposition().Count} cells!!");
            }

            GD.Print($"Nb-child-after={parent.GetChildren().Count}"); // test


            return buildingView;
        }

        public void UpdateDirection(Direction2D direction)
        {
            throw new System.NotImplementedException();  // TODO
        }
    }
}