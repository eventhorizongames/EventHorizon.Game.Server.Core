using System;

namespace EventHorizon.Game.Server.Core.Level.Exceptions
{
    public class LevelExistsException : Exception
    {
        private string LevelId { get; }
        public LevelExistsException(string levelId)
            : base(string.Format("Level entity already exists with LevelId of {0}", levelId))
        {
            this.LevelId = levelId;
        }
    }
}