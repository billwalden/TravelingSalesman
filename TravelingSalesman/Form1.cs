using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApplication1
{
    public partial class Form1 : Form
    {
        List<node> nodelist;
        List<Edge> edgelist;
        List<node> marked;
        Thread mainthread;
        

        public Form1(List<node> nodes, List <Edge> route, List <node> markednodes, Thread thread)
        {
            InitializeComponent();
            nodelist = nodes;
            edgelist = route;
            marked = markednodes;
            mainthread = thread;

        }


        private void chart2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        Pen blackpen = new Pen(Color.Black);
        Brush blackbrush = new SolidBrush(Color.Black);
        Brush redbrush = new SolidBrush(Color.Red);
        Brush greenbrush = new SolidBrush(Color.Green);
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
          
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (node item in nodelist)
            {
                g.FillEllipse(blackbrush, item.x * 5, item.y * 5, 8, 8);

            }
            foreach (Edge edge in edgelist)
            {
                g.DrawLine(blackpen, edge.FromNode.x * 5, edge.FromNode.y * 5, edge.ToNode.x * 5, edge.ToNode.y * 5);
            }
            foreach (node item in marked)
            {
                g.FillEllipse(redbrush, item.x * 5, item.y * 5, 8, 8);
              
            }
            
            g.FillEllipse(greenbrush, marked.Last().x * 5, marked.Last().y * 5, 8, 8);

            if(edgelist.Count() == nodelist.Count())
            {
                button1.Hide();
                float distance = 0;
                foreach(Edge item in edgelist)
                {
                    distance += item.distance;
                }
                textBox2.Text = "Route Distance:  " + distance.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            panel1.Refresh();
     
            mainthread.Resume();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
