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

namespace C.Scrape
{
    public partial class Graph : Form
    {
        DataTable chartData;
        public Graph(DataTable dataIn)
        {
            InitializeComponent();

            chartData = dataIn;

            foreach (DataRow row in chartData.Rows) row["VIN"] = Properties.Settings.Default.VIN_LINK.Replace("{VIN}", row["VIN"].ToString());

            var enumerableTable = (chartData as System.ComponentModel.IListSource).GetList();
            myChart.Titles.Add("Prices for " + chartData.Columns[0].Caption + " Cars");

            myChart.Series[0].Name = "Price";

            myChart.ChartAreas[0].AxisX.Interval = 1;

            if (chartData.Columns[0].Caption.Contains("Year")) myChart.Series[0].Points.DataBind(enumerableTable, "Make", "Price", "LabelToolTip=Link,CustomProperties=VIN,Tooltip=Model");
            else myChart.Series[0].Points.DataBind(enumerableTable, "Model", "Price", "LabelToolTip=Link,CustomProperties=VIN,Tooltip=Year");
            
            myChart.Series[0].Sort(System.Windows.Forms.DataVisualization.Charting.PointSortOrder.Ascending);
        }

        private void myChart_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult result = myChart.HitTest(e.X, e.Y);

            if (result.ChartElementType == ChartElementType.DataPoint && ((DataPoint)result.Object).LabelToolTip != null)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        System.Diagnostics.Process.Start("iexplore.exe", ((DataPoint)result.Object).LabelToolTip.ToString());
                        break;
                    case MouseButtons.Right:
                        string url = ((DataPoint)result.Object).CustomProperties.Split('=')[1].ToString();
                        System.Diagnostics.Process.Start("iexplore.exe", url);
                        break;
                }
            }
        }
    }
}
