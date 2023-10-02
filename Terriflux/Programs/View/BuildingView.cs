using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model;
using Terriflux.Programs.Observers;

namespace Terriflux.Programs.View
{
    public partial class BuildingView : CellView, IBuildingObserver // TODO - doesn't inherit from CellView, but composed of!
    {

        private BuildingView() : base() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buildingName"></param>
        /// <returns>The created BuildingView node, with basic cell skin </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static BuildingView Create(string buildingName)
        {
            string texturePath = Paths.TEXTURES + buildingName.ToLower() + Paths.PNGEXT;

            if (File.Exists(texturePath))
            {
                BuildingView building = (BuildingView)GD.Load<PackedScene>(Paths.VIEW_NODES + "BuildingView" + Paths.GDEXT)
                    .Instantiate();
                return building;
            }
            else
            {
                throw new FileNotFoundException();
            }
        }


        public override void _Ready()
        {
            GD.Print("Building created!"); // test
        }

        public override void _Process(double delta)
        {
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