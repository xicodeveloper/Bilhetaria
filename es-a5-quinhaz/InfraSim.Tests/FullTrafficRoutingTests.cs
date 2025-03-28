using Moq;
using Xunit;
using InfraSim.Services.Traffic.Interfaces;
using InfraSim.Services.Traffic.Classes;
using InfraSim.Services.Traffic.Enums;
using System.Collections.Generic;

namespace InfraSim.Tests
{
    public class FullTrafficRoutingTests
    {
        [Fact]
        public void RouteTraffic_ShouldSendRequestsToCorrectServers()
        {
            // Arrange
            var mockServer1 = new Mock<IServer>();
            mockServer1.Setup(s => s.ServerType).Returns(ServerType.Server);

            var mockServer2 = new Mock<IServer>();
            mockServer2.Setup(s => s.ServerType).Returns(ServerType.CDN);

            var servers = new List<IServer> { mockServer1.Object, mockServer2.Object };
            var fullTrafficRouting = new FullTrafficRouting(servers, ServerType.Server);

            // Act
            fullTrafficRouting.RouteTraffic(100);

            // Assert
            mockServer1.Verify(s => s.HandleRequests(100), Times.Once);
            mockServer2.Verify(s => s.HandleRequests(It.IsAny<int>()), Times.Never);
        }
    }
}