namespace ESFTool
{
    partial class frmMScatterResults
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMScatterResults));
            this.pChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRegression = new System.Windows.Forms.Label();
            this.conMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToImageFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pChart)).BeginInit();
            this.panel1.SuspendLayout();
            this.conMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pChart
            // 
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.Name = "ChartArea1";
            this.pChart.ChartAreas.Add(chartArea1);
            this.pChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.pChart.Legends.Add(legend1);
            this.pChart.Location = new System.Drawing.Point(0, 29);
            this.pChart.Name = "pChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.pChart.Series.Add(series1);
            this.pChart.Size = new System.Drawing.Size(561, 337);
            this.pChart.TabIndex = 4;
            this.pChart.Text = "chart1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblRegression);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(561, 29);
            this.panel1.TabIndex = 5;
            // 
            // lblRegression
            // 
            this.lblRegression.AutoSize = true;
            this.lblRegression.Location = new System.Drawing.Point(13, 10);
            this.lblRegression.Name = "lblRegression";
            this.lblRegression.Size = new System.Drawing.Size(35, 13);
            this.lblRegression.TabIndex = 0;
            this.lblRegression.Text = "label1";
            // 
            // conMenu
            // 
            this.conMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToImageFileToolStripMenuItem});
            this.conMenu.Name = "conMenu";
            this.conMenu.Size = new System.Drawing.Size(181, 26);
            // 
            // exportToImageFileToolStripMenuItem
            // 
            this.exportToImageFileToolStripMenuItem.Name = "exportToImageFileToolStripMenuItem";
            this.exportToImageFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportToImageFileToolStripMenuItem.Text = "Export to image file";
            // 
            // frmMScatterResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 366);
            this.Controls.Add(this.pChart);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMScatterResults";
            this.Text = "frmMScatterResults";
            ((System.ComponentModel.ISupportInitialize)(this.pChart)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.conMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart pChart;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lblRegression;
        private System.Windows.Forms.ContextMenuStrip conMenu;
        private System.Windows.Forms.ToolStripMenuItem exportToImageFileToolStripMenuItem;
    }
}