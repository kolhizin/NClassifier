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
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lstRegions = new System.Windows.Forms.ComboBox();
            this.grpCheckBox = new System.Windows.Forms.GroupBox();
            this.chkAutosaveExit = new System.Windows.Forms.CheckBox();
            this.chkRegion = new System.Windows.Forms.CheckBox();
            this.btnView = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkAutosaveNext = new System.Windows.Forms.CheckBox();
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
            this.pnlInfo.Controls.Add(this.btnSave);
            this.pnlInfo.Controls.Add(this.btnView);
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
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(160, 107);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(94, 36);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear All";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(160, 73);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(94, 31);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lstRegions
            // 
            this.lstRegions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lstRegions.FormattingEnabled = true;
            this.lstRegions.Location = new System.Drawing.Point(160, 8);
            this.lstRegions.Name = "lstRegions";
            this.lstRegions.Size = new System.Drawing.Size(94, 28);
            this.lstRegions.TabIndex = 1;
            // 
            // grpCheckBox
            // 
            this.grpCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCheckBox.Controls.Add(this.chkAutosaveNext);
            this.grpCheckBox.Controls.Add(this.chkAutosaveExit);
            this.grpCheckBox.Controls.Add(this.chkRegion);
            this.grpCheckBox.Location = new System.Drawing.Point(261, 8);
            this.grpCheckBox.Name = "grpCheckBox";
            this.grpCheckBox.Size = new System.Drawing.Size(177, 137);
            this.grpCheckBox.TabIndex = 0;
            this.grpCheckBox.TabStop = false;
            this.grpCheckBox.Text = "Behavior";
            // 
            // chkAutosaveExit
            // 
            this.chkAutosaveExit.AutoSize = true;
            this.chkAutosaveExit.Checked = true;
            this.chkAutosaveExit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutosaveExit.Location = new System.Drawing.Point(8, 66);
            this.chkAutosaveExit.Name = "chkAutosaveExit";
            this.chkAutosaveExit.Size = new System.Drawing.Size(161, 24);
            this.chkAutosaveExit.TabIndex = 2;
            this.chkAutosaveExit.Text = "Autosave on exit?";
            this.chkAutosaveExit.UseVisualStyleBackColor = true;
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
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.Location = new System.Drawing.Point(160, 39);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(94, 31);
            this.btnView.TabIndex = 4;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(8, 107);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(146, 36);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkAutosaveNext
            // 
            this.chkAutosaveNext.AutoSize = true;
            this.chkAutosaveNext.Location = new System.Drawing.Point(8, 101);
            this.chkAutosaveNext.Name = "chkAutosaveNext";
            this.chkAutosaveNext.Size = new System.Drawing.Size(167, 24);
            this.chkAutosaveNext.TabIndex = 3;
            this.chkAutosaveNext.Text = "Autosave on next?";
            this.chkAutosaveNext.UseVisualStyleBackColor = true;
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
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ImageSelector_FormClosed);
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
        private System.Windows.Forms.CheckBox chkRegion;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ComboBox lstRegions;
        private System.Windows.Forms.CheckBox chkAutosaveExit;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkAutosaveNext;
    }
}