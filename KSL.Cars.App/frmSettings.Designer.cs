namespace KSL.Cars.App
{
    partial class frmSettings
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkKeepStatsData = new System.Windows.Forms.CheckBox();
            this.chkSaveLastListings = new System.Windows.Forms.CheckBox();
            this.chkKeepSearchParameters = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(197, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(116, 226);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkKeepStatsData
            // 
            this.chkKeepStatsData.AutoSize = true;
            this.chkKeepStatsData.Location = new System.Drawing.Point(12, 35);
            this.chkKeepStatsData.Name = "chkKeepStatsData";
            this.chkKeepStatsData.Size = new System.Drawing.Size(212, 17);
            this.chkKeepStatsData.TabIndex = 0;
            this.chkKeepStatsData.Text = "Save year / make /  model stats on exit";
            this.chkKeepStatsData.UseVisualStyleBackColor = true;
            // 
            // chkSaveLastListings
            // 
            this.chkSaveLastListings.AutoSize = true;
            this.chkSaveLastListings.Location = new System.Drawing.Point(12, 58);
            this.chkSaveLastListings.Name = "chkSaveLastListings";
            this.chkSaveLastListings.Size = new System.Drawing.Size(161, 17);
            this.chkSaveLastListings.TabIndex = 0;
            this.chkSaveLastListings.Text = "Save results from last search";
            this.chkSaveLastListings.UseVisualStyleBackColor = true;
            // 
            // chkKeepSearchParameters
            // 
            this.chkKeepSearchParameters.AutoSize = true;
            this.chkKeepSearchParameters.Checked = true;
            this.chkKeepSearchParameters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKeepSearchParameters.Location = new System.Drawing.Point(12, 12);
            this.chkKeepSearchParameters.Name = "chkKeepSearchParameters";
            this.chkKeepSearchParameters.Size = new System.Drawing.Size(186, 17);
            this.chkKeepSearchParameters.TabIndex = 0;
            this.chkKeepSearchParameters.Text = "Keep last used search parameters";
            this.chkKeepSearchParameters.UseVisualStyleBackColor = true;
            // 
            // frmSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkSaveLastListings);
            this.Controls.Add(this.chkKeepSearchParameters);
            this.Controls.Add(this.chkKeepStatsData);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "frmSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.CheckBox chkKeepSearchParameters;
        public System.Windows.Forms.CheckBox chkKeepStatsData;
        public System.Windows.Forms.CheckBox chkSaveLastListings;
    }
}