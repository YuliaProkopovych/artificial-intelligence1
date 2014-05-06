using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class GraphView : Form
    {
        protected Microsoft.Glee.Drawing.Graph graph;
        Microsoft.Glee.GraphViewerGdi.GViewer viewer;
        PSA agent;
        public GraphView()
        {
            InitializeComponent();
            agent = new PSA(this);
        }
        public void setGraph(Microsoft.Glee.Drawing.Graph gr)
        {
            graph = gr;
            viewer = new Microsoft.Glee.GraphViewerGdi.GViewer();
            viewer.Graph = graph;
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            Controls.Add(viewer);
            this.Refresh();
        }
        public void drawWay(List<Edge> way)
        {
            for (int i = 0; i < way.Count; i++)
            {
                graph.FindNode(way.ElementAt(i).label1).Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Green;
                graph.FindNode(way.ElementAt(i).label2).Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Green;

                string id = way.ElementAt(i).label1 + way.ElementAt(i).label2;
                if (graph.EdgeById(id) != null)
                {
                    graph.EdgeById(id).EdgeAttr.Color = Microsoft.Glee.Drawing.Color.YellowGreen;
                }
                else
                {
                    id = way.ElementAt(i).label2 + way.ElementAt(i).label1;
                    if (graph.EdgeById(id) != null)
                    {
                        graph.EdgeById(id).EdgeAttr.Color = Microsoft.Glee.Drawing.Color.YellowGreen;
                    }
                }

            }

            viewer.Graph = graph;
            this.Refresh();
        }
        public void graphRedraw(Problem p)
        {


            for (int i = 0; i < p.ways1.Count; i++)
            {
                for (int j = 0; j < p.ways1.ElementAt(i).Count; j++)
                {
                    string id = p.ways1.ElementAt(i)[j].label1 + p.ways1.ElementAt(i)[j].label2;
                    if (graph.EdgeById(id) != null)
                    {
                        graph.EdgeById(id).EdgeAttr.Color = Microsoft.Glee.Drawing.Color.Plum;
                    }
                    else
                    {
                        id = p.ways1.ElementAt(i)[j].label2 + p.ways1.ElementAt(i)[j].label1;
                        if (graph.EdgeById(id) != null)
                        {
                            graph.EdgeById(id).EdgeAttr.Color = Microsoft.Glee.Drawing.Color.PaleVioletRed;
                        }
                    }
                }
            }
            for (int i = 0; i < p.ways2.Count; i++)
            {
                for (int j = 0; j < p.ways2.ElementAt(i).Count; j++)
                {
                    string id = p.ways2.ElementAt(i)[j].label1 + p.ways2.ElementAt(i)[j].label2;
                    if (graph.EdgeById(id) != null)
                    {
                        graph.EdgeById(id).EdgeAttr.Color = Microsoft.Glee.Drawing.Color.Plum;
                    }
                    else
                    {
                        id = p.ways2.ElementAt(i)[j].label2 + p.ways2.ElementAt(i)[j].label1;
                        if (graph.EdgeById(id) != null)
                        {
                            graph.EdgeById(id).EdgeAttr.Color = Microsoft.Glee.Drawing.Color.PaleVioletRed;
                        }
                    }
                }
            }

            for (int i = 0; i < p.checked1.Count; i++)
            {
                graph.FindNode(p.checked1.ElementAt(i)).Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Yellow;
            }
            for (int i = 0; i < p.checked2.Count; i++)
            {
                graph.FindNode(p.checked2.ElementAt(i)).Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Yellow;
            }
            for (int i = 0; i < p.front2().Count; i++)
            {
                graph.FindNode(p.front2().ElementAt(i)).Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Plum;
            }
            for (int i = 0; i < p.front1().Count; i++)
            {
                graph.FindNode(p.front1().ElementAt(i)).Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Plum;
            }
            if (p.isSolved())
            {
                graph.FindNode(p.result).Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Red;
            }
            graph.FindNode(p.point1).Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Red;
            graph.FindNode(p.point2).Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Red;
            viewer.Graph = graph;
            this.Refresh();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            agent.Run();
        }
        public Button getButton()
        {
            return button1;
        }
        public TextBox getTextBox()
        {
            return textBox1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            agent.setGraph(textBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            agent.setProblem(textBox3.Text, textBox4.Text);
        }
    }
}
