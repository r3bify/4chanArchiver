using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace _4chan_Archiver
{
    public partial class frm4ChanMain : Form
    {
        Dictionary<string, DateTime> Threads = new Dictionary<string, DateTime>();
        System.IO.StreamWriter log;
        string oldText;
        NotifyIcon notifyIcon1 = new NotifyIcon();
        public frm4ChanMain()
        {
            InitializeComponent();
            
            string[] parts = Application.ExecutablePath.ToString().Split('\\');
            string exePath = "";
            for (int index = 0; index < (parts.Length - 1); index++)
                exePath += parts[index] + '\\';

            log = new System.IO.StreamWriter(exePath + "4Chan Archiver.log", true);

            log.WriteLine(DateTime.Now + " - Logging started");

            if (System.IO.File.Exists(exePath + "custom.ico"))
            {
                log.WriteLine(DateTime.Now + " - Found custom icon at \"" + exePath + "custom.ico,\" setting..");
                Icon custom = new Icon(exePath + "custom.ico");
                this.Icon = custom;
            }
            else
                log.WriteLine(DateTime.Now + " - No \"custom.ico\" file found, using default.");

            notifyIcon1.Icon = this.Icon;
            notifyIcon1.Visible = true;
            notifyIcon1.Text = this.Text;
            notifyIcon1.ContextMenuStrip = cmsNotify;
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;

            oldText = this.Text;
            timer1.Interval = ((1000 * 60) * 10); // 10 minutes
        }

        /// <summary>
        /// Check for valid thread url
        /// </summary>
        /// <param name="threadURL">thread url to check for</param>
        /// <returns>true or false</returns>
        private bool CheckValidURL(string threadURL)
        {
            try
            {
                log.WriteLine(DateTime.Now + " - Checking if " + threadURL + " is valid..");
                using(WebClient wc = new WebClient())
                    wc.DownloadString(threadURL);
                log.WriteLine(DateTime.Now + " - URL was valid.");
                return true;
            }
            catch
            {
                log.WriteLine(DateTime.Now + " - URL was not valid.");
                return false;
            }
        }

        /// <summary>
        /// Downloads the thread and all images
        /// </summary>
        /// <param name="board">the board letter</param>
        /// <param name="thread">the thread number</param>
        /// <param name="saveDir">where the thread and images get saved to</param>
        /// <param name="fullOrImage">full thread or just images</param>
        private void Download(string board, string thread, string saveDir, string fullOrImage)
        {
            try
            {
                saveDir += board + "\\" + thread + "\\";

                if (!System.IO.Directory.Exists(saveDir))
                {
                    log.WriteLine(DateTime.Now + " - Creating directory " + saveDir);
                    System.IO.Directory.CreateDirectory(saveDir);
                }

                string downloadString = "https://boards.4chan.org/" + board + "/thread/" + thread;
                string downloadRegFull = "\\\"\\/\\/\\w(\\.|\\.\\w\\.)4cdn\\.org\\/\\w+\\/(\\w+|\\w+\\/\\w+|\\w+\\.\\w+)\\.\\w+\\\"";
                string downloadRegImages = "\\\"\\/\\/\\w(\\.|\\.\\w\\.)4cdn\\.org\\/\\w+\\/\\w+\\.\\w+\\\"";
                string threadHTML = null;
                int newFiles = 0;

                using (WebClient wc = new WebClient())
                {
                    log.WriteLine(DateTime.Now + " - Download Request");
                    log.WriteLine(DateTime.Now + " - Fetching thread: " + downloadString);

                    threadHTML = wc.DownloadString(downloadString);
                    MatchCollection allMatchResults;

                    if (fullOrImage.ToLower() == "full")
                    {
                        log.WriteLine(DateTime.Now + " - Downloading full thread.");
                        Regex reg = new Regex(downloadRegFull, RegexOptions.Singleline);

                        log.WriteLine(DateTime.Now + " - Finding files in thread: \"" + board + "/" + thread + "\"");
                        allMatchResults = reg.Matches(threadHTML);
                        log.WriteLine(DateTime.Now + " - Found " + allMatchResults.Count + " matches.");
                        foreach (Match match in allMatchResults)
                        {
                            string filename = match.ToString().Substring(1, match.Length - 2);
                            string[] pieces = filename.Split('/');
                            string prevFileName = null;
                            try
                            {
                                if (prevFileName != filename && !System.IO.File.Exists(saveDir + pieces[pieces.Length - 1]))
                                {
                                    newFiles++;
                                    log.WriteLine(DateTime.Now + " - Downloading file: \"https:" + filename + "\" to \"" + saveDir + pieces[pieces.Length - 1] + "\"");
                                    wc.DownloadFile("https:" + filename, saveDir + pieces[pieces.Length - 1]);
                                    threadHTML = threadHTML.Replace(filename, "./" + pieces[pieces.Length - 1]);
                                }
                            }
                            catch (Exception e)
                            {
                                log.WriteLine(DateTime.Now + " - " + e.Message);
                                MessageBox.Show(e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            prevFileName = filename;
                        }
                        log.WriteLine(DateTime.Now + " - New files: " + newFiles);
                        System.IO.StreamWriter index = new System.IO.StreamWriter(saveDir + @"\index.html");
                        try
                        {
                            log.WriteLine(DateTime.Now + " - writing index.html file");
                            threadHTML = threadHTML.Replace("<div class=\"postContainer", "\n<div class=\"postContainer");
                            threadHTML = threadHTML.Replace("//s.4cdn.org/image", ".");
                            threadHTML = threadHTML.Replace("//s.4cdn.org/css", ".");
                            threadHTML = threadHTML.Replace("//s.4cdn.org/js", ".");
                            index.WriteLine(threadHTML);
                            log.WriteLine(DateTime.Now + " - Finished writing index.html file");
                            index.Close();
                            index.Dispose();
                        }
                        catch (Exception e)
                        {
                            log.WriteLine(DateTime.Now + " - " + e.Message);
                            index.Close();
                            index.Dispose();
                            return;
                        }
                    }
                    else
                    {
                        Regex reg = new Regex(downloadRegImages, RegexOptions.Singleline);

                        log.WriteLine(DateTime.Now + " - Finding images in thread: \"" + board + "/" + thread + "\"");
                        allMatchResults = reg.Matches(threadHTML);
                        log.WriteLine(DateTime.Now + " - Found " + allMatchResults.Count + " matches.");
                        foreach (Match match in allMatchResults)
                        {
                            string filename = match.ToString().Substring(1, match.Length - 2);
                            string[] pieces = filename.Split('/');
                            string prevFileName = null;
                            try
                            {
                                if (prevFileName != filename && !System.IO.File.Exists(saveDir + pieces[pieces.Length - 1]) && !filename.Contains("s") && !filename.Contains("ico"))
                                {
                                    newFiles++;
                                    log.WriteLine(DateTime.Now + " - Downloading image: \" https:" + filename + "\" to \"" + saveDir + pieces[pieces.Length - 1] + "\"");
                                    wc.DownloadFile("https:" + filename, saveDir + pieces[pieces.Length - 1]);
                                }
                            }
                            catch (Exception e)
                            {
                                log.WriteLine(DateTime.Now + " - " + e.Message);
                                return;
                            }
                            prevFileName = filename;
                        }
                    }
                    log.WriteLine(DateTime.Now + " - Download Complete");
                    return;
                }
            }
            catch(Exception e)
            {
                log.WriteLine(DateTime.Now + " - " + e.Message);
                return;
            }
        }

        private void WriteTXT()
        {
            try 
            {
                log.WriteLine(DateTime.Now + " - Creating TXT file");
                System.IO.StreamWriter threads = new System.IO.StreamWriter(txtSaveDir.Text + "threads.txt");
                log.AutoFlush = true;
                try
                {
                    log.WriteLine(DateTime.Now + " - Writing TXT file.");
                    foreach(ListViewItem item in lstThreads.Items)
                        threads.WriteLine(item.Text + "|" + item.SubItems[1].Text + "|" 
                            + item.SubItems[2].Text + "|" + item.SubItems[3].Text);
                    threads.Flush();
                    threads.Close();
                    log.WriteLine(DateTime.Now + " - TXT file written successfully.");
                }
                catch (Exception e)
                {
                    log.WriteLine(DateTime.Now + " - " + e.Message);
                    return;
                }
            }
            catch(Exception e)
            {
                log.WriteLine(DateTime.Now + " - " + e.Message);
                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtSaveDir.Text.Substring(txtSaveDir.Text.Length - 1, 1) != "\\")
                txtSaveDir.Text += '\\';
            if (!System.IO.Directory.Exists(txtSaveDir.Text))
            {
                System.IO.Directory.CreateDirectory(txtSaveDir.Text);
                log.WriteLine(DateTime.Now + " - Creating directory");
            }

            if(txtSaveDir.Enabled)
                txtSaveDir.Enabled = false;
            if(btnBrowse.Enabled)
                btnBrowse.Enabled = false;

            Regex chanURL = new Regex("^(http|https):\\/\\/boards\\.4chan\\.org\\/\\w+\\/thread\\/\\d+");
            Match check = chanURL.Match(txtThreadURL.Text);
            if (CheckValidURL(check.ToString()))
            {
                foreach(ListViewItem item in lstThreads.Items)
                    if (item.Text.Contains(check.ToString())) 
                    {
                        item.SubItems[1].Text = cbFullOrImages.Text;
                        item.SubItems[2].Text = cbFetch.Text;
                        if (cbFetch.Text.ToLower() == "no")
                            item.SubItems[3].Text = "N/A";
                        else
                        {
                            if (!timer1.Enabled)
                                timer1.Start();
                            item.SubItems[3].Text = cbMinutes.Text;
                        }
                        txtThreadURL.Text = "";
                        log.WriteLine(DateTime.Now + " - Successfully modified entry: " + item.Text);
                        WriteTXT();
                        return;
                    }
                ListViewItem row = new ListViewItem(check.ToString());
                row.SubItems.Add(cbFullOrImages.Text);
                if (cbFetch.Text.ToLower() == "yes")
                {
                    row.SubItems.Add(cbFetch.Text);
                    row.SubItems.Add(cbMinutes.Text);
                    if (!timer1.Enabled)
                        timer1.Start();
                }
                else
                {
                    row.SubItems.Add(cbFetch.Text);
                    row.SubItems.Add("N/A");
                }
                Threads.Add(check.ToString(), DateTime.Now);
                lstThreads.Items.Add(row);
                txtThreadURL.Text = "";
                log.WriteLine(DateTime.Now + " - Successfully added thread: " + row.Text);
                WriteTXT();
                return;
            }
            else
                MessageBox.Show("Thread URL is not valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtSaveDir.Text.Substring(txtSaveDir.Text.Length - 1, 1) != "\\")
                txtSaveDir.Text += "\\";
            foreach (ListViewItem item in lstThreads.Items)
                if (item.Selected)
                {
                    string[] pieces = item.Text.Split('/');
                    string dir = txtSaveDir.Text + pieces[pieces.Length - 3] + "\\" + pieces[pieces.Length - 1];
                    if (!System.IO.Directory.Exists(dir))
                        System.IO.Directory.CreateDirectory(dir);
                    log.WriteLine(DateTime.Now + " - Opening folder: " + dir);
                    System.Diagnostics.Process.Start(dir);
                }
        }

        private void cbFetch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFetch.Text.ToLower() != "yes" && cbFetch.Text.ToLower() != "no")
                cbFetch.Text = "No";
            if (cbFetch.Text.ToLower() == "yes")
                cbMinutes.Enabled = true;
            else
                cbMinutes.Enabled = false;
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach(ListViewItem item in lstThreads.Items)
                    if (item.Selected)
                    {
                        log.WriteLine(DateTime.Now + " - Opening thread " + item.Text + " for viewing.");
                        System.Diagnostics.Process.Start(item.Text);
                    }
            }
            catch(Exception ex)
            {
                log.WriteLine(DateTime.Now + " - " + ex.Message);
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbFullOrImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFullOrImages.Text != "Full" && cbFullOrImages.Text != "Images")
                cbFullOrImages.Text = "Full";
        }

        private void lstThreads_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                log.WriteLine(DateTime.Now + " - Opening context menu in thread list.");
                lstThreads.FocusedItem.Bounds.Contains(e.Location);
            }
                
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
                txtSaveDir.Text = folderBrowserDialog1.SelectedPath.ToString();
            if (txtSaveDir.Text.Substring(txtSaveDir.Text.Length - 1, 1) != "\\")
                txtSaveDir.Text += "\\";
            log.WriteLine(DateTime.Now + " - Setting archive path to: " + txtSaveDir.Text);
        }

        private void checkNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtSaveDir.Text.Substring(txtSaveDir.Text.Length - 1, 1) != "\\")
                txtSaveDir.Text += "\\";

            foreach (ListViewItem item in lstThreads.Items)
            {
                if (item.Selected)
                {
                    if (!System.IO.Directory.Exists(txtSaveDir.Text))
                        System.IO.Directory.CreateDirectory(txtSaveDir.Text);
                    
                    string[] pieces = item.Text.Split('/');
                    
                    log.WriteLine(DateTime.Now + " - Manual update requested for: " + item.Text);

                    notifyIcon1.ShowBalloonTip(3000, "Updates", "Checking " + item.Text + " for updates", ToolTipIcon.Info);
                    
                    bwDownloader.DoWork += delegate(object s, DoWorkEventArgs eA)
                    {
                        Download(pieces[3], pieces[5], txtSaveDir.Text, item.SubItems[1].Text);
                    };
                    
                    if(!bwDownloader.IsBusy)
                        bwDownloader.RunWorkerAsync();

                    if (bwDownloader.IsBusy)
                    {
                        this.Text += " Downloading " + pieces[3] + "/" + pieces[5];
                        pictureBox1.Visible = true;
                    }
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstThreads.Items)
                if (item.Selected)
                {
                    if (Threads.ContainsKey(item.Text))
                        Threads.Remove(item.Text);
                    log.WriteLine(DateTime.Now + " - Removed thread: " + item.Text);
                    item.Remove();
                }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            log.WriteLine(DateTime.Now + " - Checking for threads set to auto");
            foreach (ListViewItem thread in lstThreads.Items)
            {
                if (thread.SubItems[2].Text.ToLower() == "yes")
                {
                    log.WriteLine(DateTime.Now + " - Checking " + thread.Text + " for updates.");
                    DateTime lastChecked;
                    if (Threads.TryGetValue(thread.Text, out lastChecked))
                    {
                        TimeSpan check = DateTime.Now - lastChecked;
                        
                        int minutes = Convert.ToInt32(check.Minutes);
                        int interval = Convert.ToInt32(thread.SubItems[3].Text);
                        
                        if (minutes > (interval - 6) && minutes < (interval + 6))
                        {
                            string[] pieces = thread.Text.Split('/');

                            if (txtSaveDir.Text.Substring(txtSaveDir.Text.Length - 1, 1) != "\\")
                                txtSaveDir.Text += "\\";

                            if (!System.IO.Directory.Exists(txtSaveDir.Text))
                            {
                                log.WriteLine(DateTime.Now + " - Creating directory: " + txtSaveDir.Text);
                                System.IO.Directory.CreateDirectory(txtSaveDir.Text);
                            }

                            notifyIcon1.ShowBalloonTip(3000, "Updates", "Checking " + thread.Text + " for updates",ToolTipIcon.Info);

                            bwDownloader.DoWork += delegate (object s, DoWorkEventArgs eA)
                            {
                                Download(pieces[3], pieces[5], txtSaveDir.Text, thread.SubItems[1].Text);
                            };

                            if (!bwDownloader.IsBusy)
                                bwDownloader.RunWorkerAsync();

                            if (bwDownloader.IsBusy)
                            {
                                this.Text += " Downloading " + pieces[3] + "/" + pieces[5];
                                pictureBox1.Visible = true;
                            }
                            
                            Threads[thread.Text] = DateTime.Now;

                        }
                    }
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            ReadTXT();
        }

        private void ReadTXT()
        {

            OpenFileDialog ofdTxtFile = new OpenFileDialog();

            ofdTxtFile.InitialDirectory = txtSaveDir.Text;
            ofdTxtFile.Filter = "txt files (*.txt)|*.txt";
            ofdTxtFile.RestoreDirectory = true;

            log.WriteLine(DateTime.Now + " - looking for TXT file.");

            if (ofdTxtFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (ofdTxtFile.OpenFile() != null)
                    {
                        log.WriteLine(DateTime.Now + " - Found TXT file " + ofdTxtFile.FileName);
                        log.WriteLine(DateTime.Now + " - Loading data from TXT file..");

                        string[] lines = System.IO.File.ReadAllLines(ofdTxtFile.FileName);

                        foreach(string line in lines)
                        {
                            string[] pieces = line.Split('|');
                            ListViewItem row;
                            Regex chanURL = new Regex("^(http|https):\\/\\/boards\\.4chan\\.org\\/\\w+\\/thread\\/\\d+");
                            Match check = chanURL.Match(pieces[0]);
                            if (CheckValidURL(check.ToString()))
                            {
                                log.WriteLine(DateTime.Now + " - TXT File Contained Valid URL: " + check.ToString());
                                row = new ListViewItem(check.ToString());
                            }
                            else
                            {
                                log.WriteLine(DateTime.Now + " - TXT File Contained Invalid URL");
                                return;
                            }

                            if (pieces[1].ToLower() == "full" || pieces[1].ToLower() == "images")
                            {
                                log.WriteLine(DateTime.Now + " - \"" + pieces[1] + "\" Requested");
                                row.SubItems.Add(pieces[1]);
                            }
                            else
                            {
                                log.WriteLine(DateTime.Now + " - TXT File Contained Invalid FullOrImage String");
                                return;
                            }

                            if (pieces[2].ToLower() == "yes" || pieces[2].ToLower() == "no")
                            {
                                log.WriteLine(DateTime.Now + " - Auto Download: " + pieces[2]);
                                row.SubItems.Add(pieces[2]);
                            }
                            else
                            {
                                log.WriteLine(DateTime.Now + " - TXT File Contained Invalid AutoCheck String");
                                return;
                            }
                            if (pieces[3] == "10" || pieces[3] == "20" || pieces[3] == "30" ||
                                pieces[3] == "40" || pieces[3] == "50" || pieces[3] == "60" ||
                                pieces[3] == "70" || pieces[3] == "80" || pieces[3] == "90" ||
                                pieces[3] == "120" || pieces[3] == "150" || pieces[3] == "180" ||
                                pieces[3] == "N/A")
                            {
                                log.WriteLine(DateTime.Now + " - Interval: " + pieces[3] + " Minutes");
                                row.SubItems.Add(pieces[3]);
                            }
                            else
                            {
                                log.WriteLine(DateTime.Now + " - TXT File Contained Invalid UpdateInterval String");
                                return;
                            }
                            lstThreads.Items.Add(row);
                            log.WriteLine(DateTime.Now + " - Row added successfully");
                            Threads.Add(check.ToString(), DateTime.Now);
                        }
                    }
                    DialogResult result = MessageBox.Show("Will you be adding any more threads?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.No)
                    {
                        if(txtSaveDir.Enabled)
                            txtSaveDir.Enabled = false;
                        if(btnBrowse.Enabled)
                            btnBrowse.Enabled = false;
                        if(!timer1.Enabled)
                            timer1.Start();
                    }
                }
                catch (Exception ex)
                {
                    log.WriteLine(DateTime.Now + " - " + ex.Message);
                    return;
                }
            }
         
        }

        private void frm4ChanMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            log.WriteLine(DateTime.Now + " - Logging stopped.");
            log.Close();
            log.Dispose();
            if (notifyIcon1.Visible)
                notifyIcon1.Visible = false;
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                log.WriteLine(DateTime.Now + " - Showing Form.");
                this.Visible = true;
            }
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                log.WriteLine(DateTime.Now + " - Hiding form.");
                this.Visible = false;
                hideToolStripMenuItem.Text = "Show";
            }
            else
            {
                log.WriteLine(DateTime.Now + " - Showing form.");
                this.Visible = true;
                hideToolStripMenuItem.Text = "Hide";
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.WriteLine(DateTime.Now + " - Logging stopped.");
            log.Close();
            log.Dispose();
            if (notifyIcon1.Visible)
                notifyIcon1.Visible = false;
            Application.ExitThread();
        }

        private void cbMinutes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbMinutes.Text == "10" || cbMinutes.Text == "20" || cbMinutes.Text == "30" || 
                cbMinutes.Text == "40" || cbMinutes.Text == "50" || cbMinutes.Text == "60" || 
                cbMinutes.Text == "70" || cbMinutes.Text == "80" || cbMinutes.Text == "90" ||
                cbMinutes.Text == "120" || cbMinutes.Text == "150" || cbMinutes.Text == "180")
            {
                return;
            }
            cbMinutes.Text = "120";
        }

        private void checkNowToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (txtSaveDir.Text.Substring(txtSaveDir.Text.Length - 1, 1) != "\\")
                txtSaveDir.Text += "\\";

            foreach (ListViewItem item in lstThreads.Items)
            {
                if (!System.IO.Directory.Exists(txtSaveDir.Text))
                    System.IO.Directory.CreateDirectory(txtSaveDir.Text);
                
                string[] pieces = item.Text.Split('/');
                log.WriteLine(DateTime.Now + " - Manual update requested for: " + item.Text);
                notifyIcon1.ShowBalloonTip(3000, "Updates", "Checking " + item.Text + " for updates", ToolTipIcon.Info);

                bwDownloader.DoWork += delegate(object s, DoWorkEventArgs eA)
                {
                    Download(pieces[3], pieces[5], txtSaveDir.Text, item.SubItems[1].Text);
                };

                if (!bwDownloader.IsBusy)
                    bwDownloader.RunWorkerAsync();

                if (bwDownloader.IsBusy)
                {
                    this.Text += " Downloading " + pieces[3] + "/" + pieces[5];
                    pictureBox1.Visible = true;
                }
            }
        }

        private void bwDownloader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Text = oldText + " Done";
            System.Threading.Thread.Sleep(500);
            this.Text = oldText;
            pictureBox1.Visible = false;
        }

        private void btnCheckNow_Click(object sender, EventArgs e)
        {
            if (txtSaveDir.Text.Substring(txtSaveDir.Text.Length - 1, 1) != "\\")
                txtSaveDir.Text += "\\";

            foreach (ListViewItem item in lstThreads.Items)
            {
                if (!System.IO.Directory.Exists(txtSaveDir.Text))
                    System.IO.Directory.CreateDirectory(txtSaveDir.Text);

                string[] pieces = item.Text.Split('/');
                log.WriteLine(DateTime.Now + " - Manual update requested for: " + item.Text);
                notifyIcon1.ShowBalloonTip(3000, "Updates", "Checking " + item.Text + " for updates", ToolTipIcon.Info);

                bwDownloader.DoWork += delegate(object s, DoWorkEventArgs eA)
                {
                    Download(pieces[3], pieces[5], txtSaveDir.Text, item.SubItems[1].Text);
                };

                if (!bwDownloader.IsBusy)
                    bwDownloader.RunWorkerAsync();

                if (bwDownloader.IsBusy)
                {
                    this.Text += " Downloading " + pieces[3] + "/" + pieces[5];
                    pictureBox1.Visible = true;
                }
            }
        }

        private void frm4ChanMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (notifyIcon1.Visible)
                notifyIcon1.Visible = false;
        }

        private void notifyIcon1_MouseDoubleClick(Object sender, MouseEventArgs e)
        {
            if (this.Visible)
            {
                log.WriteLine(DateTime.Now + " - Hiding form.");
                this.Visible = false;
                hideToolStripMenuItem.Text = "Show";
            }
            else
            {
                log.WriteLine(DateTime.Now + " - Showing form.");
                this.Visible = true;
                hideToolStripMenuItem.Text = "Hide";
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstThreads.Items)
                if (item.Selected)
                {
                    if (Threads.ContainsKey(item.Text))
                        Threads.Remove(item.Text);
                    log.WriteLine(DateTime.Now + " - Removed thread: " + item.Text);
                    item.Remove();
                }
        }
    }
}
