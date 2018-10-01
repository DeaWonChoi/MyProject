using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Invader
{
    class Star
    {
        private Point point;
        private Pen pen;
        public Point Point {
            get { return point; }
            set { point = value; }
        }
        public Pen Pen { get { return pen; } }

        public Star(Point point, Pen pen)
        {
            this.point = point;
            this.pen = pen;
        }

        public void downfall(Rectangle rectangle)
        {
            point.Y = point.Y + (pen.Color.A / 40);
            if (point.Y > rectangle.Height) { point.Y = 0; }
        }
    }

    
}
