namespace ESFTool
{
    partial class frmAttributeTable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAttributeTable));
            this.dgvAttTable = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttTable)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAttTable
            // 
            this.dgvAttTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAttTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvAttTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAttTable.Location = new System.Drawing.Point(0, 0);
            this.dgvAttTable.Name = "dgvAttTable";
            this.dgvAttTable.RowTemplate.Height = 23;
            this.dgvAttTable.Size = new System.Drawing.Size(510, 370);
            this.dgvAttTable.TabIndex = 1;
            // 
            // frmAttributeTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 370);
            this.Controls.Add(this.dgvAttTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAttributeTable";
            this.Text = "frmAttributeTable";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dgvAttTable;
    }
}