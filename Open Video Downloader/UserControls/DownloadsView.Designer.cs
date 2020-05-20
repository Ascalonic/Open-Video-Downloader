namespace Open_Video_Downloader.UserControls
{
    partial class DownloadsView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowDownloadListContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowDownloadListContainer
            // 
            this.flowDownloadListContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowDownloadListContainer.Location = new System.Drawing.Point(0, 0);
            this.flowDownloadListContainer.Name = "flowDownloadListContainer";
            this.flowDownloadListContainer.Size = new System.Drawing.Size(602, 242);
            this.flowDownloadListContainer.TabIndex = 0;
            // 
            // DownloadsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowDownloadListContainer);
            this.Name = "DownloadsView";
            this.Size = new System.Drawing.Size(602, 242);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowDownloadListContainer;
    }
}
