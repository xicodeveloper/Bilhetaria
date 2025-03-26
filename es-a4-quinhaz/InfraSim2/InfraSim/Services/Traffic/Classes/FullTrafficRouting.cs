// vai ser usado para testar o método RouteTraffic da classe FullTrafficRouting
// usa SendRequestsToServers patra enviar solicitações

using InfraSim.Services.Traffic.Interfaces;
using InfraSim.Services.Traffic.Enums;
using System.Linq;

namespace InfraSim.Services.Traffic.Classes
{
    public class FullTrafficRouting : TrafficRouting
    {
        private readonly ServerType _serverType;

        public FullTrafficRouting(List<IServer> servers, ServerType serverType) : base(servers)
        {
        }

        protected override int CalculateRequests(int totalRequests)
        {
            return totalRequests;
        }

        protected override List<IServer> ObtainTargetServers()
        {
            return Servers.Where(s => s.ServerType == ServerType.LoadBalancer 
                                    || s.ServerType == ServerType.Server).ToList();
        }
    }
}