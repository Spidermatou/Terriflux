using Godot;
using System;
using System.IO;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model;

namespace Terriflux.Programs.View
{
    public static partial class BuildingViewsFactory
    {
        /// <summary>
        /// Instantiates a building in the specified scene.
        /// </summary>
        /// <param name="model">Model on which this is based, from which this retrieves its information</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="FileLoadException"></exception>
        public static BuildingView DesignFromModel(BuildingModel model, Node parent, bool addAsObserver = true)
        {
            // instantiate view in scene
            BuildingView buildingView = BuildingView.Design();
            parent.AddChild(buildingView);

            // observers and update
            if (addAsObserver) model.AddObserver(buildingView);

            /* Configuration */
            string texturePath = Paths.TEXTURES + model.GetName() + ".png";
            int expectedNbOfParts = model.GetPartsNumber();

            // security : texture into path exists?
            if (!File.Exists(texturePath))
            {
                throw new FileNotFoundException(texturePath + " file doesn't found.");
            }

            // load and cut the texture
            Texture2D[] allPartsTextures = ImageToolsProvider.SliceImageTexture(texturePath, expectedNbOfParts);

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
                // appearance
                Sprite2D skin = new()
                {
                    Texture = allPartsTextures[i]
                };
                allCellsViews[i].ChangeSkin(skin);
            }

            // config building himself
            model.CallForUpdates();

            return buildingView;
        }

    }
}