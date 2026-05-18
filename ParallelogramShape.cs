using System;
using System.Collections.Generic;
using System.Text;

namespace final_project
{
    public class ParallelogramShape : BaseShape
    {
        public int Offset
        {
            get; set;
        }

        public int Height
        {
            get; set;
        }

        public int Width
        {
            get; set;
        }

        public ParallelogramShape(int x, int y, Color shapeColor, int offset, int height, int width) : base(x, y, shapeColor)
        {
            Offset = offset;
            this.Height= height;
            this.Width = width;
        }

        public override void Draw(Graphics g)
        {
            PointF point1 = new PointF((X + Offset), Y);
            PointF point2 = new PointF((X + Offset + Width), Y);
            PointF point3 = new PointF((X + Width), (Y + Height));
            PointF point4 = new PointF(X, (Y + Height));
            PointF[] pointsArray = [point1, point2, point3, point4];

            using (Brush brush = new SolidBrush(ShapeColor))
            {
                g.FillPolygon(brush, pointsArray);
            }
        }
        public override bool IsPointInside(int mouseX, int mouseY)
        {
            return (mouseX >= X && mouseX <= X + Width && mouseY >= Y && mouseY <= Y + Height);
        }
    }
}
