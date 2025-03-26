using InfraSim.Services.Traffic.Interfaces;
using InfraSim.Services.Traffic.Enums;
using System.Collections.Generic;

namespace InfraSim.Services.Traffic.Classes
{
    public class TrafficCoordinator
    {
        public void RouteTraffic(int totalRequests, List<IServer> servers)
        {
            int remaining = totalRequests;
            
            // 70% for CDN
            var cdnRouter = new CDNTrafficRouting(servers);
            int cdnRequests = (int)(totalRequests * 0.7);
            cdnRouter.RouteTraffic(cdnRequests);
            remaining -= cdnRequests;

            // 80% of remaining for Cache
            var cacheRouter = new CacheTrafficRouting(servers);
            int cacheRequests = (int)(remaining * 0.8);
            cacheRouter.RouteTraffic(cacheRequests);
            remaining -= cacheRequests;

            // Rest for normal servers
            var fullRouter = new FullTrafficRouting(servers, ServerType.Server);
            fullRouter.RouteTraffic(remaining);
        }
    }
}