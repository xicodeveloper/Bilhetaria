// classe abstrata que implementa a interface ITrafficRouting
// e define o routing de solicitações para servidores

// é "pai" de FullTrafficRouting, CacheTrafficRouting e CDNTrafcRouting
// que implementam o método abstrato RouteTraffic

using System.Collections.Generic;
using InfraSim.Services.Traffic.Interfaces;

namespace InfraSim.Services.Traffic.Classes
{
    public abstract class TrafficRouting : ITrafficRouting
    {
        protected readonly List<IServer> Servers;

        public TrafficRouting(List<IServer> servers)
        {
            Servers = servers ?? new List<IServer>();
        }

        // abstrato
        // public abstract void RouteTraffic(int requestsCount);
        public void RouteTraffic(int requestCount)
        {
            int requestsToDistribute = CalculateRequests(requestCount);
            List<IServer> targetServers = ObtainTargetServers();
            SendRequestsToServers(requestsToDistribute, targetServers);
        }

        // protegidos
        protected abstract int CalculateRequests(int totalRequests);
        protected abstract List<IServer> ObtainTargetServers();
        protected virtual void SendRequestsToServers(int requestsCount, List<IServer> targetServers)
        {
            if (targetServers == null || targetServers.Count == 0) return;

            int baseRequests = requestsCount / targetServers.Count;
            int remainder = requestsCount % targetServers.Count;

            for (int i = 0; i < targetServers.Count; i++)
            {
                int requestsForServer = baseRequests + (i < remainder ? 1 : 0);
                targetServers[i].HandleRequests(requestsForServer);
            }
        }
    }
}