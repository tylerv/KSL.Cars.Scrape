using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using KSL.Cars.Parse;

namespace KSL.Cars.App
{
    public partial class frmMain : Form
    {
        Parser myParser = new Parser();

        public frmMain()
        {
            InitializeComponent();
        }

        #region EventHandlers

        /// <summary>
        /// This starts the whole process off. Clears any existing data, builds a URL from the search parameters, then passes it to the recursive parse function in a worker process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScrape_Click(object sender, EventArgs e)
        {
            if (minimumWageWorker.IsBusy != true)
            {
                flowYears.Controls.Clear();
                flowMakes.Controls.Clear();
                myParser.dataStorage.ContactInfo.Clear();
                myParser.dataStorage.Listings.Clear();
                myParser.dataStorage.StatsByYear.Clear();
                myParser.dataStorage.StatsByMake.Clear();
                myParser.dataStorage.StatsByModel.Clear();

                CarListings.SearchesRow newRow = myParser.dataStorage.Searches.NewSearchesRow();

                newRow[myParser.dataStorage.Searches.TimeOfSearchColumn.ColumnName] = DateTime.Now;
                newRow[myParser.dataStorage.Searches.YearFromColumn.ColumnName] = int.Parse(txtYearFrom.Text);
                newRow[myParser.dataStorage.Searches.YearToColumn.ColumnName] = int.Parse(txtYearTo.Text);
                newRow[myParser.dataStorage.Searches.PriceFromColumn.ColumnName] = double.Parse(txtPriceFrom.Text);
                newRow[myParser.dataStorage.Searches.PriceToColumn.ColumnName] = double.Parse(txtPriceTo.Text);
                newRow[myParser.dataStorage.Searches.MilesFromColumn.ColumnName] = int.Parse(txtMileageFrom.Text);
                newRow[myParser.dataStorage.Searches.MilesToColumn.ColumnName] = int.Parse(txtMileageTo.Text);
                newRow[myParser.dataStorage.Searches.ZipColumn.ColumnName] = int.Parse(txtZip.Text);
                newRow[myParser.dataStorage.Searches.DistanceColumn.ColumnName] = int.Parse(txtMiles.Text);
                newRow[myParser.dataStorage.Searches.KeywordColumn.ColumnName] = txtKeyword.Text;

                myParser.dataStorage.Searches.AddSearchesRow(newRow);

                string url = "http://www.ksl.com/auto/search/index?"
                    + "&keyword=" + txtKeyword.Text
                    + "&yearFrom=" + txtYearFrom.Text
                    + "&yearTo=" + txtYearTo.Text
                    + "&priceFrom=" + int.Parse(txtPriceFrom.Text)
                    + "&priceTo=" + txtPriceTo.Text.Replace(",", "").Replace("$", "").Replace(".", "")
                    + "&mileageFrom=" + txtMileageFrom.Text.Replace(",", "")
                    + "&mileageTo=" + String.Format("{0:D}", txtMileageTo.Text)
                    + "&zip=" + txtZip.Text;

                if (txtMiles.Text.Length > 0)
                {
                    url += "&miles=" + txtMiles.Text;
                }

                btnScrape.Enabled = false;
                btnCancel.Enabled = true;
                dgvResults.Visible = false;

                minimumWageWorker.RunWorkerAsync(url);
            }
        }

