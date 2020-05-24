using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Open_Video_Downloader
{
    public partial class DownloadSettings : Form
    {
        public DownloadSettings()
        {
            InitializeComponent();
        }

        private void LoadSettings()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            txtDownloadDirectory.Text = config.AppSettings.Settings["saveDirectory"].Value.ToString();
            txtMaxThreads.Text = config.AppSettings.Settings["maxThreads"].Value.ToString();

            if(string.IsNullOrEmpty(txtDownloadDirectory.Text))
            {
                txtDownloadDirectory.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            }
        }

        private void DownloadSettings_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void btnBrowseDownloadDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowDialog();
            if(!Directory.Exists(folderBrowser.SelectedPath))
            {
                MessageBox.Show("Please select a valid directory to save the downloaded videos", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                txtDownloadDirectory.Text = folderBrowser.SelectedPath;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtDownloadDirectory.Text))
            {
                int maxthreads = Convert.ToInt16(txtMaxThreads.Value);
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["saveDirectory"].Value = txtDownloadDirectory.Text;
                config.AppSettings.Settings["maxThreads"].Value = maxthreads.ToString();

                config.Save();
                DialogResult = DialogResult.OK;
            }
            else
            {
                Directory.CreateDirectory(txtDownloadDirectory.Text);
                btnSave_Click(null, null);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
