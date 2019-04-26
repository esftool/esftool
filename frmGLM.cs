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
    public partial class frmGLM : Form
    {
        private MainForm m_pForm;
        private REngine m_pEngine;
        private IMapLayer m_pMaplayer;
        private DataTable m_dt = null;
        private clsSnippet m_pSnippet;
        //Varaibles for SWM
        private bool m_blnCreateSWM = false;

        public frmGLM()
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
                if (cboTargetLayer.Text != "" && cboFamily.Text != "")
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
                    cboNormalization.Text = "";

                    for (int i = 0; i < m_dt.Columns.Count; i++) //No restriction for dependent variable.
                    {
                        if (FindNumberFieldType(m_dt.Columns[i]))
                        {
                            lstFields.Items.Add(m_dt.Columns[i].ColumnName);
                            cboNormalization.Items.Add(m_dt.Columns[i].ColumnName);
                            cboFieldName.Items.Add(m_dt.Columns[i].ColumnName);
                        }

                    }

                    if (chkSave.Checked)
                        UpdateListview(lstSave, m_dt);
                    
                    //Add intercept in the listview for independent variables
                    lstIndeVar.Items.Add("Intercept");
                }
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
                string strResiFldNm = "glm_resi";

                //Update Name Using the UpdateFldName Function

                strResiFldNm = UpdateFldName(strResiFldNm, dt);

                if (strResiFldNm == null)
                    return;

                pListView.Items[0].SubItems[1].Text = strResiFldNm;
                pListView.EndUpdate();
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
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

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            m_pSnippet.MoveSelectedItemsinListBoxtoOtherListBox(lstFields, lstIndeVar);
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            m_pSnippet.MoveSelectedItemsinListBoxtoOtherListBox(lstIndeVar, lstFields);
        }

        private void cboFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (cboTargetLayer.Text != "" && cboFamily.Text != "")
                {
                    if (cboFamily.Text == "Binomial")
                    {
                        lblNorm.Text = "Normalization:";
                        lblNorm.Enabled = true;
                        cboNormalization.Enabled = true;
                    }

                    else if (cboFamily.Text == "Poisson" || cboFamily.Text == "Negative Binomial")
                    {
                        lblNorm.Text = "Offset (Optional):";
                        lblNorm.Enabled = true;
                        cboNormalization.Enabled = true;
                    }
                    else if (cboFamily.Text == "Logistic")
                    {
                        cboNormalization.Enabled = false;
                        lblNorm.Enabled = false;
                    }

                    string strLayerName = cboTargetLayer.Text;
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

                    cboFieldName.Items.Clear();
                    cboFieldName.Text = "";
                    lstFields.Items.Clear();
                    lstIndeVar.Items.Clear();
                    cboNormalization.Text = "";

                    for (int i = 0; i < m_dt.Columns.Count; i++) //No restiction for the dependent variable. 
                    {
                        if (FindNumberFieldType(m_dt.Columns[i]))
                        {
                            lstFields.Items.Add(m_dt.Columns[i].ColumnName);
                            cboNormalization.Items.Add(m_dt.Columns[i].ColumnName);
                            cboFieldName.Items.Add(m_dt.Columns[i].ColumnName);
                        }

                    }

                    //Add intercept in the listview for independent variables
                    lstIndeVar.Items.Add("Intercept");

                    if (chkSave.Checked)
                        UpdateListview(lstSave, m_dt);
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
                string strNoramlName = cboNormalization.Text;
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

                //Get index for independent and dependent variables
                int intDepenIdx = m_dt.Columns.IndexOf(dependentName);
                int intNoramIdx = -1;
                if (strNoramlName != "")
                    intNoramIdx = m_dt.Columns.IndexOf(strNoramlName);

                int[] idxes = new int[nIDepen];

                for (int j = 0; j < nIDepen; j++)
                {
                    idxes[j] = m_dt.Columns.IndexOf(independentNames[j]);
                }

                //Store independent values at Array
                double[] arrDepen = new double[nFeature];
                double[][] arrInDepen = new double[nIDepen][];
                double[] arrNormal = new double[nFeature];

                for (int j = 0; j < nIDepen; j++)
                {
                    arrInDepen[j] = new double[nFeature];
                }

                int i = 0;
                foreach (DataRow row in m_dt.Rows)
                {

                    arrDepen[i] = Convert.ToDouble(row[intDepenIdx]);

                    if (intNoramIdx != -1)
                        arrNormal[i] = Convert.ToDouble(row[intNoramIdx]);

                    for (int j = 0; j < nIDepen; j++)
                    {
                        arrInDepen[j][i] = Convert.ToDouble(row[idxes[j]]);
                    }
                    i++;
                }

                //Check the range of binomial RV
                if (cboFamily.Text == "Binomial" && intNoramIdx == -1)
                {
                    double dblMaxDepen = arrDepen.Max();
                    double dblMinDepen = arrDepen.Min();
                    if (dblMinDepen < 0 || dblMaxDepen > 1)
                    {
                        MessageBox.Show("The value range of a dependent variable must be between 0 and 1");

                        pfrmProgress.Close();
                        return;
                    }
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
                NumericVector vecNormal = null;
                if (cboFamily.Text == "Binomial" && intNoramIdx != -1)
                {
                    vecNormal = m_pEngine.CreateNumericVector(arrNormal);
                    m_pEngine.SetSymbol(strNoramlName, vecNormal);
                    plotCommmand.Append("cbind(" + dependentName + ", " + strNoramlName + "-" + dependentName + ")~");
                }
                else if (cboFamily.Text == "Poisson" && intNoramIdx != -1)
                {
                    vecNormal = m_pEngine.CreateNumericVector(arrNormal);
                    m_pEngine.SetSymbol(strNoramlName, vecNormal);
                    plotCommmand.Append(dependentName + "~");
                }
                else if (cboFamily.Text == "Negative Binomial" && intNoramIdx != -1)
                {
                    vecNormal = m_pEngine.CreateNumericVector(arrNormal);
                    m_pEngine.SetSymbol(strNoramlName, vecNormal);
                    plotCommmand.Append(dependentName + "~");
                }
                else
                    plotCommmand.Append(dependentName + "~");

                if (intInterceptModel == 2)
                {
                    plotCommmand.Append("1");
                }
                else
                {
                    for (int j = 0; j < nIDepen; j++)
                    {
                        NumericVector vecIndepen = m_pEngine.CreateNumericVector(arrInDepen[j]);
                        m_pEngine.SetSymbol(independentNames[j], vecIndepen);
                        plotCommmand.Append(independentNames[j] + "+");
                    }
                    plotCommmand.Remove(plotCommmand.Length - 1, 1);

                    if (intInterceptModel == 1)
                        plotCommmand.Append("-1");
                }


                if (cboFamily.Text == "Poisson")
                {
                    PoissonRegression(pfrmProgress, m_pMaplayer, plotCommmand.ToString(), nIDepen, independentNames, strNoramlName, intDeciPlaces, intInterceptModel);
                }
                else if (cboFamily.Text == "Negative Binomial")
                {
                    NBRegression(pfrmProgress, m_pMaplayer, plotCommmand.ToString(), nIDepen, independentNames, strNoramlName, intDeciPlaces, intInterceptModel);
                }
                else if (cboFamily.Text == "Binomial" || cboFamily.Text == "Logistic")
                {
                    BinomRegression(pfrmProgress, m_pMaplayer, plotCommmand.ToString(), nIDepen, independentNames, intDeciPlaces, intInterceptModel);
                }
                pfrmProgress.Close();
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void NBRegression(frmProgress pfrmProgress, IMapLayer pMapLayer, string strLM, int nIDepen, String[] independentNames, string strNoramlName, int intDeciPlaces, int intInterceptModel)
        {
            try
            {
                pfrmProgress.lblStatus.Text = "Calculate Regression Coefficients";

                if (strNoramlName == "")
                    m_pEngine.Evaluate("sample.glm <- glm.nb(" + strLM + ")");
                else
                {
                    try
                    {
                        m_pEngine.Evaluate("sample.glm <- glm.nb(" + strLM + "+ offset(" + strNoramlName + "))");
                    }
                    catch
                    {
                        MessageBox.Show("An offset requires a logarithm form. Please check the model again.");
                        pfrmProgress.Close();
                    }
                }

                pfrmProgress.lblStatus.Text = "Printing Output:";
                m_pEngine.Evaluate("sum.glm <- summary(sample.glm)");
                m_pEngine.Evaluate("sample.n <- length(sample.nb)");

                NumericMatrix matCoe = m_pEngine.Evaluate("as.matrix(sum.glm$coefficient)").AsNumericMatrix();
                CharacterVector vecNames = m_pEngine.Evaluate("attributes(sum.glm$coefficients)$dimnames[[1]]").AsCharacter();
                double dblNullDevi = m_pEngine.Evaluate("sum.glm$null.deviance").AsNumeric().First();
                double dblNullDF = m_pEngine.Evaluate("sum.glm$df.null").AsNumeric().First();
                double dblResiDevi = m_pEngine.Evaluate("sum.glm$deviance").AsNumeric().First();
                double dblResiDF = m_pEngine.Evaluate("sum.glm$df.residual").AsNumeric().First();

                //Nagelkerke r squared //Previous
                //double dblPseudoRsquared = m_pEngine.Evaluate("(1 - exp((sample.glm$dev - sample.glm$null)/sample.n))/(1 - exp(-sample.glm$null/sample.n))").AsNumeric().First(); 
                //New pseduo R squared calculation
                double dblPseudoRsquared = m_pEngine.Evaluate("summary(lm(sample.glm$y~sample.glm$fitted.values))$r.squared").AsNumeric().First();

                double dblResiLMMC = 0;
                double dblResiLMpVal = 0;
                if (chkResiAuto.Checked)
                {
                    if (cboAlternative.Text == "Greater")
                        m_pEngine.Evaluate("orgresi.mc <- moran.mc(residuals(sample.glm, type='" + cboResiType.Text + "'), listw =sample.listb, nsim=999, alternative = 'greater', zero.policy=TRUE)");
                    else if (cboAlternative.Text == "Less")
                        m_pEngine.Evaluate("orgresi.mc <- moran.mc(residuals(sample.glm, type='" + cboResiType.Text + "'), listw =sample.listb, nsim=999, alternative = 'less', zero.policy=TRUE)");
                    else
                        m_pEngine.Evaluate("orgresi.mc <- moran.mc(residuals(sample.glm, type='" + cboResiType.Text + "'), listw =sample.listb, nsim=999, alternative = 'greater', zero.policy=TRUE)");


                    dblResiLMMC = m_pEngine.Evaluate("orgresi.mc$statistic").AsNumeric().First();
                    dblResiLMpVal = m_pEngine.Evaluate("orgresi.mc$p.value").AsNumeric().First();

                }

                NumericVector nvecNonAIC = m_pEngine.Evaluate("sample.glm$aic").AsNumeric();
                //Open Ouput form
                frmRegResult pfrmRegResult = new frmRegResult();

                if (strNoramlName == "")
                    pfrmRegResult.Text = "Negative Binomial Regression Summary";
                else
                    pfrmRegResult.Text = "Negative Binomial Regression with Offset (" + strNoramlName + ") Summary";

                //Create DataTable to store Result
                System.Data.DataTable tblRegResult = new DataTable("PoiResult");

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
                dColTValue.ColumnName = "z value";
                tblRegResult.Columns.Add(dColTValue);

                DataColumn dColPvT = new DataColumn();
                dColPvT.DataType = System.Type.GetType("System.Double");
                dColPvT.ColumnName = "Pr(>|z|)";
                tblRegResult.Columns.Add(dColPvT);

                int intNCoeff = nIDepen + 1;

                //Store Data Table by R result
                for (int j = 0; j < intNCoeff; j++)
                {
                    DataRow pDataRow = tblRegResult.NewRow();
                    if (j == 0 && intInterceptModel != 1)
                    {
                        pDataRow["Name"] = "(Intercept)";
                    }
                    else if (intInterceptModel == 1)
                        pDataRow["Name"] = independentNames[j];
                    else
                    {
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

                //Get Data Table
                DataTable pDT = null;
                if (pMapLayer is MapPointLayer)
                {
                    MapPointLayer pMapPointLyr = default(MapPointLayer);
                    pMapPointLyr = (MapPointLayer)pMapLayer;
                    pDT = pMapPointLyr.DataSet.DataTable;
                }
                else if (pMapLayer is MapPolygonLayer)
                {
                    MapPolygonLayer pMapPolyLyr = default(MapPolygonLayer);
                    pMapPolyLyr = (MapPolygonLayer)pMapLayer;
                    pDT = pMapPolyLyr.DataSet.DataTable;
                }

                int nFeature = pDT.Rows.Count;

                //Assign values at Textbox
                string strDecimalPlaces = "N" + intDeciPlaces.ToString();
                string[] strResults = new string[6];
                strResults[0] = "Number of rows: " + nFeature.ToString();
                strResults[1] = "AIC: " + nvecNonAIC.Last().ToString(strDecimalPlaces);
                strResults[2] = "Null deviance: " + dblNullDevi.ToString(strDecimalPlaces) + " on " + dblNullDF.ToString("N0") + " degrees of freedom";
                strResults[3] = "Residual deviance: " + dblResiDevi.ToString(strDecimalPlaces) + " on " + dblResiDF.ToString("N0") + " degrees of freedom";

                if (intInterceptModel != 1)
                    strResults[4] = "Pseudo R squared: " + dblPseudoRsquared.ToString(strDecimalPlaces);
                else
                    strResults[4] = "";

                if (chkResiAuto.Checked)
                {
                    if (dblResiLMpVal < 0.001)
                        strResults[5] = "MC of residuals: " + dblResiLMMC.ToString("N3") + ", p-value < 0.001";
                    else if (dblResiLMpVal > 0.999)
                        strResults[5] = "MC of residuals: " + dblResiLMMC.ToString("N3") + ", p-value > 0.999";
                    else
                        strResults[5] = "MC of residuals: " + dblResiLMMC.ToString("N3") + ", p-value: " + dblResiLMpVal.ToString("N3");
                }
                else
                    strResults[5] = "";

                pfrmRegResult.txtOutput.Lines = strResults;

                if (chkSave.Checked)
                {
                    pfrmProgress.lblStatus.Text = "Saving residuals:";
                    //The field names are related with string[] DeterminedName in clsSnippet 
                    string strResiFldName = lstSave.Items[0].SubItems[1].Text;

                    //Get EVs and residuals
                    NumericVector nvResiduals = m_pEngine.Evaluate("as.numeric(residuals(sample.glm, type='" + cboResiType.Text + "'))").AsNumeric();


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


                }

                pfrmRegResult.Show();
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void PoissonRegression(frmProgress pfrmProgress, IMapLayer pMapLayer, string strLM, int nIDepen, String[] independentNames, string strNoramlName, int intDeciPlaces, int intInterceptModel)
        {
            try
            {
                pfrmProgress.lblStatus.Text = "Calculate Regression Coefficients";

                if (strNoramlName == "")
                    m_pEngine.Evaluate("sample.glm <- glm(" + strLM + ", family='poisson')");
                else
                {
                    try
                    {
                        m_pEngine.Evaluate("sample.glm <- glm(" + strLM + ", offset=" + strNoramlName + ", family='poisson')");
                    }
                    catch
                    {
                        MessageBox.Show("An offset requires a logarithm form. Please check the model again.");
                        pfrmProgress.Close();
                        return;
                    }

                }

                pfrmProgress.lblStatus.Text = "Printing Output:";
                m_pEngine.Evaluate("sum.glm <- summary(sample.glm)");
                m_pEngine.Evaluate("sample.n <- length(sample.nb)");

                NumericMatrix matCoe = m_pEngine.Evaluate("as.matrix(sum.glm$coefficient)").AsNumericMatrix();
                CharacterVector vecNames = m_pEngine.Evaluate("attributes(sum.glm$coefficients)$dimnames[[1]]").AsCharacter();
                double dblNullDevi = m_pEngine.Evaluate("sum.glm$null.deviance").AsNumeric().First();
                double dblNullDF = m_pEngine.Evaluate("sum.glm$df.null").AsNumeric().First();
                double dblResiDevi = m_pEngine.Evaluate("sum.glm$deviance").AsNumeric().First();
                double dblResiDF = m_pEngine.Evaluate("sum.glm$df.residual").AsNumeric().First();

                //Nagelkerke r squared //Previous
                //double dblPseudoRsquared = m_pEngine.Evaluate("(1 - exp((sample.glm$dev - sample.glm$null)/sample.n))/(1 - exp(-sample.glm$null/sample.n))").AsNumeric().First(); 
                //New pseduo R squared calculation
                double dblPseudoRsquared = m_pEngine.Evaluate("summary(lm(sample.glm$y~sample.glm$fitted.values))$r.squared").AsNumeric().First();

                double dblResiLMMC = 0;
                double dblResiLMpVal = 0;
                if (chkResiAuto.Checked)
                {
                    if (cboAlternative.Text == "Greater")
                        m_pEngine.Evaluate("orgresi.mc <- moran.mc(residuals(sample.glm, type='" + cboResiType.Text + "'), listw =sample.listb, nsim=999, alternative = 'greater', zero.policy=TRUE)");
                    else if (cboAlternative.Text == "Less")
                        m_pEngine.Evaluate("orgresi.mc <- moran.mc(residuals(sample.glm, type='" + cboResiType.Text + "'), listw =sample.listb, nsim=999, alternative = 'less', zero.policy=TRUE)");
                    else
                        m_pEngine.Evaluate("orgresi.mc <- moran.mc(residuals(sample.glm, type='" + cboResiType.Text + "'), listw =sample.listb, nsim=999, alternative = 'greater', zero.policy=TRUE)");


                    dblResiLMMC = m_pEngine.Evaluate("orgresi.mc$statistic").AsNumeric().First();
                    dblResiLMpVal = m_pEngine.Evaluate("orgresi.mc$p.value").AsNumeric().First();

                }

                NumericVector nvecNonAIC = m_pEngine.Evaluate("sample.glm$aic").AsNumeric();
                //Open Ouput form
                frmRegResult pfrmRegResult = new frmRegResult();

                if (strNoramlName == "")
                    pfrmRegResult.Text = "Poisson Regression Summary";
                else
                    pfrmRegResult.Text = "Poisson Regression with Offset (" + strNoramlName + ") Summary";

                //Create DataTable to store Result
                System.Data.DataTable tblRegResult = new DataTable("PoiResult");

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
                dColTValue.ColumnName = "z value";
                tblRegResult.Columns.Add(dColTValue);

                DataColumn dColPvT = new DataColumn();
                dColPvT.DataType = System.Type.GetType("System.Double");
                dColPvT.ColumnName = "Pr(>|z|)";
                tblRegResult.Columns.Add(dColPvT);

                int intNCoeff = nIDepen + 1;

                //Store Data Table by R result
                for (int j = 0; j < intNCoeff; j++)
                {
                    DataRow pDataRow = tblRegResult.NewRow();
                    if (j == 0 && intInterceptModel != 1)
                    {
                        pDataRow["Name"] = "(Intercept)";
                    }
                    else if (intInterceptModel == 1)
                        pDataRow["Name"] = independentNames[j];
                    else
                    {
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

                //Get Data Table
                DataTable pDT = null;
                if (pMapLayer is MapPointLayer)
                {
                    MapPointLayer pMapPointLyr = default(MapPointLayer);
                    pMapPointLyr = (MapPointLayer)pMapLayer;
                    pDT = pMapPointLyr.DataSet.DataTable;
                }
                else if (pMapLayer is MapPolygonLayer)
                {
                    MapPolygonLayer pMapPolyLyr = default(MapPolygonLayer);
                    pMapPolyLyr = (MapPolygonLayer)pMapLayer;
                    pDT = pMapPolyLyr.DataSet.DataTable;
                }

                int nFeature = pDT.Rows.Count;

                //Assign values at Textbox
                string strDecimalPlaces = "N" + intDeciPlaces.ToString();
                string[] strResults = new string[6];
                strResults[0] = "Number of rows: " + nFeature.ToString();
                strResults[1] = "AIC: " + nvecNonAIC.Last().ToString(strDecimalPlaces);
                strResults[2] = "Null deviance: " + dblNullDevi.ToString(strDecimalPlaces) + " on " + dblNullDF.ToString("N0") + " degrees of freedom";
                strResults[3] = "Residual deviance: " + dblResiDevi.ToString(strDecimalPlaces) + " on " + dblResiDF.ToString("N0") + " degrees of freedom";

                if (intInterceptModel != 1)
                    strResults[4] = "Pseudo R squared: " + dblPseudoRsquared.ToString(strDecimalPlaces);
                else
                    strResults[4] = "";

                if (chkResiAuto.Checked)
                {
                    if (dblResiLMpVal < 0.001)
                        strResults[5] = "MC of residuals: " + dblResiLMMC.ToString("N3") + ", p-value < 0.001";
                    else if (dblResiLMpVal > 0.999)
                        strResults[5] = "MC of residuals: " + dblResiLMMC.ToString("N3") + ", p-value > 0.999";
                    else
                        strResults[5] = "MC of residuals: " + dblResiLMMC.ToString("N3") + ", p-value: " + dblResiLMpVal.ToString("N3");
                }
                else
                    strResults[5] = "";

                pfrmRegResult.txtOutput.Lines = strResults;

                if (chkSave.Checked)
                {
                    pfrmProgress.lblStatus.Text = "Saving residuals:";
                    //The field names are related with string[] DeterminedName in clsSnippet 
                    string strResiFldName = lstSave.Items[0].SubItems[1].Text;

                    //Get EVs and residuals
                    NumericVector nvResiduals = m_pEngine.Evaluate("as.numeric(residuals(sample.glm, type='" + cboResiType.Text + "'))").AsNumeric();


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


                }

                pfrmRegResult.Show();
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }
        private void BinomRegression(frmProgress pfrmProgress, IMapLayer pMapLayer, string strLM, int nIDepen, String[] independentNames, int intDeciPlaces, int intInterceptModel)
        {
            try
            {
                pfrmProgress.lblStatus.Text = "Calculate Regression Coefficients";
                m_pEngine.Evaluate("sample.glm <- glm(" + strLM + ", family='binomial')");

                pfrmProgress.lblStatus.Text = "Printing Output:";
                m_pEngine.Evaluate("sum.glm <- summary(sample.glm)");
                m_pEngine.Evaluate("sample.n <- length(sample.nb)");

                NumericMatrix matCoe = m_pEngine.Evaluate("as.matrix(sum.glm$coefficient)").AsNumericMatrix();
                CharacterVector vecNames = m_pEngine.Evaluate("attributes(sum.glm$coefficients)$dimnames[[1]]").AsCharacter();
                double dblNullDevi = m_pEngine.Evaluate("sum.glm$null.deviance").AsNumeric().First();
                double dblNullDF = m_pEngine.Evaluate("sum.glm$df.null").AsNumeric().First();
                double dblResiDevi = m_pEngine.Evaluate("sum.glm$deviance").AsNumeric().First();
                double dblResiDF = m_pEngine.Evaluate("sum.glm$df.residual").AsNumeric().First();

                //Nagelkerke r squared //Previous
                //double dblPseudoRsquared = m_pEngine.Evaluate("(1 - exp((sample.glm$dev - sample.glm$null)/sample.n))/(1 - exp(-sample.glm$null/sample.n))").AsNumeric().First(); 
                //New pseduo R squared calculation
                double dblPseudoRsquared = m_pEngine.Evaluate("summary(lm(sample.glm$y~sample.glm$fitted.values))$r.squared").AsNumeric().First();

                double dblResiLMMC = 0;
                double dblResiLMpVal = 0;
                if (chkResiAuto.Checked)
                {
                    if (cboAlternative.Text == "Greater")
                        m_pEngine.Evaluate("orgresi.mc <- moran.mc(residuals(sample.glm, type='" + cboResiType.Text + "'), listw =sample.listb, nsim=999, alternative = 'greater', zero.policy=TRUE)");
                    else if (cboAlternative.Text == "Less")
                        m_pEngine.Evaluate("orgresi.mc <- moran.mc(residuals(sample.glm, type='" + cboResiType.Text + "'), listw =sample.listb, nsim=999, alternative = 'less', zero.policy=TRUE)");
                    else
                        m_pEngine.Evaluate("orgresi.mc <- moran.mc(residuals(sample.glm, type='" + cboResiType.Text + "'), listw =sample.listb, nsim=999, alternative = 'greater', zero.policy=TRUE)");

                    dblResiLMMC = m_pEngine.Evaluate("orgresi.mc$statistic").AsNumeric().First();
                    dblResiLMpVal = m_pEngine.Evaluate("orgresi.mc$p.value").AsNumeric().First();
                }
                NumericVector nvecNonAIC = m_pEngine.Evaluate("sample.glm$aic").AsNumeric();

                //Open Ouput form
                frmRegResult pfrmRegResult = new frmRegResult();
                pfrmRegResult.Text = "Binomial Regression Summary";

                //Create DataTable to store Result
                System.Data.DataTable tblRegResult = new DataTable("BinoResult");

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
                dColTValue.ColumnName = "z value";
                tblRegResult.Columns.Add(dColTValue);

                DataColumn dColPvT = new DataColumn();
                dColPvT.DataType = System.Type.GetType("System.Double");
                dColPvT.ColumnName = "Pr(>|z|)";
                tblRegResult.Columns.Add(dColPvT);

                int intNCoeff = nIDepen + 1;

                //Store Data Table by R result
                for (int j = 0; j < intNCoeff; j++)
                {
                    DataRow pDataRow = tblRegResult.NewRow();
                    if (j == 0 && intInterceptModel != 1)
                    {
                        pDataRow["Name"] = "(Intercept)";
                    }
                    else if (intInterceptModel == 1)
                        pDataRow["Name"] = independentNames[j];
                    else
                    {
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

                //Get Data Table
                DataTable pDT = null;
                if (pMapLayer is MapPointLayer)
                {
                    MapPointLayer pMapPointLyr = default(MapPointLayer);
                    pMapPointLyr = (MapPointLayer)pMapLayer;
                    pDT = pMapPointLyr.DataSet.DataTable;
                }
                else if (pMapLayer is MapPolygonLayer)
                {
                    MapPolygonLayer pMapPolyLyr = default(MapPolygonLayer);
                    pMapPolyLyr = (MapPolygonLayer)pMapLayer;
                    pDT = pMapPolyLyr.DataSet.DataTable;
                }

                int nFeature = pDT.Rows.Count;

                //Assign values at Textbox
                string strDecimalPlaces = "N" + intDeciPlaces.ToString();
                string[] strResults = new string[6];
                strResults[0] = "Number of rows: " + nFeature.ToString();
                strResults[1] = "AIC: " + nvecNonAIC.Last().ToString(strDecimalPlaces);
                strResults[2] = "Null deviance: " + dblNullDevi.ToString(strDecimalPlaces) + " on " + dblNullDF.ToString("N0") + " degrees of freedom";
                strResults[3] = "Residual deviance: " + dblResiDevi.ToString(strDecimalPlaces) + " on " + dblResiDF.ToString("N0") + " degrees of freedom";
                strResults[4] = "Nagelkerke pseudo R squared: " + dblPseudoRsquared.ToString(strDecimalPlaces);
                if (intInterceptModel != 1)
                    strResults[4] = "Pseudo R squared: " + dblPseudoRsquared.ToString(strDecimalPlaces);
                else
                    strResults[4] = "";
                if (chkResiAuto.Checked)
                {
                    if (dblResiLMpVal < 0.001)
                        strResults[3] = "MC of residuals: " + dblResiLMMC.ToString("N3") + ", p-value < 0.001";
                    else if (dblResiLMpVal > 0.999)
                        strResults[3] = "MC of residuals: " + dblResiLMMC.ToString("N3") + ", p-value > 0.999";
                    else
                        strResults[3] = "MC of residuals: " + dblResiLMMC.ToString("N3") + ", p-value: " + dblResiLMpVal.ToString("N3");
                }
                else
                    strResults[5] = "";

                pfrmRegResult.txtOutput.Lines = strResults;

                if (chkSave.Checked)
                {
                    pfrmProgress.lblStatus.Text = "Saving residuals:";
                    //The field names are related with string[] DeterminedName in clsSnippet 
                    string strResiFldName = lstSave.Items[0].SubItems[1].Text;

                    //Get EVs and residuals
                    NumericVector nvResiduals = m_pEngine.Evaluate("as.numeric(residuals(sample.glm, type='" + cboResiType.Text + "'))").AsNumeric();


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

                }

                pfrmRegResult.Show();
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
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

        private void chkSave_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSave.Checked)
            {
                if (m_dt != null)
                {
                    UpdateListview(lstSave, m_dt);
                    lstSave.Enabled = true;
                    lblType.Enabled = true;
                    cboResiType.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Select a target layer first");

                    chkSave.Checked = false;
                    return;
                }
            }
            else
            {
                lstSave.Enabled = false;
                lblType.Enabled = false;
                cboResiType.Enabled = false;
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

        private void cboNormalization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNormalization.Text == "offset")
            {
                MessageBox.Show("The field name of 'offset' cannot be used for an offset variable name in this tool. Please assign the field to another name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNormalization.Text = "";
            }

            if (cboNormalization.Text == "area")
            {
                MessageBox.Show("The field name of 'area' cannot be used for an offset variable name in this tool. Please assign the field to another name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNormalization.Text = "";
            }
        }
    }
}
