using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terriflux.Programs.GameContext
{
    /* TODO - URGENT - there's a prb into SliceImg: return transparent textures! */


    public class ImageToolsProvider
    {
        public static Texture2D[] SliceImage(Texture2D texture, int expectedNbOfParts) 
        {
            int tileWidth = (int)(texture.GetWidth() / expectedNbOfParts);
            int tileHeight = (int)(texture.GetHeight() / expectedNbOfParts);

            Texture2D[] regions = new Texture2D[expectedNbOfParts];

            for (int index = 0; index < expectedNbOfParts; index++)
            {
                Image subImage = Image.Create(tileWidth, tileHeight, false, Image.Format.Rgba8);
                Texture2D subTexture = ImageTexture.CreateFromImage(subImage);

                // save
                regions[index] = subTexture;
            }

            return regions;
        }

        public static Texture2D LoadTexture(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Not found {path}");
            }
            else
            {
                return GD.Load<Texture2D>(path);
            }
        }
    }
}
