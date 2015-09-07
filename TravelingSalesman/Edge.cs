using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Edge
    {
        public float xdif { get; set; }
        public float ydif { get; set;}
        public float distance { get; set; }
        public node FromNode { get; }
        public node ToNode { get; }
        public Edge(node Node1, node Node2)
        {

            FromNode = Node1;
            ToNode = Node2;
            distance = Driver.distance(Node1.x, Node1.y, Node2.x, Node2.y);
            xdif = Node2.x - Node1.x;
            ydif = Node2.y - Node1.y;
        } 
    }
}
