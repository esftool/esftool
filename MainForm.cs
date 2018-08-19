using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;

using RDotNet;
using RDotNet.NativeLibrary;
using DotSpatial.Controls;

namespace ESFTool
{
    public partial class MainForm : Form
    {
        #region class private members
        private string strREngineName = "RDotNet";
        #endregion
        
        #region class public members
        public REngine pEngine;
        public string strPath;
        public bool[] blnsInstalledPcks;
        public string[] LibHome;
        //Feature counts for warning sign.
        public int intWarningCount = 200;
        //for drawing R plots
        public System.Collections.Generic.List<string> multipageImage; 
        public int intCurrentIdx = 0;

        #endregion

        [Export("Shell", typeof(ContainerControl))]
        private static ContainerControl Shell;

        public MainForm()
        {
            InitializeComponent();
            if (DesignMode) return;
            Shell = this;
            //appManager.LoadExtensions(); //Add new 

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

                //Get Start up path to set a sample data path and path of temporary folder
                strPath = System.Windows.Forms.Application.StartupPath;
                //R environment setting
                //Current version of R is 3.4.4 (05/23/18 HK)
                var envPath = Environment.GetEnvironmentVariable("PATH");
                var rBinPath = strPath + @"\R-3.4.4\bin\i386"; // R is copited into startup path
                Environment.SetEnvironmentVariable("PATH", envPath + System.IO.Path.PathSeparator + rBinPath);
                Environment.SetEnvironmentVariable("R_HOME", strPath + @"\R-3.4.4");

                //Loading REngine
                pEngine = REngine.CreateInstance(strREngineName);
                pEngine.Initialize();

                //Set Library home and remove local home!
                LibHome = pEngine.Evaluate(".libPaths()").AsCharacter().ToArray();
                string strLibPath = strPath.Replace(@"\", @"/") + "/R-3.4.4/library"; //path for R packages
                pEngine.Evaluate(".Library.site <- file.path('" + strLibPath + "')");
                pEngine.Evaluate("Sys.setenv(R_LIBS_USER='" + strLibPath + "')");
                pEngine.Evaluate(".libPaths(c('" + strLibPath + "', .Library.site, .Library))");

                //Checked installed packages and R 
                LibHome = pEngine.Evaluate(".libPaths()").AsCharacter().ToArray();
                pEngine.Evaluate("ip <- installed.packages()").AsCharacter();
                string[] installedPackages = pEngine.Evaluate("ip[,1]").AsCharacter().ToArray(); //To Check Installed Packages in R

                //The funtion below will be added to check installed packages in R (HK 05/23/18)
                //clsRPackageNames pPckNames = new clsRPackageNames();
                //blnsInstalledPcks = pPckNames.CheckedRequiredPackages(installedPackages);
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.map1.AddFeatureLayers();
        }

        private void clearLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.map1.ClearLayers();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void eigenvectorSpatialFilteringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmESF pfrmESF = new frmESF();
            pfrmESF.Show();
        }

        private void NewMap_Click(object sender, EventArgs e)
        {
            this.map1.ClearLayers();
        }

        private void btnAddMap_Click(object sender, EventArgs e)
        {
            this.map1.AddFeatureLayers();
        }

        private void zoomIn_Click(object sender, EventArgs e)
        {
            zoomIn.Checked = true;
            zoomOut.Checked = false;
            pan.Checked = false;
            identify.Checked = false;
            SelectTool.Checked = false;
            Pointer.Checked = false;
            this.map1.FunctionMode = FunctionMode.ZoomIn;
        }

        private void zoomOut_Click(object sender, EventArgs e)
        {
            zoomIn.Checked = false;
            zoomOut.Checked = true;
            pan.Checked = false;
            identify.Checked = false;
            SelectTool.Checked = false;
            Pointer.Checked = false;
            this.map1.FunctionMode = FunctionMode.ZoomOut;
        }

        private void pan_Click(object sender, EventArgs e)
        {
            zoomIn.Checked = false;
            zoomOut.Checked = false;
            pan.Checked = true;
            identify.Checked = false;
            SelectTool.Checked = false;
            Pointer.Checked = false;
            this.map1.FunctionMode = FunctionMode.Pan;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.map1.ZoomToMaxExtent();
        }

