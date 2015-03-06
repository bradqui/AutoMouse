using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Deployment.Application;

namespace AutoMouse
{
    public partial class MainForm : Form
    {

        #region Variables and Constructor

        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private int msgNumber = -1;
        private int progMax = 0;

        Icon enabledIcon = new Icon("automouse_green.ico");
        Icon disabledIcon = new Icon("automouse_red.ico");

        public MainForm()
        {
            InitializeComponent();
            this.Resize += new EventHandler(form1_Resize);
        }

        #endregion

        #region Methods

        private void errorMessage(string message)
        {
            MessageBox.Show(message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                minimizeToTray();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblTimeLeft.Text = "";
            timer2.Interval = 1000;
            progressBar1.Maximum = (int)(this.numericUpDown1.Value + progMax);
            progressBar1.Minimum = 0;
            timer2.Tick += new EventHandler(IncreaseProgressBar);
            timer2.Enabled = true;
            timer1.Interval = (int)((this.numericUpDown1.Value) * 1000);
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(timer1_Tick);
            NotificationIcon1.Visible = false;
        }

        private void IncreaseProgressBar(object sender, EventArgs e)
        {
            progressBar1.Increment(1);
            if (progressBar1.Value != numericUpDown1.Value)
            {
                lblTimeLeft.Text = String.Format("{0} seconds left", (numericUpDown1.Value - progressBar1.Value) + msgNumber);
            }
        }

        private void StartAutoMousing()
        {
            timer2.Enabled = true;
            timer1.Enabled = true;
            lblTimeLeft.Text = String.Format("{0} seconds left", (numericUpDown1.Value - progressBar1.Value) + msgNumber);
            checkEnabled.Checked = true;
            enabledToolStripMenuItem.Checked = true;
            string TTtext = string.Format("AutoMouse running every {0} seconds.", numericUpDown1.Value);
            NotificationIcon1.Text = TTtext;
            NotificationIcon1.Icon = enabledIcon;
        }

        private void StopAutoMousing()
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            lblTimeLeft.Text = "";
            progressBar1.Value = progressBar1.Minimum;
            checkEnabled.Checked = false;
            enabledToolStripMenuItem.Checked = false;
            string TTtext = string.Format("AutoMouse is currently disabled.", numericUpDown1.Value);
            NotificationIcon1.Text = TTtext;
            NotificationIcon1.Icon = disabledIcon;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.timer1.Interval = (int)(this.numericUpDown1.Value * 1000);
            progressBar1.Maximum = (int)(this.numericUpDown1.Value + progMax);
            progressBar1.Value = progressBar1.Minimum;
            lblTimeLeft.Text = String.Format("{0} seconds left", (numericUpDown1.Value - progressBar1.Value) + msgNumber);

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            //lblTimeLeft.Text = "Zero seconds left";
            progressBar1.Value = (int)numericUpDown1.Value;
            lblTimeLeft.Text = String.Format("zero");
            mouse_event(MOUSEEVENTF_MOVE, 5, 5, 0, 0);
            Thread.Sleep(1000);
            timer1.Start();
            timer2.Start();
            progressBar1.Value = progressBar1.Minimum;
            lblTimeLeft.Text = String.Format("{0} seconds left", (numericUpDown1.Value - progressBar1.Value) + msgNumber);
            mouse_event(MOUSEEVENTF_MOVE, -5, -5, 0, 0);

            if (this.checkBox1.Checked)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            }
        }

        private void minimizeToTray()
        {
            NotificationIcon1.Visible = true;
            NotificationIcon1.ShowBalloonTip(20,"Information","AutoMouse is now running in the system tray.  Double-click to open, Right-click to access the context menu.", ToolTipIcon.Info);
            string TTtext;
            if (timer1.Enabled == false)
            {
                TTtext = "AutoMouse is currently disabled";
            }
            else
            {
                TTtext = string.Format("AutoMouse running every {0} seconds.", numericUpDown1.Value);
            }
            NotificationIcon1.Text = TTtext;
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        #endregion

        #region ButtonClicks

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
        }

        private void NotificationIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            NotificationIcon1.Visible = false;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            minimizeToTray();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 theAboutBox = new AboutBox1();
            theAboutBox.ShowDialog();
        }

        private void directionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Directions theDirections = new Directions();
            theDirections.ShowDialog();
        }

        private void minimizeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            minimizeToTray();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void maximizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            NotificationIcon1.Visible = false;
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void enabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (enabledToolStripMenuItem.Checked)
            {
                StartAutoMousing();
                //NotificationIcon1.ShowBalloonTip(20, "Information", "AutoMouse is now running...", ToolTipIcon.Info);
            }
            else
            {
                StopAutoMousing();
                //NotificationIcon1.ShowBalloonTip(20, "Information", "AutoMouse is now stopped...", ToolTipIcon.Info);
            }
        }

        private void checkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEnabled.Checked)
            {
                StartAutoMousing();
            }
            else
            {
                StopAutoMousing();
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region ManualUpdate - InstallUpdateSyncWithInfo()

        private void InstallUpdateSyncWithInfo()
        {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde)
                {
                    errorMessage("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    errorMessage("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    errorMessage("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    Boolean doUpdate = true;

                    if (!info.IsUpdateRequired)
                    {
                        DialogResult dr = MessageBox.Show("An update is available. Would you like to update the application now?", "Update Available", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (!(DialogResult.OK == dr))
                        {
                            doUpdate = false;
                        }
                    }
                    else
                    {
                        // Display a message that the app MUST reboot. Display the minimum required version.
                        MessageBox.Show("This application has detected a mandatory update from your current " +
                            "version to version " + info.MinimumRequiredVersion.ToString() +
                            ". The application will now install the update and restart.",
                            "Update Available", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    if (doUpdate)
                    {
                        try
                        {
                            ad.Update();
                            MessageBox.Show("The application has been upgraded, and will now restart.", "Application Restart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Application.Restart();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            errorMessage("Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + dde);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("AutoMouse is up-to-date.", "No update available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                errorMessage("Application is not network deployed.  Is this a test/debug build?  If not, then something crazy is going on...");
                return;
            }
        }
        #endregion

    }
}
