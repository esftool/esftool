namespace ESFTool
{
    partial class frmBoxCox
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBoxCox));
            this.exportToImageFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkGamma = new System.Windows.Forms.CheckBox();
            this.btnTrans = new System.Windows.Forms.Button();
            this.cboFieldName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTargetLayer = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblGamma = new System.Windows.Forms.Label();
            this.txtSW = new System.Windows.Forms.TextBox();
            this.nudGamma = new System.Windows.Forms.NumericUpDown();
            this.nudLambda = new System.Windows.Forms.NumericUpDown();
            this.btnAddPlot = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trbLambda = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.conMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.grbPara = new System.Windows.Forms.GroupBox();
            this.trbGamma = new System.Windows.Forms.TrackBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSaveResult = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.grbSave = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pHistogram = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pQQPlot = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.nudGamma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLambda)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbLambda)).BeginInit();
            this.conMenu.SuspendLayout();
            this.grbPara.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbGamma)).BeginInit();
            this.grbSave.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pHistogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pQQPlot)).BeginInit();
            this.SuspendLayout();
            // 
            // exportToImageFileToolStripMenuItem
            // 
            this.exportToImageFileToolStripMenuItem.Name = "exportToImageFileToolStripMenuItem";
            this.exportToImageFileToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.exportToImageFileToolStripMenuItem.Text = "Export to image file";
            // 
            // chkGamma
            // 
            this.chkGamma.AutoSize = true;
            this.chkGamma.Location = new System.Drawing.Point(9, 100);
            this.chkGamma.Name = "chkGamma";
            this.chkGamma.Size = new System.Drawing.Size(84, 17);
            this.chkGamma.TabIndex = 33;
            this.chkGamma.Text = "Add Gamma";
            this.chkGamma.UseVisualStyleBackColor = true;
            // 
            // btnTrans
            // 
            this.btnTrans.Location = new System.Drawing.Point(6, 120);
            this.btnTrans.Name = "btnTrans";
            this.btnTrans.Size = new System.Drawing.Size(148, 23);
            this.btnTrans.TabIndex = 32;
            this.btnTrans.Text = "Box-Cox Transformation";
            this.btnTrans.UseVisualStyleBackColor = true;
            this.btnTrans.Click += new System.EventHandler(this.btnTrans_Click);
            // 
            // cboFieldName
            // 
            this.cboFieldName.FormattingEnabled = true;
            this.cboFieldName.Location = new System.Drawing.Point(6, 73);
            this.cboFieldName.Name = "cboFieldName";
            this.cboFieldName.Size = new System.Drawing.Size(155, 21);
            this.cboFieldName.TabIndex = 31;
            this.cboFieldName.SelectedIndexChanged += new System.EventHandler(this.cboFieldName_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Variable:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Select a Target Layer:";
            // 
            // cboTargetLayer
            // 
            this.cboTargetLayer.FormattingEnabled = true;
            this.cboTargetLayer.Location = new System.Drawing.Point(6, 35);
            this.cboTargetLayer.Name = "cboTargetLayer";
            this.cboTargetLayer.Size = new System.Drawing.Size(155, 21);
            this.cboTargetLayer.TabIndex = 28;
            this.cboTargetLayer.SelectedIndexChanged += new System.EventHandler(this.cboTargetLayer_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Shapiro-Wilk test:";
            // 
            // lblGamma
            // 
            this.lblGamma.AutoSize = true;
            this.lblGamma.Location = new System.Drawing.Point(9, 78);
            this.lblGamma.Name = "lblGamma";
            this.lblGamma.Size = new System.Drawing.Size(46, 13);
            this.lblGamma.TabIndex = 2;
            this.lblGamma.Text = "Gamma:";
            // 
            // txtSW
            // 
            this.txtSW.Location = new System.Drawing.Point(99, 135);
            this.txtSW.Name = "txtSW";
            this.txtSW.ReadOnly = true;
            this.txtSW.Size = new System.Drawing.Size(54, 20);
            this.txtSW.TabIndex = 30;
            // 
            // nudGamma
            // 
            this.nudGamma.DecimalPlaces = 2;
            this.nudGamma.Enabled = false;
            this.nudGamma.Increment = new decimal(new int[] {
            2,
            0,
            0,
            131072});
            this.nudGamma.Location = new System.Drawing.Point(60, 76);
            this.nudGamma.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudGamma.Name = "nudGamma";
            this.nudGamma.ReadOnly = true;
            this.nudGamma.Size = new System.Drawing.Size(92, 20);
            this.nudGamma.TabIndex = 8;
            this.nudGamma.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudGamma.ValueChanged += new System.EventHandler(this.nudGamma_ValueChanged);
            // 
            // nudLambda
            // 
            this.nudLambda.DecimalPlaces = 2;
            this.nudLambda.Increment = new decimal(new int[] {
            2,
            0,
            0,
            131072});
            this.nudLambda.Location = new System.Drawing.Point(61, 16);
            this.nudLambda.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudLambda.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            -2147483648});
            this.nudLambda.Name = "nudLambda";
            this.nudLambda.Size = new System.Drawing.Size(92, 20);
            this.nudLambda.TabIndex = 7;
            this.nudLambda.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudLambda.ValueChanged += new System.EventHandler(this.nudLambda_ValueChanged);
            // 
            // btnAddPlot
            // 
            this.btnAddPlot.Enabled = false;
            this.btnAddPlot.Location = new System.Drawing.Point(7, 145);
            this.btnAddPlot.Name = "btnAddPlot";
            this.btnAddPlot.Size = new System.Drawing.Size(148, 23);
            this.btnAddPlot.TabIndex = 34;
            this.btnAddPlot.Text = "Show Original Q-Q Plot";
            this.btnAddPlot.UseVisualStyleBackColor = true;
            this.btnAddPlot.Click += new System.EventHandler(this.btnAddPlot_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddPlot);
            this.groupBox1.Controls.Add(this.chkGamma);
            this.groupBox1.Controls.Add(this.btnTrans);
            this.groupBox1.Controls.Add(this.cboFieldName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboTargetLayer);
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(166, 174);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input";
            // 
            // trbLambda
            // 
            this.trbLambda.Location = new System.Drawing.Point(12, 42);
            this.trbLambda.Maximum = 200;
            this.trbLambda.Minimum = -200;
            this.trbLambda.Name = "trbLambda";
            this.trbLambda.Size = new System.Drawing.Size(142, 45);
            this.trbLambda.TabIndex = 2;
            this.trbLambda.TickFrequency = 5;
            this.trbLambda.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trbLambda.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trbLambda_MouseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Lambda:";
            // 
            // conMenu
            // 
            this.conMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToImageFileToolStripMenuItem});
            this.conMenu.Name = "conMenu";
            this.conMenu.Size = new System.Drawing.Size(177, 26);
            // 
            // grbPara
            // 
            this.grbPara.Controls.Add(this.label5);
            this.grbPara.Controls.Add(this.lblGamma);
            this.grbPara.Controls.Add(this.txtSW);
            this.grbPara.Controls.Add(this.nudGamma);
            this.grbPara.Controls.Add(this.nudLambda);
            this.grbPara.Controls.Add(this.trbLambda);
            this.grbPara.Controls.Add(this.label3);
            this.grbPara.Controls.Add(this.trbGamma);
            this.grbPara.Enabled = false;
            this.grbPara.Location = new System.Drawing.Point(12, 190);
            this.grbPara.Name = "grbPara";
            this.grbPara.Size = new System.Drawing.Size(161, 178);
            this.grbPara.TabIndex = 29;
            this.grbPara.TabStop = false;
            this.grbPara.Text = "Parameters";
            // 
            // trbGamma
            // 
            this.trbGamma.Enabled = false;
            this.trbGamma.Location = new System.Drawing.Point(12, 100);
            this.trbGamma.Maximum = 200;
            this.trbGamma.Minimum = -200;
            this.trbGamma.Name = "trbGamma";
            this.trbGamma.Size = new System.Drawing.Size(142, 45);
            this.trbGamma.TabIndex = 6;
            this.trbGamma.TickFrequency = 5;
            this.trbGamma.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trbGamma.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trbGamma_MouseUp);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(94, 52);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(59, 23);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Field Name:";
            // 
            // txtSaveResult
            // 
            this.txtSaveResult.Location = new System.Drawing.Point(77, 20);
            this.txtSaveResult.Name = "txtSaveResult";
            this.txtSaveResult.Size = new System.Drawing.Size(75, 20);
            this.txtSaveResult.TabIndex = 31;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 52);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(59, 23);
            this.btnSave.TabIndex = 32;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grbSave
            // 
            this.grbSave.Controls.Add(this.btnCancel);
            this.grbSave.Controls.Add(this.label4);
            this.grbSave.Controls.Add(this.txtSaveResult);
            this.grbSave.Controls.Add(this.btnSave);
            this.grbSave.Enabled = false;
            this.grbSave.Location = new System.Drawing.Point(12, 374);
            this.grbSave.Name = "grbSave";
            this.grbSave.Size = new System.Drawing.Size(161, 83);
            this.grbSave.TabIndex = 34;
            this.grbSave.TabStop = false;
            this.grbSave.Text = "Save Result";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grbSave);
            this.panel1.Controls.Add(this.grbPara);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(189, 460);
            this.panel1.TabIndex = 3;
            // 
            // pHistogram
            // 
            this.pHistogram.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX.LabelStyle.Interval = 0D;
            chartArea1.AxisX.LabelStyle.IntervalOffset = 0D;
            chartArea1.AxisX.LabelStyle.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisX.LabelStyle.TruncatedLabels = true;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorTickMark.Interval = 1D;
            chartArea1.AxisX.MajorTickMark.IntervalOffset = 0.5D;
            chartArea1.AxisX.MajorTickMark.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX2.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.Name = "ChartArea1";
            this.pHistogram.ChartAreas.Add(chartArea1);
            this.pHistogram.Dock = System.Windows.Forms.DockStyle.Right;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.pHistogram.Legends.Add(legend1);
            this.pHistogram.Location = new System.Drawing.Point(556, 0);
            this.pHistogram.Name = "pHistogram";
            this.pHistogram.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series1.BorderColor = System.Drawing.Color.Black;
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.White;
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.MarkerBorderColor = System.Drawing.Color.White;
            series1.Name = "Series1";
            this.pHistogram.Series.Add(series1);
            this.pHistogram.Size = new System.Drawing.Size(348, 460);
            this.pHistogram.TabIndex = 5;
            this.pHistogram.Text = "chart1";
            // 
            // pQQPlot
            // 
            this.pQQPlot.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea2.AxisX.LabelStyle.Interval = 0D;
            chartArea2.AxisX.LabelStyle.IntervalOffset = 0D;
            chartArea2.AxisX.LabelStyle.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea2.AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea2.AxisX.LabelStyle.TruncatedLabels = true;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.MajorTickMark.Interval = 1D;
            chartArea2.AxisX.MajorTickMark.IntervalOffset = 0.5D;
            chartArea2.AxisX.MajorTickMark.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisX2.MajorGrid.Enabled = false;
            chartArea2.AxisY.MajorGrid.Enabled = false;
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisY2.MajorGrid.Enabled = false;
            chartArea2.Name = "ChartArea1";
            this.pQQPlot.ChartAreas.Add(chartArea2);
            this.pQQPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.pQQPlot.Legends.Add(legend2);
            this.pQQPlot.Location = new System.Drawing.Point(189, 0);
            this.pQQPlot.Name = "pQQPlot";
            this.pQQPlot.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series2.BorderColor = System.Drawing.Color.Black;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.White;
            series2.IsVisibleInLegend = false;
            series2.Legend = "Legend1";
            series2.MarkerBorderColor = System.Drawing.Color.White;
            series2.Name = "Series1";
            this.pQQPlot.Series.Add(series2);
            this.pQQPlot.Size = new System.Drawing.Size(367, 460);
            this.pQQPlot.TabIndex = 6;
            this.pQQPlot.Text = "chart1";
            // 
            // frmBoxCox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(904, 460);
            this.Controls.Add(this.pQQPlot);
            this.Controls.Add(this.pHistogram);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBoxCox";
            this.Text = "Box-Cox Transformation";
            ((System.ComponentModel.ISupportInitialize)(this.nudGamma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLambda)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbLambda)).EndInit();
            this.conMenu.ResumeLayout(false);
            this.grbPara.ResumeLayout(false);
            this.grbPara.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbGamma)).EndInit();
            this.grbSave.ResumeLayout(false);
            this.grbSave.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pHistogram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pQQPlot)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem exportToImageFileToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkGamma;
        private System.Windows.Forms.Button btnTrans;
        private System.Windows.Forms.ComboBox cboFieldName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTargetLayer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblGamma;
        private System.Windows.Forms.TextBox txtSW;
        private System.Windows.Forms.NumericUpDown nudGamma;
        private System.Windows.Forms.NumericUpDown nudLambda;
        private System.Windows.Forms.Button btnAddPlot;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar trbLambda;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip conMenu;
        private System.Windows.Forms.GroupBox grbPara;
        private System.Windows.Forms.TrackBar trbGamma;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSaveResult;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grbSave;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.DataVisualization.Charting.Chart pHistogram;
        public System.Windows.Forms.DataVisualization.Charting.Chart pQQPlot;
    }
}