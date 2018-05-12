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
        private OrientedRect cur_rect = null; //current rect
        private ImageRegionInfo cur_regions = null; //current regions
        private Dictionary<string, ImageRegionInfo> result = null;

        private bool selectionModeOrientation = false; //true -- classic region selection, false -- orientation selection

        private Point selectPoint1;
        private Point selectPoint2;

        
        private void updateCurrentOrientation(float dx, float dy)
        {
            float l2 = dx * dx + dy * dy;
            float il = 1.0f / (float)Math.Sqrt(l2);
            cur_direction = new PointF(dx * il, dy * il);
            pbOrientation.Invalidate();
        }
        private void updateCurRegionInfoViews()
        {
            lstRegions.Items.Clear();
            if (cur_regions == null)
                return;
            int num = cur_regions.regions.Count;
            for(int i = 0; i < num; i++)
                lstRegions.Items.Add(i + 1);
            pbMain.Invalidate();
        }
        private void resetCurRegionInfo(string imgName)
        {
            cur_regions = new ImageRegionInfo();
            cur_regions.imgName = imgName;
            cur_regions.regions = new LinkedList<OrientedRect>();
            updateCurRegionInfoViews();
        }
        private void resetCurRegionInfo(ImageRegionInfo info)
        {
            cur_regions = info;
            updateCurRegionInfoViews();
        }
        private void addCurRegionInfo()
        {
            cur_regions.add(cur_rect);
            updateCurRegionInfoViews();
        }
        private void removeCurRegionInfo(int idx)
        {
            cur_regions.remove(idx);
            updateCurRegionInfoViews();
        }
        private bool saveCurRegionInfo()
        {
            if (cur_regions == null)
                return true;
            string imgName = cur_regions.imgName;
            if (string.IsNullOrEmpty(imgName))
                return true;
            if (result.ContainsKey(imgName))
                result[imgName] = cur_regions;
            else
                result.Add(imgName, cur_regions);
            return true;
        }
        private void loadOrCreateImageRegion(string fname)
        {
            if (string.IsNullOrEmpty(fname))
                cur_regions = null;
            else
            {
                string imgName = System.IO.Path.GetFileName(fname);
                if(result.ContainsKey(imgName))
                    resetCurRegionInfo(result[imgName]);
                else
                    resetCurRegionInfo(imgName);
            }
        }
        private void saveResult()
        {
            string res = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            System.IO.File.WriteAllText(out_fname, res);
        }
        private void loadResult()
        {
            string res = System.IO.File.ReadAllText(out_fname);
            result = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, ImageRegionInfo>>(res);
        }
        private void setMainImage(int idx)
        {
            if (idx >= in_fnames.Length || idx < 0)
            {
                MessageBox.Show("Required image is out of bounds.", "Note");
                return;
            }
            cur_index = idx;
            pbMain.Image = null;
            if (cur_index < in_fnames.Length)
            {
                pbMain.Image = Image.FromFile(in_fnames[cur_index]);
                loadOrCreateImageRegion(in_fnames[cur_index]);
            }
        }
        private void setMainImageNext()
        {
            if(cur_index + 1 >= in_fnames.Length)
            {
                MessageBox.Show("Reached the end of input files.", "Note");
                return;
            }
            cur_index += 1;
            pbMain.Image = null;
            if (cur_index < in_fnames.Length)
            {
                pbMain.Image = Image.FromFile(in_fnames[cur_index]);
                loadOrCreateImageRegion(in_fnames[cur_index]);
            }
        }

        public ImageSelector(string [] in_fnames, string out_fname)
        {
            this.in_fnames = in_fnames;
            this.out_fname = out_fname;
            this.result = new Dictionary<string, ImageRegionInfo>();
            InitializeComponent();
            loadResult();
            setMainImage(0);
            cur_direction = new PointF(0.0f, 1.0f);
        }

        private void pnlImageList_SizeChanged(object sender, EventArgs e)
        {
        }

        private void ImageSelector_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void ImageSelector_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                if (saveCurRegionInfo())
                {
                    if (chkAutosaveNext.Checked)
                        saveResult();
                    setMainImageNext();
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                if (saveCurRegionInfo())
                {
                    if (chkAutosaveNext.Checked)
                        saveResult();
                    setMainImage(cur_index - 1);
                }
            }
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
            PointF coef = getTransformCoef();
            PointF off = getTransformOff();

            if (cur_rect != null)
                using (Pen pen = new Pen(Color.Gray, 1))
                {
                    cur_rect.draw(e, pen, coef.X, coef.Y, off.X, off.Y);
                }
            
            if(cur_regions != null)
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    int idx = 1;
                    Font f = new Font(FontFamily.GenericSansSerif, 12.0f, FontStyle.Bold);
                    foreach (OrientedRect r in cur_regions.regions)
                    {
                        r.draw(e, pen, coef.X, coef.Y, off.X, off.Y);
                        PointF pos = r.getAbsPoint(-0.9f, 0.9f, coef.X, coef.Y, off.X, off.Y);
                        e.Graphics.DrawString(idx.ToString(), f, Brushes.Lime, pos);
                        idx++;
                    }
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
                cur_rect = new OrientedRect(cur_direction, new PointF(x1, y1), new PointF(x2, y2));
                pbMain.Invalidate();
            }
        }

        private void pbMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectionModeOrientation)
                selectionModeOrientation = false;
            else
            {
                bool addRegion = true;
                if (selectPoint1.X == selectPoint2.X && selectPoint1.Y == selectPoint2.Y)
                    return; //covers only the case of non-move
                

                if (chkRegion.Checked)
                {
                    ConfirmRegionDialog crd = new ConfirmRegionDialog(pbMain.Image, cur_rect);
                    DialogResult res = crd.ShowDialog();
                    if (res != DialogResult.OK)
                        addRegion = false;
                }
                if(addRegion)
                {
                    addCurRegionInfo();
                    cur_rect = null;
                    pbMain.Invalidate();
                }
            }
        }

        private void ImageSelector_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                e.IsInputKey = true;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstRegions.SelectedItem == null)
            {
                MessageBox.Show("Region id is not specified!", "Note");
                return;
            }
            int idx = (int)lstRegions.SelectedItem - 1;
            if(idx >= cur_regions.regions.Count)
            {
                MessageBox.Show("Specified region id is out of bounds!", "Error!");
                return;
            }
            removeCurRegionInfo(idx);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (cur_regions == null)
                return;
            string imgname = cur_regions.imgName;
            resetCurRegionInfo(imgname);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (lstRegions.SelectedItem == null)
            {
                MessageBox.Show("Region id is not specified!", "Note");
                return;
            }
            int idx = (int)lstRegions.SelectedItem - 1;
            if (idx >= cur_regions.regions.Count)
            {
                MessageBox.Show("Specified region id is out of bounds!", "Error!");
                return;
            }
            ConfirmRegionDialog crd = new ConfirmRegionDialog(pbMain.Image, cur_regions.regions.ElementAt(idx));
            crd.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveResult();
        }

        private void ImageSelector_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (chkAutosaveExit.Checked)
                saveResult();
        }
    }
}
