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
    public partial class frmRegression : Form
    {
        private MainForm m_pForm;
        private REngine m_pEngine;
        private IMapLayer m_pMaplayer;
        private DataTable m_dt = null;
        private clsSnippet m_pSnippet;
        //Varaibles for SWM
        private bool m_blnCreateSWM = false;

        public frmRegression()
        {
            try
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
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void cboTargetLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strLayerName = cboTargetLayer.Text;

                if (strLayerName == "")
                    return;

                m_pMaplayer = null;
                for (int i = 0; i < m_pForm.map1.Layers.Count; i++)
                {
                    if (strLayerName == m_pForm.map1.Layers[i].DataSet.Name)
                    {
                        m_pMaplayer = m_pForm.map1.Layers[i];
                    }
                }

                groupBox1.Enabled = true;

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
                    MessageBox.Show("Spatial weight matrix for polyline is not supported.");
                    groupBox1.Enabled = false;
                    chkResiAuto.Checked = false;
                }

                cboFieldName.Text = "";
                lstFields.Items.Clear();
                lstIndeVar.Items.Clear();

                for (int i = 0; i < m_dt.Columns.Count; i++)
                {
                    if (m_pSnippet.FindNumberFieldType(m_dt.Columns[i]))
                    {
                        lstFields.Items.Add(m_dt.Columns[i].ColumnName);
                        cboFieldName.Items.Add(m_dt.Columns[i].ColumnName);
                    }

                }

                if (chkSave.Checked)
                    UpdateListview(lstSave, m_dt);
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }
        private void UpdateListview(ListView pListView, DataTable dt)
        {
            try
            {
                pListView.BeginUpdate();
                string strResiFldNm = "lin_resi";

                //Update Name Using the UpdateFldName Function

                strResiFldNm = UpdateFldName(strResiFldNm, dt);

                if (strResiFldNm == null)
                    return;

                pListView.Items[0].SubItems[1].Text = strResiFldNm;
                pListView.EndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception:" + ex.Message);
                return;
            }

        }
        private string UpdateFldName(string strFldNM, DataTable dt)
        {
            try
            {
                string returnNM = strFldNM;
                int i = 1;
                while (dt.Columns.IndexOf(returnNM) != -1)
                {
                    returnNM = strFldNM + "_" + i.ToString();
                    i++;
                }
                return returnNM;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception:" + ex.Message);
                return null;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            m_pSnippet.MoveSelectedItemsinListBoxtoOtherListBox(lstFields, lstIndeVar);
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            m_pSnippet.MoveSelectedItemsinListBoxtoOtherListBox(lstFields, lstIndeVar);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboFieldName.Text == "")
                {
                    MessageBox.Show("Please select the dependent input variables to be used in the regression model.",
                        "Please choose at least one input variable");
                    return;
                }
                if (lstIndeVar.Items.Count == 0 && chkIntercept.Checked == false)
                {
                    MessageBox.Show("Please select independents input variables to be used in the regression model.",
                        "Please choose at least one input variable");
                    return;
                }

                frmProgress pfrmProgress = new frmProgress();
                pfrmProgress.lblStatus.Text = "Pre-Processing:";
                pfrmProgress.pgbProgress.Style = ProgressBarStyle.Marquee;
                pfrmProgress.Show();


                //Decimal places
                int intDeciPlaces = 5;

                //Get number of Independent variables            
                int nIDepen = lstIndeVar.Items.Count;
                if (chkIntercept.Checked)
                    nIDepen = 0;

                // Gets the column of the dependent variable
                String dependentName = (string)cboFieldName.SelectedItem;
                //sourceTable.AcceptChanges();
                //DataTable dependent = sourceTable.DefaultView.ToTable(false, dependentName);

                // Gets the columns of the independent variables
                String[] independentNames = new string[nIDepen];
                for (int j = 0; j < nIDepen; j++)
                {
                    independentNames[j] = (string)lstIndeVar.Items[j];
                }

                int nFeature = m_dt.Rows.Count;

                //Get index for independent and dependent variables
                int intDepenIdx = m_dt.Columns.IndexOf(dependentName);

                int[] idxes = new int[nIDepen];

                for (int j = 0; j < nIDepen; j++)
                {
                    idxes[j] = m_dt.Columns.IndexOf(independentNames[j]);
                }

                //Store independent values at Array
                double[] arrDepen = new double[nFeature];
                double[][] arrInDepen = new double[nIDepen][];

                for (int j = 0; j < nIDepen; j++)
                {
                    arrInDepen[j] = new double[nFeature];
                }

                int i = 0;
                foreach (DataRow row in m_dt.Rows)
                {

                    arrDepen[i] = Convert.ToDouble(row[intDepenIdx]);

                    for (int j = 0; j < nIDepen; j++)
                    {
                        arrInDepen[j][i] = Convert.ToDouble(row[idxes[j]]);
                    }
                    i++;
                }

                //Plot command for R
                StringBuilder plotCommmand = new StringBuilder();

                //Dependent variable to R vector
                NumericVector vecDepen = m_pEngine.CreateNumericVector(arrDepen);
                m_pEngine.SetSymbol(dependentName, vecDepen);
                plotCommmand.Append("lm(" + dependentName + "~");

                if (chkIntercept.Checked == false)
                {
                    for (int j = 0; j < nIDepen; j++)
                    {
                        //double[] arrVector = arrInDepen.GetColumn<double>(j);
                        NumericVector vecIndepen = m_pEngine.CreateNumericVector(arrInDepen[j]);
                        m_pEngine.SetSymbol(independentNames[j], vecIndepen);
                        plotCommmand.Append(independentNames[j] + "+");
                    }
                    plotCommmand.Remove(plotCommmand.Length - 1, 1);
                }
                else
                    plotCommmand.Append("1");

                plotCommmand.Append(")");
                m_pEngine.Evaluate("sum.lm <- summary(" + plotCommmand + ")");

                NumericMatrix matCoe = m_pEngine.Evaluate("as.matrix(sum.lm$coefficient)").AsNumericMatrix();
                NumericVector vecF = m_pEngine.Evaluate("as.numeric(sum.lm$fstatistic)").AsNumeric();
                m_pEngine.Evaluate("fvalue <- as.numeric(sum.lm$fstatistic)");
                double dblPValueF = m_pEngine.Evaluate("pf(fvalue[1],fvalue[2],fvalue[3],lower.tail=F)").AsNumeric().First();
                double dblRsqaure = m_pEngine.Evaluate("sum.lm$r.squared").AsNumeric().First();
                double dblAdjRsqaure = m_pEngine.Evaluate("sum.lm$adj.r.squared").AsNumeric().First();
                double dblResiSE = m_pEngine.Evaluate("sum.lm$sigma").AsNumeric().First();
                NumericVector vecResiDF = m_pEngine.Evaluate("sum.lm$df").AsNumeric();

                double dblResiMC = 0;
                double dblResiMCpVal = 0;

                if (chkResiAuto.Checked)
                {
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


                    m_pEngine.Evaluate("sample.n <- length(sample.nb)");

                    //Calculate MC
                    m_pEngine.Evaluate("zmc <- lm.morantest(" + plotCommmand.ToString() + ", listw=sample.listw, zero.policy=TRUE)");
                    dblResiMC = m_pEngine.Evaluate("zmc$estimate[1]").AsNumeric().First();
                    dblResiMCpVal = m_pEngine.Evaluate("zmc$p.value").AsNumeric().First();
                }

                pfrmProgress.lblStatus.Text = "Printing Output:";
                //Open Ouput form
                frmRegResult pfrmRegResult = new frmRegResult();
                pfrmRegResult.Text = "Linear Regression Summary";

                //Create DataTable to store Result
                System.Data.DataTable tblRegResult = new DataTable("OLSResult");

                //Assign DataTable
                DataColumn dColName = new DataColumn();
                dColName.DataType = System.Type.GetType("System.String");
                dColName.ColumnName = "Name";
                tblRegResult.Columns.Add(dColName);

                DataColumn dColValue = new DataColumn();
                dColValue.DataType = System.Type.GetType("System.Double");
                dColValue.ColumnName = "Estimate";
                tblRegResult.Columns.Add(dColValue);

                DataColumn dColSE = new DataColumn();
                dColSE.DataType = System.Type.GetType("System.Double");
                dColSE.ColumnName = "Std. Error";
                tblRegResult.Columns.Add(dColSE);

                DataColumn dColTValue = new DataColumn();
                dColTValue.DataType = System.Type.GetType("System.Double");
                dColTValue.ColumnName = "t value";
                tblRegResult.Columns.Add(dColTValue);

                DataColumn dColPvT = new DataColumn();
                dColPvT.DataType = System.Type.GetType("System.Double");
                dColPvT.ColumnName = "Pr(>|t|)";
                tblRegResult.Columns.Add(dColPvT);

                //Store Data Table by R result
                for (int j = 0; j < nIDepen + 1; j++)
                {
                    DataRow pDataRow = tblRegResult.NewRow();
                    if (j == 0)
                    {
                        pDataRow["Name"] = "(Intercept)";
                    }
                    else
                    {
                        pDataRow["Name"] = independentNames[j - 1];
                    }
                    pDataRow["Estimate"] = Math.Round(matCoe[j, 0], intDeciPlaces);
                    pDataRow["Std. Error"] = Math.Round(matCoe[j, 1], intDeciPlaces);
                    pDataRow["t value"] = Math.Round(matCoe[j, 2], intDeciPlaces);
                    pDataRow["Pr(>|t|)"] = Math.Round(matCoe[j, 3], intDeciPlaces);
                    tblRegResult.Rows.Add(pDataRow);
                }

                //Assign Datagridview to Data Table
                pfrmRegResult.dgvResults.DataSource = tblRegResult;

                //Assign values at Textbox
                string[] strResults = null;
                if (chkResiAuto.Checked)
                {
                    strResults = new string[4];
                    strResults[3] = "MC of residuals: " + dblResiMC.ToString("N3") + ", p-value: " + dblResiMCpVal.ToString("N3");
                }
                else
                    strResults = new string[3];

                strResults[0] = "Residual standard error: " + dblResiSE.ToString("N" + intDeciPlaces.ToString()) +
                        " on " + vecResiDF[1].ToString() + " degrees of freedom";
                strResults[1] = "Multiple R-squared: " + dblRsqaure.ToString("N" + intDeciPlaces.ToString()) +
                    ", Adjusted R-squared: " + dblAdjRsqaure.ToString("N" + intDeciPlaces.ToString());
                if (chkIntercept.Checked)
                    strResults[2] = "";
                else
                {
                    strResults[2] = "F-Statistic: " + vecF[0].ToString("N" + intDeciPlaces.ToString()) +
                                        " on " + vecF[1].ToString() + " and " + vecF[2].ToString() + " DF, p-value: " + dblPValueF.ToString("N" + intDeciPlaces.ToString());
                }
                pfrmRegResult.txtOutput.Lines = strResults;
                pfrmRegResult.Show();

                //Create Plots for Regression
                if (chkPlots.Checked)
                {
                    string strTitle = "Linear Regression Results";
                    string strCommand = "plot(" + plotCommmand + ");";

                    m_pSnippet.drawPlottoForm(strTitle, strCommand);

                }

                //Save Outputs in SHP
                if (chkSave.Checked)
                {
                    //The field names are related with string[] DeterminedName in clsSnippet 
                    string strResiFldName = lstSave.Items[0].SubItems[1].Text;

                    //Get EVs and residuals
                    NumericVector nvResiduals = m_pEngine.Evaluate("as.numeric(sum.lm$residuals)").AsNumeric();

                    // Create field, if there isn't
                    // Create field, if there isn't
                    if (m_dt.Columns.IndexOf(strResiFldName) == -1)
                    {
                        //Add fields
                        DataColumn pColumn = new DataColumn(strResiFldName);
                        pColumn.DataType = Type.GetType("System.Double");
                        m_dt.Columns.Add(pColumn);
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Do you want to overwrite " + strResiFldName + " field?", "Overwrite", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.No)
                        {
                            return;
                        }
                    }

                    //Update Field
                    int featureIdx = 0;
                    int intResiFldIdx = m_dt.Columns.IndexOf(strResiFldName);

                    foreach (DataRow row in m_dt.Rows)
                    {
                        //Update Residuals
                        row[intResiFldIdx] = nvResiduals[featureIdx];
                        featureIdx++;
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
                    else if (m_pMaplayer is MapLineLayer)
                    {
                        MapLineLayer pMapLineLyr = default(MapLineLayer);
                        pMapLineLyr = (MapLineLayer)m_pMaplayer;
                        pMapLineLyr.DataSet.Save();
                    }

                    MessageBox.Show("Residuals are stored in the shape file");
                }

                pfrmProgress.Close();
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void lstFields_DoubleClick(object sender, EventArgs e)
        {
            m_pSnippet.MoveSelectedItemsinListBoxtoOtherListBox(lstFields, lstIndeVar);
        }

        private void lstIndeVar_DoubleClick(object sender, EventArgs e)
        {
            m_pSnippet.MoveSelectedItemsinListBoxtoOtherListBox(lstIndeVar, lstFields);
        }

        private void chkSave_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSave.Checked)
            {
                if (m_dt != null)
                {
                    UpdateListview(lstSave, m_dt);
                    lstSave.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Select a target layer first");

                    chkSave.Checked = false;
                    return;
                }
            }
            else
                lstSave.Enabled = false;
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

        private void chkResiAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkResiAuto.Checked)
            {
                lblSWM.Enabled = true;
                txtSWM.Enabled = true;
                btnOpenSWM.Enabled = true;
            }
            else
            {
                lblSWM.Enabled = false;
                txtSWM.Enabled = false;
                btnOpenSWM.Enabled = false;
            }
        }

        private void chkIntercept_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIntercept.Checked)
            {
                lstFields.Enabled = false;
                lstIndeVar.Enabled = false;
                btnMoveLeft.Enabled = false;
                btnMoveRight.Enabled = false;
            }
            else
            {
                lstFields.Enabled = false;
                lstIndeVar.Enabled = false;
                btnMoveLeft.Enabled = false;
                btnMoveRight.Enabled = false;
            }
        }
    }
}
