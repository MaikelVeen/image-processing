using System;
using System.Drawing;

namespace ImageProcessing
{
    public class ImageResizer
    {
        public delegate Color[,] InterpolationMethod(Color[,] image, float factor);

        public Color[,] Scale(InterpolationMethod im, Color[,] image, float scaleFactor)
        {
            return im(image, scaleFactor);
        }


        public Color[,] NearestNeighbour(Color[,] image, float factor)
        {
            int width = (int) Math.Ceiling(image.GetLength(0) * factor);
            int height = (int) Math.Ceiling(image.GetLength(1) * factor);

            Color[,] scaledImage = new Color[width, height];

            for (int x = 0; x < scaledImage.GetLength(0) - 1; x++)
            {
                for (int y = 0; y < scaledImage.GetLength(1) - 1; y++)
                {
                    int sourcePixelX = (int) Math.Round(x / factor);
                    int sourcePixelY = (int) Math.Round(y / factor);

                    scaledImage[x, y] = image[sourcePixelX, sourcePixelY];
                }
            }

            return scaledImage;
        }

        public Color[,] Bilinear(Color[,] image, float factor)
        {
            int width = (int) Math.Ceiling(image.GetLength(0) * factor);
            int height = (int) Math.Ceiling(image.GetLength(1) * factor);

            Color[,] scaledImage = new Color[width, height];

            for (int x = 0; x < scaledImage.GetLength(0) - 1; x++)
            {
                for (int y = 0; y < scaledImage.GetLength(1) - 1; y++)
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
                    Color colourOne = image[floorX, floorY];

                    // (r, c+1)
                    Color colourTwo = image[ceilingX, floorY];

                    // (r+1, c)
                    Color colourThree = image[floorX, ceilingY];

                    // (r+1, c+1)
                    Color colourFour = image[ceilingX, ceilingY];

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

                    scaledImage[x, y] = Color.FromArgb(255, red, green, blue);
                }
            }

            return scaledImage;
        }
    }
}