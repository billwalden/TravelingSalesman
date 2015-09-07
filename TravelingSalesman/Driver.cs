
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ConsoleApplication1
{
    class Driver
    {

        [STAThread]
        static void Main(string[] args)
        {


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Driver program = new Driver();


            /*int[][] map =new int[10][] {
                         new int[3]{1, 2, 3}, new int[1]{2}, new int[2]{3, 4}, new int[3]{4, 5, 6}, new int[2]{6, 7},
                         new int[1]{7}, new int[2]{8, 9}, new int[3]{8, 9, 10}, new int[1]{10}, new int[1]{10}
                      };*/
            List<node> nodes = ParseTextFornodes("C:\\Users\\walde_000\\Desktop\\Art. Itelli\\project 3\\Random40.tsp");
            List<Edge> edges = program.createEdgeList(nodes);

            program.bruteFindFastest(nodes, nodes[0]);
            

           program.greedy(edges, nodes, nodes[0]);
            
            /* for(int i = 0; i < nodes.Count() -2; i++)
             {
                 nodes[i].setAdjNodes(map[i]);
             }*/
            //var watch = Stopwatch.StartNew();
            
           // watch.Stop();
           // var elapsedMs = watch.ElapsedMilliseconds;
           // Console.Write("\n"+elapsedMs);

            //routes.printDistances();
          
           
           
        
            Console.ReadLine();
        }
        public static float distance(node fromNode, node toNode)
        {
            return distance(fromNode.x, fromNode.y, toNode.x, toNode.y);
        }


        //calculates distance between two points
        public static float distance(float x0, float y0, float x1, float y1)
        {            
            float xdif = x1 - x0;
            float ydif = y1 - y0;
            double Dsquared = Math.Pow(Convert.ToDouble(xdif), 2) + Math.Pow(Convert.ToDouble(ydif), 2);
            double D = Math.Sqrt(Dsquared);
            return Convert.ToSingle(D);

        }

        //Parses text file for lines that begins with a digit, assumes the line contains coordinates and creates a node from coordinates. Then, returns an array of all nodes.
        public static List<node> ParseTextFornodes(string filepath)
        {
            StreamReader reader = File.OpenText(filepath);
            string line;
            List<node> nodes = new List<node>();
            while (((line = reader.ReadLine()) != null))
            {
                if (char.IsDigit(line[0]))
                {
                    string[] coordinfo = line.Split(' ');

                    nodes.Add(new node(Convert.ToInt32(coordinfo[0])-1, Convert.ToSingle(coordinfo[1]), Convert.ToSingle(coordinfo[2])));
                }

            }
            return nodes;
        }
        public static List<DirectedNode> ParseTextFornodes2(string filepath)
        {
            StreamReader reader = File.OpenText(filepath);
            string line;
            List<DirectedNode> nodes = new List<DirectedNode>();
            while (((line = reader.ReadLine()) != null))
            {
                if (char.IsDigit(line[0]))
                {
                    string[] coordinfo = line.Split(' ');

                    nodes.Add(new DirectedNode(Convert.ToInt32(coordinfo[0]) - 1, Convert.ToSingle(coordinfo[1]), Convert.ToSingle(coordinfo[2])));
                }

            }
            return nodes;
        }


        //creats an array that holds distances between each node pair
        public float[,] distanceList(List<node> nodes) 
        {
            float[,] distances = new float[nodes.Count()- 1, this.TriangleNumber(nodes.Count() - 1)];
            for (int i = 0; i < nodes.Count(); i++)
            {
                for (int x = 1; x < nodes.Count() - i; x++)
                {
                    distances[i, x-1] = distance(nodes[i].x, nodes[i].y, nodes[i + x].x, nodes[i + x].y);
                }
            }
            
            return distances;
            
        }

        public List <Edge> createEdgeList(List <node> nodelist)
        {
            List<Edge> edgelist = new List<Edge>();
            for(int i = 0; i < nodelist.Count(); i++)
            {
                for(int x = 0; x < nodelist.Count(); x++)
                {
                    if(x != i)
                    edgelist.Add(new Edge(nodelist[i], nodelist[x]));
                }
            }
            return edgelist;
        }
        //finds factorial of an integer
        public int FactNum(int num1)
        {
            int i = 0;
            int numfact = num1;
            for (i = num1 - 1; i > 0; i--)
                numfact *= i; 
            return numfact; 
        }
        //returns triangular number given the base number
        public int TriangleNumber(int baseNum)
        {
            int triangle = 0;
            while (baseNum != 0)
            {
                triangle += baseNum;
                baseNum--;
            }
            return triangle;
        }

        //finds all permutations of a size set (by item number, not value)
        public List<int[]> AllPermutations(int n)
        {
            int[] numList = new int[n];
            for (int i = 0; i < n; i++)
            {
                numList[i] = i;
            }
            
            return AllPermutations(numList);
  
        }

        public List<int[]> AllPermutations(int[] numList)
        {
            int[] newList = new int[numList.Length];
            int numperms = this.FactNum(numList.Length);
            List<int[]> permutationList = new List<int[]>();
            int[] intarray = new int[numList.Length];

            for(int i=0; i < numList.Length; i++)
            {
                newList[i] = numList[i];
            }

            for (int i = 0; i < numperms; i++)
            {
                permutationList.Add(intarray);
            }

            permutationList[0] = newList;

            for (int i = 1; i < numperms; i++)
            {
                
                permutationList[i] = NextPermutation(permutationList[i-1]);
            }
            
            return permutationList;
        }

        //Returns one perm at a time
        public int[] NextPermutation(int[] numList)
        {
            /*
             Knuths
             1. Find the largest index j such that a[j] < a[j + 1]. If no such index exists, the permutation is the last permutation.
             2. Find the largest index l such that a[j] < a[l]. Since j + 1 is such an index, l is well defined and satisfies j < l.
             3. Swap a[j] with a[l].
             4. Reverse the sequence from a[j + 1] up to and including the final element a[n].

             */

            int[] newList = new int[numList.Length];
            for (int i = 0; i < numList.Length; i++)
            {
                newList[i] = numList[i];
            }
            numList = null;

            int largestIndex = -1;
            for (int i = newList.Length - 2; i >= 0; i--)
            {
                if (newList[i] < newList[i + 1])
                {
                    largestIndex = i;
                    break;
                }
            }

            if (largestIndex < 0) Console.Write("false");

            int largestIndex2 = -1;
            for (int i = newList.Length - 1; i >= 0; i--)
            {
                if (newList[largestIndex] < newList[i])
                {
                    largestIndex2 = i;
                    break;
                }
            }

            int tmp = newList[largestIndex];
            newList[largestIndex] = newList[largestIndex2];
            newList[largestIndex2] = tmp;

            for (int i = largestIndex + 1, j = newList.Length - 1; i < j; i++, j--)
            {
                tmp = newList[i];
                newList[i] = newList[j];
                newList[j] = tmp;
            }
            
            return newList;
        }

        public void bruteFindFastest(List <node> nodes, node startnode)
        {
            int[] nodeIDlist = new int[nodes.Count() -1];
            float[,] distancelist = this.distanceList(nodes);
            int numperms = this.FactNum(nodes.Count() - 1);
            int[] fastestpath = new int[nodes.Count()];
            int[] currentpath = new int[nodes.Count()];
            route fastestroute;
            route currentroute;

            fastestpath[0] = startnode.id;
            currentpath[0] = startnode.id;
            int count = 0;
            for (int i = 0; i < nodes.Count(); i++)
            {
                if(nodes[i].id != startnode.id)
                {
                    nodeIDlist[count] = nodes[i].id;
                    count++;
                }
            }
            nodeIDlist.CopyTo(fastestpath, 1);
            fastestroute = new route(distancelist, fastestpath);

            for(int i = 0; i < numperms-1;i++)
            {
                nodeIDlist = NextPermutation(nodeIDlist);
                nodeIDlist.CopyTo(currentpath, 1);
                currentroute = new route(distancelist, currentpath);
                
                if(currentroute.netdistance < fastestroute.netdistance)
                {
                    currentpath.CopyTo(fastestpath, 0);
                    fastestroute = new route(distancelist, fastestpath);
                }
               
            }
            fastestroute.printPath();
            Console.WriteLine("\nFast Distance:  " + fastestroute.netdistance);
            Console.WriteLine("Traveling from and back to Node# " + startnode.id);
        }

        public void BFS(List <DirectedNode> nodelist)
        {
            Queue<DirectedNode> queue = new Queue<DirectedNode>();
            queue.Enqueue(nodelist[0]);
            DirectedNode frontiernode;
            int[] adjlist;

            while (queue.Count() != 0)
            {
                frontiernode = queue.Dequeue();
                adjlist = frontiernode.adjnodes;
                if(adjlist == null) { break; }
               
                    foreach (int i in adjlist)
                    {
                        
                        queue.Enqueue(nodelist[i]);
                   
                        node previousnode = nodelist[nodelist[i].PreviousNodeid];
                        float edgeweight = ConsoleApplication1.Driver.distance(frontiernode, nodelist[i]);
                        float newdistance = frontiernode.ShortestDistanceTo + edgeweight;

                        if (nodelist[i].ShortestDistanceTo == 0)
                        {
                            nodelist[i].ShortestDistanceTo = newdistance;
                        nodelist[i].PreviousNodeid = frontiernode.id;
                        }
                        else if (newdistance < nodelist[i].ShortestDistanceTo)
                        {
                            nodelist[i].ShortestDistanceTo = newdistance;
                        nodelist[i].PreviousNodeid = frontiernode.id;
                        }
                        else { }

                    }
               
            }
            int prevNodeId = nodelist[nodelist.Count() - 1].PreviousNodeid;
            Console.Write("Path: " + prevNodeId);
            while(prevNodeId != 0)
            {
                Console.Write(" <- " + nodelist[prevNodeId].PreviousNodeid);
                prevNodeId = nodelist[prevNodeId].PreviousNodeid;
            }
            Console.Write("\nDistance: " + nodelist[10].ShortestDistanceTo);
            

        }
        public void DFS(List<DirectedNode> nodelist)
        {
            Stack<DirectedNode> stack = new Stack<DirectedNode>();
            stack.Push(nodelist[0]);
            DirectedNode frontiernode;
            int[] adjlist;

            while(stack.Count() != 0)
            {
                frontiernode = stack.Pop();
                adjlist = frontiernode.adjnodes;

                if (adjlist == null) { continue; }

                foreach (int i in adjlist)
                {

                    stack.Push(nodelist[i]);

                    node previousnode = nodelist[nodelist[i].PreviousNodeid];
                    float edgeweight = ConsoleApplication1.Driver.distance(frontiernode, nodelist[i]);
                    float newdistance = frontiernode.ShortestDistanceTo + edgeweight;
                    

                    if (nodelist[i].ShortestDistanceTo == 0)
                    {
                        nodelist[i].ShortestDistanceTo = newdistance;
                        nodelist[i].PreviousNodeid = frontiernode.id;
                    }
                    else if (newdistance < nodelist[i].ShortestDistanceTo)
                    {
                        nodelist[i].ShortestDistanceTo = newdistance;
                        nodelist[i].PreviousNodeid = frontiernode.id;
                    }
                    else { }

                }

            }
            int prevNodeId = nodelist[nodelist.Count() - 1].PreviousNodeid;
            Console.Write("Path: " + prevNodeId);
            while (prevNodeId != 0)
            {
                Console.Write(" <- " + nodelist[prevNodeId].PreviousNodeid);
                prevNodeId = nodelist[prevNodeId].PreviousNodeid;
            }
            Console.Write("\nDistance: " + nodelist[10].ShortestDistanceTo);
        }
    

        public void greedy(List<Edge> edgelist,List <node> nodelist, node startnode)
        {
            Thread mainthread = Thread.CurrentThread;
            int count = nodelist.Count();
            Edge greatestEdge = edgelist[0];
            List<node> markednodes = new List<node>();
            List<Edge> route = new List<Edge>();
            List<node> nodelistcopy = new List<node>(nodelist);

            markednodes.Add(startnode);
            nodelist.Remove(startnode);
            
            for(int i = 0; i < edgelist.Count(); i++)
            {
                if (edgelist[i].FromNode == startnode)
                {
                    greatestEdge = edgelist[i];
                    break;
                }
            }
            for (int i = 0; i < edgelist.Count(); i++)
            {
                if (edgelist[i].FromNode == startnode)
                {
                    if (edgelist[i].distance > greatestEdge.distance)
                        greatestEdge = edgelist[i];
                }
            }
           

            route.Add(greatestEdge);
            route.Add(new Edge(greatestEdge.ToNode, greatestEdge.FromNode));

            markednodes.Add(greatestEdge.ToNode);
            nodelist.Remove(greatestEdge.ToNode);

            Form1 plot = new Form1(nodelistcopy, route, markednodes, mainthread);
            new Thread(delegate () {
                Application.Run(plot);
            }).Start();
            mainthread.Suspend();


            Edge NodetoEdgeStart;
            Edge NodetoEdgeEnd;
            Edge NodetoEdge2Start;
            Edge NodetoEdge2End;

            foreach (node item in nodelist)
            {
                NodetoEdgeStart = edgelist.Find(i => ((i.ToNode == item) && (i.FromNode == startnode)));
                float CrossProduct = Math.Abs((greatestEdge.xdif * NodetoEdgeStart.ydif) - (greatestEdge.ydif * NodetoEdgeStart.xdif));
                item.ClosestEdgeDist = CrossProduct / greatestEdge.distance;
                item.ClosestEdge = greatestEdge;
            }
            node closestnode = nodelist[0];
           
            foreach(node item in nodelist)
            {
                if(item.ClosestEdgeDist < closestnode.ClosestEdgeDist)
                {
                    closestnode = item;
                }
            }
            markednodes.Add(closestnode);
            nodelist.Remove(closestnode);
            
            route.Remove(route[0]);

       
            route.Insert(0, new Edge(closestnode, greatestEdge.ToNode));
            route.Insert(0, new Edge(greatestEdge.FromNode, closestnode));

            mainthread.Suspend();


            while (route.Count() < count)
            {
                closestnode = nodelist[0];
                List <node> nodelistcopy2 = new List<node>(nodelist);
                Edge edge2; 
              
                foreach (node item in nodelistcopy2)
                {
                    foreach (Edge edge in route)
                    {
                        NodetoEdgeStart = edgelist.Find(i => ((i.ToNode == item) && (i.FromNode == edge.FromNode)));
                        NodetoEdgeEnd = edgelist.Find(i => ((i.ToNode == edge.ToNode) && (i.FromNode == item)));
                        float CrossProduct = Math.Abs((edge.xdif * NodetoEdgeStart.ydif) - (edge.ydif * NodetoEdgeStart.xdif));
                        float D = CrossProduct / edge.distance;
                        float Dotproduct = edge.xdif * NodetoEdgeStart.xdif + edge.ydif * NodetoEdgeStart.ydif;
                        float t = Dotproduct / (edge.distance * edge.distance);
                        float D1 = NodetoEdgeStart.distance + NodetoEdgeEnd.distance;
                        edge2 = route.Find(i => (i.ToNode == edge.FromNode));
                        NodetoEdge2Start = edgelist.Find(i => ((i.ToNode == item) && (i.FromNode == edge2.FromNode)));
                        NodetoEdge2End = edgelist.Find(i => ((i.ToNode == edge2.ToNode) && (i.FromNode == item)));
                        float D2 = NodetoEdge2Start.distance + NodetoEdge2End.distance;

                        if ((t > 0) && (t < 1))
                        {

                            if (!route.Contains(item.ClosestEdge))
                            {
                                item.ClosestEdgeDist = D;
                                item.ClosestEdge = edge;
                            }
                            else if (D < item.ClosestEdgeDist)
                            {
                                item.ClosestEdgeDist = D;
                                item.ClosestEdge = edge;
                            }
                        }
                       
           
              
                        else if (NodetoEdgeStart.distance < item.ClosestEdgeDist)
                        {
                            item.ClosestEdgeDist = NodetoEdgeStart.distance;
                            if (D1 < D2)
                            {
                                item.ClosestEdge = edge;
                            }
                            else
                                item.ClosestEdge = edge2;  
                        }
                        else { }
                        


                    }
                    if (item.ClosestEdgeDist < closestnode.ClosestEdgeDist)
                    {
                        closestnode = item;
                    }


                }
              
                markednodes.Add(closestnode);
                nodelist.Remove(closestnode);
                
                route.Insert(route.IndexOf(closestnode.ClosestEdge), new Edge(closestnode, closestnode.ClosestEdge.ToNode));
                route.Insert(route.IndexOf(closestnode.ClosestEdge) -1, new Edge(closestnode.ClosestEdge.FromNode, closestnode));
                markednodes.Add(closestnode);
                nodelist.Remove(closestnode);
                route.Remove(closestnode.ClosestEdge);
                mainthread.Suspend();

              
            }
        

        }
        
        
      
    }
}