        /// <summary>
        /// This fires off the links for the Link and VIN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (dgvResults.Columns[e.ColumnIndex].Name.Equals("Listing_Link") || dgvResults.Columns[e.ColumnIndex].Name.Equals("VIN_Link"))
                {
                    System.Diagnostics.Process.Start("iexplore.exe", this.dgvResults.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText);
                }
            }
        }

        /// <summary>
        /// Saves the different bits of data if set in the options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!myParser.dataStorage.Settings.First<CarListings.SettingsRow>().Field<Boolean>(myParser.dataStorage.Settings.SaveSearchResultsColumn))
                {
                    myParser.dataStorage.ContactInfo.Clear();
                    myParser.dataStorage.Listings.Clear();
                }

                if (!myParser.dataStorage.Settings.First<CarListings.SettingsRow>().Field<Boolean>(myParser.dataStorage.Settings.SaveStatsColumn))
                {
                    myParser.dataStorage.StatsByYear.Clear();
                    myParser.dataStorage.StatsByMake.Clear();
                    myParser.dataStorage.StatsByModel.Clear();
                }

                if (myParser.dataStorage.Settings.First<CarListings.SettingsRow>().Field<Boolean>(myParser.dataStorage.Settings.LoadLastSearchParamsColumn))
                {
                    myParser.dataStorage.Searches.Clear();

                    CarListings.SearchesRow newRow = myParser.dataStorage.Searches.NewSearchesRow();

                    newRow.TimeOfSearch = DateTime.Now;
                    newRow.YearFrom = int.Parse(txtYearFrom.Text);
                    newRow.YearTo = int.Parse(txtYearTo.Text);
                    newRow.PriceFrom = double.Parse(txtPriceFrom.Text);
                    newRow.PriceTo = double.Parse(txtPriceTo.Text);
                    newRow.MilesFrom = int.Parse(txtMileageFrom.Text);
                    newRow.MilesTo = int.Parse(txtMileageTo.Text);
                    newRow.Zip = int.Parse(txtZip.Text);
                    newRow.Distance = int.Parse(txtMiles.Text);
                    newRow.Keyword = txtKeyword.Text;

                    myParser.dataStorage.Searches.AddSearchesRow(newRow);

                    //Properties.Settings.Default.priceLow = double.Parse(txtPriceFrom.Text);
                    //Properties.Settings.Default.priceHigh = double.Parse(txtPriceTo.Text);
                    //Properties.Settings.Default.milesLow = int.Parse(txtMileageFrom.Text);
                    //Properties.Settings.Default.milesHigh = int.Parse(txtMileageTo.Text);
                    //Properties.Settings.Default.yearLow = int.Parse(txtYearFrom.Text);
                    //Properties.Settings.Default.yearHigh = int.Parse(txtYearTo.Text);
                    //Properties.Settings.Default.zipCode = int.Parse(txtZip.Text);
                    //Properties.Settings.Default.distance = int.Parse(txtMiles.Text);
                    //Properties.Settings.Default.Keyword = txtKeyword.Text;

                    ////apply the changes to the settings file
                    //Properties.Settings.Default.Save();
                }

                myParser.dataStorage.WriteXml("KSL.Cars.App.settings");
            }
            catch { } //Don't do anything if there's an error.
        }

        /// <summary>
        /// Loads the data saved from the last session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists("KSL.Cars.App.settings"))
            {
                myParser.dataStorage.ReadXml("KSL.Cars.App.settings");

                if (myParser.dataStorage.Settings.First<CarListings.SettingsRow>().Field<Boolean>(myParser.dataStorage.Settings.LoadLastSearchParamsColumn))
                {
                    CarListings.SearchesRow lastSearch = (CarListings.SearchesRow)(myParser.dataStorage.Searches.Rows[myParser.dataStorage.Searches.Rows.Count - 1]);

                    txtPriceFrom.Text = lastSearch.PriceFrom.ToString();
                    txtPriceTo.Text = lastSearch.PriceTo.ToString();
                    txtMileageFrom.Text = lastSearch.MilesFrom.ToString();
                    txtMileageTo.Text = lastSearch.MilesTo.ToString();
                    txtYearFrom.Text = lastSearch.YearFrom.ToString();
                    txtYearTo.Text = lastSearch.YearTo.ToString();
                    txtZip.Text = lastSearch.Zip.ToString();
                    txtMiles.Text = lastSearch.Distance.ToString();
                    txtKeyword.Text = lastSearch.Keyword;
                }
            }
            else
            {
                if (myParser.dataStorage.Settings.Rows.Count == 0)
                {
                    CarListings.SettingsRow settings = myParser.dataStorage.Settings.NewSettingsRow();

                    settings.LoadLastSearchParams = true;
                    settings.SaveStats = false;
                    settings.SaveSearchResults = false;

                    myParser.dataStorage.Settings.Rows.Add(settings);
                }

            }
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

            if (dgvResults.Columns[e.ColumnIndex].Name.Equals("VIN_Link"))
            {
                cell.ToolTipText = Properties.Settings.Default.VIN_LINK.Replace("{VIN}", cell.OwningRow.Cells["VIN"].Value.ToString());
                e.FormattingApplied = true;
            }
            else if (dgvResults.Columns[e.ColumnIndex].Name.Equals("Listing_Link"))
            {
                //cell.Value = "Link";
                cell.ToolTipText = Properties.Settings.Default.LISTING_LINK.Replace("{LISTING_ID}", cell.OwningRow.Cells["ListingID"].Value.ToString());
                e.FormattingApplied = true;
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
            frmSettings settings = new frmSettings((CarListings.SettingsRow)(myParser.dataStorage.Settings.Rows[0]));

            if (settings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (myParser.dataStorage.Settings.Rows.Count == 0) myParser.dataStorage.Settings.Rows.Add(myParser.dataStorage.Settings.NewSettingsRow());

                myParser.dataStorage.Settings.Rows[0][myParser.dataStorage.Settings.SaveSearchResultsColumn.ColumnName] = settings.chkSaveLastListings.Checked;
                myParser.dataStorage.Settings.Rows[0][myParser.dataStorage.Settings.LoadLastSearchParamsColumn.ColumnName] = settings.chkKeepSearchParameters.Checked;
                myParser.dataStorage.Settings.Rows[0][myParser.dataStorage.Settings.SaveStatsColumn.ColumnName] = settings.chkKeepStatsData.Checked;
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
                myParser.parsePage(ref minimumWageWorker, ref e, url, 0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                btnScrape.Enabled = true;
                btnCancel.Enabled = false;

                dgvResults.TopLeftHeaderCell.ToolTipText = "Total Listings Found: " + myParser.dataStorage.Listings.Rows.Count;

                dgvResults.Visible = true;
                dgvResults.AutoResizeColumns();
                dgvResults.Update();
                computeStats();

                dgvResults.Sort(dgvResults.Columns["Price"], ListSortDirection.Ascending);
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
            dataForChart = myParser.dataStorage.Listings.Clone();
            dataForChart.Columns.Remove("Description");

            switch (((Control)sender).Tag.ToString())
            {

                case "Year":
                    dataForChart.Columns[0].Caption = "Year " + ((LinkLabel)sender).Text.ToString();
                    var yearQuery = from row in myParser.dataStorage.Listings.AsEnumerable()
                                    where (row.Field<Int32>("Year") == Int32.Parse(((LinkLabel)sender).Text.Split(' ')[0].ToString()))
                                    select row;
                    foreach (DataRow dr in yearQuery) dataForChart.ImportRow(dr);
                    break;
                case "Make":
                    dataForChart.Columns[0].Caption = ((LinkLabel)sender).Text.ToString();
                    var makeQuery = from row in myParser.dataStorage.Listings.AsEnumerable()
                                    where (row.Field<string>("Make") == ((LinkLabel)sender).Text.Split(' ')[0].ToString())
                                    select row;
                    foreach (DataRow dr in makeQuery) dataForChart.ImportRow(dr);
                    break;
            }

            Graph myChartForm = new Graph(dataForChart);
            myChartForm.Show();
        }

        #endregion

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

            foreach (DataRow row in myParser.dataStorage.Listings.Rows)
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

                LinkLabel temp = new LinkLabel();
                temp.AutoSize = true;
                temp.Click += new System.EventHandler(this.openGraph);
                temp.Tag = "Year";
                temp.Text = Year + " (" + myParser.dataStorage.Listings.Compute("Count(Year)", filter) + ")";
                toolTip.SetToolTip(temp,
                    String.Format("{0:C}", (double)myParser.dataStorage.Listings.Compute("Min(Price)", filter)) + "/"
                    + String.Format("{0:C}", (double)myParser.dataStorage.Listings.Compute("Max(Price)", filter)) + "/"
                    + String.Format("{0:C}", (double)myParser.dataStorage.Listings.Compute("Avg(Price)", filter)));
                flowYears.Controls.Add(temp);

                CarListings.StatsByYearRow newRow = myParser.dataStorage.StatsByYear.NewStatsByYearRow();

                newRow["Year"] = int.Parse(Year);
                newRow["Min"] = myParser.dataStorage.Listings.Compute("Min(Price)", filter);
                newRow["Max"] = myParser.dataStorage.Listings.Compute("Max(Price)", filter);

                double avg;
                if (double.TryParse(myParser.dataStorage.Listings.Compute("Avg(Price)", filter).ToString(), out avg)) newRow["Avg"] = avg;
                else newRow["Avg"] = 0;

                double stDev;
                if (double.TryParse(myParser.dataStorage.Listings.Compute("StDev(Price)", filter).ToString(), out stDev)) newRow["StDev"] = stDev;
                else newRow["StDev"] = 0;

                newRow["Count"] = myParser.dataStorage.Listings.Compute("Count(Price)", filter);
                newRow["Sum"] = myParser.dataStorage.Listings.Compute("Sum(Price)", filter);

                double var;
                if (double.TryParse(myParser.dataStorage.Listings.Compute("Var(Price)", filter).ToString(), out var)) newRow["Var"] = stDev;
                else newRow["Var"] = 0;

                myParser.dataStorage.StatsByYear.AddStatsByYearRow(newRow);
            }

            foreach (string Make in Makes)
            {
                string filter = "Make='" + Make + "'";

                LinkLabel temp = new LinkLabel();
                temp.AutoSize = true;
                temp.Click += new System.EventHandler(this.openGraph);
                temp.Tag = "Make";
                temp.Text = Make + " (" + myParser.dataStorage.Listings.Compute("Count(Make)", filter) + ")";
                toolTip.SetToolTip(temp,
                    String.Format("{0:C}", (double)myParser.dataStorage.Listings.Compute("Min(Price)", filter)) + "/"
                    + String.Format("{0:C}", (double)myParser.dataStorage.Listings.Compute("Max(Price)", filter)) + "/"
                    + String.Format("{0:C}", (double)myParser.dataStorage.Listings.Compute("Avg(Price)", filter)));
                flowMakes.Controls.Add(temp);

                CarListings.StatsByMakeRow newRow = myParser.dataStorage.StatsByMake.NewStatsByMakeRow();

                newRow["Make"] = Make;
                newRow["Min"] = myParser.dataStorage.Listings.Compute("Min(Price)", filter);
                newRow["Max"] = myParser.dataStorage.Listings.Compute("Max(Price)", filter);

                double avg;
                if (double.TryParse(myParser.dataStorage.Listings.Compute("Avg(Price)", filter).ToString(), out avg)) newRow["Avg"] = avg;
                else newRow["Avg"] = 0;

                double stDev;
                if (double.TryParse(myParser.dataStorage.Listings.Compute("StDev(Price)", filter).ToString(), out stDev)) newRow["StDev"] = stDev;
                else newRow["StDev"] = 0;

                newRow["Count"] = myParser.dataStorage.Listings.Compute("Count(Price)", filter);
                newRow["Sum"] = myParser.dataStorage.Listings.Compute("Sum(Price)", filter);

                double var;
                if (double.TryParse(myParser.dataStorage.Listings.Compute("Var(Price)", filter).ToString(), out var)) newRow["Var"] = stDev;
                else newRow["Var"] = 0;

                myParser.dataStorage.StatsByMake.AddStatsByMakeRow(newRow);
            }

            foreach (string Model in Models)
            {
                string filter = "Make='" + Model + "'";

                /*LinkLabel temp = new LinkLabel();
                temp.AutoSize = true;
                temp.Click += new System.EventHandler(this.openGraph);
                temp.Tag = "Model";
                temp.Text = Model + " (" + myParser.dataStorage.Listings.Compute("Count(Model)", filter) + ")";
                toolTip.SetToolTip(temp,
                    String.Format("{0:C}", (double)myParser.dataStorage.Listings.Compute("Min(Price)", filter)) + "/"
                    + String.Format("{0:C}", (double)myParser.dataStorage.Listings.Compute("Max(Price)", filter)) + "/"
                    + String.Format("{0:C}", (double)myParser.dataStorage.Listings.Compute("Avg(Price)", filter)));
                flowModels.Controls.Add(temp);*/

                CarListings.StatsByModelRow newRow = myParser.dataStorage.StatsByModel.NewStatsByModelRow();

                newRow["Model"] = Model;
                newRow["Min"] = myParser.dataStorage.Listings.Compute("Min(Price)", filter);
                newRow["Max"] = myParser.dataStorage.Listings.Compute("Max(Price)", filter);

                double avg;
                if (double.TryParse(myParser.dataStorage.Listings.Compute("Avg(Price)", filter).ToString(), out avg)) newRow["Avg"] = avg;
                else newRow["Avg"] = 0;

                double stDev;
                if (double.TryParse(myParser.dataStorage.Listings.Compute("StDev(Price)", filter).ToString(), out stDev)) newRow["StDev"] = stDev;
                else newRow["StDev"] = 0;

                newRow["Count"] = myParser.dataStorage.Listings.Compute("Count(Price)", filter);
                newRow["Sum"] = myParser.dataStorage.Listings.Compute("Sum(Price)", filter);

                double var;
                if (double.TryParse(myParser.dataStorage.Listings.Compute("Var(Price)", filter).ToString(), out var)) newRow["Var"] = stDev;
                else newRow["Var"] = 0;

                myParser.dataStorage.StatsByModel.AddStatsByModelRow(newRow);
            }
        }

        /// <summary>
        /// Highlights certain rows based on price and the presence of contact information. Also adds tooltips to rows.
        /// </summary>
        private void highlight()
        {
            foreach (DataGridViewRow row in dgvResults.Rows)
            {
                CarListings.StatsByYearRow yearStats = myParser.dataStorage.StatsByYear.FindByYear(int.Parse(row.Cells["Year"].Value.ToString()));
                CarListings.StatsByMakeRow makeStats = myParser.dataStorage.StatsByMake.FindByMake(row.Cells["Make"].Value.ToString());
                CarListings.StatsByModelRow modelStats = myParser.dataStorage.StatsByModel.FindByModel(row.Cells["Model"].Value.ToString());

                CarListings.ListingsRow matchingListing = myParser.dataStorage.Listings.FindByListingID(int.Parse(row.Cells["ListingID"].Value.ToString()));
                CarListings.ContactInfoRow contactInfo = myParser.dataStorage.ContactInfo.FindByListingID(int.Parse(row.Cells["ListingID"].Value.ToString()));

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

                    if (contactInfo.IsPhoneNull())
                    {
                        row.Cells["Highlighted"].ToolTipText = "This car doesn't have a phone number listed (properly). Beware a scam...";
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        if (contactInfo.IsNameNull()) row.Cells["City"].ToolTipText = contactInfo.Phone;
                        else row.Cells["City"].ToolTipText = contactInfo.Name + ": " + contactInfo.Phone;

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

    }
}
