﻿using HtmlAgilityPack;
using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C.Scrape
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnScrape_Click(object sender, EventArgs e)
        {
            flowYears.Controls.Clear();
            flowMakes.Controls.Clear();
            carListings.Listings.Clear();
            carListings.StatsByYear.Clear();
            carListings.StatsByMake.Clear();
            carListings.StatsByModel.Clear();

            CarListings.SearchesRow newRow = carListings.Searches.NewSearchesRow();

            newRow[carListings.Searches.TimeOfSearchColumn.ColumnName] = DateTime.Now;
            newRow[carListings.Searches.YearFromColumn.ColumnName] = int.Parse(txtYearFrom.Text);
            newRow[carListings.Searches.YearToColumn.ColumnName] = int.Parse(txtYearTo.Text);
            newRow[carListings.Searches.PriceFromColumn.ColumnName] = double.Parse(txtPriceFrom.Text);
            newRow[carListings.Searches.PriceToColumn.ColumnName] = double.Parse(txtPriceTo.Text);
            newRow[carListings.Searches.MilesFromColumn.ColumnName] = int.Parse(txtMileageFrom.Text);
            newRow[carListings.Searches.MilesToColumn.ColumnName] = int.Parse(txtMileageTo.Text);
            newRow[carListings.Searches.ZipColumn.ColumnName] = int.Parse(txtZip.Text);
            newRow[carListings.Searches.DistanceColumn.ColumnName] = int.Parse(txtMiles.Text);
            newRow[carListings.Searches.KeywordColumn.ColumnName] = txtKeyword.Text;

            carListings.Searches.AddSearchesRow(newRow);

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
            dgvResults.Visible = false;

            minimumWageWorker.RunWorkerAsync(url);
        }

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

        private void parsePage(string url, int currentPageNum, int lastPageNum)
        {
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

            // There are various options, set as needed
            htmlDoc.OptionFixNestedTags = true;

            string htmlCode = (new WebClient()).DownloadString(url);

            htmlDoc.LoadHtml(htmlCode);  // Used to load from a string (was htmlDoc.LoadXML(xmlString)

            if (htmlDoc.DocumentNode != null)
            {
                HtmlAgilityPack.HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

                if (bodyNode != null)
                {
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


                    // Do something with bodyNode
                    HtmlNodeCollection results = bodyNode.SelectNodes(Properties.Settings.Default.LISTING_SEARCH_RESULTS);

                    if (results != null && results.Count > 0)
                    {
                        //List<Listing> Cars = new List<Listing>();

                        foreach (HtmlNode node in results)
                        {
                            CarListings.ListingsRow newRow = carListings.Listings.NewListingsRow();

                            newRow.Price = Double.Parse(node.SelectSingleNode(Properties.Settings.Default.LISTING_RESULT_PRICE).InnerText.Replace("$", "").Replace(",", ""));

                            newRow.ListingID = int.Parse(node.SelectSingleNode(Properties.Settings.Default.LISTING_RESULT_ID).Attributes["href"].Value.Split('/').Last<string>().Split('?').First<string>());

                            newRow.Link = Properties.Settings.Default.LISTING_LINK.Replace("{LISTING_ID}", newRow.ListingID.ToString());

                            //We've got all the info from the listing page, new we go into the page itself for info like VIN and Mileage.

                            HtmlAgilityPack.HtmlDocument singleListing = new HtmlAgilityPack.HtmlDocument();

                            // There are various options, set as needed
                            singleListing.OptionFixNestedTags = true;

                            string singleListingCode = (new WebClient()).DownloadString(newRow.Link);

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
                                    if (specTable != null) newRow.Mileage = specTable.NextSibling.NextSibling.InnerText;

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
                                }
                            }

                            carListings.Listings.Rows.Add(newRow);

                            //pageScrapeProgress = current node out of results list.
                            int pageProgress = (int)(((double)(results.IndexOf(node) + 1) / (double)(results.Count + 1)) * 100);

                            //totalScrapeProgress = current page out of total pages.
                            int totalProgress = (int)(((double)(currentPageNum + 1) / (double)(lastPageNum + 1)) * 100);

                            //Mux the two together.
                            minimumWageWorker.ReportProgress((pageProgress * 100) + totalProgress);

                            //minimumWageWorker.ReportProgress((int)((double)(results.IndexOf(node) * (currentPageNum + 1)) / (double)(results.Count * (lastPageNum + 1)) * 100));
                            //minimumWageWorker.ReportProgress((int)(((double)((currentPageNum + 1) * results.Count) / (double)((lastPageNum + 1)) * results.Count) * 100));
                        }

                        if (currentPageNum < lastPageNum)
                        {
                            string newUrl = "";

                            if (url.Contains("page="))
                            {
                                newUrl = url.Replace("page=" + currentPageNum, "page=" + (currentPageNum + 1));
                            }
                            else
                            {
                                newUrl = url + "&page=" + (currentPageNum + 1);
                            }

                            parsePage(newUrl, currentPageNum + 1, lastPageNum);
                        }
                    }
                }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!carListings.Settings.First<CarListings.SettingsRow>().Field<Boolean>(carListings.Settings.SaveSearchResultsColumn))
                {
                    carListings.Listings.Clear();
                }

                if (!carListings.Settings.First<CarListings.SettingsRow>().Field<Boolean>(carListings.Settings.SaveStatsColumn))
                {
                    carListings.StatsByYear.Clear();
                    carListings.StatsByMake.Clear();
                    carListings.StatsByModel.Clear();
                }

                if (carListings.Settings.First<CarListings.SettingsRow>().Field<Boolean>(carListings.Settings.LoadLastSearchParamsColumn))
                {

                    CarListings.SearchesRow newRow = carListings.Searches.NewSearchesRow();

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

                    carListings.Searches.AddSearchesRow(newRow);

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

                carListings.WriteXml("C.Scrape.settings");
            }
            catch { } //Don't do anything if there's an error.
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists("C.Scrape.settings"))
            {
                carListings.ReadXml("C.Scrape.settings");

                if (carListings.Settings.First<CarListings.SettingsRow>().Field<Boolean>(carListings.Settings.LoadLastSearchParamsColumn))
                {
                    CarListings.SearchesRow lastSearch = (CarListings.SearchesRow)(carListings.Searches.Rows[carListings.Searches.Rows.Count - 1]);

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
        }

        private void minimumWageWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string url = e.Argument.ToString();
            minimumWageWorker.ReportProgress(0);
            parsePage(url, 0, 0);
        }

        private void minimumWageWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int pageProgress = e.ProgressPercentage / 100;
            int totalProgress = e.ProgressPercentage % 100;

            pageScrapeProgress.Value = pageProgress;
            totalScrapeProgress.Value = totalProgress;
        }

        private void minimumWageWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pageScrapeProgress.Value = 100;
            totalScrapeProgress.Value = 100;

            btnScrape.Enabled = true;

            dgvResults.TopLeftHeaderCell.ToolTipText = "Total Listings Found: " + carListings.Listings.Rows.Count;

            dgvResults.Visible = true;
            dgvResults.AutoResizeColumns();
            dgvResults.Update();
            computeStats();

            dgvResults.Sort(dgvResults.Columns["Price"], ListSortDirection.Ascending);
        }

        private void computeStats()
        {
            //Get list of unique Years, Makes, and //Models
            List<string> Years = new List<string>();
            List<string> Makes = new List<string>();
            List<string> Models = new List<string>();

            foreach (DataRow row in carListings.Listings.Rows)
            {
                string tempYear = row["Year"].ToString().Trim();
                if (!Years.Contains(tempYear)) Years.Add(tempYear);

                string tempMake = row["Make"].ToString().Trim();
                if (!Makes.Contains(tempMake)) Makes.Add(tempMake);

                string tempModel = row["Model"].ToString().Trim();
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
                temp.Text = Year + " (" + carListings.Listings.Compute("Count(Year)", filter) + ")";
                toolTip.SetToolTip(temp,
                    String.Format("{0:C}", (double)carListings.Listings.Compute("Min(Price)", filter)) + "/"
                    + String.Format("{0:C}", (double)carListings.Listings.Compute("Max(Price)", filter)) + "/"
                    + String.Format("{0:C}", (double)carListings.Listings.Compute("Avg(Price)", filter)));
                flowYears.Controls.Add(temp);

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

                LinkLabel temp = new LinkLabel();
                temp.AutoSize = true;
                temp.Click += new System.EventHandler(this.openGraph);
                temp.Tag = "Make";
                temp.Text = Make + " (" + carListings.Listings.Compute("Count(Make)", filter) + ")";
                toolTip.SetToolTip(temp,
                    String.Format("{0:C}", (double)carListings.Listings.Compute("Min(Price)", filter)) + "/"
                    + String.Format("{0:C}", (double)carListings.Listings.Compute("Max(Price)", filter)) + "/"
                    + String.Format("{0:C}", (double)carListings.Listings.Compute("Avg(Price)", filter)));
                flowMakes.Controls.Add(temp);

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
                string filter = "Make='" + Model + "'";

                /*LinkLabel temp = new LinkLabel();
                temp.AutoSize = true;
                temp.Click += new System.EventHandler(this.openGraph);
                temp.Tag = "Model";
                temp.Text = Model + " (" + carListings.Listings.Compute("Count(Model)", filter) + ")";
                toolTip.SetToolTip(temp,
                    String.Format("{0:C}", (double)carListings.Listings.Compute("Min(Price)", filter)) + "/"
                    + String.Format("{0:C}", (double)carListings.Listings.Compute("Max(Price)", filter)) + "/"
                    + String.Format("{0:C}", (double)carListings.Listings.Compute("Avg(Price)", filter)));
                flowModels.Controls.Add(temp);*/

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

        private void openGraph(object sender, EventArgs e)
        {
            DataTable dataForChart = new DataTable();
            dataForChart = carListings.Listings.Clone();
            dataForChart.PrimaryKey = null;
            dataForChart.Columns.Remove("ListingID");
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

            Graph myChartForm = new Graph(dataForChart);
            myChartForm.Show();
        }

        private void highlight()
        {
            foreach (DataGridViewRow row in dgvResults.Rows)
            {
                CarListings.StatsByYearRow yearStats = carListings.StatsByYear.FindByYear(int.Parse(row.Cells["Year"].Value.ToString()));
                CarListings.StatsByMakeRow makeStats = carListings.StatsByMake.FindByMake(row.Cells["Make"].Value.ToString());
                CarListings.StatsByModelRow modelStats = carListings.StatsByModel.FindByModel(row.Cells["Model"].Value.ToString());

                CarListings.ListingsRow matchingListing = carListings.Listings.FindByListingID(int.Parse(row.Cells["ListingID"].Value.ToString()));

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

                    if (highlight)
                    {
                        row.Cells["Highlighted"].ToolTipText = "Price is significantly lower than average for a " + row.Cells[matchingColumn].Value.ToString();
                        row.Cells["Highlighted"].Value = true;
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
            }
        }

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

        private void dgvResults_Sorted(object sender, EventArgs e)
        {
            highlight();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout aboot = new frmAbout();
            aboot.ShowDialog();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSettings settings = new frmSettings();

            if (settings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (carListings.Settings.Rows.Count == 0) carListings.Settings.Rows.Add(carListings.Settings.NewSettingsRow());

                carListings.Settings.Rows[0][carListings.Settings.SaveSearchResultsColumn.ColumnName] = settings.chkSaveLastListings.Checked;
                carListings.Settings.Rows[0][carListings.Settings.LoadLastSearchParamsColumn.ColumnName] = settings.chkKeepSearchParameters.Checked;
                carListings.Settings.Rows[0][carListings.Settings.SaveStatsColumn.ColumnName] = settings.chkKeepStatsData.Checked;
            }
        }
    }
}
