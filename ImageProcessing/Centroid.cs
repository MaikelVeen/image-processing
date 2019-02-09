using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ImageProcessing
{
    /// <summary>
    /// Data class representing a K-means centroid
    /// </summary>
    public class Centroid
    {
        public Centroid(Vector3 position)
        {
            Position = position;
            Elements = new List<Vector3>();
        }

        /// <summary>
        /// The current position of the centroid
        /// </summary>
        public Vector3 Position { get; private set; }

        /// <summary>
        /// List of elements assigned to this centroid
        /// </summary>
        public List<Vector3> Elements { get; private set; }

        /// <summary>
        /// Recalculates the position of the centroid.
        /// Returns true if position did not change
        /// </summary>
        /// <returns></returns>
        public bool RecalculatePosition()
        {
            int numberOfElements = Elements.Count;
            Vector3 total = new Vector3(0, 0, 0);
            foreach (Vector3 element in Elements)
            {
                total.X += element.X;
                total.Y += element.Y;
                total.Z += element.Z;
            }

            Vector3 average = new Vector3(total.X / numberOfElements, total.Y / numberOfElements,
                total.Z / numberOfElements);

            if (Position == average)
            {
                return true;
            }

            Position = average;
            return false;
        }
    }
}