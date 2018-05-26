using System;

namespace EventHorizon.Game.Server.Core.Level.Exceptions
{
    public class LevelNotFoundException : Exception
    {
        public string LevelId { get; }
        public LevelNotFoundException(string levelId)
            : base(string.Format("Level not found with LevelId of {0}", levelId))
        {
            this.LevelId = levelId;
        }

        public LevelNotFoundException(InvalidOperationException ex)
            : base("Level not found, please see inner exception.", ex)
        {
        }
    }
}