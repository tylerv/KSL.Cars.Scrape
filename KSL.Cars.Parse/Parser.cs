using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KSL.Cars.Parse
{
    public class Parser
    {
        public CarListings dataStorage = new CarListings();

        public Parser()
        {
            dataStorage.DataSetName = "CarListings";
            dataStorage.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        }

        /// <summary>
        /// Workhorse that gets the HTML and parses it into the datatable.
        /// </summary>
        public void parsePage(ref BackgroundWorker worker1, ref DoWorkEventArgs e, string url, int currentPageNum, int lastPageNum)
        {
            //Check for and cancel if needed.
            if ((worker1.CancellationPending == true))
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
                            if ((worker1.CancellationPending == true))
                            {
                                e.Cancel = true;
                                return;
                            }
                            CarListings.ListingsRow newRow = dataStorage.Listings.NewListingsRow();
                            CarListings.ContactInfoRow contactRow = dataStorage.ContactInfo.NewContactInfoRow();

                            newRow.Price = Double.Parse(node.SelectSingleNode(Properties.Settings.Default.LISTING_RESULT_PRICE).InnerText.Replace("$", "").Replace(",", ""));

                            newRow.ListingID = int.Parse(node.SelectSingleNode(Properties.Settings.Default.LISTING_RESULT_ID).Attributes["href"].Value.Split('/').Last<string>().Split('?').First<string>());

                            //newRow.Link = Properties.Settings.Default.LISTING_LINK.Replace("{LISTING_ID}", newRow.ListingID.ToString());

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

                            dataStorage.Listings.Rows.Add(newRow);
                            dataStorage.ContactInfo.Rows.Add(contactRow);

                            int count = results.Count();

                            double currentPosition = (currentPageNum) * count + results.IndexOf(node);
                            double maxPosition = (lastPageNum + 1) * count;

                            worker1.ReportProgress((int)(currentPosition / maxPosition * 100));
                        }

                        if (currentPageNum < lastPageNum)
                        {
                            string newUrl = "";

                            if (url.Contains("page=")) { newUrl = url.Replace("page=" + currentPageNum, "page=" + (currentPageNum + 1)); }
                            else { newUrl = url + "&page=" + (currentPageNum + 1); }

                            try
                            {
                                //Yay! Recursion!
                                parsePage(ref worker1, ref e, newUrl, currentPageNum + 1, lastPageNum);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                }
            }
        }
    }
}
