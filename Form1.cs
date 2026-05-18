using System.Drawing;
namespace C__project

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool flag = false;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            flag = !flag;
            if (flag)
            {
                button1.Text = "end";
            }
            else
            {
                button1.Text = "start";
            }
            pictureBox1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (flag)
            {
                g.FillEllipse(Brushes.Crimson, 50, 50, 50,50);
            }
            else
            {
                g.Clear(Color.White);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
