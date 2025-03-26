// A INTERFACE

// garante que o método RouteTraffic seja implementado nas 
// classes que herdam de TrafficRouting

using InfraSim.Services.Traffic.Enums;

namespace InfraSim.Services.Traffic.Interfaces
{
    public interface IServer
    {
        ServerType ServerType { get; }
        int RequestsCount { get; set; }
        void HandleRequests(int requestsCount);
    }
}
