using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace ALT_algorithm
{

    /*
     *  Exception class for the GraphBuilder
     */

    internal class GraphException : Exception
    {
        // Stores the message of exception
        private string _message;

        public string Message
        {
            get { return _message; }
        }

        public GraphException(string message)
        {
            _message = message;
        }
    }

    #region Vertex & Edge

    /*
     * Vertex represent the nodes of the graph
     * x, y are the coordinates where the node is in the window
     * Name is the name of the node and it has to be unique
     * LMT is the flight distance between the actual node and other nodes
     * Edges are the edges what connect to the actual node
     */

    internal class Vertex
    {
        public double X { get; set; }
        public double Y { get; set; }
        public string Name { get; set; }

        public List<Edge> Edges { get; set; }

        public Vertex(string name, double x, double y)
        {
            Name = name;
            X = x;
            Y = y;
        }

        public void Draw(PaintEventArgs e, double zoom, double ofsetX, double ofsetY, Brush color = null, int radius = 5)
        {
            double xNow = ((X * zoom + ofsetX));
            double yNow = ((Y * zoom + ofsetY));
            if (color == null)
                color = Brushes.Red;
            e.Graphics.FillEllipse(color, (int)xNow - radius, (int)yNow - radius, 2 * radius, 2 * radius);
            e.Graphics.DrawString(Name, new Font("Arial", 7), Brushes.Black, (int)xNow - 2 * Name.Length, (int)yNow - radius - 15);
        }

        public void Draw(PaintEventArgs e, double zoom, double ofsetX, double ofsetY, bool isLandmark)
        {
            
            double xNow = ((X * zoom + ofsetX));
            double yNow = ((Y * zoom + ofsetY));
            e.Graphics.FillEllipse(Brushes.Green, (int)xNow - 5, (int)yNow - 5, 10, 10);
            e.Graphics.DrawString(Name, new Font("Arial", 7), Brushes.Black, (int)xNow - 2 * Name.Length, (int)yNow - 20);
        }

        public static double BeeLineDist(Vertex v1, Vertex v2)
        {
            return Math.Sqrt((v1.X - v2.X) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y));
        }

    }

    /*
     *  Edge class represents the edges in the graph
     *  v1, v2 are the two nodes what connect to the edge
     *  Value is the value of the edge
     */

    internal class Edge
    {
        public Vertex V1 { get; set; }
        public Vertex V2 { get; set; }

        public double Value { get; set; }

        public Edge(Vertex v1, Vertex v2, double value)
        {
            V1 = v1;
            V2 = v2;
            Value = value;
        }

        public Vertex GetNeighbr(Vertex v)
        {
            if (v.Equals(V1))
            {
                return V2;
            }
            if (v.Equals(V2))
            {
                return V1;
            }
            return null;
        }

        public void Draw(PaintEventArgs e, double zoom, double ofsetX, double ofsetY, Pen pen = null)
        {
            if (pen == null)
            {
                pen = Pens.Black;
            }
            e.Graphics.DrawLine(pen, (int)((V1.X * zoom + ofsetX)), (int)((V1.Y * zoom + ofsetY)), (int)((V2.X * zoom + ofsetX)), (int)((V2.Y * zoom + ofsetY)));
        }
    }

    #endregion

    /*
     *  Represents a graph with nodes and edges
     *  GraphBuilder responsible for create and manage the graph
     */

    internal class GraphBuilder
    {

        public Dictionary<string, Vertex> Nodes;
        public List<Edge> Edges;
        public LandmarkAlgorithm Landmark;

        private double _zoom;

        private Dictionary<Vertex, double> ReachedNodes;
        private List<Vertex> SettledNodes;
        private List<Edge> SettledEdges;
        private Dictionary<Vertex, Vertex> ParentNodes;
        private Dictionary<Vertex, double> Distances;
        private Vertex startNode;
        private Vertex endNode;
        private List<Vertex> ResultNodes;
        private List<Edge> ResultEdges;

        public GraphBuilder(List<Vertex> nodes, List<Edge> edges)
        {
            foreach (Vertex node in nodes)
            {
                Nodes.Add(node.Name, node);
            }
            Edges = edges;
            try
            {
                Edge smallestEdge = Edges.OrderBy(e => e.Value).First();
                _zoom = smallestEdge.Value/Vertex.BeeLineDist(smallestEdge.V1, smallestEdge.V2);
            }
            catch(InvalidOperationException)
            {
                _zoom = 1;
            }

            Landmark = LandmarkAlgorithm.PlanarAlgorithm(Nodes,true);
            ReachedNodes = new Dictionary<Vertex, double>();
            ParentNodes = new Dictionary<Vertex, Vertex>();
            Distances = new Dictionary<Vertex, double>();
            SettledNodes = new List<Vertex>();
            ResultNodes = new List<Vertex>();
            ResultEdges = new List<Edge>();
            SettledEdges = new List<Edge>();
        }

        public GraphBuilder(StreamReader nodes, StreamReader edges)
        {
            Nodes = new Dictionary<string, Vertex>();
            Edges = new List<Edge>();

            while (!nodes.EndOfStream)
            {
                try
                {
                    /*
                     *  Read the next line and split it with the ';' separator
                     *  Put the results in the Nodes list
                     */

                    var line = nodes.ReadLine();
                    var nodeArray = line.Split(';');
                    if (nodeArray.Length != 3)
                        throw new GraphException(
                            "At least one of the lines in the given StreamReader for nodes is not suitable for a graph!");
                    double x, y;
                    if (Double.TryParse(nodeArray[1], out x) && Double.TryParse(nodeArray[2], out y))
                    {
                        Nodes.Add(nodeArray[0], new Vertex(nodeArray[0], x, y));
                        Nodes[nodeArray[0]].Edges = new List<Edge>();
                    }
                    else
                        throw new GraphException(
                            "In at least one line of the given StreamReader for nodes, the second or the third value is not a number!");
                }
                catch (NullReferenceException)
                {
                    throw new GraphException("The given StreamReader for nodes is null!");
                }
            }
            while (!edges.EndOfStream)
            {
                try
                {
                    /*
                     *  Read the next line and split it with the ';' separator
                     *  Put the results in the Edges list and update the nodes
                     */

                    var line = edges.ReadLine();
                    var edgeArray = line.Split(';');
                    if (edgeArray.Length != 3)
                        throw new GraphException("At least one of the lines in the given StreamReader for edges is not suitable for a graph!");
                    double dist;
                    if (Double.TryParse(edgeArray[2], out dist))
                    {
                        
                        try
                        {
                            Edges.Add(new Edge(Nodes[edgeArray[0]], Nodes[edgeArray[1]], dist));
                            Nodes[edgeArray[0]].Edges.Add(Edges.Last());
                            Nodes[edgeArray[1]].Edges.Add(Edges.Last());
                        }
                        catch
                        {
                            throw new GraphException("Node was not found for edge!");
                        }
                    }
                    else
                        throw new GraphException("In at least one line of the given StreamReader for edges, the third value is not a number!");
                }
                catch (NullReferenceException)
                {
                    throw new GraphException("The given StreamReader for edges is null!");
                }
            }

            try
            {
                Edge smallestEdge = Edges.OrderBy(e => e.Value).First();
                _zoom = smallestEdge.Value / Vertex.BeeLineDist(smallestEdge.V1, smallestEdge.V2);
            }
            catch (InvalidOperationException)
            {
                _zoom = 1;
            }

            Landmark = LandmarkAlgorithm.PlanarAlgorithm(Nodes,true);
            ReachedNodes = new Dictionary<Vertex, double>();
            ParentNodes = new Dictionary<Vertex, Vertex>();
            Distances = new Dictionary<Vertex, double>();
            SettledNodes = new List<Vertex>();
            SettledEdges = new List<Edge>();
            ResultNodes = new List<Vertex>();
            ResultEdges = new List<Edge>();
        }

        public void Draw(PaintEventArgs e, double zoom, double ofsetX, double ofsetY)
        {
            foreach (var edge in Edges)
            {
                if(ResultEdges.Contains(edge))
                    edge.Draw(e, zoom, ofsetX, ofsetY, new Pen(Color.Blue, 2));
                else if(SettledEdges.Contains(edge))
                    edge.Draw(e, zoom, ofsetX, ofsetY, new Pen(Color.Cyan, 2));
                else
                    edge.Draw(e, zoom, ofsetX, ofsetY);
            }
            foreach (var node in Nodes)
            {
                if (node.Value != startNode && node.Value != endNode)
                {
                    if (ResultNodes.Contains(node.Value))
                        node.Value.Draw(e, zoom, ofsetX, ofsetY, Brushes.Blue);
                    else if (SettledNodes.Contains(node.Value))
                        node.Value.Draw(e, zoom, ofsetX, ofsetY, Brushes.Cyan);
                    else
                        node.Value.Draw(e, zoom, ofsetX, ofsetY);
                }
            }

            foreach (var landmark in Landmark.Landmarks)
            {
                if (landmark.Value != startNode && landmark.Value != endNode && !SettledNodes.Contains(landmark.Value) && !ResultNodes.Contains(landmark.Value))
                        landmark.Value.Draw(e, zoom, ofsetX, ofsetY, true);
            }
            if (startNode != null && endNode != null)
            {
                startNode.Draw(e, zoom, ofsetX, ofsetY, Brushes.Indigo, 8);
                endNode.Draw(e, zoom, ofsetX, ofsetY, Brushes.Indigo, 8);
            }
        }

        private double calculateH(Vertex v)
        {
            List<double> heuristics = new List<double>();
            foreach (var node in Landmark.Landmarks.Values)
            {
                heuristics.Add(Vertex.BeeLineDist(v, node) * _zoom - Vertex.BeeLineDist(endNode, node) * _zoom);
                heuristics.Add(Vertex.BeeLineDist(node, endNode) * _zoom - Vertex.BeeLineDist(node, v) * _zoom);
            }

            return heuristics.Max();
        }

        public Vertex SearchNextNode()
        {
            Vertex v = ReachedNodes.OrderBy(r => (Distances[r.Key] + r.Value)).First().Key;
            foreach (var edge in Edges.Where(e => e.V1 == v || e.V2 == v))
            {
                Vertex u = edge.GetNeighbr(v);
                if (Distances[v] + edge.Value + ReachedNodes[v] < Distances[u] + calculateH(u))
                {
                    ParentNodes[u] = v;
                    Distances[u] = Distances[v] + edge.Value;
                    if (!ReachedNodes.ContainsKey(u))
                        ReachedNodes.Add(u, calculateH(u));
                }
            } 
            SettledNodes.Add(v);
            SettledEdges.Add(Edges.First(e => (e.GetNeighbr(v) == ParentNodes[v])));
            ReachedNodes.Remove(v);
            return v;
        }

        public void SearchPaths(Vertex firstNode, Vertex lastNode, out int time, out int nodeNumber)
        {
            int startTime = DateTime.Now.Millisecond;
            ReachedNodes.Clear();
            ParentNodes.Clear();
            ResultNodes.Clear();
            SettledNodes.Clear();
            startNode = firstNode;
            endNode = lastNode;
            SettledEdges.Clear();
            ResultEdges.Clear();
            foreach (var node in Nodes.Values)
            {
                ParentNodes[node] = null;
                Distances[node] = Double.PositiveInfinity;
            }
            Distances[startNode] = 0;
            ReachedNodes.Add(startNode, calculateH(startNode));

            while (ReachedNodes.Count != 0 && !ReachedNodes.ContainsKey(endNode))
            {
                SearchNextNode();
            }
            if (ReachedNodes.ContainsKey(endNode))
            {
                SettledNodes.Add(endNode);
                Vertex act = endNode;
                while (act != startNode)
                {
                    ResultNodes.Add(act);
                    Vertex parent = ParentNodes[act];
                    ResultEdges.Add(Edges.First(e => (e.GetNeighbr(act) == parent)));
                    act = parent;
                }
                ResultNodes.Add(act);
            }
            nodeNumber = SettledNodes.Count();
            SettledNodes.Clear();
            SettledEdges.Clear();
            int endTime = DateTime.Now.Millisecond;
            time = endTime - startTime;
        }

        public void AnimateStart(Vertex firstNode, Vertex lastNode, Timer animateTimer)
        {
            ReachedNodes.Clear();
            ParentNodes.Clear();
            ResultNodes.Clear();
            SettledNodes.Clear();
            startNode = firstNode;
            endNode = lastNode;
            SettledEdges.Clear();
            ResultEdges.Clear();
            foreach (var node in Nodes.Values)
            {
                ParentNodes[node] = null;
                Distances[node] = Double.PositiveInfinity;
            }
            Distances[startNode] = 0;
            ReachedNodes.Add(startNode, calculateH(startNode));

            animateTimer.Start();
        }

        public bool AnimateAlg(Timer animateTimer, out int nodes)
        {
            if (ReachedNodes.Count == 0 || ReachedNodes.ContainsKey(endNode))
            {
                if (ReachedNodes.ContainsKey(endNode))
                {
                    SettledNodes.Add(endNode);
                    Vertex act = endNode;
                    while (act != startNode)
                    {
                        ResultNodes.Add(act);
                        Vertex parent = ParentNodes[act];
                        ResultEdges.Add(Edges.First(e => (e.GetNeighbr(act) == parent)));
                        act = parent;
                    }
                    ResultNodes.Add(act);
                }
                animateTimer.Stop();
                nodes = SettledNodes.Count();
                return true;
            }
            SearchNextNode();
            nodes = 0;
            return false;
        }

        public void Clear(Timer animateTimer)
        {
            if(animateTimer.Enabled)
                animateTimer.Stop();
            startNode = null;
            endNode = null;
            ReachedNodes.Clear();
            ParentNodes.Clear();
            ResultNodes.Clear();
            SettledNodes.Clear();
            SettledEdges.Clear();
            ResultEdges.Clear();
        }

    }
}
