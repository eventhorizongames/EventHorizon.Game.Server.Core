namespace EventHorizon.Game.Server.Core.Zone.Register;

using MediatR;

public record ZoneUnregisteredEvent(string ZoneId) : INotification;
