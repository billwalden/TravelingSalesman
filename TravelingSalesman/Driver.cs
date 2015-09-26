using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ConsoleApplication1
{
    internal class Driver
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Driver program = new Driver();

            //Directed edge map:
            /*int[][] map =new int[10][] {
                          new int[3]{1, 2, 3}, new int[1]{2}, new int[2]{3, 4}, new int[3]{4, 5, 6}, new int[2]{6, 7},
                          new int[1]{7}, new int[2]{8, 9}, new int[3]{8, 9, 10}, new int[1]{10}, new int[1]{10}
                       };
            List<DirectedNode> nodes = createRandomNodeList2(11);
            for(int i =0; i < nodes.Count-1; i++)
             {
                 nodes[i].adjnodes = map[i];
             }
             program.BFS(nodes, 11);*/

            // List<node> nodes = createRandomNodeList(11);
            // program.bruteFindFastest(nodes, nodes[0]);

            Form1 form = new Form1();
            Application.Run(form);

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

        // creates a list of random nodes
        public static List<node> createRandomNodeList(int numNodes)
        {
            List<node> nodelist = new List<node>();
            Random rand = new Random();

            for (int i = 0; i < numNodes; i++)
            {
                float randomx = (float)(rand.NextDouble() * 100 + rand.NextDouble());
                float randomy = (float)(rand.NextDouble() * 100 + rand.NextDouble());
                nodelist.Add(new node(i, randomx, randomy));
            }
            return nodelist;
        }

        public static List<DirectedNode> createRandomNodeList2(int numNodes)
        {
            List<DirectedNode> nodelist = new List<DirectedNode>();
            Random rand = new Random();

            for (int i = 0; i < numNodes; i++)
            {
                float randomx = (float)(rand.NextDouble() * 100 + rand.NextDouble());
                float randomy = (float)(rand.NextDouble() * 100 + rand.NextDouble());
                nodelist.Add(new DirectedNode(i, randomx, randomy));
            }
            return nodelist;
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

                    nodes.Add(new node(Convert.ToInt32(coordinfo[0]) - 1, Convert.ToSingle(coordinfo[1]), Convert.ToSingle(coordinfo[2])));
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
            float[,] distances = new float[nodes.Count() - 1, this.TriangleNumber(nodes.Count() - 1)];
            for (int i = 0; i < nodes.Count(); i++)
            {
                for (int x = 1; x < nodes.Count() - i; x++)
                {
                    distances[i, x - 1] = distance(nodes[i].x, nodes[i].y, nodes[i + x].x, nodes[i + x].y);
                }
            }

            return distances;
        }

        public static List<Edge> createEdgeList(List<node> nodelist)
        {
            List<Edge> edgelist = new List<Edge>();
            for (int i = 0; i < nodelist.Count(); i++)
            {
                for (int x = 0; x < nodelist.Count(); x++)
                {
                    if (x != i)
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

            for (int i = 0; i < numList.Length; i++)
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
                permutationList[i] = NextPermutation(permutationList[i - 1]);
            }

            return permutationList;
        }

        //Returns one perm at a time
        public int[] NextPermutation(int[] numList)
        {
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

        public void bruteFindFastest(List<node> nodes, node startnode)
        {
            int[] nodeIDlist = new int[nodes.Count() - 1];
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
                if (nodes[i].id != startnode.id)
                {
                    nodeIDlist[count] = nodes[i].id;
                    count++;
                }
            }
            nodeIDlist.CopyTo(fastestpath, 1);
            fastestroute = new route(distancelist, fastestpath);

            for (int i = 0; i < numperms - 1; i++)
            {
                nodeIDlist = NextPermutation(nodeIDlist);
                nodeIDlist.CopyTo(currentpath, 1);
                currentroute = new route(distancelist, currentpath);

                if (currentroute.netdistance < fastestroute.netdistance)
                {
                    currentpath.CopyTo(fastestpath, 0);
                    fastestroute = new route(distancelist, fastestpath);
                }
            }
            fastestroute.printPath();
            Console.WriteLine("\nDistance:  " + fastestroute.netdistance);
            Console.WriteLine("Traveling from and back to Node# " + startnode.id);
        }

        public void BFS(List<DirectedNode> nodelist, int goalNodeid)
        {
            Queue<DirectedNode> queue = new Queue<DirectedNode>();
            queue.Enqueue(nodelist[0]);
            DirectedNode frontiernode;
            int[] adjlist;

            while (queue.Count() != 0)
            {
                frontiernode = queue.Dequeue();
                adjlist = frontiernode.adjnodes;
                if (adjlist == null) { break; }

                foreach (int i in adjlist)
                {
                    queue.Enqueue(nodelist[i]);

                    node previousnode = nodelist[nodelist[i].PreviousNodeid];
                    float edgeweight = Driver.distance(frontiernode, nodelist[i]);
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
            int prevNodeId = goalNodeid - 1;
            Console.Write("Fastest Path: " + prevNodeId);
            while (prevNodeId != 0)
            {
                Console.Write(" <- " + nodelist[prevNodeId].PreviousNodeid);
                prevNodeId = nodelist[prevNodeId].PreviousNodeid;
            }
            Console.Write("\nDistance: " + nodelist[goalNodeid - 1].ShortestDistanceTo);
        }

        public void DFS(List<DirectedNode> nodelist, int goalNodeid)
        {
            Stack<DirectedNode> stack = new Stack<DirectedNode>();
            stack.Push(nodelist[0]);
            DirectedNode frontiernode;
            int[] adjlist;

            while (stack.Count() != 0)
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
            int prevNodeId = goalNodeid - 1;
            Console.Write("Path: " + prevNodeId);
            while (prevNodeId != 0)
            {
                Console.Write(" <- " + nodelist[prevNodeId].PreviousNodeid);
                prevNodeId = nodelist[prevNodeId].PreviousNodeid;
            }
            Console.Write("\nDistance: " + nodelist[goalNodeid - 1].ShortestDistanceTo);
        }

        public static void farthestInsertion(List<Edge> edgelist, List<node> nodelist, node startnode, Form1 f)
        {
            Thread thisThread = Thread.CurrentThread;
            int count = nodelist.Count();
            Edge greatestEdge = edgelist[0];
            List<node> markednodes = new List<node>();
            List<Edge> Route = new List<Edge>();
            f.route = Route;
            List<node> nodelistcopy = new List<node>(nodelist);
            f.secondThread = thisThread;
            f.marked = markednodes;

            markednodes.Add(startnode);
            nodelist.Remove(startnode);

            for (int i = 0; i < edgelist.Count(); i++)
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

            Route.Add(greatestEdge);
            Route.Add(new Edge(greatestEdge.ToNode, greatestEdge.FromNode));

            markednodes.Add(greatestEdge.ToNode);
            nodelist.Remove(greatestEdge.ToNode);

            f.Invoke(f.addPanelDelegate);

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

            foreach (node item in nodelist)
            {
                if (item.ClosestEdgeDist < closestnode.ClosestEdgeDist)
                {
                    closestnode = item;
                }
            }
            markednodes.Add(closestnode);
            nodelist.Remove(closestnode);

            Route.Remove(Route[0]);

            Route.Insert(0, new Edge(closestnode, greatestEdge.ToNode));
            Route.Insert(0, new Edge(greatestEdge.FromNode, closestnode));

            f.Invoke(f.refresherDelegate);

            while (Route.Count() < count)
            {
                closestnode = nodelist[0];
                List<node> nodelistcopy2 = new List<node>(nodelist);
                Edge edge2;

                foreach (node item in nodelistcopy2)
                {
                    foreach (Edge edge in Route)
                    {
                        NodetoEdgeStart = edgelist.Find(i => ((i.ToNode == item) && (i.FromNode == edge.FromNode)));
                        NodetoEdgeEnd = edgelist.Find(i => ((i.ToNode == edge.ToNode) && (i.FromNode == item)));
                        float CrossProduct = Math.Abs((edge.xdif * NodetoEdgeStart.ydif) - (edge.ydif * NodetoEdgeStart.xdif));
                        float D = CrossProduct / edge.distance;
                        float Dotproduct = edge.xdif * NodetoEdgeStart.xdif + edge.ydif * NodetoEdgeStart.ydif;
                        float t = Dotproduct / (edge.distance * edge.distance);
                        edge2 = Route.Find(i => (i.ToNode == edge.FromNode));
                        NodetoEdge2Start = edgelist.Find(i => ((i.ToNode == item) && (i.FromNode == edge2.FromNode)));
                        NodetoEdge2End = edgelist.Find(i => ((i.ToNode == edge2.ToNode) && (i.FromNode == item)));
                        float D2 = NodetoEdge2Start.distance + NodetoEdge2End.distance + edge.distance;
                        float D1 = NodetoEdgeStart.distance + NodetoEdgeEnd.distance + edge2.distance;

                        if ((t > 0) && (t < 1))
                        {
                            if (!Route.Contains(item.ClosestEdge))
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
                        else if (!Route.Contains(item.ClosestEdge))
                        {
                            item.ClosestEdgeDist = NodetoEdgeStart.distance;
                            item.ClosestEdge = edge;
                        }

                        if (NodetoEdgeStart.distance <= item.ClosestEdgeDist)
                        {
                            item.ClosestEdgeDist = NodetoEdgeStart.distance;
                            if (D1 < D2)
                            {
                                item.ClosestEdge = edge;
                            }
                            else if (D1 > D2)
                                item.ClosestEdge = edge2;
                            else
                                item.ClosestEdge = edge;
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

                Route.Insert(Route.IndexOf(closestnode.ClosestEdge), new Edge(closestnode, closestnode.ClosestEdge.ToNode));
                Route.Insert(Route.IndexOf(closestnode.ClosestEdge) - 1, new Edge(closestnode.ClosestEdge.FromNode, closestnode));
                markednodes.Add(closestnode);
                nodelist.Remove(closestnode);
                Route.Remove(closestnode.ClosestEdge);

                f.Invoke(f.refresherDelegate);
            }
        }
    }
}