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
            this.spltMain.Name = "spltMain";
            // 
            // spltMain.Panel1
            // 
            this.spltMain.Panel1.Controls.Add(this.pbMain);
            // 
            // spltMain.Panel2
            // 
            this.spltMain.Panel2.Controls.Add(this.btnClear);
            this.spltMain.Panel2.Controls.Add(this.btnRemove);
            this.spltMain.Panel2.Controls.Add(this.lstLabels);
            this.spltMain.Panel2.Controls.Add(this.txtLabel);
            this.spltMain.Panel2.Controls.Add(this.btnSave);
            this.spltMain.Panel2.Controls.Add(this.chkSaveExit);
            this.spltMain.Panel2.Controls.Add(this.chkSaveNext);
            this.spltMain.Panel2.Controls.Add(this.chkAutoNext);
            this.spltMain.Panel2.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.spltMain_Panel2_PreviewKeyDown);
            this.spltMain.Size = new System.Drawing.Size(734, 489);
            this.spltMain.SplitterDistance = 563;
            this.spltMain.TabIndex = 0;
            // 
            // pbMain
            // 
            this.pbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMain.Location = new System.Drawing.Point(0, 0);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(563, 489);
            this.pbMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMain.TabIndex = 0;
            this.pbMain.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(10, 338);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(146, 42);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(10, 291);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(146, 42);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lstLabels
            // 
            this.lstLabels.FormattingEnabled = true;
            this.lstLabels.ItemHeight = 20;
            this.lstLabels.Location = new System.Drawing.Point(10, 157);
            this.lstLabels.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstLabels.Name = "lstLabels";
            this.lstLabels.Size = new System.Drawing.Size(144, 124);
            this.lstLabels.TabIndex = 5;
            // 
            // txtLabel
            // 
            this.txtLabel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtLabel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtLabel.Location = new System.Drawing.Point(10, 118);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(144, 26);
            this.txtLabel.TabIndex = 4;
            this.txtLabel.TextChanged += new System.EventHandler(this.txtLabel_TextChanged);
            this.txtLabel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLabel_KeyDown);
            this.txtLabel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLabel_KeyPress);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(10, 431);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(146, 42);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // chkSaveExit
            // 
            this.chkSaveExit.AutoSize = true;
            this.chkSaveExit.Location = new System.Drawing.Point(10, 72);
            this.chkSaveExit.Name = "chkSaveExit";
            this.chkSaveExit.Size = new System.Drawing.Size(121, 24);
            this.chkSaveExit.TabIndex = 2;
            this.chkSaveExit.Text = "Save on exit";
            this.chkSaveExit.UseVisualStyleBackColor = true;
            // 
            // chkSaveNext
            // 
            this.chkSaveNext.AutoSize = true;
            this.chkSaveNext.Location = new System.Drawing.Point(10, 43);
            this.chkSaveNext.Name = "chkSaveNext";
            this.chkSaveNext.Size = new System.Drawing.Size(127, 24);
            this.chkSaveNext.TabIndex = 1;
            this.chkSaveNext.Text = "Save on next";
            this.chkSaveNext.UseVisualStyleBackColor = true;
            // 
            // chkAutoNext
            // 
            this.chkAutoNext.AutoSize = true;
            this.chkAutoNext.Location = new System.Drawing.Point(10, 12);
            this.chkAutoNext.Name = "chkAutoNext";
            this.chkAutoNext.Size = new System.Drawing.Size(103, 24);
            this.chkAutoNext.TabIndex = 0;
            this.chkAutoNext.Text = "Auto next";
            this.chkAutoNext.UseVisualStyleBackColor = true;
            // 
            // ImageClassifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 489);
            this.Controls.Add(this.spltMain);
            this.KeyPreview = true;
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
    }
}