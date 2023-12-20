namespace Terriflux.Programs.GameContext
{
    public static partial class OurPaths
    {
        // EXTENSIONS
        public static readonly string NODEXT = ".tscn"; // godot node extension
        public static readonly string PNGEXT = ".png";
        public static readonly string SVGEXT = ".svg";
        public static readonly string TEXTEXT = ".txt";

        // Ressources
        public static readonly string RESSOURCES = "Ressources/";
        public static readonly string NODES_DIRECTORY = "Nodes/";
        public static readonly string IMAGES = RESSOURCES + "Images/";
        public static readonly string ICONS = RESSOURCES + "Images/Icons/";
        public static readonly string TEXTURES = RESSOURCES + "Textures/";
        public static readonly string PROGRAMS = "Programs/";
        public static readonly string GENERATED_BUILDINGS_PARTS = TEXTURES + "GeneratedBuildingsParts/";

        // MVC
        public static readonly string VIEW_NODES = NODES_DIRECTORY + "View/";
        public static readonly string MODEL_NODES = NODES_DIRECTORY + "Model/";
        public static readonly string CONTROLLER_NODES = NODES_DIRECTORY + "Controller/";

        // DATA
        public static readonly string DATA = PROGRAMS + "Data/";

        // DEFAULT FILES NAMES
        public static readonly string DEFAULT_CELL_TEXTURE = TEXTURES + "default" + PNGEXT;
        public static readonly string DEFAULT_BUILDING_TEXTURE = TEXTURES + "building_default" + PNGEXT;
    }
}