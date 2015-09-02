using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class DirectedNode: node
    {
        private int PreviousNodeid;
        private float ShortestDistanceTo;

        private int[] adjnodes;
        
        public DirectedNode(int id, float n, float z, int[] neighbors = null): base(id, n, z)
        {
            adjnodes = neighbors;
            ShortestDistanceTo = 0;
            PreviousNodeid = 0;
        }
        public int[] getAdjNodes() { return this.adjnodes; }
        public void setAdjNodes(int[] a) { adjnodes = a; }

        public float getShortestDistanceTo() { return ShortestDistanceTo; }
        public void setShortestDistanceTo(float e) { ShortestDistanceTo = e;}

        public int getPrevNodeId() { return PreviousNodeid; }
        public void setPrevNodeId(int id) { PreviousNodeid = id; }
    }
   
}
