using final_project;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace C__project

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool flag = false;

        private List<BaseShape> shapeList = new List<BaseShape>();

        Color currentColor = Color.Empty;

        string currentShape = "";

        bool IsLineMode = false;

        List<LineConnections> connectionsList = new List<LineConnections>();

        BaseShape firstSelectedShape = null;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            flag = !flag;
            if (flag)
            {
                button1.Text = "end";
                shapeList.Clear();
                connectionsList.Clear();   
                firstSelectedShape = null;

            }
            else
            {
                button1.Text = "start";
            }
            pictureBox1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Filter = "model files (.mdl)|*.mdl|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string json = File.ReadAllText(openFileDialog1.FileName);
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.Converters.Add(new ColorJsonConverter()); 
                MyGameData loadedData = JsonSerializer.Deserialize<MyGameData>(json, options);

                if (loadedData != null)
                {
                    shapeList = loadedData.Shapes;
                    connectionsList = loadedData.Connections;
                    pictureBox1.Invalidate(); 
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (flag)
            {
                foreach (LineConnections connection in connectionsList)
                {
                    connection.Draw(g);
                }

                foreach (BaseShape shape in shapeList)
                {
                    shape.Draw(g);
                }
            }
            else
            {
                g.Clear(Color.White);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int i;
            if (flag)
            {
                if(!IsLineMode && (currentShape == "" || currentColor == Color.Empty))
                {
                    return;
                }
                if (e.Button == MouseButtons.Right)
                {
                    for (i = shapeList.Count - 1; i >= 0; i--)
                    {
                        if (shapeList[i].IsPointInside(e.X, e.Y))
                        {
                            BaseShape shapeToDelete = shapeList[i];
                            shapeList.RemoveAt(i);
                            for (int j = connectionsList.Count - 1; j >= 0; j--)
                            {
                                if (connectionsList[j].ShapeA == shapeToDelete || connectionsList[j].ShapeB == shapeToDelete)
                                {
                                    connectionsList.RemoveAt(j);
                                }
                            }
                            firstSelectedShape = null;
                            pictureBox1.Invalidate();
                            return;
                        }
                    }
                    return; 
                }
                if (IsLineMode)
                {
                    for (i = shapeList.Count - 1; i >= 0; i--)
                    {
                        if (shapeList[i].IsPointInside(e.X, e.Y))
                        {
                            if (firstSelectedShape == null)
                            {
                                firstSelectedShape = shapeList[i];
                                return;
                            }
                            else
                            {
                                BaseShape secondSelectioinShape = shapeList[i];
                                if (firstSelectedShape.GetType() == secondSelectioinShape.GetType())
                                {
                                    connectionsList.Add(new LineConnections(firstSelectedShape, secondSelectioinShape));
                                    pictureBox1.Invalidate();
                                }
                                else
                                {
                                    MessageBox.Show("הצורות אינן זהות, או שלחצת על אותה הצורה פעמיים!");
                                }
                                firstSelectedShape = null;
                                return;
                            }
                        }
                    }
                    return;
                }
                else
                {
                    for (i = shapeList.Count - 1; i >= 0; i--)
                    {
                        if (shapeList[i].IsPointInside(e.X, e.Y))
                        {
                            if (shapeList[i] is ParallelogramShape && !(shapeList[i] is RectangleShape))
                            {
                                ParallelogramShape temp = (ParallelogramShape)shapeList[i];
                                int currentX = temp.X;
                                int currentY = temp.Y;
                                Color currColor = temp.ShapeColor;
                                int currentWidth = temp.Width;
                                int currentHeight = temp.Height;
                                shapeList[i] = new RectangleShape(currentX, currentY, currColor, currentWidth, currentHeight + 50);
                                for (int j = connectionsList.Count - 1; j >= 0; j--)
                                {
                                    if (connectionsList[j].ShapeA == temp || connectionsList[j].ShapeB == temp)
                                    {
                                        connectionsList.RemoveAt(j);
                                    }
                                }
                                pictureBox1.Invalidate();
                            }
                            return;
                        }
                    }
                }
                switch (currentShape)
                {
                    case "Circle":
                        shapeList.Add(new CircleShape(e.X, e.Y, currentColor, 50));
                        break;
                    case "Parallelogram":
                        shapeList.Add(new ParallelogramShape(e.X, e.Y, currentColor, 20, 50, 50));
                        break;
                    case "Rectangle":
                        shapeList.Add(new RectangleShape(e.X, e.Y, currentColor, 50, 100));
                        break;
                    default:
                        break;
                }
                pictureBox1.Invalidate();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            currentColor = Color.Yellow;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            currentColor = Color.Black;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            currentColor = Color.DarkBlue;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                currentShape = "Rectangle";
                IsLineMode = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                currentShape = "Parallelogram";
                IsLineMode = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                currentShape = "Circle";
                IsLineMode = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                IsLineMode = true;
                firstSelectedShape = null;
                MessageBox.Show("עברת למצב מתיחת קווים! לחץ על שתי צורות זהות כדי לחבר אותן.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog1.Filter = "model files (.mdl)|.mdl|All files (.)|.";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MyGameData saveData = new MyGameData
                {
                    Shapes = shapeList,
                    Connections = connectionsList
                };
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                options.Converters.Add(new ColorJsonConverter()); 

                string json = JsonSerializer.Serialize(saveData, options);
                File.WriteAllText(saveFileDialog1.FileName, json);
            }
        }
    }
    public class MyGameData
    {
        public List<BaseShape> Shapes { get; set; }
        public List<LineConnections> Connections { get; set; }
    }
    public class ColorJsonConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Color.FromArgb(reader.GetInt32());
        }
        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.ToArgb());
        }
    }
}
