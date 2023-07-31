using Streckennetz;
using Xunit;

namespace Tester
{
    public class UnitTest1
    {
        [Fact]
        public void CreatingGraphTest()
        {
            // Arrange
            char[] nodes = { 'A', 'B' };
            var graph = new Graph(nodes);
            int[,] correctReachability = { { 0, 1 }, { 0, 0 } };

            // Act
            graph.AddEdge('A', 'B', 1);

            // Assert
            Assert.Equal(correctReachability, graph.GetReachabilityMatrix());
        }

        [Fact]
        public void TestGetDistanceAlongRoute_ValidRoute()
        {
            // Arrange
            char[] nodes = { 'A', 'B', 'C', 'D', 'E' };
            var graph = new Graph(nodes);
            graph.AddEdge('A', 'B', 5);
            graph.AddEdge('B', 'C', 4);
            graph.AddEdge('C', 'D', 8);

            // Act
            var distance = graph.GetDistanceAlongRoute(new[] { 'A', 'B', 'C' });

            // Assert
            Assert.Equal(9, distance);
        }

        [Fact]
        public void TestGetDistanceAlongRoute_ValidRoute_MultipleUsageOfNodes()
        {
            // Arrange
            char[] nodes = { 'A', 'B', 'C', 'D', 'E' };
            var graph = new Graph(nodes);
            graph.AddEdge('A', 'B', 5);
            graph.AddEdge('B', 'C', 4);
            graph.AddEdge('C', 'B', 6);
            graph.AddEdge('C', 'D', 8);

            // Act
            var distance = graph.GetDistanceAlongRoute(new[] { 'A', 'B', 'C', 'B', 'C' });

            // Assert
            Assert.Equal(19, distance);
        }

        [Fact]
        public void TestGetDistanceAlongRoute_InvalidRoute()
        {
            // Arrange
            char[] nodes = { 'A', 'B', 'C', 'D', 'E' };
            var graph = new Graph(nodes);
            graph.AddEdge('A', 'B', 5);
            graph.AddEdge('B', 'C', 4);
            graph.AddEdge('C', 'D', 8);

            // Act
            var distance = graph.GetDistanceAlongRoute(new[] { 'A', 'D', 'E' });

            // Assert
            Assert.Equal(-1, distance);
        }

        [Fact]
        public void TestGetDistanceAlongRoute_InvalidRoute_StartAndEndEqual()
        {
            // Arrange
            char[] nodes = { 'A', 'B', 'C', 'D', 'E' };
            var graph = new Graph(nodes);
            graph.AddEdge('A', 'B', 5);
            graph.AddEdge('B', 'C', 4);
            graph.AddEdge('C', 'D', 8);

            // Act
            var distance = graph.GetDistanceAlongRoute(new[] { 'A', 'A' });

            // Assert
            Assert.Equal(-1, distance);
        }

        [Fact]
        public void TestGetNumRoutesBetween_ValidRoute()
        {
            // Arrange
            char[] nodes = { 'A', 'B', 'C', 'D', 'E' };
            var graph = new Graph(nodes);
            graph.AddEdge('A', 'B', 5);
            graph.AddEdge('B', 'C', 4);
            graph.AddEdge('C', 'D', 8);

            // Act
            var numRoutes = graph.GetNumRoutesBetween('A', 'D', 4);

            // Assert
            Assert.Equal(1, numRoutes);
        }

        [Fact]
        public void TestGetNumRoutesBetween_InvalidRoute()
        {
            // Arrange
            char[] nodes = { 'A', 'B', 'C', 'D', 'E' };
            var graph = new Graph(nodes);
            graph.AddEdge('A', 'B', 5);
            graph.AddEdge('B', 'C', 4);
            graph.AddEdge('C', 'D', 8);

            // Act
            var numRoutes = graph.GetNumRoutesBetween('A', 'E', 4);

            // Assert
            Assert.Equal(-1, numRoutes);
        }

        [Fact]
        public void TestGetNumRoutesBetween_InvalidRoute_StartAndEndEqual()
        {
            // Arrange
            char[] nodes = { 'A', 'B', 'C', 'D', 'E' };
            var graph = new Graph(nodes);
            graph.AddEdge('A', 'B', 5);
            graph.AddEdge('B', 'C', 4);
            graph.AddEdge('C', 'D', 8);

            // Act
            var numRoutes = graph.GetNumRoutesBetween('A', 'A', 4);

            // Assert
            Assert.Equal(-1, numRoutes);
        }

        [Fact]
        public void TestGetShortestRouteLength_ValidRoute()
        {
            // Arrange
            char[] nodes = { 'A', 'B', 'C', 'D', 'E' };
            var graph = new Graph(nodes);
            graph.AddEdge('A', 'B', 5);
            graph.AddEdge('B', 'C', 4);
            graph.AddEdge('C', 'D', 8);

            // Act
            var shortestRouteLength = graph.GetShortestRouteLength('A', 'D');

            // Assert
            Assert.Equal(17, shortestRouteLength);
        }

