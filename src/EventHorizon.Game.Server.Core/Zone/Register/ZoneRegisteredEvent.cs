namespace EventHorizon.Game.Server.Core.Zone.Register;

using EventHorizon.Game.Server.Core.Zone.Model;

using MediatR;

public record ZoneRegisteredEvent(ZoneDetails Zone) : INotification;
