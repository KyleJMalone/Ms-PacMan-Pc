using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

//Rules of the Game –
//Player can move up down left and right//
//Player will collect coins – collect all coin and you WIN the game//
//Player cannot touch Wall or Ghost. If they do GAME OVER//
//2 (Red and Yellow)ghost will have static left to right movement//
//1 (PINK) ghost will have a random movement which will scale across the form.//
//When the game is over there will be a game over text or when the player won the game it will show You WIN.//

namespace Ms_PacMan_Pc
{
    public partial class Form1 : Form
    {
        // start the variables
        bool goup, godown, goleft, goright, isGameOver;

        int score, playerSpeed, redGhostSpeed, yellowGhostSpeed, pinkGhostX, pinkGhostY;
        public Form1()
        {
            InitializeComponent();

            resetGame();
        }

        //In this function we are tracking 4 keys. Up, down, let and right. When either of these keys are pressed by the user we change the directional images of pacman dynamically so resemble the movement.//

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goup = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }
        }

        //In this key up function we are checking for the left, right, up and down keys again.//
        //Once the user has pressed them and left them we can turn those Booleans to false.//
        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goup = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                resetGame();
            }
        }

        private void mainGameTimer(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score; // show the score on the board


            //player movement codes start//

            //IF key code is LEFT//
            //Go left is set to true//
            //Change pac man image to LEFT//

            if (goleft == true)
            {
                mspacman.Left -= playerSpeed;
                mspacman.Image = Properties.Resources.left;
                //moving player to the left. 
            }

            //IF key code is RIGHT//
            //Go right is set to true//
            //Change pac man image to RIGHT//

            if (goright == true)
            {
                mspacman.Left += playerSpeed;
                mspacman.Image = Properties.Resources.right;
                //moving player to the right
            }

            //IF key code is DOWN//
            //Go down is set to true//
            //Change pac man image to down//
            if (godown == true)
            {
                mspacman.Top += playerSpeed;
                mspacman.Image = Properties.Resources.down;
                //moving down
            }

            //IF key code is UP//
            //Go up is set to true//
            //Change pac man image to UP//
            if (goup == true)
            {
                mspacman.Top -= playerSpeed;
                mspacman.Image = Properties.Resources.Up;
                //moving to the top
            }

            //teleporting to each side ability//
            if (mspacman.Left < -10)
            {
                mspacman.Left = 680;
            }

            if (mspacman.Left > 680)
            {
                mspacman.Left = -10;
            }

            if (mspacman.Top < -10)
            {
                mspacman.Top = 550;
            }

            if (mspacman.Top > 550)
            {
                mspacman.Top = 0;
            }
            //teleporting ability ends//
            //player movements code end//


            //coin section//
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    //checking if the player hits the coin picturebox then we can add to the score//
                    if ((string)x.Tag == "coin" && x.Visible == true)
                    {

                        if (mspacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            score += 1;
                            x.Visible = false;
                        }
                    }

                    //wall check section//
                    if((string)x.Tag == "wall") 
                    {
                        if(mspacman.Bounds.IntersectsWith (x.Bounds))
                        {
                            gameOver("You Lose :(");
                        }

                        if (pinkGhost.Bounds.IntersectsWith(x.Bounds))
                        {
                            pinkGhostX = -pinkGhostX;
                        }
                    }


                    //ghost check//
                    if((string)x.Tag == "ghost")
                    {
                        if (mspacman.Bounds.IntersectsWith(x.Bounds)) 
                        {
                            gameOver("You Lose :(");
                        }
                    }
                }
            }

            //moving ghosts//

            redGhost.Left += redGhostSpeed;

            if (redGhost.Bounds.IntersectsWith(pictureBox1.Bounds) || redGhost.Bounds.IntersectsWith(pictureBox2.Bounds))
            {
                redGhostSpeed = -redGhostSpeed;
            }

            yellowGhost.Left -= yellowGhostSpeed;

            if (yellowGhost.Bounds.IntersectsWith(pictureBox3.Bounds) || yellowGhost.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                yellowGhostSpeed = -yellowGhostSpeed;
            }

            pinkGhost.Left -= pinkGhostX;
            pinkGhost.Top -= pinkGhostY;

            if (pinkGhost.Top < 0 || pinkGhost.Top > 520)
            {
                pinkGhostY = -pinkGhostY ;
            }

            if (pinkGhost.Left < 0 || pinkGhost.Left >620)
            {
                pinkGhostX= -pinkGhostX ;
            }

            //Score//
            if (score == 51)
            {
                gameOver("You Win!");
            }
        }


     
       
        
        
        private void resetGame()
        {
            txtScore.Text = "Score: 0";
            score= 0;

            redGhostSpeed = 5;
            yellowGhostSpeed = 5;
            pinkGhostX= 5;
            pinkGhostY= 5;
            playerSpeed= 8;

            isGameOver= false;

            mspacman.Left = 33;
            mspacman.Top = 67;

            redGhost.Left = 196;
            redGhost.Top = 67;

            yellowGhost.Left = 455;
            yellowGhost.Top = 397;

            pinkGhost.Left = 548;
            pinkGhost.Top = 236;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox) 
                {
                  x.Visible = true;
                }
            }

            gameTimer.Start();
            
        }
        
        private void gameOver(string message) 
        {
            isGameOver = true;

            gameTimer.Stop();

            txtScore.Text = "Score: " + score + Environment.NewLine + message;
        }
    }
}
