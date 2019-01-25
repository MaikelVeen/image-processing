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
        public Vector3 Position { get; set; }

        /// <summary>
        /// Which centroid the element belongs to in the current iteration
        /// Initially this value is null
        /// </summary>
        public Centroid Centroid { get; set; }
    }
}