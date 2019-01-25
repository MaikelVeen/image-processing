using System.Drawing;
using ImageUtilities;


namespace ImageProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = args[0]; // right click template -> properties -> Debug -> command line arguments -> "../../../images/tulips.png"
            const string imagesForestJpg = @"../../../images/forest.jpg";
            const string omni1Jpg = @"../../../images/omni1.jpg";
            const string omni2Jpg = @"../../../images/omni2.jpg";
            const string omni3Jpg = @"../../../images/omni3.jpg";

            Color[,] image = ImageViewer.LoadImage(imagesForestJpg);
            Color[,] omniImage1 = ImageViewer.LoadImage(omni1Jpg);
            Color[,] omniImage2 = ImageViewer.LoadImage(omni2Jpg);
            Color[,] omniImage3 = ImageViewer.LoadImage(omni3Jpg);

            #region Assignment 1

/*

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
            ImageViewer.DrawImagePair(image, greyscaleImageYuv);*/

            #endregion


            #region Assignment 2

            // Resize images using nearest neighbour interpolation
            Color[,] scaledUpImageNN = ImageScaler.Scale(ImageScaler.NearestNeighbour, image, 2.0f);
            Color[,] scaledDownImageNN = ImageScaler.Scale(ImageScaler.NearestNeighbour, image, .25f);

            // Resize images using bilinear interpolation
            Color[,] scaledUpImageB = ImageScaler.Scale(ImageScaler.Bilinear, image, 2.0f);
            Color[,] scaledDownImageB = ImageScaler.Scale(ImageScaler.Bilinear, image, .25f);

            // Display the results
            ImageViewer.DrawImagePair(image, scaledUpImageNN);
            ImageViewer.DrawImagePair(image, scaledUpImageB);
            ImageViewer.DrawImagePair(image, scaledDownImageNN);
            ImageViewer.DrawImagePair(image, scaledDownImageB);

            #endregion

            #region Assignment 4

            // Unwarp and display
            Color[,] perspective1 = OmniUnwarper.Unwarp(omniImage1, 330, 240, 16, 236);
            ImageViewer.DrawImage(perspective1);

            Color[,] perspective2 = OmniUnwarper.Unwarp(omniImage2, 240, 231, 81, 232);
            ImageViewer.DrawImage(perspective2);

            Color[,] perspective3 = OmniUnwarper.Unwarp(omniImage3, 312, 239, 35, 227);
            ImageViewer.DrawImage(perspective3);

            #endregion
        }
    }
}