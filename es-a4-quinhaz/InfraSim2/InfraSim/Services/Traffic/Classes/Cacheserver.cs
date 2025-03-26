// classe especializada para Cache Servers
// implementa a interface IServer

using InfraSim.Services.Traffic.Interfaces;
using InfraSim.Services.Traffic.Enums;

namespace InfraSim.Services.Traffic.Classes
{
    public class CacheServer : IServer
    {
        public ServerType ServerType => ServerType.Cache;
        public int RequestsCount { get; set; }

        public void HandleRequests(int requests)
        {
            RequestsCount += requests;
        }
    }
}