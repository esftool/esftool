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
    public partial class frmSpatialRegression : Form
    {
        private MainForm m_pForm;
        private REngine m_pEngine;
        private IMapLayer m_pMaplayer;
        private DataTable m_dt = null;
        private clsSnippet m_pSnippet;
        //Varaibles for SWM
        private bool m_blnCreateSWM = false;

        public frmSpatialRegression()
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
                    return;
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

                //Add intercept in the listview for independent variables
                lstIndeVar.Items.Add("Intercept");

            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }
        #region FindNumberFld, and Update listview
        private bool FindNumberFieldType(DataColumn column)
        {
            try
            {
                bool blnNField = true;
                if (column.DataType == Type.GetType("System.String") || column.DataType == Type.GetType("System.Boolean"))
                    blnNField = false;

                return blnNField;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception:" + ex.Message);
                return false;
            }
        }
        private void UpdateListview(ListView pListView, DataTable dt)
        {
            try
            {
                pListView.BeginUpdate();
                string strResiFldNm = "spr_resi";

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

        #endregion
        private void lstIndeVar_DoubleClick(object sender, EventArgs e)
        {
            m_pSnippet.MoveSelectedItemsinListBoxtoOtherListBox(lstIndeVar, lstFields);
        }

        private void lstFields_DoubleClick(object sender, EventArgs e)
        {
            m_pSnippet.MoveSelectedItemsinListBoxtoOtherListBox(lstFields, lstIndeVar);
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
            m_pSnippet.MoveSelectedItemsinListBoxtoOtherListBox(lstIndeVar, lstFields);
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
                if (lstIndeVar.Items.Count == 0)
                {
                    MessageBox.Show("Please select independents input variables to be used in the regression model.",
                        "Please choose at least one input variable");
                    return;
                }
                if (cboFieldName.Text == "")
                {
                    MessageBox.Show("Please select the dependent input variables to be used in the regression model.",
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
                int nIndevarlistCnt = lstIndeVar.Items.Count;
                //Indicate an intercept only model (2) or a non-intercept model (1) or not (0)
                int intInterceptModel = 1;
                for (int j = 0; j < nIndevarlistCnt; j++)
                {
                    if ((string)lstIndeVar.Items[j] == "Intercept")
                        intInterceptModel = 0;
                }
                if (nIndevarlistCnt == 1 && intInterceptModel == 0)
                    intInterceptModel = 2;

                int nIDepen = 0;
                if (intInterceptModel == 0)
                    nIDepen = nIndevarlistCnt - 1;
                else if (intInterceptModel == 1)
                    nIDepen = nIndevarlistCnt;

                // Gets the column of the dependent variable
                String dependentName = (string)cboFieldName.SelectedItem;
                //sourceTable.AcceptChanges();
                //DataTable dependent = sourceTable.DefaultView.ToTable(false, dependentName);

                // Gets the columns of the independent variables
                String[] independentNames = new string[nIDepen];
                int intIdices = 0;
                string strIndependentName = "";
                for (int j = 0; j < nIndevarlistCnt; j++)
                {
                    strIndependentName = (string)lstIndeVar.Items[j];
                    if (strIndependentName != "Intercept")
                    {
                        independentNames[intIdices] = strIndependentName;
                        intIdices++;
                    }
                }

                int nFeature = m_dt.Rows.Count;

                //Warning for method
                if (rbtEigen.Checked)
                {
                    if (nFeature > m_pForm.intWarningCount)
                    {

                        DialogResult dialogResult = MessageBox.Show("It might take a lot of time. Do you want to continue?", "Warning", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.No)
                        {
                            pfrmProgress.Close();
                            return;
                        }
                    }
                }

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

                //Dependent variable to R vector
                NumericVector vecDepen = m_pEngine.CreateNumericVector(arrDepen);
                m_pEngine.SetSymbol(dependentName, vecDepen);
                if (rbtError.Checked)
                    plotCommmand.Append("errorsarlm(" + dependentName + "~");
                else if (rbtLag.Checked || rbtDurbin.Checked)
                    plotCommmand.Append("lagsarlm(" + dependentName + "~");
                else
                    plotCommmand.Append("spautolm(" + dependentName + "~");

                if (intInterceptModel == 2)
                {
                    plotCommmand.Append("1");
                }
                else
                {
                    for (int j = 0; j < nIDepen; j++)
                    {
                        //double[] arrVector = arrInDepen.GetColumn<double>(j);
                        NumericVector vecIndepen = m_pEngine.CreateNumericVector(arrInDepen[j]);
                        m_pEngine.SetSymbol(independentNames[j], vecIndepen);
                        plotCommmand.Append(independentNames[j] + "+");
                    }
                    plotCommmand.Remove(plotCommmand.Length - 1, 1);

                    if (intInterceptModel == 1)
                        plotCommmand.Append("-1");
                }

                //Select Method
                if (rbtEigen.Checked)
                    plotCommmand.Append(", method='eigen'");
                else if (rbtMatrix.Checked)
                    plotCommmand.Append(", method='Matrix'");
                else if (rbtMatrixJ.Checked)
                    plotCommmand.Append(", method='Matrix_J'");
                else if (rbtLU.Checked)
                    plotCommmand.Append(", method='LU'");
                else if (rbtChebyshev.Checked)
                    plotCommmand.Append(", method='Chebyshev'");
                else if (rbtMC.Checked)
                    plotCommmand.Append(", method='MC'");
                else
                    plotCommmand.Append(", method='eigen'");

                if (rbtError.Checked)
                    plotCommmand.Append(", listw=sample.listw,  tol.solve=1.0e-20, zero.policy=TRUE)");
                else if (rbtLag.Checked)
                    plotCommmand.Append(", listw=sample.listw,  tol.solve=1.0e-20, zero.policy=TRUE)");
                else if (rbtCAR.Checked)
                    plotCommmand.Append(", listw=sample.listw, family='CAR', verbose=TRUE, zero.policy=TRUE)");
                else if (rbtSMA.Checked)
                    plotCommmand.Append(", listw=sample.listw, family='SMA', verbose=TRUE, zero.policy=TRUE)");
                else if (rbtDurbin.Checked)
                    plotCommmand.Append(", type='mixed', listw=sample.listw,  tol.solve=1.0e-20, zero.policy=TRUE)");
                else
                    return;

                try
                {
                    m_pEngine.Evaluate("sum.lm <- summary(" + plotCommmand.ToString() + ", Nagelkerke=T)");
                }
                catch
                {
                    MessageBox.Show("Cannot solve the regression. Try again with different variables.");
                    pfrmProgress.Close();
                    return;
                }

                //Collect results from R
                NumericMatrix matCoe = m_pEngine.Evaluate("as.matrix(sum.lm$Coef)").AsNumericMatrix();

                double dblLRLambda = m_pEngine.Evaluate("as.numeric(sum.lm$LR1$statistic)").AsNumeric().First();
                double dblpLambda = m_pEngine.Evaluate("as.numeric(sum.lm$LR1$p.value)").AsNumeric().First();

                double dblLRErrorModel = m_pEngine.Evaluate("as.numeric(sum.lm$LR1$estimate)").AsNumeric().First();

                double dblSigmasquared = 0;
                double dblAIC = 0;
                double dblWald = 0;
                double dblpWald = 0;

                if (rbtLag.Checked || rbtError.Checked || rbtDurbin.Checked)
                {
                    dblSigmasquared = m_pEngine.Evaluate("as.numeric(sum.lm$s2)").AsNumeric().First();
                    //dblAIC = pEngine.Evaluate("as.numeric(sum.lm$AIC_lm.model)").AsNumeric().First();
                    dblWald = m_pEngine.Evaluate("as.numeric(sum.lm$Wald1$statistic)").AsNumeric().First();
                    dblpWald = m_pEngine.Evaluate("as.numeric(sum.lm$Wald1$p.value)").AsNumeric().First();

                    double dblParaCnt = m_pEngine.Evaluate("as.numeric(sum.lm$parameters)").AsNumeric().First();
                    dblAIC = (2 * dblParaCnt) - (2 * dblLRErrorModel);

                }
                else
                {
                    dblSigmasquared = m_pEngine.Evaluate("as.numeric(sum.lm$fit$s2)").AsNumeric().First();
                    double dblParaCnt = m_pEngine.Evaluate("as.numeric(sum.lm$parameters)").AsNumeric().First();
                    dblAIC = (2 * dblParaCnt) - (2 * dblLRErrorModel);
                }

                
                double dblLambda = 0;
                double dblSELambda = 0;
                double dblResiAuto = 0;
                double dblResiAutoP = 0;


                if (rbtLag.Checked || rbtDurbin.Checked)
                {
                    dblLambda = m_pEngine.Evaluate("as.numeric(sum.lm$rho)").AsNumeric().First();
                    dblSELambda = m_pEngine.Evaluate("as.numeric(sum.lm$rho.se)").AsNumeric().First();
                    dblResiAuto = m_pEngine.Evaluate("as.numeric(sum.lm$LMtest)").AsNumeric().First();
                    dblResiAutoP = m_pEngine.Evaluate("as.numeric(sum.lm$rho.se)").AsNumeric().First();
                }
                else
                {
                    dblLambda = m_pEngine.Evaluate("as.numeric(sum.lm$lambda)").AsNumeric().First();
                    dblSELambda = m_pEngine.Evaluate("as.numeric(sum.lm$lambda.se)").AsNumeric().First();
                }
                double dblRsquared = 0;
                //Previous method
                //if(intInterceptModel != 1)
                //    dblRsquared = m_pEngine.Evaluate("as.numeric(sum.lm$NK)").AsNumeric().First();

                //New pseduo R squared calculation
                if (rbtError.Checked || rbtLag.Checked || rbtDurbin.Checked)
                    dblRsquared = m_pEngine.Evaluate("summary(lm(sum.lm$y~sum.lm$fitted.values))$r.squared").AsNumeric().First();
                else
                    dblRsquared = m_pEngine.Evaluate("summary(lm(sum.lm$Y~sum.lm$fit$fitted.values))$r.squared").AsNumeric().First();
                
                //Draw result form

                //Open Ouput form
                frmRegResult pfrmRegResult = new frmRegResult();
                if (rbtError.Checked)
                    pfrmRegResult.Text = "Spatial Autoregressive Model Summary (Error Model)";
                else if (rbtLag.Checked)
                    pfrmRegResult.Text = "Spatial Autoregressive Model Summary (Lag Model)";
                else if (rbtCAR.Checked)
                    pfrmRegResult.Text = "Spatial Autoregressive Model Summary (CAR Model)";
                else if (rbtDurbin.Checked)
                    pfrmRegResult.Text = "Spatial Autoregressive Model Summary (Spatial Durbin Model)";
                else
                    pfrmRegResult.Text = "Spatial Autoregressive Model Summary (SMA Model)";

                //pfrmRegResult.panel2.Visible = true;
                //Create DataTable to store Result
                System.Data.DataTable tblRegResult = new DataTable("SRResult");

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
                String.Format("{0:0.##}", tblRegResult.Columns["Std. Error"]);

                DataColumn dColTValue = new DataColumn();
                dColTValue.DataType = System.Type.GetType("System.Double");
                dColTValue.ColumnName = "z value";
                tblRegResult.Columns.Add(dColTValue);

                DataColumn dColPvT = new DataColumn();
                dColPvT.DataType = System.Type.GetType("System.Double");
                dColPvT.ColumnName = "Pr(>|z|)";
                tblRegResult.Columns.Add(dColPvT);

                int intNCoeff = matCoe.RowCount;

                //Store Data Table by R result
                for (int j = 0; j < intNCoeff; j++)
                {
                    DataRow pDataRow = tblRegResult.NewRow();
                    if (j == 0 && intInterceptModel != 1)
                    {
                        pDataRow["Name"] = "(Intercept)";
                    }
                    else if (intInterceptModel == 1)
                    {
                        if (rbtDurbin.Checked)
                        {
                            if (j <= intNCoeff / 2)
                                pDataRow["Name"] = independentNames[j];
                            else
                                pDataRow["Name"] = "lag." + independentNames[j - (intNCoeff / 2)];

                        }
                        else
                            pDataRow["Name"] = independentNames[j];
                    }
                    else
                    {
                        if (rbtDurbin.Checked)
                        {
                            if (j <= intNCoeff / 2)
                                pDataRow["Name"] = independentNames[j - 1];
                            else
                                pDataRow["Name"] = "lag." + independentNames[j - (intNCoeff / 2) - 1];

                        }
                        else
                            pDataRow["Name"] = independentNames[j - 1];
                    }
                    pDataRow["Estimate"] = Math.Round(matCoe[j, 0], intDeciPlaces);
                    pDataRow["Std. Error"] = Math.Round(matCoe[j, 1], intDeciPlaces);
                    pDataRow["z value"] = Math.Round(matCoe[j, 2], intDeciPlaces);
                    pDataRow["Pr(>|z|)"] = Math.Round(matCoe[j, 3], intDeciPlaces);


                    tblRegResult.Rows.Add(pDataRow);
                }

                //Assign Datagridview to Data Table
                pfrmRegResult.dgvResults.DataSource = tblRegResult;

                //Assign values at Textbox
                string strDecimalPlaces = "N" + intDeciPlaces.ToString();
                string[] strResults = new string[5];
                if (rbtLag.Checked || rbtDurbin.Checked)
                {
                    if (dblpLambda < 0.001)
                        strResults[0] = "rho: " + dblLambda.ToString(strDecimalPlaces) +
                                                ", LR Test Value: " + dblLRLambda.ToString(strDecimalPlaces) + ", p-value < 0.001";
                    else if (dblpLambda > 0.999)
                        strResults[0] = "rho: " + dblLambda.ToString(strDecimalPlaces) +
                                                ", LR Test Value: " + dblLRLambda.ToString(strDecimalPlaces) + ", p-value > 0.999";
                    else
                        strResults[0] = "rho: " + dblLambda.ToString(strDecimalPlaces) +
                                                ", LR Test Value: " + dblLRLambda.ToString(strDecimalPlaces) + ", p-value: " + dblpLambda.ToString(strDecimalPlaces);

                    if (dblpWald < 0.001)
                        strResults[1] = "Asymptotic S.E: " + dblSELambda.ToString(strDecimalPlaces) +
                         ", Wald: " + dblWald.ToString(strDecimalPlaces) + ", p-value < 0.001";
                    else if (dblpWald > 0.999)
                        strResults[1] = "Asymptotic S.E: " + dblSELambda.ToString(strDecimalPlaces) +
                        ", Wald: " + dblWald.ToString(strDecimalPlaces) + ", p-value > 0.999";
                    else
                        strResults[1] = "Asymptotic S.E: " + dblSELambda.ToString(strDecimalPlaces) +
                        ", Wald: " + dblWald.ToString(strDecimalPlaces) + ", p-value: " + dblpWald.ToString(strDecimalPlaces);


                    strResults[2] = "Log likelihood: " + dblLRErrorModel.ToString(strDecimalPlaces) +
                        ", Sigma-squared: " + dblSigmasquared.ToString(strDecimalPlaces);
                    strResults[3] = "AIC: " + dblAIC.ToString(strDecimalPlaces) + ", LM test for residuals autocorrelation: " + dblResiAuto.ToString(strDecimalPlaces);
                }
                else if (rbtError.Checked)
                {
                    if (dblpLambda < 0.001)
                        strResults[0] = "Lambda: " + dblLambda.ToString(strDecimalPlaces) +
                                                ", LR Test Value: " + dblLRLambda.ToString(strDecimalPlaces) + ", p-value < 0.001";
                    else if (dblpLambda > 0.999)
                        strResults[0] = "Lambda: " + dblLambda.ToString(strDecimalPlaces) +
                                                ", LR Test Value: " + dblLRLambda.ToString(strDecimalPlaces) + ", p-value > 0.999";
                    else
                        strResults[0] = "Lambda: " + dblLambda.ToString(strDecimalPlaces) +
                                                ", LR Test Value: " + dblLRLambda.ToString(strDecimalPlaces) + ", p-value: " + dblpLambda.ToString(strDecimalPlaces);

                    if (dblpWald < 0.001)
                        strResults[1] = "Asymptotic S.E: " + dblSELambda.ToString(strDecimalPlaces) +
                         ", Wald: " + dblWald.ToString(strDecimalPlaces) + ", p-value < 0.001";
                    else if (dblpWald > 0.999)
                        strResults[1] = "Asymptotic S.E: " + dblSELambda.ToString(strDecimalPlaces) +
                        ", Wald: " + dblWald.ToString(strDecimalPlaces) + ", p-value > 0.999";
                    else
                        strResults[1] = "Asymptotic S.E: " + dblSELambda.ToString(strDecimalPlaces) +
                        ", Wald: " + dblWald.ToString(strDecimalPlaces) + ", p-value: " + dblpWald.ToString(strDecimalPlaces);

                    //strResults[0] = "Lambda: " + dblLambda.ToString(strDecimalPlaces) +
                    //    ", LR Test Value: " + dblLRLambda.ToString(strDecimalPlaces) + ", p-value: " + dblpLambda.ToString(strDecimalPlaces);
                    //strResults[1] = "Asymptotic S.E: " + dblSELambda.ToString(strDecimalPlaces) +
                    //    ", Wald: " + dblWald.ToString(strDecimalPlaces) + ", p-value: " + dblpWald.ToString(strDecimalPlaces);
                    strResults[2] = "Log likelihood: " + dblLRErrorModel.ToString(strDecimalPlaces) +
                        ", Sigma-squared: " + dblSigmasquared.ToString(strDecimalPlaces);
                    strResults[3] = "AIC: " + dblAIC.ToString(strDecimalPlaces);
                }
                else
                {
                    if (dblpLambda < 0.001)
                        strResults[0] = "Lambda: " + dblLambda.ToString(strDecimalPlaces) +
                                                ", LR Test Value: " + dblLRLambda.ToString(strDecimalPlaces) + ", p-value < 0.001";
                    else if (dblpLambda > 0.999)
                        strResults[0] = "Lambda: " + dblLambda.ToString(strDecimalPlaces) +
                                                ", LR Test Value: " + dblLRLambda.ToString(strDecimalPlaces) + ", p-value > 0.999";
                    else
                        strResults[0] = "Lambda: " + dblLambda.ToString(strDecimalPlaces) +
                                                ", LR Test Value: " + dblLRLambda.ToString(strDecimalPlaces) + ", p-value: " + dblpLambda.ToString(strDecimalPlaces);

                    strResults[1] = "Numerical Hessian S.E of lambda: " + dblSELambda.ToString(strDecimalPlaces);
                    strResults[2] = "Log likelihood: " + dblLRErrorModel.ToString(strDecimalPlaces) +
                        ", Sigma-squared: " + dblSigmasquared.ToString(strDecimalPlaces);
                    strResults[3] = "AIC: " + dblAIC.ToString(strDecimalPlaces);
                }
                strResults[4] = "Pseudo-R-squared: " + dblRsquared.ToString(strDecimalPlaces);

                pfrmRegResult.txtOutput.Lines = strResults;

                if (chkSave.Checked)
                {
                    pfrmProgress.lblStatus.Text = "Saving residuals:";
                    //The field names are related with string[] DeterminedName in clsSnippet 
                    string strResiFldName = lstSave.Items[0].SubItems[1].Text;

                    //Get EVs and residuals
                    NumericVector nvResiduals = m_pEngine.Evaluate("as.numeric(sum.lm$residuals)").AsNumeric();
                    if (rbtCAR.Checked || rbtSMA.Checked)
                        nvResiduals = m_pEngine.Evaluate("as.numeric(sum.lm$fit$residuals)").AsNumeric();

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


                }

                pfrmProgress.Close();
                pfrmRegResult.Show();
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void rbtError_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtError.Checked)
            {
                rbtLag.Checked = false;
                rbtCAR.Checked = false;
                rbtSMA.Checked = false;
                rbtDurbin.Checked = false;


                rbtMatrix.Enabled = true;
                rbtMatrixJ.Enabled = true;
                rbtLU.Enabled = true;
                rbtChebyshev.Enabled = true;
                rbtMC.Enabled = true;
            }
        }

        private void rbtLag_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtLag.Checked)
            {
                rbtError.Checked = false;
                //rbtLag.Checked = false;
                rbtCAR.Checked = false;
                rbtSMA.Checked = false;
                rbtDurbin.Checked = false;


                rbtMatrix.Enabled = true;
                rbtMatrixJ.Enabled = true;
                rbtLU.Enabled = true;
                rbtChebyshev.Enabled = true;
                rbtMC.Enabled = true;
            }
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

        private void rbtCAR_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtCAR.Checked)
            {
                rbtError.Checked = false;
                rbtLag.Checked = false;
                //rbtCAR.Checked = false;
                rbtSMA.Checked = false;
                rbtDurbin.Checked = false;

                rbtMatrix.Enabled = true;
                rbtMatrixJ.Enabled = true;
                rbtLU.Enabled = true;
                rbtChebyshev.Enabled = true;
                rbtMC.Enabled = true;
            }
        }

        private void rbtSMA_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSMA.Checked)
            {
                rbtError.Checked = false;
                rbtLag.Checked = false;
                rbtCAR.Checked = false;
                //rbtSMA.Checked = false;
                rbtDurbin.Checked = false;

                rbtEigen.Checked = true;
                rbtMatrix.Checked = false;
                rbtMatrixJ.Checked = false;
                rbtLU.Checked = false;
                rbtChebyshev.Checked = false;
                rbtMC.Checked = false;

                rbtMatrix.Enabled = false;
                rbtMatrixJ.Enabled = false;
                rbtLU.Enabled = false;
                rbtChebyshev.Enabled = false;
                rbtMC.Enabled = false;
            }
        }

        private void rbtDurbin_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtDurbin.Checked)
            {
                rbtError.Checked = false;
                rbtLag.Checked = false;
                rbtCAR.Checked = false;
                rbtSMA.Checked = false;
                //rbtDurbin.Checked = false;


                rbtMatrix.Enabled = true;
                rbtMatrixJ.Enabled = true;
                rbtLU.Enabled = true;
                rbtChebyshev.Enabled = true;
                rbtMC.Enabled = true;
            }
        }

        private void rbtEigen_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtEigen.Checked)
            {
                rbtMatrix.Checked = false;
                rbtMatrixJ.Checked = false;
                rbtLU.Checked = false;
                rbtChebyshev.Checked = false;
                rbtMC.Checked = false;
            }
        }

        private void rbtMatrix_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtMatrix.Checked)
            {
                //rbtMatrix.Checked = false;
                rbtMatrixJ.Checked = false;
                rbtLU.Checked = false;
                rbtChebyshev.Checked = false;
                rbtMC.Checked = false;
                rbtEigen.Checked = false;
            }
        }

        private void rbtMatrixJ_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtMatrixJ.Checked)
            {
                rbtMatrix.Checked = false;
                //rbtMatrixJ.Checked = false;
                rbtLU.Checked = false;
                rbtChebyshev.Checked = false;
                rbtMC.Checked = false;
                rbtEigen.Checked = false;
            }
        }

        private void rbtLU_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtLU.Checked)
            {
                rbtMatrix.Checked = false;
                rbtMatrixJ.Checked = false;
                //rbtLU.Checked = false;
                rbtChebyshev.Checked = false;
                rbtMC.Checked = false;
                rbtEigen.Checked = false;
            }
        }

        private void rbtChebyshev_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtChebyshev.Checked)
            {
                rbtMatrix.Checked = false;
                rbtMatrixJ.Checked = false;
                rbtLU.Checked = false;
                //rbtChebyshev.Checked = false;
                rbtMC.Checked = false;
                rbtEigen.Checked = false;
            }
        }

        private void rbtMC_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtMC.Checked)
            {
                rbtMatrix.Checked = false;
                rbtMatrixJ.Checked = false;
                rbtLU.Checked = false;
                rbtChebyshev.Checked = false;
                //rbtMC.Checked = false;
                rbtEigen.Checked = false;
            }
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

    }
}
