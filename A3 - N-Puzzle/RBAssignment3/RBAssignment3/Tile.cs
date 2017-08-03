/*
	Tile.cs
	Assignment 3
	Revision History:
		Randy Bimm - 10/14/16 - Created
*/
using System.Windows.Forms;

namespace RBAssignment3
{
    /// <summary>
    /// Tile class, extends Button
    /// </summary>
    class Tile : Button
    {
        public static int SIZE = 100;

        /// <summary>
        /// Create new instance of tile
        /// </summary>
        /// <param name="name">Name of the tile</param>
        /// <param name="x">X Pos</param>
        /// <param name="y">Y Pos</param>
        public Tile(string name, int x, int y)
        {
            this.Location = new System.Drawing.Point(x, y);
            this.Name = name;
            this.Size = new System.Drawing.Size(SIZE, SIZE);
            this.Text = name;
            this.UseVisualStyleBackColor = true;
            this.Click += new System.EventHandler(NPuzzle.form.click);
            NPuzzle.form.Controls.Add(this);
        }

        /// <summary>
        /// Remove tile from form
        /// </summary>
        public void remove()
        {
            NPuzzle.form.Controls.Remove(this);
        }
    }
}
