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
    public partial class frmAddField : Form
    {
        private MainForm m_pForm;
        private IMapLayer m_pMaplayer;
        private DataTable m_dt = null;
        private clsSnippet m_pSnippet;


        public frmAddField()
        {
            InitializeComponent();
            m_pForm = Application.OpenForms["MainForm"] as MainForm;

            for (int i = 0; i < m_pForm.map1.Layers.Count; i++)
            {
                cboLayer.Items.Add(m_pForm.map1.Layers[i].DataSet.Name);
            }
            

            m_pSnippet = new clsSnippet();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strLayerName = cboLayer.Text;
            string strFieldType = cboType.Text;
            string strFieldName = txtName.Text;

            if (strLayerName == "" || strFieldType == "" || strFieldName == "")
                MessageBox.Show("Please select variables");

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



            // Create field, if there isn't
            if (m_dt.Columns.IndexOf(strFieldName) == -1)
            {
                //Add fields
                DataColumn pColumn = new DataColumn(strFieldName);

                switch (strFieldType)
                {
                    case "Integer":
                        pColumn.DataType = Type.GetType("System.Int32");
                        break;
                    case "Float":
                        pColumn.DataType = Type.GetType("System.Single");
                        break;
                    case "Double":
                        pColumn.DataType = Type.GetType("System.Double");
                        break;
                    case "Text":
                        pColumn.DataType = Type.GetType("System.String");
                        break;
                    case "Date":
                        pColumn.DataType = Type.GetType("System.DateTime");
                        break;
                }
                
                m_dt.Columns.Add(pColumn);
                
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

            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to overwrite " + strFieldName + " field?", "Overwrite", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

            MessageBox.Show("Done");
        }
    }
}
