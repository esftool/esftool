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
    public partial class frmErrorLog : Form
    {
        public Exception ex;

        public frmErrorLog()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmErrorLog_Shown(object sender, EventArgs e)
        {
            this.Text = ex.Source + " Error";
            this.txtMessages.Text = ex.Message;
            this.txtStackTrace.Text = ex.StackTrace;
        }
    }
}
