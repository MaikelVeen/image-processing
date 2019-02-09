using System.Numerics;

namespace ImageProcessing
{
    /// <summary>
    /// Data class for k-means element / object
    /// </summary>
    public class ClusterElement
    {
        public ClusterElement(Vector3 position)
        {
            Position = position;
        }

        /// <summary>
        /// The 'value' of the element
        /// </summary>
        public Vector3 Position { get; private set; }

        /// <summary>
        /// Which centroid the element belongs to in the current iteration
        /// </summary>
        public Centroid Centroid { get; set; }
    }
}