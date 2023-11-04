using Godot;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
            name = name.ToLower().Replace(" ", ""); // efface les espaces

            // Définir le chemin du fichier
            string filePath = OurPaths.DATA + "Buildings" + OurPaths.TEXTEXT;

            try
            {
                using StreamReader reader = new(filePath);
                string actualLine;
                while ((actualLine = reader.ReadLine()) != null)
                {
                    /* Extraction des informations */
                    string[] splitedLine = actualLine.Split(";");
                    string rawData_name = splitedLine[0].Replace(" ", "").ToLower();

                    if (rawData_name.Equals(name))
                    {
                        // Extraire les impacts
                        string[] rawData_impacts = splitedLine[1].Split(",");
                        double[] extracted_impacts = new double[3];

                        if (rawData_impacts.Length == 3 &&
                            double.TryParse(rawData_impacts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out extracted_impacts[0]) &&
                            double.TryParse(rawData_impacts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out extracted_impacts[1]) &&
                            double.TryParse(rawData_impacts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out extracted_impacts[2]))
                        {
                            // Extraire les besoins
                            string[] rawData_needs = splitedLine[2].Replace(" ", "").Split(",");
                            Dictionary<FlowKind, int> extracted_needs = new();

                            for (int i = 0; i < rawData_needs.Length - 1; i += 2)
                            {
                                if (Enum.TryParse(rawData_needs[i], out FlowKind flowKind) &&
                                    int.TryParse(rawData_needs[i + 1], out int amount))
                                {
                                    extracted_needs[flowKind] = amount;
                                }
                            }

                            // Extraire la production
                            string[] rawData_products = splitedLine[3].Replace(" ", "").Split(",");
                            Dictionary<FlowKind, int> extracted_production = new();

                            for (int i = 0; i < rawData_products.Length - 1; i += 2)
                            {
                                if (Enum.TryParse(rawData_products[i], out FlowKind flowKind) &&
                                    int.TryParse(rawData_products[i + 1], out int amount))
                                {
                                    extracted_production[flowKind] = amount;
                                }
                            }

                            return new BuildingModel(name, extracted_impacts, influence, extracted_needs, extracted_production);
                        }
                        else
                        {
                            throw new FormatException("Format invalide dans les données d'impact.");
                        }
                    }
                }

                throw new ArgumentException($"Aucun bâtiment avec le nom '{name}' n'a été trouvé parmi les bâtiments disponibles");
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"Impossible de trouver le fichier spécifié à l'emplacement '{filePath}'", ex);
            }
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