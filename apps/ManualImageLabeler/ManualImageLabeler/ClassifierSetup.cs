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
    public partial class ClassifierSetup : Form
    {
        private string[] in_fnames;
        private string out_fname;
        private ClassifierResult cres;
        private void save()
        {
            if (cres == null)
                cres = new ClassifierResult();
            cres.labels.Clear();
            int numLables = dgvLabels.Rows.Count;
            for (int i = 0; i < numLables; i++)
            {
                object lbl_obj = dgvLabels.Rows[i].Cells["LabelName"].Value;
                if (lbl_obj == null)
                    continue;
                string lbl = ((string)lbl_obj).ToLower();
                object hk_obj = dgvLabels.Rows[i].Cells["Hotkey"].Value;
                if (hk_obj == null)
                    cres.labels[lbl] = 0;
                else
                    cres.labels[lbl] = int.Parse((string)hk_obj);

            }

            string txt = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
            System.IO.File.WriteAllText(out_fname, txt);
        }
        private void restore()
        {
            dgvLabels.Rows.Clear();
            if (System.IO.File.Exists(out_fname))
            {
                string txt = System.IO.File.ReadAllText(out_fname);
                cres = Newtonsoft.Json.JsonConvert.DeserializeObject<ClassifierResult>(txt);
                if (cres.observations.Any())
                    MessageBox.Show("File contains already labeled observations, changes to label dictionary may cause inconsistency!", "Note");
                foreach (KeyValuePair<string, int> kv in cres.labels)
                {
                    object hk = null;
                    if (kv.Value > 0)
                        hk = kv.Value.ToString();
                    dgvLabels.Rows.Add(kv.Key, hk);
                }
            }
            else
                cres = null;
        }
        private Tuple<bool, string> check()
        {
            int numLables = dgvLabels.Rows.Count;
            if (numLables < 2)
                return new Tuple<bool, string>(false, "Number of labels should be no less than 2.");

            Dictionary<string, int> labelHK = new Dictionary<string, int>();
            int[] hkCount = new int[9];
            for (int i = 0; i < 9; i++)
                hkCount[i] = 0;

            int realNumLables = 0;
            for (int i = 0; i < numLables; i++)
            {
                object lbl_obj = dgvLabels.Rows[i].Cells["LabelName"].Value;
                if (lbl_obj == null)
                    continue;
                realNumLables++;
                string lbl = ((string)lbl_obj).ToLower();
                if (labelHK.ContainsKey(lbl))
                    return new Tuple<bool, string>(false, string.Format("Labels should be unique. \"{0}\" specified at least twice.", lbl));

                object hk_obj = dgvLabels.Rows[i].Cells["Hotkey"].Value;
                if (hk_obj == null)
                    labelHK[lbl] = 0;
                else
                {
                    int hk = int.Parse((string)hk_obj);
                    hkCount[hk - 1]++;
                    if (hkCount[hk - 1] > 1)
                        return new Tuple<bool, string>(false, string.Format("Hotkeys should be used no more than onece. [{0}] specified at least twice.", hk));
                    labelHK[lbl] = hk;
                }
            }
            if (realNumLables < 2)
                return new Tuple<bool, string>(false, "Number of labels should be no less than 2.");
            return new Tuple<bool, string>(true, "Ok");
        }
        public ClassifierSetup(string[] in_fnames, string out_fname)
        {
            this.in_fnames = in_fnames;
            this.out_fname = out_fname;
            InitializeComponent();
            restore();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Tuple<bool, string> res = check();
            if (!res.Item1)
            {
                MessageBox.Show(res.Item2, "Cannot confirm!");
                return;
            }
            save();
            this.Hide();
            var f = new ImageClassifier(in_fnames, out_fname);
            f.FormClosed += (s, agrs) => this.Close();
            f.Show();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            restore();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Tuple<bool, string> res = check();
            if(!res.Item1)
            {
                MessageBox.Show(res.Item2, "Saving error!");
                return;
            }
            save();
        }
    }
}
