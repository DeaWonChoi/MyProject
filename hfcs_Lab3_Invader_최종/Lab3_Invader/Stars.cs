using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Invader
{
    class Stars : Draw
    {
        private List<Star> starList;
        private Random random;
        private Rectangle rectangle;
        private const int starsNumber = 300;
        private Pen pen;
        
        public Stars(Random random, Rectangle rectangle)
        {
            this.random = random;
            this.rectangle = rectangle;

            starList = new List<Star>();

            for (int i = 0; i < starsNumber; i++)
            {
                pen = new Pen(Color.FromArgb(random.Next(100, 255), 255, 255, 255));
                starList.Add(new Star(new Point(random.Next(1, rectangle.Width), random.Next(1, rectangle.Height)),pen));
                //Console.WriteLine("starList.point = " + starList[i].Point);
            }
            //Console.WriteLine("starList.Count = " + starList.Count);
        }

        public void Draw(Graphics g)
        {
            for (int i = 0; i < starsNumber; i++)
            {
                g.DrawRectangle(starList[i].Pen, starList[i].Point.X, starList[i].Point.Y, 1, 1);
                starList[i].downfall(rectangle);
            }
            //Console.WriteLine("starList[1].point = " + starList[1].Point);
        }
    }
}
