using Moq;
using Xunit;
using InfraSim.Services.Traffic.Interfaces;
using InfraSim.Services.Traffic.Classes;
using InfraSim.Services.Traffic.Enums;
using System.Collections.Generic;

namespace InfraSim.Tests
{
    public class TrafficRoutingTests
    {
        [Fact]
        public void CalculateRequests_ShouldReturnCorrectRequestCount()
        {
            // Arrange
            var mockServer1 = new Mock<IServer>();
            var mockServer2 = new Mock<IServer>();
            var servers = new List<IServer> { mockServer1.Object, mockServer2.Object };
            var trafficRouting = new FullTrafficRouting(servers, ServerType.Server);

            // Act
            var result = trafficRouting.CalculateRequests(100);

            // Assert
            Assert.Equal(100, result);
        }

        [Fact]
        public void RouteTraffic_ShouldDistributeRequestsCorrectly()
        {
            // Arrange
            var mockServer1 = new Mock<IServer>();
            mockServer1.Setup(s => s.ServerType).Returns(ServerType.Server);

            var mockServer2 = new Mock<IServer>();
            mockServer2.Setup(s => s.ServerType).Returns(ServerType.Server);

            var servers = new List<IServer> { mockServer1.Object, mockServer2.Object };
            var trafficRouting = new FullTrafficRouting(servers, ServerType.Server);

            int totalRequests = 100;
            int expectedRequestsPerServer = totalRequests / servers.Count;

            // Act
            trafficRouting.RouteTraffic(totalRequests);

            // Assert
            mockServer1.Verify(s => s.HandleRequests(expectedRequestsPerServer), Times.Once);
            mockServer2.Verify(s => s.HandleRequests(expectedRequestsPerServer), Times.Once);
        }
    }
}