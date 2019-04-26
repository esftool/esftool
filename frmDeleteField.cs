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

namespace ESFTool
{
    public partial class frmDeleteField : Form
    {
        private MainForm m_pForm;
        private IMapLayer m_pMaplayer;
        private DataTable m_dt = null;
        private clsSnippet m_pSnippet;

        public frmDeleteField()
        {
            InitializeComponent();
            m_pForm = Application.OpenForms["MainForm"] as MainForm;

            for (int i = 0; i < m_pForm.map1.Layers.Count; i++)
            {
                cboLayer.Items.Add(m_pForm.map1.Layers[i].DataSet.Name);
            }


            m_pSnippet = new clsSnippet();
        }

        private void cboLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strLayerName = cboLayer.Text;

                m_pMaplayer = null;
                for (int i = 0; i < m_pForm.map1.Layers.Count; i++)
                {
                    if (strLayerName == m_pForm.map1.Layers[i].DataSet.Name)
                    {
                        m_pMaplayer = m_pForm.map1.Layers[i];
                    }
                }

                if (m_pMaplayer is MapPointLayer)
                {
                    MapPointLayer pMapPointLyr = default(MapPointLayer);
                    pMapPointLyr = (MapPointLayer)m_pMaplayer;
                    m_dt = pMapPointLyr.DataSet.DataTable;
                }
                else if (m_pMaplayer is MapPolygonLayer)
                {
                    MapPolygonLayer pMapPolyLyr = default(MapPolygonLayer);
                    pMapPolyLyr = (MapPolygonLayer)m_pMaplayer;
                    m_dt = pMapPolyLyr.DataSet.DataTable;
                }
                else if (m_pMaplayer is MapLineLayer)
                {
                    MapLineLayer pMapLineLyr = default(MapLineLayer);
                    pMapLineLyr = (MapLineLayer)m_pMaplayer;
                    m_dt = pMapLineLyr.DataSet.DataTable;
                }

                clistFields.Items.Clear();

                for (int i = 0; i < m_dt.Columns.Count; i++) //No restriction for a field type
                {
                    clistFields.Items.Add(m_dt.Columns[i].ColumnName);
                }
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clistFields.Items.Count; i++)
                clistFields.SetItemChecked(i, true);
        }

        private void btnUnselect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clistFields.Items.Count; i++)
                clistFields.SetItemChecked(i, false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (clistFields.CheckedItems.Count > 0)
            {
                for (int i = 0; i < clistFields.CheckedItems.Count; i++)
                {
                    DeleteField(m_dt, (string)clistFields.CheckedItems[i]);
                }
            }
            else
                MessageBox.Show("Select Fields to delete");

            if (m_pMaplayer is MapPointLayer)
            {
                MapPointLayer pMapPointLyr = default(MapPointLayer);
                pMapPointLyr = (MapPointLayer)m_pMaplayer;
                pMapPointLyr.DataSet.Save();
            }
            else if (m_pMaplayer is MapPolygonLayer)
            {
                MapPolygonLayer pMapPolyLyr = default(MapPolygonLayer);
                pMapPolyLyr = (MapPolygonLayer)m_pMaplayer;
                pMapPolyLyr.DataSet.Save();
            }
            else if (m_pMaplayer is MapLineLayer)
            {
                MapLineLayer pMapLineLyr = default(MapLineLayer);
                pMapLineLyr = (MapLineLayer)m_pMaplayer;
                pMapLineLyr.DataSet.Save();
            }

            clistFields.Items.Clear(); //Reload

            for (int i = 0; i < m_dt.Columns.Count; i++) 
            {
                clistFields.Items.Add(m_dt.Columns[i].ColumnName);
            }

            MessageBox.Show("Done");
        }
        private void DeleteField(DataTable dt, string fieldName)
        {
            try
            {
                int intIdx = dt.Columns.IndexOf(fieldName);
                dt.Columns.RemoveAt(intIdx);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception:" + ex.Message);
                return;
            }
        }
    }
}
