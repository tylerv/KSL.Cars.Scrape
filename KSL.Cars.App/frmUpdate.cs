using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KSL.Cars.App
{
    public partial class frmUpdate : Form
    {
        public frmUpdate(Dictionary<Label, Control> items)
        {
            InitializeComponent();

            Version currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            lblCurrentVersion.Text = currentVersion.Major + "." + currentVersion.Minor + "." + currentVersion.Build + "." + currentVersion.Revision;

            int row = 1;

            foreach (Label key in items.Keys)
            {
                Control myItem = items[key];

                //We've only got Labels and LinkLabels, which both have an autosize property.
                myItem.AutoSize = false;
                myItem.Dock = DockStyle.Fill;

                tableDetails.Controls.Add(key, 0, row);
                tableDetails.Controls.Add(myItem, 1, row++);

                if (myItem is LinkLabel)
                {
                    ((LinkLabel)myItem).LinkClicked += new LinkLabelLinkClickedEventHandler(LinkClicked);
                }
            }
        }

        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }

    }
}
