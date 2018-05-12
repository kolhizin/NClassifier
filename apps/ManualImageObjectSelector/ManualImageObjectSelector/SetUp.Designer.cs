﻿namespace ManualImageObjectSelector
{
    partial class SetUp
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
            this.pnlSetup1 = new System.Windows.Forms.Panel();
            this.txtRegex = new System.Windows.Forms.TextBox();
            this.lblFiles = new System.Windows.Forms.Label();
            this.lblPreviewStat = new System.Windows.Forms.Label();
            this.lblPreview = new System.Windows.Forms.Label();
            this.lblMask = new System.Windows.Forms.Label();
            this.lblFolder = new System.Windows.Forms.Label();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.lblSetup1 = new System.Windows.Forms.Label();
            this.pnlSetup2 = new System.Windows.Forms.Panel();
            this.lblOutputFile = new System.Windows.Forms.Label();
            this.btnSelectOutput = new System.Windows.Forms.Button();
            this.lblSetup2 = new System.Windows.Forms.Label();
            this.pnlSetup3 = new System.Windows.Forms.Panel();
            this.btnObjectSelector = new System.Windows.Forms.Button();
            this.lblSetup3 = new System.Windows.Forms.Label();
            this.btnClassifier = new System.Windows.Forms.Button();
            this.pnlSetup1.SuspendLayout();
            this.pnlSetup2.SuspendLayout();
            this.pnlSetup3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSetup1
            // 
            this.pnlSetup1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSetup1.Controls.Add(this.txtRegex);
            this.pnlSetup1.Controls.Add(this.lblFiles);
            this.pnlSetup1.Controls.Add(this.lblPreviewStat);
            this.pnlSetup1.Controls.Add(this.lblPreview);
            this.pnlSetup1.Controls.Add(this.lblMask);
            this.pnlSetup1.Controls.Add(this.lblFolder);
            this.pnlSetup1.Controls.Add(this.btnSelectFolder);
            this.pnlSetup1.Controls.Add(this.lblSetup1);
            this.pnlSetup1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSetup1.Location = new System.Drawing.Point(0, 0);
            this.pnlSetup1.Name = "pnlSetup1";
            this.pnlSetup1.Size = new System.Drawing.Size(431, 500);
            this.pnlSetup1.TabIndex = 0;
            // 
            // txtRegex
            // 
            this.txtRegex.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtRegex.Location = new System.Drawing.Point(16, 223);
            this.txtRegex.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRegex.Name = "txtRegex";
            this.txtRegex.Size = new System.Drawing.Size(404, 35);
            this.txtRegex.TabIndex = 8;
            this.txtRegex.Text = "\\w+.(jpg|jpeg)";
            this.txtRegex.TextChanged += new System.EventHandler(this.txtRegex_TextChanged);
            // 
            // lblFiles
            // 
            this.lblFiles.AutoSize = true;
            this.lblFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFiles.Location = new System.Drawing.Point(16, 323);
            this.lblFiles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFiles.Name = "lblFiles";
            this.lblFiles.Size = new System.Drawing.Size(51, 29);
            this.lblFiles.TabIndex = 7;
            this.lblFiles.Text = "test";
            // 
            // lblPreviewStat
            // 
            this.lblPreviewStat.AutoSize = true;
            this.lblPreviewStat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPreviewStat.Location = new System.Drawing.Point(138, 286);
            this.lblPreviewStat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPreviewStat.Name = "lblPreviewStat";
            this.lblPreviewStat.Size = new System.Drawing.Size(118, 29);
            this.lblPreviewStat.TabIndex = 6;
            this.lblPreviewStat.Text = "<no files>";
            // 
            // lblPreview
            // 
            this.lblPreview.AutoSize = true;
            this.lblPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPreview.Location = new System.Drawing.Point(16, 286);
            this.lblPreview.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(114, 29);
            this.lblPreview.TabIndex = 5;
            this.lblPreview.Text = "Preview:";
            // 
            // lblMask
            // 
            this.lblMask.AutoSize = true;
            this.lblMask.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblMask.Location = new System.Drawing.Point(16, 188);
            this.lblMask.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMask.Name = "lblMask";
            this.lblMask.Size = new System.Drawing.Size(91, 29);
            this.lblMask.TabIndex = 3;
            this.lblMask.Text = "RegEx:";
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFolder.Location = new System.Drawing.Point(16, 128);
            this.lblFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(141, 29);
            this.lblFolder.TabIndex = 2;
            this.lblFolder.Text = "<No folder>";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSelectFolder.Location = new System.Drawing.Point(16, 69);
            this.btnSelectFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(406, 54);
            this.btnSelectFolder.TabIndex = 1;
            this.btnSelectFolder.Text = "Select Input Folder";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // lblSetup1
            // 
            this.lblSetup1.AutoSize = true;
            this.lblSetup1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSetup1.Location = new System.Drawing.Point(10, 8);
            this.lblSetup1.Name = "lblSetup1";
            this.lblSetup1.Size = new System.Drawing.Size(192, 55);
            this.lblSetup1.TabIndex = 0;
            this.lblSetup1.Text = "1. Input";
            // 
            // pnlSetup2
            // 
            this.pnlSetup2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSetup2.Controls.Add(this.lblOutputFile);
            this.pnlSetup2.Controls.Add(this.btnSelectOutput);
            this.pnlSetup2.Controls.Add(this.lblSetup2);
            this.pnlSetup2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSetup2.Location = new System.Drawing.Point(431, 0);
            this.pnlSetup2.Name = "pnlSetup2";
            this.pnlSetup2.Size = new System.Drawing.Size(347, 287);
            this.pnlSetup2.TabIndex = 1;
            // 
            // lblOutputFile
            // 
            this.lblOutputFile.AutoSize = true;
            this.lblOutputFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblOutputFile.Location = new System.Drawing.Point(6, 128);
            this.lblOutputFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOutputFile.Name = "lblOutputFile";
            this.lblOutputFile.Size = new System.Drawing.Size(111, 29);
            this.lblOutputFile.TabIndex = 3;
            this.lblOutputFile.Text = "<No file>";
            // 
            // btnSelectOutput
            // 
            this.btnSelectOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSelectOutput.Location = new System.Drawing.Point(6, 69);
            this.btnSelectOutput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelectOutput.Name = "btnSelectOutput";
            this.btnSelectOutput.Size = new System.Drawing.Size(333, 54);
            this.btnSelectOutput.TabIndex = 2;
            this.btnSelectOutput.Text = "Select Output File";
            this.btnSelectOutput.UseVisualStyleBackColor = true;
            this.btnSelectOutput.Click += new System.EventHandler(this.btnSelectOutput_Click);
            // 
            // lblSetup2
            // 
            this.lblSetup2.AutoSize = true;
            this.lblSetup2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSetup2.Location = new System.Drawing.Point(10, 8);
            this.lblSetup2.Name = "lblSetup2";
            this.lblSetup2.Size = new System.Drawing.Size(230, 55);
            this.lblSetup2.TabIndex = 0;
            this.lblSetup2.Text = "2. Output";
            // 
            // pnlSetup3
            // 
            this.pnlSetup3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSetup3.Controls.Add(this.btnClassifier);
            this.pnlSetup3.Controls.Add(this.btnObjectSelector);
            this.pnlSetup3.Controls.Add(this.lblSetup3);
            this.pnlSetup3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSetup3.Location = new System.Drawing.Point(431, 287);
            this.pnlSetup3.Name = "pnlSetup3";
            this.pnlSetup3.Size = new System.Drawing.Size(347, 213);
            this.pnlSetup3.TabIndex = 2;
            // 
            // btnObjectSelector
            // 
            this.btnObjectSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnObjectSelector.Location = new System.Drawing.Point(6, 144);
            this.btnObjectSelector.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnObjectSelector.Name = "btnObjectSelector";
            this.btnObjectSelector.Size = new System.Drawing.Size(333, 54);
            this.btnObjectSelector.TabIndex = 3;
            this.btnObjectSelector.Text = "Object Selector";
            this.btnObjectSelector.UseVisualStyleBackColor = true;
            this.btnObjectSelector.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // lblSetup3
            // 
            this.lblSetup3.AutoSize = true;
            this.lblSetup3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSetup3.Location = new System.Drawing.Point(10, 8);
            this.lblSetup3.Name = "lblSetup3";
            this.lblSetup3.Size = new System.Drawing.Size(213, 55);
            this.lblSetup3.TabIndex = 0;
            this.lblSetup3.Text = "3. Finish";
            // 
            // btnClassifier
            // 
            this.btnClassifier.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClassifier.Location = new System.Drawing.Point(6, 80);
            this.btnClassifier.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClassifier.Name = "btnClassifier";
            this.btnClassifier.Size = new System.Drawing.Size(333, 54);
            this.btnClassifier.TabIndex = 4;
            this.btnClassifier.Text = "Classifier";
            this.btnClassifier.UseVisualStyleBackColor = true;
            this.btnClassifier.Click += new System.EventHandler(this.btnClassifier_Click);
            // 
            // SetUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 500);
            this.Controls.Add(this.pnlSetup2);
            this.Controls.Add(this.pnlSetup3);
            this.Controls.Add(this.pnlSetup1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SetUp";
            this.Text = "Configuraton";
            this.pnlSetup1.ResumeLayout(false);
            this.pnlSetup1.PerformLayout();
            this.pnlSetup2.ResumeLayout(false);
            this.pnlSetup2.PerformLayout();
            this.pnlSetup3.ResumeLayout(false);
            this.pnlSetup3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSetup1;
        private System.Windows.Forms.Label lblSetup1;
        private System.Windows.Forms.Panel pnlSetup2;
        private System.Windows.Forms.Label lblSetup2;
        private System.Windows.Forms.Panel pnlSetup3;
        private System.Windows.Forms.Label lblSetup3;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Label lblFiles;
        private System.Windows.Forms.Label lblPreviewStat;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Label lblMask;
        private System.Windows.Forms.TextBox txtRegex;
        private System.Windows.Forms.Label lblOutputFile;
        private System.Windows.Forms.Button btnSelectOutput;
        private System.Windows.Forms.Button btnObjectSelector;
        private System.Windows.Forms.Button btnClassifier;
    }
}

