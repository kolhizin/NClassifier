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
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.pbOrientation = new System.Windows.Forms.PictureBox();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblSplit = new System.Windows.Forms.Label();
            this.txtCurIndex = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lstRegions = new System.Windows.Forms.ComboBox();
            this.grpCheckBox = new System.Windows.Forms.GroupBox();
            this.chkAutosaveNext = new System.Windows.Forms.CheckBox();
            this.chkAutosaveExit = new System.Windows.Forms.CheckBox();
            this.chkRegion = new System.Windows.Forms.CheckBox();
            this.chkValidObs = new System.Windows.Forms.CheckBox();
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
            this.lpanRoot.Controls.Add(this.pbMain, 0, 1);
            this.lpanRoot.Controls.Add(this.pbOrientation, 1, 0);
            this.lpanRoot.Controls.Add(this.pnlInfo, 0, 0);
            this.lpanRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lpanRoot.Location = new System.Drawing.Point(0, 0);
            this.lpanRoot.Name = "lpanRoot";
            this.lpanRoot.RowCount = 2;
            this.lpanRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.lpanRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.lpanRoot.Size = new System.Drawing.Size(400, 346);
            this.lpanRoot.TabIndex = 0;
            // 
            // pbMain
            // 
            this.lpanRoot.SetColumnSpan(this.pbMain, 2);
            this.pbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMain.Location = new System.Drawing.Point(4, 105);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(392, 237);
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
            this.pbOrientation.Location = new System.Drawing.Point(301, 3);
            this.pbOrientation.Margin = new System.Windows.Forms.Padding(2);
            this.pbOrientation.Name = "pbOrientation";
            this.pbOrientation.Size = new System.Drawing.Size(96, 96);
            this.pbOrientation.TabIndex = 2;
            this.pbOrientation.TabStop = false;
            this.pbOrientation.Paint += new System.Windows.Forms.PaintEventHandler(this.pbOrientation_Paint);
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.chkValidObs);
            this.pnlInfo.Controls.Add(this.lblProgress);
            this.pnlInfo.Controls.Add(this.lblTotal);
            this.pnlInfo.Controls.Add(this.lblSplit);
            this.pnlInfo.Controls.Add(this.txtCurIndex);
            this.pnlInfo.Controls.Add(this.btnSave);
            this.pnlInfo.Controls.Add(this.btnView);
            this.pnlInfo.Controls.Add(this.btnClear);
            this.pnlInfo.Controls.Add(this.btnRemove);
            this.pnlInfo.Controls.Add(this.lstRegions);
            this.pnlInfo.Controls.Add(this.grpCheckBox);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfo.Location = new System.Drawing.Point(3, 3);
            this.pnlInfo.Margin = new System.Windows.Forms.Padding(2);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(293, 96);
            this.pnlInfo.TabIndex = 3;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblProgress.Location = new System.Drawing.Point(4, 10);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(69, 17);
            this.lblProgress.TabIndex = 15;
            this.lblProgress.Text = "Progress:";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTotal.Location = new System.Drawing.Point(71, 32);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(16, 17);
            this.lblTotal.TabIndex = 14;
            this.lblTotal.Text = "1";
            // 
            // lblSplit
            // 
            this.lblSplit.AutoSize = true;
            this.lblSplit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSplit.Location = new System.Drawing.Point(53, 32);
            this.lblSplit.Name = "lblSplit";
            this.lblSplit.Size = new System.Drawing.Size(12, 17);
            this.lblSplit.TabIndex = 13;
            this.lblSplit.Text = "/";
            // 
            // txtCurIndex
            // 
            this.txtCurIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtCurIndex.Location = new System.Drawing.Point(7, 29);
            this.txtCurIndex.Name = "txtCurIndex";
            this.txtCurIndex.Size = new System.Drawing.Size(43, 23);
            this.txtCurIndex.TabIndex = 12;
            this.txtCurIndex.Text = "1";
            this.txtCurIndex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCurIndex_KeyDown);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(5, 70);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.Location = new System.Drawing.Point(106, 25);
            this.btnView.Margin = new System.Windows.Forms.Padding(2);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(63, 20);
            this.btnView.TabIndex = 4;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(106, 70);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(63, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear All";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(106, 47);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(63, 20);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lstRegions
            // 
            this.lstRegions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lstRegions.FormattingEnabled = true;
            this.lstRegions.Location = new System.Drawing.Point(106, 5);
            this.lstRegions.Margin = new System.Windows.Forms.Padding(2);
            this.lstRegions.Name = "lstRegions";
            this.lstRegions.Size = new System.Drawing.Size(64, 21);
            this.lstRegions.TabIndex = 1;
            // 
            // grpCheckBox
            // 
            this.grpCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCheckBox.Controls.Add(this.chkAutosaveNext);
            this.grpCheckBox.Controls.Add(this.chkAutosaveExit);
            this.grpCheckBox.Controls.Add(this.chkRegion);
            this.grpCheckBox.Location = new System.Drawing.Point(173, 5);
            this.grpCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.grpCheckBox.Name = "grpCheckBox";
            this.grpCheckBox.Padding = new System.Windows.Forms.Padding(2);
            this.grpCheckBox.Size = new System.Drawing.Size(118, 89);
            this.grpCheckBox.TabIndex = 0;
            this.grpCheckBox.TabStop = false;
            this.grpCheckBox.Text = "Behavior";
            // 
            // chkAutosaveNext
            // 
            this.chkAutosaveNext.AutoSize = true;
            this.chkAutosaveNext.Location = new System.Drawing.Point(5, 66);
            this.chkAutosaveNext.Margin = new System.Windows.Forms.Padding(2);
            this.chkAutosaveNext.Name = "chkAutosaveNext";
            this.chkAutosaveNext.Size = new System.Drawing.Size(115, 17);
            this.chkAutosaveNext.TabIndex = 3;
            this.chkAutosaveNext.Text = "Autosave on next?";
            this.chkAutosaveNext.UseVisualStyleBackColor = true;
            // 
            // chkAutosaveExit
            // 
            this.chkAutosaveExit.AutoSize = true;
            this.chkAutosaveExit.Checked = true;
            this.chkAutosaveExit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutosaveExit.Location = new System.Drawing.Point(5, 43);
            this.chkAutosaveExit.Margin = new System.Windows.Forms.Padding(2);
            this.chkAutosaveExit.Name = "chkAutosaveExit";
            this.chkAutosaveExit.Size = new System.Drawing.Size(111, 17);
            this.chkAutosaveExit.TabIndex = 2;
            this.chkAutosaveExit.Text = "Autosave on exit?";
            this.chkAutosaveExit.UseVisualStyleBackColor = true;
            // 
            // chkRegion
            // 
            this.chkRegion.AutoSize = true;
            this.chkRegion.Location = new System.Drawing.Point(5, 22);
            this.chkRegion.Margin = new System.Windows.Forms.Padding(2);
            this.chkRegion.Name = "chkRegion";
            this.chkRegion.Size = new System.Drawing.Size(99, 17);
            this.chkRegion.TabIndex = 0;
            this.chkRegion.Text = "Confirm region?";
            this.chkRegion.UseVisualStyleBackColor = true;
            // 
            // chkValidObs
            // 
            this.chkValidObs.AutoSize = true;
            this.chkValidObs.Location = new System.Drawing.Point(9, 54);
            this.chkValidObs.Name = "chkValidObs";
            this.chkValidObs.Size = new System.Drawing.Size(55, 17);
            this.chkValidObs.TabIndex = 16;
            this.chkValidObs.Text = "Valid?";
            this.chkValidObs.UseVisualStyleBackColor = true;
            this.chkValidObs.CheckedChanged += new System.EventHandler(this.chkValidObs_CheckedChanged);
            // 
            // ImageSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 346);
            this.Controls.Add(this.lpanRoot);
            this.KeyPreview = true;
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
            this.pnlInfo.PerformLayout();
            this.grpCheckBox.ResumeLayout(false);
            this.grpCheckBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel lpanRoot;
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
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblSplit;
        private System.Windows.Forms.TextBox txtCurIndex;
        private System.Windows.Forms.CheckBox chkValidObs;
    }
}