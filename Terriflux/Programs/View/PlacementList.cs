using Godot;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Terriflux.Programs.Controller;
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
            StreamReader reader;
            string line;

            // open data file
            string filePath = OurPaths.DATA + "Buildings" + OurPaths.TEXTEXT;
            if (File.Exists(filePath))
            {
                reader = new StreamReader(filePath);
            }
            else
            {
                throw new FileNotFoundException($"Unable to find the specified file at '{filePath}'");
            }

            int i = 0;
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
                AddItem(draftModel.GetName(), texture, true);

                // save his informations access
                InstantiateHelper(draftModel, i);
                i++;

                // save the draft for later use
                buildingsInfos.Add(draftModel);
            }

            // close file
            reader.Close();
        }

        private void InstantiateHelper(IVerbosable verbosable, int index)
        {
            const int HELP_RIGHT_POSITION = 230;
            const int BORDER_OFFSET = 8;
            // Node
            Help helper = Help.Design();
            helper.Scale = new Vector2 ((float)0.8, (float)0.8);
            helper.Position = new Vector2(HELP_RIGHT_POSITION, this.FixedIconSize.X * index + BORDER_OFFSET * index);
            this.AddChild(helper);

            // Content
            helper.Set(verbosable);
        }

        // SIGNALS
        private void OnItemSelected(int index)
        {
            GridController.SetModelRequested(buildingsInfos[index]);

            // if all ok: change the cell!
            GridController.StartPlacement();

            // reset select
            DeselectAll();
        }

        // VERBOSE
        public string Verbose()
        {
            StringBuilder sb = new();
            // concrete items
            sb.AppendLine($"Items ({ItemCount}):");
            for (int i = 0; i < ItemCount; i++)
            {
                sb.AppendLine($"\t{GetItemText(i)}");
            }
            // buildings infos
            sb.AppendLine($"Drafts of building ({buildingsInfos.Count}):");
            for (int i = 0; i < buildingsInfos.Count; i++)
            {
                sb.AppendLine($"\t{buildingsInfos[i].GetName()}");
            }

            return sb.ToString();
        }
    }
}