namespace KSL.Cars.App
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.grpDistance = new System.Windows.Forms.GroupBox();
            this.txtMiles = new System.Windows.Forms.TextBox();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.btnScrape = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.grpMileage = new System.Windows.Forms.GroupBox();
            this.txtMileageTo = new System.Windows.Forms.TextBox();
            this.txtMileageFrom = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.grpPrice = new System.Windows.Forms.GroupBox();
            this.txtPriceFrom = new System.Windows.Forms.TextBox();
            this.txtPriceTo = new System.Windows.Forms.TextBox();
            this.grpYear = new System.Windows.Forms.GroupBox();
            this.txtYearFrom = new System.Windows.Forms.TextBox();
            this.txtYearTo = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.grpResults = new System.Windows.Forms.GroupBox();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.carListingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.carListings = new KSL.Cars.App.CarListings();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowYears = new System.Windows.Forms.FlowLayoutPanel();
            this.flowMakes = new System.Windows.Forms.FlowLayoutPanel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.totalScrapeProgress = new System.Windows.Forms.ProgressBar();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancel = new System.Windows.Forms.Button();
            this.minimumWageWorker = new System.ComponentModel.BackgroundWorker();
            this.Delete = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Highlighted = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mileage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Listing_Link = new System.Windows.Forms.DataGridViewLinkColumn();
            this.VIN = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Make = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.City = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpDistance.SuspendLayout();
            this.grpMileage.SuspendLayout();
            this.grpPrice.SuspendLayout();
            this.grpYear.SuspendLayout();
            this.grpResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.carListingsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.carListings)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDistance
            // 
            this.grpDistance.Controls.Add(this.txtMiles);
            this.grpDistance.Controls.Add(this.txtZip);
            this.grpDistance.Controls.Add(this.Label4);
            this.grpDistance.Location = new System.Drawing.Point(183, 87);
            this.grpDistance.Name = "grpDistance";
            this.grpDistance.Size = new System.Drawing.Size(165, 55);
            this.grpDistance.TabIndex = 3;
            this.grpDistance.TabStop = false;
            this.grpDistance.Text = "Distance";
            // 
            // txtMiles
            // 
            this.txtMiles.Location = new System.Drawing.Point(6, 19);
            this.txtMiles.MaxLength = 3;
            this.txtMiles.Name = "txtMiles";
            this.txtMiles.Size = new System.Drawing.Size(47, 20);
            this.txtMiles.TabIndex = 0;
            this.txtMiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_LimitToNumbers);
            // 
            // txtZip
            // 
            this.txtZip.Location = new System.Drawing.Point(105, 19);
            this.txtZip.MaxLength = 5;
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(47, 20);
            this.txtZip.TabIndex = 1;
            this.txtZip.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_LimitToNumbers);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(56, 22);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(43, 13);
            this.Label4.TabIndex = 1;
            this.Label4.Text = "from zip";
            // 
            // btnScrape
            // 
            this.btnScrape.Location = new System.Drawing.Point(354, 27);
            this.btnScrape.Name = "btnScrape";
            this.btnScrape.Size = new System.Drawing.Size(117, 115);
            this.btnScrape.TabIndex = 5;
            this.btnScrape.Text = "Search";
            this.btnScrape.UseVisualStyleBackColor = true;
            this.btnScrape.Click += new System.EventHandler(this.btnScrape_Click);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(74, 22);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(10, 13);
            this.Label3.TabIndex = 1;
            this.Label3.Text = "-";
            // 
            // grpMileage
            // 
            this.grpMileage.Controls.Add(this.txtMileageTo);
            this.grpMileage.Controls.Add(this.txtMileageFrom);
            this.grpMileage.Controls.Add(this.Label3);
            this.grpMileage.Location = new System.Drawing.Point(12, 27);
            this.grpMileage.Name = "grpMileage";
            this.grpMileage.Size = new System.Drawing.Size(165, 55);
            this.grpMileage.TabIndex = 0;
            this.grpMileage.TabStop = false;
            this.grpMileage.Text = "Mileage";
            // 
            // txtMileageTo
            // 
            this.txtMileageTo.Location = new System.Drawing.Point(90, 19);
            this.txtMileageTo.MaxLength = 6;
            this.txtMileageTo.Name = "txtMileageTo";
            this.txtMileageTo.Size = new System.Drawing.Size(62, 20);
            this.txtMileageTo.TabIndex = 1;
            this.txtMileageTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMileageTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_LimitToNumbers);
            // 
            // txtMileageFrom
            // 
            this.txtMileageFrom.Location = new System.Drawing.Point(6, 19);
            this.txtMileageFrom.MaxLength = 6;
            this.txtMileageFrom.Name = "txtMileageFrom";
            this.txtMileageFrom.Size = new System.Drawing.Size(62, 20);
            this.txtMileageFrom.TabIndex = 0;
            this.txtMileageFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMileageFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_LimitToNumbers);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(74, 22);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(10, 13);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "-";
            // 
            // grpPrice
            // 
            this.grpPrice.Controls.Add(this.txtPriceFrom);
            this.grpPrice.Controls.Add(this.txtPriceTo);
            this.grpPrice.Controls.Add(this.Label2);
            this.grpPrice.Location = new System.Drawing.Point(12, 87);
            this.grpPrice.Name = "grpPrice";
            this.grpPrice.Size = new System.Drawing.Size(165, 55);
            this.grpPrice.TabIndex = 1;
            this.grpPrice.TabStop = false;
            this.grpPrice.Text = "Price";
            // 
            // txtPriceFrom
            // 
            this.txtPriceFrom.Location = new System.Drawing.Point(6, 19);
            this.txtPriceFrom.MaxLength = 6;
            this.txtPriceFrom.Name = "txtPriceFrom";
            this.txtPriceFrom.Size = new System.Drawing.Size(62, 20);
            this.txtPriceFrom.TabIndex = 0;
            this.txtPriceFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPriceFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_LimitToNumbers);
            // 
            // txtPriceTo
            // 
            this.txtPriceTo.Location = new System.Drawing.Point(90, 19);
            this.txtPriceTo.MaxLength = 6;
            this.txtPriceTo.Name = "txtPriceTo";
            this.txtPriceTo.Size = new System.Drawing.Size(62, 20);
            this.txtPriceTo.TabIndex = 1;
            this.txtPriceTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPriceTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_LimitToNumbers);
            // 
            // grpYear
            // 
            this.grpYear.Controls.Add(this.txtYearFrom);
            this.grpYear.Controls.Add(this.txtYearTo);
            this.grpYear.Controls.Add(this.Label1);
            this.grpYear.Location = new System.Drawing.Point(183, 27);
            this.grpYear.Name = "grpYear";
            this.grpYear.Size = new System.Drawing.Size(165, 55);
            this.grpYear.TabIndex = 2;
            this.grpYear.TabStop = false;
            this.grpYear.Text = "Year";
            // 
            // txtYearFrom
            // 
            this.txtYearFrom.Location = new System.Drawing.Point(6, 19);
            this.txtYearFrom.MaxLength = 4;
            this.txtYearFrom.Name = "txtYearFrom";
            this.txtYearFrom.Size = new System.Drawing.Size(39, 20);
            this.txtYearFrom.TabIndex = 0;
            this.txtYearFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtYearFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_LimitToNumbers);
            // 
            // txtYearTo
            // 
            this.txtYearTo.Location = new System.Drawing.Point(67, 19);
            this.txtYearTo.MaxLength = 4;
            this.txtYearTo.Name = "txtYearTo";
            this.txtYearTo.Size = new System.Drawing.Size(39, 20);
            this.txtYearTo.TabIndex = 1;
            this.txtYearTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtYearTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_LimitToNumbers);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(51, 23);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(10, 13);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "-";
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(72, 148);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(276, 20);
            this.txtKeyword.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Keyword:";
            // 
            // grpResults
            // 
            this.grpResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpResults.Controls.Add(this.dgvResults);
            this.grpResults.Location = new System.Drawing.Point(12, 174);
            this.grpResults.Name = "grpResults";
            this.grpResults.Size = new System.Drawing.Size(721, 401);
            this.grpResults.TabIndex = 15;
            this.grpResults.TabStop = false;
            this.grpResults.Text = "Results";
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToOrderColumns = true;
            this.dgvResults.AutoGenerateColumns = false;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Delete,
            this.Highlighted,
            this.Price,
            this.Year,
            this.Mileage,
            this.Listing_Link,
            this.VIN,
            this.Make,
            this.Model,
            this.City,
            this.Description});
            this.dgvResults.DataSource = this.carListingsBindingSource;
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.Location = new System.Drawing.Point(3, 16);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.Size = new System.Drawing.Size(715, 382);
            this.dgvResults.TabIndex = 0;
            this.dgvResults.Visible = false;
            this.dgvResults.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResults_CellContentClick);
            this.dgvResults.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvResults_CellFormatting);
            this.dgvResults.Sorted += new System.EventHandler(this.dgvResults_Sorted);
            // 
            // carListingsBindingSource
            // 
            this.carListingsBindingSource.DataMember = "Listings";
            this.carListingsBindingSource.DataSource = this.carListings;
            // 
            // carListings
            // 
            this.carListings.DataSetName = "CarListings";
            this.carListings.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Location = new System.Drawing.Point(477, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 142);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Statistics (Hover for Min / Max / Avg)";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowYears);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flowMakes);
            this.splitContainer1.Size = new System.Drawing.Size(250, 123);
            this.splitContainer1.SplitterDistance = 84;
            this.splitContainer1.TabIndex = 1;
            // 
            // flowYears
            // 
            this.flowYears.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowYears.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowYears.Location = new System.Drawing.Point(0, 0);
            this.flowYears.Name = "flowYears";
            this.flowYears.Size = new System.Drawing.Size(82, 121);
            this.flowYears.TabIndex = 0;
            // 
            // flowMakes
            // 
            this.flowMakes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowMakes.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowMakes.Location = new System.Drawing.Point(0, 0);
            this.flowMakes.Name = "flowMakes";
            this.flowMakes.Size = new System.Drawing.Size(160, 121);
            this.flowMakes.TabIndex = 0;
            // 
            // totalScrapeProgress
            // 
            this.totalScrapeProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalScrapeProgress.Location = new System.Drawing.Point(12, 581);
            this.totalScrapeProgress.Name = "totalScrapeProgress";
            this.totalScrapeProgress.Size = new System.Drawing.Size(721, 23);
            this.totalScrapeProgress.Step = 1;
            this.totalScrapeProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.totalScrapeProgress.TabIndex = 16;
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(745, 24);
            this.mainMenu.TabIndex = 18;
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(354, 145);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(117, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // minimumWageWorker
            // 
            this.minimumWageWorker.WorkerReportsProgress = true;
            this.minimumWageWorker.WorkerSupportsCancellation = true;
            this.minimumWageWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.minimumWageWorker_DoWork);
            this.minimumWageWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.minimumWageWorker_ProgressChanged);
            this.minimumWageWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.minimumWageWorker_RunWorkerCompleted);
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Delete";
            this.Delete.Name = "Delete";
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Delete.Text = "Delete";
            this.Delete.TrackVisitedState = false;
            this.Delete.UseColumnTextForLinkValue = true;
            // 
            // Highlighted
            // 
            this.Highlighted.HeaderText = "(X)";
            this.Highlighted.Name = "Highlighted";
            this.Highlighted.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Highlighted.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "Price";
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            // 
            // Year
            // 
            this.Year.DataPropertyName = "Year";
            this.Year.HeaderText = "Year";
            this.Year.Name = "Year";
            // 
            // Mileage
            // 
            this.Mileage.DataPropertyName = "Mileage";
            this.Mileage.HeaderText = "Mileage";
            this.Mileage.Name = "Mileage";
            // 
            // Listing_Link
            // 
            this.Listing_Link.DataPropertyName = "ListingID";
            this.Listing_Link.HeaderText = "Link";
            this.Listing_Link.Name = "Listing_Link";
            this.Listing_Link.Text = "Link";
            // 
            // VIN
            // 
            this.VIN.DataPropertyName = "VIN";
            this.VIN.HeaderText = "VIN";
            this.VIN.Name = "VIN";
            // 
            // Make
            // 
            this.Make.DataPropertyName = "Make";
            this.Make.HeaderText = "Make";
            this.Make.Name = "Make";
            // 
            // Model
            // 
            this.Model.DataPropertyName = "Model";
            this.Model.HeaderText = "Model";
            this.Model.Name = "Model";
            // 
            // City
            // 
            this.City.DataPropertyName = "City";
            this.City.HeaderText = "City";
            this.City.Name = "City";
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            // 
            // frmMain
            // 
            this.AcceptButton = this.btnScrape;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(745, 616);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpYear);
            this.Controls.Add(this.totalScrapeProgress);
            this.Controls.Add(this.grpResults);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.grpDistance);
            this.Controls.Add(this.btnScrape);
            this.Controls.Add(this.grpMileage);
            this.Controls.Add(this.grpPrice);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "frmMain";
            this.Text = "KSL Cars Search";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.grpDistance.ResumeLayout(false);
            this.grpDistance.PerformLayout();
            this.grpMileage.ResumeLayout(false);
            this.grpMileage.PerformLayout();
            this.grpPrice.ResumeLayout(false);
            this.grpPrice.PerformLayout();
            this.grpYear.ResumeLayout(false);
            this.grpYear.PerformLayout();
            this.grpResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.carListingsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.carListings)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDistance;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Button btnScrape;
        internal System.Windows.Forms.Label Label3;
        private System.Windows.Forms.GroupBox grpMileage;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.GroupBox grpPrice;
        internal System.Windows.Forms.GroupBox grpYear;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TextBox txtKeyword;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grpResults;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowYears;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowMakes;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ProgressBar totalScrapeProgress;
        private System.Windows.Forms.TextBox txtPriceTo;
        private System.Windows.Forms.TextBox txtMileageTo;
        private System.Windows.Forms.TextBox txtMileageFrom;
        private System.Windows.Forms.TextBox txtPriceFrom;
        private System.Windows.Forms.TextBox txtYearTo;
        private System.Windows.Forms.TextBox txtMiles;
        private System.Windows.Forms.TextBox txtZip;
        private System.Windows.Forms.TextBox txtYearFrom;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button btnCancel;
        private System.ComponentModel.BackgroundWorker minimumWageWorker;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.BindingSource carListingsBindingSource;
        private CarListings carListings;
        private System.Windows.Forms.DataGridViewLinkColumn Delete;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Highlighted;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Year;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mileage;
        private System.Windows.Forms.DataGridViewLinkColumn Listing_Link;
        private System.Windows.Forms.DataGridViewLinkColumn VIN;
        private System.Windows.Forms.DataGridViewTextBoxColumn Make;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn City;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}

