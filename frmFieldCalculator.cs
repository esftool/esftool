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
    public partial class frmFieldCalculator : Form
    {
        private MainForm m_pForm;
        private IMapLayer m_pMaplayer;
        private DataTable m_dt = null;
        private clsSnippet m_pSnippet;

        public frmFieldCalculator()
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

                lstFields.Items.Clear();

                for (int i = 0; i < m_dt.Columns.Count; i++) //No restriction for a field type
                {
                    if (FindNumberFieldType(m_dt.Columns[i]))
                    {
                        lstFields.Items.Add(m_dt.Columns[i].ColumnName);
                    }
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
                if (column.DataType == Type.GetType("System.String") || column.DataType == Type.GetType("System.Boolean") || column.DataType == Type.GetType("System.DateTime"))
                    blnNField = false;

                return blnNField;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception:" + ex.Message);
                return false;
            }
        }

        private void btnAddTarget_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstFields.SelectedItems.Count > 0)
                {
                    txtTargetField.Text = lstFields.Items[lstFields.SelectedIndex].ToString();
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void btnAddExpression_Click(object sender, EventArgs e)
        {
            AddFieldNametoExpression();
        }

        private void AddFieldNametoExpression()
        {
            try
            {
                if (lstFields.SelectedItems.Count > 0)
                {
                    txtNExpression.Text = txtNExpression.Text + (string)lstFields.Items[lstFields.SelectedIndex];
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {

                string strLayerName = cboLayer.Text;
                string strExpression = txtNExpression.Text;
                string strTargetFld = txtTargetField.Text;

                if (strTargetFld == "")
                {
                    MessageBox.Show("Please select the target field");
                    return;
                }
                else if (strExpression == "")
                {
                    MessageBox.Show("Please insert expression to calculate field");
                    return;
                }

                REngine pEngine = m_pForm.pEngine;
                // Split the text into an array of words
                string[] words = strExpression.Split(new char[] { ',', '$', '(', ')', '/', '*', '+', '-', '^', '=', ' ' }, StringSplitOptions.RemoveEmptyEntries); //Any other splitters?

                //Get number and names of Expressed fields
                int intNField = lstFields.Items.Count;
                int intNExpressedFld = 0;

                string[] fieldNames = new string[intNField];

                for (int j = 0; j < intNField; j++)
                {
                    string strTemp = (string)lstFields.Items[j];
                    if (words.Contains(strTemp))
                    {
                        fieldNames[intNExpressedFld] = (string)lstFields.Items[j];
                        intNExpressedFld++;
                    }
                }

                //Get Features
                int nFeature = m_dt.Rows.Count;

                //Get index for target and source fields
                int intTargetIdx = m_dt.Columns.IndexOf(strTargetFld);

                NumericVector vecResult = null;
                bool blnINF = false;
                bool blnNAN = false;
                LogicalVector vecINF = null;
                LogicalVector vecNAN = null;

                //Get values from selected fields
                if (intNExpressedFld > 0)
                {
                    int[] idxes = new int[intNExpressedFld];
                    for (int j = 0; j < intNExpressedFld; j++)
                    {
                        idxes[j] = m_dt.Columns.IndexOf(fieldNames[j]);
                    }

                    //Store values in source fields into Array
                    //double[,] arrSource = new double[nFeature, intNExpressedFld];
                    double[][] arrSource = new double[intNExpressedFld][];

                    for (int j = 0; j < intNExpressedFld; j++)
                    {
                        arrSource[j] = new double[nFeature];
                    }

                    int i = 0;
                    foreach (DataRow row in m_dt.Rows)
                    {
                        for (int j = 0; j < intNExpressedFld; j++)
                        {
                            arrSource[j][i] = Convert.ToDouble(row[idxes[j]]);
                        }

                        i++;
                    }

                    //Source value to R vector
                    for (int j = 0; j < intNExpressedFld; j++)
                    {
                        //double[] arrVector = arrSource.GetColumn<double>(j);
                        NumericVector vecSource = pEngine.CreateNumericVector(arrSource[j]);
                        pEngine.SetSymbol(fieldNames[j], vecSource);
                    }

                    //Verifying before evaluation
                    vecINF = pEngine.Evaluate("is.infinite(" + strExpression + ")").AsLogical();
                    vecNAN = pEngine.Evaluate("is.nan(" + strExpression + ")").AsLogical();

                    for (int k = 0; k < vecINF.Length; k++)
                    {
                        blnINF = vecINF[k];
                        blnNAN = vecNAN[k];
                        if (blnINF)
                        {
                            MessageBox.Show("Fail to calculate due to infinite-values");
                            return;
                        }
                        else if (blnNAN)
                        {
                            MessageBox.Show("Fail to calculate due to NAN");
                            return;
                        }
                    }

                    //Calculate
                    vecResult = pEngine.Evaluate(strExpression).AsNumeric();
                }
                else if (intNExpressedFld == 0) //Constant
                {
                    //Verifying before evaluation
                    blnINF = pEngine.Evaluate("is.infinite(" + strExpression + ")").AsLogical().First();
                    blnNAN = pEngine.Evaluate("is.nan(" + strExpression + ")").AsLogical().First();
                    if (blnINF)
                    {
                        MessageBox.Show("Fail to calculate due to infinite-values");
                        return;
                    }
                    else if (blnNAN)
                    {
                        MessageBox.Show("Fail to calculate due to NAN");
                        return;
                    }

                    vecResult = pEngine.Evaluate("rep(" + strExpression + ", " + nFeature.ToString() + ")").AsNumeric();
                }

                //Update Field
                int featureIdx = 0;

                try
                {
                    foreach (DataRow row in m_dt.Rows)
                    {
                        //Update Residuals
                        row[intTargetIdx] = vecResult[featureIdx];
                        featureIdx++;
                    }

                }
                catch
                {
                    MessageBox.Show("Fail to update field values. The field type might not support this calculation.", "Field Calcualation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Done");
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

        private void lstFields_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lstFields.SelectedItems.Count > 0)
                {
                    if (txtTargetField.Text == "")
                        txtTargetField.Text = (string)lstFields.Items[lstFields.SelectedIndex];
                    else
                        AddFieldNametoExpression();
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }
    }
}
