using DotSpatial.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ESFTool
{
    public partial class frmQQPlotResult : Form
    {
        public MainForm m_pForm;
        public IMapLayer m_pMaplayer;
        private DataTable m_dt = null;

        //private clsBrusingLinking m_pBL;

        //Variables for QQPlot
        public Double[] adblVar1;
        public Double[] adblVar2;
        //public string strVar2Name;
        public string strVar1Name;
        public System.Drawing.Color pMakerColor;
        public string strDistribution;
        public string strDF1;
        public string strDF2;

        //Private variables
        #region Variables for Drawing Rectangle
        private int _startX, _startY;
        private bool _canDraw;
        private Rectangle _rect;
        private Graphics pGraphics;
        #endregion

        //private void pChart_MouseUp(object sender, MouseEventArgs e)
        //{
        //    //Export the chart to an image file
        //    if (e.Button == System.Windows.Forms.MouseButtons.Right)
        //    {
        //        conMenu.Show(pChart, e.X, e.Y);
        //        return;
        //    }

        //    //m_pActiveView.GraphicsContainer.DeleteAllElements(); This part is not  converted, need to be checked HK 061418

        //    if (pChart.Series[pChart.Series.Count - 1].Name == "SelPoints")
        //        pChart.Series.RemoveAt(pChart.Series.Count - 1);


        //    HitTestResult result = pChart.HitTest(e.X, e.Y);

        //    int dblOriPtsSize = pChart.Series[0].MarkerSize;
        //    _canDraw = false;

        //    System.Drawing.Color pMarkerColor = System.Drawing.Color.Cyan;
        //    var seriesPts = new System.Windows.Forms.DataVisualization.Charting.Series
        //    {
        //        Name = "SelPoints",
        //        Color = pMarkerColor,
        //        BorderColor = pMarkerColor,
        //        IsVisibleInLegend = false,
        //        IsXValueIndexed = false,
        //        ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point,
        //        MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle,
        //        MarkerSize = dblOriPtsSize * 2

        //    };

        //    pChart.Series.Add(seriesPts);

        //    StringBuilder plotCommmand = new StringBuilder();

        //    for (int i = 0; i < pChart.Series[0].Points.Count; i++)
        //    {
        //        int intX = (int)pChart.ChartAreas[0].AxisX.ValueToPixelPosition(pChart.Series[0].Points[i].XValue);
        //        int intY = (int)pChart.ChartAreas[0].AxisY.ValueToPixelPosition(pChart.Series[0].Points[i].YValues[0]);

        //        Point SelPts = new Point(intX, intY);

        //        if (_rect.Contains(SelPts))
        //        {
        //            int index = result.PointIndex;
        //            seriesPts.Points.AddXY(pChart.Series[0].Points[i].XValue, pChart.Series[0].Points[i].YValues[0]);
        //            plotCommmand.Append("["+strVar1Name + "] = " + adblVar1[i].ToString() + " OR ");
        //        }
        //    }

        //    //Brushing on ArcView
        //    if (plotCommmand.Length > 3)
        //    {
        //        plotCommmand.Remove(plotCommmand.Length - 3, 3);
        //        string whereClause = plotCommmand.ToString();
        //        FeatureSelection(whereClause, m_pMaplayer);
        //    }
        //    else
        //    {
        //        ClearSelection(m_pMaplayer);
        //    }
        //    //Brushing to other graphs //The Function should be locatated after MapView Brushing
        //    //m_pBL.BrushingToOthers(m_pFLayer, this.Handle);//
        //}

        //private void FeatureSelection(string whereClause, IMapLayer pMapLayer)
        //{

        //    if (pMapLayer is MapPointLayer)
        //    {
        //        MapPointLayer pMapPointLyr = default(MapPointLayer);
        //        pMapPointLyr = (MapPointLayer)pMapLayer;
        //        pMapPointLyr.SelectByAttribute(whereClause);
                
        //    }
        //    else if (pMapLayer is MapPolygonLayer)
        //    {
        //        MapPolygonLayer pMapPolyLyr = default(MapPolygonLayer);
        //        pMapPolyLyr = (MapPolygonLayer)pMapLayer;
        //        pMapPolyLyr.SelectByAttribute(whereClause);
        //    }
        //    else if (pMapLayer is MapLineLayer)
        //    {
        //        MapLineLayer pMapLineLyr = default(MapLineLayer);
        //        pMapLineLyr = (MapLineLayer)pMapLayer;
        //        pMapLineLyr.SelectByAttribute(whereClause);
        //    }
        //}
        //private void ClearSelection(IMapLayer pMapLayer)
        //{

        //    if (pMapLayer is MapPointLayer)
        //    {
        //        MapPointLayer pMapPointLyr = default(MapPointLayer);
        //        pMapPointLyr = (MapPointLayer)pMapLayer;
        //        pMapPointLyr.RemoveSelectedFeatures(); 
        //    }
        //    else if (pMapLayer is MapPolygonLayer)
        //    {
        //        MapPolygonLayer pMapPolyLyr = default(MapPolygonLayer);
        //        pMapPolyLyr = (MapPolygonLayer)pMapLayer;
        //        pMapPolyLyr.RemoveSelectedFeatures(); 

        //    }
        //    else if (pMapLayer is MapLineLayer)
        //    {
        //        MapLineLayer pMapLineLyr = default(MapLineLayer);
        //        pMapLineLyr = (MapLineLayer)pMapLayer;
        //        pMapLineLyr.RemoveSelectedFeatures(); 
        //    }
        //}

        //private void frmQQPlotResult_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    ClearSelection(m_pMaplayer);
        //    ////int intFCnt = m_pBL.RemoveBrushing(m_pForm, m_pFLayer);//

        //    //if (intFCnt == -1)
        //    //    return;
        //    //else if (intFCnt == 0)
        //    //{
        //    //    IFeatureSelection featureSelection = (IFeatureSelection)m_pFLayer;
        //    //    m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
        //    //    featureSelection.Clear();
        //    //    m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
        //    //}
        //    //else
        //    //    return;
        //}

        //private void pChart_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (!_canDraw) return;

        //    int x = Math.Min(_startX, e.X);
        //    int y = Math.Min(_startY, e.Y);

        //    int width = Math.Max(_startX, e.X) - Math.Min(_startX, e.X);

        //    int height = Math.Max(_startY, e.Y) - Math.Min(_startY, e.Y);
        //    _rect = new Rectangle(x, y, width, height);
        //    Refresh();

        //    Pen pen = new Pen(Color.Cyan, 1);
        //    pGraphics = pChart.CreateGraphics();
        //    pGraphics.DrawRectangle(pen, _rect);
        //    pGraphics.Dispose();
        //}

        //private void pChart_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == System.Windows.Forms.MouseButtons.Right)
        //        _canDraw = false;
        //    else
        //    {
        //        _canDraw = true;
        //        _startX = e.X;
        //        _startY = e.Y;
        //    }
        //}

        private void exportToImageFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Not applied (HK 061418)
            //frmExportChart pfrmExportChart = new frmExportChart();
            //pfrmExportChart.thisChart = pChart;
            //pfrmExportChart.nudHeight.Value = pChart.Height;
            //pfrmExportChart.nudHeight.Maximum = pChart.Height * 5; //Restriction of maximum size
            //pfrmExportChart.nudWidth.Value = pChart.Width;
            //pfrmExportChart.nudWidth.Maximum = pChart.Width * 5;
            //pfrmExportChart.Show();
        }

        public frmQQPlotResult()
        {
            InitializeComponent();
        }
    }
}
