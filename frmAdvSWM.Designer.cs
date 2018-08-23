namespace ESFTool
{
    partial class frmAdvSWM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdvSWM));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.chkCumulate = new System.Windows.Forms.CheckBox();
            this.nudAdvanced = new System.Windows.Forms.NumericUpDown();
            this.lblAdvanced = new System.Windows.Forms.Label();
            this.txtSWM = new System.Windows.Forms.ComboBox();
            this.lblClip = new System.Windows.Forms.Label();
            this.lblGeoDa = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSubset = new System.Windows.Forms.Button();
            this.btnOpenSWM = new System.Windows.Forms.Button();
            this.ofdOpenSWM = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdvanced)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(132, 166);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 99;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(26, 166);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 98;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // chkCumulate
            // 
            this.chkCumulate.AutoSize = true;
            this.chkCumulate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCumulate.Location = new System.Drawing.Point(16, 73);
            this.chkCumulate.Name = "chkCumulate";
            this.chkCumulate.Size = new System.Drawing.Size(122, 17);
            this.chkCumulate.TabIndex = 97;
            this.chkCumulate.Text = "Cumulate neighbors:";
            this.chkCumulate.UseVisualStyleBackColor = true;
            // 
            // nudAdvanced
            // 
            this.nudAdvanced.Location = new System.Drawing.Point(122, 45);
            this.nudAdvanced.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudAdvanced.Name = "nudAdvanced";
            this.nudAdvanced.Size = new System.Drawing.Size(85, 20);
            this.nudAdvanced.TabIndex = 96;
            this.nudAdvanced.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudAdvanced.ValueChanged += new System.EventHandler(this.nudAdvanced_ValueChanged);
            // 
            // lblAdvanced
            // 
            this.lblAdvanced.AutoSize = true;
            this.lblAdvanced.Location = new System.Drawing.Point(16, 46);
            this.lblAdvanced.Name = "lblAdvanced";
            this.lblAdvanced.Size = new System.Drawing.Size(100, 13);
            this.lblAdvanced.TabIndex = 95;
            this.lblAdvanced.Text = "Threshold distance:";
            // 
            // txtSWM
            // 
            this.txtSWM.FormattingEnabled = true;
            this.txtSWM.Location = new System.Drawing.Point(76, 15);
            this.txtSWM.Name = "txtSWM";
            this.txtSWM.Size = new System.Drawing.Size(131, 21);
            this.txtSWM.TabIndex = 93;
            this.txtSWM.TextChanged += new System.EventHandler(this.txtSWM_TextChanged);
            // 
            // lblClip
            // 
            this.lblClip.AutoSize = true;
            this.lblClip.Location = new System.Drawing.Point(16, 100);
            this.lblClip.Name = "lblClip";
            this.lblClip.Size = new System.Drawing.Size(86, 13);
            this.lblClip.TabIndex = 94;
            this.lblClip.Text = "Clip by polygons:";
            // 
            // lblGeoDa
            // 
            this.lblGeoDa.AutoSize = true;
            this.lblGeoDa.Location = new System.Drawing.Point(16, 131);
            this.lblGeoDa.Name = "lblGeoDa";
            this.lblGeoDa.Size = new System.Drawing.Size(145, 13);
            this.lblGeoDa.TabIndex = 92;
            this.lblGeoDa.Text = "Load SWM from GeoDa files:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 89;
            this.label5.Text = "Definition:";
            // 
            // btnSubset
            // 
            this.btnSubset.Enabled = false;
            this.btnSubset.Location = new System.Drawing.Point(155, 95);
            this.btnSubset.Name = "btnSubset";
            this.btnSubset.Size = new System.Drawing.Size(52, 23);
            this.btnSubset.TabIndex = 91;
            this.btnSubset.Text = "Clip";
            this.btnSubset.UseVisualStyleBackColor = true;
            this.btnSubset.Click += new System.EventHandler(this.btnSubset_Click);
            // 
            // btnOpenSWM
            // 
            this.btnOpenSWM.Location = new System.Drawing.Point(176, 126);
            this.btnOpenSWM.Name = "btnOpenSWM";
            this.btnOpenSWM.Size = new System.Drawing.Size(31, 23);
            this.btnOpenSWM.TabIndex = 90;
            this.btnOpenSWM.Text = "...";
            this.btnOpenSWM.UseVisualStyleBackColor = true;
            this.btnOpenSWM.Click += new System.EventHandler(this.btnOpenSWM_Click);
            // 
            // ofdOpenSWM
            // 
            this.ofdOpenSWM.Filter = "GAL files|*.gal|GWT files|*.gwt";
            this.ofdOpenSWM.Title = "Open GAL files";
            // 
            // frmAdvSWM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(223, 205);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.chkCumulate);
            this.Controls.Add(this.nudAdvanced);
            this.Controls.Add(this.lblAdvanced);
            this.Controls.Add(this.txtSWM);
            this.Controls.Add(this.lblClip);
            this.Controls.Add(this.lblGeoDa);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSubset);
            this.Controls.Add(this.btnOpenSWM);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdvSWM";
            this.Text = "Spatial Weights";
            this.Load += new System.EventHandler(this.frmAdvSWM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudAdvanced)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.CheckBox chkCumulate;
        private System.Windows.Forms.NumericUpDown nudAdvanced;
        private System.Windows.Forms.Label lblAdvanced;
        public System.Windows.Forms.ComboBox txtSWM;
        private System.Windows.Forms.Label lblClip;
        private System.Windows.Forms.Label lblGeoDa;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSubset;
        private System.Windows.Forms.Button btnOpenSWM;
        private System.Windows.Forms.OpenFileDialog ofdOpenSWM;
    }
}