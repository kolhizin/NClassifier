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
    public partial class ImageClassifier : Form
    {
        private string[] in_fnames;
        private string out_fname;
        private int cur_index = 0;
        private ClassifierResult result;

        private void save()
        {
            string txt = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            System.IO.File.WriteAllText(out_fname, txt);
        }
        private void setImage(int idx)
        {
            if (chkSaveNext.Checked)
                save();
            if (idx >= in_fnames.Length || idx < 0)
            {
                MessageBox.Show("Required image is out of bounds.", "Note");
                return;
            }
            cur_index = idx;
            pbMain.Image = null;
            if (cur_index < in_fnames.Length)
                pbMain.Image = Image.FromFile(in_fnames[cur_index]);
        }
        private void nextImage()
        {
            if (chkSaveNext.Checked)
                save();
            if (cur_index + 1 >= in_fnames.Length)
            {
                MessageBox.Show("Reached the end of input files.", "Note");
                return;
            }
            cur_index += 1;
            pbMain.Image = null;
            if (cur_index < in_fnames.Length)
                pbMain.Image = Image.FromFile(in_fnames[cur_index]);
        }
        private void addLabel(string lbl)
        {
            string fk = System.IO.Path.GetFileName(in_fnames[cur_index]);
            if (!result.observations.ContainsKey(fk))
                result.observations[fk] = new LinkedList<string>();
            result.observations[fk].AddLast(lbl);
            if (chkAutoNext.Checked)
                nextImage();
        }
        public ImageClassifier(string[] in_fnames, string out_fname)
        {
            this.in_fnames = in_fnames;
            this.out_fname = out_fname;
            InitializeComponent();
            if (!System.IO.File.Exists(out_fname))
            {
                MessageBox.Show("Output file (containing at least label dictionary) does not exist!", "Critical error!");
                throw new Exception("Output file (containing at least label dictionary) does not exist!");
            }
            string txt = System.IO.File.ReadAllText(out_fname);
            result = Newtonsoft.Json.JsonConvert.DeserializeObject<ClassifierResult>(txt);
            if(result.labels.Count < 2)
            {
                MessageBox.Show("Label dictionary is incomplete: must contain at least 2 labels!", "Critical error!");
                throw new Exception("Label dictionary is incomplete: must contain at least 2 labels!");
            }

            string[] values = new string[result.labels.Count];
            int idx = 0;
            foreach(string v in result.labels.Keys)
            {
                values[idx] = v;
                idx++;
            }
            txtLabel.AutoCompleteCustomSource.AddRange(values);
            setImage(0);
        }

        private void txtLabel_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtLabel_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string lbl = txtLabel.Text.ToLower();
                if (!result.labels.ContainsKey(lbl))
                {
                    DialogResult res = MessageBox.Show("Specified label does not exist in dictionary!", "Attention!", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                        result.labels.Add(lbl, 0);
                    else
                        return;
                }
                addLabel(lbl);
            }
        }

        private void ImageClassifier_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (chkSaveExit.Checked)
                save();
        }
    }
}
