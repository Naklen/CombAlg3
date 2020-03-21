using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace CombAlg3
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = GetStartData();
            Console.WriteLine("Vertex count: " + data.Item2);
            Console.WriteLine($"Start: {data.Item3} Target: {data.Item4}");
            foreach (var edge in data.Item1)
                Console.WriteLine(edge.ToString());
            Console.ReadKey();

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
                    for (int i = 1; i <= vertexCount; i++)
                    {
                        var incidentVertex = sr.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                        for (int j = 1; j <= vertexCount; j++)
                        {
                            if (incidentVertex[j - 1] > -32768)
                                edges.Add(new Edge(i, j, incidentVertex[j - 1]));
                        }
                    }
                    start = int.Parse(sr.ReadLine());
                    target = int.Parse(sr.ReadLine());
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