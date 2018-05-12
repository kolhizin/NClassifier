namespace ManualImageObjectSelector
{
    partial class ClassifierSetup
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
            this.pnlControl = new System.Windows.Forms.Panel();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.pnlLabels = new System.Windows.Forms.Panel();
            this.dgvLabels = new System.Windows.Forms.DataGridView();
            this.LabelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hotkey = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.pnlControl.SuspendLayout();
            this.pnlLabels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabels)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlControl
            // 
            this.pnlControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlControl.Controls.Add(this.btnRestore);
            this.pnlControl.Controls.Add(this.btnSave);
            this.pnlControl.Controls.Add(this.btnConfirm);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControl.Location = new System.Drawing.Point(0, 0);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(689, 77);
            this.pnlControl.TabIndex = 0;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(500, 10);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(177, 52);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // pnlLabels
            // 
            this.pnlLabels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLabels.Controls.Add(this.dgvLabels);
            this.pnlLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLabels.Location = new System.Drawing.Point(0, 77);
            this.pnlLabels.Name = "pnlLabels";
            this.pnlLabels.Size = new System.Drawing.Size(689, 326);
            this.pnlLabels.TabIndex = 1;
            // 
            // dgvLabels
            // 
            this.dgvLabels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLabels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LabelName,
            this.Hotkey});
            this.dgvLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLabels.Location = new System.Drawing.Point(0, 0);
            this.dgvLabels.Name = "dgvLabels";
            this.dgvLabels.RowTemplate.Height = 28;
            this.dgvLabels.Size = new System.Drawing.Size(687, 324);
            this.dgvLabels.TabIndex = 0;
            // 
            // LabelName
            // 
            this.LabelName.HeaderText = "Label";
            this.LabelName.MaxInputLength = 255;
            this.LabelName.MinimumWidth = 255;
            this.LabelName.Name = "LabelName";
            this.LabelName.Width = 255;
            // 
            // Hotkey
            // 
            this.Hotkey.HeaderText = "Hotkey";
            this.Hotkey.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.Hotkey.Name = "Hotkey";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(11, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(177, 52);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(194, 11);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(177, 52);
            this.btnRestore.TabIndex = 2;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // ClassifierSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 403);
            this.Controls.Add(this.pnlLabels);
            this.Controls.Add(this.pnlControl);
            this.Name = "ClassifierSetup";
            this.Text = "ClassifierSetup";
            this.pnlControl.ResumeLayout(false);
            this.pnlLabels.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabels)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Panel pnlLabels;
        private System.Windows.Forms.DataGridView dgvLabels;
        private System.Windows.Forms.DataGridViewTextBoxColumn LabelName;
        private System.Windows.Forms.DataGridViewComboBoxColumn Hotkey;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnSave;
    }
}