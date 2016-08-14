namespace SmallGame
{
    /// <summary>
    /// A IGameObject is the primary object for all SmallGame Games. 
    /// Pretty much every object in game should inherit from this to get the benefits of SG. 
    /// </summary>
    public interface IGameObject
    {
        /// <summary>
        /// Gets the type of this object. This is the Name of the class name of this object.
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Gets the Id of this object. This is either set from the level data, or is a random Guid.
        /// </summary>
        string Id { get; set; }

        string Script { get; set; }

        void OnInit();
    }
}
