namespace ESFTool
{
    partial class frmPlot
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
            this.picPlot = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPreviousPlot = new System.Windows.Forms.Button();
            this.btnNextPlot = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picPlot)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picPlot
            // 
            this.picPlot.BackColor = System.Drawing.SystemColors.Window;
            this.picPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPlot.Location = new System.Drawing.Point(0, 0);
            this.picPlot.Name = "picPlot";
            this.picPlot.Size = new System.Drawing.Size(300, 260);
            this.picPlot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPlot.TabIndex = 2;
            this.picPlot.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnPreviousPlot);
            this.panel1.Controls.Add(this.btnNextPlot);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 260);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 39);
            this.panel1.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(213, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPreviousPlot
            // 
            this.btnPreviousPlot.Location = new System.Drawing.Point(3, 8);
            this.btnPreviousPlot.Name = "btnPreviousPlot";
            this.btnPreviousPlot.Size = new System.Drawing.Size(85, 25);
            this.btnPreviousPlot.TabIndex = 1;
            this.btnPreviousPlot.Text = "Previous Plot";
            this.btnPreviousPlot.UseVisualStyleBackColor = true;
            this.btnPreviousPlot.Click += new System.EventHandler(this.btnPreviousPlot_Click);
            // 
            // btnNextPlot
            // 
            this.btnNextPlot.Location = new System.Drawing.Point(93, 8);
            this.btnNextPlot.Name = "btnNextPlot";
            this.btnNextPlot.Size = new System.Drawing.Size(85, 25);
            this.btnNextPlot.TabIndex = 0;
            this.btnNextPlot.Text = "Next Plot";
            this.btnNextPlot.UseVisualStyleBackColor = true;
            this.btnNextPlot.Click += new System.EventHandler(this.btnNextPlot_Click);
            // 
            // frmPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(300, 299);
            this.Controls.Add(this.picPlot);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(632, 674);
            this.MinimumSize = new System.Drawing.Size(158, 168);
            this.Name = "frmPlot";
            this.Text = "frmPlot";
            ((System.ComponentModel.ISupportInitialize)(this.picPlot)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox picPlot;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnPreviousPlot;
        public System.Windows.Forms.Button btnNextPlot;
    }
}