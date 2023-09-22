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

        public static readonly string RESSOURCES = "Ressources/";
        public static readonly string NODES = "Nodes/";
        public static readonly string IMAGES = RESSOURCES + "Images/";

        // MVC
        public static readonly string VIEW_NODES = NODES + "View/";
        public static readonly string MODEL_NODES = NODES + "Model/";
        public static readonly string CONTROLLER_NODES = NODES + "Controller/";

        // DEFAULTS TOOLS
        public static readonly string DEFAULT_TEXTURE = IMAGES + "icon" + SVGEXT;



    }
}