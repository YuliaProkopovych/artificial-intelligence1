using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class Problem
    {
        public GraphView form;
        public string point1;
        public string point2;
        public Queue<List<Edge>> ways1;
        public Queue<List<Edge>> ways2;
        public HashSet<string> checked1;
        public HashSet<string> checked2;
        public string result = " ";
        public bool isSolved()
        {
            if (front1().Intersect(front2()).Count() != 0)
            {
                result = front1().Intersect(front2()).Last();
            }
            return front1().Intersect(front2()).Count() != 0;
        }
        public List<Edge> getWays()
        {
            List<Edge> rez = new List<Edge>();
            for (int j = 0; j < ways1.Count; j++)
            {
                if (ways1.ElementAt(j).Last().label2 == result)
                {
                    rez=(ways1.ElementAt(j));
                }
            }
            for (int j = 0; j < ways2.Count; j++)
            {
                if (ways2.ElementAt(j).Last().label2 == result)
                {
                    for (int i = ways2.ElementAt(j).Count-1; i >= 0; i--)
                    {
                        rez.Add(ways2.ElementAt(j)[i]);
                    }
                    break;
                }
            }
            return rez;
        }
        public Queue<string> front1()
        {
            Queue<string> rez = new Queue<string>();
            rez.Enqueue(point1);
            for (int i = 0; i < ways1.Count; i++)
            {
                rez.Enqueue(ways1.ElementAt(i).Last().label2);
            }
            return rez;
        }
        public Queue<string> front2()
        {
            Queue<string> rez = new Queue<string>();
            rez.Enqueue(point2);
            for (int i = 0; i < ways2.Count; i++)
            {
                rez.Enqueue(ways2.ElementAt(i).Last().label2);
            }
            return rez;
        }
    }
    public class Graph
    {
        List<Edge> edges;
        public Microsoft.Glee.Drawing.Graph getDrawable()
        {
            Microsoft.Glee.Drawing.Graph graph = new Microsoft.Glee.Drawing.Graph("graph");
            for (int i = 0; i < edges.Count; i++)
            {
                graph.AddEdge(edges[i].label1, edges[i].label2);
                graph.Edges.Last().Attr.Id = edges[i].label1 + edges[i].label2;
                graph.Directed = false;
            }
            return graph;
        }
        public List<string> findNeighbors(string a)
        {
            List<string> rez = new List<string>();
            foreach (Edge edge in edges)
            {
                if (edge.label1 == a || edge.label2 == a)
                {
                    rez.Add(edge.getOther(a));
                }
            }
            return rez;
        }
        public Graph()
        {
            edges = new List<Edge>();
        }
        public void ReadEdges(string file)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            int t = path.Count();
            int d = path.LastIndexOf("\\");
            path = path.Remove(d, t - d);
            t = path.Count();
            d = path.LastIndexOf("\\");
            path = path.Remove(d, t - d);
            t = path.Count();
            d = path.LastIndexOf("\\");
            path = path.Remove(d, t - d);
            path = Path.Combine(path, file);
            System.IO.StreamReader myFile = new System.IO.StreamReader(path);
            while (!myFile.EndOfStream)
            {
                string edge = myFile.ReadLine();
                Edge e = new Edge(edge.Split(' ')[0], edge.Split(' ')[1], double.Parse(edge.Split(' ')[2]));
                edges.Add(e);
            }
            myFile.Close();
        }
        public double edgeExists(string a, string b)
        {
            double rez = -1;
            Edge current = new Edge(a, b, 0);
            for (int i = 0; i < edges.Count; i++)
            {
                if (edges[i].Equals(current))
                {
                    rez = edges[i].weight;
                }
            }
            return rez;
        }
    }
}
public class Edge
{
    public string label1;
    public string label2;
    public double weight;
    public Edge(string l1, string l2, double w)
    {
        label1 = l1;
        label2 = l2;
        weight = w;
    }
    public Edge(string l1, string l2)
    {
        label1 = l1;
        label2 = l2;
        weight = 0;
    }
    public string getOther(string a)
    {
        if (a != label1)
            return label1;
        else
            return label2;
    }
    public bool isEqual(Edge other)
    {
        if ((label1 == other.label1 && label2 == other.label2) || (label1 == other.label2 && label2 == other.label1))
            return true;
        else return false;
    }
}

 
