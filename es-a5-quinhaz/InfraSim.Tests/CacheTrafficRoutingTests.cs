using Moq;
using InfraSim.Services.Traffic.Interfaces;
using InfraSim.Services.Traffic.Classes;
using InfraSim.Services.Traffic.Enums;
using Xunit;

namespace InfraSim.Tests
{
    public class CacheTrafficRoutingTests
    {
        [Fact]
        public void RouteTraffic_ShouldSendRequestsOnlyToCacheServers()
        {
            // Arrange
            var mockServer1 = new Mock<IServer>();
            mockServer1.Setup(s => s.ServerType).Returns(ServerType.Cache); // Configura o ServerType como Cache

            var mockServer2 = new Mock<IServer>();
            mockServer2.Setup(s => s.ServerType).Returns(ServerType.Server);

            var servers = new List<IServer> { mockServer1.Object, mockServer2.Object };
            var cacheTrafficRouting = new CacheTrafficRouting(servers);

            // Act
            cacheTrafficRouting.RouteTraffic(100);

            // Assert
            mockServer1.Verify(s => s.HandleRequests(100), Times.Once); // Verifica se o servidor do tipo Cache recebeu as solicitações
            mockServer2.Verify(s => s.HandleRequests(It.IsAny<int>()), Times.Never); // Verifica se o servidor do tipo Server não recebeu solicitações
        }
    }
}