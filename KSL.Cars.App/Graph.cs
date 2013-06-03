using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KSL.Cars.App
{
    public partial class Graph : Form
    {
        DataTable chartData;
        public Graph(DataTable dataIn)
        {
            InitializeComponent();

            chartData = dataIn;

            //foreach (DataRow row in chartData.Rows) row["VIN"] = Properties.Settings.Default.VIN_LINK.Replace("{VIN}", row["VIN"].ToString());

            var enumerableTable = (chartData as System.ComponentModel.IListSource).GetList();
            myChart.Titles.Add("Prices for " + chartData.Columns[0].Caption + " Cars");

            myChart.Series[0].Name = "Price";

            myChart.ChartAreas[0].AxisX.Interval = 1;

            if (chartData.Columns[0].Caption.Contains("Year")) myChart.Series[0].Points.DataBind(enumerableTable, "Make", "Price", "LabelToolTip=ListingID,Tooltip=Model");
            else myChart.Series[0].Points.DataBind(enumerableTable, "Model", "Price", "LabelToolTip=ListingID,Tooltip=Year");

            myChart.Series[0].Sort(System.Windows.Forms.DataVisualization.Charting.PointSortOrder.Ascending);
        }

        private void myChart_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult result = myChart.HitTest(e.X, e.Y);

            if (result.ChartElementType == ChartElementType.DataPoint && ((DataPoint)result.Object).LabelToolTip != null)
            {
                DataRow foundRow = chartData.Rows.Find(((DataPoint)result.Object).LabelToolTip.ToString());
                if (foundRow != null)
                {
                    string url;
                    switch (e.Button)
                    {
                        case MouseButtons.Right:
                            url = Properties.Settings.Default.VIN_LINK.Replace("{VIN}", foundRow.Field<String>("VIN"));
                            break;
                        default:
                            url = Properties.Settings.Default.LISTING_LINK.Replace("{LISTING_ID}", foundRow.Field<Int32>("ListingID").ToString());
                            break;
                    }
                    System.Diagnostics.Process.Start("iexplore.exe", url);
                }
            }
        }
    }
}
