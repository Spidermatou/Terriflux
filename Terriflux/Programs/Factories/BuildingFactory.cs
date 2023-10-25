using Godot;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Terriflux.Programs.Data;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model.Placeables;
using Terriflux.Programs.View;

namespace Terriflux.Programs.Factories
{
    /// <summary>
    /// Can load building from data files, and create view from a specified model.
    /// </summary>
    public static partial class BuildingFactory
    {
        public static BuildingModel LoadModel(string name, InfluenceScale influence)
        {
            StreamReader reader = DataManager.ReadBuildingDatabase();
            name = name.ToLower().Replace(" ", ""); // erase spaces

            // read each actualLine to find the correct building
            string actualLine;
            string[] splitedLine;
            string rawData_name;
            string[] rawData_impacts;
            string[] rawData_products;
            string[] rawData_needs;

            double[] extracted_impacts = new double[3];
            Dictionary<FlowKind, int> extracted_production = new();
            Dictionary<FlowKind, int> extracted_needs = new();

            while ((actualLine = reader.ReadLine()) != null)
            {
                /* Extraction of the informations */
                // split all line
                splitedLine = actualLine.Split(";");

                // extract name
                rawData_name = splitedLine[0].Replace(" ", "").ToLower();

                // is it the one we want? 
                if (rawData_name.Equals(name))     // yes
                {
                    // extract impacts
                    rawData_impacts = splitedLine[1].Split(",");
                    extracted_impacts[0] = double.Parse(rawData_impacts[0], CultureInfo.InvariantCulture);
                    extracted_impacts[0] = double.Parse(rawData_impacts[1], CultureInfo.InvariantCulture);
                    extracted_impacts[0] = double.Parse(rawData_impacts[2], CultureInfo.InvariantCulture);

                    // extract needs
                    rawData_needs = splitedLine[2].Replace(" ", "").Split(",");
                    for (int i = 0; i < rawData_needs.Length - 1; i += 2)
                    {
                        if (!extracted_needs.ContainsKey(GlobalTools.TranslateToFlowKind(rawData_needs[i])))
                        {
                            extracted_needs.Add(GlobalTools.TranslateToFlowKind(rawData_needs[i]), int.Parse(rawData_needs[i + 1]));
                        }
                    }

                    // extract production
                    rawData_products = splitedLine[3].Replace(" ", "").Split(",");
                    for (int i = 0; i < rawData_products.Length - 1; i++)
                    {
                        if (!extracted_production.ContainsKey(GlobalTools.TranslateToFlowKind(rawData_products[i])))
                        {
                            extracted_production.Add(GlobalTools.TranslateToFlowKind(rawData_products[i]), int.Parse(rawData_products[i + 1]));
                        }
                    }

                    return new BuildingModel(name, extracted_impacts, influence, extracted_needs, extracted_production);
                }
                // no? skip to next building
            }

            // correct name never founded
            throw new ArgumentException($"No building with the name '{name}' has been" +
                    "found among the available buildings");
        }

        /// <summary>
        /// Design a Building View with good texture. 
        /// Remember to add it to your scene to display it.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="orientation"></param>
        /// <param name="addGrass"></param>
        /// <returns></returns>
        public static BuildingView CreateView(BuildingModel model)
        {
            // verify if texture exists
            string textureFilePath = OurPaths.TEXTURES + model.GetName().ToLower() + OurPaths.PNGEXT;
            if (!File.Exists(textureFilePath))      // no texture provided?
            {
                textureFilePath = OurPaths.DEFAULT_BUILDING_TEXTURE;   // load default texture
            }

            // load texture
            Texture2D texture = GD.Load<Texture2D>(textureFilePath);

            // create
            return BuildingView.Design(model, texture);
        }
    }
}