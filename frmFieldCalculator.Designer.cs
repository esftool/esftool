namespace ESFTool
{
    partial class frmFieldCalculator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFieldCalculator));
            this.btnAddExpression = new System.Windows.Forms.Button();
            this.btnAddTarget = new System.Windows.Forms.Button();
            this.txtNExpression = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTargetField = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstFields = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboLayer = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddExpression
            // 
            this.btnAddExpression.Location = new System.Drawing.Point(133, 139);
            this.btnAddExpression.Name = "btnAddExpression";
            this.btnAddExpression.Size = new System.Drawing.Size(26, 23);
            this.btnAddExpression.TabIndex = 23;
            this.btnAddExpression.Text = ">";
            this.btnAddExpression.UseVisualStyleBackColor = true;
            this.btnAddExpression.Click += new System.EventHandler(this.btnAddExpression_Click);
            // 
            // btnAddTarget
            // 
            this.btnAddTarget.Location = new System.Drawing.Point(134, 67);
            this.btnAddTarget.Name = "btnAddTarget";
            this.btnAddTarget.Size = new System.Drawing.Size(26, 23);
            this.btnAddTarget.TabIndex = 22;
            this.btnAddTarget.Text = ">";
            this.btnAddTarget.UseVisualStyleBackColor = true;
            this.btnAddTarget.Click += new System.EventHandler(this.btnAddTarget_Click);
            // 
            // txtNExpression
            // 
            this.txtNExpression.AcceptsReturn = true;
            this.txtNExpression.Location = new System.Drawing.Point(169, 106);
            this.txtNExpression.Multiline = true;
            this.txtNExpression.Name = "txtNExpression";
            this.txtNExpression.Size = new System.Drawing.Size(205, 95);
            this.txtNExpression.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(166, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Numeric Expression:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(288, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "=";
            // 
            // txtTargetField
            // 
            this.txtTargetField.Location = new System.Drawing.Point(169, 67);
            this.txtTargetField.Name = "txtTargetField";
            this.txtTargetField.Size = new System.Drawing.Size(113, 20);
            this.txtTargetField.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Target Field:";
            // 
            // lstFields
            // 
            this.lstFields.FormattingEnabled = true;
            this.lstFields.Location = new System.Drawing.Point(12, 67);
            this.lstFields.Name = "lstFields";
            this.lstFields.Size = new System.Drawing.Size(115, 134);
            this.lstFields.TabIndex = 16;
            this.lstFields.DoubleClick += new System.EventHandler(this.lstFields_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Fields:";
            // 
            // cboLayer
            // 
            this.cboLayer.FormattingEnabled = true;
            this.cboLayer.Location = new System.Drawing.Point(55, 13);
            this.cboLayer.Name = "cboLayer";
            this.cboLayer.Size = new System.Drawing.Size(227, 21);
            this.cboLayer.TabIndex = 28;
            this.cboLayer.SelectedIndexChanged += new System.EventHandler(this.cboLayer_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Layer:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(274, 207);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 23);
            this.button2.TabIndex = 26;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(169, 207);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 23);
            this.btnOK.TabIndex = 25;
            this.btnOK.Text = "Calculate";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmFieldCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(388, 241);
            this.Controls.Add(this.btnAddExpression);
            this.Controls.Add(this.btnAddTarget);
            this.Controls.Add(this.txtNExpression);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTargetField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstFields);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboLayer);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFieldCalculator";
            this.Text = "Field Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddExpression;
        private System.Windows.Forms.Button btnAddTarget;
        private System.Windows.Forms.TextBox txtNExpression;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtTargetField;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstFields;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cboLayer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnOK;
    }
}