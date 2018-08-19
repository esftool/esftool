using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DotSpatial.Controls;
using System.Windows.Forms;
using RDotNet;
using System.Data;
using System.IO;

namespace ESFTool
{
    public class clsSnippet
    {
        public bool FindNumberFieldType(DataColumn column)
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

        public void MoveSelectedItemsinListBoxtoOtherListBox(ListBox FromListBox, ListBox ToListBox)
        {
            try
            {
                int intSelectedItems = FromListBox.SelectedItems.Count;
                if (intSelectedItems > 0)
                {
                    for (int i = 0; i < intSelectedItems; i++)
                    {
                        ToListBox.Items.Add(FromListBox.SelectedItems[0]);
                        FromListBox.Items.Remove(FromListBox.SelectedItems[0]);
                    }
                    FromListBox.ClearSelected();
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception:" + ex.Message);
                return;
            }
        }
        public void MoveSelectedItemsinListBoxtoOtherCheckedListBox(ListBox FromListBox, CheckedListBox ToListBox)
        {
            try
            {
                int intSelectedItems = FromListBox.SelectedItems.Count;
                int intToItemCount = ToListBox.Items.Count;
                if (intSelectedItems > 0)
                {
                    for (int i = 0; i < intSelectedItems; i++)
                    {
                        ToListBox.Items.Add(FromListBox.SelectedItems[0]);
                        FromListBox.Items.Remove(FromListBox.SelectedItems[0]);
                        
                    }
                    FromListBox.ClearSelected();
                }
                else
                    return;
                ToListBox.SetItemChecked(intToItemCount, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception:" + ex.Message);
                return;
            }
        }
        public void MoveSelectedItemsinCheckedListBoxtoOtherListBox(CheckedListBox FromListBox, ListBox ToListBox)
        {
            try
            {
                int intSelectedItems = FromListBox.SelectedItems.Count;
                if (intSelectedItems > 0)
                {
                    for (int i = 0; i < intSelectedItems; i++)
                    {
                        ToListBox.Items.Add(FromListBox.SelectedItems[0]);
                        FromListBox.Items.Remove(FromListBox.SelectedItems[0]);
                    }
                    FromListBox.ClearSelected();
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception:" + ex.Message);
                return;
            }
        }
        public string FilePathinRfromLayer(IMapLayer pMapLayer)
        {
            try
            {
                string strfullname = pMapLayer.DataSet.Filename;
                string strNameR = strfullname.Replace(@"\", @"/");
                return strNameR;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception:" + ex.Message);
                return null;
            }
        }
        public class SpatialWeightMatrixType
        {
            //Polygon 
            public string strPolySWM = "Contiguity (Queen)"; //Default
            public string[] strPolyDefs = new string[] { "Contiguity (Queen)", "Contiguity (Rook)" };

            //Points
            public string strPointSWM = "Delaunay triangulation";
            public string[] strPointDef = new string[] { "Delaunay triangulation", "Distance", "k-Nearest neigbors" };
        }

        //Creating Spatial Weight Matrix
        public int CreateSpatialWeightMatrix(REngine pEngine, IMapLayer pMapLayer, string strSWMtype, frmProgress pfrmProgress)
        {
            try
            {
                //Return 0, means fails to create spatial weight matrix, 1 means success.

                SpatialWeightMatrixType pSWMType = new SpatialWeightMatrixType();

                if (pMapLayer is MapPolygonLayer)
                {

                    if (strSWMtype == pSWMType.strPolyDefs[0])
                    {
                        pEngine.Evaluate("sample.nb <- poly2nb(sample.shp, queen=T)");
                    }
                    else if (strSWMtype == pSWMType.strPolyDefs[1])
                    {
                        pEngine.Evaluate("sample.nb <- poly2nb(sample.shp, queen=F)");
                    }
                    else
                    {
                        pEngine.Evaluate("sample.nb <- poly2nb(sample.shp, queen=T)");
                    }
                    //For dealing empty neighbors
                    try
                    {
                        pEngine.Evaluate("sample.listw <- nb2listw(sample.nb, style='W');sample.listb <- nb2listw(sample.nb, style='B')");
                    }
                    catch
                    {
                        DialogResult dialogResult = MessageBox.Show("Empty neighbor sets are founded. Do you want to continue?", "Empty neighbor", MessageBoxButtons.YesNo);


                        if (dialogResult == DialogResult.Yes)
                        {
                            pEngine.Evaluate("sample.listw <- nb2listw(sample.nb, style='W', zero.policy=TRUE);sample.listb <- nb2listw(sample.nb, style='B', zero.policy=TRUE)");
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            pfrmProgress.Close();
                            return 0;
                        }
                    }
                }
                else if (pMapLayer is MapPointLayer)
                {
                    FormCollection pFormCollection = System.Windows.Forms.Application.OpenForms;
                    bool blnOpen = false;
                    int intIdx = 0;

                    for (int j = 0; j < pFormCollection.Count; j++)
                    {
                        if (pFormCollection[j].Name == "frmSubsetPoly")//Brushing to Histogram
                        {
                            intIdx = j;

                            blnOpen = true;
                        }
                    }

                    if (blnOpen) //Delaunay with clipping
                    {
                        frmSubsetPoly pfrmSubsetPoly1 = pFormCollection[intIdx] as frmSubsetPoly;
                        if (pfrmSubsetPoly1.m_blnSubset)
                        {
                            string strPolypathR = FilePathinRfromLayer(pfrmSubsetPoly1.m_pMaplayer);

                            pEngine.Evaluate("sample.sub.shp <- readShapePoly('" + strPolypathR + "')");

                            pEngine.Evaluate("sample.nb <- del.subset(sample.shp, sample.sub.shp)");
                            bool blnError = pEngine.Evaluate("nrow(sample.shp) == length(sample.nb)").AsLogical().First();

                            if (blnError == false)
                            {
                                MessageBox.Show("The number of features in points and the rows of neighbors is not matched.", "Error");
                                return 0;
                            }
                            else
                            {
                                try
                                {
                                    pEngine.Evaluate("sample.listw <- nb2listw(sample.nb, style='W'); sample.listb <- nb2listw(sample.nb, style='B')");
                                }
                                catch
                                {
                                    DialogResult dialogResult = MessageBox.Show("Empty neighbor sets are founded. Do you want to continue?", "Empty neighbor", MessageBoxButtons.YesNo);


                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        pEngine.Evaluate("sample.listw <- nb2listw(sample.nb, style='W', zero.policy=TRUE);sample.listb <- nb2listw(sample.nb, style='B', zero.policy=TRUE)");
                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {
                                        pfrmProgress.Close();
                                        return 0;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        pEngine.Evaluate("sample.nb <- tri2nb(coordinates(sample.shp))");
                        //For dealing empty neighbors

                        try
                        {
                            pEngine.Evaluate("sample.listw <- nb2listw(sample.nb, style='W'); sample.listb <- nb2listw(sample.nb, style='B')");
                        }
                        catch
                        {
                            DialogResult dialogResult = MessageBox.Show("Empty neighbor sets are founded. Do you want to continue?", "Empty neighbor", MessageBoxButtons.YesNo);


                            if (dialogResult == DialogResult.Yes)
                            {
                                pEngine.Evaluate("sample.listw <- nb2listw(sample.nb, style='W', zero.policy=TRUE);sample.listb <- nb2listw(sample.nb, style='B', zero.policy=TRUE)");
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                pfrmProgress.Close();
                                return 0;
                            }
                        }
                    }
                }
                else
                {
                    int intResult = SWMusingGAL(pEngine, pMapLayer, strSWMtype);
                    if (intResult == -1)
                    {
                        pfrmProgress.Close();
                        return 0;
                    }

                }
                return 1;
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return 0;
            }
        }

        public int SWMusingGAL(REngine pEngine, IMapLayer pMaplayer, string strGALPath)
        {
            try
            {
                
                int intResult = 0;
                string strExtension = strGALPath.Substring(strGALPath.Length - 3);
                string strRegionFld = null;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(strGALPath))
                {
                    String line = sr.ReadLine();
                    int inttemp = line.LastIndexOf(" ");
                    strRegionFld = line.Substring(inttemp + 1);
                    Console.WriteLine(line);
                }
                DataTable dt = null;

                if (pMaplayer is MapPointLayer)
                {
                    MapPointLayer pMapPointLyr = default(MapPointLayer);
                    pMapPointLyr = (MapPointLayer)pMaplayer;
                    dt = pMapPointLyr.DataSet.DataTable;
                }
                else if (pMaplayer is MapPolygonLayer)
                {
                    MapPolygonLayer pMapPolyLyr = default(MapPolygonLayer);
                    pMapPolyLyr = (MapPolygonLayer)pMaplayer;
                    dt = pMapPolyLyr.DataSet.DataTable;
                }

                
                if (dt.Columns.IndexOf(strRegionFld) == -1)
                {
                    if (strExtension == "gal")
                    {
                        if (strRegionFld == "POLY_ID")
                            MessageBox.Show("Unique IDs are overrided.");
                        else
                        {
                            MessageBox.Show("GAL file is invalid; When you create spatial weight matrix in GeoDa, you need to specify ID field, or use default ID field name (POLY_ID)");

                        }
                    }
                    else if (strExtension == "gwt")
                    {
                        MessageBox.Show("GWT file is invalid; When you create spatial weight matrix in GeoDa, you should specify ID field");
                        intResult = -1;
                        return intResult;
                    }
                }


                string strGALpathR = strGALPath.Replace(@"\", @"/");
                if (strExtension == "gal")
                {
                    string strTemp = null;
                    if (strRegionFld == "FID" || strRegionFld == "POLY_ID")
                    {
                        strTemp = "sample.nb <- read.gal('" + strGALpathR + "', override.id = TRUE)";
                    }
                    else
                    {
                        strTemp = "sample.nb <- read.gal('" + strGALpathR + "', region.id = sample.shp@data$" + strRegionFld + ")";
                    }
                    pEngine.Evaluate(strTemp);

                    bool blnSymmetric = pEngine.Evaluate("is.symmetric.nb(sample.nb)").AsLogical().First();
                    if (blnSymmetric == false)
                    {
                        DialogResult dialogResult = MessageBox.Show("This spatial weight matrix is asymmetric. Some functions are restricted in an asymmetric matrix. Do you want to continue?", "Asymmetric spatial weight matrix", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.No)
                        {
                            intResult = -1;
                            return intResult;
                        }
                    }

                    pEngine.Evaluate("sample.listw <- nb2listw(sample.nb, style='W', zero.policy=TRUE);sample.listb <- nb2listw(sample.nb, style='B', zero.policy=TRUE)");

                }
                else if (strExtension == "gwt")
                {
                    pEngine.Evaluate("sample.nb <- read.gwt2nb('" + strGALpathR + "', region.id = sample.shp@data$" + strRegionFld + ")");

                    bool blnSymmetric = pEngine.Evaluate("is.symmetric.nb(sample.nb)").AsLogical().First();
                    if (blnSymmetric == false)
                    {
                        DialogResult dialogResult = MessageBox.Show("This spatial weight matrix is asymmetric. Some functions are restricted in an asymmetric matrix. Do you want to continue?", "Asymmetric spatial weight matrix", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.No)
                        {
                            intResult = -1;
                            return intResult;
                        }
                    }

                    pEngine.Evaluate("sample.listw <- nb2listw(sample.nb, style='W', zero.policy=TRUE);sample.listb <- nb2listw(sample.nb, style='B', zero.policy=TRUE)");
                }


                return intResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
                return -1;
            }
        }

        public int CreateSpatialWeightMatrixPoly(REngine pEngine, IMapLayer pMapLayer, string strSWMtype, frmProgress pfrmProgress, double dblAdvancedValue, bool blnCumul)
        {
            try
            {
                //Return 0, means fails to create spatial weight matrix, 1 means success.

                SpatialWeightMatrixType pSWMType = new SpatialWeightMatrixType();

                if (pMapLayer is MapPolygonLayer)
                {
                    if (strSWMtype == pSWMType.strPolyDefs[0])
                    {
                        pEngine.Evaluate("sample.nb <- poly2nb(sample.shp, queen=T)");
                    }
                    else if (strSWMtype == pSWMType.strPolyDefs[1])
                    {
                        pEngine.Evaluate("sample.nb <- poly2nb(sample.shp, queen=F)");
                    }

                    if (dblAdvancedValue > 1)
                    {
                        try
                        {
                            pEngine.Evaluate("sample.nblags <- nblag(sample.nb, maxlag = " + dblAdvancedValue.ToString() + ")");
                        }
                        catch
                        {
                            MessageBox.Show("Please reduce the maximum lag order");
                        }

                        if (blnCumul)
                            pEngine.Evaluate("sample.nb <- nblag_cumul(sample.nblags)");
                        else
                            pEngine.Evaluate("sample.nb <- sample.nblags[[" + dblAdvancedValue.ToString() + "]]");

                    }

                    //For dealing empty neighbors
                    try
                    {
                        pEngine.Evaluate("sample.listw <- nb2listw(sample.nb, style='W');sample.listb <- nb2listw(sample.nb, style='B')");
                    }
                    catch
                    {
                        DialogResult dialogResult = MessageBox.Show("Empty neighbor sets are founded. Do you want to continue?", "Empty neighbor", MessageBoxButtons.YesNo);


                        if (dialogResult == DialogResult.Yes)
                        {
                            pEngine.Evaluate("sample.listw <- nb2listw(sample.nb, style='W', zero.policy=TRUE);sample.listb <- nb2listw(sample.nb, style='B', zero.policy=TRUE)");
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            pfrmProgress.Close();
                            return 0;
                        }
                    }
                }
                else
                {
                    int intResult = SWMusingGAL(pEngine, pMapLayer, strSWMtype);
                    if (intResult == -1)
                    {
                        pfrmProgress.Close();
                        return 0;
                    }

                }
                return 1;
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return 0;
            }
        }//For only polygons

        public int CreateSpatialWeightMatrixPts(REngine pEngine, IMapLayer pMapLayer, string strSWMtype, frmProgress pfrmProgress, double dblAdvancedValue, bool blnCumul)//For only point dataset
        {
            try
            {
                //Return 0, means fails to create spatial weight matrix, 1 means success.

                SpatialWeightMatrixType pSWMType = new SpatialWeightMatrixType();

                if (pMapLayer is MapPointLayer)
                {
                    #region Delaunay
                    if (strSWMtype == pSWMType.strPointDef[0])
                    {

                        if (blnCumul == false)
                        {
                            pEngine.Evaluate("sample.nb <- tri2nb(coordinates(sample.shp))");
                            //For dealing empty neighbors

                            try
                            {
                                pEngine.Evaluate("sample.listw <- nb2listw(sample.nb, style='W'); sample.listb <- nb2listw(sample.nb, style='B')");
                            }
                            catch
                            {
                                DialogResult dialogResult = MessageBox.Show("Empty neighbor sets are founded. Do you want to continue?", "Empty neighbor", MessageBoxButtons.YesNo);


                                if (dialogResult == DialogResult.Yes)
                                {
                                    pEngine.Evaluate("sample.listw <- nb2listw(sample.nb, style='W', zero.policy=TRUE);sample.listb <- nb2listw(sample.nb, style='B', zero.policy=TRUE)");
                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                    pfrmProgress.Close();
                                    return 0;
                                }
                            }
                        }
                        else
                        {
                            if (pMapLayer == null)
                                return 0;

                            MainForm pForm = System.Windows.Forms.Application.OpenForms["MainForm"] as MainForm;

                            string strStartPath = pForm.strPath;
                            string pathr = strStartPath.Replace(@"\", @"/");
                            pEngine.Evaluate("source('" + pathr + "/del.subset.R')");

                            try
                            {
                                pEngine.Evaluate("library(deldir); library(rgeos)");

                            }
                            catch
                            {
                                MessageBox.Show("Please checked R packages installed in your local computer.");
                                return 0;
                            }


                            string strPolypathR = FilePathinRfromLayer(pMapLayer);

                            pEngine.Evaluate("sample.sub.shp <- readShapePoly('" + strPolypathR + "')");

                            pEngine.Evaluate("sample.nb <- del.subset(sample.shp, sample.sub.shp)");
                            bool blnError = pEngine.Evaluate("nrow(sample.shp) == length(sample.nb)").AsLogical().First();

                            if (blnError == false)
                            {
                                MessageBox.Show("The number of features in points and the rows of neighbors is not matched.", "Error");
                                return 0;
                            }
                            else
                            {
                                try
                                {
                                    pEngine.Evaluate("sample.listw <- nb2listw(sample.nb, style='W'); sample.listb <- nb2listw(sample.nb, style='B')");
                                }
                                catch
                                {
                                    DialogResult dialogResult = MessageBox.Show("Empty neighbor sets are founded. Do you want to continue?", "Empty neighbor", MessageBoxButtons.YesNo);


                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        pEngine.Evaluate("sample.listw <- nb2listw(sample.nb, style='W', zero.policy=TRUE);sample.listb <- nb2listw(sample.nb, style='B', zero.policy=TRUE)");
                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {
                                        return 0;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    else if (strSWMtype == pSWMType.strPointDef[1])
                    {
                        pEngine.Evaluate("sample.nb <- dnearneigh(coordinates(sample.shp), 0, " + dblAdvancedValue.ToString() + ")");
                        //For dealing empty neighbors
                        if (pEngine.Evaluate("sum(card(sample.nb)) < 1").AsLogical().First())
                        {
                            MessageBox.Show("There are too many empty neighbors");
                            pfrmProgress.Close();
                            return 0;
                        }

                        try
                        {
                            pEngine.Evaluate("sample.listw <- nb2listw(sample.nb, style='W'); sample.listb <- nb2listw(sample.nb, style='B')");
                        }
                        catch
                        {
                            DialogResult dialogResult = MessageBox.Show("Empty neighbor sets are founded. Do you want to continue?", "Empty neighbor", MessageBoxButtons.YesNo);


                            if (dialogResult == DialogResult.Yes)
                            {
                                pEngine.Evaluate("sample.listw <- nb2listw(sample.nb, style='W', zero.policy=TRUE);sample.listb <- nb2listw(sample.nb, style='B', zero.policy=TRUE)");
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                pfrmProgress.Close();
                                return 0;
                            }
                        }
                        finally
                        {
                            pfrmProgress.Close();
                            MessageBox.Show("Fail to create spatial weights matrix");
                        }
                    }
                    else if (strSWMtype == pSWMType.strPointDef[2])
                    {
                        pEngine.Evaluate("col.knn <- knearneigh(coordinates(sample.shp), k=" + dblAdvancedValue.ToString() + ")");
                        pEngine.Evaluate("sample.nb <- knn2nb(col.knn)");
                        //For dealing empty neighbors

                        bool blnSymmetric = pEngine.Evaluate("is.symmetric.nb(sample.nb)").AsLogical().First();
                        if (blnSymmetric == false)
                        {
                            DialogResult dialogResult = MessageBox.Show("This spatial weight matrix is asymmetric. Some functions are restricted in an asymmetric matrix. Do you want to continue?", "Asymmetric spatial weight matrix", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.No)
                            {
                                pfrmProgress.Close();
                                return 0;
                            }
                        }

                        try
                        {
                            pEngine.Evaluate("sample.listw <- nb2listw(sample.nb, style='W'); sample.listb <- nb2listw(sample.nb, style='B')");
                        }
                        catch
                        {
                            DialogResult dialogResult = MessageBox.Show("Empty neighbor sets are founded. Do you want to continue?", "Empty neighbor", MessageBoxButtons.YesNo);


                            if (dialogResult == DialogResult.Yes)
                            {
                                pEngine.Evaluate("sample.listw <- nb2listw(sample.nb, style='W', zero.policy=TRUE);sample.listb <- nb2listw(sample.nb, style='B', zero.policy=TRUE)");
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                pfrmProgress.Close();
                                return 0;
                            }
                        }
                    }

                }
                else
                {
                    int intResult = SWMusingGAL(pEngine, pMapLayer, strSWMtype);
                    if (intResult == -1)
                    {
                        pfrmProgress.Close();
                        return 0;
                    }

                }
                return 1;
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return 0;
            }
        }

        public void drawPlottoForm(string strTitle, string strCommand)
        {
            try
            {
                MainForm mForm = Application.OpenForms["MainForm"] as MainForm;
                REngine pEngine = mForm.pEngine;
                //Create Plots in R
                StringBuilder CommandPlot = new StringBuilder();

                //Plots are saved in temporary folders. 
                string path = Path.GetTempPath();
                //Have to assing pathes differently at R and ArcObject
                string pathr = path.Replace(@"\", @"/");
                //Remove existing image file pathes
                if (mForm.multipageImage == null)
                    mForm.multipageImage = new List<string>();
                else
                    mForm.multipageImage.Clear();

                //Delete existing image files
                mForm.multipageImage.AddRange(Directory.GetFiles(path, "rnet*.wmf"));

                for (int j = 0; j < mForm.multipageImage.Count; j++)
                {
                    FileInfo pinfo = new FileInfo(mForm.multipageImage[j]);
                    if (pinfo.Exists)
                        pinfo.Delete();
                    pinfo.Refresh();
                }

                //Load Form and assign the settings
                frmPlot pfrmPlot = new frmPlot();
                pfrmPlot.Text = strTitle;
                pfrmPlot.Show();
                string strwidth = pfrmPlot.picPlot.Size.Width.ToString();
                string strHeight = pfrmPlot.picPlot.Size.Height.ToString();

                //Create Plots in R
                CommandPlot.Append("win.metafile('" + pathr + "rnet%01d.wmf');");
                CommandPlot.Append(strCommand);
                CommandPlot.Append("graphics.off()");
                pEngine.Evaluate(CommandPlot.ToString());

                //Add Plot pathes at List
                mForm.multipageImage.Clear();
                mForm.multipageImage.AddRange(Directory.GetFiles(path, "rnet*.wmf"));

                //Draw plots at the Form
                mForm.intCurrentIdx = 0;
                drawCurrentChart(mForm.multipageImage, mForm.intCurrentIdx, pfrmPlot);
                enableButtons(mForm.multipageImage, mForm.intCurrentIdx, pfrmPlot);
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }
        public void drawCurrentChart(System.Collections.Generic.List<string> multipageImage, int currIndex, frmPlot mPlot)
        {
            try
            {
                if (multipageImage.Count > 0)
                {
                    using (System.IO.StreamReader str = new System.IO.StreamReader(multipageImage[currIndex]))
                    {
                        //mPlot.picPlot.Image = new System.Drawing.Bitmap(str.BaseStream);
                        mPlot.picPlot.Image = new System.Drawing.Imaging.Metafile(str.BaseStream);
                        str.Close();
                    }
                    mPlot.picPlot.Invalidate();
                }
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }

        }
        public void enableButtons(System.Collections.Generic.List<string> multipageImage, int intCurrentIdx, frmPlot pfrmPlot)
        {
            try
            {
                if (intCurrentIdx > 0)
                    pfrmPlot.btnPreviousPlot.Enabled = true;
                else
                    pfrmPlot.btnPreviousPlot.Enabled = false;

                if (intCurrentIdx >= multipageImage.Count - 1)
                    pfrmPlot.btnNextPlot.Enabled = false;
                else
                    pfrmPlot.btnNextPlot.Enabled = true;
            }
            catch (Exception ex)
            {
                frmErrorLog pfrmErrorLog = new frmErrorLog(); pfrmErrorLog.ex = ex; pfrmErrorLog.ShowDialog();
                return;
            }
        }

    }
}
