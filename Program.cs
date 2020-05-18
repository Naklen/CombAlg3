using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.InteropServices;

namespace CombAlg3
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = GetStartData();
            Console.WriteLine("Vertex count: " + data.Item2);
            Console.WriteLine($"Start: {data.Item3 + 1} Target: {data.Item4 + 1}");
            foreach (var edge in data.Item1)
                Console.WriteLine(edge.ToString());
            Console.WriteLine($"Path from {data.Item3 + 1} to {data.Item4 + 1}:");
            Console.WriteLine(GetMinPath(data));
            Console.ReadKey();

        }

        static string GetMinPath(Tuple<List<Edge>, int, int, int> data)
        {
            var edges = data.Item1;
            var vCount = data.Item2;
            var start = data.Item3;
            var target = data.Item4;
            var minCosts = Enumerable.Repeat(int.MaxValue, vCount).ToArray();
            var previous = Enumerable.Repeat(-1, vCount).ToArray();
            minCosts[start] = 0;
            while (true)
            {
                var cont = false;
                for (int i = 0; i < edges.Count; i++)
                    if (minCosts[edges[i].From] < int.MaxValue)
                        if (minCosts[edges[i].To] > minCosts[edges[i].From] + edges[i].Cost)
                        { 
                            minCosts[edges[i].To] = minCosts[edges[i].From] + edges[i].Cost;
                            previous[edges[i].To] = edges[i].From;
                            cont = true;
                        }
                if (!cont) break;
            }
            if (minCosts[target] == int.MaxValue)
                return null;
            var reversedPath = new Queue<int>();
            var cur = target;
            while (cur != -1)
            {
                reversedPath.Enqueue(cur);
                cur = previous[cur];
            }
            var result = "";
            var path = reversedPath.Reverse();
            foreach (var v in path)
                result += (v + 1) + " ";
            result = result.Substring(0, result.Length - 1);
            return result;
        }

        static Tuple<List<Edge>, int, int, int> GetStartData()
        {
            var edges = new List<Edge>();
            var vertexCount = 0;
            var start = 0;
            var target = 0;
            try
            {
                using (var sr = new StreamReader("./in.txt"))
                {
                    vertexCount = int.Parse(sr.ReadLine());
                    for (int i = 0; i < vertexCount; i++)
                    {
                        var incidentVertex = sr.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                        for (int j = 0; j < vertexCount; j++)
                        {
                            if (incidentVertex[j] > -32768)
                                edges.Add(new Edge(i, j, incidentVertex[j]));
                        }
                    }
                    start = int.Parse(sr.ReadLine()) - 1;
                    target = int.Parse(sr.ReadLine()) - 1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Environment.Exit(1);
            }
            return new Tuple<List<Edge>, int, int, int>(edges, vertexCount, start, target);
        }
    }

    class Edge
    {
        public int From { get; }
        public int To { get; }
        public int Cost { get; }

        public Edge(int from, int to, int cost)
        {
            From = from;
            To = to;
            Cost = cost;
        }

        public override string ToString()
        {
            return From + " --> " + To + " Cost: " + Cost;
        }
    }
}