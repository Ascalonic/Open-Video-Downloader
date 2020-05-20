using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Open_Video_Downloader
{
    public partial class VideoQualitySelector : Form
    {
        public List<string> QualityLabels { get; set; }
        public string SelectedQuality { get; set; }

        public VideoQualitySelector()
        {
            InitializeComponent();
        }

        private void VideoQualitySelector_Load(object sender, EventArgs e)
        {
            cmbxVideoQuality.DataSource = QualityLabels;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(cmbxVideoQuality.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a video quality to start download", "No video quality selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SelectedQuality = cmbxVideoQuality.Text;
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
