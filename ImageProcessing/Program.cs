using System.Drawing;
using ImageUtilities;


namespace ImageProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = args[0]; // right click template -> properties -> Debug -> command line arguments -> "../../../images/tulips.png"
            string path = @"../../../images/forest.jpg";
            Color[,] image = ImageViewer.LoadImage(path);

            #region Assignment 1

            ColorConverter colorConverter = new ColorConverter();

            // Convert image to yuv and back
            YuvColor[,] yuvColors = colorConverter.ConvertRgbImageToYuv(image);
            Color[,] imageConvertedBack = colorConverter.ConvertYuvToRgbImage(yuvColors);

            //Convert rgb to greyscale
            Color[,] greyscaleImage = colorConverter.GreyscaleRgbImage(image);

            // Convert yuv to greyscale and to rgb for display
            YuvColor[,] greyscaleYuv = colorConverter.GreyscaleYuvColors(yuvColors);
            Color[,] greyscaleImageYuv = colorConverter.ConvertYuvToRgbImage(greyscaleYuv);


            // Display the results
            ImageViewer.DrawImagePair(image, imageConvertedBack);
            ImageViewer.DrawImagePair(image, greyscaleImage);
            ImageViewer.DrawImagePair(image, greyscaleImageYuv);

            #endregion


            #region Assignment 2

            ImageResizer imageResizer = new ImageResizer();
            
            // Resize images using nearest neighbour interpolation
            Color[,] scaledUpImageNN = imageResizer.Scale(imageResizer.NearestNeighbour, image, 2.0f);
            Color[,] scaledDownImageNN = imageResizer.Scale(imageResizer.NearestNeighbour, image, .25f);

            // Resize images using bilinear interpolation
            Color[,] scaledUpImageB = imageResizer.Scale(imageResizer.Bilinear, image, 2.0f);
            Color[,] scaledDownImageB = imageResizer.Scale(imageResizer.Bilinear, image, .25f);

            // Display the results
            ImageViewer.DrawImagePair(image, scaledUpImageNN);
            ImageViewer.DrawImagePair(image, scaledUpImageB);
            ImageViewer.DrawImagePair(image, scaledUpImageNN);
            ImageViewer.DrawImagePair(image, scaledUpImageB);

            #endregion
        }
    }
}