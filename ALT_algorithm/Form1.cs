 using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Text;

namespace ALT_algorithm
{
    public partial class Form1 : Form
    {

        private GraphBuilder _graph;
        private double zoom;
        private double ofsetX;
        private double ofsetY;
        private bool mouseDown;
        private double mouseX;
        private double mouseY;

        public Form1()
        {
            InitializeComponent();

            Application.Idle += OnIdle;

            typeof(Panel).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, graphPanel, new object[] { true });

            /*
             *  Background of the Panel where is the map
             */
            graphPanel.BackColor = Color.BurlyWood;

            /*
             *  Reead the vertexes and edges from csv files
             */
            if (!(File.Exists("..\\..\\GraphDatas\\Vertex.csv"))) File.Create("..\\..\\GraphDatas\\Vertex.csv");
            if (!(File.Exists("..\\..\\GraphDatas\\Edges.csv"))) File.Create("..\\..\\GraphDatas\\Edges.csv");

            StreamReader _nodes = new StreamReader("..\\..\\GraphDatas\\Vertex.csv", Encoding.UTF8);
            StreamReader _edges = new StreamReader("..\\..\\GraphDatas\\Edges.csv", Encoding.UTF8);

            _graph = new GraphBuilder(_nodes, _edges);

            
                fromBox.DataSource =
                    _graph.Nodes.Select(c => c.Key).OrderBy(c => c[0]).ThenBy(c => c[1]).ThenBy(c => c[2]).ToList();
                toBox.DataSource =
                    _graph.Nodes.Select(c => c.Key).OrderBy(c => c[0]).ThenBy(c => c[1]).ThenBy(c => c[2]).ToList();
            

            /*
             *  Close the files
             */
            _nodes.Close();
            _edges.Close();

            /*
             *  Set zoom and ofset parameters
             */
            zoom = 1;
            ofsetX = 15;
            ofsetY = 100;

            _graph.Landmark = LandmarkAlgorithm.PlanarAlgorithm(_graph.Nodes, true);

        }

        private void OnIdle(Object sender, EventArgs e)
        {
            MemoryUsage.Text = (Process.GetCurrentProcess().PrivateMemorySize64 / 1000).ToString() + " kB";
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            int time;
            int nodeNumber;
            _graph.SearchPaths(_graph.Nodes[fromBox.Text], _graph.Nodes[toBox.Text], out time, out nodeNumber);
            graphPanel.Invalidate();
            timeValue.Text = time.ToString() + " ms";
            examinedNodes.Text = nodeNumber.ToString();

        }

        private void animateButton_Click(object sender, EventArgs e)
        {
            _graph.AnimateStart(_graph.Nodes[fromBox.Text], _graph.Nodes[toBox.Text], AnimateTimer);
        }

        private void graphPanel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            _graph.Draw(e, zoom, ofsetX, ofsetY);
           
        }

        private void graphPanel_Click(object sender, EventArgs e)
        {
            graphPanel.Focus();
        }

        private void mouse_Down(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }

        private void mouse_Up(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void mouse_Move(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                ofsetX -= (mouseX - e.X);
                ofsetY -= (mouseY - e.Y);
                graphPanel.Invalidate();
            }
            mouseX = e.X;
            mouseY = e.Y;
        }

        private void mouse_Wheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                zoom *= 1.2;
                graphPanel.Invalidate();
            }
            if (e.Delta < 0)
            {
                zoom /= 1.2;
                graphPanel.Invalidate();
            }

        }

        private void AnimateTimer_Tick(object sender, EventArgs e)
        {
            int nodes;
            if(_graph.AnimateAlg(AnimateTimer, out nodes))
                examinedNodes.Text = nodes.ToString();
            graphPanel.Invalidate();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                if (i != e.Index)
                    checkedListBox1.SetItemChecked(i, false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*Calling the selected landmark refresher algorithm*/
            int numOfChecked = 0;
            for(int i = 0; i < checkedListBox1.Items.Count; i++)
                if(checkedListBox1.GetSelected(i))
                    numOfChecked = i;

            switch (numOfChecked)
            {
                case 0:
                    _graph.Landmark = LandmarkAlgorithm.PlanarAlgorithm(_graph.Nodes, true);
                    graphPanel.Invalidate();
                    break;
                case 1:
                    _graph.Landmark = LandmarkAlgorithm.PlanarAlgorithm(_graph.Nodes, false);
                    graphPanel.Invalidate();
                    break;
                case 2:
                    _graph.Landmark = LandmarkAlgorithm.RandomAlgorithm(_graph.Nodes);
                    graphPanel.Invalidate();
                    break;
                case 3:
                    _graph.Landmark = LandmarkAlgorithm.ShortestLandmarkAlgorithm(_graph.Nodes);
                    graphPanel.Invalidate();
                    break;
                default:
                    _graph.Landmark = LandmarkAlgorithm.PlanarAlgorithm(_graph.Nodes, true);
                    graphPanel.Invalidate();
                    break;

            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _graph.Clear(AnimateTimer);
            graphPanel.Invalidate();
        }

    }
}
