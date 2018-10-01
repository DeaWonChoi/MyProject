using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_Invader
{
    public partial class Invader_최대원 : Form
    {
        List<Keys> KeyPress;
        static bool GameOver = false;
        Rectangle Bounderies;
        Game game;
        Random random;

        //draw test
        //int a = 10;

        public Invader_최대원()
        {
            InitializeComponent();
            Bounderies = ClientRectangle;
            random = new Random();
            game = new Game(Bounderies, random);
            KeyPress = new List<Keys>();
        }

        private void PaintEvent(object sender, PaintEventArgs e)
        {
            game.g = e.Graphics;
            game.Update(random);
            DoubleBuffered = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (Keys key in KeyPress)
            {
                if(key == Keys.Left)
                {
                    game.playerMove(Direction.Left);
                }
                if (key == Keys.Right)
                {
                    game.playerMove(Direction.Right);
                }
            }
            Invalidate();
        }

        private void KeyPressDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }

            if (GameOver)
            {
                if(e.KeyCode == Keys.Enter)
                {
                    game = null;
                    game = new Game(Bounderies, random);
                }
            }

            if(e.KeyCode == Keys.Z)
            {
                game.PlayerFire();
            }

            if(e.KeyCode == Keys.F1)
            {
                game.DrawLine = !game.DrawLine;
            }

            if (e.KeyCode == Keys.F2)
            {
                game.NextWave();
            }

            if (KeyPress.Contains(e.KeyCode))
            {
                KeyPress.Remove(e.KeyCode);
            }
            KeyPress.Add(e.KeyCode);
            //Console.WriteLine(e.KeyCode);
        }

        private void KepPressUp(object sender, KeyEventArgs e)
        {
            if (KeyPress.Contains(e.KeyCode))
            {
                KeyPress.Remove(e.KeyCode);
            }
        }

        public static void SetGameOver()
        {
            GameOver = true;
        }
    }
}

