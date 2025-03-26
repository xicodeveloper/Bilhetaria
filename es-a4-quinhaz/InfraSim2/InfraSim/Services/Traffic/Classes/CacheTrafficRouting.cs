// routing de APENAS servidores do tipo Cache
// usando o método SendRequestsToServers para enviar as solicitações para os servidores Cache
// que envia as solicitações para os servidores passados como parâmetro

// Adiciona cálculo proporcional (80%) para servidores Cache


using InfraSim.Services.Traffic.Interfaces;
using InfraSim.Services.Traffic.Enums;
using System.Linq;

namespace InfraSim.Services.Traffic.Classes
{
    public class CacheTrafficRouting : TrafficRouting
    {
        // construtor que recebe servers e chama TrafficRouting
        public CacheTrafficRouting(List<IServer> servers) : base(servers)
        {
        }

        protected override int CalculateRequests(int totalRequests)
        {
            return (int)(totalRequests * 0.8);
        }

        protected override List<IServer> ObtainTargetServers()
        {
            return Servers.Where(s => s.ServerType == ServerType.Cache).ToList();
        }
    }
}