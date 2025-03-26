// define o contrato de routing 
// (public interface ITrafficRouting{void RouteTraffic(int requestsCount);})

// permite implementar FullTrafficRouting, CacheTrafficRouting, etc

// injetada em Home.razor pata distribuir solicitações 
// quando o botão "Enviar Solicitações" é clicado

using System.Collections.Generic;

namespace InfraSim.Services.Traffic.Interfaces
{
    public interface ITrafficRouting
    {
        void RouteTraffic(int requestsCount);
    }
}