using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManualImageObjectSelector
{
    public partial class ImageSelector : Form
    {
        private string[] in_fnames;
        private string out_fname;
        private int cur_index = 0;
        private PointF cur_direction;

        private bool selectionModeOrientation = false; //true -- classic region selection, false -- orientation selection

        private Point selectPoint1;
        private Point selectPoint2;

        OrientedRect rect = null;

        private PictureBox[] pBoxes = null;
        private void updateCurrentOrientation(float dx, float dy)
        {
            float l2 = dx * dx + dy * dy;
            float il = 1.0f / (float)Math.Sqrt(l2);
            cur_direction = new PointF(dx * il, dy * il);
            pbOrientation.Invalidate();
        }
        private void recreatePreviews()
        {
            int w = pnlImageList.Width;
            int h = pnlImageList.Height;
            int n = h / w;
            if (w > h)
                n = 1;
            if(pBoxes != null)
            {
                if (pBoxes.Length != n)
                {
                    foreach (PictureBox p in pBoxes)
                    {
                        pnlImageList.Controls.Remove(p);
                        p.Dispose();
                    }
                    pBoxes = null;
                }
                else
                {
                    resizePreviews();
                    return;
                }
            }
            pBoxes = new PictureBox[n];
            for (int i = 0; i < n; i++)
            {
                pBoxes[i] = new PictureBox();
                pnlImageList.Controls.Add(pBoxes[i]);
                pBoxes[i].Parent = pnlImageList;
                pBoxes[i].SizeMode = PictureBoxSizeMode.Zoom;
                pBoxes[i].Enabled = true;
                pBoxes[i].Visible = true;
                pBoxes[i].Show();
            }
            resizePreviews();
            updateNewPreviews();
        }
        private void resizePreviews()
        {
            int w = pnlImageList.Width;
            int h = pnlImageList.Height;
            int dh = (h - pBoxes.Length * w) / pBoxes.Length;
            int dh0 = dh / 2;
            int dh1 = dh - dh0;

            int cur_y = 0;
            for(int i = 0; i < pBoxes.Length; i++)
            {
                cur_y += dh0;
                pBoxes[i].Location = new System.Drawing.Point(0, cur_y);
                cur_y += w + dh1;
                pBoxes[i].Width = w;
                pBoxes[i].Height = w;
            }
        }

        private void updateNewPreviews()
        {
            for(int i = 0; i < pBoxes.Length; i++)
            {
                int idx = cur_index + i + 1;
                if (idx < in_fnames.Length && pBoxes[i].Image == null)
                    pBoxes[i].Image = Image.FromFile(in_fnames[idx]);
            }
        }

        private void setMainImage(int idx)
        {
            cur_index = idx;
            pbMain.Image = null;
            if (cur_index < in_fnames.Length)
                pbMain.Image = Image.FromFile(in_fnames[cur_index]);
            for(int i = 0; i < pBoxes.Length; i++)
            {
                int pidx = cur_index + i + 1;
                if (pidx < in_fnames.Length)
                    pBoxes[i].Image = Image.FromFile(in_fnames[pidx]);
                else
                    pBoxes[i].Image = null;
            }
        }
        private void setMainImageNext()
        {
            cur_index += 1;
            pbMain.Image = null;
            if (cur_index < in_fnames.Length)
            {
                if (pBoxes.Length > 0)
                    pbMain.Image = pBoxes[0].Image;
                else
                    pbMain.Image = Image.FromFile(in_fnames[cur_index]);
            }
            for (int i = 0; i < pBoxes.Length - 1; i++)
                pBoxes[i].Image = pBoxes[i + 1].Image;
            if (cur_index + pBoxes.Length < in_fnames.Length)
                pBoxes.Last().Image = Image.FromFile(in_fnames[cur_index + pBoxes.Length]);
            else
                pBoxes.Last().Image = null;
        }

        public ImageSelector(string [] in_fnames, string out_fname)
        {
            this.in_fnames = in_fnames;
            this.out_fname = out_fname;
            InitializeComponent();
            recreatePreviews();
            setMainImage(0);
            rect = new OrientedRect(new PointF(0.2f, 0.4f), new PointF(1, 3), 0.1f, 0.1f);
            cur_direction = new PointF(0.0f, 1.0f);
        }

        private void pnlImageList_SizeChanged(object sender, EventArgs e)
        {
            recreatePreviews();
        }

        private void ImageSelector_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void ImageSelector_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
                setMainImageNext();
            if (e.KeyCode == Keys.W)
                updateCurrentOrientation(0.0f, 1.0f);
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.X)
                updateCurrentOrientation(0.0f, -1.0f);
            if (e.KeyCode == Keys.A)
                updateCurrentOrientation(-1.0f, 0.0f);
            if (e.KeyCode == Keys.D)
                updateCurrentOrientation(1.0f, 0.0f);
            if (e.KeyCode == Keys.Q)
                updateCurrentOrientation(-1.0f, 1.0f);
            if (e.KeyCode == Keys.E)
                updateCurrentOrientation(1.0f, 1.0f);
            if (e.KeyCode == Keys.Z)
                updateCurrentOrientation(-1.0f, -1.0f);
            if (e.KeyCode == Keys.C)
                updateCurrentOrientation(1.0f, -1.0f);
            if (e.KeyCode == Keys.R)
                selectionModeOrientation = !selectionModeOrientation;
        }

        private PointF getTransformCoef()
        {
            float imgAspect = (float)pbMain.Image.Width / (float)pbMain.Image.Height;
            float pbAspect = (float)pbMain.ClientSize.Width / (float)pbMain.ClientSize.Height;
            int uWidth = 0;
            int uHeight = 0;
            if (imgAspect > pbAspect)
            {
                uWidth = pbMain.ClientSize.Width;
                uHeight = (int)(pbMain.ClientSize.Width / imgAspect);
            }
            else
            {
                uWidth = (int)(pbMain.ClientSize.Height * imgAspect);
                uHeight = pbMain.ClientSize.Height;
            }
            float xCoef = (float)uWidth / 2.0f;
            float yCoef = -(float)uHeight / 2.0f; //invert for viewer <-> window coordinate systems
            return new PointF(xCoef, yCoef);
        }
        private PointF getTransformOff()
        {
            float xOff = pbMain.ClientSize.Width / 2.0f;
            float yOff = pbMain.ClientSize.Height / 2.0f;
            return new PointF(xOff, yOff);
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.Red, 2))
            {
                PointF coef = getTransformCoef();
                PointF off = getTransformOff();
                rect.draw(e, pen, coef.X, coef.Y, off.X, off.Y);
            }
        }

        private void pbOrientation_Paint(object sender, PaintEventArgs e)
        {
            float sizeFactor = 0.8f;
            int size = Math.Min(pbOrientation.ClientSize.Width, pbOrientation.ClientSize.Height);
            float arrSize = size * sizeFactor;
            int xc = pbOrientation.ClientSize.Width / 2;
            int yc = pbOrientation.ClientSize.Height / 2;
            float dx = 0.5f * cur_direction.X * arrSize;
            float dy = -0.5f * cur_direction.Y * arrSize;
            using (Pen p = new Pen(Color.Black, 20.0f))
            {
                p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                e.Graphics.DrawLine(p, xc - dx, yc - dy, xc + dx, yc + dy);
            }
        }

        private void pbMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            selectPoint1 = e.Location;
            selectPoint2 = e.Location;
        }

        private void pbMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            selectPoint2 = e.Location;
            PointF coef = getTransformCoef();
            PointF off = getTransformOff();
            float x1 = ((float)selectPoint1.X - off.X) / coef.X;
            float x2 = ((float)selectPoint2.X - off.X) / coef.X;
            float y1 = ((float)selectPoint1.Y - off.Y) / coef.Y;
            float y2 = ((float)selectPoint2.Y - off.Y) / coef.Y;
            if (selectionModeOrientation)
                updateCurrentOrientation(x2 - x1, y2 - y1);
            else
            {
                rect = new OrientedRect(cur_direction, new PointF(x1, y1), new PointF(x2, y2));
                pbMain.Invalidate();
            }
        }

        private void pbMain_MouseUp(object sender, MouseEventArgs e)
        {
            selectionModeOrientation = false;
        }
    }
}
