using Godot;
using System.IO;
using Terriflux.Programs.GameContext;

namespace Terriflux.Programs.Data
{
    public static partial class DataManager
    {
        /// <param name="path"></param>
        /// <returns>The Texture saved at the specified path, if she exists.</returns>
        /// <exception cref="FileNotFoundException"></exception>
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

        /// <param name="name"></param>
        /// <returns>The texture of the object with the specified name, if it is normalized (i.e. it exists in the Resource/Textures folder and is in png format).</returns>
        public static Texture2D LoadNodeTexture(string name)
        {
            return LoadTexture(OurPaths.TEXTURES + name.ToLower() + OurPaths.PNGEXT);
        }

        /// <summary>
        /// Open the file saved at the specified path, if he exists.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>His reader stream.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static StreamReader ReadFile(string fileName)
        {
            string filePath = OurPaths.DATA + fileName + OurPaths.TEXTEXT;
            if (File.Exists(filePath))
            {
                return new StreamReader(filePath);
            }
            else
            {
                throw new FileNotFoundException($"Unable to find the specified file at '{filePath}'");
            }
        }

        /// <summary>
        /// Open the file whre all buildings informations are saved.
        /// </summary>
        /// <returns>His reader stream.</returns>
        public static StreamReader ReadBuildingDatabase()
        {
            return ReadFile("Buildings");
        }
    }
}
