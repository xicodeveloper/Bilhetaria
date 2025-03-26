// routing de APENAS servidores do tipo CDN
// usando o método SendRequestsToServers para enviar as solicitações para os servidores CDN
// que envia as solicitações para os servidores passados como parâmetro

// adiciona cálculo proporcional (70%) para servidores CDN

using InfraSim.Services.Traffic.Interfaces;
using InfraSim.Services.Traffic.Enums;
//using Linq;

namespace InfraSim.Services.Traffic.Classes
{
    public class CDNTrafficRouting : TrafficRouting
    {
        public CDNTrafficRouting(List<IServer> servers) : base(servers) { }

         // 70% do total
        protected override int CalculateRequests(int totalRequests)
        {
            return (int)(totalRequests * 0.7);
        }

        protected override List<IServer> ObtainTargetServers()
        {
            return Servers.Where(s => s.ServerType == ServerType.CDN).ToList();
        }
    }
}