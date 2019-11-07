using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breakout {
    public partial class BreakoutForm : Form {
        // Collision variables
        public int xSpeed = 3;
        public int ySpeed = 3;
        public int paddleSpeed = 20;
        public int score = 0;

        // Block variables
        public const int row = 5;
        public const int col = 6;
        public PictureBox [,] blocks;

        public BreakoutForm() {
            setBlocks();
            InitializeComponent();
        }

        private void setBlocks() {
            int blockHeight = 25;
            int blockWidth = 100;

            blocks = new PictureBox[row, col];

            for (int x = 1; x < row; x++) {
                for (int y = 0; y < col; y++) {
                    blocks[x, y] = new PictureBox();
                    blocks[x, y].BackColor = Color.LightGreen;
                    blocks[x, y].Height = blockHeight;
                    blocks[x, y].Width = blockWidth;
                    blocks[x, y].Top = blockHeight * x;
                    blocks[x, y].Left = blockWidth * y;
                    blocks[x, y].BorderStyle = BorderStyle.FixedSingle;

                    this.Controls.Add( blocks[x, y] );
                }
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e) {
            picBall.Top += ySpeed;
            picBall.Left += xSpeed;

            if (picBall.Bottom > this.ClientSize.Height || picBall.Top < 0)
                ySpeed = -ySpeed;

            if (picBall.Right > this.ClientSize.Width || picBall.Left < 0)
                xSpeed = -xSpeed;

            if (picBall.Bounds.IntersectsWith(picPaddle.Bounds))
                ySpeed = -ySpeed;

            for (int x = 1; x < row; x++) {
                for (int y = 0; y < col; y++) {
                    if (picBall.Bounds.IntersectsWith(blocks[x,y].Bounds) && blocks[x, y].Visible == true) {
                        ySpeed = -ySpeed;
                        blocks[x, y].Visible = false;
                        score++;
                        labelScoreVal.Text = Convert.ToString(score);
                    }
                }
            }
        }
        private void BreakoutForm_MouseMove(object sender, MouseEventArgs e) {
            picPaddle.Left = e.X - (picPaddle.Width / 2);
        }

        private void BreakoutForm_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.A)
                picPaddle.Left -= paddleSpeed;

            if (e.KeyCode == Keys.D)
                picPaddle.Left += paddleSpeed;
        }
    }
}