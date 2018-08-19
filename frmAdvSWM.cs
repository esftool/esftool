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
    public partial class frmAdvSWM : Form
    {
        public IMapLayer m_pMapLayer;
        public bool blnCorrelogram; //For removing higher order option for correlogram

        private clsSnippet m_pSnippet;

        private MainForm m_pForm;
        //private IActiveView m_pActiveView;
        private REngine m_pEngine;

        //Varaibles for SWM
        private IMapLayer m_pClippedPolygon;
        private bool m_blnSubset = false;

        //Output
        public bool blnSWMCreation = false;


        public frmAdvSWM()
        {
            InitializeComponent();
            m_pSnippet = new clsSnippet();
            m_pForm = Application.OpenForms["MainForm"] as MainForm;
            m_pEngine = m_pForm.pEngine;
        }

        private void txtSWM_TextChanged(object sender, EventArgs e)
        {
            try
            {

                clsSnippet.SpatialWeightMatrixType pSWMType = new clsSnippet.SpatialWeightMatrixType();

                if (txtSWM.Text == pSWMType.strPointDef[0])
                {

                    lblClip.Visible = true; lblClip.Enabled = true;
                    btnSubset.Visible = true; btnSubset.Enabled = true;
                    chkCumulate.Visible = false;
                    lblAdvanced.Text = "Threshold distance:";
                    lblAdvanced.Enabled = false;
                    nudAdvanced.Value = 100; nudAdvanced.Increment = 10; nudAdvanced.DecimalPlaces = 2;
                    nudAdvanced.Enabled = false;

                }
                else if (txtSWM.Text == pSWMType.strPointDef[1])
                {
                    lblClip.Visible = true; btnSubset.Visible = true; btnSubset.Enabled = true;
                    chkCumulate.Visible = false;
                    lblAdvanced.Text = "Threshold distance:";
                    lblAdvanced.Enabled = true;
                    nudAdvanced.Value = 100; nudAdvanced.Increment = 10; nudAdvanced.DecimalPlaces = 2;
                    nudAdvanced.Enabled = true;
                    btnSubset.Enabled = false; btnSubset.Visible = false;
                    lblClip.Enabled = false; lblClip.Visible = false;
                    //if (m_dblDefaultDistThres != null)
                    //    nudAdvanced.Value = Convert.ToDecimal(m_dblDefaultDistThres);
                }
                else if (txtSWM.Text == pSWMType.strPointDef[2])
                {
                    lblClip.Visible = true; btnSubset.Visible = true; btnSubset.Enabled = true;
                    chkCumulate.Visible = false;
                    lblAdvanced.Text = "Number of neighbors:";
                    lblAdvanced.Enabled = true;
                    nudAdvanced.Value = 4; nudAdvanced.Increment = 1; nudAdvanced.DecimalPlaces = 0;
                    nudAdvanced.Enabled = true;
                    btnSubset.Enabled = false; btnSubset.Visible = false;
                    lblClip.Enabled = false; lblClip.Visible = false;
                }
                else
                {
                    lblClip.Visible = false; btnSubset.Visible = false; btnSubset.Enabled = false;
                    chkCumulate.Enabled = false; chkCumulate.Visible = true;
                    lblAdvanced.Enabled = true; lblAdvanced.Text = "Contiguity Order:";
                    nudAdvanced.Enabled = true; nudAdvanced.Value = 1; nudAdvanced.Increment = 1; nudAdvanced.DecimalPlaces = 0;

                    int intT = pSWMType.strPolyDefs.Length;
                    for (int i = 0; i < intT; i++)
                    {
                        if (txtSWM.Text == pSWMType.strPolyDefs[i])
                            return;
                    }
                    btnSubset.Enabled = false;
                    chkCumulate.Enabled = false;
                    lblAdvanced.Enabled = false;
                    nudAdvanced.Enabled = false;
                    lblClip.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void nudAdvanced_ValueChanged(object sender, EventArgs e)
        {
            if (m_pMapLayer == null)
                return;

            if (m_pMapLayer is MapPolygonLayer)
            {
                if (nudAdvanced.Value > 1)
                {
                    chkCumulate.Enabled = true;
                }
                else if (nudAdvanced.Value == 1)
                    chkCumulate.Enabled = false;
            }
        }

        private void frmAdvSWM_Load(object sender, EventArgs e)
        {
            try
            {

                //New Spatial Weight matrix function 083117 HK
                clsSnippet.SpatialWeightMatrixType pSWMType = new clsSnippet.SpatialWeightMatrixType();
                if (m_pMapLayer is MapPolygonLayer) //Apply Different Spatial weight matrix according to geometry type 05/24/18 HK
                {
                    //txtSWM.Text = pSWMType.strPolySWM;
                    txtSWM.DataSource = pSWMType.strPolyDefs;

                    lblClip.Visible = false; btnSubset.Visible = false; btnSubset.Enabled = false;
                    chkCumulate.Visible = true; chkCumulate.Text = "Cumulate neighbors:";
                    lblAdvanced.Text = "Contiguity Order:";
                    nudAdvanced.Value = 1; nudAdvanced.Increment = 1; nudAdvanced.DecimalPlaces = 0;

                    if (blnCorrelogram)
                    {
                        chkCumulate.Visible = false; nudAdvanced.Visible = false;
                        lblAdvanced.Visible = false;
                    }
                }
                else if (m_pMapLayer is MapPointLayer)
                {
                    //txtSWM.Text = pSWMType.strPointSWM;
                    txtSWM.DataSource = pSWMType.strPointDef;
                    lblClip.Visible = true; btnSubset.Visible = true; btnSubset.Enabled = true;
                    chkCumulate.Visible = false;
                    lblAdvanced.Text = "Threshold distance:";
                    nudAdvanced.Value = 100; nudAdvanced.Increment = 10; nudAdvanced.DecimalPlaces = 2;
                }
                else if (m_pMapLayer is MapLineLayer)
                {
                    MessageBox.Show("Spatial weight matrix for polyline is not supported.");
                    btnSubset.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Please check the type of the shapefile.");
                    btnSubset.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void btnSubset_Click(object sender, EventArgs e)
        {
            try
            {

                frmSubsetPoly pfrmSubsetPoly = new frmSubsetPoly();
                pfrmSubsetPoly.ShowDialog();
                m_blnSubset = pfrmSubsetPoly.m_blnSubset;
                if (m_blnSubset)
                {
                    m_pClippedPolygon = pfrmSubsetPoly.m_pMaplayer; ;
                    chkCumulate.Visible = true;
                    chkCumulate.Text = "Clipped by '" + m_pClippedPolygon.DataSet.Name + "'";
                    chkCumulate.Checked = true;
                    chkCumulate.Enabled = true;
                }
                else
                {
                    chkCumulate.Visible = false;
                }
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {

                frmProgress pfrmProgress = new frmProgress();
                pfrmProgress.lblStatus.Text = "Processing:";
                pfrmProgress.pgbProgress.Style = ProgressBarStyle.Marquee;
                pfrmProgress.Show();

                //Get the file path and name to create spatial weight matrix
                string strNameR = m_pSnippet.FilePathinRfromLayer(m_pMapLayer);

                if (strNameR == null)
                    return;

                int intSuccess = 0;

                //Create spatial weight matrix in R
                if (m_pMapLayer is MapPolygonLayer)
                {
                    m_pEngine.Evaluate("sample.shp <- readShapePoly('" + strNameR + "')");
                    intSuccess = m_pSnippet.CreateSpatialWeightMatrixPoly(m_pEngine, m_pMapLayer, txtSWM.Text, pfrmProgress, Convert.ToDouble(nudAdvanced.Value), chkCumulate.Checked);

                }
                else if (m_pMapLayer is MapPointLayer)
                {
                    m_pEngine.Evaluate("sample.shp <- readShapePoints('" + strNameR + "')");
                    intSuccess = m_pSnippet.CreateSpatialWeightMatrixPts(m_pEngine, m_pMapLayer, txtSWM.Text, pfrmProgress, Convert.ToDouble(nudAdvanced.Value), chkCumulate.Checked);

                    //chkCumulate.Visible = false;
                }
                else
                {
                    MessageBox.Show("This geometry type is not supported");
                    pfrmProgress.Close();
                    this.Close();
                }

                if (intSuccess == 0)
                    return;

                blnSWMCreation = true;
                pfrmProgress.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void btnOpenSWM_Click(object sender, EventArgs e)
        {
            if (ofdOpenSWM.ShowDialog() == DialogResult.OK)
                txtSWM.Text = string.Concat(ofdOpenSWM.FileName);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
