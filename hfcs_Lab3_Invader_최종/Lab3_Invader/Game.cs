using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_Invader
{
    enum Direction
    {
        Left,
        Right,
        Up,
        Down,
    }


    class Game
    {
        private int PlayerLife;
        private Random random;
        private List<Enemy> enemyList;
        private List<Bullet> enemyBullet;
        private List<Bullet> playerBullet;
        private List<DieEffect> dieEffect;
        private int wave;
        private int gameScore;
        private Stars stars;
        private Player player;
        private int enemyani;
        private int frameskip;
        private int frameskipMax;
        private int moveDirectionEnemy;
        private bool enemyMoveDown = false;
        private int enemyShootDelay;
        private int enemyShootDelayMax;
        private List<EnemyDie> enemyScore;
        private Point drawGameScore;
        private Bitmap gameOverImage;
        public Graphics g;
        public Rectangle CilentRect;
        public delegate void GameOverEvent();
        public event GameOverEvent GameOver;
        public bool DrawLine;
        

        //생성자==================================================================
        public Game(Rectangle rectangle, Random random)
        {
            PlayerLife = 2;
            enemyList = new List<Enemy>();
            enemyBullet = new List<Bullet>();
            playerBullet = new List<Bullet>();
            enemyScore = new List<EnemyDie>();
            dieEffect = new List<DieEffect>();
            wave = 0;
            gameScore = 0;
            enemyShootDelay = 0;
            enemyShootDelayMax = 10;
            drawGameScore = new Point(20, 20);
            CilentRect = rectangle;
            this.random = random;
            stars = new Stars(random, CilentRect);
            player = new Player(new Point(350,800),CilentRect);
            enemyani = 0;
            frameskip = 0;
            frameskipMax = 3;
            moveDirectionEnemy = 1;
            NextWave();
            GameOver += new GameOverEvent(Game_GameOver);
            DrawLine = false;
        }

        //매 타이머마다 실행========================================================
        public void Update(Random random)
        {
            Twinkle(g); //background
            player.Draw(g); //player
            EnemyDraw(g); //all enemy
            EnemyBulletDraw(g); //all enemy bullet
            PlayerBulletDraw(g); //all player bullet
            KillScoreDraw(g); //kill score draw
            TotalScoreDraw(g); //draw total score
            drawPlayerLife(g); //draw player life
            CheckForEnemyByPlayerBullet(g); //check enemy dead by player bullet
            CheckForPlayerByEnemyBullet(g); //player dead
            DrawDieEffect(g);
        }

        //이하 나머지 메소드=================================================================
        private void Game_GameOver()
        {
            //Console.WriteLine("event");
            if (gameOverImage == null)
            {
                gameOverImage = new Bitmap(Properties.Resource.gameoverimage2);
                gameOverImage.MakeTransparent(gameOverImage.GetPixel(0, 0));
            }
            else
            {
                g.DrawImage(gameOverImage, new Point((CilentRect.Width - gameOverImage.Width)/2, 200));
            }
            Invader_최대원.SetGameOver();
        }//게임 오버 이벤트 발생시 호출 할 함수
        public void DrawDieEffect(Graphics g)
        {
            for (int i = dieEffect.Count - 1; i >= 0; i--)
            {
                if (!dieEffect[i].draw(g)) { dieEffect.Remove(dieEffect[i]); }
            }
        }//적 죽을 때 나올 효과
        public void CheckForPlayerByEnemyBullet(Graphics g)
        {
            if (CheckForPlayerCollision())
            {
                player.Dead();
                if (PlayerLife < 0)
                {
                    //Console.WriteLine("game over");
                    GameOver?.Invoke();
                    return;
                }
                if (DateTime.Now > player.deadtime.AddSeconds(3))
                {
                    player.Alive = true;
                    player.SetImagePlayer();
                }
            }
            else
            {
                EnemyShoot();
            }
        }
        public void TotalScoreDraw(Graphics g)
        {
            g.DrawString(gameScore.ToString(), Invader_최대원.DefaultFont, new SolidBrush(Color.Red), drawGameScore);
            g.DrawString("Lavel #" + wave.ToString(), Invader_최대원.DefaultFont, new SolidBrush(Color.White), drawGameScore.X,drawGameScore.Y+20);

            if (DrawLine)
            {
                using (Pen dPen = new Pen(Color.White))
                {
                    g.DrawLine(dPen, new Point(CilentRect.X, CilentRect.Bottom - 50), new Point(CilentRect.Width, CilentRect.Bottom - 50));
                }
            }
        }
        public void CheckForEnemyByPlayerBullet(Graphics g)
        {
            for (int i = 0; i < playerBullet.Count; i++)
            {
                Enemy enemy = CheckForEnemyCollision(playerBullet[i].Location);

                if (enemy != null)
                {
                    enemyScore.Add(new EnemyDie(playerBullet[i].Location, enemy.score));
                    gameScore += enemy.score;
                    dieEffect.Add(new DieEffect(playerBullet[i].Location, g));
                    enemyList.Remove(CheckForEnemyCollision(playerBullet[i].Location));
                    playerBullet.Remove(playerBullet[i]);
                }
            }
        }//적이 죽을 때 각종 처리
        public void KillScoreDraw(Graphics g)
        {
            if (enemyScore.Count > 0)
            {
                for (int i = 0; i < enemyScore.Count; i++)
                {
                    enemyScore[i].draw(g);
                    if (enemyScore[i].die())
                    {
                        enemyScore.Remove(enemyScore[i]);
                    }
                }
            }
        }
        public void PlayerBulletDraw(Graphics g) {
            if (playerBullet.Count > 0)
            {
                for (int i = 0; i < playerBullet.Count; i++)
                {
                    playerBullet[i].draw(g, Color.Yellow);
                    playerBullet[i].Move(Direction.Up);
                    if (playerBullet[i].NotInRect)
                        playerBullet.Remove(playerBullet[i]);
                }
            }
        }
        public void EnemyBulletDraw(Graphics g) {
            if (enemyBullet.Count > 0)
            {
                for (int i = 0; i < enemyBullet.Count; i++)
                {
                    enemyBullet[i].draw(g, Color.Orange);
                    enemyBullet[i].Move(Direction.Down);
                    if (enemyBullet[i].NotInRect)
                        enemyBullet.Remove(enemyBullet[i]);
                }
            }
        }
        public void EnemyDraw(Graphics g)
        {
            if (enemyList.Count > 0)
            {
                if (enemyani <= 3)
                {
                    //Console.WriteLine("frameskip = {0}, enemyani = {1}", frameskip.ToString(), enemyani.ToString());
                    for (int i = 0; i < enemyList.Count; i++)
                    {
                        enemyList[i].Draw(g, enemyani);
                    }
                    if (frameskip < frameskipMax)
                    {
                        frameskip++;
                    }
                    else
                    {
                        frameskip = 0;
                        enemyani++;
                        if (enemyani == 4) enemyani = 0;
                        //enemy draw
                        if (player.Alive)
                        {
                            CheckForMoveAllEnemy();
                            MovingAllEnemy(moveDirectionEnemy);
                        }
                    }
                }
            }
            else NextWave();
        }
        public void drawPlayerLife(Graphics g)
        {
            int drawInterval = player.image.Width + 5;
            for (int i = 0; i < PlayerLife; i++)
            {
                g.DrawImage(Properties.Resource.player, new Point(CilentRect.Width - drawInterval * (i + 1),10));
            }
        }
        public void NextWave()
        {
            int offsetX = 100;
            int offsetY = 400;
            int respawnIntervalX = 100;
            int respawnIntervalY = 70;
            wave++;
            enemyList.Clear();
            enemyBullet.Clear();
            playerBullet.Clear();
            for (int i = 0; i < 30; i++)
            {
                int col = i % 6;
                int row = i / 6;
                int _image = i / 6;
                enemyList.Add(new Enemy(_image, (_image+1)*10, 
                    new Point(
                        (CilentRect.X + offsetX) + (col * respawnIntervalX),
                        (CilentRect.Y + offsetY) - (row * respawnIntervalY)
                        )));
            }
        }
        public void MoveAllEnemy(Direction direction)//적을 이동시킴
        {
            if(enemyList.Count > 0 && player.Alive)
            {
                foreach (Enemy item in enemyList)
                {
                    item.Move(direction);
                    if(item.Area.Bottom > CilentRect.Bottom-50)
                    {
                        PlayerLife = -1;
                        player.Alive = false;
                        GameOver?.Invoke();
                    }
                }
            }
        }
        private void MovingAllEnemy(int dir)
        {
            if (dir == 1)
                MoveAllEnemy(Direction.Right);
            if (dir == -1)
                MoveAllEnemy(Direction.Left);
            if(enemyMoveDown)
            {
                MoveAllEnemy(Direction.Down);
                enemyMoveDown = false;
            }
        }
        public bool CheckForPlayerCollision()
        {
            if (!player.Alive) return true;
            else
            {
                for (int i = 0; i < enemyBullet.Count; i++)
                {
                    if (player.area.Contains(enemyBullet[i].Location))
                    {
                        player.Alive = false;
                        PlayerLife--;
                        player.deadtime = DateTime.Now;
                        return true;
                    }
                }
                return false;
            }
        }
        private void CheckForMoveAllEnemy() 
        {
            if (enemyList.Count > 0)
            {
                foreach (Enemy item in enemyList)
                {
                    if(item.Area.X < CilentRect.X + 50)
                    {
                        moveDirectionEnemy = 1;
                        enemyMoveDown = true;
                    }
                    if (item.Area.X > CilentRect.Width - 100)
                    {
                        moveDirectionEnemy = -1;
                        enemyMoveDown = true;
                    }
                }
                //Console.WriteLine("moveDirectionEnemy = " + moveDirectionEnemy);
            }
        }//적이 이동 할 방향을 정해주는 함수
        public void EnemyShoot()
        {
            enemyShootDelay += 1 + wave/2;
            if (enemyShootDelay > enemyShootDelayMax)
            {
                enemyShootDelay = 0;
                EnemyFire();
            }
        }//적이 총을 쏘는 함수
        public void EnemyFire()
        {
            if (enemyBullet.Count > 5 + wave)
            {
                return;
            }

            var result = from enemy in enemyList
                         group enemy by enemy.Area.X into col
                         select col;

                int randomColNum = random.Next(result.Count());
                    var randomCol = result.ElementAt(randomColNum);

                var randomRow = from enemy in randomCol
                                orderby enemy.Area.Y descending
                                select enemy;

                Enemy shootEnemy = randomRow.First();
                Point shootPoint = new Point(shootEnemy.Area.X + shootEnemy.Area.Width / 2, shootEnemy.Area.Y + shootEnemy.Area.Height);
                enemyBullet.Add(new Bullet(shootPoint, CilentRect, 20));
        }//누가 총을 쏠지 구하는 함수
        public void PlayerFire()
        {
            if (!player.Alive) return;
            Point point = new Point(player.Location.X + (player.image.Width / 2), player.Location.Y + (player.image.Height / 2));
            if(playerBullet.Count < 6 + (wave * 2))
            {
                playerBullet.Add(new Bullet(point, CilentRect, 30));
            }
        }
        public Enemy CheckForEnemyCollision(Point point)
        {
            Enemy tempbool = null;
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i].Area.Contains(point))
                {
                    tempbool = enemyList[i];
                }
            }
            return tempbool;
        }//총에 맞은 적이 누구인지 반환
        public void playerMove(Direction direction)
        {
            if (!player.Alive) return;
            player.Move(direction);
        }
        public void Twinkle(Graphics g)
        {
            stars.Draw(g);
        }//반짝반짝
    }
}
