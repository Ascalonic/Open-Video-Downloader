namespace Open_Video_Downloader.UserControls
{
    partial class DownloadItem
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
            this.lblSourceUrl = new System.Windows.Forms.LinkLabel();
            this.pnlDownloadStatus = new System.Windows.Forms.Panel();
            this.lblCurrentStatus = new System.Windows.Forms.Label();
            this.lblProgressPercentage = new System.Windows.Forms.Label();
            this.prgxDownloadProgress = new System.Windows.Forms.ProgressBar();
            this.pnlUrlResolution = new System.Windows.Forms.Panel();
            this.lblResolvingUrl = new System.Windows.Forms.Label();
            this.pnlPostDownload = new System.Windows.Forms.Panel();
            this.btnViewInFolder = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.picSpinner = new System.Windows.Forms.PictureBox();
            this.pnlDownloadStatus.SuspendLayout();
            this.pnlUrlResolution.SuspendLayout();
            this.pnlPostDownload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSourceUrl
            // 
            this.lblSourceUrl.AutoSize = true;
            this.lblSourceUrl.Location = new System.Drawing.Point(12, 10);
            this.lblSourceUrl.Name = "lblSourceUrl";
            this.lblSourceUrl.Size = new System.Drawing.Size(57, 15);
            this.lblSourceUrl.TabIndex = 0;
            this.lblSourceUrl.TabStop = true;
            this.lblSourceUrl.Text = "Page URL";
            this.lblSourceUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblSourceUrl_LinkClicked);
            // 
            // pnlDownloadStatus
            // 
            this.pnlDownloadStatus.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlDownloadStatus.Controls.Add(this.lblCurrentStatus);
            this.pnlDownloadStatus.Controls.Add(this.lblProgressPercentage);
            this.pnlDownloadStatus.Controls.Add(this.prgxDownloadProgress);
            this.pnlDownloadStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDownloadStatus.Location = new System.Drawing.Point(501, 0);
            this.pnlDownloadStatus.Name = "pnlDownloadStatus";
            this.pnlDownloadStatus.Size = new System.Drawing.Size(160, 34);
            this.pnlDownloadStatus.TabIndex = 1;
            // 
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.Location = new System.Drawing.Point(73, 16);
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Size = new System.Drawing.Size(87, 15);
            this.lblCurrentStatus.TabIndex = 2;
            this.lblCurrentStatus.Text = "Downloading...";
            // 
            // lblProgressPercentage
            // 
            this.lblProgressPercentage.AutoSize = true;
            this.lblProgressPercentage.Location = new System.Drawing.Point(3, 16);
            this.lblProgressPercentage.Name = "lblProgressPercentage";
            this.lblProgressPercentage.Size = new System.Drawing.Size(23, 15);
            this.lblProgressPercentage.TabIndex = 1;
            this.lblProgressPercentage.Text = "0%";
            // 
            // prgxDownloadProgress
            // 
            this.prgxDownloadProgress.Location = new System.Drawing.Point(3, 3);
            this.prgxDownloadProgress.Name = "prgxDownloadProgress";
            this.prgxDownloadProgress.Size = new System.Drawing.Size(154, 12);
            this.prgxDownloadProgress.TabIndex = 0;
            // 
            // pnlUrlResolution
            // 
            this.pnlUrlResolution.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlUrlResolution.Controls.Add(this.lblResolvingUrl);
            this.pnlUrlResolution.Controls.Add(this.picSpinner);
            this.pnlUrlResolution.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlUrlResolution.Location = new System.Drawing.Point(341, 0);
            this.pnlUrlResolution.Name = "pnlUrlResolution";
            this.pnlUrlResolution.Size = new System.Drawing.Size(160, 34);
            this.pnlUrlResolution.TabIndex = 2;
            // 
            // lblResolvingUrl
            // 
            this.lblResolvingUrl.AutoSize = true;
            this.lblResolvingUrl.Location = new System.Drawing.Point(54, 10);
            this.lblResolvingUrl.Name = "lblResolvingUrl";
            this.lblResolvingUrl.Size = new System.Drawing.Size(100, 15);
            this.lblResolvingUrl.TabIndex = 1;
            this.lblResolvingUrl.Text = "Resolving Video...";
            // 
            // pnlPostDownload
            // 
            this.pnlPostDownload.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlPostDownload.Controls.Add(this.btnViewInFolder);
            this.pnlPostDownload.Controls.Add(this.btnPlay);
            this.pnlPostDownload.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlPostDownload.Location = new System.Drawing.Point(150, 0);
            this.pnlPostDownload.Name = "pnlPostDownload";
            this.pnlPostDownload.Size = new System.Drawing.Size(191, 34);
            this.pnlPostDownload.TabIndex = 3;
            // 
            // btnViewInFolder
            // 
            this.btnViewInFolder.Image = global::Open_Video_Downloader.Properties.Resources.open_folder;
            this.btnViewInFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewInFolder.Location = new System.Drawing.Point(68, 2);
            this.btnViewInFolder.Name = "btnViewInFolder";
            this.btnViewInFolder.Size = new System.Drawing.Size(119, 29);
            this.btnViewInFolder.TabIndex = 1;
            this.btnViewInFolder.Text = "    View in Folder";
            this.btnViewInFolder.UseVisualStyleBackColor = true;
            this.btnViewInFolder.Click += new System.EventHandler(this.btnViewInFolder_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Image = global::Open_Video_Downloader.Properties.Resources.movie;
            this.btnPlay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPlay.Location = new System.Drawing.Point(0, 2);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(67, 29);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "       Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // picSpinner
            // 
            this.picSpinner.Image = global::Open_Video_Downloader.Properties.Resources.loading_png_gif;
            this.picSpinner.Location = new System.Drawing.Point(4, 2);
            this.picSpinner.Name = "picSpinner";
            this.picSpinner.Size = new System.Drawing.Size(54, 31);
            this.picSpinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSpinner.TabIndex = 0;
            this.picSpinner.TabStop = false;
            // 
            // DownloadItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(this.pnlPostDownload);
            this.Controls.Add(this.pnlUrlResolution);
            this.Controls.Add(this.pnlDownloadStatus);
            this.Controls.Add(this.lblSourceUrl);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DownloadItem";
            this.Size = new System.Drawing.Size(661, 34);
            this.Load += new System.EventHandler(this.DownloadItem_Load);
            this.Enter += new System.EventHandler(this.DownloadItem_Enter);
            this.Leave += new System.EventHandler(this.DownloadItem_Leave);
            this.pnlDownloadStatus.ResumeLayout(false);
            this.pnlDownloadStatus.PerformLayout();
            this.pnlUrlResolution.ResumeLayout(false);
            this.pnlUrlResolution.PerformLayout();
            this.pnlPostDownload.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSpinner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lblSourceUrl;
        private System.Windows.Forms.Panel pnlDownloadStatus;
        private System.Windows.Forms.Label lblProgressPercentage;
        private System.Windows.Forms.ProgressBar prgxDownloadProgress;
        private System.Windows.Forms.Label lblCurrentStatus;
        private System.Windows.Forms.Panel pnlUrlResolution;
        private System.Windows.Forms.PictureBox picSpinner;
        private System.Windows.Forms.Label lblResolvingUrl;
        private System.Windows.Forms.Panel pnlPostDownload;
        private System.Windows.Forms.Button btnViewInFolder;
        private System.Windows.Forms.Button btnPlay;
    }
}
