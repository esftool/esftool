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
    public partial class frmESFResult : Form
    {
        public Double[,] arrSelectedEVs;
        public String[] arrSelectedEvsNM;
        public IMapLayer m_pMaplayer;

        public frmESFResult()
        {
            InitializeComponent();
        }

        private void btnESFSave_Click(object sender, EventArgs e)
        {
            frmSaveESF pfrmSaveESF = new frmSaveESF();
            pfrmSaveESF.arrSelectedEVs = arrSelectedEVs;
            pfrmSaveESF.arrSelectedEvsNM = arrSelectedEvsNM;
            pfrmSaveESF.m_pMaplayer = m_pMaplayer;

            pfrmSaveESF.Show();
        }

        private void frmESFResult_Load(object sender, EventArgs e)
        {
            if (m_pMaplayer == null)
            {
                btnESFSave.Enabled = false;
            }
        }
    }
}
