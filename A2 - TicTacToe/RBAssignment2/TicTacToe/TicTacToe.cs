/*
	TicTacToe.cs
	Assignment 2
	Revision History:
		Randy Bimm - 9/28/16 - Created
		Randy Bimm - 10/6/16 - Revisions
*/

using System;
using System.Windows.Forms;

namespace TicTacToe
{
    /// <summary>
    /// Class to make TicTacToe on a Windows Form Application
    /// </summary>
    public partial class TicTacToe : Form
    {
        private const int SIZE = 3, PLAYERS = 2;



        private string[,] _grid;
        private int[] _wins = new int[PLAYERS];
        private int _player;
        private int _moves;

        /// <summary>
        /// Create a new instance of TicTacToe
        /// </summary>
        public TicTacToe()
        {
            InitializeComponent();
            start();
        }
        /// <summary>
        /// Initialize everything so there is an easy reset
        /// </summary>
        public void start()
        {
            //Rest Everything
            _player = 1;
            _moves = 0;
            _grid = new string[SIZE, SIZE];
            topLeft.Image = null;
            topMiddle.Image = null;
            topRight.Image = null;

            middleLeft.Image = null;
            middleMiddle.Image = null;
            middleRight.Image = null;

            bottomLeft.Image = null;
            bottomMiddle.Image = null;
            bottomRight.Image = null;

            //Update the title's text
            this.Text = "Tic Tac Toe --- [P1:"+_wins[0] + " - P2:"+_wins[1]+"]"
                +" --- Player " + _player + "'s turn";
        }

        /// <summary>
        /// When an image gets clicked perform the needed actions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void click(object sender, EventArgs e)
        {
            //Get the pBox's name and the row and col of that box
            PictureBox pBox = (PictureBox)sender;
            string cell = pBox.Name;
            int row = getCellIndex(cell.Split('_')[0]);
            int col = getCellIndex(cell.Split('_')[1]);

            //Check to make sure the spot isn't taken
            if (_grid[row, col] != null)
            {
                MessageBox.Show("That spot is already taken!");
                return;
            }

            //Replace the needed values in the array and on the form
            _grid[row, col] = _player == 1 ? "X" : "O";
            pBox.Image = _player == 1 ? Properties.Resources.X : Properties.Resources.O;


            //Check if a player has won
            if (!checkForWinner())
            {
                //If no one has one, switch the player
                _player = _player == 1 ? 2 : 1;
                this.Text = "Tic Tac Toe --- [P1:" + _wins[0] + " - P2:" + _wins[1] + "]"
                    + " --- Player " + _player + "'s turn";
                _moves++;
            }
            else
                //Restart the game if someone has won
                start();

        }
        /// <summary>
        /// Check if someone has won or if there is a tie
        /// </summary>
        /// <returns></returns>
        private bool checkForWinner()
        {
            bool winner = false;

            // Horizontal
            if (_grid[0, 0] != null && _grid[0, 0] == _grid[0, 1] && _grid[0, 0] == _grid[0, 2])
                winner = true;
            else if (_grid[1, 0] != null && _grid[1, 0] == _grid[1, 1] && _grid[1, 0] == _grid[1, 2])
                winner = true;
            else if (_grid[2, 0] != null && _grid[2, 0] == _grid[2, 1] && _grid[2, 0] == _grid[2, 2])
                winner = true;
            
            // Vertical
            if (_grid[0, 0] != null && _grid[0, 0] == _grid[1, 0] && _grid[0, 0] == _grid[2, 0])
                winner = true;
            if (_grid[0, 1] != null && _grid[0, 1] == _grid[1, 1] && _grid[0, 1] == _grid[2, 1])
                winner = true;
            if (_grid[0, 2] != null && _grid[0, 2] == _grid[1, 2] && _grid[0, 2] == _grid[2, 2])
                winner = true;

            //Diagnal
            if (_grid[0, 0] != null && _grid[0, 0] == _grid[1, 1] && _grid[0, 0] == _grid[2, 2])
                winner = true;
            if (_grid[0, 2] != null && _grid[0, 2] == _grid[1, 1] && _grid[0, 2] == _grid[2, 0])
                winner = true;

            //If a winner has been found notifty and keep track of the wins
            if (winner)
            {
                MessageBox.Show("Player " + _player + " has won!");
                _wins[_player-1] += 1;
            }

            //If 8 or more moves have been done then the game is a tie as there are no other places
            else if (_moves >= 8)
            {
                MessageBox.Show("Tie");
                winner = true;
            }
            return winner;
        }

        /// <summary>
        /// Get an index value based on the picturebox's name
        /// top|left = 0
        /// middle = 1
        /// bottom|right = 2
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private int getCellIndex(string cell)
        {
            if (cell == "top" || cell == "left")
                return 0;
            if (cell == "middle")
                return 1;
            if (cell == "bottom" || cell == "right")
                return 2;
            return -1;
        }
    }
}
