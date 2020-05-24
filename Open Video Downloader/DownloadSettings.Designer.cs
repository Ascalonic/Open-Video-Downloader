namespace Open_Video_Downloader
{
    partial class DownloadSettings
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadSettings));
            this.lblDownloadDirectory = new System.Windows.Forms.Label();
            this.txtDownloadDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowseDownloadDirectory = new System.Windows.Forms.Button();
            this.lblMaxThreads = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtMaxThreads = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxThreads)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDownloadDirectory
            // 
            this.lblDownloadDirectory.AutoSize = true;
            this.lblDownloadDirectory.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDownloadDirectory.Location = new System.Drawing.Point(12, 20);
            this.lblDownloadDirectory.Name = "lblDownloadDirectory";
            this.lblDownloadDirectory.Size = new System.Drawing.Size(117, 15);
            this.lblDownloadDirectory.TabIndex = 0;
            this.lblDownloadDirectory.Text = "Download Directory:";
            // 
            // txtDownloadDirectory
            // 
            this.txtDownloadDirectory.Location = new System.Drawing.Point(15, 47);
            this.txtDownloadDirectory.Name = "txtDownloadDirectory";
            this.txtDownloadDirectory.Size = new System.Drawing.Size(404, 23);
            this.txtDownloadDirectory.TabIndex = 1;
            // 
            // btnBrowseDownloadDirectory
            // 
            this.btnBrowseDownloadDirectory.Location = new System.Drawing.Point(425, 47);
            this.btnBrowseDownloadDirectory.Name = "btnBrowseDownloadDirectory";
            this.btnBrowseDownloadDirectory.Size = new System.Drawing.Size(34, 23);
            this.btnBrowseDownloadDirectory.TabIndex = 2;
            this.btnBrowseDownloadDirectory.Text = "...";
            this.btnBrowseDownloadDirectory.UseVisualStyleBackColor = true;
            this.btnBrowseDownloadDirectory.Click += new System.EventHandler(this.btnBrowseDownloadDirectory_Click);
            // 
            // lblMaxThreads
            // 
            this.lblMaxThreads.AutoSize = true;
            this.lblMaxThreads.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxThreads.Location = new System.Drawing.Point(12, 97);
            this.lblMaxThreads.Name = "lblMaxThreads";
            this.lblMaxThreads.Size = new System.Drawing.Size(156, 15);
            this.lblMaxThreads.TabIndex = 3;
            this.lblMaxThreads.Text = "Maximum Threads Allowed:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(439, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "(0 for indefinite. Will use the CPU capabilties to calculate the maxmimum threads" +
    ")";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(303, 171);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(384, 171);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtMaxThreads
            // 
            this.txtMaxThreads.Location = new System.Drawing.Point(174, 95);
            this.txtMaxThreads.Name = "txtMaxThreads";
            this.txtMaxThreads.Size = new System.Drawing.Size(104, 23);
            this.txtMaxThreads.TabIndex = 8;
            // 
            // DownloadSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 206);
            this.Controls.Add(this.txtMaxThreads);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMaxThreads);
            this.Controls.Add(this.btnBrowseDownloadDirectory);
            this.Controls.Add(this.txtDownloadDirectory);
            this.Controls.Add(this.lblDownloadDirectory);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DownloadSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Download Settings";
            this.Load += new System.EventHandler(this.DownloadSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxThreads)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDownloadDirectory;
        private System.Windows.Forms.TextBox txtDownloadDirectory;
        private System.Windows.Forms.Button btnBrowseDownloadDirectory;
        private System.Windows.Forms.Label lblMaxThreads;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown txtMaxThreads;
    }
}