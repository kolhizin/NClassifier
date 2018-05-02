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
            this.lpanRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOrientation)).BeginInit();
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
            // ImageSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 532);
            this.Controls.Add(this.lpanRoot);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ImageSelector";
            this.Text = "Image viewer";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ImageSelector_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ImageSelector_KeyPress);
            this.lpanRoot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOrientation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel lpanRoot;
        private System.Windows.Forms.Panel pnlImageList;
        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.PictureBox pbOrientation;
    }
}