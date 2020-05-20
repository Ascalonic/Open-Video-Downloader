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
            this.prgxDownloadProgress = new System.Windows.Forms.ProgressBar();
            this.lblProgressPercentage = new System.Windows.Forms.Label();
            this.lblCurrentStatus = new System.Windows.Forms.Label();
            this.pnlDownloadStatus.SuspendLayout();
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
            this.pnlDownloadStatus.Location = new System.Drawing.Point(285, 0);
            this.pnlDownloadStatus.Name = "pnlDownloadStatus";
            this.pnlDownloadStatus.Size = new System.Drawing.Size(160, 34);
            this.pnlDownloadStatus.TabIndex = 1;
            // 
            // prgxDownloadProgress
            // 
            this.prgxDownloadProgress.Location = new System.Drawing.Point(3, 3);
            this.prgxDownloadProgress.Name = "prgxDownloadProgress";
            this.prgxDownloadProgress.Size = new System.Drawing.Size(154, 12);
            this.prgxDownloadProgress.TabIndex = 0;
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
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.Location = new System.Drawing.Point(73, 16);
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Size = new System.Drawing.Size(87, 15);
            this.lblCurrentStatus.TabIndex = 2;
            this.lblCurrentStatus.Text = "Downloading...";
            // 
            // DownloadItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(this.pnlDownloadStatus);
            this.Controls.Add(this.lblSourceUrl);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DownloadItem";
            this.Size = new System.Drawing.Size(445, 34);
            this.Load += new System.EventHandler(this.DownloadItem_Load);
            this.Enter += new System.EventHandler(this.DownloadItem_Enter);
            this.Leave += new System.EventHandler(this.DownloadItem_Leave);
            this.pnlDownloadStatus.ResumeLayout(false);
            this.pnlDownloadStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lblSourceUrl;
        private System.Windows.Forms.Panel pnlDownloadStatus;
        private System.Windows.Forms.Label lblProgressPercentage;
        private System.Windows.Forms.ProgressBar prgxDownloadProgress;
        private System.Windows.Forms.Label lblCurrentStatus;
    }
}
