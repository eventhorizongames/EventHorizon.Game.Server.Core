using System;

namespace EventHorizon.Game.Server.Core.Zone.Exceptions
{
    [System.Serializable]
    public class ZoneIdInvalidException : System.Exception
    {
        public string Code { get; } = "zone_id_invalid";
    }
}