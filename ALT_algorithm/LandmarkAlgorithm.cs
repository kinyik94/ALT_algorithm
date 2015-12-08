using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ALT_algorithm
{
    class LandmarkAlgorithm
    {
        public Dictionary<int, Vertex> Landmarks {get; set;}

        public LandmarkAlgorithm()
        {
            Landmarks = new Dictionary<int, Vertex>();
        }

        public static LandmarkAlgorithm PlanarAlgorithm(Dictionary<string, Vertex> Nodes, bool isBorder){

            const double angleOfSections = Math.PI / 6.0;

            LandmarkAlgorithm Result = new LandmarkAlgorithm();

            double minX, minY, maxX, maxY;
            double centerX, centerY;

            minX = maxX = Nodes.Values.ElementAt(0).X;
            minY = maxY = Nodes.Values.ElementAt(0).Y;
            centerX = centerY = 0.0;

            for (int i = 0; i < 12; i++)
                Result.Landmarks.Add(i, null);

            /*calculate the center point*/
            foreach (Vertex node in Nodes.Values)
            {
                if (node.X > maxX)
                    maxX = node.X;

                if (node.X < minX)
                    minX = node.X;

                if (node.Y > maxY)
                    maxY = node.Y;

                if (node.Y < minY)
                    minY = node.Y;
            }

            centerX = (maxX + minX) / 2.0;
            centerY = (maxY + minY) / 2.0;


            foreach (Vertex node in Nodes.Values)
            {
        
                /*if the vertex is at a border of a section, it isn't useful.*/
                if (node.Y - centerY != 0.0 && node.X - centerX != 0.0)
                {
                   
                    /*calculate the angle to determine which section contains the vertex*/
                    double angle = Math.Atan((centerY - node.Y) / (node.X - centerX));
               
                    if (node.X < centerX && node.Y < centerY)
                        angle += Math.PI;
                    else if (node.X < centerX && node.Y > centerY)
                        angle +=  Math.PI;
                    else if (node.X > centerX && node.Y > centerY)
                        angle += 2 * Math.PI;

                    /*the serial number of section*/
                    int serialNumber = System.Convert.ToInt32(Math.Floor(angle / angleOfSections));

                    /*Border points*/
                    if (isBorder)
                    {

                        /*determine the 12 landmarks*/
                        if (Result.Landmarks[serialNumber] == null)
                            Result.Landmarks[serialNumber] = node;
                            
                        else
                        {
                            double new_dist = Math.Sqrt(Math.Pow(node.X - centerX, 2.0) + Math.Pow(node.Y - centerY, 2.0));
                            double old_dist = Math.Sqrt(Math.Pow(Result.Landmarks[serialNumber].X - centerX, 2.0) +
                                                     Math.Pow(Result.Landmarks[serialNumber].Y - centerY, 2.0));


                            if (new_dist > old_dist)
                                Result.Landmarks[serialNumber] = node;

                        }
                    }
                        /*Inside points*/
                    else
                    {
                        double maxDist = Math.Sqrt(Math.Pow(maxX - centerX, 2.0) + Math.Pow(maxY - centerY, 2.0)) * 0.6;
                        double minDist = Math.Sqrt(Math.Pow(maxX - centerX, 2.0) + Math.Pow(maxY - centerY, 2.0)) * 0.2;
                        double newDist = Math.Sqrt(Math.Pow(node.X - centerX, 2.0) + Math.Pow(node.Y - centerY, 2.0));
                        if(Result.Landmarks[serialNumber] == null && minDist < newDist && newDist < maxDist)
                            Result.Landmarks[serialNumber] = node;
                        
                    }
                }

            }
                return Result;
        }

        public static LandmarkAlgorithm RandomAlgorithm(Dictionary<string, Vertex> Nodes)
        {
            LandmarkAlgorithm Result = new LandmarkAlgorithm();
            Random rnd = new Random();

            bool found = false;
            int LandmarksIndex = 0;


            while (!found)
            {
                int NodesIndex = rnd.Next(Nodes.Count);
                Result.Landmarks.Add(LandmarksIndex, Nodes.Values.ElementAt(NodesIndex));
                LandmarksIndex++;
                if (LandmarksIndex == 12)
                {
                    found = true;
                }
                
            }
            return Result;
        }

        public static LandmarkAlgorithm ShortestLandmarkAlgorithm(Dictionary<string, Vertex> Nodes)
        {
            LandmarkAlgorithm Result = new LandmarkAlgorithm();

            Random rnd = new Random();
            int firstIndex = rnd.Next(Nodes.Count);
            Vertex v1 = Nodes.Values.ElementAt(firstIndex);
            Vertex closestVertex;
            if (firstIndex != 0)
                closestVertex = Nodes.Values.ElementAt(0);
            else
                closestVertex = Nodes.Values.ElementAt(1);

            double minDist = Math.Sqrt(Math.Pow(v1.X - closestVertex.X, 2.0) + Math.Pow(v1.Y - closestVertex.Y, 2.0));

            foreach (Vertex node in Nodes.Values)
            {
                double newDist = Math.Sqrt(Math.Pow(v1.X - node.X, 2.0) + Math.Pow(v1.Y - node.Y, 2.0));
                if (newDist < minDist && newDist != 0.0)
                {
                    minDist = newDist;
                    closestVertex = node;
                }
            }

            int count = 0;
            bool found = false;
            double mul = 5.0;

            SortedDictionary<double, Vertex> sorteddic = new SortedDictionary<double, Vertex>();

            while(!found)
            {
                foreach (Vertex node in Nodes.Values)
                {
                    double newDist = Math.Sqrt(Math.Pow(v1.X - node.X, 2.0) + Math.Pow(v1.Y - node.Y, 2.0));
                    if (newDist <= (minDist*mul))
                    {
                        try
                        {
                            count++;
                            sorteddic.Add(newDist, node);
                        }
                        catch (ArgumentException)
                        {
                            count--;
                            sorteddic[newDist] = node;
                        }
                    }
                }
                if (count >= 12)
                    found = true;
                else
                    mul+=1.5;
            }

            for (int i = 0; i < 12; i++)
            {
                Result.Landmarks.Add(i, sorteddic.Values.ElementAt(i));
            }

                return Result;
        }
    }
}
