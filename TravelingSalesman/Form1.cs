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
        public List<Edge> route { get; set; }
        public List<node> marked { get; set; }
        node startnode; 
        public Thread formThread { get; set; }
        public Thread secondThread { get; set; }
        int count;
        public delegate void addpanel2();
        public addpanel2 addPanelDelegate;
        public delegate void Refresher();
        public Refresher refresherDelegate;
        Panel panel1;
        Panel panel2;
        TextBox textBox1;
        Button startButton;

        public Form1()
        {
            InitializeComponent();
            maskedTextBox1.Mask = "000";
            maskedTextBox1.Text = "20";
            formThread = Thread.CurrentThread;
            addPanelDelegate = new addpanel2(addPanel2);
            refresherDelegate = new Refresher(refresher);

        }

        Pen blackpen = new Pen(Color.Black);
        Brush blackbrush = new SolidBrush(Color.Black);
        Brush redbrush = new SolidBrush(Color.Red);
        Brush greenbrush = new SolidBrush(Color.Green);
        Brush purplebrush = new SolidBrush(Color.Purple);
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();


            toolTip1.AutoPopDelay = 4000;
            toolTip1.InitialDelay = 250;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            toolTip1.SetToolTip(this.button2, "Set number of random positioned nodes to\n" +
                                                "be generated the box to right");
            startButton = new Button();
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(12, 3);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(98, 44);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Find route";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click_1);

            panel1 = new Panel();
            this.panel1.Location = new System.Drawing.Point(0, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(569, 548);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);

            panel2 = new Panel();
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(569, 548);
            this.panel2.TabIndex = 5;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);

            textBox1 = new TextBox();
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(321, 9);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox2";
            this.textBox1.Size = new System.Drawing.Size(238, 29);
            this.textBox1.TabIndex = 2;
            //this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

             foreach (node item in nodelist)
             {
                 g.FillEllipse(blackbrush, item.x * 5, item.y * 5, 8, 8);

             }
              g.FillEllipse(greenbrush, startnode.x * 5, startnode.y * 5, 8, 8);
            
        }
        

        private void startButton_Click_1(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Driver.farthestInsertion(edgelist, nodelist, startnode, this);
            }).Start();
            startButton.Enabled = false;
            panel1.Dispose();
        }
        private void node_added(object sender, EventArgs e)
        {

        }

        private void textBox1_Texthanged(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int numNodes = int.Parse(maskedTextBox1.Text);
            maskedTextBox1.ReadOnly = true;
            nodelist = Driver.createRandomNodeList(numNodes);
            edgelist = Driver.createEdgeList(nodelist);
            startnode = nodelist[0];
            this.count = nodelist.Count();       
            this.Controls.Add(panel1);
            button2.Hide();
            this.Controls.Add(startButton);
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }
        public void addPanel2()
        {
            secondThread.Suspend();
            this.Controls.Add(panel2);
        }

        public void refresher()
        {
            secondThread.Suspend();
            panel2.Refresh();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (node item in nodelist)
            {
                if (!marked.Contains(item))
                    g.FillEllipse(blackbrush, item.x * 5, item.y * 5, 8, 8);

            }
            foreach (Edge edge in route)
            {
                g.DrawLine(blackpen, edge.FromNode.x * 5, edge.FromNode.y * 5, edge.ToNode.x * 5, edge.ToNode.y * 5);
            }
            foreach (node item in marked)
            {
                g.FillEllipse(redbrush, item.x * 5, item.y * 5, 8, 8);
            }

            g.FillEllipse(greenbrush, startnode.x * 5, startnode.y * 5, 8, 8);
            g.FillEllipse(purplebrush, marked.Last().x * 5, marked.Last().y * 5, 8, 8);


            if (route.Count() == this.count)
            {

                float distance = 0;
                foreach (Edge item in route)
                {
                    distance += item.distance;

                }
        
                this.Controls.Add(textBox1);
                textBox1.Text = "Route Distance:  " + distance.ToString();
                this.textBox1.ReadOnly = true;
            }
            secondThread.Resume();
        }
    }
}
