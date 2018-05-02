using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManualImageObjectSelector
{
    public partial class SetUp : Form
    {
        private string[] in_fnames = null;
        private string out_fname = null;

        public SetUp()
        {
            InitializeComponent();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                lblFolder.Text = fbd.SelectedPath;
                updatePreview();
            }
        }
                
        private void updatePreview()
        {
            string folder = lblFolder.Text;
            string regex = txtRegex.Text;
            if(string.IsNullOrWhiteSpace(folder) || string.IsNullOrWhiteSpace(regex))
            {
                lblPreviewStat.Text = "Folder or regex not specified!";
                lblFiles.Text = "No files (due to issues)";
                in_fnames = null;
                return;
            }
            if(!Directory.Exists(folder))
            {
                lblPreviewStat.Text = "Folder does not exist!";
                lblFiles.Text = "No files (due to issues)";
                in_fnames = null;
                return;
            }
            Regex re = null;
            try
            {
                re = new Regex(regex, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            catch(ArgumentException)
            {
                lblPreviewStat.Text = "Incorrect regex!";
                lblFiles.Text = "No files (due to issues)";
                in_fnames = null;
                return;
            }
            in_fnames = Directory.GetFiles(folder).Where(f => re.Match(f).Success).ToArray();
            lblPreviewStat.Text = string.Format("{0} files selected", in_fnames.Length);
            if(in_fnames.Length <= 4)
                lblFiles.Text = string.Join("\n", in_fnames);
            else
                lblFiles.Text = string.Format("{0}\n{1}\n...\n{2}\n{3}", in_fnames[0], in_fnames[1], in_fnames[in_fnames.Length - 2], in_fnames[in_fnames.Length - 1]);
        }

        private void txtRegex_TextChanged(object sender, EventArgs e)
        {
            updatePreview();
        }

        private void btnSelectOutput_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if(Directory.Exists(lblFolder.Text))
                sfd.InitialDirectory = lblFolder.Text;
            sfd.Filter = "JSON files (.json)|*.json";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                lblOutputFile.Text = sfd.FileName;
                out_fname = sfd.FileName;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if(in_fnames == null)
            {
                MessageBox.Show("No input files selected (due to issues)!");
                return;
            }
            if(out_fname == null)
            {
                MessageBox.Show("No output file specified!");
                return;
            }
            this.Hide();
            var f = new ImageSelector(in_fnames, out_fname);
            f.FormClosed += (s, agrs) => this.Close();
            f.Show();
        }
    }
}
