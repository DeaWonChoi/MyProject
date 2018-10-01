using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Invader
{
    class EnemyDie
    {
        Point point;
        DateTime dateTimeNow;
        DateTime dateTimeAfterSecond;
        int score;

        public EnemyDie(Point point, int score)
        {
            this.point = point;
            dateTimeNow = DateTime.Now;
            dateTimeAfterSecond = dateTimeNow.AddSeconds(1);
            //Console.WriteLine(dateTimeAfterSecond);
            this.score = score;
        }

        public bool die()
        {
            if (dateTimeAfterSecond <= DateTime.Now)
            {
                return true;
            }
            return false;
        }

        public void draw(Graphics g)
        {
            g.DrawString(score.ToString(), Invader_최대원.DefaultFont, new SolidBrush(Color.White), point);
            point.Y -= 2;
        }
    }
}
