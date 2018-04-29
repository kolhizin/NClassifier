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
    public class OrientedRect
    {
        public OrientedRect(PointF center, PointF direction, float w, float h)
        {
            this.center = center;
            float len2 = direction.X * direction.X + direction.Y * direction.Y;
            float ilen = (float)(1.0 / Math.Sqrt(len2));
            this.dir = new PointF(direction.X * ilen, direction.Y * ilen);
            this.w = w;
            this.h = h;
        }
        public PointF dir, center;
        public float w, h;

        public PointF getNormal()
        {
            return new PointF(dir.Y, -dir.X);
        }
        public PointF getPoint(float x, float y)
        {
            //r = c + x * normal + y * dir
            //rx = cx + x * nx + y * dx
            //ry = cy + x * ny + y * dy
            //rx = cx + x * dy + y * dx
            //ry = cy - x * dx + y * dy
            return new PointF(center.X + w * x * dir.Y + h * y * dir.X, center.Y - w * x * dir.X + h * y * dir.Y);
        }
        public PointF[] getAbsPoints(float xCoef, float yCoef, float xOff, float yOff)
        {
            PointF[] res = new PointF[4];

            PointF tmp = this.getPoint(-1.0f, -1.0f);
            res[0] = new PointF(xOff + tmp.X * xCoef, yOff + tmp.Y * yCoef);

            tmp = this.getPoint(1.0f, -1.0f);
            res[1] = new PointF(xOff + tmp.X * xCoef, yOff + tmp.Y * yCoef);

            tmp = this.getPoint(1.0f, 1.0f);
            res[2] = new PointF(xOff + tmp.X * xCoef, yOff + tmp.Y * yCoef);

            tmp = this.getPoint(-1.0f, 1.0f);
            res[3] = new PointF(xOff + tmp.X * xCoef, yOff + tmp.Y * yCoef);

            return res;
        }

        public void draw(PaintEventArgs e, Pen pen, float xCoef, float yCoef, float xOff, float yOff)
        {
            PointF[] pts = this.getAbsPoints(xCoef, yCoef, xOff, yOff);
            
            for(int i = 0; i < pts.Length; i++)
            {
                int j = i + 1;
                if (j >= pts.Length)
                    j = 0;
                e.Graphics.DrawLine(pen, pts[i], pts[j]);    
            }
        }
    }
    public partial class ImageSelector : Form
    {
        private string[] in_fnames;
        private string out_fname;
        private int cur_index = 0;

        OrientedRect rect = null;

        private PictureBox[] pBoxes = null;

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
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.Red, 2))
            {
                float imgAspect = (float)pbMain.Image.Width / (float)pbMain.Image.Height;
                float pbAspect = (float)pbMain.ClientSize.Width / (float)pbMain.ClientSize.Height;
                int uWidth = 0;
                int uHeight = 0;
                if(imgAspect > pbAspect)
                {
                    uWidth = pbMain.ClientSize.Width;
                    uHeight = (int)(pbMain.ClientSize.Width / imgAspect);
                }else
                {
                    uWidth = (int)(pbMain.ClientSize.Height * imgAspect);
                    uHeight = pbMain.ClientSize.Height;
                }
                float xCoef = (float)uWidth / 2.0f;
                float yCoef = (float)uHeight / 2.0f;
                float xOff = pbMain.ClientSize.Width / 2.0f;
                float yOff = pbMain.ClientSize.Height / 2.0f;
                rect.draw(e, pen, xCoef, yCoef, xOff, yOff);
            }
        }
    }
}
