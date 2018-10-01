using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Invader
{
    class Bullet : Draw
    {
        private Point location;
        public Point Location { get { return location; } }

        private int moveInterval;
        private int width = 5;
        private int height = 15;
        private Size size;

        private Rectangle rectangle;
        public bool NotInRect;

        public Bullet(Point location, Rectangle rectangle, int speed)
        {
            this.location = location;
            this.rectangle = rectangle;
            moveInterval = speed;
            NotInRect = false;
            size = new Size(width, height);
        }

        public void draw(Graphics g, Color color)
        {
            using (SolidBrush solidBrush = new SolidBrush(color))
            {
                g.FillRectangle(solidBrush, new Rectangle(Location, size));
            }
        }

        public void Move(Direction direction)
        {
            if (direction == Direction.Up)
            {
                location.Y -= moveInterval;
            }
            if (direction == Direction.Down)
            {
                location.Y += moveInterval;
            }
            if (!rectangle.Contains(location)) NotInRect = true;
        }
    }
}
