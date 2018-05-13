namespace ManualImageObjectSelector
{
    partial class ImageClassifier
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
            this.spltMain = new System.Windows.Forms.SplitContainer();
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lstLabels = new System.Windows.Forms.ListBox();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkSaveExit = new System.Windows.Forms.CheckBox();
            this.chkSaveNext = new System.Windows.Forms.CheckBox();
            this.chkAutoNext = new System.Windows.Forms.CheckBox();
            this.txtCurIndex = new System.Windows.Forms.TextBox();
            this.lblSplit = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.spltMain)).BeginInit();
            this.spltMain.Panel1.SuspendLayout();
            this.spltMain.Panel2.SuspendLayout();
            this.spltMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.SuspendLayout();
            // 
            // spltMain
            // 
            this.spltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltMain.Location = new System.Drawing.Point(0, 0);
            this.spltMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.spltMain.Name = "spltMain";
            // 
            // spltMain.Panel1
            // 
            this.spltMain.Panel1.Controls.Add(this.pbMain);
            // 
            // spltMain.Panel2
            // 
            this.spltMain.Panel2.Controls.Add(this.lblProgress);
            this.spltMain.Panel2.Controls.Add(this.lblTotal);
            this.spltMain.Panel2.Controls.Add(this.lblSplit);
            this.spltMain.Panel2.Controls.Add(this.txtCurIndex);
            this.spltMain.Panel2.Controls.Add(this.btnClear);
            this.spltMain.Panel2.Controls.Add(this.btnRemove);
            this.spltMain.Panel2.Controls.Add(this.lstLabels);
            this.spltMain.Panel2.Controls.Add(this.txtLabel);
            this.spltMain.Panel2.Controls.Add(this.btnSave);
            this.spltMain.Panel2.Controls.Add(this.chkSaveExit);
            this.spltMain.Panel2.Controls.Add(this.chkSaveNext);
            this.spltMain.Panel2.Controls.Add(this.chkAutoNext);
            this.spltMain.Panel2.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.spltMain_Panel2_PreviewKeyDown);
            this.spltMain.Size = new System.Drawing.Size(500, 453);
            this.spltMain.SplitterDistance = 372;
            this.spltMain.SplitterWidth = 3;
            this.spltMain.TabIndex = 0;
            // 
            // pbMain
            // 
            this.pbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMain.Location = new System.Drawing.Point(0, 0);
            this.pbMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(372, 453);
            this.pbMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMain.TabIndex = 0;
            this.pbMain.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(7, 220);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(106, 27);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(7, 189);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(106, 27);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lstLabels
            // 
            this.lstLabels.FormattingEnabled = true;
            this.lstLabels.Location = new System.Drawing.Point(7, 102);
            this.lstLabels.Name = "lstLabels";
            this.lstLabels.Size = new System.Drawing.Size(106, 82);
            this.lstLabels.TabIndex = 5;
            // 
            // txtLabel
            // 
            this.txtLabel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtLabel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtLabel.Location = new System.Drawing.Point(7, 77);
            this.txtLabel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(106, 20);
            this.txtLabel.TabIndex = 4;
            this.txtLabel.TextChanged += new System.EventHandler(this.txtLabel_TextChanged);
            this.txtLabel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLabel_KeyDown);
            this.txtLabel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLabel_KeyPress);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(7, 415);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(106, 27);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // chkSaveExit
            // 
            this.chkSaveExit.AutoSize = true;
            this.chkSaveExit.Checked = true;
            this.chkSaveExit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveExit.Location = new System.Drawing.Point(7, 47);
            this.chkSaveExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkSaveExit.Name = "chkSaveExit";
            this.chkSaveExit.Size = new System.Drawing.Size(111, 17);
            this.chkSaveExit.TabIndex = 2;
            this.chkSaveExit.Text = "Autosave on exit?";
            this.chkSaveExit.UseVisualStyleBackColor = true;
            // 
            // chkSaveNext
            // 
            this.chkSaveNext.AutoSize = true;
            this.chkSaveNext.Location = new System.Drawing.Point(7, 28);
            this.chkSaveNext.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkSaveNext.Name = "chkSaveNext";
            this.chkSaveNext.Size = new System.Drawing.Size(115, 17);
            this.chkSaveNext.TabIndex = 1;
            this.chkSaveNext.Text = "Autosave on next?";
            this.chkSaveNext.UseVisualStyleBackColor = true;
            // 
            // chkAutoNext
            // 
            this.chkAutoNext.AutoSize = true;
            this.chkAutoNext.Location = new System.Drawing.Point(7, 8);
            this.chkAutoNext.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkAutoNext.Name = "chkAutoNext";
            this.chkAutoNext.Size = new System.Drawing.Size(74, 17);
            this.chkAutoNext.TabIndex = 0;
            this.chkAutoNext.Text = "Autonext?";
            this.chkAutoNext.UseVisualStyleBackColor = true;
            // 
            // txtCurIndex
            // 
            this.txtCurIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtCurIndex.Location = new System.Drawing.Point(7, 320);
            this.txtCurIndex.Name = "txtCurIndex";
            this.txtCurIndex.Size = new System.Drawing.Size(43, 23);
            this.txtCurIndex.TabIndex = 8;
            this.txtCurIndex.Text = "1";
            this.txtCurIndex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCurIndex_KeyDown);
            // 
            // lblSplit
            // 
            this.lblSplit.AutoSize = true;
            this.lblSplit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSplit.Location = new System.Drawing.Point(53, 323);
            this.lblSplit.Name = "lblSplit";
            this.lblSplit.Size = new System.Drawing.Size(12, 17);
            this.lblSplit.TabIndex = 9;
            this.lblSplit.Text = "/";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTotal.Location = new System.Drawing.Point(71, 323);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(16, 17);
            this.lblTotal.TabIndex = 10;
            this.lblTotal.Text = "1";
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblProgress.Location = new System.Drawing.Point(4, 301);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(119, 17);
            this.lblProgress.TabIndex = 11;
            this.lblProgress.Text = "Current progress:";
            // 
            // ImageClassifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 453);
            this.Controls.Add(this.spltMain);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ImageClassifier";
            this.Text = "ImageClassifier";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ImageClassifier_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ImageClassifier_KeyDown);
            this.spltMain.Panel1.ResumeLayout(false);
            this.spltMain.Panel2.ResumeLayout(false);
            this.spltMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltMain)).EndInit();
            this.spltMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spltMain;
        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.CheckBox chkSaveExit;
        private System.Windows.Forms.CheckBox chkSaveNext;
        private System.Windows.Forms.CheckBox chkAutoNext;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.ListBox lstLabels;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblSplit;
        private System.Windows.Forms.TextBox txtCurIndex;
    }
}