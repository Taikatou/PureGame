namespace SmallGame
{
    /// <summary>
    /// A GameObject is the primary object for all SmallGame Games. 
    /// Pretty much every object in game should inherit from this to get the benefits of SG. 
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// Gets the type of this object. This is the Name of the class name of this object.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets the Id of this object. This is either set from the level data, or is a random Guid.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets the Script attached to this GameObject.
        /// </summary>
        public string Script { get; set; }

        /// <summary>
        /// Constructs the GameObject. Sets State to New.
        /// </summary>
        protected GameObject()
        {
        }
    }
}
