using Godot;
using System;

namespace Terriflux.Programs.GameContext
{
    public static partial class Paths
    {
        // EXTENSIONS
        public static readonly string GDEXT = ".tscn"; // godot node extension
        public static readonly string PNGEXT = ".png";
        public static readonly string SVGEXT = ".svg";
        public static readonly string TEXTEXT = ".txt";

        public static readonly string RESSOURCES = "Ressources/";
        public static readonly string NODES = "Nodes/";
        public static readonly string IMAGES = RESSOURCES + "Images/";
        public static readonly string TEXTURES = RESSOURCES + "Textures/";
        public static readonly string PROGRAMS = "Programs/";

        // MVC
        public static readonly string VIEW_NODES = NODES + "View/";
        public static readonly string MODEL_NODES = NODES + "Model/";
        public static readonly string CONTROLLER_NODES = NODES + "Controller/";

        // DATA
        public static readonly string DATA = PROGRAMS + "Data/";
    }
}