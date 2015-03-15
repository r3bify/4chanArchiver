namespace _4chan_Archiver
{
    partial class frm4ChanMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm4ChanMain));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtThreadURL = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.cmsListThreads = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkNowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cbFetch = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMinutes = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSaveDir = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.cbFullOrImages = new System.Windows.Forms.ComboBox();
            this.lstThreads = new System.Windows.Forms.ListView();
            this.colURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGrab = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAuto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colInterval = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkNowToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bwDownloader = new System.ComponentModel.BackgroundWorker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCheckNow = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.cmsListThreads.SuspendLayout();
            this.cmsNotify.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Thread URL: ";
            // 
            // txtThreadURL
            // 
            this.txtThreadURL.Location = new System.Drawing.Point(89, 9);
            this.txtThreadURL.Name = "txtThreadURL";
            this.txtThreadURL.Size = new System.Drawing.Size(305, 20);
            this.txtThreadURL.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(482, 8);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(35, 22);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // cmsListThreads
            // 
            this.cmsListThreads.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem,
            this.openFolderToolStripMenuItem,
            this.checkNowToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.cmsListThreads.Name = "cmsListThreads";
            this.cmsListThreads.Size = new System.Drawing.Size(140, 92);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.openFolderToolStripMenuItem.Text = "Open Folder";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // checkNowToolStripMenuItem
            // 
            this.checkNowToolStripMenuItem.Name = "checkNowToolStripMenuItem";
            this.checkNowToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.checkNowToolStripMenuItem.Text = "Check Now";
            this.checkNowToolStripMenuItem.Click += new System.EventHandler(this.checkNowToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // cbFetch
            // 
            this.cbFetch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFetch.FormattingEnabled = true;
            this.cbFetch.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cbFetch.Location = new System.Drawing.Point(89, 68);
            this.cbFetch.MaxLength = 3;
            this.cbFetch.Name = "cbFetch";
            this.cbFetch.Size = new System.Drawing.Size(50, 22);
            this.cbFetch.TabIndex = 5;
            this.cbFetch.Text = "No";
            this.cbFetch.SelectedIndexChanged += new System.EventHandler(this.cbFetch_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "Auto Fetch: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(150, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 14);
            this.label3.TabIndex = 7;
            this.label3.Text = "Time: ";
            // 
            // cbMinutes
            // 
            this.cbMinutes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbMinutes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbMinutes.Enabled = false;
            this.cbMinutes.FormatString = "N0";
            this.cbMinutes.FormattingEnabled = true;
            this.cbMinutes.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50",
            "60",
            "70",
            "80",
            "90",
            "120",
            "150",
            "180"});
            this.cbMinutes.Location = new System.Drawing.Point(195, 68);
            this.cbMinutes.MaxLength = 3;
            this.cbMinutes.Name = "cbMinutes";
            this.cbMinutes.Size = new System.Drawing.Size(43, 22);
            this.cbMinutes.TabIndex = 8;
            this.cbMinutes.Text = "120";
            this.cbMinutes.SelectedIndexChanged += new System.EventHandler(this.cbMinutes_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(244, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 14);
            this.label4.TabIndex = 9;
            this.label4.Text = "minutes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 14);
            this.label5.TabIndex = 10;
            this.label5.Text = "Save Directory: ";
            // 
            // txtSaveDir
            // 
            this.txtSaveDir.AllowDrop = true;
            this.txtSaveDir.Location = new System.Drawing.Point(89, 37);
            this.txtSaveDir.Name = "txtSaveDir";
            this.txtSaveDir.Size = new System.Drawing.Size(305, 20);
            this.txtSaveDir.TabIndex = 11;
            this.txtSaveDir.Text = "C:\\4Chan Archives";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(400, 36);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(76, 23);
            this.btnBrowse.TabIndex = 12;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(482, 67);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 13;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // cbFullOrImages
            // 
            this.cbFullOrImages.FormattingEnabled = true;
            this.cbFullOrImages.Items.AddRange(new object[] {
            "Full",
            "Images"});
            this.cbFullOrImages.Location = new System.Drawing.Point(400, 9);
            this.cbFullOrImages.MaxDropDownItems = 2;
            this.cbFullOrImages.Name = "cbFullOrImages";
            this.cbFullOrImages.Size = new System.Drawing.Size(76, 22);
            this.cbFullOrImages.TabIndex = 14;
            this.cbFullOrImages.Text = "Full";
            this.cbFullOrImages.SelectedIndexChanged += new System.EventHandler(this.cbFullOrImages_SelectedIndexChanged);
            // 
            // lstThreads
            // 
            this.lstThreads.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colURL,
            this.colGrab,
            this.colAuto,
            this.colInterval});
            this.lstThreads.ContextMenuStrip = this.cmsListThreads;
            this.lstThreads.Location = new System.Drawing.Point(15, 96);
            this.lstThreads.Name = "lstThreads";
            this.lstThreads.Size = new System.Drawing.Size(542, 185);
            this.lstThreads.TabIndex = 15;
            this.lstThreads.UseCompatibleStateImageBehavior = false;
            this.lstThreads.View = System.Windows.Forms.View.Details;
            // 
            // colURL
            // 
            this.colURL.Text = "Thread";
            this.colURL.Width = 388;
            // 
            // colGrab
            // 
            this.colGrab.Text = "Grab";
            this.colGrab.Width = 49;
            // 
            // colAuto
            // 
            this.colAuto.Text = "Auto";
            this.colAuto.Width = 39;
            // 
            // colInterval
            // 
            this.colInterval.Text = "Interval";
            // 
            // cmsNotify
            // 
            this.cmsNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideToolStripMenuItem,
            this.checkNowToolStripMenuItem1,
            this.quitToolStripMenuItem});
            this.cmsNotify.Name = "cmsNotify";
            this.cmsNotify.Size = new System.Drawing.Size(136, 70);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            // 
            // checkNowToolStripMenuItem1
            // 
            this.checkNowToolStripMenuItem1.Name = "checkNowToolStripMenuItem1";
            this.checkNowToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
            this.checkNowToolStripMenuItem1.Text = "Check Now";
            this.checkNowToolStripMenuItem1.Click += new System.EventHandler(this.checkNowToolStripMenuItem1_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // bwDownloader
            // 
            this.bwDownloader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwDownloader_RunWorkerCompleted);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(453, 68);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // btnCheckNow
            // 
            this.btnCheckNow.Location = new System.Drawing.Point(482, 36);
            this.btnCheckNow.Name = "btnCheckNow";
            this.btnCheckNow.Size = new System.Drawing.Size(75, 23);
            this.btnCheckNow.TabIndex = 17;
            this.btnCheckNow.Text = "Check Now";
            this.btnCheckNow.UseVisualStyleBackColor = true;
            this.btnCheckNow.Click += new System.EventHandler(this.btnCheckNow_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(522, 8);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(35, 22);
            this.btnRemove.TabIndex = 18;
            this.btnRemove.Text = "-";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // frm4ChanMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 292);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnCheckNow);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lstThreads);
            this.Controls.Add(this.cbFullOrImages);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtSaveDir);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbMinutes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbFetch);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtThreadURL);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frm4ChanMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "R3bify\'s 4chan Archiver";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm4ChanMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frm4ChanMain_FormClosed);
            this.cmsListThreads.ResumeLayout(false);
            this.cmsNotify.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtThreadURL;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ContextMenuStrip cmsListThreads;
        private System.Windows.Forms.ToolStripMenuItem checkNowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbFetch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbMinutes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSaveDir;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ComboBox cbFullOrImages;
        private System.Windows.Forms.ListView lstThreads;
        private System.Windows.Forms.ColumnHeader colURL;
        private System.Windows.Forms.ColumnHeader colGrab;
        private System.Windows.Forms.ColumnHeader colAuto;
        private System.Windows.Forms.ColumnHeader colInterval;
        private System.Windows.Forms.ContextMenuStrip cmsNotify;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkNowToolStripMenuItem1;
        private System.ComponentModel.BackgroundWorker bwDownloader;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCheckNow;
        private System.Windows.Forms.Button btnRemove;
    }
}

