using System;

namespace Streckennetz
{
    public class Graph
    {
        private readonly char[] nodes; //array containing nodes
        private readonly int[,] reachabilityMatrix; //2D-Array for reachability

        public Graph(char[] nodes)
        {
            reachabilityMatrix = new int[nodes.Length, nodes.Length];
            this.nodes = nodes;
        }

        //method for defining edges between nodes
        public void AddEdge(char start, char end, int length)
        {
            var startIndex = Array.IndexOf(nodes, start);
            var endIndex = Array.IndexOf(nodes, end);
            reachabilityMatrix[startIndex, endIndex] = length;
        }

        //method for calculating length of a given route
        public int GetDistanceAlongRoute(char[] route) //function gets route as char array
        {
            var distance = 0;
            for (var i = 0; i < route.Length - 1; i++) //looks if the next node is reachable from current node
            {
                var startIndex = Array.IndexOf(nodes, route[i]);
                var endIndex = Array.IndexOf(nodes, route[i + 1]);

                if (reachabilityMatrix[startIndex, endIndex] == 0) //if not return -1 (not reachable)
                    return -1;

                distance += reachabilityMatrix[startIndex, endIndex]; //if there is a connection add the distance
            }

            return distance;
        }

        //method for calculating number of routes between nodes either with max number of stops or exact number of stops
        public int GetNumRoutesBetween(char start, char end, int stops, bool exactStops = false)
        {
            var startIndex = Array.IndexOf(nodes, start);
            var endIndex = Array.IndexOf(nodes, end);
            var result = GetNumRoutesBetweenRecursive(startIndex, endIndex, stops, 0, exactStops);

            return result == 0 ? -1 : result; //if no routes found return -1 else return result
        }

        private int GetNumRoutesBetweenRecursive(int current, int end, int stops, int currentStops, bool exactStops)
        {
            var numRoutes = 0;
            if (current == end && currentStops > 0 &&
                (!exactStops || currentStops == stops)) //return 1 when end is reached and we moved
                return 1;

            if (currentStops >= stops) return 0; //no route between start and end for given max stops

            for (var i = 0;
                 i < nodes.Length;
                 i++) //loop searching for routes by trying every route and counting routes ending at given end node
                if (reachabilityMatrix[current, i] > 0) //looking for reachable nodes and try finding a route to end
                    numRoutes += GetNumRoutesBetweenRecursive(i, end, stops, currentStops + 1, exactStops);

            return numRoutes;
        }

        // method for calculating number of routes between nodes with a given max length
        public int GetNumRoutesWithMaxDistance(char start, char end, int maxDistance)
        {
            var startIndex = Array.IndexOf(nodes, start);
            var endIndex = Array.IndexOf(nodes, end);
            var result = GetNumRoutesWithMaxDistanceRecursive(startIndex, endIndex, maxDistance, 0);

            return result == 0 ? -1 : result; // if no routes found return -1 else return result
        }

        private int GetNumRoutesWithMaxDistanceRecursive(int current, int end, int maxDistance, int currentDistance)
        {
            var numRoutes = 0;
            if (current == end && currentDistance > 0 && currentDistance < maxDistance)
                numRoutes++; //count as possible route when end is reached,  we moved and the distance didn't reach max distance

            if (currentDistance >= maxDistance)
                return numRoutes; //when max distance is exceeded return amount of routes we found so far

            for (var i = 0;
                 i < nodes.Length;
                 i++) //loop searching for routes by trying every route and counting routes ending at given end node
            {
                var distance = reachabilityMatrix[current, i];
                if (distance > 0) //checking for reachability else try next node with loop
                    numRoutes += GetNumRoutesWithMaxDistanceRecursive(i, end, maxDistance, currentDistance + distance);
            }

            return numRoutes;
        }


        // method for calculating shortest route between to given nodes (Dijkstra-Algorithmus)
        public int GetShortestRouteLength(char start, char end)
        {
            var startIndex = Array.IndexOf(nodes, start);
            var endIndex = Array.IndexOf(nodes, end);
            var size = nodes.Length;
            var minDistance = int.MaxValue;
            var distances = new int[size]; // create an array to store the distances from the starting node to each node
            var doneNodes = new bool[size]; // create a boolean array to track which nodes have been processed

            if (startIndex == endIndex) //start node is the same as end node edge case
            {
                for (var i = 0; i < nodes.Length; i++)
                    if (i != startIndex && reachabilityMatrix[startIndex, i] > 0)
                    {
                        var distance = GetShortestRouteLength(nodes[i], end);
                        if (distance > 0)
                            minDistance = Math.Min(minDistance, reachabilityMatrix[startIndex, i] + distance);
                    }

                return minDistance == int.MaxValue ? -1 : minDistance;
            }


            for (var i = 0; i < size; i++)
                distances[i] = int.MaxValue; //initialize distances array for dijkstra algorithm

            distances[startIndex] = 0; //start with starting node

            //run the Dijkstra algorithm to find the shortest distances from the starting node to all other nodes
            for (var count = 0; count < size - 1; count++)
            {
                var u = MinDistance(distances, doneNodes); // get index of node with shortest distance 
                doneNodes[u] = true; //mark node as done

                //update distances to all adjacent nodes of 'u' that are not processed yet and have a shorter path
                for (var v = 0; v < size; v++)
                    if (!doneNodes[v] && reachabilityMatrix[u, v] > 0 && distances[u] != int.MaxValue
                        && distances[u] + reachabilityMatrix[u, v] < distances[v])
                        distances[v] = distances[u] + reachabilityMatrix[u, v]; //add length of reachable nodes where the route would be shorter to length to get to current node 
            }

            return distances[endIndex] == int.MaxValue ? -1 : distances[endIndex];
        }

        private int
            MinDistance(int[] distances,
                bool[] doneNodes) //get index of node with shortest distance to reach and was not already done
        {
            var min = int.MaxValue;
            var minIndex = -1;

            for (var v = 0; v < nodes.Length; v++)
                if (!doneNodes[v] && distances[v] <= min)
                {
                    min = distances[v];
                    minIndex = v;
                }

            return minIndex;
        }

        public int[,] GetReachabilityMatrix()
        {
            return reachabilityMatrix;
        }
    }
}