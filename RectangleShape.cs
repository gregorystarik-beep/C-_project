using System;
using System.Collections.Generic;
using System.Text;

namespace final_project
{
    public class RectangleShape : ParallelogramShape//dirived from shape and ParallelogramShape
    {
        public RectangleShape(int x, int y, Color shapeColor, int height, int width): base(x, y, shapeColor, 0, height, width) { }

        public override void Draw(Graphics g)
        {
           base.Draw(g);
        }

        public override bool IsPointInside(int mouseX, int mouseY)//overrrides with polymorphisem 
        {
            return base.IsPointInside(mouseX, mouseY);
        }
    }
}
