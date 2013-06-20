using HtmlAgilityPack;
using System;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Net;

namespace KSL.Cars.App
{
    public partial class frmMain : Form
    {

        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This starts the whole process off. Clears any existing data, builds a URL from the search parameters, then passes it to the recursive parse function in a worker process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScrape_Click(object sender, EventArgs e)
        {
            if (!minimumWageWorker.IsBusy)
            {
                try
                {
                    flowYears.Controls.Clear();
                    flowMakes.Controls.Clear();
                    flowModels.Controls.Clear();

                    carListings.ContactInfo.Clear();
                    carListings.Listings.Clear();

                    carListings.StatsByYear.Clear();
                    carListings.StatsByMake.Clear();
                    carListings.StatsByModel.Clear();

                    SaveSearch();

                    DisableWhileSearching();

                    string url = buildURL();

                    minimumWageWorker.RunWorkerAsync(url);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    EventLogger.LogEvent(ex);
                }
            }
        }

        /// <summary>
        /// Performs different actions based on which cell was clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                switch (dgvResults.Columns[e.ColumnIndex].Name)
                {
                    //Use IE to open the link stored in the tooltip.
                    //TODO: Open link in default browser.
                    case "Link":
                    case "VIN":
                        System.Diagnostics.Process.Start("iexplore.exe", this.dgvResults.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText);
                        break;

                    //Delete the row
                    case "Delete":
                        dgvResults.Rows.Remove(dgvResults.Rows[e.RowIndex]);
                        break;
                }
            }
        }

        /// <summary>
        /// Loads the data saved from the last session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            if (LoadData(false))
            {
                dgvResults.Visible = true;
            }
        }

        /// <summary>
        /// Saves the different bits of data if set in the options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData(false);
        }

        /// <summary>
        /// Limits keypresses in certain textboxes to only numbers and basic text editing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDown_LimitToNumbers(object sender, KeyEventArgs e)
        {
            Keys[] acceptable = { Keys.D0, 
                                    Keys.D1, 
                                    Keys.D2,
                                    Keys.D3,
                                    Keys.D4,
                                    Keys.D5,
                                    Keys.D6, 
                                    Keys.D7, 
                                    Keys.D8, 
                                    Keys.D9,
                                    Keys.NumPad0, 
                                    Keys.NumPad1,
                                    Keys.NumPad2,
                                    Keys.NumPad3,
                                    Keys.NumPad4,
                                    Keys.NumPad5,
                                    Keys.NumPad6, 
                                    Keys.NumPad7, 
                                    Keys.NumPad8, 
                                    Keys.NumPad9, 
                                    Keys.Delete, 
                                    Keys.Back, 
                                    Keys.Return, 
                                    Keys.Right, 
                                    Keys.Left, 
                                    Keys.Home, 
                                    Keys.End};
            if (!acceptable.Contains<Keys>(e.KeyCode)) e.SuppressKeyPress = true;
        }

        /// <summary>
        /// Adds tooltips to the Link and VIn columns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewCell cell = dgvResults[e.ColumnIndex, e.RowIndex];

            switch (dgvResults.Columns[e.ColumnIndex].Name)
            {
                case "VIN":
                    //Add url tooltip
                    cell.ToolTipText = Properties.Settings.Default.VIN_LINK.Replace("{VIN}", cell.Value.ToString());
                    break;
                case "Link":
                    //Add url tooltip
                    cell.ToolTipText = Properties.Settings.Default.LISTING_LINK.Replace("{LISTING_ID}", cell.Value.ToString());
                    break;
                case "City":
                    //Add tooltip for contact information
                    CarListings.ContactInfoRow row = carListings.ContactInfo.FindByListingID(int.Parse(cell.OwningRow.Cells[Link.Name].Value.ToString()));

                    //Default value is a single space, check for a longer value.
                    cell.ToolTipText = (row.Name.Length > 1 ? row.Name + ": " : "") + (row.Phone.Length > 1 ? row.Phone : "");
                    break;
            }
        }

        /// <summary>
        /// Event handler for the sorted method to keep the rows highlighted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvResults_Sorted(object sender, EventArgs e)
        {
            highlight();
        }

        /// <summary>
        /// Shows the About dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout aboot = new frmAbout();
            aboot.ShowDialog();
        }

        /// <summary>
        /// Loads the Settings window and passes it the current settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSettings settingsFrm = new frmSettings((CarListings.SettingsRow)(carListings.Settings.Rows[0]));

            if (settingsFrm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (carListings.Settings.Rows.Count == 0) carListings.Settings.Rows.Add(carListings.Settings.NewSettingsRow());

                CarListings.SettingsRow settings = carListings.Settings.First();

                settings.SaveSearchResults = settingsFrm.chkSaveLastListings.Checked;
                settings.LoadLastSearchParams = settingsFrm.chkKeepSearchParameters.Checked;


                settings.SMTPHost = settingsFrm.txtSMTPHost.Text;
                settings.PortNumber = int.Parse(settingsFrm.txtPort.Text);
                settings.Username = settingsFrm.txtUsername.Text;

                string tempPass = "";
                if (settingsFrm.txtPassword.TextLength > 0) tempPass = Encryption.Encrypt(settingsFrm.txtPassword.Text);
                settings.Password = tempPass;

                settings.FromAddress = settingsFrm.txtFrom.Text;
                settings.ToAddress = settingsFrm.txtTo.Text;
                settings.UseSSL = settingsFrm.chkUseSSL.Checked;

            }
        }

        /// <summary>
        /// Worker process to start the html parse function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minimumWageWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string url = e.Argument.ToString();
            minimumWageWorker.ReportProgress(0);
            try
            {
                parsePage(url, e, 0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                EventLogger.LogEvent(ex);

                minimumWageWorker.CancelAsync();
            }
        }

        /// <summary>
        /// Updates the progress bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minimumWageWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            totalScrapeProgress.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Makes sure that the progress bar shows completed, re-enables buttons, and computes the stats.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minimumWageWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!(e.Error == null))
            {
                MessageBox.Show("Error: " + e.Error.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                totalScrapeProgress.Value = 100;

                dgvResults.TopLeftHeaderCell.ToolTipText = "Total Listings Found: " + carListings.Listings.Rows.Count;

                EnableAfterSearching();


                computeStats();

                AddGraphLinks();

                dgvResults.Sort(Price, ListSortDirection.Ascending);
            }
        }

        /// <summary>
        /// Opens a graph window based on the stats linked clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openGraph(object sender, EventArgs e)
        {
            DataTable dataForChart = new DataTable();
            dataForChart = carListings.Listings.Clone();
            dataForChart.Columns.Remove("Description");

            switch (((Control)sender).Tag.ToString())
            {

                case "Year":
                    dataForChart.Columns[0].Caption = "Year " + ((LinkLabel)sender).Text.ToString();
                    var yearQuery = from row in carListings.Listings.AsEnumerable()
                                    where (row.Field<Int32>("Year") == Int32.Parse(((LinkLabel)sender).Text.Split(' ')[0].ToString()))
                                    select row;
                    foreach (DataRow dr in yearQuery) dataForChart.ImportRow(dr);
                    break;
                case "Make":
                    dataForChart.Columns[0].Caption = ((LinkLabel)sender).Text.ToString();
                    var makeQuery = from row in carListings.Listings.AsEnumerable()
                                    where (row.Field<string>("Make") == ((LinkLabel)sender).Text.Split(' ')[0].ToString())
                                    select row;
                    foreach (DataRow dr in makeQuery) dataForChart.ImportRow(dr);
                    break;
            }

            frmGraph myChartForm = new frmGraph(dataForChart);
            myChartForm.Show();
        }

        /// <summary>
        /// Downloads the update control files and populates the update window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlDocument updateFile = Updater.getUpdateFile();

            if (updateFile != null)
            {
                Dictionary<Label, Control> items = new Dictionary<Label, Control>();

                foreach (XmlNode node in updateFile.SelectSingleNode("/KSL.Cars.App").ChildNodes)
                {
                    if (node.Attributes != null && node.Attributes.Count == 2)
                    {
                        Label label = new Label();
                        label.Text = node.Attributes["desc"].InnerText;

                        Control details;

                        switch (node.Attributes["type"].InnerText)
                        {
                            case "text":
                                details = new Label();
                                details.Text = node.InnerText;
                                items.Add(label, details);
                                break;
                            case "link":
                                details = new LinkLabel();
                                details.Text = "Link";

                                LinkLabel.Link link = new LinkLabel.Link();
                                link.LinkData = node.InnerText;

                                ((LinkLabel)details).Links.Add(link);
                                items.Add(label, details);
                                break;
                        }
                    }
                }

                frmUpdate form = new frmUpdate(items);
                form.ShowDialog();
            }
        }

        /// <summary>
        /// Cancels the search process. This button should only be active while a search is running.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (minimumWageWorker.WorkerSupportsCancellation == true)
            {
                minimumWageWorker.CancelAsync();
            }
        }

        /// <summary>
        /// Sample email results.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void emailResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HaveWeGotCompleteEmailSettings())
            {
                try
                {
                    emailResults();
                }
                catch (Exception ex)
                {
                    EventLogger.LogEvent(ex.Message);
                }
            }
            else MessageBox.Show("Email settings seem to be incomplete, please check them.");
        }


        /// <summary>
        /// Calculates the basic stats for year, make and models found in the dataset.
        /// </summary>
        private void computeStats()
        {
            //Get list of unique Years, Makes, and //Models
            List<string> Years = new List<string>();
            List<string> Makes = new List<string>();
            List<string> Models = new List<string>();

            TextInfo titleCase = new CultureInfo("en-US", false).TextInfo;

            foreach (DataRow row in carListings.Listings.Rows)
            {
                string tempYear = titleCase.ToTitleCase(row["Year"].ToString().ToLower().Trim());
                if (!Years.Contains(tempYear)) Years.Add(tempYear);

                string tempMake = titleCase.ToTitleCase(row["Make"].ToString().ToLower().Trim());
                if (!Makes.Contains(tempMake)) Makes.Add(tempMake);

                string tempModel = titleCase.ToTitleCase(row["Model"].ToString().ToLower().Trim());
                if (!Models.Contains(tempModel)) Models.Add(tempModel);
            }
            Years.Sort();
            Makes.Sort();
            Models.Sort();

            foreach (string Year in Years)
            {
                string filter = "Year=" + Year;

                CarListings.StatsByYearRow newRow = carListings.StatsByYear.NewStatsByYearRow();

                newRow["Year"] = int.Parse(Year);
                newRow["Min"] = carListings.Listings.Compute("Min(Price)", filter);
                newRow["Max"] = carListings.Listings.Compute("Max(Price)", filter);

                double avg;
                if (double.TryParse(carListings.Listings.Compute("Avg(Price)", filter).ToString(), out avg)) newRow["Avg"] = avg;
                else newRow["Avg"] = 0;

                double stDev;
                if (double.TryParse(carListings.Listings.Compute("StDev(Price)", filter).ToString(), out stDev)) newRow["StDev"] = stDev;
                else newRow["StDev"] = 0;

                newRow["Count"] = carListings.Listings.Compute("Count(Price)", filter);
                newRow["Sum"] = carListings.Listings.Compute("Sum(Price)", filter);

                double var;
                if (double.TryParse(carListings.Listings.Compute("Var(Price)", filter).ToString(), out var)) newRow["Var"] = stDev;
                else newRow["Var"] = 0;

                carListings.StatsByYear.AddStatsByYearRow(newRow);
            }

            foreach (string Make in Makes)
            {
                string filter = "Make='" + Make + "'";

                CarListings.StatsByMakeRow newRow = carListings.StatsByMake.NewStatsByMakeRow();

                newRow["Make"] = Make;
                newRow["Min"] = carListings.Listings.Compute("Min(Price)", filter);
                newRow["Max"] = carListings.Listings.Compute("Max(Price)", filter);

                double avg;
                if (double.TryParse(carListings.Listings.Compute("Avg(Price)", filter).ToString(), out avg)) newRow["Avg"] = avg;
                else newRow["Avg"] = 0;

                double stDev;
                if (double.TryParse(carListings.Listings.Compute("StDev(Price)", filter).ToString(), out stDev)) newRow["StDev"] = stDev;
                else newRow["StDev"] = 0;

                newRow["Count"] = carListings.Listings.Compute("Count(Price)", filter);
                newRow["Sum"] = carListings.Listings.Compute("Sum(Price)", filter);

                double var;
                if (double.TryParse(carListings.Listings.Compute("Var(Price)", filter).ToString(), out var)) newRow["Var"] = stDev;
                else newRow["Var"] = 0;

                carListings.StatsByMake.AddStatsByMakeRow(newRow);
            }

            foreach (string Model in Models)
            {
                string filter = "Model='" + Model + "'";

                CarListings.StatsByModelRow newRow = carListings.StatsByModel.NewStatsByModelRow();

                newRow["Model"] = Model;
                newRow["Min"] = carListings.Listings.Compute("Min(Price)", filter);
                newRow["Max"] = carListings.Listings.Compute("Max(Price)", filter);

                double avg;
                if (double.TryParse(carListings.Listings.Compute("Avg(Price)", filter).ToString(), out avg)) newRow["Avg"] = avg;
                else newRow["Avg"] = 0;

                double stDev;
                if (double.TryParse(carListings.Listings.Compute("StDev(Price)", filter).ToString(), out stDev)) newRow["StDev"] = stDev;
                else newRow["StDev"] = 0;

                newRow["Count"] = carListings.Listings.Compute("Count(Price)", filter);
                newRow["Sum"] = carListings.Listings.Compute("Sum(Price)", filter);

                double var;
                if (double.TryParse(carListings.Listings.Compute("Var(Price)", filter).ToString(), out var)) newRow["Var"] = stDev;
                else newRow["Var"] = 0;

                carListings.StatsByModel.AddStatsByModelRow(newRow);
            }
        }

        /// <summary>
        /// Adds links to the stats area to open graphs
        /// </summary>
        private void AddGraphLinks()
        {
            LinkLabel temp;

            foreach (CarListings.StatsByYearRow row in carListings.StatsByYear.Rows)
            {
                temp = new LinkLabel();
                temp.AutoSize = true;
                temp.Click += new System.EventHandler(this.openGraph);
                temp.Tag = "Year";
                temp.Text = row.Year + " (" + row.Count + ")";
                toolTip.SetToolTip(temp,
                    String.Format("{0:C}", row.Min) + "/"
                    + String.Format("{0:C}", row.Max) + "/"
                    + String.Format("{0:C}", row.Avg));
                flowYears.Controls.Add(temp);
            }

            foreach (CarListings.StatsByMakeRow row in carListings.StatsByMake.Rows)
            {
                temp = new LinkLabel();
                temp.AutoSize = true;
                temp.Click += new System.EventHandler(this.openGraph);
                temp.Tag = "Make";
                temp.Text = row.Make + " (" + row.Count + ")";
                toolTip.SetToolTip(temp,
                    String.Format("{0:C}", row.Min) + "/"
                    + String.Format("{0:C}", row.Max) + "/"
                    + String.Format("{0:C}", row.Avg));
                flowMakes.Controls.Add(temp);
            }

            foreach (CarListings.StatsByModelRow row in carListings.StatsByModel.Rows)
            {
                temp = new LinkLabel();
                temp.AutoSize = true;
                temp.Click += new System.EventHandler(this.openGraph);
                temp.Tag = "Model";
                temp.Text = row.Model + " (" + row.Count + ")";
                toolTip.SetToolTip(temp,
                    String.Format("{0:C}", row.Min) + "/"
                    + String.Format("{0:C}", row.Max) + "/"
                    + String.Format("{0:C}", row.Avg));
                flowModels.Controls.Add(temp);
            }
        }

        /// <summary>
        /// Highlights certain rows based on price and the presence of contact information. Also adds tooltips to rows.
        /// </summary>
        private void highlight()
        {
            foreach (DataGridViewRow row in dgvResults.Rows)
            {
                CarListings.StatsByYearRow yearStats = carListings.StatsByYear.FindByYear(int.Parse(row.Cells["Year"].Value.ToString()));
                CarListings.StatsByMakeRow makeStats = carListings.StatsByMake.FindByMake(row.Cells["Make"].Value.ToString());
                CarListings.StatsByModelRow modelStats = carListings.StatsByModel.FindByModel(row.Cells["Model"].Value.ToString());

                CarListings.ListingsRow matchingListing = carListings.Listings.FindByListingID(int.Parse(row.Cells["Link"].Value.ToString()));
                CarListings.ContactInfoRow contactInfo = carListings.ContactInfo.FindByListingID(int.Parse(row.Cells["Link"].Value.ToString()));

                if (yearStats != null && makeStats != null && matchingListing != null)
                {
                    bool highlight = false;

                    string matchingColumn = "";

                    if ((matchingListing.Price < (yearStats.Avg - yearStats.StDev)))
                    {
                        matchingColumn = "Year";
                        highlight = true;

                    }
                    else if ((matchingListing.Price < (modelStats.Avg - modelStats.StDev)))
                    {
                        matchingColumn = "Model";
                        highlight = true;
                    }
                    else if ((matchingListing.Price < (makeStats.Avg - makeStats.StDev)))
                    {
                        matchingColumn = "Make";
                        highlight = true;
                    }

                    //default value for phone number is a single space, 
                    //check for that or smaller for phone number.
                    if (contactInfo.Phone.Length <= 1)
                    {
                        row.Cells["Highlighted"].ToolTipText = "This car does not have a phone number listed (properly). Beware a scam...";
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        if (highlight)
                        {
                            row.Cells["Highlighted"].ToolTipText = "Price is significantly lower than average for a " + row.Cells[matchingColumn].Value.ToString();
                            row.Cells["Highlighted"].Value = true;
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Workhorse that gets the HTML and parses it into the datatable.
        /// </summary>
        public void parsePage(string url, DoWorkEventArgs e = null, int currentPageNum = 0, int lastPageNum = 0)
        {
            //Check for and cancel if needed.
            if (e != null && minimumWageWorker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

            // There are various options, set as needed
            htmlDoc.OptionFixNestedTags = true;

            string htmlCode;
            try
            {
                htmlCode = (new WebClient()).DownloadString(url);
            }
            catch (Exception ex)
            {
                //Throw exception for recursion to handle.
                throw ex;
            }

            // Used to load from a string (was htmlDoc.LoadXML(xmlString)
            htmlDoc.LoadHtml(htmlCode);

            if (htmlDoc.DocumentNode != null)
            {
                HtmlAgilityPack.HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

                if (bodyNode != null)
                {
                    // Do something with bodyNode
                    HtmlNodeCollection results = bodyNode.SelectNodes(Properties.Settings.Default.LISTING_SEARCH_RESULTS);

                    if (lastPageNum == 0)
                    {
                        HtmlNodeCollection lastPage = bodyNode.SelectNodes(Properties.Settings.Default.LAST_PAGE_NODE);

                        if (lastPage != null && lastPage.Count > 0)
                        {
                            //<a href="/auto/search/index?page=15" title="Go to last page">16</a>
                            List<int> maxPages = new List<int>();

                            foreach (HtmlNode node in lastPage)
                            {
                                try
                                {
                                    maxPages.Add(int.Parse(node.InnerText));
                                }
                                catch {/*It's not a number, we don't want it.*/}
                            }

                            lastPageNum = maxPages.Max();
                        }
                        else { /*lastPage is null or empty, there's only one page.*/}
                    }


                    if (results != null && results.Count > 0)
                    {

                        foreach (HtmlNode node in results)
                        {
                            //Check for cancel again, provides better cancel if there's only one page of results.
                            if ((minimumWageWorker.CancellationPending == true))
                            {
                                e.Cancel = true;
                                return;
                            }
                            CarListings.ListingsRow newRow = carListings.Listings.NewListingsRow();
                            CarListings.ContactInfoRow contactRow = carListings.ContactInfo.NewContactInfoRow();

                            newRow.Price = Double.Parse(node.SelectSingleNode(Properties.Settings.Default.LISTING_RESULT_PRICE).InnerText.Replace("$", "").Replace(",", ""));

                            newRow.ListingID = int.Parse(node.SelectSingleNode(Properties.Settings.Default.LISTING_RESULT_ID).Attributes["href"].Value.Split('/').Last<string>().Split('?').First<string>());

                            //We've got all the info from the listing page, new we go into the page itself for info like VIN and Mileage.
                            HtmlAgilityPack.HtmlDocument singleListing = new HtmlAgilityPack.HtmlDocument();

                            // There are various options, set as needed
                            singleListing.OptionFixNestedTags = true;

                            string singleListingCode = (new WebClient()).DownloadString(Properties.Settings.Default.LISTING_LINK.Replace("{LISTING_ID}", newRow.ListingID.ToString()));

                            singleListing.LoadHtml(singleListingCode);

                            if (singleListing.DocumentNode != null)
                            {
                                HtmlAgilityPack.HtmlNode listingBodyNode = singleListing.DocumentNode.SelectSingleNode("//body");

                                if (listingBodyNode != null)
                                {
                                    // Do something with bodyNode
                                    HtmlNode specTable = listingBodyNode.SelectSingleNode(Properties.Settings.Default.LISTING_VIN);
                                    if (specTable != null) newRow.VIN = specTable.NextSibling.NextSibling.InnerText;

                                    specTable = listingBodyNode.SelectSingleNode(Properties.Settings.Default.LISTING_DESCRIPTION);
                                    try { newRow.Description = specTable.InnerText.Trim(); }
                                    catch { newRow.Description = ""; }//No Description for listing                                

                                    specTable = listingBodyNode.SelectSingleNode(Properties.Settings.Default.LISTING_MILEAGE);
                                    if (specTable != null) newRow.Mileage = int.Parse(specTable.NextSibling.NextSibling.InnerText.Replace(",", ""));

                                    specTable = listingBodyNode.SelectSingleNode(Properties.Settings.Default.LISTING_YEAR);
                                    if (specTable != null) newRow.Year = int.Parse(specTable.NextSibling.NextSibling.InnerText);

                                    specTable = listingBodyNode.SelectSingleNode(Properties.Settings.Default.LISTING_MAKE);
                                    if (specTable != null) newRow.Make = specTable.NextSibling.NextSibling.InnerText;

                                    specTable = listingBodyNode.SelectSingleNode(Properties.Settings.Default.LISTING_MODEL);
                                    if (specTable != null) newRow.Model = specTable.NextSibling.NextSibling.InnerText.Replace("&amp;", "&&");

                                    specTable = listingBodyNode.SelectSingleNode(Properties.Settings.Default.LISTING_MODEL);
                                    if (specTable != null) newRow.Model = specTable.NextSibling.NextSibling.InnerText;

                                    specTable = listingBodyNode.SelectSingleNode(Properties.Settings.Default.LISTING_CITY);
                                    if (specTable != null) newRow.City = specTable.InnerText.Trim().Split('|')[0];

                                    /*========Get Contact Info========*/
                                    contactRow.ListingID = newRow.ListingID;

                                    //<div class="contactName large blue">
                                    specTable = listingBodyNode.SelectSingleNode(Properties.Settings.Default.CONTACT_NAME);
                                    if (specTable != null) contactRow.Name = specTable.InnerText;

                                    //<a href="tel: 1 (888) 555-1234">
                                    specTable = listingBodyNode.SelectSingleNode(Properties.Settings.Default.CONTACT_PHONE);
                                    if (specTable != null) contactRow.Phone = specTable.Attributes["href"].Value.Split(':')[1].Trim();
                                }
                            }

                            carListings.Listings.Rows.Add(newRow);
                            carListings.ContactInfo.Rows.Add(contactRow);

                            int count = results.Count();

                            double currentPosition = (currentPageNum) * count + results.IndexOf(node);
                            double maxPosition = (lastPageNum + 1) * count;

                            minimumWageWorker.ReportProgress((int)(currentPosition / maxPosition * 100));
                        }

                        if (currentPageNum < lastPageNum)
                        {
                            string newUrl = "";

                            if (url.Contains("page=")) { newUrl = url.Replace("page=" + currentPageNum, "page=" + (currentPageNum + 1)); }
                            else { newUrl = url + "&page=" + (currentPageNum + 1); }

                            try
                            {
                                //Yay! Recursion!
                                parsePage(newUrl, e, currentPageNum + 1, lastPageNum);
                            }
                            catch (Exception ex)
                            {
                                //Throw the exception for recursion to handle.
                                throw ex;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Builds a URL from the contents of the text boxes
        /// </summary>
        /// <returns></returns>
        public string buildURL()
        {
            if (carListings.Searches.Count > 0)
            {
                //if (txtMiles.Text.Length < 0) txtMiles.Text = "0";

                CarListings.SearchesRow lastSearch = carListings.Searches.Last<CarListings.SearchesRow>();

                return "http://www.ksl.com/auto/search/index?"
                        + "&keyword=" + lastSearch.Keyword
                        + "&yearFrom=" + lastSearch.YearFrom
                        + "&yearTo=" + lastSearch.YearTo
                        + "&priceFrom=" + lastSearch.PriceFrom
                        + "&priceTo=" + lastSearch.PriceTo
                        + "&mileageFrom=" + lastSearch.MilesFrom
                        + "&mileageTo=" + lastSearch.MilesTo
                        + "&miles=" + lastSearch.Distance
                        + "&zip=" + lastSearch.Zip
                        + lastSearch.BodyStyles.Replace("|", "&body[]=").Replace(" ", "+");
            }
            else return "";
        }

        /// <summary>
        /// Loads data from the settings file, if it exists.
        /// </summary>
        public bool LoadData(bool CmdLine)
        {
            if (System.IO.File.Exists(Properties.Settings.Default.SettingsFileName))
            {
                carListings.ReadXml(Properties.Settings.Default.SettingsFileName);

                //You need to run the program from the GUI at least once before running from the commandline
                if (CmdLine)
                {
                    carListings.Listings.Clear();
                }
                else
                {
                    if (carListings.Searches.Count > 0 && carListings.Settings.First<CarListings.SettingsRow>().Field<Boolean>(carListings.Settings.LoadLastSearchParamsColumn))
                    {
                        CarListings.SearchesRow lastSearch = carListings.Searches.Last<CarListings.SearchesRow>();

                        numPriceLow.Value = (decimal)lastSearch.PriceFrom;
                        numPriceHigh.Value = (decimal)lastSearch.PriceTo;
                        numMileageLow.Value = lastSearch.MilesFrom;
                        numMileageHigh.Value = lastSearch.MilesTo;
                        numYearLow.Value = lastSearch.YearFrom;
                        numYearHigh.Value = lastSearch.YearTo;
                        txtZip.Text = lastSearch.Zip.ToString();
                        numDistance.Value = lastSearch.Distance;
                        txtKeyword.Text = lastSearch.Keyword;

                        string[] bodyStyles = lastSearch.BodyStyles.Split('|');

                        for (int i = 0; i < clbBodyTypes.Items.Count; i++)
                        {
                            if (bodyStyles.Contains(clbBodyTypes.Items[i].ToString()))
                            {
                                clbBodyTypes.SetItemChecked(i, true);
                            }
                        }
                    }

                    if (carListings.Listings.Count > 0 && carListings.Settings.First<CarListings.SettingsRow>().Field<Boolean>(carListings.Settings.SaveSearchResultsColumn))
                    {
                        dgvResults.Sort(Price, ListSortDirection.Ascending);
                        AddGraphLinks();
                    }

                    if (carListings.Settings.Rows.Count == 0)
                    {
                        CarListings.SettingsRow settings = carListings.Settings.NewSettingsRow();

                        carListings.Settings.Rows.Add(settings);
                    }
                }

                //Return whether or not we need to display any loaded search results from the last session;
                return carListings.Settings.First<CarListings.SettingsRow>().Field<Boolean>(carListings.Settings.SaveSearchResultsColumn);

            }
            else
            {
                //Preload default settings
                CarListings.SettingsRow settings = carListings.Settings.NewSettingsRow();

                carListings.Settings.Rows.Add(settings);

                //Didn't load any result data;
                return false;
            }

        }

        /// <summary>
        /// Save all the data!
        /// </summary>
        public void SaveData(bool CmdLine)
        {
            try
            {
                if (CmdLine)
                {
                    carListings.ContactInfo.Clear();
                    carListings.StatsByYear.Clear();
                    carListings.StatsByMake.Clear();
                    carListings.StatsByModel.Clear();
                }
                else
                {

                    if (!carListings.Settings.First<CarListings.SettingsRow>().Field<Boolean>(carListings.Settings.SaveSearchResultsColumn))
                    {
                        carListings.ContactInfo.Clear();
                        carListings.Listings.Clear();
                        carListings.StatsByYear.Clear();
                        carListings.StatsByMake.Clear();
                        carListings.StatsByModel.Clear();
                    }

                    if (carListings.Settings.First<CarListings.SettingsRow>().Field<Boolean>(carListings.Settings.LoadLastSearchParamsColumn))
                    {
                        carListings.Searches.Clear();

                        SaveSearch();
                    }
                }
            }
            catch (Exception ex)
            { //Log the message if there's an error.
                EventLogger.LogEvent(ex);
            }
            finally
            {
                //And then write the file.
                carListings.WriteXml(Properties.Settings.Default.SettingsFileName);
            }
        }

        /// <summary>
        /// Loads the current email settings and emails the results.
        /// </summary>
        public void emailResults()
        {
            if (carListings.Listings.Count > 0)
            {
                CarListings.SettingsRow currentSettings = carListings.Settings.First();

                Mailer postman = new Mailer(currentSettings.Username,
                                            Encryption.Decrypt(currentSettings.Password),
                                            currentSettings.FromAddress,
                                            currentSettings.SMTPHost,
                                            currentSettings.PortNumber,
                                            currentSettings.UseSSL);

                string htmlBody = buildTable(dgvResults.Rows);
                string subjectLine = "KSL Cars Search Results";

                this.Cursor = Cursors.WaitCursor;
                postman.SendMail(currentSettings.ToAddress, subjectLine, htmlBody);
                this.Cursor = Cursors.Default;
                MessageBox.Show("Email sent!", "Sent!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("There aren't any listings to email!", "No listings...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Builds an HTML table for the body of the email.
        /// </summary>
        /// <param name="sourceRows">Takes the DataGridViewRowCollection from the DataGridView</param>
        /// <returns>html table in a string</returns>
        public string buildTable(DataGridViewRowCollection sourceRows)
        {
            string tab = "\t";
            string nl = "\n";

            //standardize the start and end tags where possible
            //to make it easier to control tab indents and newlines.
            string rowStart = tab + "<tr>" + nl;
            string rowClose = tab + "</tr>" + nl;

            string cellStart = tab + tab + "<td>" + nl;
            string cellEnd = "</td>" + nl;

            //A little bit of CSS to put a border between the headers and data rows.
            string styleText = "<style media=\"screen\" type=\"text/css\">" + nl +
                                    tab + "tr.border_bottom th {" + nl +
                                      tab + tab + "border-bottom:1pt solid black;" + nl +
                                    tab + "}" + nl +
                                "</style>" + nl + nl;

            string table = "";

            //Gotta have some search results to display!
            if (sourceRows.Count > 0)
            {

                table += styleText + "<table>" + nl;

                //a little bit of CSS links in here to separate the headers.
                table += tab + "<tr class=\"border_bottom\">" + nl;
                foreach (DataGridViewColumn column in sourceRows[0].DataGridView.Columns)
                {
                    switch (column.Name)
                    {
                        case "Delete":
                            //We don't want this column, do nothing.
                            break;
                        case "Highlighted":
                            table += tab + tab + "<th>(X)</th>" + nl;
                            break;
                        default:
                            table += tab + tab + "<th>" + column.Name + "</th>" + nl;
                            break;
                    }
                }

                table += rowClose;

                foreach (DataGridViewRow row in sourceRows)
                {

                    // style="background-color:red">
                    table += rowStart.Replace(">", " style=\"background-color:" + row.DefaultCellStyle.BackColor.Name.ToLower() + "\">");
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        switch (cell.OwningColumn.Name)
                        {
                            case "Delete":
                                //We don't want this column, do nothing.
                                break;
                            case "Highlighted":
                                table += tab + tab + "<td title='" + cell.ToolTipText + "'><input type=\"checkbox\" disabled=\"disabled\" " + (bool.Parse(cell.FormattedValue.ToString()) ? "checked=\"checked\"" : "") + " />" + cellEnd;
                                break;
                            case "Link":
                            case "VIN":
                                table += cellStart + "<a href=\"" + cell.ToolTipText + "\">" + cell.Value + "</a>" + cellEnd;
                                break;
                            case "Price":
                                table += cellStart + String.Format("{0:C}", cell.Value) + cellEnd;
                                break;
                            case "City":
                                table += cellStart + "<div title=\"" + cell.ToolTipText + "\">" + cell.Value + "</div>" + cellEnd;
                                break;
                            case "Description":
                                //We only want the first little bit of the Description column...
                                table += cellStart + cell.Value.ToString().Remove(Properties.Settings.Default.Truncate_Desc_Column) + "..." + cellEnd;
                                break;
                            default:
                                //Default text (no formatting)
                                table += cellStart + cell.Value + cellEnd;
                                break;
                        }
                    }
                    table += rowClose;
                }

                table += "</table>";
            }
            return table;
        }

        /// <summary>
        /// Checks to see if all the email settings are filled out
        /// </summary>
        /// <returns>Returns True is the email settings are complete.</returns>
        private bool HaveWeGotCompleteEmailSettings()
        {
            bool returnVal = true;

            CarListings.SettingsRow settings = carListings.Settings.First();

            returnVal = returnVal && (settings.SMTPHost.Length > 0);
            returnVal = returnVal && (settings.ToAddress.Length > 0);
            returnVal = returnVal && (settings.FromAddress.Length > 0);
            returnVal = returnVal && (settings.Username.Length > 0);
            returnVal = returnVal && (settings.Password.Length > 0);
            //settings.UseSSL is always either checked or unchecked, so the 
            //value is valid. we can't know whether or not it is *correct*

            return returnVal;
        }

        /// <summary>
        /// Saves the current search parameters to a row in the Searches table.
        /// </summary>
        private void SaveSearch()
        {
            int zip;
            if (!int.TryParse(txtZip.Text, out zip)) zip = 0;

            string bodyStyles = "";
            foreach (object item in clbBodyTypes.CheckedItems)
            {
                bodyStyles += "|" + item.ToString();
            }

            CarListings.SearchesRow newRow = carListings.Searches.NewSearchesRow();
            newRow.TimeOfSearch = DateTime.Now;
            newRow.YearFrom = Convert.ToInt32(numYearLow.Value);
            newRow.YearTo = Convert.ToInt32(numYearHigh.Value);
            newRow.PriceFrom = Convert.ToInt32(numPriceLow.Value);
            newRow.PriceTo = Convert.ToInt32(numPriceHigh.Value);
            newRow.MilesFrom = Convert.ToInt32(numMileageLow.Value);
            newRow.MilesTo = Convert.ToInt32(numMileageHigh.Value);
            newRow.Distance = Convert.ToInt32(numDistance.Value);
            newRow.Zip = zip;
            newRow.Keyword = txtKeyword.Text;
            newRow.BodyStyles = bodyStyles;

            carListings.Searches.AddSearchesRow(newRow);
        }

        /// <summary>
        /// Disables a set of controls while a search is running.
        /// </summary>
        private void DisableWhileSearching()
        {
            foreach (Control control in grpSearchParams.Controls)
            {
                if (!(control is Label))
                {
                    control.Enabled = false;
                }
            }

            btnScrape.Enabled = false;
            tabStats.Enabled = false;
            dgvResults.Visible = false;

            btnCancel.Enabled = true;

        }

        /// <summary>
        /// Re-enabled a set of controls after a search has been completed or cancelled.
        /// </summary>
        private void EnableAfterSearching()
        {
            foreach (Control control in grpSearchParams.Controls)
            {
                if (!(control is Label))
                {
                    control.Enabled = true;
                }
            }

            btnScrape.Enabled = true;
            tabStats.Enabled = true;
            dgvResults.Visible = true;

            btnCancel.Enabled = false;
        }
    }
}
