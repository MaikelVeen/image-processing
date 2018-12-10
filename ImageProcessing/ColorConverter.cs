using System.Drawing;

namespace ImageProcessing
{
    public class ColorConverter
    {
        private const float Wr = 0.299f;
        private const float Wb = 0.114f;
        private const float Wg = 1 - Wr - Wb;

        /// <summary>
        /// Delegate that describes color encoding conversion, can be 
        /// </summary>
        /// <param name="convertedColor"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        private delegate T ColorTransformation<T, T2>(T2 color);

        /// <summary>
        /// Generic method that loops through an multidimensional array of a color format and
        /// applies a method to the function
        /// </summary>
        /// <param name="colorArray">Multi Dimensional array of color encodings</param>
        /// <param name="convertMethod">The method used to convert T into T2</param>
        /// <typeparam name="T">Input color encoding</typeparam>
        /// <typeparam name="T2">Output color enconding</typeparam>
        /// <returns>Multi dimensional array of color encodings</returns>
        private T[,] ExecuteColorSpaceTransformation<T, T2>(T2[,] colorArray, ColorTransformation<T, T2> transformation)
        {
            int width = colorArray.GetLength(0);
            int height = colorArray.GetLength(1);

            T[,] convertedColours = new T[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    convertedColours[x, y] = transformation(colorArray[x, y]);
                }
            }

            return convertedColours;
        }

        private YuvColor ConvertRgbToYuv(Color rgbColor)
        {
            float normalizedRed = NormalizeRgb(rgbColor.R);
            float normalizedBlue = NormalizeRgb(rgbColor.B);
            float normalizedGreen = NormalizeRgb(rgbColor.G);

            float y = (Wr * normalizedRed) + (Wg * normalizedGreen) + (Wb * normalizedBlue);
            float u = 0.492f * (normalizedBlue - y);
            float v = 0.877f * (normalizedRed - y);

            return new YuvColor {Y = y, U = u, V = v};
        }

        /// <summary>
        /// Converts an Color matrix to Yuv matrix
        /// </summary>
        /// <param name="image">Rgb matrix</param>
        /// <returns>Yuv matrix</returns>
        public YuvColor[,] ConvertRgbImageToYuv(Color[,] image)
        {
            return ExecuteColorSpaceTransformation(image, ConvertRgbToYuv);
        }

        private Color ConvertYuvToRgb(YuvColor yuvColor)
        {
            float r = yuvColor.Y + 1.14f * yuvColor.V;
            float g = yuvColor.Y - 0.395f * yuvColor.U - 0.581f * yuvColor.V;
            float b = yuvColor.Y + yuvColor.U * 2.033f;


            return Color.FromArgb((int) (r * 255), (int) (g * 255), (int) (b * 255));
        }

        /// <summary>
        /// Converts an Yuv matrix to an Rgb matrix
        /// </summary>
        /// <param name="yuvColors">Yuv matrix</param>
        /// <returns>Rgb matrix</returns>
        public Color[,] ConvertYuvToRgbImage(YuvColor[,] yuvColors)
        {
            return ExecuteColorSpaceTransformation(yuvColors, ConvertYuvToRgb);
        }

        private Color RgbToGreyscale(Color rgbColor)
        {
            int average = (rgbColor.A + rgbColor.G + rgbColor.B) / 3;
            return Color.FromArgb(255, average, average, average);
        }

        /// <summary>
        /// Greyscales the values of an Rgb matrix
        /// </summary>
        /// <param name="image">Rgb matrix</param>
        /// <returns>Rgb matrix</returns>
        public Color[,] GreyscaleRgbImage(Color[,] image)
        {
            return ExecuteColorSpaceTransformation(image, RgbToGreyscale);
        }

        private static YuvColor YuvToGreyscale(YuvColor yuvColor)
        {
            return new YuvColor {Y = yuvColor.Y, U = 0, V = 0};
        }

        /// <summary>
        /// Greyscales the values of an Yuv Matrix
        /// </summary>
        /// <param name="yuvColors">Yuv Matrix</param>
        /// <returns>Yuv Matrix</returns>
        public YuvColor[,] GreyscaleYuvColors(YuvColor[,] yuvColors)
        {
            return ExecuteColorSpaceTransformation(yuvColors, YuvToGreyscale);
        }

        private float NormalizeRgb(float value) => value / 255;
    }
}