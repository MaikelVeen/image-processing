using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}