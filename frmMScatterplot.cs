using DotSpatial.Controls;
using RDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESFTool
{
    public partial class frmMScatterplot : Form
    {
        private MainForm m_pForm;
        private REngine m_pEngine;
        private IMapLayer m_pMaplayer;
        private DataTable m_dt = null;
        private clsSnippet m_pSnippet;

        //Varaibles for SWM
        private bool m_blnCreateSWM = false;

        public frmMScatterplot()
        {
            InitializeComponent();
            m_pForm = Application.OpenForms["MainForm"] as MainForm;

            for (int i = 0; i < m_pForm.map1.Layers.Count; i++)
            {
                cboTargetLayer.Items.Add(m_pForm.map1.Layers[i].DataSet.Name);
            }

            m_pEngine = m_pForm.pEngine;
            m_pEngine.Evaluate("library(spdep); library(maptools); library(MASS)");

            m_pSnippet = new clsSnippet();
        }

        private void cboTargetLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTargetLayer.Text != "")
                {

                    string strLayerName = cboTargetLayer.Text;
                    m_pMaplayer = null;
                    for (int i = 0; i < m_pForm.map1.Layers.Count; i++)
                    {
                        if (strLayerName == m_pForm.map1.Layers[i].DataSet.Name)
                        {
                            m_pMaplayer = m_pForm.map1.Layers[i];
                        }
                    }


                    clsSnippet.SpatialWeightMatrixType pSWMType = new clsSnippet.SpatialWeightMatrixType();
                    if (m_pMaplayer is MapPointLayer)
                    {
                        MapPointLayer pMapPointLyr = default(MapPointLayer);
                        pMapPointLyr = (MapPointLayer)m_pMaplayer;
                        m_dt = pMapPointLyr.DataSet.DataTable;
                        txtSWM.Text = pSWMType.strPointSWM;
                    }
                    else if (m_pMaplayer is MapPolygonLayer)
                    {
                        MapPolygonLayer pMapPolyLyr = default(MapPolygonLayer);
                        pMapPolyLyr = (MapPolygonLayer)m_pMaplayer;
                        m_dt = pMapPolyLyr.DataSet.DataTable;
                        txtSWM.Text = pSWMType.strPolySWM;
                    }
                    else if (m_pMaplayer is MapLineLayer)
                    {
                        MessageBox.Show("Spatial weights matrix for polyline is not supported.");
                        return;
                    }

                    cboFldNm1.Items.Clear();
                    cboFldNm1.Text = "";

                    for (int i = 0; i < m_dt.Columns.Count; i++)
                    {
                        if (m_pSnippet.FindNumberFieldType(m_dt.Columns[i]))
                        {
                            cboFldNm1.Items.Add(m_dt.Columns[i].ColumnName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOpenSWM_Click(object sender, EventArgs e)
        {
            if (m_dt == null)
            {
                MessageBox.Show("Please select a target layer");
                return;
            }
            frmAdvSWM pfrmAdvSWM = new frmAdvSWM();
            pfrmAdvSWM.m_pMapLayer = m_pMaplayer;
            pfrmAdvSWM.blnCorrelogram = false;
            pfrmAdvSWM.ShowDialog();
            m_blnCreateSWM = pfrmAdvSWM.blnSWMCreation;
            txtSWM.Text = pfrmAdvSWM.txtSWM.Text;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {

                frmProgress pfrmProgress = new frmProgress();
                pfrmProgress.lblStatus.Text = "Processing:";
                pfrmProgress.pgbProgress.Style = ProgressBarStyle.Marquee;
                pfrmProgress.Show();


                if (cboFldNm1.Text == "")
                {
                    MessageBox.Show("Please select a proper field");
                    return;
                }

                int nFeature = m_dt.Rows.Count;

                if (!m_blnCreateSWM)
                {
                    //Get the file path and name to create spatial weight matrix
                    string strNameR = m_pSnippet.FilePathinRfromLayer(m_pMaplayer);

                    if (strNameR == null)
                        return;

                    //Create spatial weight matrix in R
                    if (m_pMaplayer is MapPointLayer)
                        m_pEngine.Evaluate("sample.shp <- readShapePoints('" + strNameR + "')");
                    else if (m_pMaplayer is MapPolygonLayer)
                        m_pEngine.Evaluate("sample.shp <- readShapePoly('" + strNameR + "')");
                    else
                    {
                        MessageBox.Show("This geometry type is not supported");
                        pfrmProgress.Close();
                        this.Close();
                    }


                    int intSuccess = m_pSnippet.CreateSpatialWeightMatrix(m_pEngine, m_pMaplayer, txtSWM.Text, pfrmProgress);
                    if (intSuccess == 0)
                        return;
                }

                string strVarNM = (string)cboFldNm1.SelectedItem;
                int intVarIdx = m_dt.Columns.IndexOf(strVarNM);
                double[] arrVar = new double[nFeature];
                int i = 0;
                foreach (DataRow row in m_dt.Rows)
                {

                    arrVar[i] = Convert.ToDouble(row[intVarIdx]);
                    i++;
                }

                NumericVector vecVar = m_pEngine.CreateNumericVector(arrVar);
                m_pEngine.SetSymbol(strVarNM, vecVar);

                if (chkStd.Checked)
                {
                    m_pEngine.Evaluate(strVarNM + " <- scale(" + strVarNM + ")"); //Scaled
                    vecVar = m_pEngine.Evaluate(strVarNM).AsNumeric();
                }
                NumericVector vecWeightVar = null;
                vecWeightVar = m_pEngine.Evaluate("wx.sample <- lag.listw(sample.listw, " + strVarNM + ", zero.policy=TRUE)").AsNumeric();
                m_pEngine.SetSymbol("WVar.sample", vecWeightVar);
                NumericVector vecCoeff = m_pEngine.Evaluate("lm(WVar.sample~" + strVarNM + ")$coefficients").AsNumeric();

                frmMScatterResults pfrmMScatterResult = new frmMScatterResults();
                pfrmMScatterResult.pChart.ChartAreas[0].AxisX.IsStartedFromZero = false;
                pfrmMScatterResult.pChart.ChartAreas[0].AxisX.IsMarginVisible = true;

                pfrmMScatterResult.pChart.ChartAreas[0].AxisY.IsStartedFromZero = false;

                pfrmMScatterResult.Text = "Moran Scatter Plot of " + m_pMaplayer.DataSet.Name;
                pfrmMScatterResult.pChart.Series.Clear();
                System.Drawing.Color pMarkerColor = System.Drawing.Color.Blue;
                var seriesPts = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Points",
                    Color = pMarkerColor,
                    BorderColor = pMarkerColor,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = false,
                    ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point,
                    MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle

                };

                pfrmMScatterResult.pChart.Series.Add(seriesPts);

                for (int j = 0; j < vecVar.Length; j++)
                    seriesPts.Points.AddXY(vecVar[j], vecWeightVar[j]);



                var VLine = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "VLine",
                    Color = System.Drawing.Color.Black,
                    BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash,
                    //BorderColor = System.Drawing.Color.Black,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = false,
                    ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
                };
                pfrmMScatterResult.pChart.Series.Add(VLine);

                VLine.Points.AddXY(vecVar.Average(), vecWeightVar.Min());
                VLine.Points.AddXY(vecVar.Average(), vecWeightVar.Max());

                var HLine = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "HLine",
                    Color = System.Drawing.Color.Black,
                    BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash,
                    //BorderColor = System.Drawing.Color.Black,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = false,
                    ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
                };
                pfrmMScatterResult.pChart.Series.Add(HLine);

                HLine.Points.AddXY(vecVar.Min(), vecWeightVar.Average());
                HLine.Points.AddXY(vecVar.Max(), vecWeightVar.Average());

                var seriesLine = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "RegLine",
                    Color = System.Drawing.Color.Red,
                    //BorderColor = System.Drawing.Color.Black,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = false,
                    ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
                };

                pfrmMScatterResult.pChart.Series.Add(seriesLine);

                seriesLine.Points.AddXY(vecVar.Min(), vecVar.Min() * vecCoeff[1] + vecCoeff[0]);
                seriesLine.Points.AddXY(vecVar.Max(), vecVar.Max() * vecCoeff[1] + vecCoeff[0]);

                if (chkStd.Checked)
                {
                    pfrmMScatterResult.pChart.ChartAreas[0].AxisX.Title = "standardized " + strVarNM;
                    pfrmMScatterResult.pChart.ChartAreas[0].AxisY.Title = "Spatially lagged standardized " + strVarNM;
                    pfrmMScatterResult.lblRegression.Text = "Spatially lagged standardized " + strVarNM + " = " + vecCoeff[1].ToString("N3") + " * " + "standardized " + strVarNM;
                }
                else
                {
                    pfrmMScatterResult.pChart.ChartAreas[0].AxisX.Title = strVarNM;
                    pfrmMScatterResult.pChart.ChartAreas[0].AxisY.Title = "Spatially lagged " + strVarNM;
                    pfrmMScatterResult.lblRegression.Text = "Spatially lagged " + strVarNM + " = " + vecCoeff[1].ToString("N3") + " * " + strVarNM + " + " + vecCoeff[0].ToString("N3");
                }

                if (chkStd.Checked)
                {
                    pfrmMScatterResult.pChart.ChartAreas[0].AxisX.IsLabelAutoFit = false;
                    pfrmMScatterResult.pChart.ChartAreas[0].AxisX.CustomLabels.Clear();
                    pfrmMScatterResult.pChart.ChartAreas[0].AxisX.MajorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.None;
                    //pfrmMScatterResult.pChart.ChartAreas[0].AxisX.MajorTickMark.Interval = 1;
                    //pfrmMScatterResult.pChart.ChartAreas[0].AxisX.MajorTickMark.IntervalOffset = -2;

                    int intMin = Convert.ToInt32(Math.Floor(vecVar.Min()));
                    int intMax = Convert.ToInt32(Math.Ceiling(vecVar.Max()));
                    for (int n = intMin; n < intMax; n++)
                    {
                        System.Windows.Forms.DataVisualization.Charting.CustomLabel pcutsomLabel = new System.Windows.Forms.DataVisualization.Charting.CustomLabel();
                        pcutsomLabel.FromPosition = n - 0.5;
                        pcutsomLabel.ToPosition = n + 0.5;
                        pcutsomLabel.Text = n.ToString();
                        pfrmMScatterResult.pChart.ChartAreas[0].AxisX.CustomLabels.Add(pcutsomLabel);

                    }
                }

                pfrmMScatterResult.Show();
                pfrmProgress.Close();
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }
    }
}