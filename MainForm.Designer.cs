namespace ESFTool
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.appManager = new DotSpatial.Controls.AppManager();
            this.spatialDockManager1 = new DotSpatial.Controls.SpatialDockManager();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.legend1 = new DotSpatial.Controls.Legend();
            this.map1 = new DotSpatial.Controls.Map();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataTransformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regressionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linearRegressionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalizedLinearModelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spatialAutoregressionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eigenvectorSpatialFilteringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.NewMap = new System.Windows.Forms.ToolStripButton();
            this.btnAddMap = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomIn = new System.Windows.Forms.ToolStripButton();
            this.zoomOut = new System.Windows.Forms.ToolStripButton();
            this.FixedZoomIn = new System.Windows.Forms.ToolStripButton();
            this.FixedZoomOut = new System.Windows.Forms.ToolStripButton();
            this.ZoomPrevious = new System.Windows.Forms.ToolStripButton();
            this.zoomNext = new System.Windows.Forms.ToolStripButton();
            this.pan = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.SelectTool = new System.Windows.Forms.ToolStripButton();
            this.identify = new System.Windows.Forms.ToolStripButton();
            this.Pointer = new System.Windows.Forms.ToolStripButton();
            this.ViewAttributeTable = new System.Windows.Forms.ToolStripButton();
            this.spatiallyVaryingCoefficientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.spatialDockManager1)).BeginInit();
            this.spatialDockManager1.Panel1.SuspendLayout();
            this.spatialDockManager1.Panel2.SuspendLayout();
            this.spatialDockManager1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.MainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // appManager
            // 
            this.appManager.Directories = ((System.Collections.Generic.List<string>)(resources.GetObject("appManager.Directories")));
            this.appManager.DockManager = this.spatialDockManager1;
            this.appManager.HeaderControl = null;
            this.appManager.Legend = this.legend1;
            this.appManager.Map = this.map1;
            this.appManager.ProgressHandler = null;
            // 
            // spatialDockManager1
            // 
            this.spatialDockManager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spatialDockManager1.Location = new System.Drawing.Point(0, 55);
            this.spatialDockManager1.Name = "spatialDockManager1";
            // 
            // spatialDockManager1.Panel1
            // 
            this.spatialDockManager1.Panel1.Controls.Add(this.tabControl1);
            // 
            // spatialDockManager1.Panel2
            // 
            this.spatialDockManager1.Panel2.Controls.Add(this.map1);
            this.spatialDockManager1.Size = new System.Drawing.Size(1016, 561);
            this.spatialDockManager1.SplitterDistance = 205;
            this.spatialDockManager1.SplitterWidth = 3;
            this.spatialDockManager1.TabControl1 = this.tabControl1;
            this.spatialDockManager1.TabControl2 = null;
            this.spatialDockManager1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(205, 561);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.legend1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(197, 535);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Table of Contents";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // legend1
            // 
            this.legend1.BackColor = System.Drawing.Color.White;
            this.legend1.ControlRectangle = new System.Drawing.Rectangle(0, 0, 191, 529);
            this.legend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.legend1.DocumentRectangle = new System.Drawing.Rectangle(0, 0, 187, 428);
            this.legend1.HorizontalScrollEnabled = true;
            this.legend1.Indentation = 30;
            this.legend1.IsInitialized = false;
            this.legend1.Location = new System.Drawing.Point(3, 3);
            this.legend1.MinimumSize = new System.Drawing.Size(4, 5);
            this.legend1.Name = "legend1";
            this.legend1.ProgressHandler = null;
            this.legend1.ResetOnResize = false;
            this.legend1.SelectionFontColor = System.Drawing.Color.Black;
            this.legend1.SelectionHighlight = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(238)))), ((int)(((byte)(252)))));
            this.legend1.Size = new System.Drawing.Size(191, 529);
            this.legend1.TabIndex = 0;
            this.legend1.Text = "legend1";
            this.legend1.UseLegendForSelection = true;
            this.legend1.VerticalScrollEnabled = true;
            // 
            // map1
            // 
            this.map1.AllowDrop = true;
            this.map1.BackColor = System.Drawing.Color.White;
            this.map1.CollisionDetection = false;
            this.map1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map1.ExtendBuffer = false;
            this.map1.FunctionMode = DotSpatial.Controls.FunctionMode.None;
            this.map1.IsBusy = false;
            this.map1.IsZoomedToMaxExtent = false;
            this.map1.Legend = this.legend1;
            this.map1.Location = new System.Drawing.Point(0, 0);
            this.map1.Name = "map1";
            this.map1.ProgressHandler = null;
            this.map1.ProjectionModeDefine = DotSpatial.Controls.ActionMode.Prompt;
            this.map1.ProjectionModeReproject = DotSpatial.Controls.ActionMode.Prompt;
            this.map1.RedrawLayersWhileResizing = false;
            this.map1.SelectionEnabled = true;
            this.map1.Size = new System.Drawing.Size(808, 561);
            this.map1.TabIndex = 0;
            this.map1.ZoomOutFartherThanMaxExtent = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.regressionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1016, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.clearLayerToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.addToolStripMenuItem.Text = "Add Shapefiles";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // clearLayerToolStripMenuItem
            // 
            this.clearLayerToolStripMenuItem.Name = "clearLayerToolStripMenuItem";
            this.clearLayerToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.clearLayerToolStripMenuItem.Text = "Clear All Layers";
            this.clearLayerToolStripMenuItem.Click += new System.EventHandler(this.clearLayerToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataTransformationToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // dataTransformationToolStripMenuItem
            // 
            this.dataTransformationToolStripMenuItem.Name = "dataTransformationToolStripMenuItem";
            this.dataTransformationToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.dataTransformationToolStripMenuItem.Text = "Data Transformation";
            this.dataTransformationToolStripMenuItem.Click += new System.EventHandler(this.dataTransformationToolStripMenuItem_Click);
            // 
            // regressionToolStripMenuItem
            // 
            this.regressionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.linearRegressionToolStripMenuItem,
            this.generalizedLinearModelsToolStripMenuItem,
            this.spatialAutoregressionToolStripMenuItem,
            this.eigenvectorSpatialFilteringToolStripMenuItem,
            this.spatiallyVaryingCoefficientsToolStripMenuItem});
            this.regressionToolStripMenuItem.Name = "regressionToolStripMenuItem";
            this.regressionToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.regressionToolStripMenuItem.Text = "Regression";
            // 
            // linearRegressionToolStripMenuItem
            // 
            this.linearRegressionToolStripMenuItem.Name = "linearRegressionToolStripMenuItem";
            this.linearRegressionToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.linearRegressionToolStripMenuItem.Text = "Linear Regression";
            this.linearRegressionToolStripMenuItem.Click += new System.EventHandler(this.linearRegressionToolStripMenuItem_Click);
            // 
            // generalizedLinearModelsToolStripMenuItem
            // 
            this.generalizedLinearModelsToolStripMenuItem.Name = "generalizedLinearModelsToolStripMenuItem";
            this.generalizedLinearModelsToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.generalizedLinearModelsToolStripMenuItem.Text = "Generalized Linear Models";
            this.generalizedLinearModelsToolStripMenuItem.Click += new System.EventHandler(this.generalizedLinearModelsToolStripMenuItem_Click);
            // 
            // spatialAutoregressionToolStripMenuItem
            // 
            this.spatialAutoregressionToolStripMenuItem.Name = "spatialAutoregressionToolStripMenuItem";
            this.spatialAutoregressionToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.spatialAutoregressionToolStripMenuItem.Text = "Spatial Autoregression";
            this.spatialAutoregressionToolStripMenuItem.Click += new System.EventHandler(this.spatialAutoregressionToolStripMenuItem_Click);
            // 
            // eigenvectorSpatialFilteringToolStripMenuItem
            // 
            this.eigenvectorSpatialFilteringToolStripMenuItem.Name = "eigenvectorSpatialFilteringToolStripMenuItem";
            this.eigenvectorSpatialFilteringToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.eigenvectorSpatialFilteringToolStripMenuItem.Text = "Eigenvector Spatial Filtering";
            this.eigenvectorSpatialFilteringToolStripMenuItem.Click += new System.EventHandler(this.eigenvectorSpatialFilteringToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MainToolStrip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1016, 31);
            this.panel1.TabIndex = 2;
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.MainToolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewMap,
            this.btnAddMap,
            this.toolStripSeparator1,
            this.zoomIn,
            this.zoomOut,
            this.FixedZoomIn,
            this.FixedZoomOut,
            this.ZoomPrevious,
            this.zoomNext,
            this.pan,
            this.toolStripButton1,
            this.toolStripSeparator2,
            this.SelectTool,
            this.identify,
            this.Pointer,
            this.ViewAttributeTable});
            this.MainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MainToolStrip.Size = new System.Drawing.Size(1016, 31);
            this.MainToolStrip.TabIndex = 0;
            this.MainToolStrip.Text = "Main Tools";
            // 
            // NewMap
            // 
            this.NewMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewMap.Image = ((System.Drawing.Image)(resources.GetObject("NewMap.Image")));
            this.NewMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewMap.Name = "NewMap";
            this.NewMap.Size = new System.Drawing.Size(28, 28);
            this.NewMap.Text = "New Project";
            this.NewMap.Click += new System.EventHandler(this.NewMap_Click);
            // 
            // btnAddMap
            // 
            this.btnAddMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddMap.Image = ((System.Drawing.Image)(resources.GetObject("btnAddMap.Image")));
            this.btnAddMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddMap.Name = "btnAddMap";
            this.btnAddMap.Size = new System.Drawing.Size(28, 28);
            this.btnAddMap.Text = "Add Shapefile";
            this.btnAddMap.Click += new System.EventHandler(this.btnAddMap_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // zoomIn
            // 
            this.zoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomIn.Image = ((System.Drawing.Image)(resources.GetObject("zoomIn.Image")));
            this.zoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomIn.Name = "zoomIn";
            this.zoomIn.Size = new System.Drawing.Size(28, 28);
            this.zoomIn.Text = "Zoom In";
            this.zoomIn.Click += new System.EventHandler(this.zoomIn_Click);
            // 
            // zoomOut
            // 
            this.zoomOut.CheckOnClick = true;
            this.zoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOut.Image = ((System.Drawing.Image)(resources.GetObject("zoomOut.Image")));
            this.zoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomOut.Name = "zoomOut";
            this.zoomOut.Size = new System.Drawing.Size(28, 28);
            this.zoomOut.Text = "Zoom Out";
            this.zoomOut.Click += new System.EventHandler(this.zoomOut_Click);
            // 
            // FixedZoomIn
            // 
            this.FixedZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FixedZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("FixedZoomIn.Image")));
            this.FixedZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FixedZoomIn.Name = "FixedZoomIn";
            this.FixedZoomIn.Size = new System.Drawing.Size(28, 28);
            this.FixedZoomIn.Text = "Fixed Zoom In";
            this.FixedZoomIn.Click += new System.EventHandler(this.FixedZoomIn_Click);
            // 
            // FixedZoomOut
            // 
            this.FixedZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FixedZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("FixedZoomOut.Image")));
            this.FixedZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FixedZoomOut.Name = "FixedZoomOut";
            this.FixedZoomOut.Size = new System.Drawing.Size(28, 28);
            this.FixedZoomOut.Text = "Fixed Zoom Out";
            this.FixedZoomOut.Click += new System.EventHandler(this.FixedZoomOut_Click);
            // 
            // ZoomPrevious
            // 
            this.ZoomPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomPrevious.Image = ((System.Drawing.Image)(resources.GetObject("ZoomPrevious.Image")));
            this.ZoomPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomPrevious.Name = "ZoomPrevious";
            this.ZoomPrevious.Size = new System.Drawing.Size(28, 28);
            this.ZoomPrevious.Text = "Zoom to Previous";
            this.ZoomPrevious.Click += new System.EventHandler(this.ZoomPrevious_Click);
            // 
            // zoomNext
            // 
            this.zoomNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomNext.Image = ((System.Drawing.Image)(resources.GetObject("zoomNext.Image")));
            this.zoomNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomNext.Name = "zoomNext";
            this.zoomNext.Size = new System.Drawing.Size(28, 28);
            this.zoomNext.Text = "Zoom to Next";
            this.zoomNext.Click += new System.EventHandler(this.zoomNext_Click);
            // 
            // pan
            // 
            this.pan.CheckOnClick = true;
            this.pan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pan.Image = ((System.Drawing.Image)(resources.GetObject("pan.Image")));
            this.pan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pan.Name = "pan";
            this.pan.Size = new System.Drawing.Size(28, 28);
            this.pan.Text = "Pan";
            this.pan.Click += new System.EventHandler(this.pan_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton1.Text = "Zoom to Full Extent";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // SelectTool
            // 
            this.SelectTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SelectTool.Image = ((System.Drawing.Image)(resources.GetObject("SelectTool.Image")));
            this.SelectTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SelectTool.Name = "SelectTool";
            this.SelectTool.Size = new System.Drawing.Size(28, 28);
            this.SelectTool.Text = "Select Features";
            this.SelectTool.Click += new System.EventHandler(this.SelectTool_Click);
            // 
            // identify
            // 
            this.identify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.identify.Image = ((System.Drawing.Image)(resources.GetObject("identify.Image")));
            this.identify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.identify.Name = "identify";
            this.identify.Size = new System.Drawing.Size(28, 28);
            this.identify.Text = "Identify";
            this.identify.Click += new System.EventHandler(this.identify_Click);
            // 
            // Pointer
            // 
            this.Pointer.Checked = true;
            this.Pointer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Pointer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Pointer.Image = ((System.Drawing.Image)(resources.GetObject("Pointer.Image")));
            this.Pointer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Pointer.Name = "Pointer";
            this.Pointer.Size = new System.Drawing.Size(28, 28);
            this.Pointer.Text = "Pointer";
            this.Pointer.Click += new System.EventHandler(this.Pointer_Click);
            // 
            // ViewAttributeTable
            // 
            this.ViewAttributeTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ViewAttributeTable.Image = ((System.Drawing.Image)(resources.GetObject("ViewAttributeTable.Image")));
            this.ViewAttributeTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ViewAttributeTable.Name = "ViewAttributeTable";
            this.ViewAttributeTable.Size = new System.Drawing.Size(28, 28);
            this.ViewAttributeTable.Text = "View AttributeTable";
            this.ViewAttributeTable.Click += new System.EventHandler(this.ViewAttributeTable_Click);
            // 
            // spatiallyVaryingCoefficientsToolStripMenuItem
            // 
            this.spatiallyVaryingCoefficientsToolStripMenuItem.Name = "spatiallyVaryingCoefficientsToolStripMenuItem";
            this.spatiallyVaryingCoefficientsToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.spatiallyVaryingCoefficientsToolStripMenuItem.Text = "Spatially Varying Coefficients";
            this.spatiallyVaryingCoefficientsToolStripMenuItem.Click += new System.EventHandler(this.spatiallyVaryingCoefficientsToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 616);
            this.Controls.Add(this.spatialDockManager1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.spatialDockManager1.Panel1.ResumeLayout(false);
            this.spatialDockManager1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spatialDockManager1)).EndInit();
            this.spatialDockManager1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DotSpatial.Controls.AppManager appManager;
        private DotSpatial.Controls.SpatialDockManager spatialDockManager1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DotSpatial.Controls.Legend legend1;
        public DotSpatial.Controls.Map map1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem regressionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linearRegressionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generalizedLinearModelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spatialAutoregressionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eigenvectorSpatialFilteringToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.ToolStripButton btnAddMap;
        private System.Windows.Forms.ToolStripButton NewMap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton zoomIn;
        private System.Windows.Forms.ToolStripButton zoomOut;
        private System.Windows.Forms.ToolStripButton pan;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton SelectTool;
        private System.Windows.Forms.ToolStripButton identify;
        private System.Windows.Forms.ToolStripButton Pointer;
        private System.Windows.Forms.ToolStripButton FixedZoomIn;
        private System.Windows.Forms.ToolStripButton FixedZoomOut;
        private System.Windows.Forms.ToolStripButton ZoomPrevious;
        private System.Windows.Forms.ToolStripButton zoomNext;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataTransformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton ViewAttributeTable;
        private System.Windows.Forms.ToolStripMenuItem spatiallyVaryingCoefficientsToolStripMenuItem;
    }
}

