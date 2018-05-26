using System.Collections.Generic;

namespace EventHorizon.Game.Server.Core.Level.Model
{
    public class LevelRegister
    {
        public string ServerAddress { get; set; }
        public IList<string> Tags { get; set; }
    }
}