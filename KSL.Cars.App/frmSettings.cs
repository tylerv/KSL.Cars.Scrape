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
            try
            {
                chkKeepSearchParameters.Checked = currentSettings.LoadLastSearchParams;
                chkSaveLastListings.Checked = currentSettings.SaveSearchResults;

                txtSMTPHost.Text = currentSettings.SMTPHost;
                txtPort.Text = currentSettings.PortNumber.ToString();
                txtUsername.Text = currentSettings.Username;
                txtPassword.Text = Encryption.Decrypt(currentSettings.Password);
                txtFrom.Text = currentSettings.FromAddress;
                txtTo.Text = currentSettings.ToAddress;
                chkUseSSL.Checked = currentSettings.UseSSL;
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent("Error loading saved settings: " + ex.Message);
            }
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

    }
}
