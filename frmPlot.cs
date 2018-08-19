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
    public partial class frmPlot : Form
    {
        private MainForm m_pForm;
        //private List<string> pmultipageImage;
        private clsSnippet m_pSnippet = new clsSnippet();


        public frmPlot()
        {
            InitializeComponent();
            m_pForm = Application.OpenForms["MainForm"] as MainForm;
            m_pSnippet = new clsSnippet();
        }

        private void btnNextPlot_Click(object sender, EventArgs e)
        {
            m_pForm.intCurrentIdx++;
            m_pSnippet.drawCurrentChart(m_pForm.multipageImage, m_pForm.intCurrentIdx, this);

            if (m_pForm.intCurrentIdx == m_pForm.multipageImage.Count - 1)
                btnNextPlot.Enabled = false;
            else
                btnNextPlot.Enabled = true;

            if (m_pForm.intCurrentIdx > 0)
                btnPreviousPlot.Enabled = true;
            else
                btnPreviousPlot.Enabled = false;
        }

        private void btnPreviousPlot_Click(object sender, EventArgs e)
        {
            m_pForm.intCurrentIdx--;
            m_pSnippet.drawCurrentChart(m_pForm.multipageImage, m_pForm.intCurrentIdx, this);

            if (m_pForm.intCurrentIdx > 0)
                btnPreviousPlot.Enabled = true;
            else
                btnPreviousPlot.Enabled = false;

            if (m_pForm.intCurrentIdx == m_pForm.multipageImage.Count - 1)
                btnNextPlot.Enabled = false;
            else
                btnNextPlot.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
