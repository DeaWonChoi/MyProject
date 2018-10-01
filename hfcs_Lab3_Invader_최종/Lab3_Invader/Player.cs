using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Invader
{
    class Player : Draw
    {
        private const int moveHorizonInterval = 25;
        //private const int moveVerticalInterval = 25;
        private int deadShipHeight;
        public DateTime deadtime { get; set; }
        public Rectangle area { get; private set; }
        public Bitmap image { get; private set; }
        public bool Alive;
        public Rectangle bounderies;
        private Point location;
        public Point Location
        {
            get { return location; }
        }

        public Player(Point location, Rectangle bounderies)
        {
            Alive = true;
            this.location = location;
            SetImagePlayer();
            area = new Rectangle(location, image.Size);
            this.bounderies = bounderies;
        }

        public void SetImagePlayer()
        { 
            image = new Bitmap(Properties.Resource.player);
            deadShipHeight = image.Height;
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(image, Location.X,Location.Y, image.Width, deadShipHeight);
            area = new Rectangle(Location.X, Location.Y, image.Width, deadShipHeight);
            
            //g.DrawRectangle(new Pen(Color.White) ,area);
            //Console.WriteLine("player draw" + Location + image.ToString());
        }

        public void Move(Direction direction)
        {
            if(direction == Direction.Left && Location.X > bounderies.X)
            {
                location.X -= moveHorizonInterval;
            }
            if (direction == Direction.Right && Location.X + area.Width*1.5 < bounderies.Width)
            {
                location.X += moveHorizonInterval;
            }
        }

        public void Dead()
        {
            if (!Alive)
            {
                if (deadShipHeight > 1)
                {
                    deadShipHeight -= 1;
                    //image = (Bitmap)resizeImage(image, new Size(area.Width, deadShipHeight));
                }
            }
        }

        //private static Image resizeImage(Image image, Size size)
        //{
        //    return new Bitmap(image, size);
        //}
    }
}
