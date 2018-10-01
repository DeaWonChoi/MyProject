using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Invader
{
    enum EnemyImage
    {
        Star,
        FlyingCup,
        SpaceShip,
        Satellite,
        Bug,
    }


    class Enemy : Draw
    {
        const int moveHorizonInterval = 10;
        const int moveVerticalInterval = 30;
        private EnemyImage enemytype;
        private Bitmap bitmap;
        public int score { get; private set; }
        private Rectangle area;
        public Rectangle Area { get { return area; } }
        private int setEnemyImage;

        public Enemy(int enemyImage, int score, Point location)
        {
            if(enemyImage == 0)
            {
                enemytype = EnemyImage.Star;
            }
            else if (enemyImage == 1)
            {
                enemytype = EnemyImage.FlyingCup;
            }
            else if (enemyImage == 2)
            {
                enemytype = EnemyImage.SpaceShip;
            }
            else if (enemyImage == 3)
            {
                enemytype = EnemyImage.Satellite;
            }
            else if (enemyImage == 4)
            {
                enemytype = EnemyImage.Bug;
            }
            this.score = score;
            bitmap = EnemyAni(0);
            area = new Rectangle(location, bitmap.Size);
        }

        public void Move(Direction direction)
        {
            if(direction == Direction.Left)
            {
                area.X -= moveHorizonInterval;
            }
            if (direction == Direction.Right)
            {
                area.X += moveHorizonInterval;
            }
            if (direction == Direction.Up)
            {
                area.Y -= moveVerticalInterval;
            }
            if (direction == Direction.Down)
            {
                area.Y += moveVerticalInterval;
            }
        }

        public void Draw(Graphics g, int animation)
        {
            bitmap = EnemyAni(animation);

            if (bitmap != null)
            {
                g.DrawImage(bitmap, area);
            }
        }

        private Bitmap EnemyAni(int ani)
        {
            switch (enemytype)
            {
                case EnemyImage.Star:
                    if (ani == 0) return Properties.Resource.star1;
                    if (ani == 1) return Properties.Resource.star2;
                    if (ani == 2) return Properties.Resource.star3;
                    if (ani == 3) return Properties.Resource.star4;
                    else return null;
                case EnemyImage.FlyingCup:
                    if (ani == 0) return Properties.Resource.flyingsaucer1;
                    if (ani == 1) return Properties.Resource.flyingsaucer2;
                    if (ani == 2) return Properties.Resource.flyingsaucer3;
                    if (ani == 3) return Properties.Resource.flyingsaucer4;
                    else return null;
                case EnemyImage.SpaceShip:
                    if (ani == 0) return Properties.Resource.spaceship1;
                    if (ani == 1) return Properties.Resource.spaceship2;
                    if (ani == 2) return Properties.Resource.spaceship3;
                    if (ani == 3) return Properties.Resource.spaceship4;
                    else return null;
                case EnemyImage.Satellite:
                    if (ani == 0) return Properties.Resource.satellite1;
                    if (ani == 1) return Properties.Resource.satellite2;
                    if (ani == 2) return Properties.Resource.satellite3;
                    if (ani == 3) return Properties.Resource.satellite4;
                    else return null;
                case EnemyImage.Bug:
                    if (ani == 0) return Properties.Resource.bug1;
                    if (ani == 1) return Properties.Resource.bug2;
                    if (ani == 2) return Properties.Resource.bug3;
                    if (ani == 3) return Properties.Resource.bug4;
                    else return null;
                default: return null;
            }
        }
    }
}
