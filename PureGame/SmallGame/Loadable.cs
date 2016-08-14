namespace SmallGame
{

    /// <summary>
    /// A GameLevel is a collection of IGameObjects and metaData for the level being used in CoreGame. 
    /// There is usually only level active at a time. Level is pretty much synomous with 'scene'. 
    /// </summary>
    public abstract class Loadable
    {
        /// <summary>
        /// Gets or Sets the IGameObjectCollection that holds all of the IGameObjects in this Level.
        /// </summary>
        public IGameObjectCollection Objects { get; set; }

        /// <summary>
        /// Gets or Sets the raw LevelData. LevelData is loaded from the JSON file. 
        /// </summary>
        public LevelData Data { get; set; }

        /// <summary>
        /// Gets the Name of the Level
        /// </summary>
        public string Name { get { return Data.Name; } }


        protected Loadable()
        {
            Objects = new IGameObjectCollection();
        }
    }
}
