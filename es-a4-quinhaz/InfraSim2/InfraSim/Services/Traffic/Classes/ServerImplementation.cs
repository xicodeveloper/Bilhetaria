// A IMPLEMENTAÇÃO ESPECÍFICA

// Usando ServerImplementation (tipo fixo)
//var server2 = new ServerImplementation(); // Sempre ServerType.Server

// implementação alternativa de IServer (likely a Server.cs)
// implementação padrão de IServer m Program.cs (builder.Services.AddTransient<IServer, ServerImplementation>();)

using InfraSim.Services.Traffic.Interfaces;
using InfraSim.Services.Traffic.Enums;

namespace InfraSim.Services.Traffic.Classes
{
    public class ServerImplementation : IServer
    {
        public ServerType ServerType => ServerType.Server;
        public int RequestsCount { get; set; }

        public void HandleRequests(int requests)
        {
            RequestsCount += requests;
        }
    }
}