using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terriflux.Programs.Data.Management;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.View;


/*
 *  The error comes from FlowKind, 
 *  which is either repeated twice in the same dictionary (2x in need, or 2x in products); 
 *  or else it's in the translation function; 
 *  to test!
 */



namespace Terriflux.Programs.Model
{
    public partial class BuildingFactory // TODO - factory useless! put this function in constructor
    {
        public static BuildingModel CreateBuilding(string name)
        {
            StreamReader reader = DataManager.LoadBuildingData();
            name = name.ToLower().Replace(" ", ""); // erase spaces

            // read each line to find the correct building
            string line;
            string[] split;
            string line_name;
            string[] splited_products;
            string[] splited_needs;
            int size;

            Dictionary<FlowKind, int> effective_products = new();
            Dictionary<FlowKind, int> effective_needs = new();
            /* Dictionary<FlowKind, int> effective_products = new();
            Dictionary<FlowKind, int> effective_needs = new(); */
            while ((line = reader.ReadLine()) != null)
            {
                split = line.Split(";");
                line_name = split[0].Replace(" ", "").ToLower();
                size = int.Parse(split[1]);
                splited_needs = split[2].Replace(" ", "").Split(",");
                splited_products = split[3].Replace(" ", "").Split(",");

                GD.Print(line_name, size, splited_needs, splited_products);

                if (line_name.Equals(name))
                {
                    // extract needs
                    for (int i = 0; i < splited_needs.Length - 1; i += 2)
                    {
                        if (!effective_needs.ContainsKey(GlobalTools.TranslateToFlowKind(splited_needs[i])))
                        {
                            effective_needs.Add(GlobalTools.TranslateToFlowKind(splited_needs[i]), int.Parse(splited_needs[i + 1]));
                        }
                    }

                    // extract products
                    for (int i = 0; i < splited_products.Length - 1; i++)
                    {
                        if (!effective_products.ContainsKey(GlobalTools.TranslateToFlowKind(splited_products[i])))
                        {
                            effective_products.Add(GlobalTools.TranslateToFlowKind(splited_products[i]), int.Parse(splited_products[i + 1]));
                        }
                    }

                    return new BuildingModel(name, size, effective_needs, effective_products);
                }
            }

            // correct name never founded
            throw new ArgumentException($"No building with the name '{name}' has been" +
                    "found among the available buildings");
        }

        public static BuildingView DesignBuilding(Node parent, BuildingModel model) // TODO
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
    }
}
