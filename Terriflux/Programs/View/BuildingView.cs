using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model;
using Terriflux.Programs.Observers;

namespace Terriflux.Programs.View
{
    public partial class BuildingView : Node2D, IBuildingObserver 
    {
        private readonly List<Sprite2D> _skins = new();

        private BuildingView() : base() { }

        /// <param name="buildingName"></param>
        /// <returns>The created BuildingView node </returns>
        /// <exception cref="FileNotFoundException"></exception>
        private static BuildingView Create()
        {
            return (BuildingView)GD.Load<PackedScene>(Paths.VIEW_NODES + "BuildingView" + Paths.GDEXT)
                    .Instantiate();
        }

        public static BuildingView Design(Node parent, BuildingModel model) // TODO - move
        {
            string texturePath = Paths.TEXTURES + model.GetName() + ".png";
            if (!File.Exists(texturePath))
            {
                throw new FileNotFoundException(texturePath + " file doesn't found.");
            }
            BuildingView bv = BuildingView.Create();
            parent.AddChild(bv); // instantiate this and his children
            bv.ChangeSkin(texturePath, model.GetPartsNumber());

            // observer and update
            model.AddObserver(bv);
            model.NotifyAlls();

            return bv;
        }

        public override void _Ready()
        {
            GD.Print("Building created!"); // test
        }

        public override void _Process(double delta)
        {
        }

        public void ChangeSkin(string texturePath, int expectedNbOfParts)
        {
            // cut the texture
            Texture2D origin = ImageToolsProvider.LoadTexture(texturePath);
            Texture2D[] textureParts = ImageToolsProvider.SliceImage(origin, expectedNbOfParts);

            // reset actual appearance
            this._skins.Clear();

            // add new appearance
            foreach (Texture2D textureX in textureParts) {
                Sprite2D cellSkinX = new();
                cellSkinX.Texture = textureX;
                this._skins.Add(cellSkinX);
            }

            // refresh/update view
            RefreshSkin();
        }

        private void RefreshSkin()
        {
            // reset children
            this.GetChildren().Clear();

            // replace with new
            foreach (Sprite2D sprite in this._skins)
            {
                this.AddChild(sprite);
            }
        }


        public void UpdateName(string name)
        {
            throw new NotImplementedException();
        }

        public void UpdateImpacts(int[] impacts)
        {
            throw new NotImplementedException();
        }

        public void UpdateInfluence(InfluenceScale actualInfluenceScale)
        {
            throw new NotImplementedException();
        }

        public void UpdateProducts(Dictionary<FlowKind, int> products)
        {
            throw new NotImplementedException();
        }

        public void UpdateNeeds(Dictionary<FlowKind, int> needs)
        {
            throw new NotImplementedException();
        }

        public void UpdateOccupation(int occupation)
        {
            throw new NotImplementedException();
        }
    }
}