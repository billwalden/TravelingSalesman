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
                g.FillEllipse(blackbrush, item.getx() * 5, item.gety() * 5, 8, 8);

            }
            foreach (Edge edge in edgelist)
            {
                g.DrawLine(blackpen, edge.FromNode.getx() * 5, edge.FromNode.gety() * 5, edge.ToNode.getx() * 5, edge.ToNode.gety() * 5);
            }
            foreach (node item in marked)
            {
                g.FillEllipse(redbrush, item.getx() * 5, item.gety() * 5, 8, 8);
              
            }
            
            g.FillEllipse(greenbrush, marked.Last().getx() * 5, marked.Last().gety() * 5, 8, 8);

            if(edgelist.Count() == nodelist.Count())
            {
                button1.Hide();
                float distance = 0;
                foreach(Edge item in edgelist)
                {
                    distance += item.getdist();
                }
                textBox2.Text = distance.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            panel1.Refresh();
            textBox1.Refresh();
            textBox1.Text = "";
            foreach (Edge item in edgelist)
            {
                textBox1.Text += item.FromNode.getid();
            }
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
