// classe especializada para CDN Servers
// implementa a interface IServer

using InfraSim.Services.Traffic.Interfaces;
using InfraSim.Services.Traffic.Enums;

namespace InfraSim.Services.Traffic.Classes
{
    public class CDNServer : IServer
    {
        public ServerType ServerType => ServerType.CDN;
        public int RequestsCount { get; set; }

        public void HandleRequests(int requests)
        {
            RequestsCount += requests;
        }
    }
}


