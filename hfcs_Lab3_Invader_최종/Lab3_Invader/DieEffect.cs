using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Invader
{
    class DieEffect
    {
        Point point;
        Color color;
        Graphics g;
        DateTime time;
        Pen pen;
        Rectangle rectangle;
        int frame;
        int x;
        int y;
        int width;
        int height;

        public DieEffect(Point point, Graphics g)
        {
            this.point = point;
            this.color = color;
            this.g = g;
            time = DateTime.Now;
            pen = new Pen(color);
            x = point.X;
            y = point.Y;
            width = 200;
            height = 200;
            rectangle = new Rectangle(x - width / 2, y - height / 2, width, height);
            frame = 0;
        }

        public bool draw(Graphics g)
        {
            
            if (frame < 9)
            {
                g.DrawImage(showimage(frame), rectangle);
                frame++;
                return true;
            }
            else return false;
        }

        private Image showimage(int ani)
        {
            switch (ani)
            {
                case 0:
                    return Properties.Resource.row_1_col_1;
                case 1:
                    return Properties.Resource.row_1_col_2;
                case 2:
                    return Properties.Resource.row_1_col_3;
                case 3:
                    return Properties.Resource.row_2_col_1;
                case 4:
                    return Properties.Resource.row_2_col_2;
                case 5:
                    return Properties.Resource.row_2_col_3;
                case 6:
                    return Properties.Resource.row_3_col_1;
                case 7:
                    return Properties.Resource.row_3_col_2;
                case 8:
                    return Properties.Resource.row_3_col_3;
                default:
                    return null;
            }
            
        }
    }
}
