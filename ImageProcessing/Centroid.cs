using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ImageProcessing
{
    /// <summary>
    /// Data class for a K-means Centroid
    /// </summary>
    public class Centroid
    {
        public Centroid(Vector3 position)
        {
            Position = position;
            clusterElements = new List<Vector3>();
        }

        /// <summary>
        /// The current position of the centroid
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// All elements assigned to this centroid
        /// </summary>
        public List<Vector3> clusterElements { get; set; }

        /// <summary>
        /// Set the position of the centroid to its average of elements
        /// </summary>
        public void RecalculatePosition()
        {
            if (clusterElements.Count <= 0) return;

            Vector3 average = new Vector3(
                clusterElements.Average(x => x.X),
                clusterElements.Average(x => x.Y),
                clusterElements.Average(x => x.Z));

            Position = average;
        }
    }
}