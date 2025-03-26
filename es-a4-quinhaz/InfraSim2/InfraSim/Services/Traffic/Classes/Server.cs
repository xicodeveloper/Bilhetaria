// A IMPLEMENTAÇÃO BASE

// Usando Server (tipo mutável)
//var server1 = new Server { ServerType = ServerType.Cache }; 

// implementation of IServer
// podendo ser herdaddo de CacheServer ou CDNServer

using InfraSim.Services.Traffic.Enums;
using InfraSim.Services.Traffic.Interfaces;

namespace InfraSim.Services.Traffic.Classes
{
    public class Server : IServer
    {
        public int RequestsCount { get; set; } // Número de solicitações
        public ServerType ServerType { get; set; } // Implementação a interface IServer

        public void HandleRequests(int requestsCount)
        {
            RequestsCount += requestsCount;
        }
    }
}