using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security;

namespace Assignment6
{

    //class for the Vertexes in the Graph
    public class Vertex
    {
        public Vertex(string name)
        {
            Name = name;
            Visited = false;
            Distance = 0;
            Predecessor = null;
            neighborNodes = new Dictionary<string, Vertex>();
        }

        public Dictionary<string, Vertex> neighborNodes;
        public string Name;
        public Boolean Visited;
        public long Distance;
        public Vertex Predecessor;
        public int pos;

        public void AddNeighbor(Vertex neighbor)
        {
            if (neighborNodes.ContainsKey(neighbor.Name))
            {
                throw new ArgumentException("Edge already exists");
            }

            neighborNodes.Add(neighbor.Name, neighbor);
        }

        public Boolean CheckNeighbor(Vertex neighbor)
        {
            if (neighborNodes.ContainsKey(neighbor.Name)) return true;
            return false;
        }

        public IEnumerable<string> Neighbors()
        {
            for (int x = 0; x < neighborNodes.Count; x++)
            {
                yield return neighborNodes.ElementAt(x).Key;
            }
        }

    }

    //class for the actual graph, holds all the vertexes and algorithms.
    public class UndirectedGraph
    {

        public UndirectedGraph()
        {
            Vertexes = new Dictionary<string, Vertex>();
        }

        private Dictionary<string, Vertex> Vertexes;

        //functions to help with testing
        public int Size()
        {
            return Vertexes.Count;
        }

        public Vertex GetVertex(string name)
        {
            return Vertexes[name];
        }

        public Boolean HasVertex(string name)
        {
            return Vertexes.ContainsKey(name);
        }

        public void AddVertex(string name)
        {
            if (Vertexes.ContainsKey(name))
            {
                throw new ArgumentException("Node already exists");
            }
            else
            {
                Vertexes.Add(name, new Vertex(name));
            }
        }


        public IEnumerable<string> Vertices()
        {
            for (int x = 0; x < Size(); x++)
            {
                yield return Vertexes.ElementAt(x).Key;
            }
        }

        public void AddEdge(string node1, string node2)
        {
            if (!Vertexes.ContainsKey(node1) || !Vertexes.ContainsKey(node2))
            {
                throw new ArgumentException("No such node");
            }

            Vertexes[node1].AddNeighbor(Vertexes[node2]);
            Vertexes[node2].AddNeighbor(Vertexes[node1]);
        }

        public IEnumerable<string> Neighbors(string node)
        {
            if (Vertexes.ContainsKey(node))
            {
                return Vertexes[node].Neighbors();
            }

            throw new ArgumentException("No such node");
        }

        public static UndirectedGraph ReadFile(string path)
        {
            UndirectedGraph ug = new UndirectedGraph();

            string[] edges = File.ReadAllLines(path);

            string[] temp;

            for (int x = 0; x < edges.Length; x++)
            {
                temp = edges[x].Split(new char[] {' '});

                if (temp.Length != 2 || temp[0] == null || temp[1] == null)
                {
                    throw new ArgumentException("Invalid Format");
                }

                if (temp[0] == temp[1])
                {
                    ug.AddVertex(temp[0]);
                }
                else
                {
                    if (!ug.HasVertex(temp[0])) ug.AddVertex(temp[0]);
                    if (!ug.HasVertex(temp[1])) ug.AddVertex(temp[1]);

                    ug.AddEdge(temp[0], temp[1]);
                }
            }

            return ug;
        }

        public List<string> ShortestPath(string Start, string end)
        {
            if (!Vertexes.ContainsKey(Start) || !Vertexes.ContainsKey(end))
            {
                throw new ArgumentException("No such node");
            }

            if (Start == end) return new List<string> {"end"};

            for (int x = 0; x < Vertexes.Count; x++)
            {
                Vertex vt = Vertexes.ElementAt(x).Value;
                vt.Visited = false;
                vt.Distance = 1000000000000;
            }

            Vertex start = Vertexes[Start];
            start.Distance = 0;
            start.Predecessor = null;
            start.Visited = true;

            PriorityQueue q = new PriorityQueue();

            for (int x = 0; x < Vertexes.Count; x++)
            {
                q.Insert(Vertexes.ElementAt(x).Value);
            }

            Vertex node = null;

            while (q.Count != 0)
            {
                node = q.ExtractMin();
                long dist = node.Distance;

                if (node.Name == end)
                {
                    if (node.Predecessor == null) return null;
                    return Retrace(node);
                }

                foreach (Vertex v in node.neighborNodes.Values)
                {
                    string name = v.Name;
                    if (v.Distance > node.Distance + 1)
                    {
                        q.DecreaseKey(v, node.Distance + 1);
                        v.Predecessor = node;
                    }
                }
            }

            return null;
        }

        public int Distance(string node1, string node2)
        {
            List<string> path = ShortestPath(node1, node2);
            if (path == null) return -1;
            return path.Count - 1;
        }

        private List<string> Retrace(Vertex node)
        {
            Stack<Vertex> reversePath = new Stack<Vertex>();

            while (node.Predecessor != null)
            {
                reversePath.Push(node);
                node = node.Predecessor;
            }

            List<string> path = new List<string>();
            while (reversePath.Count != 0)
            {
                path.Add(node.Name);
                node = reversePath.Pop();
            }

            path.Add(node.Name);

            return path;
        }


    }


    //Class for the PriorityQueue Structure

    public class PriorityQueue
    {
        public PriorityQueue()
        {
            Count = 0;
            q = new Vertex[100000];
        }

        public int Count;
        private Vertex[] q;

        public void Insert(Vertex v)
        {
            q[Count] = v;
            Count++;
            Sort();
        }

        public Vertex ExtractMin()
        {
            Count--;
            return q[Count];
        }

        public void DecreaseKey(Vertex v, long newDistance)
        {
            q[v.pos].Distance = newDistance;
            Sort();
        }

        private void Sort()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                for (int j = 0; j < Count - i - 1; j++)
                {
                    if (q[j].Distance < q[j + 1].Distance)
                    {
                        Vertex temp = q[j];
                        q[j] = q[j + 1];
                        q[j + 1] = temp;
                    }
                }
            }

            for (int x = 0; x < Count; x++)
            {
                q[x].pos = x;
            }
        }
    }

}
