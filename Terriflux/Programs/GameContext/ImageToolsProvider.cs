using Godot;
using System;
using System.IO;
using Terriflux.Programs.Model;

namespace Terriflux.Programs.GameContext
{
    public class ImageToolsProvider
    {
        private static readonly Color FILL_COLOR = new(8f / 255f, 180f / 255f, 89f / 255f, 1f);     // color of the game's grass 

        /// <summary>
        /// Load and split an image for texture utilisation.  
        /// </summary>
        /// <param name="texturePath">Path to the texture file.</param>
        /// <param name="numSlices">Number of cuts required (i.e. number of pieces returned)</param>
        /// <param name="addBackgroundGrass">If true, and some pieces are not big enough (due to image size or cutting), 
        /// fill in the gaps with the color of the grass.</param>
        /// <param name="orientation">Designates cutting direction</param>
        /// <returns>The split parts in a texture format. 
        /// (You can cast ImageTextures into Textures2D.)
        /// </returns>
        public static ImageTexture[] SplitImageTexture(string texturePath, int numSlices, Orientation2D orientation, bool addBackgroundGrass = false)
        {
            // Array to store the resulting image textures.
            ImageTexture[] textures = new ImageTexture[numSlices];

            // Create a new image and load the texture from the specified path.
            Image image = new();
            Image blitImage = image;
            blitImage.Load(texturePath);

            // Get the used rectangle in the image.
            Rect2I usedRect = blitImage.GetUsedRect();

            int sliceWidth;
            int sliceHeight;

            if (orientation == Orientation2D.HORIZONTAL)
            {
                sliceWidth = Math.Min(usedRect.Size.X / numSlices, 128);
                sliceHeight = 128; // Set to 128 pixels height
            }
            else
            {
                sliceWidth = 128; // Set to 128 pixels width
                sliceHeight = Math.Min(usedRect.Size.Y / numSlices, 128);
            }

            for (int i = 0; i < numSlices; i++)
            {
                // Create a new image with the specified dimensions.
                Image img = Image.Create(128, 128, false, Image.Format.Rgb8);

                if (addBackgroundGrass)
                {
                    // Fill the image with the grass color.
                    Color backgroundColor = FILL_COLOR;
                    img.Fill(backgroundColor);
                }

                // Calculate the rectangle of the slice to extract.
                Rect2I sliceRect;
                if (orientation == Orientation2D.HORIZONTAL)
                {
                    sliceRect = new(i * sliceWidth, 0, sliceWidth, usedRect.Size.Y);
                }
                else
                {
                    sliceRect = new(0, i * sliceHeight, usedRect.Size.X, sliceHeight);
                }

                // Copy the slice from the original image to the new image.
                img.BlitRect(blitImage, sliceRect, Vector2I.Zero);

                // Create a texture from the resulting image.
                ImageTexture tex = ImageTexture.CreateFromImage(img);

                // Store the texture in the array.
                textures[i] = tex;
            }

            return textures;
        }


    }
}
