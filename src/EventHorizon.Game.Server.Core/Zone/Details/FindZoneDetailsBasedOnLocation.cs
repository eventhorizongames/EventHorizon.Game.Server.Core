namespace EventHorizon.Game.Server.Core.Zone.Details
{
    using EventHorizon.Game.Server.Core.Player.Model;
    using EventHorizon.Game.Server.Core.Zone.Model;
    using MediatR;

    public struct FindZoneDetailsBasedOnLocation : IRequest<ZoneDetails>
    {
        public LocationState Location { get; }

        public FindZoneDetailsBasedOnLocation(
            LocationState location
        )
        {
            Location = location;
        }
    }
}