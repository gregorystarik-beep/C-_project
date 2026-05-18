using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace final_project
{
    public class CircleShape : BaseShape
    {
        public double Radius
        {
            get; set; 
        }
        public CircleShape(int x,  int y, Color shapeColor, double radius) : base(x, y, shapeColor)
        {
            Radius = radius;
        }

        public override void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(ShapeColor))
            {
                float fRadius = (float)Radius;
                g.FillEllipse(brush, X, Y, fRadius, fRadius);
            }
        }

        public override bool IsPointInside(int mouseX, int mouseY)
        {
            return Math.Sqrt((mouseX - X) * (mouseX - X) + (mouseY - Y) * (mouseY - Y)) < Radius;
        }
    }
}
