using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class node
    {
        public int id { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public Edge ClosestEdge { get; set; }
        public float ClosestEdgeDist { get; set; }

        public node(int j, float n, float z)
        {
            x = n;
            y = z;
            id = j;
        }

    }
}
