//Programmieraufgabe der AIT GmbH gelöst von Fabian Zaiser am 31.07.2023
using System;

namespace Streckennetz
{
    public class Program
    {
        public static void Main(string[] args)
        {
            char[] nodes = { 'A', 'B', 'C', 'D', 'E' };
            var graph = new Graph(nodes);
            graph.AddEdge('A', 'B', 5);
            graph.AddEdge('B', 'C', 4);
            graph.AddEdge('C', 'D', 8);
            graph.AddEdge('D', 'C', 8);
            graph.AddEdge('D', 'E', 6);
            graph.AddEdge('A', 'D', 5);
            graph.AddEdge('C', 'E', 2);
            graph.AddEdge('E', 'B', 3);
            graph.AddEdge('A', 'E', 7);

            for (var i = 0; i < nodes.Length; i++) graph.AddEdge(nodes[i], nodes[i], 0);

            // Testabfragen
            Console.WriteLine("1. Länge der Route A-B-C: " +
                              MyStringConverter(graph.GetDistanceAlongRoute(new[] { 'A', 'B', 'C' })));
            Console.WriteLine("2. Länge der Route A-D: " +
                              MyStringConverter(graph.GetDistanceAlongRoute(new[] { 'A', 'D' })));
            Console.WriteLine("3. Länge der Route A-D-C: " +
                              MyStringConverter(graph.GetDistanceAlongRoute(new[] { 'A', 'D', 'C' })));
            Console.WriteLine("4. Länge der Route A-E-B-C-D: " +
                              MyStringConverter(graph.GetDistanceAlongRoute(new[] { 'A', 'E', 'B', 'C', 'D' })));
            Console.WriteLine("5. Länge der Route A-E-D: " +
                              MyStringConverter(graph.GetDistanceAlongRoute(new[] { 'A', 'E', 'D' })));
            Console.WriteLine("6. Anzahl der Routen, die bei C anfangen und auch bei C wieder enden und die nicht mehr als 3 Stopps haben: " +
                              MyStringConverter(graph.GetNumRoutesBetween('C', 'C', 3)));
            Console.WriteLine("7. Anzahl der Routen, die bei A anfangen und bei C enden mit exakt 4 Stopps: " +
                              MyStringConverter(graph.GetNumRoutesBetween('A', 'C', 4, true)));
            Console.WriteLine("8. Länge der kürzesten Route von A nach C: " +
                              MyStringConverter(graph.GetShortestRouteLength('A', 'C')));
            Console.WriteLine("9. Länge der kürzesten Route von B nach B: " +
                              MyStringConverter(graph.GetShortestRouteLength('B', 'B')));
            Console.WriteLine("10. Anzahl von Routen von C nach C mit einer Länge < 30: " +
                              MyStringConverter(graph.GetNumRoutesWithMaxDistance('C', 'C', 30)));
            Console.WriteLine("Drücken Sie eine beliebige Taste, um das Programm zu beenden...");
            Console.ReadKey();
        }

        public static string MyStringConverter(int number)
        {
            if (number > 0) return number.ToString();
            return "NO SUCH ROUTE";
        }
    }
}