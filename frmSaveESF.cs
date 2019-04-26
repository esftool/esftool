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
    public partial class frmSaveESF : Form
    {
        public Double[,] arrSelectedEVs;
        public String[] arrSelectedEvsNM;
        public IMapLayer m_pMaplayer;

        private int intNSelectedEVs;

        public frmSaveESF()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSaveESF_Load(object sender, EventArgs e)
        {
            intNSelectedEVs = arrSelectedEvsNM.Length;


            for (int i = 0; i < intNSelectedEVs; i++)
            {
                clistFields.Items.Add(arrSelectedEvsNM[i]);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (clistFields.CheckedItems.Count == 0)
                {
                    MessageBox.Show("EV is not selected.");
                    return;
                }


                frmProgress pfrmProgress = new frmProgress();
                pfrmProgress.lblStatus.Text = "Saving EVs:"; ;
                pfrmProgress.pgbProgress.Style = ProgressBarStyle.Marquee;
                pfrmProgress.Show();

                pfrmProgress.lblStatus.Text = "Saving residuals and spatial filter:";

                //Get Data Table
                DataTable pDT = null;
                if (m_pMaplayer is MapPointLayer)
                {
                    MapPointLayer pMapPointLyr = default(MapPointLayer);
                    pMapPointLyr = (MapPointLayer)m_pMaplayer;
                    pDT = pMapPointLyr.DataSet.DataTable;
                }
                else if (m_pMaplayer is MapPolygonLayer)
                {
                    MapPolygonLayer pMapPolyLyr = default(MapPolygonLayer);
                    pMapPolyLyr = (MapPolygonLayer)m_pMaplayer;
                    pDT = pMapPolyLyr.DataSet.DataTable;
                }


                for (int k = 0; k < clistFields.CheckedItems.Count; k++)
                {
                    int i = clistFields.CheckedIndices[k];
                    string strEVfieldName = arrSelectedEvsNM[i];
                    pfrmProgress.lblStatus.Text = "Saving EV (" + strEVfieldName + ")";

                    // Create field, if there isn't
                    if (pDT.Columns.IndexOf(strEVfieldName) == -1)
                    {
                        //Add fields
                        DataColumn pColumn = new DataColumn(strEVfieldName);
                        pColumn.DataType = Type.GetType("System.Double");
                        pDT.Columns.Add(pColumn);
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Do you want to overwrite " + strEVfieldName + " field?", "Overwrite", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.No)
                        {
                            return;
                        }
                    }

                    //Update Field
                    int featureIdx = 0;
                    int intValueIdx = pDT.Columns.IndexOf(strEVfieldName);

                    foreach (DataRow row in pDT.Rows)
                    {
                        //Update Residuals
                        row[intValueIdx] = arrSelectedEVs[featureIdx, i];
                        featureIdx++;
                    }

                }
                
                //Save Result;
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
                pfrmProgress.Close();
                MessageBox.Show("Complete. The results are stored in the shape file");

            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }
    }
}
