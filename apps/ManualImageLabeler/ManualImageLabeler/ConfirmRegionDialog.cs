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
            float srcAspect = ((float)img.Width * rect.w) / ((float)img.Height * rect.h);
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
