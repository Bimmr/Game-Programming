namespace RBAssignment3
{
    partial class NPuzzle
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSave = new System.Windows.Forms.ToolStripMenuItem();
            this.ltLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdLoad = new System.Windows.Forms.OpenFileDialog();
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsCreate,
            this.tsSave,
            this.ltLoad});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(259, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsCreate
            // 
            this.tsCreate.Name = "tsCreate";
            this.tsCreate.Size = new System.Drawing.Size(53, 20);
            this.tsCreate.Text = "Create";
            this.tsCreate.Click += new System.EventHandler(this.tsCreate_Click);
            // 
            // tsSave
            // 
            this.tsSave.Enabled = false;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(43, 20);
            this.tsSave.Text = "Save";
            this.tsSave.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // ltLoad
            // 
            this.ltLoad.Name = "ltLoad";
            this.ltLoad.Size = new System.Drawing.Size(45, 20);
            this.ltLoad.Text = "Load";
            this.ltLoad.Click += new System.EventHandler(this.ltLoad_Click);
            // 
            // ofdLoad
            // 
            this.ofdLoad.Filter = "Text Files|*.txt";
            // 
            // sfdSave
            // 
            this.sfdSave.FileName = "NPuzzle.txt";
            this.sfdSave.Filter = "Text Files|*.txt";
            // 
            // NPuzzle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(259, 30);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "NPuzzle";
            this.Text = "Puzzle";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsSave;
        private System.Windows.Forms.ToolStripMenuItem ltLoad;
        private System.Windows.Forms.ToolStripMenuItem tsCreate;
        private System.Windows.Forms.OpenFileDialog ofdLoad;
        private System.Windows.Forms.SaveFileDialog sfdSave;
    }
}