        [Fact]
        public void TestGetShortestRouteLength_InvalidRoute()
        {
            // Arrange
            char[] nodes = { 'A', 'B', 'C', 'D', 'E' };
            var graph = new Graph(nodes);
            graph.AddEdge('A', 'B', 5);
            graph.AddEdge('B', 'C', 4);
            graph.AddEdge('C', 'D', 8);

            // Act
            var shortestRouteLength = graph.GetShortestRouteLength('A', 'E');

            // Assert
            Assert.Equal(-1, shortestRouteLength);
        }

        [Fact]
        public void TestGetNumRoutesWithExactStops_WithOnePossibleRoute()
        {
            // Arrange
            char[] nodes = { 'A', 'B', 'C', 'D', 'E' };
            var graph = new Graph(nodes);
            graph.AddEdge('A', 'B', 5);
            graph.AddEdge('B', 'C', 4);
            graph.AddEdge('C', 'D', 8);
            graph.AddEdge('A', 'D', 5);

            // Act
            var numRoutesExactStops = graph.GetNumRoutesBetween('A', 'D', 3, true);

            // Assert
            Assert.Equal(1, numRoutesExactStops);
        }

        [Fact]
        public void TestGetNumRoutesWithExactStops_WithTwoPossibleRoutes()
        {
            // Arrange
            char[] nodes = { 'A', 'B', 'C', 'D', 'E' };
            var graph = new Graph(nodes);
            graph.AddEdge('A', 'B', 5);
            graph.AddEdge('B', 'C', 4);
            graph.AddEdge('C', 'D', 8);
            graph.AddEdge('A', 'E', 3);
            graph.AddEdge('E', 'C', 2);

            // Act
            var numRoutesExactStops = graph.GetNumRoutesBetween('A', 'C', 2, true);

            // Assert
            Assert.Equal(2, numRoutesExactStops);
        }


        [Fact]
        public void TestGetNumRoutesWithMaxDistance_ValidRoute()
        {
            // Arrange
            char[] nodes = { 'A', 'B', 'C', 'D', 'E' };
            var graph = new Graph(nodes);
            graph.AddEdge('A', 'B', 5);
            graph.AddEdge('B', 'C', 4);
            graph.AddEdge('C', 'D', 8);
            graph.AddEdge('A', 'D', 5);

            // Act
            var numRoutes = graph.GetNumRoutesWithMaxDistance('A', 'D', 10);

            // Assert
            Assert.Equal(1, numRoutes);
        }

        [Fact]
        public void TestGetNumRoutesWithMaxDistance_TwoValidRoutes()
        {
            // Arrange
            char[] nodes = { 'A', 'B', 'C', 'D', 'E' };
            var graph = new Graph(nodes);
            graph.AddEdge('A', 'B', 5);
            graph.AddEdge('B', 'C', 4);
            graph.AddEdge('C', 'D', 8);
            graph.AddEdge('A', 'D', 5);

            // Act
            var numRoutes = graph.GetNumRoutesWithMaxDistance('A', 'D', 18);

            // Assert
            Assert.Equal(2, numRoutes);
        }

        [Fact]
        public void TestGetNumRoutesWithMaxDistance_InvalidRoute()
        {
            // Arrange
            char[] nodes = { 'A', 'B', 'C', 'D', 'E' };
            var graph = new Graph(nodes);
            graph.AddEdge('A', 'B', 5);
            graph.AddEdge('B', 'C', 4);
            graph.AddEdge('C', 'D', 8);

            // Act
            var numRoutes = graph.GetNumRoutesWithMaxDistance('A', 'D', 8);

            // Assert
            Assert.Equal(-1, numRoutes);
        }

        [Fact]
        public void TestGetNumRoutesWithMaxDistance_InvalidRoute_StartAndEndEqualWithoutLoop()
        {
            // Arrange
            char[] nodes = { 'A', 'B', 'C', 'D', 'E' };
            var graph = new Graph(nodes);
            graph.AddEdge('A', 'B', 5);
            graph.AddEdge('B', 'C', 4);
            graph.AddEdge('C', 'D', 8);

            // Act
            var numRoutes = graph.GetNumRoutesWithMaxDistance('A', 'A', 8);

            // Assert
            Assert.Equal(-1, numRoutes);
        }

        [Fact]
        public void TestGetNumRoutesWithMaxDistance_ValidRoute_StartAndEndEqualWithLoop()
        {
            // Arrange
            char[] nodes = { 'A', 'B', 'C', 'D', 'E' };
            var graph = new Graph(nodes);
            graph.AddEdge('A', 'B', 5);
            graph.AddEdge('B', 'C', 4);
            graph.AddEdge('C', 'D', 8);
            graph.AddEdge('D', 'A', 10);

            // Act
            var numRoutes = graph.GetNumRoutesWithMaxDistance('A', 'A', 55);

            // Assert
            Assert.Equal(2, numRoutes);
        }
    }
}