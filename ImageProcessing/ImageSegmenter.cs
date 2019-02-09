using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace ImageProcessing
{
    public static class ImageSegmenter
    {
        /// <summary>
        /// Segments an image to a given amount of colors
        /// </summary>
        /// <param name="image">The image to segment</param>
        /// <param name="clusterAmount">The amount of clusters</param>
        /// <param name="maxIterations">Maximum iteration threshold</param>
        /// <returns>Segmented color matrix</returns>
        public static Color[,] Segment(Color[,] image, int clusterAmount, int maxIterations = 200)
        {
            // Initialization of algorithm
            ClusterElement[,] elements = GetClusterElements(image);
            Centroid[] centroids = GetRandomCentroids(elements, clusterAmount).ToArray();
            List<Centroid> finishedCentroid = new List<Centroid>();

            // Start stopwatch
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("Starting K-means segmentation");

            // Main iteration loop
            for (int i = 0; i != maxIterations; i++)
            {
                // Reset elements of centroid at start of every iteration
                foreach (Centroid centroid in centroids)
                {
                    centroid.Elements.Clear();
                }

                Console.WriteLine($"Running iteration {i}");
                for (int x = 0; x < elements.GetLength(0); x++)
                {
                    for (int y = 0; y < elements.GetLength(1); y++)
                    {
                        // Get the position of current element
                        Vector3 currentElement = elements[x, y].Position;
                        float minimumDistance = float.MaxValue;
                        int indexClosestCentroid = 0;

                        // Determine which centroid is the nearest
                        for (int j = 0; j < centroids.Length; j++)
                        {
                            float distance = CalculateSquaredDistance(currentElement, centroids[j].Position);
                            if (!(minimumDistance > distance)) continue;

                            minimumDistance = distance;
                            indexClosestCentroid = j;
                        }

                        centroids[indexClosestCentroid].Elements.Add(currentElement);
                        elements[x, y].Centroid = centroids[indexClosestCentroid];
                    }
                }

                // Recalculate positions of centroids
                finishedCentroid.Clear();
                foreach (Centroid centroid in centroids)
                {
                    bool finished = centroid.RecalculatePosition();
                    if (finished)
                    {
                        finishedCentroid.Add(centroid);
                    }
                }

                // Check if convergence is reached
                Console.WriteLine($"Iteration {i} finished");
                if (finishedCentroid.Count == centroids.Length)
                {
                    break;
                }
            }

            stopwatch.Stop();
            Console.WriteLine($"Finished k-means image clustering in {stopwatch.ElapsedMilliseconds} ms");

            return ConvertToImage(elements);
        }

        private static float CalculateSquaredDistance(Vector3 element, Vector3 element2)
        {
            float x = element2.X - element.X;
            float y = element2.Y - element.Y;
            float z = element2.Z - element.Z;
            return x * x + y * y + z * z;
        }

        private static IEnumerable<Centroid> GetRandomCentroids(ClusterElement[,] elements, int k)
        {
            // Initialize new list of Vectors 
            List<Centroid> centroids = new List<Centroid>();
            Random random = new Random();

            // Generate up to k amount of centroids
            for (int i = 0; i < k; i++)
            {
                Vector3 randomPosition = elements[random.Next(0, elements.GetLength(0)),
                    random.Next(0, elements.GetLength(1))].Position;
                centroids.Add(new Centroid(randomPosition));
            }

            return centroids;
        }

        private static ClusterElement[,] GetClusterElements(Color[,] image)
        {
            ClusterElement[,] elements = new ClusterElement[image.GetLength(0), image.GetLength(1)];
            for (int x = 0; x < image.GetLength(0); x++)
            {
                for (int y = 0; y < image.GetLength(1); y++)
                {
                    elements[x, y] = new ClusterElement(new Vector3(image[x, y].R, image[x, y].G, image[x, y].B));
                }
            }

            return elements;
        }


        private static Color[,] ConvertToImage(ClusterElement[,] elements)
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