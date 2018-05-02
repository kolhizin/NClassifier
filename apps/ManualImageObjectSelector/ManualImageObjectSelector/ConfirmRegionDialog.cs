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
            picBox.Image = rect.transformBitmap(new Bitmap(img), picBox.ClientSize.Width, picBox.ClientSize.Height);
        }
    }
}
