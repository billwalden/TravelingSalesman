using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Edge
    {
        private float xdif;
        private float ydif;
        private float distance;
        public node FromNode { get; }
        public node ToNode { get; }
        public Edge(node Node1, node Node2)
        {

            FromNode = Node1;
            ToNode = Node2;
            distance = Program.distance(Node1.getx(), Node1.gety(), Node2.getx(), Node2.gety());
            xdif = Node2.getx() - Node1.getx();
            ydif = Node2.gety() - Node1.gety();
        }
        public float getXdif() { return xdif; }
        public float getYdif() { return ydif; }
        public float getdist() { return distance; }
        

        
    }
}
