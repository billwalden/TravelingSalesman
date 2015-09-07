using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class route
    { 
        private float[,] distanceList;
        public int[] path { get; }
        public float netdistance { get; set; }

        public route(float[,] distances, int[] iarray)
        {
            distanceList = distances;
            path = iarray;
            calcNetDist(this.distanceList, this.path);
        }
        
        public void calcNetDist(float[,] distanceList, int[] path)
        {
            for (int i = 0; i < path.Length - 1; i++)
            {
                if (path[i] == path[path.Length - 1])
                {
                    if (path[0] < path[i])
                        this.netdistance += distanceList[path[0], (path[i] - path[0]) - 1];
                    else
                    {

                        this.netdistance += distanceList[path[i], (path[0] - path[i])];
                    }
                }
                else if (path[i] > path[i + 1])
                    this.netdistance += distanceList[path[i + 1], path[i] - path[i + 1] - 1];
                else
                    this.netdistance += distanceList[path[i], path[i + 1] - path[i] - 1]; 
            }
            if (path[0] < path[path.Length-1])
                this.netdistance += distanceList[path[0], (path[path.Length-1] - path[0]) - 1];
            else
            {

                this.netdistance += distanceList[path[path.Length], (path[0] - path[path.Length-1])];
            }



        }
        
        public void printPath() { Console.Write("Path: "); foreach (int i in path) { Console.Write(i+ " -> "); }Console.Write(path[0]); }
    
    }
}
