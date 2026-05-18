using System;
using System.Collections.Generic;
using System.Text;

namespace final_project
{
    public class LineConnections
    {
        public BaseShape ShapeA
        {
            get; set; 
        }

        public BaseShape ShapeB 
        {
            get; set;
        }

        public LineConnections(BaseShape shapeA, BaseShape shapeB)
        {
            ShapeA = shapeA;
            ShapeB = shapeB;
        }

        public void Draw(Graphics g)
        {
            PointF centerA = GetShapeCenter(ShapeA);
            PointF centerB = GetShapeCenter(ShapeB);

            using (Pen pen = new Pen(Color.Black, 3)) 
            {
                g.DrawLine(pen, centerA, centerB);
            }
        }

        private PointF GetShapeCenter(BaseShape shape)
        {
            if (shape is ParallelogramShape para)
            {
                return new PointF(para.X + para.Width / 2f, para.Y + para.Height / 2f);
            }
            if (shape is RectangleShape rect)
            {
                return new PointF(rect.X + rect.Width / 2f, rect.Y + rect.Height / 2f);
            }
            return new PointF(shape.X + 25f, shape.Y + 25f);
        }
    }
}
