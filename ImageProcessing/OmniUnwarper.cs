using System;
 using System.Drawing;
 using System.Numerics;
 using System.Text.RegularExpressions;
 
 namespace ImageProcessing
 {
     public class OmniUnwarper
     {
         public static Color[,] Unwarp(Color[,] omniImage, int centerX, int centerY, int blindSpotRadius,
             int outerRadius)
         {
             // Initialize a new perspective Image
             int circumference = (int) Math.Round(2 * Math.PI * outerRadius);
             Color[,] perspectiveImage = new Color[circumference, outerRadius - blindSpotRadius];
 
             // Loop through coordinates of perspective image
             for (int x = 0; x < perspectiveImage.GetLength(0) - 1; x++)
             {
                 for (int y = 0; y < perspectiveImage.GetLength(1) - 1; y++)
                 {
                     // Convert to polar in omni
                     float normalizedX = (float) x / perspectiveImage.GetLength(0);
                     double angle = -2 * Math.PI * normalizedX;
                     float r = outerRadius - y;
 
                     // Convert polar to cartesian
                     double perspectiveX = r * Math.Cos(angle) + centerX;
                     double perspectiveY = r * Math.Sin(angle) + centerY;
 
                     // Interpolate the fractional coordinates
                     perspectiveImage[x, y] =
                         ImageScaler.Bilinear(omniImage, (float) Math.Abs(perspectiveX), (float) Math.Abs(perspectiveY));
                 }
             }
 
             return perspectiveImage;
         }
     }
 }