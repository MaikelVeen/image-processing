﻿using System;
using System.Drawing;
using ImageUtilities;


namespace ImageProcessing
{
    class Program
    {
        const string imagesForestJpg = @"../../../images/forest.jpg";
        const string omni1Jpg = @"../../../images/omni1.jpg";
        const string omni2Jpg = @"../../../images/omni2.jpg";
        const string omni3Jpg = @"../../../images/omni3.jpg";
        const string tulipsPng = @"../../../images/tulips.png";
        const string baboonPng = @"../../../images/baboon.png";
        const string auroraJpg = @"../../../images/aurora.jpg";
        const string flowersPng = @"../../../images/flower.png";

        private static void Main(string[] args)
        {
            Color[,] forestImage = ImageViewer.LoadImage(imagesForestJpg);
            Color[,] omniImage1 = ImageViewer.LoadImage(omni1Jpg);
            Color[,] omniImage2 = ImageViewer.LoadImage(omni2Jpg);
            Color[,] omniImage3 = ImageViewer.LoadImage(omni3Jpg);
            Color[,] tulips = ImageViewer.LoadImage(tulipsPng);
            Color[,] baboon = ImageViewer.LoadImage(baboonPng);
            Color[,] aurora = ImageViewer.LoadImage(auroraJpg);
            Color[,] flowers = ImageViewer.LoadImage(flowersPng);

           /* #region Assignment 1

            ColorConverter colorConverter = new ColorConverter();

            // Convert image to yuv and back
            Console.WriteLine("Showing conversion to YUY and back");
            YuvColor[,] yuvColors = colorConverter.ConvertRgbImageToYuv(forestImage);
            Color[,] imageConvertedBack = colorConverter.ConvertYuvToRgbImage(yuvColors);
            ImageViewer.DrawImagePair(forestImage, imageConvertedBack);

            //Convert rgb to greyscale
            Console.WriteLine("Showing RGB to greyscale");
            Color[,] greyscaleImage = colorConverter.GreyscaleRgbImage(forestImage);
            ImageViewer.DrawImagePair(forestImage, greyscaleImage);

            // Convert yuv to greyscale and to rgb for display
            Console.WriteLine("Showing RGB to YUV to greyscale");
            YuvColor[,] greyscaleYuv = colorConverter.GreyscaleYuvColors(yuvColors);
            Color[,] greyscaleImageYuv = colorConverter.ConvertYuvToRgbImage(greyscaleYuv);
            ImageViewer.DrawImagePair(forestImage, greyscaleImageYuv);

            #endregion*/


            #region Assignment 2

            // Resize images using nearest neighbour interpolation
            Color[,] scaledDownImageNN = ImageScaler.Scale(ImageScaler.NearestNeighbour, baboon, .2f);
            Color[,] scaledUpImageNN = ImageScaler.Scale(ImageScaler.NearestNeighbour, scaledDownImageNN, 5.0f);

            Console.WriteLine("Showing downscaling using nearest neighbour interpolation");
            ImageViewer.DrawImagePair(baboon, scaledDownImageNN);

            Console.WriteLine("Showing upscaling using nearest neighbour interpolation");
            ImageViewer.DrawImagePair(scaledDownImageNN, scaledUpImageNN);

            // Resize images using bilinear interpolation
            Color[,] scaledDownImageB = ImageScaler.Scale(ImageScaler.Bilinear, baboon, .2f);
            Color[,] scaledUpImageB = ImageScaler.Scale(ImageScaler.Bilinear, scaledDownImageB, 5.0f);

            Console.WriteLine("Showing downscaling using bilinear interpolation");
            ImageViewer.DrawImagePair(baboon, scaledDownImageB);

            Console.WriteLine("Showing upscaling using nearest bilinear interpolation");
            ImageViewer.DrawImagePair(scaledDownImageB, scaledUpImageB);

            #endregion*/

            #region Assignment 3
            
            Console.WriteLine("Showing image segmentation using k-means clustering with 2 clusters");
            Color[,] segmentedImage = ImageSegmenter.Segment(flowers, 2);
            ImageViewer.DrawImagePair(flowers, segmentedImage);
   
            Console.WriteLine("Showing image segmentation using k-means clustering with 8 clusters");
            Color[,] segmentedImage2 = ImageSegmenter.Segment(tulips, 8);
            ImageViewer.DrawImagePair(tulips, segmentedImage2);

            Console.WriteLine("Showing image segmentation using k-means clustering with 6 clusters");
            Color[,] segmentedImage3 = ImageSegmenter.Segment(baboon, 4);
            ImageViewer.DrawImagePair(baboon, segmentedImage3);

          
            #endregion

            #region Assignment 4

            // Unwarp and display
            Console.WriteLine("Showing omnidirectional image unwarping of image 1");
            Color[,] perspective1 = OmniUnwarper.Unwarp(omniImage1, 330, 240, 16, 236);
            ImageViewer.DrawImagePair(omniImage1, perspective1);

            Console.WriteLine("Showing omnidirectional image unwarping of image 2");
            Color[,] perspective2 = OmniUnwarper.Unwarp(omniImage2, 240, 231, 81, 232);
            ImageViewer.DrawImagePair(omniImage2, perspective2);

            Console.WriteLine("Showing omnidirectional image unwarping of image 3");
            Color[,] perspective3 = OmniUnwarper.Unwarp(omniImage3, 312, 239, 35, 227);
            ImageViewer.DrawImagePair(omniImage3, perspective3);

            #endregion
        }
    }
}