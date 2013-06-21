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
            this.btnScrape = new System.Windows.Forms.Button();
            this.grpResults = new System.Windows.Forms.GroupBox();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.carListingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.carListings = new KSL.Cars.App.CarListings();
            this.grpStats = new System.Windows.Forms.GroupBox();
            this.tabStats = new System.Windows.Forms.TabControl();
            this.tabYears = new System.Windows.Forms.TabPage();
            this.flowYears = new System.Windows.Forms.FlowLayoutPanel();
            this.tabMakes = new System.Windows.Forms.TabPage();
            this.flowMakes = new System.Windows.Forms.FlowLayoutPanel();
            this.tabModels = new System.Windows.Forms.TabPage();
            this.flowModels = new System.Windows.Forms.FlowLayoutPanel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.totalScrapeProgress = new System.Windows.Forms.ProgressBar();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emailResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancel = new System.Windows.Forms.Button();
            this.minimumWageWorker = new System.ComponentModel.BackgroundWorker();
            this.grpSearchParams = new System.Windows.Forms.GroupBox();
            this.clbBodyTypes = new System.Windows.Forms.CheckedListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numDistance = new System.Windows.Forms.NumericUpDown();
            this.numPriceHigh = new System.Windows.Forms.NumericUpDown();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.numYearHigh = new System.Windows.Forms.NumericUpDown();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.numPriceLow = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.numYearLow = new System.Windows.Forms.NumericUpDown();
            this.numMileageHigh = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numMileageLow = new System.Windows.Forms.NumericUpDown();
            this.Label3 = new System.Windows.Forms.Label();
            this.Delete = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mileage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Link = new System.Windows.Forms.DataGridViewLinkColumn();
            this.VIN = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Make = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.City = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.carListingsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.carListings)).BeginInit();
            this.grpStats.SuspendLayout();
            this.tabStats.SuspendLayout();
            this.tabYears.SuspendLayout();
            this.tabMakes.SuspendLayout();
            this.tabModels.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.grpSearchParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPriceHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYearHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPriceLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYearLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMileageHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMileageLow)).BeginInit();
            this.SuspendLayout();
            // 
            // btnScrape
            // 
            this.btnScrape.Location = new System.Drawing.Point(371, 32);
            this.btnScrape.Name = "btnScrape";
            this.btnScrape.Size = new System.Drawing.Size(100, 97);
            this.btnScrape.TabIndex = 1;
            this.btnScrape.Text = "Search";
            this.btnScrape.UseVisualStyleBackColor = true;
            this.btnScrape.Click += new System.EventHandler(this.btnScrape_Click);
            // 
            // grpResults
            // 
            this.grpResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpResults.Controls.Add(this.dgvResults);
            this.grpResults.Location = new System.Drawing.Point(12, 164);
            this.grpResults.Name = "grpResults";
            this.grpResults.Size = new System.Drawing.Size(721, 411);
            this.grpResults.TabIndex = 15;
            this.grpResults.TabStop = false;
            this.grpResults.Text = "Results";
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToOrderColumns = true;
            this.dgvResults.AutoGenerateColumns = false;
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Delete,
            this.Price,
            this.Year,
            this.Mileage,
            this.Link,
            this.VIN,
            this.Make,
            this.Model,
            this.City,
            this.Description});
            this.dgvResults.DataSource = this.carListingsBindingSource;
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.Location = new System.Drawing.Point(3, 16);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.Size = new System.Drawing.Size(715, 392);
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
            // grpStats
            // 
            this.grpStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStats.Controls.Add(this.tabStats);
            this.grpStats.Location = new System.Drawing.Point(477, 26);
            this.grpStats.Name = "grpStats";
            this.grpStats.Size = new System.Drawing.Size(256, 132);
            this.grpStats.TabIndex = 17;
            this.grpStats.TabStop = false;
            this.grpStats.Text = "Statistics (Hover over Links for Min / Max / Avg)";
            // 
            // tabStats
            // 
            this.tabStats.Controls.Add(this.tabYears);
            this.tabStats.Controls.Add(this.tabMakes);
            this.tabStats.Controls.Add(this.tabModels);
            this.tabStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabStats.Location = new System.Drawing.Point(3, 16);
            this.tabStats.Name = "tabStats";
            this.tabStats.SelectedIndex = 0;
            this.tabStats.Size = new System.Drawing.Size(250, 113);
            this.tabStats.TabIndex = 2;
            // 
            // tabYears
            // 
            this.tabYears.Controls.Add(this.flowYears);
            this.tabYears.Location = new System.Drawing.Point(4, 22);
            this.tabYears.Name = "tabYears";
            this.tabYears.Padding = new System.Windows.Forms.Padding(3);
            this.tabYears.Size = new System.Drawing.Size(242, 87);
            this.tabYears.TabIndex = 0;
            this.tabYears.Text = "Years";
            this.tabYears.UseVisualStyleBackColor = true;
            // 
            // flowYears
            // 
            this.flowYears.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowYears.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowYears.Location = new System.Drawing.Point(3, 3);
            this.flowYears.Name = "flowYears";
            this.flowYears.Size = new System.Drawing.Size(236, 81);
            this.flowYears.TabIndex = 1;
            // 
            // tabMakes
            // 
            this.tabMakes.Controls.Add(this.flowMakes);
            this.tabMakes.Location = new System.Drawing.Point(4, 22);
            this.tabMakes.Name = "tabMakes";
            this.tabMakes.Padding = new System.Windows.Forms.Padding(3);
            this.tabMakes.Size = new System.Drawing.Size(242, 87);
            this.tabMakes.TabIndex = 1;
            this.tabMakes.Text = "Makes";
            this.tabMakes.UseVisualStyleBackColor = true;
            // 
            // flowMakes
            // 
            this.flowMakes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowMakes.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowMakes.Location = new System.Drawing.Point(3, 3);
            this.flowMakes.Name = "flowMakes";
            this.flowMakes.Size = new System.Drawing.Size(236, 81);
            this.flowMakes.TabIndex = 1;
            // 
            // tabModels
            // 
            this.tabModels.Controls.Add(this.flowModels);
            this.tabModels.Location = new System.Drawing.Point(4, 22);
            this.tabModels.Name = "tabModels";
            this.tabModels.Padding = new System.Windows.Forms.Padding(3);
            this.tabModels.Size = new System.Drawing.Size(242, 87);
            this.tabModels.TabIndex = 2;
            this.tabModels.Text = "Models";
            this.tabModels.UseVisualStyleBackColor = true;
            // 
            // flowModels
            // 
            this.flowModels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowModels.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowModels.Location = new System.Drawing.Point(3, 3);
            this.flowModels.Name = "flowModels";
            this.flowModels.Size = new System.Drawing.Size(236, 81);
            this.flowModels.TabIndex = 0;
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
            this.aboutToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.emailResultsToolStripMenuItem});
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
            this.aboutToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.updateToolStripMenuItem.Text = "&Update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // emailResultsToolStripMenuItem
            // 
            this.emailResultsToolStripMenuItem.Name = "emailResultsToolStripMenuItem";
            this.emailResultsToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.emailResultsToolStripMenuItem.Text = "Email Results!";
            this.emailResultsToolStripMenuItem.Click += new System.EventHandler(this.emailResultsToolStripMenuItem_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(371, 135);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 2;
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
            // grpSearchParams
            // 
            this.grpSearchParams.Controls.Add(this.clbBodyTypes);
            this.grpSearchParams.Controls.Add(this.label9);
            this.grpSearchParams.Controls.Add(this.label8);
            this.grpSearchParams.Controls.Add(this.label7);
            this.grpSearchParams.Controls.Add(this.label6);
            this.grpSearchParams.Controls.Add(this.numDistance);
            this.grpSearchParams.Controls.Add(this.numPriceHigh);
            this.grpSearchParams.Controls.Add(this.txtZip);
            this.grpSearchParams.Controls.Add(this.Label4);
            this.grpSearchParams.Controls.Add(this.numYearHigh);
            this.grpSearchParams.Controls.Add(this.txtKeyword);
            this.grpSearchParams.Controls.Add(this.numPriceLow);
            this.grpSearchParams.Controls.Add(this.label5);
            this.grpSearchParams.Controls.Add(this.Label2);
            this.grpSearchParams.Controls.Add(this.numYearLow);
            this.grpSearchParams.Controls.Add(this.numMileageHigh);
            this.grpSearchParams.Controls.Add(this.label1);
            this.grpSearchParams.Controls.Add(this.numMileageLow);
            this.grpSearchParams.Controls.Add(this.Label3);
            this.grpSearchParams.Location = new System.Drawing.Point(12, 27);
            this.grpSearchParams.Name = "grpSearchParams";
            this.grpSearchParams.Size = new System.Drawing.Size(353, 131);
            this.grpSearchParams.TabIndex = 0;
            this.grpSearchParams.TabStop = false;
            this.grpSearchParams.Text = "Search Criteria";
            // 
            // clbBodyTypes
            // 
            this.clbBodyTypes.CheckOnClick = true;
            this.clbBodyTypes.Items.AddRange(new object[] {
            "Compact",
            "Compact Car",
            "Convertible",
            "Coupe",
            "Crossover",
            "Hatchback",
            "Industrial / Semi",
            "Limited",
            "Minivan",
            "Sedan",
            "Sport Utility",
            "Truck",
            "Van",
            "Wagon"});
            this.clbBodyTypes.Location = new System.Drawing.Point(202, 17);
            this.clbBodyTypes.Name = "clbBodyTypes";
            this.clbBodyTypes.Size = new System.Drawing.Size(139, 109);
            this.clbBodyTypes.Sorted = true;
            this.clbBodyTypes.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 41;
            this.label9.Text = "Distance:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 40;
            this.label8.Text = "Price:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "Year:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 38;
            this.label6.Text = "Mileage:";
            // 
            // numDistance
            // 
            this.numDistance.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numDistance.Location = new System.Drawing.Point(62, 82);
            this.numDistance.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numDistance.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDistance.Name = "numDistance";
            this.numDistance.Size = new System.Drawing.Size(44, 20);
            this.numDistance.TabIndex = 6;
            this.numDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numDistance.ThousandsSeparator = true;
            this.numDistance.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numPriceHigh
            // 
            this.numPriceHigh.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numPriceHigh.Location = new System.Drawing.Point(134, 61);
            this.numPriceHigh.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numPriceHigh.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPriceHigh.Name = "numPriceHigh";
            this.numPriceHigh.Size = new System.Drawing.Size(62, 20);
            this.numPriceHigh.TabIndex = 5;
            this.numPriceHigh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numPriceHigh.ThousandsSeparator = true;
            this.numPriceHigh.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtZip
            // 
            this.txtZip.Location = new System.Drawing.Point(149, 82);
            this.txtZip.MaxLength = 5;
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(47, 20);
            this.txtZip.TabIndex = 7;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(108, 86);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(43, 13);
            this.Label4.TabIndex = 36;
            this.Label4.Text = "from zip";
            // 
            // numYearHigh
            // 
            this.numYearHigh.Location = new System.Drawing.Point(134, 40);
            this.numYearHigh.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numYearHigh.Minimum = new decimal(new int[] {
            1950,
            0,
            0,
            0});
            this.numYearHigh.Name = "numYearHigh";
            this.numYearHigh.Size = new System.Drawing.Size(62, 20);
            this.numYearHigh.TabIndex = 3;
            this.numYearHigh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numYearHigh.Value = new decimal(new int[] {
            1950,
            0,
            0,
            0});
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(62, 103);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(134, 20);
            this.txtKeyword.TabIndex = 8;
            // 
            // numPriceLow
            // 
            this.numPriceLow.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numPriceLow.Location = new System.Drawing.Point(62, 61);
            this.numPriceLow.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numPriceLow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPriceLow.Name = "numPriceLow";
            this.numPriceLow.Size = new System.Drawing.Size(62, 20);
            this.numPriceLow.TabIndex = 4;
            this.numPriceLow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numPriceLow.ThousandsSeparator = true;
            this.numPriceLow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Keyword:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(124, 65);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(10, 13);
            this.Label2.TabIndex = 30;
            this.Label2.Text = "-";
            // 
            // numYearLow
            // 
            this.numYearLow.Location = new System.Drawing.Point(62, 40);
            this.numYearLow.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numYearLow.Minimum = new decimal(new int[] {
            1950,
            0,
            0,
            0});
            this.numYearLow.Name = "numYearLow";
            this.numYearLow.Size = new System.Drawing.Size(62, 20);
            this.numYearLow.TabIndex = 2;
            this.numYearLow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numYearLow.Value = new decimal(new int[] {
            1950,
            0,
            0,
            0});
            // 
            // numMileageHigh
            // 
            this.numMileageHigh.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMileageHigh.Location = new System.Drawing.Point(134, 19);
            this.numMileageHigh.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numMileageHigh.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMileageHigh.Name = "numMileageHigh";
            this.numMileageHigh.Size = new System.Drawing.Size(62, 20);
            this.numMileageHigh.TabIndex = 1;
            this.numMileageHigh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numMileageHigh.ThousandsSeparator = true;
            this.numMileageHigh.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(124, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "-";
            // 
            // numMileageLow
            // 
            this.numMileageLow.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMileageLow.Location = new System.Drawing.Point(62, 19);
            this.numMileageLow.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numMileageLow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMileageLow.Name = "numMileageLow";
            this.numMileageLow.Size = new System.Drawing.Size(62, 20);
            this.numMileageLow.TabIndex = 0;
            this.numMileageLow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numMileageLow.ThousandsSeparator = true;
            this.numMileageLow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(124, 23);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(10, 13);
            this.Label3.TabIndex = 32;
            this.Label3.Text = "-";
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Delete";
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Delete.Text = "Delete";
            this.Delete.TrackVisitedState = false;
            this.Delete.UseColumnTextForLinkValue = true;
            this.Delete.Width = 44;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "Price";
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            this.Price.Width = 56;
            // 
            // Year
            // 
            this.Year.DataPropertyName = "Year";
            this.Year.HeaderText = "Year";
            this.Year.Name = "Year";
            this.Year.ReadOnly = true;
            this.Year.Width = 54;
            // 
            // Mileage
            // 
            this.Mileage.DataPropertyName = "Mileage";
            this.Mileage.HeaderText = "Mileage";
            this.Mileage.Name = "Mileage";
            this.Mileage.ReadOnly = true;
            this.Mileage.Width = 69;
            // 
            // Link
            // 
            this.Link.DataPropertyName = "ListingID";
            this.Link.HeaderText = "Link";
            this.Link.Name = "Link";
            this.Link.ReadOnly = true;
            this.Link.Text = "Link";
            this.Link.Width = 33;
            // 
            // VIN
            // 
            this.VIN.DataPropertyName = "VIN";
            this.VIN.HeaderText = "VIN";
            this.VIN.Name = "VIN";
            this.VIN.ReadOnly = true;
            this.VIN.Width = 31;
            // 
            // Make
            // 
            this.Make.DataPropertyName = "Make";
            this.Make.HeaderText = "Make";
            this.Make.Name = "Make";
            this.Make.ReadOnly = true;
            this.Make.Width = 59;
            // 
            // Model
            // 
            this.Model.DataPropertyName = "Model";
            this.Model.HeaderText = "Model";
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            this.Model.Width = 61;
            // 
            // City
            // 
            this.City.DataPropertyName = "City";
            this.City.HeaderText = "City";
            this.City.Name = "City";
            this.City.ReadOnly = true;
            this.City.Width = 49;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 85;
            // 
            // frmMain
            // 
            this.AcceptButton = this.btnScrape;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(745, 616);
            this.Controls.Add(this.grpSearchParams);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpStats);
            this.Controls.Add(this.totalScrapeProgress);
            this.Controls.Add(this.grpResults);
            this.Controls.Add(this.btnScrape);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "frmMain";
            this.Text = "KSL Cars Search";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.grpResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.carListingsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.carListings)).EndInit();
            this.grpStats.ResumeLayout(false);
            this.tabStats.ResumeLayout(false);
            this.tabYears.ResumeLayout(false);
            this.tabMakes.ResumeLayout(false);
            this.tabModels.ResumeLayout(false);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.grpSearchParams.ResumeLayout(false);
            this.grpSearchParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPriceHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYearHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPriceLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYearLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMileageHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMileageLow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnScrape;
        private System.Windows.Forms.GroupBox grpResults;
        private System.Windows.Forms.GroupBox grpStats;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ProgressBar totalScrapeProgress;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.BindingSource carListingsBindingSource;
        private CarListings carListings;
        protected internal System.ComponentModel.BackgroundWorker minimumWageWorker;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emailResultsToolStripMenuItem;
        private System.Windows.Forms.TabControl tabStats;
        private System.Windows.Forms.TabPage tabYears;
        private System.Windows.Forms.FlowLayoutPanel flowYears;
        private System.Windows.Forms.TabPage tabMakes;
        private System.Windows.Forms.FlowLayoutPanel flowMakes;
        private System.Windows.Forms.TabPage tabModels;
        private System.Windows.Forms.FlowLayoutPanel flowModels;
        private System.Windows.Forms.GroupBox grpSearchParams;
        private System.Windows.Forms.CheckedListBox clbBodyTypes;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numDistance;
        private System.Windows.Forms.NumericUpDown numPriceHigh;
        private System.Windows.Forms.TextBox txtZip;
        internal System.Windows.Forms.Label Label4;
        private System.Windows.Forms.NumericUpDown numYearHigh;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.NumericUpDown numPriceLow;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label Label2;
        private System.Windows.Forms.NumericUpDown numYearLow;
        private System.Windows.Forms.NumericUpDown numMileageHigh;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numMileageLow;
        internal System.Windows.Forms.Label Label3;
        private System.Windows.Forms.DataGridViewLinkColumn Delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Year;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mileage;
        private System.Windows.Forms.DataGridViewLinkColumn Link;
        private System.Windows.Forms.DataGridViewLinkColumn VIN;
        private System.Windows.Forms.DataGridViewTextBoxColumn Make;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn City;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}

