using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terriflux.Programs.GameContext
{
    public class ImageToolsProvider
    {
        public static ImageTexture[] SliceImageTexture(string texturePath, int numSlices)
        {
            Image image = new();
            Image blitImage = image;
            blitImage.Load(texturePath);

            Rect2I usedRect = blitImage.GetUsedRect();
            int sliceWidth = usedRect.Size.X / numSlices;

            ImageTexture[] textures = new ImageTexture[numSlices];

            for (int i = 0; i < numSlices; i++)
            {
                Image img = Image.Create(sliceWidth, usedRect.Size.Y, false, Image.Format.Rgb8);
                Rect2I sliceRect = new(i * sliceWidth, 0, sliceWidth, usedRect.Size.Y);
                img.BlitRect(blitImage, sliceRect, Vector2I.Zero);

                ImageTexture tex = ImageTexture.CreateFromImage(img);
                textures[i] = tex;
            }

            return textures;
        }

        public static ImageTexture LoadImageTexture(string texturePath)
        {
            Image blitImage = new();
            blitImage.Load(texturePath);

            Rect2I usedRect = blitImage.GetUsedRect();
            Image img = Image.Create(usedRect.Size.X, usedRect.Size.Y, false, Image.Format.Rgb8);

            img.BlitRect(blitImage, usedRect, Vector2I.Zero);

            ImageTexture tex = ImageTexture.CreateFromImage(img);

            return tex;
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
