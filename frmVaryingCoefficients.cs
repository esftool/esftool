﻿using DotSpatial.Controls;
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
    public partial class frmVaryingCoefficients : Form
    {
        private MainForm m_pForm;
        private REngine m_pEngine;
        private IMapLayer m_pMaplayer;
        private DataTable m_dt = null;
        private clsSnippet m_pSnippet;
        //Varaibles for SWM
        private bool m_blnCreateSWM = false;

        public frmVaryingCoefficients()
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

                    for (int i = 0; i < m_dt.Columns.Count; i++)
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
                    lstIndeVar.SetItemChecked(0, true);
                    lstSave.Items.Clear();
                    UpdateListview(lstSave, m_dt);
                }
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }
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
                pListView.Items.Clear();

                int intVariableCount = lstIndeVar.CheckedItems.Count;
                //if(intVariableCount > 0)
                //{
                //    //Add intercept
                //    ListViewItem lvi = new ListViewItem("intercept");
                //    string strVarNm = "sv_inter";
                //    strVarNm = UpdateFldName(strVarNm, dt);
                //    lvi.SubItems.Add(strVarNm);
                //    pListView.Items.Add(lvi);
                //}
                for(int i = 0; i< intVariableCount; i++)
                {
                    //Add intercept
                    string strVarNm = lstIndeVar.CheckedItems[i].ToString();
                    ListViewItem lvi = new ListViewItem(strVarNm);
                    string strSVNm = "";
                    if (strVarNm == "Intercept")
                        strSVNm = "sv_inter";
                    else
                        strSVNm = "sv_" + strVarNm;

                    strSVNm = UpdateFldName(strSVNm, dt);
                    lvi.SubItems.Add(strSVNm);
                    pListView.Items.Add(lvi);
                }
               
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
                string returnNM = null;
                if (strFldNM.Length > 10)
                    returnNM = strFldNM.Substring(0, 10);
                else
                    returnNM = strFldNM;

                int i = 1;
                while (dt.Columns.IndexOf(returnNM) != -1)
                {
                    if (strFldNM.Length > 8)
                        returnNM = strFldNM.Substring(0, 8);
                    else
                        returnNM = strFldNM;

                    returnNM = returnNM + "_" + i.ToString();
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

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            m_pSnippet.MoveSelectedItemsinListBoxtoOtherCheckedListBox(lstFields, lstIndeVar);
            UpdateListview(lstSave, m_dt);
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            m_pSnippet.MoveSelectedItemsinCheckedListBoxtoOtherListBox(lstIndeVar, lstFields);
            UpdateListview(lstSave, m_dt);
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

                //Warning for method
                if (nFeature > m_pForm.intWarningCount)
                {
                    DialogResult dialogResult = MessageBox.Show("It might take a lot of time. Do you want to continue?", "Warning", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.No)
                    {
                        pfrmProgress.Close();
                        return;
                    }
                }

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
                //plotCommmand.Append("lm.full <- " + dependentName + "~");
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

                m_pEngine.Evaluate("sample.n <- length(sample.nb)");
                m_pEngine.Evaluate("B <- listw2mat(sample.listb); M <- diag(sample.n) - matrix(1/sample.n, sample.n, sample.n); MBM <- M%*%B%*%M");
                m_pEngine.Evaluate("eig <- eigen(MBM)");
                m_pEngine.Evaluate("EV <- as.data.frame( eig$vectors[,]); colnames(EV) <- paste('EV', 1:NCOL(EV), sep='')");
                double dblNCandidateEvs = 0;

                //Select Candidate EVs
                pfrmProgress.lblStatus.Text = "Selecting Candidate EVs:";

                string strEValue = nudEValue.Value.ToString();
                string strDirection = cboDirection.Text;

                if (strDirection == "Positive Only")
                {
                    m_pEngine.Evaluate("np <- length(eig$values[eig$values/eig$values[1]>" + strEValue + "])");
                    m_pEngine.Evaluate("EV <- EV[,1:np]");
                    dblNCandidateEvs = m_pEngine.Evaluate("np").AsNumeric().First();
                }
                else if (strDirection == "Negative Only")
                {
                    m_pEngine.Evaluate("n.all <- length(eig$values)");
                    m_pEngine.Evaluate("nn <- length(eig$values[eig$values/eig$values[1] < -" + strEValue + "])");
                    m_pEngine.Evaluate("n.start <- n.all-nn+1");
                    m_pEngine.Evaluate("EV <- EV[,n.start:n.all]");
                    dblNCandidateEvs = m_pEngine.Evaluate("nn").AsNumeric().First();
                }
                else if (strDirection == "Both")
                {
                    m_pEngine.Evaluate("np <- length(eig$values[eig$values/eig$values[1]>" + strEValue + "])");
                    m_pEngine.Evaluate("n.all <- length(eig$values)");
                    m_pEngine.Evaluate("nn <- length(eig$values[eig$values/eig$values[1] < -" + strEValue + "])");
                    m_pEngine.Evaluate("n.start <- n.all-nn+1");
                    m_pEngine.Evaluate("EV <- EV[,c(1:np, n.start:n.all)]");
                    dblNCandidateEvs = m_pEngine.Evaluate("nn+np").AsNumeric().First();
                }

                //Adding Varing EVs
                StringBuilder SEV_builder = new StringBuilder();
                StringBuilder SEV_Name_builder = new StringBuilder();
                SEV_builder.Append("sEV <- cbind(EV, ");
                SEV_Name_builder.Append("colnames(sEV) <- c(colnames(EV), ");
                for (int j = 0; j < nIDepen; j++)
                {
                    SEV_builder.Append(independentNames[j] + "*EV, ");
                    SEV_Name_builder.Append("as.character(paste('" + independentNames[j] + "', ':', colnames(EV), sep='')), ");
                }

                SEV_builder.Remove(SEV_builder.Length - 2, 2);
                SEV_builder.Append(")");

                SEV_Name_builder.Remove(SEV_Name_builder.Length - 2, 2);
                SEV_Name_builder.Append(")");

                m_pEngine.Evaluate(SEV_builder.ToString());
                m_pEngine.Evaluate(SEV_Name_builder.ToString());

                if (cboFamily.Text == "Linear (Gaussian)")
                    LinearESF(pfrmProgress, m_pMaplayer, plotCommmand.ToString(), nIDepen, independentNames, dblNCandidateEvs, intDeciPlaces, intInterceptModel);
                else if (cboFamily.Text == "Poisson")
                    PoissonESF(pfrmProgress, m_pMaplayer, plotCommmand.ToString(), nIDepen, independentNames, strNoramlName, dblNCandidateEvs, intDeciPlaces, intInterceptModel);
                else if (cboFamily.Text == "Binomial" || cboFamily.Text == "Logistic")
                    BinomESF(pfrmProgress, m_pMaplayer, plotCommmand.ToString(), nIDepen, independentNames, dblNCandidateEvs, intDeciPlaces, intInterceptModel);
                else if (cboFamily.Text == "Negative Binomial")
                    NBESF(pfrmProgress, m_pMaplayer, plotCommmand.ToString(), nIDepen, independentNames, strNoramlName, dblNCandidateEvs, intDeciPlaces, intInterceptModel);

                pfrmProgress.Close();
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                
                return;
            }
        }
        private void LinearESF(frmProgress pfrmProgress, IMapLayer pMapLayer, string strLM, int nIDepen, String[] independentNames, double dblNCandidateEvs, int intDeciPlaces, int intInterceptModel)
        {
            try
            {
                pfrmProgress.lblStatus.Text = "Selecting EVs";
                m_pEngine.Evaluate("esf.full <- lm(" + strLM + "+., data=sEV)");
                m_pEngine.Evaluate("sample.esf <- stepAIC(lm(" + strLM + ", data=sEV), scope=list(upper= esf.full), direction='forward')");

                pfrmProgress.lblStatus.Text = "Printing Output:";
                m_pEngine.Evaluate("sum.esf <- summary(sample.esf)");
                m_pEngine.Evaluate("sample.lm <- lm(" + strLM + ")");

                NumericMatrix matCoe = m_pEngine.Evaluate("as.matrix(sum.esf$coefficient)").AsNumericMatrix();
                NumericVector vecF = m_pEngine.Evaluate("as.numeric(sum.esf$fstatistic)").AsNumeric();
                CharacterVector vecNames = m_pEngine.Evaluate("attributes(sum.esf$coefficients)$dimnames[[1]]").AsCharacter();
                m_pEngine.Evaluate("fvalue <- as.numeric(sum.esf$fstatistic)");
                double dblPValueF = m_pEngine.Evaluate("pf(fvalue[1],fvalue[2],fvalue[3],lower.tail=F)").AsNumeric().First();
                double dblRsqaure = m_pEngine.Evaluate("sum.esf$r.squared").AsNumeric().First();
                double dblAdjRsqaure = m_pEngine.Evaluate("sum.esf$adj.r.squared").AsNumeric().First();
                double dblResiSE = m_pEngine.Evaluate("sum.esf$sigma").AsNumeric().First();
                NumericVector vecResiDF = m_pEngine.Evaluate("sum.esf$df").AsNumeric();

                if (cboAlternative.Text == "Greater")
                    m_pEngine.Evaluate("sample.esf.resi.mc <- lm.morantest(sample.esf, sample.listw, alternative = 'greater', zero.policy=TRUE)");
                else if (cboAlternative.Text == "Less")
                    m_pEngine.Evaluate("sample.esf.resi.mc <- lm.morantest(sample.esf, sample.listw, alternative = 'less', zero.policy=TRUE)");
                else if (cboAlternative.Text == "Two Sided")
                    m_pEngine.Evaluate("sample.esf.resi.mc <- lm.morantest(sample.esf, sample.listw, alternative = 'two.sided', zero.policy=TRUE)");
                else
                    m_pEngine.Evaluate("sample.esf.resi.mc <- lm.morantest(sample.esf, sample.listw, alternative = 'greater', zero.policy=TRUE)");

                double dblResiMC = m_pEngine.Evaluate("sample.esf.resi.mc$estimate").AsNumeric().First();
                double dblResiMCpVal = m_pEngine.Evaluate("sample.esf.resi.mc$p.value").AsNumeric().First();


                if (cboAlternative.Text == "Greater")
                    m_pEngine.Evaluate("sample.lm.resi.mc <- lm.morantest(sample.lm, sample.listw, alternative = 'greater', zero.policy=TRUE)");
                else if (cboAlternative.Text == "Less")
                    m_pEngine.Evaluate("sample.lm.resi.mc <- lm.morantest(sample.lm, sample.listw, alternative = 'less', zero.policy=TRUE)");
                else if (cboAlternative.Text == "Two Sided")
                    m_pEngine.Evaluate("sample.lm.resi.mc <- lm.morantest(sample.lm, sample.listw, alternative = 'two.sided', zero.policy=TRUE)");
                else
                    m_pEngine.Evaluate("sample.lm.resi.mc <- lm.morantest(sample.lm, sample.listw, alternative = 'greater', zero.policy=TRUE)");

                double dblResiLMMC = m_pEngine.Evaluate("sample.lm.resi.mc$estimate").AsNumeric().First();
                double dblResiLMpVal = m_pEngine.Evaluate("sample.lm.resi.mc$p.value").AsNumeric().First();

                NumericVector nvecNonAIC = m_pEngine.Evaluate("sample.esf$anova$AIC").AsNumeric();

                //Open Ouput form
                frmRegResult pfrmRegResult = new frmRegResult();
                pfrmRegResult.Text = "ESF Linear Regression Summary";

                //Create DataTable to store Result
                System.Data.DataTable tblRegResult = new DataTable("ESFResult");

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

                int intNCoeff = 0;
                int intNSelectedEVs = matCoe.RowCount - nIDepen - 1;
                if (intInterceptModel == 1)
                    intNSelectedEVs = matCoe.RowCount - nIDepen;

                if (chkCoeEVs.Checked)
                    intNCoeff = matCoe.RowCount;
                else
                {
                    intNCoeff = matCoe.RowCount - intNSelectedEVs;
                }

                //Store Data Table by R result
                for (int j = 0; j < intNCoeff; j++)
                {
                    DataRow pDataRow = tblRegResult.NewRow();
                    if (j == 0 && intInterceptModel != 1)
                    {
                        pDataRow["Name"] = "(Intercept)";
                    }
                    else
                    {
                        if (chkCoeEVs.Checked)
                            pDataRow["Name"] = vecNames[j];
                        else
                        {
                            if (intInterceptModel == 1)
                                pDataRow["Name"] = independentNames[j];
                            else
                                pDataRow["Name"] = independentNames[j - 1];
                        }
                    }
                    pDataRow["Estimate"] = Math.Round(matCoe[j, 0], intDeciPlaces);
                    pDataRow["Std. Error"] = Math.Round(matCoe[j, 1], intDeciPlaces);
                    pDataRow["t value"] = Math.Round(matCoe[j, 2], intDeciPlaces);
                    pDataRow["Pr(>|t|)"] = Math.Round(matCoe[j, 3], intDeciPlaces);
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
                string[] strResults = new string[7];
                strResults[0] = "Number of rows: " + nFeature.ToString() + ", Number of candidate EVs: " + dblNCandidateEvs.ToString() + ", Selected EVs: " + intNSelectedEVs.ToString();

                if (dblResiLMpVal < 0.001)
                    strResults[1] = "MC of non-ESF residuals: " + dblResiLMMC.ToString("N3") + ", p-value < 0.001";
                else if (dblResiLMpVal > 0.999)
                    strResults[1] = "MC of non-ESF residuals: " + dblResiLMMC.ToString("N3") + ", p-value > 0.999";
                else
                    strResults[1] = "MC of non-ESF residuals: " + dblResiLMMC.ToString("N3") + ", p-value: " + dblResiLMpVal.ToString("N3");

                strResults[2] = "AIC of non-ESF: " + nvecNonAIC.First().ToString(strDecimalPlaces) + ", AIC of Final Model: " + nvecNonAIC.Last().ToString(strDecimalPlaces);
                strResults[3] = "Residual standard error: " + dblResiSE.ToString(strDecimalPlaces) +
                    " on " + vecResiDF[1].ToString() + " degrees of freedom";
                strResults[4] = "Multiple R-squared: " + dblRsqaure.ToString(strDecimalPlaces) +
                    ", Adjusted R-squared: " + dblAdjRsqaure.ToString(strDecimalPlaces);

                if (dblPValueF < 0.001)
                    strResults[5] = "F-Statistic: " + vecF[0].ToString(strDecimalPlaces) +
                    " on " + vecF[1].ToString() + " and " + vecF[2].ToString() + " DF, p-value < 0.001";
                else if (dblPValueF > 0.999)
                    strResults[5] = "F-Statistic: " + vecF[0].ToString(strDecimalPlaces) +
                    " on " + vecF[1].ToString() + " and " + vecF[2].ToString() + " DF, p-value > 0.999";
                else
                    strResults[5] = "F-Statistic: " + vecF[0].ToString(strDecimalPlaces) +
                     " on " + vecF[1].ToString() + " and " + vecF[2].ToString() + " DF, p-value: " + dblPValueF.ToString(strDecimalPlaces);

                if (dblResiMCpVal < 0.001)
                    strResults[6] = "MC of residuals: " + dblResiMC.ToString("N3") + ", p-value < 0.001";
                else if (dblResiMCpVal > 0.999)
                    strResults[6] = "MC of residuals: " + dblResiMC.ToString("N3") + ", p-value > 0.999";
                else
                    strResults[6] = "MC of residuals: " + dblResiMC.ToString("N3") + ", p-value: " + dblResiMCpVal.ToString("N3");

                pfrmRegResult.txtOutput.Lines = strResults;

                //Save Outputs in SHP
                pfrmProgress.lblStatus.Text = "Saving results:";

                int intResultCnt = lstSave.Items.Count;

                //Get EVs and residuals
                NumericMatrix nmModel = m_pEngine.Evaluate("as.matrix(sample.esf$model)").AsNumericMatrix();
                //NumericVector nvResiduals = m_pEngine.Evaluate("as.numeric(sample.esf$residuals)").AsNumeric();


                for (int j = 0; j < intResultCnt; j++)
                {
                    string strSVName = lstSave.Items[j].SubItems[0].Text;
                    string strFldName = lstSave.Items[j].SubItems[1].Text;

                    // Create field, if there isn't
                    if (pDT.Columns.IndexOf(strFldName) == -1)
                    {
                        //Add fields
                        DataColumn pColumn = new DataColumn(strFldName);
                        pColumn.DataType = Type.GetType("System.Double");
                        pDT.Columns.Add(pColumn);
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Do you want to overwrite " + strFldName + " field?", "Overwrite", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.No)
                        {
                            return;
                        }
                    }

                    //Collect index of related coefficients and variables                    
                    List<int> lstCoeffIdx = new List<int>();
                    int intInterceptIdx = new int();
                    for (int k = 1; k < vecNames.Length; k++) //Do not add intercept
                    {
                        string strRelatedVar = null;

                        if (k < nIDepen + 1)
                        {
                            strRelatedVar = vecNames[k];
                            if (strRelatedVar == strSVName)
                                intInterceptIdx = k;
                        }
                        else
                        {
                            int intDivision = vecNames[k].IndexOf(":");

                            if (j == 0 && intDivision == -1)
                                lstCoeffIdx.Add(k);

                            if (intDivision != -1)
                            {
                                //strRelatedVar = vecNames[k].Substring(0, intDivision);
                                strRelatedVar = vecNames[k].Substring(1, intDivision - 1); //To consider "'";;
                            }

                            if (strRelatedVar == strSVName)
                                lstCoeffIdx.Add(k);
                        }
                    }

                    //Update Field
                    int featureIdx = 0;
                    int intFldIdx = pDT.Columns.IndexOf(strFldName);

                    foreach (DataRow row in pDT.Rows)
                    {
                       
                        //Calculate and update spatial filter (Coefficient estimate * selected EVs)
                        double dblIntMedValue = 0;
                        double dblVaryingValue = 0;
                        if (j == 0)
                            dblVaryingValue = matCoe[0, 0];
                        else
                            dblVaryingValue = matCoe[intInterceptIdx, 0];

                        foreach (int k in lstCoeffIdx)
                        {
                            dblIntMedValue = matCoe[k, 0] * nmModel[featureIdx, k];
                            dblVaryingValue += dblIntMedValue;
                        }

                        row[intFldIdx] = dblVaryingValue;
                        featureIdx++;
                    }

                }

                //Save Result;
                if (pMapLayer is MapPointLayer)
                {
                    MapPointLayer pMapPointLyr = default(MapPointLayer);
                    pMapPointLyr = (MapPointLayer)pMapLayer;
                    pMapPointLyr.DataSet.Save();
                }
                else if (pMapLayer is MapPolygonLayer)
                {
                    MapPolygonLayer pMapPolyLyr = default(MapPolygonLayer);
                    pMapPolyLyr = (MapPolygonLayer)pMapLayer;
                    pMapPolyLyr.DataSet.Save();
                }

                pfrmRegResult.Show();
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }


        private void PoissonESF(frmProgress pfrmProgress, IMapLayer pMapLayer, string strLM, int nIDepen, String[] independentNames, string strNoramlName, double dblNCandidateEvs, int intDeciPlaces, int intInterceptModel)
        {
            try
            {
                pfrmProgress.lblStatus.Text = "Selecting EVs";
                if (strNoramlName == "")
                {
                    m_pEngine.Evaluate("esf.full <- glm(" + strLM + "+., data=sEV, family='poisson')");
                    m_pEngine.Evaluate("esf.org <- glm(" + strLM + ", data=sEV, family='poisson')");
                }
                else
                {
                    m_pEngine.Evaluate("esf.full <- glm(" + strLM + "+. , offset=" + strNoramlName + ", data=sEV, family='poisson')");
                    m_pEngine.Evaluate("esf.org <- glm(" + strLM + ", offset=" + strNoramlName + ", data=sEV, family='poisson')");
                }
                m_pEngine.Evaluate("sample.esf <- stepAIC(esf.org, scope=list(upper= esf.full), direction='forward')");

                pfrmProgress.lblStatus.Text = "Printing Output:";
                m_pEngine.Evaluate("sum.esf <- summary(sample.esf)");
                //m_pEngine.Evaluate("sample.lm <- lm(" + strLM + ")");

                NumericMatrix matCoe = m_pEngine.Evaluate("as.matrix(sum.esf$coefficient)").AsNumericMatrix();
                CharacterVector vecNames = m_pEngine.Evaluate("attributes(sum.esf$coefficients)$dimnames[[1]]").AsCharacter();

                double dblNullDevi = m_pEngine.Evaluate("sum.esf$null.deviance").AsNumeric().First();
                double dblNullDF = m_pEngine.Evaluate("sum.esf$df.null").AsNumeric().First();
                double dblResiDevi = m_pEngine.Evaluate("sum.esf$deviance").AsNumeric().First();
                double dblResiDF = m_pEngine.Evaluate("sum.esf$df.residual").AsNumeric().First();

                //MC Using Pearson residual (Lin and Zhang 2007, GA) 
                if (cboAlternative.Text == "Greater")
                    m_pEngine.Evaluate("sampleresi.mc <-moran.mc(residuals(sample.esf, type='pearson'), listw =sample.listb, nsim=999, alternative = 'greater', zero.policy=TRUE)");
                else if (cboAlternative.Text == "Less")
                    m_pEngine.Evaluate("sampleresi.mc <-moran.mc(residuals(sample.esf, type='pearson'), listw =sample.listb, nsim=999, alternative = 'less', zero.policy=TRUE)");
                else
                    m_pEngine.Evaluate("sampleresi.mc <-moran.mc(residuals(sample.esf, type='pearson'), listw =sample.listb, nsim=999, alternative = 'greater', zero.policy=TRUE)");

                double dblResiMC = m_pEngine.Evaluate("sampleresi.mc$statistic").AsNumeric().First();
                double dblResiMCpVal = m_pEngine.Evaluate("sampleresi.mc$p.value").AsNumeric().First();

                if (cboAlternative.Text == "Greater")
                    m_pEngine.Evaluate("orgresi.mc <-moran.mc(residuals(esf.org, type='pearson'), listw =sample.listb, nsim=999, alternative = 'greater', zero.policy=TRUE)");
                else if (cboAlternative.Text == "Less")
                    m_pEngine.Evaluate("orgresi.mc <-moran.mc(residuals(esf.org, type='pearson'), listw =sample.listb, nsim=999, alternative = 'less', zero.policy=TRUE)");
                else
                    m_pEngine.Evaluate("orgresi.mc <-moran.mc(residuals(esf.org, type='pearson'), listw =sample.listb, nsim=999, alternative = 'greater', zero.policy=TRUE)");

                double dblResiLMMC = m_pEngine.Evaluate("orgresi.mc$statistic").AsNumeric().First();
                double dblResiLMpVal = m_pEngine.Evaluate("orgresi.mc$p.value").AsNumeric().First();
                //Nagelkerke r squared
                //double dblPseudoRsquared = m_pEngine.Evaluate("(1 - exp((sample.esf$deviance - sample.esf$null.deviance)/sample.n))/(1 - exp(-sample.esf$null.deviance/sample.n))").AsNumeric().First();
                double dblPseudoRsquared = m_pEngine.Evaluate("summary(lm(sample.esf$y~sample.esf$fitted.values))$r.squared").AsNumeric().First();

                NumericVector nvecNonAIC = m_pEngine.Evaluate("sample.esf$aic").AsNumeric();

                //Open Ouput form
                frmRegResult pfrmRegResult = new frmRegResult();
                if (strNoramlName == "")
                    pfrmRegResult.Text = "ESF Poisson Regression Summary";
                else
                    pfrmRegResult.Text = "ESF Poisson Regression with Offset (" + strNoramlName + ") Summary";

                //Create DataTable to store Result
                System.Data.DataTable tblRegResult = new DataTable("ESFResult");

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

                int intNCoeff = 0;
                int intNSelectedEVs = matCoe.RowCount - nIDepen - 1;
                if (intInterceptModel == 1)
                    intNSelectedEVs = matCoe.RowCount - nIDepen;

                if (chkCoeEVs.Checked)
                    intNCoeff = matCoe.RowCount;
                else
                {
                    intNCoeff = matCoe.RowCount - intNSelectedEVs;
                }
                //Store Data Table by R result
                for (int j = 0; j < intNCoeff; j++)
                {
                    DataRow pDataRow = tblRegResult.NewRow();
                    if (j == 0 && intInterceptModel != 1)
                    {
                        pDataRow["Name"] = "(Intercept)";
                    }
                    else
                    {
                        if (chkCoeEVs.Checked)
                            pDataRow["Name"] = vecNames[j];
                        else
                        {
                            if (intInterceptModel == 1)
                                pDataRow["Name"] = independentNames[j];
                            else
                                pDataRow["Name"] = independentNames[j - 1];
                        }
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
                string[] strResults = new string[7];
                strResults[0] = "Number of rows: " + nFeature.ToString() + ", Number of candidate EVs: " + dblNCandidateEvs.ToString() + ", Selected EVs: " + intNSelectedEVs.ToString();

                if (dblResiLMpVal < 0.001)
                    strResults[1] = "MC of non-ESF residuals: " + dblResiLMMC.ToString("N3") + ", p-value < 0.001";
                else if (dblResiLMpVal > 0.999)
                    strResults[1] = "MC of non-ESF residuals: " + dblResiLMMC.ToString("N3") + ", p-value > 0.999";
                else
                    strResults[1] = "MC of non-ESF residuals: " + dblResiLMMC.ToString("N3") + ", p-value: " + dblResiLMpVal.ToString("N3");

                strResults[2] = "AIC of Final Model: " + nvecNonAIC.Last().ToString(strDecimalPlaces);
                strResults[3] = "Null deviance: " + dblNullDevi.ToString(strDecimalPlaces) + " on " + dblNullDF.ToString("N0") + " degrees of freedom";
                strResults[4] = "Residual deviance: " + dblResiDevi.ToString(strDecimalPlaces) + " on " + dblResiDF.ToString("N0") + " degrees of freedom";
                strResults[5] = "Pseudo R squared: " + dblPseudoRsquared.ToString(strDecimalPlaces);

                if (dblResiMCpVal < 0.001)
                    strResults[6] = "MC of residuals: " + dblResiMC.ToString("N3") + ", p-value < 0.001";
                else if (dblResiMCpVal > 0.999)
                    strResults[6] = "MC of residuals: " + dblResiMC.ToString("N3") + ", p-value > 0.999";
                else
                    strResults[6] = "MC of residuals: " + dblResiMC.ToString("N3") + ", p-value: " + dblResiMCpVal.ToString("N3");

                pfrmRegResult.txtOutput.Lines = strResults;

                //Save Outputs in SHP
                pfrmProgress.lblStatus.Text = "Saving results:";

                int intResultCnt = lstSave.Items.Count;

                //Get EVs and residuals
                NumericMatrix nmModel = m_pEngine.Evaluate("as.matrix(sample.esf$model)").AsNumericMatrix();
                //NumericVector nvResiduals = m_pEngine.Evaluate("as.numeric(sample.esf$residuals)").AsNumeric();


                for (int j = 0; j < intResultCnt; j++)
                {
                    string strSVName = lstSave.Items[j].SubItems[0].Text;
                    string strFldName = lstSave.Items[j].SubItems[1].Text;

                    // Create field, if there isn't
                    if (pDT.Columns.IndexOf(strFldName) == -1)
                    {
                        //Add fields
                        DataColumn pColumn = new DataColumn(strFldName);
                        pColumn.DataType = Type.GetType("System.Double");
                        pDT.Columns.Add(pColumn);
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Do you want to overwrite " + strFldName + " field?", "Overwrite", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.No)
                        {
                            return;
                        }
                    }

                    //Collect index of related coefficients and variables                    
                    List<int> lstCoeffIdx = new List<int>();
                    int intInterceptIdx = new int();
                    for (int k = 1; k < vecNames.Length; k++) //Do not add intercept
                    {
                        string strRelatedVar = null;

                        if (k < nIDepen + 1)
                        {
                            strRelatedVar = vecNames[k];
                            if (strRelatedVar == strSVName)
                                intInterceptIdx = k;
                        }
                        else
                        {
                            int intDivision = vecNames[k].IndexOf(":");

                            if (j == 0 && intDivision == -1)
                                lstCoeffIdx.Add(k);

                            if (intDivision != -1)
                            {
                                //strRelatedVar = vecNames[k].Substring(0, intDivision);
                                strRelatedVar = vecNames[k].Substring(1, intDivision - 1); //To consider "'";;
                            }

                            if (strRelatedVar == strSVName)
                                lstCoeffIdx.Add(k);
                        }
                    }

                    //Update Field
                    int featureIdx = 0;
                    int intFldIdx = pDT.Columns.IndexOf(strFldName);

                    foreach (DataRow row in pDT.Rows)
                    {

                        //Calculate and update spatial filter (Coefficient estimate * selected EVs)
                        double dblIntMedValue = 0;
                        double dblVaryingValue = 0;
                        if (j == 0)
                            dblVaryingValue = matCoe[0, 0];
                        else
                            dblVaryingValue = matCoe[intInterceptIdx, 0];

                        foreach (int k in lstCoeffIdx)
                        {
                            dblIntMedValue = matCoe[k, 0] * nmModel[featureIdx, k];
                            dblVaryingValue += dblIntMedValue;
                        }

                        row[intFldIdx] = dblVaryingValue;
                        featureIdx++;
                    }

                }

                //Save Result;
                if (pMapLayer is MapPointLayer)
                {
                    MapPointLayer pMapPointLyr = default(MapPointLayer);
                    pMapPointLyr = (MapPointLayer)pMapLayer;
                    pMapPointLyr.DataSet.Save();
                }
                else if (pMapLayer is MapPolygonLayer)
                {
                    MapPolygonLayer pMapPolyLyr = default(MapPolygonLayer);
                    pMapPolyLyr = (MapPolygonLayer)pMapLayer;
                    pMapPolyLyr.DataSet.Save();
                }

                pfrmRegResult.Show();
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void NBESF(frmProgress pfrmProgress, IMapLayer pMapLayer, string strLM, int nIDepen, String[] independentNames, string strNoramlName, double dblNCandidateEvs, int intDeciPlaces, int intInterceptModel)
        {
            try
            {
                pfrmProgress.lblStatus.Text = "Selecting EVs";
                if (strNoramlName == "")
                {
                    m_pEngine.Evaluate("esf.full <- glm.nb(" + strLM + "+., data=sEV)");
                    m_pEngine.Evaluate("esf.org <- glm.nb(" + strLM + ", data=sEV)");
                }
                else
                {
                    m_pEngine.Evaluate("esf.full <- glm.nb(" + strLM + "+ offset(" + strNoramlName + ")+., data=sEV)");
                    m_pEngine.Evaluate("esf.org <- glm(" + strLM + "+ offset(" + strNoramlName + "), data=sEV)");
                }

                m_pEngine.Evaluate("sample.esf <- stepAIC(esf.org, scope=list(upper= esf.full), direction='forward')");

                pfrmProgress.lblStatus.Text = "Printing Output:";
                m_pEngine.Evaluate("sum.esf <- summary(sample.esf)");
                //m_pEngine.Evaluate("sample.lm <- lm(" + strLM + ")");

                NumericMatrix matCoe = m_pEngine.Evaluate("as.matrix(sum.esf$coefficient)").AsNumericMatrix();
                CharacterVector vecNames = m_pEngine.Evaluate("attributes(sum.esf$coefficients)$dimnames[[1]]").AsCharacter();

                double dblNullDevi = m_pEngine.Evaluate("sum.esf$null.deviance").AsNumeric().First();
                double dblNullDF = m_pEngine.Evaluate("sum.esf$df.null").AsNumeric().First();
                double dblResiDevi = m_pEngine.Evaluate("sum.esf$deviance").AsNumeric().First();
                double dblResiDF = m_pEngine.Evaluate("sum.esf$df.residual").AsNumeric().First();

                //MC Using Pearson residual (Lin and Zhang 2007, GA) 
                if (cboAlternative.Text == "Greater")
                    m_pEngine.Evaluate("sampleresi.mc <-moran.mc(residuals(sample.esf, type='pearson'), listw =sample.listb, nsim=999, alternative = 'greater', zero.policy=TRUE)");
                else if (cboAlternative.Text == "Less")
                    m_pEngine.Evaluate("sampleresi.mc <-moran.mc(residuals(sample.esf, type='pearson'), listw =sample.listb, nsim=999, alternative = 'less', zero.policy=TRUE)");
                else
                    m_pEngine.Evaluate("sampleresi.mc <-moran.mc(residuals(sample.esf, type='pearson'), listw =sample.listb, nsim=999, alternative = 'greater', zero.policy=TRUE)");

                double dblResiMC = m_pEngine.Evaluate("sampleresi.mc$statistic").AsNumeric().First();
                double dblResiMCpVal = m_pEngine.Evaluate("sampleresi.mc$p.value").AsNumeric().First();

                if (cboAlternative.Text == "Greater")
                    m_pEngine.Evaluate("orgresi.mc <-moran.mc(residuals(esf.org, type='pearson'), listw =sample.listb, nsim=999, alternative = 'greater', zero.policy=TRUE)");
                else if (cboAlternative.Text == "Less")
                    m_pEngine.Evaluate("orgresi.mc <-moran.mc(residuals(esf.org, type='pearson'), listw =sample.listb, nsim=999, alternative = 'less', zero.policy=TRUE)");
                else
                    m_pEngine.Evaluate("orgresi.mc <-moran.mc(residuals(esf.org, type='pearson'), listw =sample.listb, nsim=999, alternative = 'greater', zero.policy=TRUE)");

                double dblResiLMMC = m_pEngine.Evaluate("orgresi.mc$statistic").AsNumeric().First();
                double dblResiLMpVal = m_pEngine.Evaluate("orgresi.mc$p.value").AsNumeric().First();
                //Nagelkerke r squared
                //double dblPseudoRsquared = m_pEngine.Evaluate("(1 - exp((sample.esf$deviance - sample.esf$null.deviance)/sample.n))/(1 - exp(-sample.esf$null.deviance/sample.n))").AsNumeric().First();
                double dblPseudoRsquared = m_pEngine.Evaluate("summary(lm(sample.esf$y~sample.esf$fitted.values))$r.squared").AsNumeric().First();

                NumericVector nvecNonAIC = m_pEngine.Evaluate("sample.esf$aic").AsNumeric();

                //Open Ouput form
                frmRegResult pfrmRegResult = new frmRegResult();
                if (strNoramlName == "")
                    pfrmRegResult.Text = "ESF Negative Binomial Regression Summary";
                else
                    pfrmRegResult.Text = "ESF Negative Binomial Regression with Offset (" + strNoramlName + ") Summary";

                //Create DataTable to store Result
                System.Data.DataTable tblRegResult = new DataTable("ESFResult");

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

                int intNCoeff = 0;
                int intNSelectedEVs = matCoe.RowCount - nIDepen - 1;
                if (intInterceptModel == 1)
                    intNSelectedEVs = matCoe.RowCount - nIDepen;

                if (chkCoeEVs.Checked)
                    intNCoeff = matCoe.RowCount;
                else
                {
                    intNCoeff = matCoe.RowCount - intNSelectedEVs;
                }
                //Store Data Table by R result
                for (int j = 0; j < intNCoeff; j++)
                {
                    DataRow pDataRow = tblRegResult.NewRow();
                    if (j == 0 && intInterceptModel != 1)
                    {
                        pDataRow["Name"] = "(Intercept)";
                    }
                    else
                    {
                        if (chkCoeEVs.Checked)
                            pDataRow["Name"] = vecNames[j];
                        else
                        {
                            if (intInterceptModel == 1)
                                pDataRow["Name"] = independentNames[j];
                            else
                                pDataRow["Name"] = independentNames[j - 1];
                        }
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
                string[] strResults = new string[7];
                strResults[0] = "Number of rows: " + nFeature.ToString() + ", Number of candidate EVs: " + dblNCandidateEvs.ToString() + ", Selected EVs: " + intNSelectedEVs.ToString();

                if (dblResiLMpVal < 0.001)
                    strResults[1] = "MC of non-ESF residuals: " + dblResiLMMC.ToString("N3") + ", p-value < 0.001";
                else if (dblResiLMpVal > 0.999)
                    strResults[1] = "MC of non-ESF residuals: " + dblResiLMMC.ToString("N3") + ", p-value > 0.999";
                else
                    strResults[1] = "MC of non-ESF residuals: " + dblResiLMMC.ToString("N3") + ", p-value: " + dblResiLMpVal.ToString("N3");

                strResults[2] = "AIC of Final Model: " + nvecNonAIC.Last().ToString(strDecimalPlaces);
                strResults[3] = "Null deviance: " + dblNullDevi.ToString(strDecimalPlaces) + " on " + dblNullDF.ToString("N0") + " degrees of freedom";
                strResults[4] = "Residual deviance: " + dblResiDevi.ToString(strDecimalPlaces) + " on " + dblResiDF.ToString("N0") + " degrees of freedom";
                strResults[5] = "Pseudo R squared: " + dblPseudoRsquared.ToString(strDecimalPlaces);

                if (dblResiMCpVal < 0.001)
                    strResults[6] = "MC of residuals: " + dblResiMC.ToString("N3") + ", p-value < 0.001";
                else if (dblResiMCpVal > 0.999)
                    strResults[6] = "MC of residuals: " + dblResiMC.ToString("N3") + ", p-value > 0.999";
                else
                    strResults[6] = "MC of residuals: " + dblResiMC.ToString("N3") + ", p-value: " + dblResiMCpVal.ToString("N3");

                pfrmRegResult.txtOutput.Lines = strResults;

                //Save Outputs in SHP
                pfrmProgress.lblStatus.Text = "Saving results:";

                int intResultCnt = lstSave.Items.Count;

                //Get EVs and residuals
                NumericMatrix nmModel = m_pEngine.Evaluate("as.matrix(sample.esf$model)").AsNumericMatrix();
                //NumericVector nvResiduals = m_pEngine.Evaluate("as.numeric(sample.esf$residuals)").AsNumeric();


                for (int j = 0; j < intResultCnt; j++)
                {
                    string strSVName = lstSave.Items[j].SubItems[0].Text;
                    string strFldName = lstSave.Items[j].SubItems[1].Text;

                    // Create field, if there isn't
                    if (pDT.Columns.IndexOf(strFldName) == -1)
                    {
                        //Add fields
                        DataColumn pColumn = new DataColumn(strFldName);
                        pColumn.DataType = Type.GetType("System.Double");
                        pDT.Columns.Add(pColumn);
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Do you want to overwrite " + strFldName + " field?", "Overwrite", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.No)
                        {
                            return;
                        }
                    }

                    //Collect index of related coefficients and variables                    
                    List<int> lstCoeffIdx = new List<int>();
                    int intInterceptIdx = new int();
                    for (int k = 1; k < vecNames.Length; k++) //Do not add intercept
                    {
                        string strRelatedVar = null;

                        if (k < nIDepen + 1)
                        {
                            strRelatedVar = vecNames[k];
                            if (strRelatedVar == strSVName)
                                intInterceptIdx = k;
                        }
                        else
                        {
                            int intDivision = vecNames[k].IndexOf(":");

                            if (j == 0 && intDivision == -1)
                                lstCoeffIdx.Add(k);

                            if (intDivision != -1)
                            {
                                //strRelatedVar = vecNames[k].Substring(0, intDivision);
                                strRelatedVar = vecNames[k].Substring(1, intDivision - 1); //To consider "'";;
                            }

                            if (strRelatedVar == strSVName)
                                lstCoeffIdx.Add(k);
                        }
                    }

                    //Update Field
                    int featureIdx = 0;
                    int intFldIdx = pDT.Columns.IndexOf(strFldName);

                    foreach (DataRow row in pDT.Rows)
                    {

                        //Calculate and update spatial filter (Coefficient estimate * selected EVs)
                        double dblIntMedValue = 0;
                        double dblVaryingValue = 0;
                        if (j == 0)
                            dblVaryingValue = matCoe[0, 0];
                        else
                            dblVaryingValue = matCoe[intInterceptIdx, 0];

                        foreach (int k in lstCoeffIdx)
                        {
                            dblIntMedValue = matCoe[k, 0] * nmModel[featureIdx, k];
                            dblVaryingValue += dblIntMedValue;
                        }

                        row[intFldIdx] = dblVaryingValue;
                        featureIdx++;
                    }

                }

                //Save Result;
                if (pMapLayer is MapPointLayer)
                {
                    MapPointLayer pMapPointLyr = default(MapPointLayer);
                    pMapPointLyr = (MapPointLayer)pMapLayer;
                    pMapPointLyr.DataSet.Save();
                }
                else if (pMapLayer is MapPolygonLayer)
                {
                    MapPolygonLayer pMapPolyLyr = default(MapPolygonLayer);
                    pMapPolyLyr = (MapPolygonLayer)pMapLayer;
                    pMapPolyLyr.DataSet.Save();
                }

                pfrmRegResult.Show();
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void BinomESF(frmProgress pfrmProgress, IMapLayer pMapLayer, string strLM, int nIDepen, String[] independentNames, double dblNCandidateEvs, int intDeciPlaces, int intInterceptModel)
        {
            try
            {
                pfrmProgress.lblStatus.Text = "Selecting EVs";
                m_pEngine.Evaluate("esf.full <- glm(" + strLM + "+., data=sEV, family='binomial')");
                m_pEngine.Evaluate("esf.org <- glm(" + strLM + ", data=sEV, family='binomial')");
                m_pEngine.Evaluate("sample.esf <- stepAIC(esf.org, scope=list(upper= esf.full), direction='forward')");

                pfrmProgress.lblStatus.Text = "Printing Output:";
                m_pEngine.Evaluate("sum.esf <- summary(sample.esf)");
                //m_pEngine.Evaluate("sample.lm <- lm(" + strLM + ")");

                NumericMatrix matCoe = m_pEngine.Evaluate("as.matrix(sum.esf$coefficient)").AsNumericMatrix();
                CharacterVector vecNames = m_pEngine.Evaluate("attributes(sum.esf$coefficients)$dimnames[[1]]").AsCharacter();
                double dblNullDevi = m_pEngine.Evaluate("sum.esf$null.deviance").AsNumeric().First();
                double dblNullDF = m_pEngine.Evaluate("sum.esf$df.null").AsNumeric().First();
                double dblResiDevi = m_pEngine.Evaluate("sum.esf$deviance").AsNumeric().First();
                double dblResiDF = m_pEngine.Evaluate("sum.esf$df.residual").AsNumeric().First();

                double dblPseudoRsquared = m_pEngine.Evaluate("summary(lm(sample.esf$y~sample.esf$fitted.values))$r.squared").AsNumeric().First();


                //MC Using Pearson residual (Lin and Zhang 2007, GA) 
                if (cboAlternative.Text == "Greater")
                    m_pEngine.Evaluate("sampleresi.mc <-moran.mc(residuals(sample.esf, type='pearson'), listw =sample.listb, nsim=999, alternative = 'greater', zero.policy=TRUE)");
                else if (cboAlternative.Text == "Less")
                    m_pEngine.Evaluate("sampleresi.mc <-moran.mc(residuals(sample.esf, type='pearson'), listw =sample.listb, nsim=999, alternative = 'less', zero.policy=TRUE)");
                else
                    m_pEngine.Evaluate("sampleresi.mc <-moran.mc(residuals(sample.esf, type='pearson'), listw =sample.listb, nsim=999, alternative = 'greater', zero.policy=TRUE)");

                double dblResiMC = m_pEngine.Evaluate("sampleresi.mc$statistic").AsNumeric().First();
                double dblResiMCpVal = m_pEngine.Evaluate("sampleresi.mc$p.value").AsNumeric().First();

                if (cboAlternative.Text == "Greater")
                    m_pEngine.Evaluate("orgresi.mc <-moran.mc(residuals(esf.org, type='pearson'), listw =sample.listb, nsim=999, alternative = 'greater', zero.policy=TRUE)");
                else if (cboAlternative.Text == "Less")
                    m_pEngine.Evaluate("orgresi.mc <-moran.mc(residuals(esf.org, type='pearson'), listw =sample.listb, nsim=999, alternative = 'less', zero.policy=TRUE)");
                else
                    m_pEngine.Evaluate("orgresi.mc <-moran.mc(residuals(esf.org, type='pearson'), listw =sample.listb, nsim=999, alternative = 'greater', zero.policy=TRUE)");

                double dblResiLMMC = m_pEngine.Evaluate("orgresi.mc$statistic").AsNumeric().First();
                double dblResiLMpVal = m_pEngine.Evaluate("orgresi.mc$p.value").AsNumeric().First();
                
                NumericVector nvecNonAIC = m_pEngine.Evaluate("sample.esf$aic").AsNumeric();

                //Open Ouput form
                frmRegResult pfrmRegResult = new frmRegResult();
                pfrmRegResult.Text = "ESF Binomial Regression Summary";

                //Create DataTable to store Result
                System.Data.DataTable tblRegResult = new DataTable("ESFResult");

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

                int intNCoeff = 0;
                int intNSelectedEVs = matCoe.RowCount - nIDepen - 1;
                if (intInterceptModel == 1)
                    intNSelectedEVs = matCoe.RowCount - nIDepen;

                if (chkCoeEVs.Checked)
                    intNCoeff = matCoe.RowCount;
                else
                {
                    intNCoeff = matCoe.RowCount - intNSelectedEVs;
                }
                //Store Data Table by R result
                for (int j = 0; j < intNCoeff; j++)
                {
                    DataRow pDataRow = tblRegResult.NewRow();
                    if (j == 0 && intInterceptModel != 1)
                    {
                        pDataRow["Name"] = "(Intercept)";
                    }
                    else
                    {
                        if (chkCoeEVs.Checked)
                            pDataRow["Name"] = vecNames[j];
                        else
                        {
                            if (intInterceptModel == 1)
                                pDataRow["Name"] = independentNames[j];
                            else
                                pDataRow["Name"] = independentNames[j - 1];
                        }
                    }
                    pDataRow["Estimate"] = Math.Round(matCoe[j, 0], intDeciPlaces);
                    pDataRow["Std. Error"] = Math.Round(matCoe[j, 1], intDeciPlaces);
                    pDataRow["z value"] = Math.Round(matCoe[j, 2], intDeciPlaces);
                    pDataRow["Pr(>|z|)"] = Math.Round(matCoe[j, 3], intDeciPlaces);
                    tblRegResult.Rows.Add(pDataRow);
                }

                //Assign Datagridview to Data Table
                pfrmRegResult.dgvResults.DataSource = tblRegResult;

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
                string[] strResults = new string[7];
                strResults[0] = "Number of rows: " + nFeature.ToString() + ", Number of candidate EVs: " + dblNCandidateEvs.ToString() + ", Selected EVs: " + intNSelectedEVs.ToString();

                if (dblResiLMpVal < 0.001)
                    strResults[1] = "MC of non-ESF residuals: " + dblResiLMMC.ToString("N3") + ", p-value < 0.001";
                else if (dblResiLMpVal > 0.999)
                    strResults[1] = "MC of non-ESF residuals: " + dblResiLMMC.ToString("N3") + ", p-value > 0.999";
                else
                    strResults[1] = "MC of non-ESF residuals: " + dblResiLMMC.ToString("N3") + ", p-value: " + dblResiLMpVal.ToString("N3");

                strResults[2] = "AIC of Final Model: " + nvecNonAIC.Last().ToString(strDecimalPlaces);
                strResults[3] = "Null deviance: " + dblNullDevi.ToString(strDecimalPlaces) + " on " + dblNullDF.ToString("N0") + " degrees of freedom";
                strResults[4] = "Residual deviance: " + dblResiDevi.ToString(strDecimalPlaces) + " on " + dblResiDF.ToString("N0") + " degrees of freedom";
                strResults[5] = "Pseudo R squared: " + dblPseudoRsquared.ToString(strDecimalPlaces);

                if (dblResiMCpVal < 0.001)
                    strResults[6] = "MC of residuals: " + dblResiMC.ToString("N3") + ", p-value < 0.001";
                else if (dblResiMCpVal > 0.999)
                    strResults[6] = "MC of residuals: " + dblResiMC.ToString("N3") + ", p-value > 0.999";
                else
                    strResults[6] = "MC of residuals: " + dblResiMC.ToString("N3") + ", p-value: " + dblResiMCpVal.ToString("N3");

                pfrmRegResult.txtOutput.Lines = strResults;

                //Save Outputs in SHP
                pfrmProgress.lblStatus.Text = "Saving results:";

                int intResultCnt = lstSave.Items.Count;

                //Get EVs and residuals
                NumericMatrix nmModel = m_pEngine.Evaluate("as.matrix(sample.esf$model)").AsNumericMatrix();
                //NumericVector nvResiduals = m_pEngine.Evaluate("as.numeric(sample.esf$residuals)").AsNumeric();


                for (int j = 0; j < intResultCnt; j++)
                {
                    string strSVName = lstSave.Items[j].SubItems[0].Text;
                    string strFldName = lstSave.Items[j].SubItems[1].Text;

                    // Create field, if there isn't
                    if (pDT.Columns.IndexOf(strFldName) == -1)
                    {
                        //Add fields
                        DataColumn pColumn = new DataColumn(strFldName);
                        pColumn.DataType = Type.GetType("System.Double");
                        pDT.Columns.Add(pColumn);
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Do you want to overwrite " + strFldName + " field?", "Overwrite", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.No)
                        {
                            return;
                        }
                    }

                    //Collect index of related coefficients and variables                    
                    List<int> lstCoeffIdx = new List<int>();
                    int intInterceptIdx = new int();
                    for (int k = 1; k < vecNames.Length; k++) //Do not add intercept
                    {
                        string strRelatedVar = null;

                        if (k < nIDepen + 1)
                        {
                            strRelatedVar = vecNames[k];
                            if (strRelatedVar == strSVName)
                                intInterceptIdx = k;
                        }
                        else
                        {
                            int intDivision = vecNames[k].IndexOf(":");

                            if (j == 0 && intDivision == -1)
                                lstCoeffIdx.Add(k);

                            if (intDivision != -1)
                            {
                                //strRelatedVar = vecNames[k].Substring(0, intDivision);
                                strRelatedVar = vecNames[k].Substring(1, intDivision - 1); //To consider "'";;
                            }

                            if (strRelatedVar == strSVName)
                                lstCoeffIdx.Add(k);
                        }
                    }

                    //Update Field
                    int featureIdx = 0;
                    int intFldIdx = pDT.Columns.IndexOf(strFldName);

                    foreach (DataRow row in pDT.Rows)
                    {

                        //Calculate and update spatial filter (Coefficient estimate * selected EVs)
                        double dblIntMedValue = 0;
                        double dblVaryingValue = 0;
                        if (j == 0)
                            dblVaryingValue = matCoe[0, 0];
                        else
                            dblVaryingValue = matCoe[intInterceptIdx, 0];

                        foreach (int k in lstCoeffIdx)
                        {
                            dblIntMedValue = matCoe[k, 0] * nmModel[featureIdx, k];
                            dblVaryingValue += dblIntMedValue;
                        }

                        row[intFldIdx] = dblVaryingValue;
                        featureIdx++;
                    }

                }

                //Save Result;
                if (pMapLayer is MapPointLayer)
                {
                    MapPointLayer pMapPointLyr = default(MapPointLayer);
                    pMapPointLyr = (MapPointLayer)pMapLayer;
                    pMapPointLyr.DataSet.Save();
                }
                else if (pMapLayer is MapPolygonLayer)
                {
                    MapPolygonLayer pMapPolyLyr = default(MapPolygonLayer);
                    pMapPolyLyr = (MapPolygonLayer)pMapLayer;
                    pMapPolyLyr.DataSet.Save();
                }

                pfrmRegResult.Show();
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void lstIndeVar_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateListview(lstSave, m_dt);
        }

        private void lstIndeVar_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateListview(lstSave, m_dt);
        }

        private void lstFields_DoubleClick(object sender, EventArgs e)
        {
            m_pSnippet.MoveSelectedItemsinListBoxtoOtherCheckedListBox(lstFields, lstIndeVar);
            UpdateListview(lstSave, m_dt);
        }

        private void lstIndeVar_DoubleClick(object sender, EventArgs e)
        {
            m_pSnippet.MoveSelectedItemsinCheckedListBoxtoOtherListBox(lstIndeVar, lstFields);
            UpdateListview(lstSave, m_dt);
        }

        private DialogResult PopupInput(ListViewItem.ListViewSubItem pSelectedSubItems, int border, int length, ref string output)
        {

            System.Drawing.Point ctrlpt = pSelectedSubItems.Bounds.Location;
            ctrlpt = this.PointToScreen(pSelectedSubItems.Bounds.Location);
            ctrlpt.Y += grbSave.Location.Y + 27;
            ctrlpt.X += grbSave.Location.X + 27 + (length / 2);
            //ctrlpt.Y += 411;
            //ctrlpt.X += 28 + (length / 2);
            TextBox input = new TextBox { Height = 20, Width = length, Top = border / 2, Left = border / 2 };
            input.BorderStyle = BorderStyle.FixedSingle;
            input.Text = output;
            //######## SetColor to your preference
            input.BackColor = Color.Azure;

            Button btnok = new Button { DialogResult = System.Windows.Forms.DialogResult.OK, Top = 25 };
            Button btncn = new Button { DialogResult = System.Windows.Forms.DialogResult.Cancel, Top = 25 };

            Form frm = new Form { ControlBox = false, AcceptButton = btnok, CancelButton = btncn, StartPosition = FormStartPosition.Manual, Location = ctrlpt };
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //######## SetColor to your preference
            frm.BackColor = Color.Black;

            RectangleF rec = new RectangleF(0, 0, (length + border), (20 + border));
            System.Drawing.Drawing2D.GraphicsPath GP = new System.Drawing.Drawing2D.GraphicsPath(); //GetRoundedRect(rec, 4.0F);
            float diameter = 8.0F;
            SizeF sizef = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(rec.Location, sizef);
            GP.AddArc(arc, 180, 90);
            arc.X = rec.Right - diameter;
            GP.AddArc(arc, 270, 90);
            arc.Y = rec.Bottom - diameter;
            GP.AddArc(arc, 0, 90);
            arc.X = rec.Left;
            GP.AddArc(arc, 90, 90);
            GP.CloseFigure();

            frm.Region = new Region(GP);
            frm.Controls.AddRange(new Control[] { input, btncn, btnok });
            DialogResult rst = frm.ShowDialog();
            output = input.Text;
            return rst;
        }

        private void lstSave_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                ListViewItem.ListViewSubItem pSelectedSubItems = null;
                //selection = lvSymbol.GetItemAt(e.X, e.Y);
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (lstSave.Items[i].SubItems[j].Bounds.Contains(e.Location))
                        {
                            pSelectedSubItems = lstSave.Items[i].SubItems[j];
                            if (j == 1)
                            {
                                string var = pSelectedSubItems.Text;

                                int intLength = var.Length * 6 + 30;

                                if (PopupInput(pSelectedSubItems, 2, intLength, ref var) == System.Windows.Forms.DialogResult.OK)
                                {
                                    lstSave.Items[i].SubItems[j].Text = var;
                                }
                            }

                        }
                    }
                }

                lstSave.Update();
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void cboFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (cboTargetLayer.Text != "" && cboFamily.Text != "")
                {
                    if (cboFamily.Text == "Linear (Gaussian)" || cboFamily.Text == "Logistic")
                    {
                        cboNormalization.Enabled = false;
                        cboNormalization.Text = "";
                        lblNorm.Enabled = false;
                    }
                    else
                    {
                        cboNormalization.Enabled = true;
                        lblNorm.Enabled = true;
                        if (cboFamily.Text == "Binomial")
                            lblNorm.Text = "Normalization";
                        else
                            lblNorm.Text = "Offset";

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

                    for (int i = 0; i < m_dt.Columns.Count; i++)
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
                    lstIndeVar.SetItemChecked(0, true);
                    lstSave.Items.Clear();
                    UpdateListview(lstSave, m_dt);
                }
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
