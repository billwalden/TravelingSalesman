using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class node
    {
        private int id;
        private float x;
        private float y;
        public Edge ClosestEdge { get; set; }
        public float ClosestEdgeDist { get; set; }




        public node(int j, float n, float z)
        {
            x = n;
            y = z;
            id = j;
        }
         
        public float getx() { return x; }
        public float gety() { return y; }
        public void setx(float n) { x = n; }
        public void sety(float n) { y = n; }
        public int getid() { return id; }
        


    }
}
