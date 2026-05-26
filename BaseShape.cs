using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.Json.Serialization;

namespace final_project
{
    [JsonPolymorphic]
    [JsonDerivedType(typeof(CircleShape), typeDiscriminator: "Circle")]
    [JsonDerivedType(typeof(RectangleShape), typeDiscriminator: "Rectangle")]
    [JsonDerivedType(typeof(ParallelogramShape), typeDiscriminator: "Parallelogram")]
    public abstract class BaseShape//base abstract class
    {
        public int X
        {
            get; set; 
        }
        public int Y
        {
            get; set;
        }
        public Color ShapeColor
        {
            get; set; 
        }
        public BaseShape(int x, int y,  Color shapeColor)
        {
            X=x;
            Y=y;
            ShapeColor=shapeColor;
        }
        public abstract void Draw(Graphics g);

        public abstract bool IsPointInside(int mouseX, int mouseY);
    }
}//base class all classes derive from him
