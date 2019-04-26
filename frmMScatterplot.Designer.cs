namespace ESFTool
{
    partial class frmMScatterplot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMScatterplot));
            this.btnOpenSWM = new System.Windows.Forms.Button();
            this.txtSWM = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkStd = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.cboFldNm1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTargetLayer = new System.Windows.Forms.ComboBox();
            this.ofdOpenSWM = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnOpenSWM
            // 
            this.btnOpenSWM.Location = new System.Drawing.Point(192, 141);
            this.btnOpenSWM.Name = "btnOpenSWM";
            this.btnOpenSWM.Size = new System.Drawing.Size(29, 23);
            this.btnOpenSWM.TabIndex = 84;
            this.btnOpenSWM.Text = "...";
            this.btnOpenSWM.UseVisualStyleBackColor = true;
            this.btnOpenSWM.Click += new System.EventHandler(this.btnOpenSWM_Click);
            // 
            // txtSWM
            // 
            this.txtSWM.Location = new System.Drawing.Point(13, 143);
            this.txtSWM.Name = "txtSWM";
            this.txtSWM.Size = new System.Drawing.Size(166, 20);
            this.txtSWM.TabIndex = 83;
            this.txtSWM.Text = "Default";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 13);
            this.label5.TabIndex = 82;
            this.label5.Text = "Spatial Weights Matrix";
            // 
            // chkStd
            // 
            this.chkStd.AutoSize = true;
            this.chkStd.Location = new System.Drawing.Point(17, 97);
            this.chkStd.Name = "chkStd";
            this.chkStd.Size = new System.Drawing.Size(151, 17);
            this.chkStd.TabIndex = 81;
            this.chkStd.Text = "Use Standardized Variable";
            this.chkStd.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(150, 175);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 80;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(14, 175);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 79;
            this.btnRun.Text = "OK";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // cboFldNm1
            // 
            this.cboFldNm1.FormattingEnabled = true;
            this.cboFldNm1.Location = new System.Drawing.Point(17, 69);
            this.cboFldNm1.Name = "cboFldNm1";
            this.cboFldNm1.Size = new System.Drawing.Size(208, 21);
            this.cboFldNm1.TabIndex = 78;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 77;
            this.label2.Text = "Field:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 76;
            this.label1.Text = "Select a Target Layer:";
            // 
            // cboTargetLayer
            // 
            this.cboTargetLayer.FormattingEnabled = true;
            this.cboTargetLayer.Location = new System.Drawing.Point(16, 28);
            this.cboTargetLayer.Name = "cboTargetLayer";
            this.cboTargetLayer.Size = new System.Drawing.Size(210, 21);
            this.cboTargetLayer.TabIndex = 75;
            this.cboTargetLayer.SelectedIndexChanged += new System.EventHandler(this.cboTargetLayer_SelectedIndexChanged);
            // 
            // ofdOpenSWM
            // 
            this.ofdOpenSWM.Filter = "GAL files|*.gal|GWT files|*.gwt";
            this.ofdOpenSWM.Title = "Open GAL files";
            // 
            // frmMScatterplot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(238, 211);
            this.Controls.Add(this.btnOpenSWM);
            this.Controls.Add(this.txtSWM);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkStd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.cboFldNm1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboTargetLayer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMScatterplot";
            this.Text = "Moran Scatter Plot";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenSWM;
        private System.Windows.Forms.TextBox txtSWM;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkStd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ComboBox cboFldNm1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTargetLayer;
        private System.Windows.Forms.OpenFileDialog ofdOpenSWM;
    }
}