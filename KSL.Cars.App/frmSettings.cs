using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KSL.Cars.App
{
    public partial class frmSettings : Form
    {
        public frmSettings(CarListings.SettingsRow currentSettings)
        {
            InitializeComponent();

            chkKeepSearchParameters.Checked = currentSettings.LoadLastSearchParams;
            chkKeepStatsData.Checked = currentSettings.SaveStats;
            chkSaveLastListings.Checked = currentSettings.SaveSearchResults;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}
