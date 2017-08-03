/*
	NPuzzle.cs
	Assignment 3
	Revision History:
		Randy Bimm - 10/14/16 - Created
        Randy Bimm - 10/26/16 - Moved FileHelper to it's own file
*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RBAssignment3
{
    /// <summary>
    /// NPuzzles Class, Extends Form
    /// </summary>
    public partial class NPuzzle : Form
    {
        public static NPuzzle form;

        private Tile openTile;
        private List<Tile> tiles;
        private List<int> numbers;

        private int row, col;

        /// <summary>
        /// Create a new instance of NPuzzle
        /// </summary>
        public NPuzzle()
        {
            InitializeComponent();

            form = this;
            tiles = new List<Tile>();
            numbers = new List<int>();

        }

        /// <summary>
        /// Start a fresh game
        /// </summary>
        public void start()
        {
            tsSave.Enabled = true;

            generateTiles(row, col);
            do randomizeNumbers(row * col);
            while (!isSolvable());
            updateTiles();
        }

        /// <summary>
        /// Generate all the needed tiles
        /// </summary>
        /// <param name="col">Columns</param>
        /// <param name="row">Rows</param>
        /// <returns></returns>
        private List<Tile> generateTiles(int col, int row)
        {
            foreach (Tile tile in tiles)
                tile.remove();

            tiles.Clear();

            for (int r = 0; r < row; r++)
                for (int c = 0; c < col; c++)
                {
                    Tile tile = new Tile(r * row + c + "", Tile.SIZE * c + 5, Tile.SIZE * r + 25);
                    tiles.Add(tile);
                }
            return tiles;
        }
        /// <summary>
        /// Match the tiles with the numbers in the list
        /// </summary>
        private void updateTiles()
        {
            for (int i = 0; i < numbers.Count; i++)
                if (numbers[i] != numbers.Count)
                    tiles[i].Text = "" + numbers[i];
                else
                {
                    tiles[i].Text = "";
                    openTile = tiles[i];
                }
        }

        /// <summary>
        /// Create a list of random numbers
        /// </summary>
        /// <param name="max">Max number of number</param>
        private void randomizeNumbers(int max)
        {
            numbers.Clear();

            for (int i = 0; i < max; i++)
            {
                int number;
                Random random = new Random();

                do number = random.Next(max) + 1;
                while (numbers.Contains(number));

                numbers.Add(number);
            }
        }

        /// <summary>
        /// Check if the given result is solvable, not 100% reliable
        /// </summary>
        /// <returns>If the puzzle is solvable</returns>
        public bool isSolvable()
        {
            int inversion = 0;
            int row = 0;
            int blankRow = 0;

            for (int i = 0; i < numbers.Count; i++)
            {
                if (i % col == 0)
                    row++;

                if (numbers[i] == 0)
                {
                    blankRow = row;
                    continue;
                }
                //Count inversions
                for (int j = i + 1; j < numbers.Count; j++)
                    if (numbers[i] > numbers[j] && numbers[j] != 0)
                        inversion++;
            }

            //N Puzzles have conditions that apply if the size is even
            if (col % 2 == 0)
            {
                //You'll need to know where the blank tile is
                if (blankRow % 2 == 0)
                    return inversion % 2 == 0;
                else
                    return inversion % 2 == 1;
            }
            else
                return inversion % 2 == 0;
        }

        /// <summary>
        /// Check if the current game is solved
        /// </summary>
        private void checkIfSolved()
        {
            for (int i = 0; i < numbers.Count - 1; i++)
                if (numbers[i] > numbers[i + 1])
                    return;

            MessageBox.Show("You win!");

            for (int i = 0; i < numbers.Count; i++)
                numbers[i] = numbers.Count;

            updateTiles();
        }

        /// <summary>
        /// Save the file
        /// </summary>
        /// <param name="sender">Item that sends the event</param>
        /// <param name="e">Arguments that were sent with the event</param>
        private void tsSave_Click(object sender, EventArgs e)
        {
            string nums = String.Join(",", numbers.ToArray());

            sfdSave.ShowDialog();
            string file = sfdSave.FileName;
            if (file != "")
            {
                FileHelper fh = new FileHelper(file);
                fh.add("numbers", nums);
                fh.add("row", row + "");
                fh.add("col", col + "");
            }
        }

        /// <summary>
        /// Load a saved file
        /// </summary>
        /// <param name="sender">Item that sends the event</param>
        /// <param name="e">Arguments that were sent with the event</param>
        private void ltLoad_Click(object sender, EventArgs e)
        {
            ofdLoad.ShowDialog();
            string file = ofdLoad.FileName;

            if (file != "")
            {
                tsSave.Enabled = true;

                FileHelper fh = new FileHelper(file);
                int[] nums = Array.ConvertAll(fh.get("numbers").Split(','), int.Parse);
                row = int.Parse(fh.get("row"));
                col = int.Parse(fh.get("col"));
                generateTiles(row, col);
                numbers.Clear();
                numbers.AddRange(nums);
                updateTiles();
            }
        }

        /// <summary>
        /// Create a new grid of tiles
        /// </summary>
        /// <param name="sender">Item that sends the event</param>
        /// <param name="e">Arguments that were sent with the event</param>
        private void tsCreate_Click(object sender, EventArgs e)
        {
            string r = "", c = "";
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "What size do you want to create?\n   Use the format: rowXcolumn\n\nEx. 3x3", "Create",
                "3x3", -1, -1);
            if (input.ToLower().Contains("x"))
            {
                r = input.ToLower().Split('x')[0];
                c = input.ToLower().Split('x')[1];
            }
            if (input != "")
                if (int.TryParse(r, out row) && int.TryParse(c, out col))
                    start();
                else
                {
                    MessageBox.Show("Invalid format, please enter a valid format for the size.");
                    tsCreate_Click(sender, e);
                }
        }

        /// <summary>
        /// When a tile gets clicked update the numbers
        /// </summary>
        /// <param name="sender">Item that sends the event</param>
        /// <param name="e">Arguments that were sent with the event</param>
        public void click(object sender, EventArgs e)
        {
            int indexOfClicked = int.Parse(((Tile)sender).Name);
            int indexOfOpen = int.Parse(openTile.Name);

            //Make sure the tile is adjacent
            if ((Math.Abs(indexOfClicked - indexOfOpen) == 1 
                    && indexOfClicked / col == indexOfOpen / col)
                || Math.Abs(indexOfClicked - indexOfOpen) == col)
            {

                int valueOfClicked = numbers[indexOfClicked];

                //Switch
                numbers[indexOfClicked] = numbers.Count;
                numbers[indexOfOpen] = valueOfClicked;

                updateTiles();
                checkIfSolved();
            }
        }
    }
}
