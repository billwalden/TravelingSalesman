using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class routes
    {
        public List<route> routeList { get; }

        public routes(float[,] distanceList, List<int[]> paths)
        {
            this.routeList = new List<route>();
            for (int i = 0; i < paths.Count; i++)

            {
             
                route route = new route(distanceList, paths[i]);
                this.routeList.Add(route);
            }
            this.routeList = routeList.OrderBy(x => x.netdistance).ToList();
        }

        public void printDistances()
        {
            foreach(route item in routeList)
            {
                float d = item.getNetDist();
                Console.Write("\n" + d);
            }
        }
        public void printFastestDistance(int startnodeID)
        {
            route fastest = routeList.Find(i => (i.path[0] == startnodeID));
            Console.WriteLine("\nFastest Distance:  " + fastest.getNetDist());
            Console.WriteLine("Starting from and traveling back to node# " + startnodeID);
        }
        
    }
}