        private void SelectTool_Click(object sender, EventArgs e)
        {
            zoomIn.Checked = false;
            zoomOut.Checked = false;
            pan.Checked = false;
            identify.Checked = false;
            SelectTool.Checked = true;
            Pointer.Checked = false;
            this.map1.FunctionMode = FunctionMode.Select;
        }

        private void identify_Click(object sender, EventArgs e)
        {
            zoomIn.Checked = false;
            zoomOut.Checked = false;
            pan.Checked = false;
            identify.Checked = true;
            SelectTool.Checked = false;
            Pointer.Checked = false;
            this.map1.FunctionMode = FunctionMode.Info;
        }

        private void Pointer_Click(object sender, EventArgs e)
        {
            zoomIn.Checked = false;
            zoomOut.Checked = false;
            pan.Checked = false;
            identify.Checked = false;
            SelectTool.Checked = false;
            Pointer.Checked = true;
            this.map1.FunctionMode = FunctionMode.None;
        }

        private void FixedZoomIn_Click(object sender, EventArgs e)
        {
            this.map1.ZoomIn();

        }

        private void FixedZoomOut_Click(object sender, EventArgs e)
        {
            this.map1.ZoomOut();
        }

        private void ZoomPrevious_Click(object sender, EventArgs e)
        {
            this.map1.ZoomToPrevious();
        }

        private void zoomNext_Click(object sender, EventArgs e)
        {
            this.map1.ZoomToNext();
        }

        private void linearRegressionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegression pfrmRegression = new frmRegression();
            pfrmRegression.Show();
        }

        private void generalizedLinearModelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGLM pfrmGLM = new frmGLM();
            pfrmGLM.Show();
        }

        private void spatialAutoregressionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSpatialRegression pfrmSpregression = new frmSpatialRegression();
            pfrmSpregression.Show();
        }

        private void dataTransformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBoxCox pfrmBoxCox = new frmBoxCox();
            pfrmBoxCox.Show();
        }

        private void ViewAttributeTable_Click(object sender, EventArgs e)
        {
            try
            {

                IMapLayer pMapLayer = this.map1.Layers.SelectedLayer;
                if (pMapLayer != null)
                {
                    frmAttributeTable pfrmAttTable = new frmAttributeTable();
                    DataTable dt = null;
                    if (pMapLayer is MapPointLayer)
                    {
                        MapPointLayer pMapPointLyr = default(MapPointLayer);
                        pMapPointLyr = (MapPointLayer)pMapLayer;
                        dt = pMapPointLyr.DataSet.DataTable;
                    }
                    else if (pMapLayer is MapPolygonLayer)
                    {
                        MapPolygonLayer pMapPolyLyr = default(MapPolygonLayer);
                        pMapPolyLyr = (MapPolygonLayer)pMapLayer;
                        dt = pMapPolyLyr.DataSet.DataTable;
                    }
                    else if (pMapLayer is MapLineLayer)
                    {
                        MapLineLayer pMapLineLyr = default(MapLineLayer);
                        pMapLineLyr = (MapLineLayer)pMapLayer;
                        dt = pMapLineLyr.DataSet.DataTable;
                    }
                    pfrmAttTable.dgvAttTable.DataSource = dt;

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (i != 1)
                        {
                            if (dt.Columns[i].DataType == System.Type.GetType("System.Int16")) pfrmAttTable.dgvAttTable.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            if (dt.Columns[i].DataType == System.Type.GetType("System.Int32")) pfrmAttTable.dgvAttTable.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            if (dt.Columns[i].DataType == System.Type.GetType("System.Single")) pfrmAttTable.dgvAttTable.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            if (dt.Columns[i].DataType == System.Type.GetType("System.Double")) pfrmAttTable.dgvAttTable.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                    }


                    pfrmAttTable.Text = "Attribute of " + pMapLayer.DataSet.Name;
                    pfrmAttTable.dgvAttTable.AllowUserToAddRows = false;

                    pfrmAttTable.Show();
                }
                else
                {
                    MessageBox.Show("Please select a feature layer");
                }
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void spatiallyVaryingCoefficientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVaryingCoefficients frmSPC = new frmVaryingCoefficients();
            frmSPC.Show();
        }
    }
}
