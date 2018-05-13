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

        private BindingSource lstBs;

        private void save()
        {
            string txt = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            System.IO.File.WriteAllText(out_fname, txt);
        }
        private void updateLabelsView()
        {
            string fk = System.IO.Path.GetFileName(in_fnames[cur_index]);
            lstBs.DataSource = null;
            if (result.observations.ContainsKey(fk))
                lstBs.DataSource = result.observations[fk];
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
            pbMain.Image = Image.FromFile(in_fnames[cur_index]);
            txtCurIndex.Text = (cur_index + 1).ToString();
            updateLabelsView();
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
            pbMain.Image = Image.FromFile(in_fnames[cur_index]);
            txtCurIndex.Text = (cur_index + 1).ToString();
            updateLabelsView();
        }
        private void addLabel(string lbl)
        {
            string fk = System.IO.Path.GetFileName(in_fnames[cur_index]);
            if (!result.observations.ContainsKey(fk))
            {
                result.observations[fk] = new List<string>();
                updateLabelsView();
            }
            //check that label is not present
            if (result.observations[fk].Contains(lbl))
                return;
            //add label if not exists
            result.observations[fk].Add(lbl);
            lstBs.ResetBindings(false);
            if (chkAutoNext.Checked)
                nextImage();
        }
        public ImageClassifier(string[] in_fnames, string out_fname)
        {
            this.in_fnames = in_fnames;
            this.out_fname = out_fname;
            InitializeComponent();
            lstBs = new BindingSource();
            lstLabels.DataSource = lstBs;
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
            lblTotal.Text = in_fnames.Length.ToString();
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
                    DialogResult res = MessageBox.Show("Specified label does not exist in dictionary! Do you want to add it?", "Attention!", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                        result.labels.Add(lbl, 0);
                    else
                        return;
                }
                addLabel(lbl);
            }
            if(e.KeyCode == Keys.Escape)
            {
                pbMain.Focus();
            }
        }

        private void ImageClassifier_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (chkSaveExit.Checked)
                save();
        }

        private void ImageClassifier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.OemCloseBrackets || e.KeyCode == Keys.OemPeriod)
            {
                nextImage();
                e.Handled = true;
                return;
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.OemOpenBrackets || e.KeyCode == Keys.Oemcomma)
            {
                setImage(cur_index - 1);
                e.Handled = true;
                return;
            }
            
            //process hotkeys
            int hotKey = -1;
            if (e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.D1)
                hotKey = 1;
            if (e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.D2)
                hotKey = 2;
            if (e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.D3)
                hotKey = 3;
            if (e.KeyCode == Keys.NumPad4 || e.KeyCode == Keys.D4)
                hotKey = 4;
            if (e.KeyCode == Keys.NumPad5 || e.KeyCode == Keys.D5)
                hotKey = 5;
            if (e.KeyCode == Keys.NumPad6 || e.KeyCode == Keys.D6)
                hotKey = 6;
            if (e.KeyCode == Keys.NumPad7 || e.KeyCode == Keys.D7)
                hotKey = 7;
            if (e.KeyCode == Keys.NumPad8 || e.KeyCode == Keys.D8)
                hotKey = 8;
            if (e.KeyCode == Keys.NumPad9 || e.KeyCode == Keys.D9)
                hotKey = 9;
            if(hotKey > 0 && !txtLabel.Focused)
            {
                try
                {
                    string label = result.labels.First(x => x.Value == hotKey).Key;
                    addLabel(label);
                    e.Handled = true;
                    return;
                }
                catch
                {
                    //no label
                }                
            }
        }

        private void spltMain_Panel2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (cur_index >= in_fnames.Length)
                return;
            string fk = System.IO.Path.GetFileName(in_fnames[cur_index]);
            if (!result.observations.ContainsKey(fk))
                return;
            result.observations[fk].Clear();
            lstBs.ResetBindings(false);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (cur_index >= in_fnames.Length)
                return;
            string fk = System.IO.Path.GetFileName(in_fnames[cur_index]);
            if (!result.observations.ContainsKey(fk))
                return;
            result.observations[fk].Remove(lstLabels.SelectedItem.ToString());
            lstBs.ResetBindings(false);
        }

        private void txtCurIndex_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                int res = 0;
                if (!int.TryParse(txtCurIndex.Text, out res))
                    MessageBox.Show("Invalid input!", "Error!");
                else
                    setImage(res - 1);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
