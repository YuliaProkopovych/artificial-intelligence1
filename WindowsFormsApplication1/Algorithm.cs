using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class PSA
    {
        class Algorism
        {
            bool switcher = true;
            Problem problem;
            Graph graph;
            public void setProblem(Problem p)
            {
                problem = p;
            }
            public Problem Run(GraphView form, Graph g)
            {
                graph = g;

                if(!problem.isSolved())
                {
                    if (switcher)
                    {
                        KeyValuePair<Queue<List<Edge>>, HashSet<string>> kv = SearchStep(problem.ways1, problem.checked1);
                        problem.ways1 = kv.Key;
                        problem.checked1 = kv.Value;
                        switcher = false;
                    }
                    else
                    {
                        KeyValuePair<Queue<List<Edge>>, HashSet<string>> kv = SearchStep(problem.ways2, problem.checked2);
                        problem.ways2 = kv.Key;
                        problem.checked2 = kv.Value;
                        switcher = true;
                    }
                    form.graphRedraw(problem);
                }
                return problem;
            }
            public KeyValuePair<Queue<List<Edge>>, HashSet<string>> SearchStep(Queue<List<Edge>> front, HashSet<string> h)
            {
                Queue<List<Edge>> newFront = new Queue<List<Edge>>();
                while (front.Count > 0)
                {
                    List<Edge> oldWay = front.Dequeue();
                    string p = oldWay.Last().label2;
                    h.Add(p);
                    foreach (string n in graph.findNeighbors(p))
                    {
                        if (!h.Contains(n))
                        {
                            List<Edge> newWay = new List<Edge>();
                            newWay.InsertRange(0,oldWay);
                            newWay.Add(new Edge(oldWay.Last().label2, n));
                            newFront.Enqueue(newWay);

                        }
                        h.Add(n);
                    }
                }
                return new KeyValuePair<Queue<List<Edge>>, HashSet<string>>(newFront, h);
            }
        }
    }
}
