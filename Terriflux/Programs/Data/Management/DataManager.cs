using Godot;
using System.IO;
using Terriflux.Programs.GameContext;

namespace Terriflux.Programs.Data.Management
{
    public static partial class DataManager
    {
        public static Texture2D LoadTexture(string path)
        {
            if (File.Exists(path))
            {
                return GD.Load<Texture2D>(path);
            }
            else
            {
                throw new FileNotFoundException(path);
            }
        }

        public static StreamReader Load(string fileName)
        {
            string filePath = (OurPaths.DATA + fileName + OurPaths.TEXTEXT);
            if (File.Exists(filePath))
            {
                return new StreamReader(filePath);
            }
            else
            {
                throw new FileNotFoundException($"Unable to find the specified file at '{filePath}'");
            }
        }

        public static StreamReader LoadBuildingData()
        {
            return Load("Buildings");
        }
    }
}
