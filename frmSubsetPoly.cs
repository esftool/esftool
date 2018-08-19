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
    public partial class frmSubsetPoly : Form
    {
        private MainForm m_pForm;
        public IMapLayer m_pMaplayer; //Clipped polygon
        private clsSnippet m_pSnippet;
        public bool m_blnSubset;

        //public IFeatureLayer m_pClipPolygon;
        public frmSubsetPoly()
        {
            InitializeComponent();
            m_pForm = Application.OpenForms["MainForm"] as MainForm;

            for (int i = 0; i < m_pForm.map1.Layers.Count; i++)
            {
                if(m_pForm.map1.Layers[i] is IMapPolygonLayer)
                    cboTargetLayer.Items.Add(m_pForm.map1.Layers[i].DataSet.Name);
            }

            m_pSnippet = new clsSnippet();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            m_blnSubset = false;

            m_blnSubset = true;
            //pfrmProgress.Close();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboTargetLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }
    }
}
