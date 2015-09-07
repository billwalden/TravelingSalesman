using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class DirectedNode: node
    {
        public int PreviousNodeid { get; set; }
        public float ShortestDistanceTo { get; set; }
        public int[] adjnodes{ get; set; }
        
        public DirectedNode(int id, float n, float z, int[] neighbors = null): base(id, n, z)
        {
            adjnodes = neighbors;
            ShortestDistanceTo = 0;
            PreviousNodeid = 0;
        }
    }
   
}
