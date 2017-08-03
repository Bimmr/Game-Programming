namespace TicTacToe
{
    partial class TicTacToe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.topLeft = new System.Windows.Forms.PictureBox();
            this.topMiddle = new System.Windows.Forms.PictureBox();
            this.topRight = new System.Windows.Forms.PictureBox();
            this.middleRight = new System.Windows.Forms.PictureBox();
            this.middleMiddle = new System.Windows.Forms.PictureBox();
            this.middleLeft = new System.Windows.Forms.PictureBox();
            this.bottomRight = new System.Windows.Forms.PictureBox();
            this.bottomMiddle = new System.Windows.Forms.PictureBox();
            this.bottomLeft = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topMiddle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.middleRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.middleMiddle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.middleLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomMiddle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox9
            // 
            this.pictureBox9.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox9.Image = global::TicTacToe.Properties.Resources.Grid;
            this.pictureBox9.Location = new System.Drawing.Point(-2, 0);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(462, 438);
            this.pictureBox9.TabIndex = 9;
            this.pictureBox9.TabStop = false;
            // 
            // topLeft
            // 
            this.topLeft.Cursor = System.Windows.Forms.Cursors.Default;
            this.topLeft.Location = new System.Drawing.Point(12, 12);
            this.topLeft.Name = "top_left";
            this.topLeft.Size = new System.Drawing.Size(133, 130);
            this.topLeft.TabIndex = 10;
            this.topLeft.TabStop = false;
            this.topLeft.Click += new System.EventHandler(this.click);
            // 
            // topMiddle
            // 
            this.topMiddle.Cursor = System.Windows.Forms.Cursors.Default;
            this.topMiddle.Location = new System.Drawing.Point(173, 12);
            this.topMiddle.Name = "top_middle";
            this.topMiddle.Size = new System.Drawing.Size(133, 130);
            this.topMiddle.TabIndex = 11;
            this.topMiddle.TabStop = false;
            this.topMiddle.Click += new System.EventHandler(this.click);
            // 
            // topRight
            // 
            this.topRight.Cursor = System.Windows.Forms.Cursors.Default;
            this.topRight.Location = new System.Drawing.Point(327, 12);
            this.topRight.Name = "top_right";
            this.topRight.Size = new System.Drawing.Size(133, 130);
            this.topRight.TabIndex = 12;
            this.topRight.TabStop = false;
            this.topRight.Click += new System.EventHandler(this.click);
            // 
            // middleRight
            // 
            this.middleRight.Cursor = System.Windows.Forms.Cursors.Default;
            this.middleRight.Location = new System.Drawing.Point(327, 174);
            this.middleRight.Name = "middle_right";
            this.middleRight.Size = new System.Drawing.Size(133, 130);
            this.middleRight.TabIndex = 15;
            this.middleRight.TabStop = false;
            this.middleRight.Click += new System.EventHandler(this.click);
            // 
            // middleMiddle
            // 
            this.middleMiddle.Cursor = System.Windows.Forms.Cursors.Default;
            this.middleMiddle.Location = new System.Drawing.Point(173, 174);
            this.middleMiddle.Name = "middle_middle";
            this.middleMiddle.Size = new System.Drawing.Size(133, 130);
            this.middleMiddle.TabIndex = 14;
            this.middleMiddle.TabStop = false;
            this.middleMiddle.Click += new System.EventHandler(this.click);
            // 
            // middleLeft
            // 
            this.middleLeft.Cursor = System.Windows.Forms.Cursors.Default;
            this.middleLeft.Location = new System.Drawing.Point(12, 174);
            this.middleLeft.Name = "middle_left";
            this.middleLeft.Size = new System.Drawing.Size(133, 130);
            this.middleLeft.TabIndex = 13;
            this.middleLeft.TabStop = false;
            this.middleLeft.Click += new System.EventHandler(this.click);
            // 
            // bottomRight
            // 
            this.bottomRight.Cursor = System.Windows.Forms.Cursors.Default;
            this.bottomRight.Location = new System.Drawing.Point(327, 329);
            this.bottomRight.Name = "bottom_right";
            this.bottomRight.Size = new System.Drawing.Size(133, 130);
            this.bottomRight.TabIndex = 18;
            this.bottomRight.TabStop = false;
            this.bottomRight.Click += new System.EventHandler(this.click);
            // 
            // bottomMiddle
            // 
            this.bottomMiddle.Cursor = System.Windows.Forms.Cursors.Default;
            this.bottomMiddle.Location = new System.Drawing.Point(173, 329);
            this.bottomMiddle.Name = "bottom_middle";
            this.bottomMiddle.Size = new System.Drawing.Size(133, 130);
            this.bottomMiddle.TabIndex = 17;
            this.bottomMiddle.TabStop = false;
            this.bottomMiddle.Click += new System.EventHandler(this.click);
            // 
            // bottomLeft
            // 
            this.bottomLeft.Cursor = System.Windows.Forms.Cursors.Default;
            this.bottomLeft.Location = new System.Drawing.Point(12, 329);
            this.bottomLeft.Name = "bottom_left";
            this.bottomLeft.Size = new System.Drawing.Size(133, 130);
            this.bottomLeft.TabIndex = 16;
            this.bottomLeft.TabStop = false;
            this.bottomLeft.Click += new System.EventHandler(this.click);
            // 
            // TicTacToe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 470);
            this.Controls.Add(this.bottomRight);
            this.Controls.Add(this.bottomMiddle);
            this.Controls.Add(this.bottomLeft);
            this.Controls.Add(this.middleRight);
            this.Controls.Add(this.middleMiddle);
            this.Controls.Add(this.middleLeft);
            this.Controls.Add(this.topRight);
            this.Controls.Add(this.topMiddle);
            this.Controls.Add(this.topLeft);
            this.Controls.Add(this.pictureBox9);
            this.Name = "TicTacToe";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topMiddle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.middleRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.middleMiddle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.middleLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomMiddle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomLeft)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.PictureBox topLeft;
        private System.Windows.Forms.PictureBox topMiddle;
        private System.Windows.Forms.PictureBox topRight;
        private System.Windows.Forms.PictureBox middleRight;
        private System.Windows.Forms.PictureBox middleMiddle;
        private System.Windows.Forms.PictureBox middleLeft;
        private System.Windows.Forms.PictureBox bottomRight;
        private System.Windows.Forms.PictureBox bottomMiddle;
        private System.Windows.Forms.PictureBox bottomLeft;
    }
}

