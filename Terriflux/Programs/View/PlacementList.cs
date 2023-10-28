using Godot;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Terriflux.Programs.Controller;
using Terriflux.Programs.Data;
using Terriflux.Programs.Factories;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model.Placeables;

namespace Terriflux.Programs.View
{
    public partial class PlacementList : ItemList, IVerbosable
    {
        private readonly List<BuildingModel> buildingsInfos = new();    // store a draft of each  buildable buildings to get further informations about 

        // CONSTRUCT
        private PlacementList() { }

        public static PlacementList Design()
        {
            return (PlacementList)GD.Load<PackedScene>(OurPaths.VIEW_NODES + "PlacementList" + OurPaths.NODEXT)
                .Instantiate();
        }

        // Specific to node
        public override void _Ready()
        {
            StreamReader reader = DataManager.ReadBuildingDatabase();
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                // load the model of each building defined into build's data file
                BuildingModel draftModel = BuildingFactory.LoadModel(line.Split(';')[0], InfluenceScale.REGIONAL);

                // load his texture
                string textureFilePath = OurPaths.TEXTURES + draftModel.GetName().ToLower() + OurPaths.PNGEXT;
                if (!File.Exists(textureFilePath))      // no texture provided?
                {
                    textureFilePath = OurPaths.DEFAULT_BUILDING_TEXTURE;   // load default texture
                }
                Texture2D texture = GD.Load<Texture2D>(textureFilePath);

                // create a new selectable item in the placement list for this building
                this.AddItem(draftModel.GetName(), texture, true);

                // save the draft for later use
                this.buildingsInfos.Add(draftModel);
            }
        }

        // SIGNALS
        private void OnItemSelected(int index)
        {
            GridController.SetModelRequested(this.buildingsInfos[index]);

            // if all ok: change the cell!
            GridController.StartPlacement();

            // reset select
            this.DeselectAll();
        }

        // VERBOSE
        public string Verbose()
        {
            StringBuilder sb = new();
            // concrete items
            sb.AppendLine($"Items ({this.ItemCount}):");
            for (int i = 0; i < this.ItemCount; i++)
            {
                sb.AppendLine($"\t{this.GetItemText(i)}");
            }
            // buildings infos
            sb.AppendLine($"Drafts of building ({this.buildingsInfos.Count}):");
            for (int i = 0; i < this.buildingsInfos.Count; i++)
            {
                sb.AppendLine($"\t{this.buildingsInfos[i].GetName()}");
            }

            return sb.ToString();
        }
    }
}