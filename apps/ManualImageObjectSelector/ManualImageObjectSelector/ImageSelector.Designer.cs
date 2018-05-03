namespace ManualImageObjectSelector
{
    partial class ImageSelector
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
            this.lpanRoot = new System.Windows.Forms.TableLayoutPanel();
            this.pnlImageList = new System.Windows.Forms.Panel();
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.pbOrientation = new System.Windows.Forms.PictureBox();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.grpCheckBox = new System.Windows.Forms.GroupBox();
            this.chkSelection = new System.Windows.Forms.CheckBox();
            this.chkRegion = new System.Windows.Forms.CheckBox();
            this.chkAutosaveExit = new System.Windows.Forms.CheckBox();
            this.lstRegions = new System.Windows.Forms.ComboBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lpanRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOrientation)).BeginInit();
            this.pnlInfo.SuspendLayout();
            this.grpCheckBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // lpanRoot
            // 
            this.lpanRoot.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.lpanRoot.ColumnCount = 2;
            this.lpanRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.lpanRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.lpanRoot.Controls.Add(this.pnlImageList, 1, 1);
            this.lpanRoot.Controls.Add(this.pbMain, 0, 1);
            this.lpanRoot.Controls.Add(this.pbOrientation, 1, 0);
            this.lpanRoot.Controls.Add(this.pnlInfo, 0, 0);
            this.lpanRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lpanRoot.Location = new System.Drawing.Point(0, 0);
            this.lpanRoot.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lpanRoot.Name = "lpanRoot";
            this.lpanRoot.RowCount = 2;
            this.lpanRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 154F));
            this.lpanRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.lpanRoot.Size = new System.Drawing.Size(600, 532);
            this.lpanRoot.TabIndex = 0;
            // 
            // pnlImageList
            // 
            this.pnlImageList.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pnlImageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImageList.Location = new System.Drawing.Point(453, 161);
            this.pnlImageList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlImageList.Name = "pnlImageList";
            this.pnlImageList.Size = new System.Drawing.Size(142, 365);
            this.pnlImageList.TabIndex = 0;
            this.pnlImageList.SizeChanged += new System.EventHandler(this.pnlImageList_SizeChanged);
            // 
            // pbMain
            // 
            this.pbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMain.Location = new System.Drawing.Point(5, 161);
            this.pbMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(439, 365);
            this.pbMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMain.TabIndex = 1;
            this.pbMain.TabStop = false;
            this.pbMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMain_Paint);
            this.pbMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseDown);
            this.pbMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseMove);
            this.pbMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseUp);
            // 
            // pbOrientation
            // 
            this.pbOrientation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbOrientation.Location = new System.Drawing.Point(452, 4);
            this.pbOrientation.Name = "pbOrientation";
            this.pbOrientation.Size = new System.Drawing.Size(144, 148);
            this.pbOrientation.TabIndex = 2;
            this.pbOrientation.TabStop = false;
            this.pbOrientation.Paint += new System.Windows.Forms.PaintEventHandler(this.pbOrientation_Paint);
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.btnClear);
            this.pnlInfo.Controls.Add(this.btnRemove);
            this.pnlInfo.Controls.Add(this.lstRegions);
            this.pnlInfo.Controls.Add(this.grpCheckBox);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfo.Location = new System.Drawing.Point(4, 4);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(441, 148);
            this.pnlInfo.TabIndex = 3;
            // 
            // grpCheckBox
            // 
            this.grpCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCheckBox.Controls.Add(this.chkAutosaveExit);
            this.grpCheckBox.Controls.Add(this.chkSelection);
            this.grpCheckBox.Controls.Add(this.chkRegion);
            this.grpCheckBox.Location = new System.Drawing.Point(261, 8);
            this.grpCheckBox.Name = "grpCheckBox";
            this.grpCheckBox.Size = new System.Drawing.Size(177, 137);
            this.grpCheckBox.TabIndex = 0;
            this.grpCheckBox.TabStop = false;
            this.grpCheckBox.Text = "Dialog control";
            // 
            // chkSelection
            // 
            this.chkSelection.AutoSize = true;
            this.chkSelection.Location = new System.Drawing.Point(8, 59);
            this.chkSelection.Name = "chkSelection";
            this.chkSelection.Size = new System.Drawing.Size(166, 24);
            this.chkSelection.TabIndex = 1;
            this.chkSelection.Text = "Confirm selection?";
            this.chkSelection.UseVisualStyleBackColor = true;
            // 
            // chkRegion
            // 
            this.chkRegion.AutoSize = true;
            this.chkRegion.Location = new System.Drawing.Point(8, 34);
            this.chkRegion.Name = "chkRegion";
            this.chkRegion.Size = new System.Drawing.Size(147, 24);
            this.chkRegion.TabIndex = 0;
            this.chkRegion.Text = "Confirm region?";
            this.chkRegion.UseVisualStyleBackColor = true;
            // 
            // chkAutosaveExit
            // 
            this.chkAutosaveExit.AutoSize = true;
            this.chkAutosaveExit.Checked = true;
            this.chkAutosaveExit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutosaveExit.Location = new System.Drawing.Point(8, 84);
            this.chkAutosaveExit.Name = "chkAutosaveExit";
            this.chkAutosaveExit.Size = new System.Drawing.Size(161, 24);
            this.chkAutosaveExit.TabIndex = 2;
            this.chkAutosaveExit.Text = "Autosave on exit?";
            this.chkAutosaveExit.UseVisualStyleBackColor = true;
            // 
            // lstRegions
            // 
            this.lstRegions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lstRegions.FormattingEnabled = true;
            this.lstRegions.Location = new System.Drawing.Point(160, 8);
            this.lstRegions.Name = "lstRegions";
            this.lstRegions.Size = new System.Drawing.Size(95, 28);
            this.lstRegions.TabIndex = 1;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(160, 42);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(94, 56);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Remove Selected";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(160, 106);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(94, 36);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear All";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ImageSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 532);
            this.Controls.Add(this.lpanRoot);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ImageSelector";
            this.Text = "Image viewer";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ImageSelector_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ImageSelector_KeyPress);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ImageSelector_PreviewKeyDown);
            this.lpanRoot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOrientation)).EndInit();
            this.pnlInfo.ResumeLayout(false);
            this.grpCheckBox.ResumeLayout(false);
            this.grpCheckBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel lpanRoot;
        private System.Windows.Forms.Panel pnlImageList;
        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.PictureBox pbOrientation;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.GroupBox grpCheckBox;
        private System.Windows.Forms.CheckBox chkSelection;
        private System.Windows.Forms.CheckBox chkRegion;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ComboBox lstRegions;
        private System.Windows.Forms.CheckBox chkAutosaveExit;
    }
}