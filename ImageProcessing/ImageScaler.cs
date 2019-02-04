using System;
using System.Drawing;

namespace ImageProcessing
{
    /// <summary>
    /// Scaling and interpolation implementation
    /// </summary>
    public static class ImageScaler
    {
        public delegate Color InterpolationMethod(Color[,] image, float x, float y, float factor = 1.0f);

        /// <summary>
        /// Scales a image with a given interpolation method
        /// </summary>
        /// <param name="im">Interpolation method</param>
        /// <param name="image">Image to scale</param>
        /// <param name="scaleFactor">Factor to scale</param>
        /// <returns>Scaled image</returns>
        public static Color[,] Scale(InterpolationMethod im, Color[,] image, float scaleFactor)
        {
            int width = (int) Math.Ceiling(image.GetUpperBound(0) * scaleFactor);
            int height = (int) Math.Ceiling(image.GetUpperBound(1) * scaleFactor);

            Color[,] scaledImage = new Color[width, height];
            for (int x = 0; x < scaledImage.GetLength(0) - 1; x++)
            {
                for (int y = 0; y < scaledImage.GetLength(1) - 1; y++)
                {
                    scaledImage[x, y] = im(image, x, y, scaleFactor);
                }
            }

            return scaledImage;
        }

        /// <summary>
        /// Interpolation using the nearest neighbour
        /// </summary>
        /// <param name="originalImage"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="factor"></param>
        /// <returns>Color</returns>
        public static Color NearestNeighbour(Color[,] originalImage, float x, float y, float factor = 1.0f)
        {
            int sourcePixelX = (int) Math.Round(x / factor);
            int sourcePixelY = (int) Math.Round(y / factor);

            return originalImage[sourcePixelX, sourcePixelY];
        }

        /// <summary>
        /// Interpolation method using bilinear interpolation
        /// </summary>
        /// <param name="originalImage"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="factor"></param>
        /// <returns>Color</returns>
        public static Color Bilinear(Color[,] originalImage, float x, float y, float factor = 1.0f)
        {
            int floorX = (int) Math.Floor(x / factor);
            int ceilingX = (int) Math.Ceiling(x / factor);

            int floorY = (int) Math.Floor(y / factor);
            int ceilingY = (int) Math.Ceiling(y / factor);


            float fractionX = x / factor - floorX;
            float fractionY = y / factor - floorY;

            float fractionXRev = 1 - fractionX;
            float fractionYRev = 1 - fractionY;

            // (r,c)
            Color colourOne = originalImage[floorX, floorY];

            // (r, c+1)
            Color colourTwo = originalImage[ceilingX, floorY];

            // (r+1, c)
            Color colourThree = originalImage[floorX, ceilingY];

            // (r+1, c+1)
            Color colourFour = originalImage[ceilingX, ceilingY];

            // Determine Red colour with weighted sum
            float red1 = fractionXRev * colourOne.R + fractionX * colourTwo.R;
            float red2 = fractionXRev * colourThree.R + fractionX * colourFour.R;
            int red = (int) (fractionYRev * red1 + fractionY * red2);

            // Determine Green Colour with weighted sum
            float green1 = fractionXRev * colourOne.G + fractionX * colourTwo.G;
            float green2 = fractionXRev * colourThree.G + fractionX * colourFour.G;
            int green = (int) (fractionYRev * green1 + fractionY * green2);

            // Determine Blue Colour with weighted sum
            float blue1 = fractionXRev * colourOne.B + fractionX * colourTwo.B;
            float blue2 = fractionXRev * colourThree.B + fractionX * colourFour.B;
            int blue = (int) (fractionYRev * blue1 + fractionY * blue2);

            return Color.FromArgb(255, red, green, blue);
        }
    }
}