namespace ESFTool
{
    partial class frmVaryingCoefficients
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVaryingCoefficients));
            this.ofdOpenSWM = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenSWM = new System.Windows.Forms.Button();
            this.txtSWM = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboNormalization = new System.Windows.Forms.ComboBox();
            this.lblNorm = new System.Windows.Forms.Label();
            this.cboFamily = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.grbSave = new System.Windows.Forms.GroupBox();
            this.lstSave = new System.Windows.Forms.ListView();
            this.colTypes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNames = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkCoeEVs = new System.Windows.Forms.CheckBox();
            this.grbEV = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblDirection = new System.Windows.Forms.Label();
            this.cboDirection = new System.Windows.Forms.ComboBox();
            this.nudEValue = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboFieldName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnMoveLeft = new System.Windows.Forms.Button();
            this.btnMoveRight = new System.Windows.Forms.Button();
            this.lstFields = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTargetLayer = new System.Windows.Forms.ComboBox();
            this.lstIndeVar = new System.Windows.Forms.CheckedListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.grbSave.SuspendLayout();
            this.grbEV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEValue)).BeginInit();
            this.SuspendLayout();
            // 
            // ofdOpenSWM
            // 
            this.ofdOpenSWM.Filter = "GAL files|*.gal|GWT files|*.gwt";
            this.ofdOpenSWM.Title = "Open GAL files";
            // 
            // btnOpenSWM
            // 
            this.btnOpenSWM.Location = new System.Drawing.Point(517, 41);
            this.btnOpenSWM.Name = "btnOpenSWM";
            this.btnOpenSWM.Size = new System.Drawing.Size(39, 23);
            this.btnOpenSWM.TabIndex = 107;
            this.btnOpenSWM.Text = "...";
            this.btnOpenSWM.UseVisualStyleBackColor = true;
            this.btnOpenSWM.Click += new System.EventHandler(this.btnOpenSWM_Click);
            // 
            // txtSWM
            // 
            this.txtSWM.Location = new System.Drawing.Point(308, 44);
            this.txtSWM.Name = "txtSWM";
            this.txtSWM.Size = new System.Drawing.Size(199, 20);
            this.txtSWM.TabIndex = 106;
            this.txtSWM.Text = "Default";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(305, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 13);
            this.label6.TabIndex = 105;
            this.label6.Text = "Spatial Weight Matrix:";
            // 
            // cboNormalization
            // 
            this.cboNormalization.Enabled = false;
            this.cboNormalization.FormattingEnabled = true;
            this.cboNormalization.Location = new System.Drawing.Point(15, 158);
            this.cboNormalization.Name = "cboNormalization";
            this.cboNormalization.Size = new System.Drawing.Size(265, 21);
            this.cboNormalization.TabIndex = 104;
            // 
            // lblNorm
            // 
            this.lblNorm.AutoSize = true;
            this.lblNorm.Enabled = false;
            this.lblNorm.Location = new System.Drawing.Point(14, 142);
            this.lblNorm.Name = "lblNorm";
            this.lblNorm.Size = new System.Drawing.Size(73, 13);
            this.lblNorm.TabIndex = 103;
            this.lblNorm.Text = "Normalization:";
            // 
            // cboFamily
            // 
            this.cboFamily.FormattingEnabled = true;
            this.cboFamily.Items.AddRange(new object[] {
            "Linear (Gaussian)",
            "Poisson",
            "Binomial"});
            this.cboFamily.Location = new System.Drawing.Point(15, 73);
            this.cboFamily.Name = "cboFamily";
            this.cboFamily.Size = new System.Drawing.Size(265, 21);
            this.cboFamily.TabIndex = 102;
            this.cboFamily.Text = "Linear (Gaussian)";
            this.cboFamily.SelectedIndexChanged += new System.EventHandler(this.cboFamily_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 101;
            this.label5.Text = "Family:";
            // 
            // grbSave
            // 
            this.grbSave.Controls.Add(this.lstSave);
            this.grbSave.Location = new System.Drawing.Point(300, 204);
            this.grbSave.Name = "grbSave";
            this.grbSave.Size = new System.Drawing.Size(264, 161);
            this.grbSave.TabIndex = 100;
            this.grbSave.TabStop = false;
            this.grbSave.Text = "Result";
            // 
            // lstSave
            // 
            this.lstSave.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTypes,
            this.colNames});
            this.lstSave.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstSave.LabelEdit = true;
            this.lstSave.Location = new System.Drawing.Point(12, 19);
            this.lstSave.Name = "lstSave";
            this.lstSave.Size = new System.Drawing.Size(244, 129);
            this.lstSave.TabIndex = 57;
            this.lstSave.UseCompatibleStateImageBehavior = false;
            this.lstSave.View = System.Windows.Forms.View.Details;
            this.lstSave.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstSave_MouseUp);
            // 
            // colTypes
            // 
            this.colTypes.Text = "Varying Coefficents";
            this.colTypes.Width = 114;
            // 
            // colNames
            // 
            this.colNames.Text = "Field Name";
            this.colNames.Width = 105;
            // 
            // chkCoeEVs
            // 
            this.chkCoeEVs.AutoSize = true;
            this.chkCoeEVs.Location = new System.Drawing.Point(308, 178);
            this.chkCoeEVs.Name = "chkCoeEVs";
            this.chkCoeEVs.Size = new System.Drawing.Size(229, 17);
            this.chkCoeEVs.TabIndex = 99;
            this.chkCoeEVs.Text = "Show coefficients of selected eigenvectors";
            this.chkCoeEVs.UseVisualStyleBackColor = true;
            // 
            // grbEV
            // 
            this.grbEV.Controls.Add(this.label7);
            this.grbEV.Controls.Add(this.lblDirection);
            this.grbEV.Controls.Add(this.cboDirection);
            this.grbEV.Controls.Add(this.nudEValue);
            this.grbEV.Location = new System.Drawing.Point(299, 85);
            this.grbEV.Name = "grbEV";
            this.grbEV.Size = new System.Drawing.Size(265, 79);
            this.grbEV.TabIndex = 98;
            this.grbEV.TabStop = false;
            this.grbEV.Text = "Candidate Eigenvector Selection";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(158, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Eigenvalues over the principal >";
            // 
            // lblDirection
            // 
            this.lblDirection.AutoSize = true;
            this.lblDirection.Enabled = false;
            this.lblDirection.Location = new System.Drawing.Point(74, 50);
            this.lblDirection.Name = "lblDirection";
            this.lblDirection.Size = new System.Drawing.Size(52, 13);
            this.lblDirection.TabIndex = 4;
            this.lblDirection.Text = "Direction:";
            // 
            // cboDirection
            // 
            this.cboDirection.Enabled = false;
            this.cboDirection.FormattingEnabled = true;
            this.cboDirection.Items.AddRange(new object[] {
            "Positive Only",
            "Negative Only",
            "Both"});
            this.cboDirection.Location = new System.Drawing.Point(132, 47);
            this.cboDirection.Name = "cboDirection";
            this.cboDirection.Size = new System.Drawing.Size(125, 21);
            this.cboDirection.TabIndex = 3;
            this.cboDirection.Text = "Positive Only";
            // 
            // nudEValue
            // 
            this.nudEValue.DecimalPlaces = 2;
            this.nudEValue.Enabled = false;
            this.nudEValue.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudEValue.Location = new System.Drawing.Point(178, 22);
            this.nudEValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudEValue.Name = "nudEValue";
            this.nudEValue.Size = new System.Drawing.Size(79, 20);
            this.nudEValue.TabIndex = 2;
            this.nudEValue.Value = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(448, 375);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(115, 23);
            this.btnCancel.TabIndex = 97;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(299, 372);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(115, 23);
            this.btnRun.TabIndex = 96;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(168, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 95;
            this.label4.Text = "Independent Variables";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 94;
            this.label3.Text = "Fields";
            // 
            // cboFieldName
            // 
            this.cboFieldName.FormattingEnabled = true;
            this.cboFieldName.Location = new System.Drawing.Point(15, 116);
            this.cboFieldName.Name = "cboFieldName";
            this.cboFieldName.Size = new System.Drawing.Size(265, 21);
            this.cboFieldName.TabIndex = 93;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 92;
            this.label2.Text = "Dependent Variable:";
            // 
            // btnMoveLeft
            // 
            this.btnMoveLeft.Location = new System.Drawing.Point(137, 269);
            this.btnMoveLeft.Name = "btnMoveLeft";
            this.btnMoveLeft.Size = new System.Drawing.Size(28, 23);
            this.btnMoveLeft.TabIndex = 91;
            this.btnMoveLeft.Text = "<";
            this.btnMoveLeft.UseVisualStyleBackColor = true;
            this.btnMoveLeft.Click += new System.EventHandler(this.btnMoveLeft_Click);
            // 
            // btnMoveRight
            // 
            this.btnMoveRight.Location = new System.Drawing.Point(137, 240);
            this.btnMoveRight.Name = "btnMoveRight";
            this.btnMoveRight.Size = new System.Drawing.Size(28, 23);
            this.btnMoveRight.TabIndex = 90;
            this.btnMoveRight.Text = ">";
            this.btnMoveRight.UseVisualStyleBackColor = true;
            this.btnMoveRight.Click += new System.EventHandler(this.btnMoveRight_Click);
            // 
            // lstFields
            // 
            this.lstFields.FormattingEnabled = true;
            this.lstFields.Location = new System.Drawing.Point(18, 208);
            this.lstFields.Name = "lstFields";
            this.lstFields.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstFields.Size = new System.Drawing.Size(113, 147);
            this.lstFields.TabIndex = 89;
            this.lstFields.DoubleClick += new System.EventHandler(this.lstFields_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 87;
            this.label1.Text = "Select a Target Layer:";
            // 
            // cboTargetLayer
            // 
            this.cboTargetLayer.FormattingEnabled = true;
            this.cboTargetLayer.Location = new System.Drawing.Point(15, 30);
            this.cboTargetLayer.Name = "cboTargetLayer";
            this.cboTargetLayer.Size = new System.Drawing.Size(266, 21);
            this.cboTargetLayer.TabIndex = 86;
            this.cboTargetLayer.SelectedIndexChanged += new System.EventHandler(this.cboTargetLayer_SelectedIndexChanged);
            // 
            // lstIndeVar
            // 
            this.lstIndeVar.FormattingEnabled = true;
            this.lstIndeVar.Location = new System.Drawing.Point(167, 206);
            this.lstIndeVar.Name = "lstIndeVar";
            this.lstIndeVar.Size = new System.Drawing.Size(113, 154);
            this.lstIndeVar.TabIndex = 108;
            this.lstIndeVar.SelectedIndexChanged += new System.EventHandler(this.lstIndeVar_SelectedIndexChanged);
            this.lstIndeVar.SelectedValueChanged += new System.EventHandler(this.lstIndeVar_SelectedValueChanged);
            this.lstIndeVar.DoubleClick += new System.EventHandler(this.lstIndeVar_DoubleClick);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(136, 366);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(144, 34);
            this.textBox1.TabIndex = 110;
            this.textBox1.Text = "Checked variables deonote Spatially Varying Coefficeints";
            // 
            // frmVaryingCoefficients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 411);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lstIndeVar);
            this.Controls.Add(this.btnOpenSWM);
            this.Controls.Add(this.txtSWM);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboNormalization);
            this.Controls.Add(this.lblNorm);
            this.Controls.Add(this.cboFamily);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.grbSave);
            this.Controls.Add(this.chkCoeEVs);
            this.Controls.Add(this.grbEV);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboFieldName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnMoveLeft);
            this.Controls.Add(this.btnMoveRight);
            this.Controls.Add(this.lstFields);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboTargetLayer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVaryingCoefficients";
            this.Text = "Spatially Varying Coefficients";
            this.grbSave.ResumeLayout(false);
            this.grbEV.ResumeLayout(false);
            this.grbEV.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdOpenSWM;
        private System.Windows.Forms.Button btnOpenSWM;
        private System.Windows.Forms.TextBox txtSWM;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboNormalization;
        private System.Windows.Forms.Label lblNorm;
        private System.Windows.Forms.ComboBox cboFamily;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grbSave;
        private System.Windows.Forms.ListView lstSave;
        private System.Windows.Forms.ColumnHeader colTypes;
        private System.Windows.Forms.ColumnHeader colNames;
        private System.Windows.Forms.CheckBox chkCoeEVs;
        private System.Windows.Forms.GroupBox grbEV;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblDirection;
        private System.Windows.Forms.ComboBox cboDirection;
        private System.Windows.Forms.NumericUpDown nudEValue;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboFieldName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMoveLeft;
        private System.Windows.Forms.Button btnMoveRight;
        private System.Windows.Forms.ListBox lstFields;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTargetLayer;
        private System.Windows.Forms.CheckedListBox lstIndeVar;
        private System.Windows.Forms.TextBox textBox1;
    }
}