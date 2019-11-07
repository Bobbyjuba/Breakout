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
        // Physics variables
        public int xSpeed = 3;
        public int ySpeed = 3;
        public int paddleSpeed = 20;

        // Game Variable
        public int score = 0;

        // Block variables
        public const int row = 5;
        public const int col = 6;
        public int blockHeight = 25;
        public int blockWidth = 100;
        public PictureBox[,] blocks = new PictureBox[row, col]; 
        // blocks is an array of picture boxes that will serve as the visualizing of the bricks

        // Constructor to draw initial state and begin game
        public BreakoutForm() {
            setBlocks();
            InitializeComponent();
        }

        // Function that will draw the bricks at the top of the screen at game start
        private void setBlocks() {
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

        // Timer used to update game state every 10ms
        private void GameTimer_Tick(object sender, EventArgs e) {
            if (picBall.Bounds.IntersectsWith(floor.Bounds)) {
                ySpeed = 0;
                xSpeed = 0;

                gameTimer.Stop();
                MessageBox.Show("Game Over!");
            }

            picBall.Top += ySpeed;
            picBall.Left += xSpeed;

            // Statements that keep ball within screen window
            if (picBall.Bottom > this.ClientSize.Height || picBall.Top < 0)
                ySpeed = -ySpeed;

            if (picBall.Right > this.ClientSize.Width || picBall.Left < 0)
                xSpeed = -xSpeed;

            if (picBall.Bounds.IntersectsWith(picPaddle.Bounds)) {
                ySpeed = -ySpeed;
            }

            // If ball's bounds hit a brick, remove that brick
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

        // Mouse-based paddle movement
        private void BreakoutForm_MouseMove(object sender, MouseEventArgs e) {
            picPaddle.Left = e.X - (picPaddle.Width / 2);
        }

        // Keyboard-based paddle movement
        private void BreakoutForm_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.A)
                picPaddle.Left = picPaddle.Left - (picPaddle.Width / 2);

            if (e.KeyCode == Keys.D)
                picPaddle.Left = picPaddle.Left + (picPaddle.Width / 2);
        }
    }
}