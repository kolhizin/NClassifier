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
    public partial class ConfirmRegionDialog : Form
    {
        public ConfirmRegionDialog(Image img, OrientedRect rect)
        {            
            InitializeComponent();
            float rW = Math.Abs(rect.dir.X) * (float)img.Height + Math.Abs(rect.dir.Y) * (float)img.Width;
            float rH = Math.Abs(rect.dir.X) * (float)img.Width + Math.Abs(rect.dir.Y) * (float)img.Height;
            float srcAspect = (rect.w * rW) / (rect.h * rH);
            float dstAspect = (float)picBox.ClientSize.Width / (float)picBox.ClientSize.Height;
            int w = 0;
            int h = 0;
            if(srcAspect > dstAspect)
            {
                //src image is wider, hence
                w = picBox.ClientSize.Width;
                h = (int)(w / srcAspect);
            }else
            {
                h = picBox.ClientSize.Height;
                w = (int)(h * srcAspect);
            }
            
            picBox.Image = rect.transformBitmap(new Bitmap(img), w, h);
        }
    }
}
