using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace ImageProcessing
{
    public static class ImageSegmenter
    {
        /// <summary>
        /// Segments the image into a number of colors based on the k-means clustering
        /// algorithms
        /// </summary>
        /// <param name="image"></param>
        /// <param name="clusterAmount"></param>
        /// <param name="maxIterations"></param>
        /// <returns></returns>
        public static Color[,] Segment(Color[,] image, int clusterAmount, int maxIterations = 10)
        {
            // Convert colors to cluster elements
            ClusterElement[,] elements = ConvertColorToClusterElements(image);

            // Generate
            List<Centroid> centroids = GetRandomCentroids(elements, clusterAmount);

            // Iterative step -> do this for max iterations
            for (int i = 0; i < maxIterations; i++)
            {
                for (int x = 0; x < elements.GetLength(0); x++)
                {
                    for (int y = 0; y < elements.GetLength(1); y++)
                    {
                        // Calculate which centroid is nearest
                        float distanceToNearestCentroid = int.MaxValue;
                        foreach (Centroid centroid in centroids)
                        {
                            float distance =
                                CalculateDistanceToCentroid(elements[x, y].Position, centroid.Position);
                            if (!(distanceToNearestCentroid > distance)) continue;

                            // Set centroid of element to new one if that one is nearer
                            elements[x, y].Centroid = centroid;

                            // Update nearest distance for next iteration step
                            distanceToNearestCentroid = distance;
                        }

                        // Add element to centroid elements list
                        elements[x, y].Centroid.clusterElements.Add(elements[x, y].Position);
                    }
                }

                // Recalculate the position of each centroid
                foreach (Centroid centroid in centroids)
                {
                    centroid.RecalculatePosition();
                }
            }

            return ConvertClusterElementsToColors(elements);
        }
        
        /// <summary>
        /// Calculate distance between two vectors using
        /// pythagoras theorem
        /// </summary>
        /// <param name="element"></param>
        /// <param name="centroid"></param>
        /// <returns></returns>
        private static float CalculateDistanceToCentroid(Vector3 element, Vector3 centroid)
        {
            return (float) Math.Sqrt(Math.Pow(element.X - centroid.X, 2) + Math.Pow(element.Y - centroid.Y, 2));
        }
        
        /// <summary>
        /// Returns k random centroids for initialization
        /// </summary>
        /// <param name="clusterElements"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        private static List<Centroid> GetRandomCentroids(ClusterElement[,] clusterElements, int k)
        {
            List<Vector3> vector3s = new List<Vector3>();
            Random random = new Random();
            for (int i = 0; i < k; i++)
            {
                vector3s.Add(clusterElements[random.Next(0, clusterElements.GetLength(0)),
                    random.Next(0, clusterElements.GetLength(1))].Position);
            }

            List<Centroid> centroids = new List<Centroid>();
            foreach (Vector3 vector3 in vector3s)
            {
                centroids.Add(new Centroid(vector3));
            }

            return centroids;
        }
        
        /// <summary>
        /// Converts a color matrix to cluster element matrix
        /// </summary>
        /// <param name="colors"></param>
        /// <returns></returns>
        private static ClusterElement[,] ConvertColorToClusterElements(Color[,] colors)
        {
            ClusterElement[,] elements = new ClusterElement[colors.GetLength(0), colors.GetLength(1)];
            for (int x = 0; x < colors.GetLength(0); x++)
            {
                for (int y = 0; y < colors.GetLength(1); y++)
                {
                    elements[x, y] = new ClusterElement(new Vector3(colors[x, y].R, colors[x, y].G, colors[x, y].B));
                }
            }

            return elements;
        }
        
        /// <summary>
        /// Converts a cluster element matrix to color matrix
        /// last step in k-means algorithm
        /// color will be set to the value of the centroid the element belongs to
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        private static Color[,] ConvertClusterElementsToColors(ClusterElement[,] elements)
        {
            Color[,] segmentedImage = new Color[elements.GetLength(0), elements.GetLength(1)];
            for (int x = 0; x < elements.GetLength(0); x++)
            {
                for (int y = 0; y < elements.GetLength(1); y++)
                {
                    byte r = (byte) elements[x, y].Centroid.Position.X;
                    byte g = (byte) elements[x, y].Centroid.Position.Y;
                    byte b = (byte) elements[x, y].Centroid.Position.Z;
                    segmentedImage[x, y] = Color.FromArgb(255, r, g, b);
                }
            }

            return segmentedImage;
        }
    }
}