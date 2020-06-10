namespace Dx2WikiWriter
{
    partial class MainForm
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
            this.tabPages = new System.Windows.Forms.TabControl();
            this.demonTab = new System.Windows.Forms.TabPage();
            this.demonGrid = new System.Windows.Forms.DataGridView();
            this.allContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectNoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skillTab = new System.Windows.Forms.TabPage();
            this.skillGrid = new System.Windows.Forms.DataGridView();
            this.logTab = new System.Windows.Forms.TabPage();
            this.logRTB = new System.Windows.Forms.RichTextBox();
            this.loadBtn = new System.Windows.Forms.Button();
            this.demonGroupBox = new System.Windows.Forms.GroupBox();
            this.exportIndividualDemonBtn = new System.Windows.Forms.Button();
            this.exportDemonAllBtn = new System.Windows.Forms.Button();
            this.skillGroupBox = new System.Windows.Forms.GroupBox();
            this.exportIndividualSkillBtn = new System.Windows.Forms.Button();
            this.exportSkillAllBtn = new System.Windows.Forms.Button();
            this.liberatorGroupBox = new System.Windows.Forms.GroupBox();
            this.exportAllLiberatorsBtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.saveAllBtn = new System.Windows.Forms.Button();
            this.searchBoxTxt = new System.Windows.Forms.TextBox();
            this.clearSearchBtn = new System.Windows.Forms.Button();
            this.topPanel = new System.Windows.Forms.Panel();
            this.scrape5News = new System.Windows.Forms.Button();
            this.scrapeAllNews = new System.Windows.Forms.Button();
            this.migratorBtn = new System.Windows.Forms.Button();
            this.loginBtn = new System.Windows.Forms.Button();
            this.retryWikiLoginBtn = new System.Windows.Forms.Button();
            this.uploadToWikiBtn = new System.Windows.Forms.Button();
            this.exportAllBtn = new System.Windows.Forms.Button();
            this.searchGroupBox = new System.Windows.Forms.GroupBox();
            this.scrape1News = new System.Windows.Forms.Button();
            this.tabPages.SuspendLayout();
            this.demonTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.demonGrid)).BeginInit();
            this.allContextMenu.SuspendLayout();
            this.skillTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skillGrid)).BeginInit();
            this.logTab.SuspendLayout();
            this.demonGroupBox.SuspendLayout();
            this.skillGroupBox.SuspendLayout();
            this.liberatorGroupBox.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.searchGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPages
            // 
            this.tabPages.Controls.Add(this.demonTab);
            this.tabPages.Controls.Add(this.skillTab);
            this.tabPages.Controls.Add(this.logTab);
            this.tabPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPages.Location = new System.Drawing.Point(0, 107);
            this.tabPages.Name = "tabPages";
            this.tabPages.SelectedIndex = 0;
            this.tabPages.Size = new System.Drawing.Size(1357, 557);
            this.tabPages.TabIndex = 7;
            this.tabPages.SelectedIndexChanged += new System.EventHandler(this.clearSearchBtn_Click);
            // 
            // demonTab
            // 
            this.demonTab.Controls.Add(this.demonGrid);
            this.demonTab.Location = new System.Drawing.Point(4, 22);
            this.demonTab.Name = "demonTab";
            this.demonTab.Padding = new System.Windows.Forms.Padding(3);
            this.demonTab.Size = new System.Drawing.Size(1349, 531);
            this.demonTab.TabIndex = 0;
            this.demonTab.Text = "Demons";
            this.demonTab.UseVisualStyleBackColor = true;
            // 
            // demonGrid
            // 
            this.demonGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.demonGrid.ContextMenuStrip = this.allContextMenu;
            this.demonGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.demonGrid.Location = new System.Drawing.Point(3, 3);
            this.demonGrid.Name = "demonGrid";
            this.demonGrid.Size = new System.Drawing.Size(1343, 525);
            this.demonGrid.TabIndex = 4;
            this.demonGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.Grid_CellBeginEdit);
            this.demonGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEndEdit);
            // 
            // allContextMenu
            // 
            this.allContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.selectNoneToolStripMenuItem});
            this.allContextMenu.Name = "allContextMenu";
            this.allContextMenu.Size = new System.Drawing.Size(138, 48);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // selectNoneToolStripMenuItem
            // 
            this.selectNoneToolStripMenuItem.Name = "selectNoneToolStripMenuItem";
            this.selectNoneToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.selectNoneToolStripMenuItem.Text = "Select None";
            this.selectNoneToolStripMenuItem.Click += new System.EventHandler(this.selectNoneToolStripMenuItem_Click);
            // 
            // skillTab
            // 
            this.skillTab.Controls.Add(this.skillGrid);
            this.skillTab.Location = new System.Drawing.Point(4, 22);
            this.skillTab.Name = "skillTab";
            this.skillTab.Padding = new System.Windows.Forms.Padding(3);
            this.skillTab.Size = new System.Drawing.Size(1349, 531);
            this.skillTab.TabIndex = 1;
            this.skillTab.Text = "Skills";
            this.skillTab.UseVisualStyleBackColor = true;
            // 
            // skillGrid
            // 
            this.skillGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.skillGrid.ContextMenuStrip = this.allContextMenu;
            this.skillGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skillGrid.Location = new System.Drawing.Point(3, 3);
            this.skillGrid.Name = "skillGrid";
            this.skillGrid.Size = new System.Drawing.Size(1343, 525);
            this.skillGrid.TabIndex = 5;
            this.skillGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.Grid_CellBeginEdit);
            this.skillGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEndEdit);
            // 
            // logTab
            // 
            this.logTab.Controls.Add(this.logRTB);
            this.logTab.Location = new System.Drawing.Point(4, 22);
            this.logTab.Name = "logTab";
            this.logTab.Padding = new System.Windows.Forms.Padding(3);
            this.logTab.Size = new System.Drawing.Size(1349, 531);
            this.logTab.TabIndex = 2;
            this.logTab.Text = "Logger";
            this.logTab.UseVisualStyleBackColor = true;
            // 
            // logRTB
            // 
            this.logRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logRTB.Location = new System.Drawing.Point(3, 3);
            this.logRTB.Name = "logRTB";
            this.logRTB.ReadOnly = true;
            this.logRTB.Size = new System.Drawing.Size(1343, 525);
            this.logRTB.TabIndex = 0;
            this.logRTB.Text = "";
            this.logRTB.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.logRTB_LinkClicked);
            this.logRTB.TextChanged += new System.EventHandler(this.logRTB_TextChanged);
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(12, 12);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(75, 42);
            this.loadBtn.TabIndex = 0;
            this.loadBtn.Text = "Load All";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // demonGroupBox
            // 
            this.demonGroupBox.Controls.Add(this.exportIndividualDemonBtn);
            this.demonGroupBox.Controls.Add(this.exportDemonAllBtn);
            this.demonGroupBox.Location = new System.Drawing.Point(255, 12);
            this.demonGroupBox.Name = "demonGroupBox";
            this.demonGroupBox.Size = new System.Drawing.Size(345, 42);
            this.demonGroupBox.TabIndex = 6;
            this.demonGroupBox.TabStop = false;
            this.demonGroupBox.Text = "Demon Export";
            this.demonGroupBox.Visible = false;
            // 
            // exportIndividualDemonBtn
            // 
            this.exportIndividualDemonBtn.Location = new System.Drawing.Point(138, 13);
            this.exportIndividualDemonBtn.Name = "exportIndividualDemonBtn";
            this.exportIndividualDemonBtn.Size = new System.Drawing.Size(201, 23);
            this.exportIndividualDemonBtn.TabIndex = 5;
            this.exportIndividualDemonBtn.Text = "Export Checked Demons Individually";
            this.exportIndividualDemonBtn.UseVisualStyleBackColor = true;
            this.exportIndividualDemonBtn.Click += new System.EventHandler(this.exportIndividualDemonBtn_Click);
            // 
            // exportDemonAllBtn
            // 
            this.exportDemonAllBtn.Location = new System.Drawing.Point(6, 13);
            this.exportDemonAllBtn.Name = "exportDemonAllBtn";
            this.exportDemonAllBtn.Size = new System.Drawing.Size(126, 23);
            this.exportDemonAllBtn.TabIndex = 4;
            this.exportDemonAllBtn.Text = "Export All Demons";
            this.exportDemonAllBtn.UseVisualStyleBackColor = true;
            this.exportDemonAllBtn.Click += new System.EventHandler(this.exportDemonAllBtn_Click);
            // 
            // skillGroupBox
            // 
            this.skillGroupBox.Controls.Add(this.exportIndividualSkillBtn);
            this.skillGroupBox.Controls.Add(this.exportSkillAllBtn);
            this.skillGroupBox.Location = new System.Drawing.Point(606, 12);
            this.skillGroupBox.Name = "skillGroupBox";
            this.skillGroupBox.Size = new System.Drawing.Size(345, 42);
            this.skillGroupBox.TabIndex = 7;
            this.skillGroupBox.TabStop = false;
            this.skillGroupBox.Text = "Skill Export";
            this.skillGroupBox.Visible = false;
            // 
            // exportIndividualSkillBtn
            // 
            this.exportIndividualSkillBtn.Location = new System.Drawing.Point(6, 13);
            this.exportIndividualSkillBtn.Name = "exportIndividualSkillBtn";
            this.exportIndividualSkillBtn.Size = new System.Drawing.Size(148, 23);
            this.exportIndividualSkillBtn.TabIndex = 5;
            this.exportIndividualSkillBtn.Text = "Export All Skills";
            this.exportIndividualSkillBtn.UseVisualStyleBackColor = true;
            this.exportIndividualSkillBtn.Click += new System.EventHandler(this.exportSkillAllBtn_Click);
            // 
            // exportSkillAllBtn
            // 
            this.exportSkillAllBtn.Location = new System.Drawing.Point(160, 13);
            this.exportSkillAllBtn.Name = "exportSkillAllBtn";
            this.exportSkillAllBtn.Size = new System.Drawing.Size(179, 23);
            this.exportSkillAllBtn.TabIndex = 4;
            this.exportSkillAllBtn.Text = "Export Checked Skills Individually";
            this.exportSkillAllBtn.UseVisualStyleBackColor = true;
            this.exportSkillAllBtn.Click += new System.EventHandler(this.exportIndividualSkillBtn_Click);
            // 
            // liberatorGroupBox
            // 
            this.liberatorGroupBox.Controls.Add(this.exportAllLiberatorsBtn);
            this.liberatorGroupBox.Controls.Add(this.button2);
            this.liberatorGroupBox.Location = new System.Drawing.Point(255, 60);
            this.liberatorGroupBox.Name = "liberatorGroupBox";
            this.liberatorGroupBox.Size = new System.Drawing.Size(345, 42);
            this.liberatorGroupBox.TabIndex = 8;
            this.liberatorGroupBox.TabStop = false;
            this.liberatorGroupBox.Text = "Liberator Export";
            this.liberatorGroupBox.Visible = false;
            // 
            // exportAllLiberatorsBtn
            // 
            this.exportAllLiberatorsBtn.Location = new System.Drawing.Point(6, 13);
            this.exportAllLiberatorsBtn.Name = "exportAllLiberatorsBtn";
            this.exportAllLiberatorsBtn.Size = new System.Drawing.Size(148, 23);
            this.exportAllLiberatorsBtn.TabIndex = 5;
            this.exportAllLiberatorsBtn.Text = "Export All Liberators";
            this.exportAllLiberatorsBtn.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(160, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(179, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Export Checked Liberators Individually";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // saveAllBtn
            // 
            this.saveAllBtn.Location = new System.Drawing.Point(93, 12);
            this.saveAllBtn.Name = "saveAllBtn";
            this.saveAllBtn.Size = new System.Drawing.Size(75, 42);
            this.saveAllBtn.TabIndex = 9;
            this.saveAllBtn.Text = "Save All";
            this.saveAllBtn.UseVisualStyleBackColor = true;
            this.saveAllBtn.Visible = false;
            this.saveAllBtn.Click += new System.EventHandler(this.saveAllBtn_Click);
            // 
            // searchBoxTxt
            // 
            this.searchBoxTxt.Location = new System.Drawing.Point(6, 16);
            this.searchBoxTxt.Name = "searchBoxTxt";
            this.searchBoxTxt.Size = new System.Drawing.Size(157, 20);
            this.searchBoxTxt.TabIndex = 10;
            this.searchBoxTxt.TextChanged += new System.EventHandler(this.searchBoxTxt_TextChanged);
            // 
            // clearSearchBtn
            // 
            this.clearSearchBtn.Location = new System.Drawing.Point(169, 14);
            this.clearSearchBtn.Name = "clearSearchBtn";
            this.clearSearchBtn.Size = new System.Drawing.Size(52, 23);
            this.clearSearchBtn.TabIndex = 12;
            this.clearSearchBtn.Text = "Clear";
            this.clearSearchBtn.UseVisualStyleBackColor = true;
            this.clearSearchBtn.Click += new System.EventHandler(this.clearSearchBtn_Click);
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.scrape1News);
            this.topPanel.Controls.Add(this.scrape5News);
            this.topPanel.Controls.Add(this.scrapeAllNews);
            this.topPanel.Controls.Add(this.migratorBtn);
            this.topPanel.Controls.Add(this.loginBtn);
            this.topPanel.Controls.Add(this.retryWikiLoginBtn);
            this.topPanel.Controls.Add(this.uploadToWikiBtn);
            this.topPanel.Controls.Add(this.exportAllBtn);
            this.topPanel.Controls.Add(this.searchGroupBox);
            this.topPanel.Controls.Add(this.saveAllBtn);
            this.topPanel.Controls.Add(this.liberatorGroupBox);
            this.topPanel.Controls.Add(this.skillGroupBox);
            this.topPanel.Controls.Add(this.demonGroupBox);
            this.topPanel.Controls.Add(this.loadBtn);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1357, 107);
            this.topPanel.TabIndex = 6;
            // 
            // scrape5News
            // 
            this.scrape5News.Location = new System.Drawing.Point(1152, 52);
            this.scrape5News.Name = "scrape5News";
            this.scrape5News.Size = new System.Drawing.Size(93, 48);
            this.scrape5News.TabIndex = 21;
            this.scrape5News.Text = "Scrape First 5 Pages of Dx2 News";
            this.scrape5News.UseVisualStyleBackColor = true;
            this.scrape5News.Visible = false;
            this.scrape5News.Click += new System.EventHandler(this.scrape5News_Click);
            // 
            // scrapeAllNews
            // 
            this.scrapeAllNews.Location = new System.Drawing.Point(1071, 52);
            this.scrapeAllNews.Name = "scrapeAllNews";
            this.scrapeAllNews.Size = new System.Drawing.Size(75, 49);
            this.scrapeAllNews.TabIndex = 20;
            this.scrapeAllNews.Text = "Scrape ALL Dx2 News";
            this.scrapeAllNews.UseVisualStyleBackColor = true;
            this.scrapeAllNews.Visible = false;
            this.scrapeAllNews.Click += new System.EventHandler(this.scrapeNews_Click);
            // 
            // migratorBtn
            // 
            this.migratorBtn.Location = new System.Drawing.Point(978, 52);
            this.migratorBtn.Name = "migratorBtn";
            this.migratorBtn.Size = new System.Drawing.Size(75, 48);
            this.migratorBtn.TabIndex = 19;
            this.migratorBtn.Text = "Migrator";
            this.migratorBtn.UseVisualStyleBackColor = true;
            this.migratorBtn.Visible = false;
            this.migratorBtn.Click += new System.EventHandler(this.migratorBtn_Click);
            // 
            // loginBtn
            // 
            this.loginBtn.Location = new System.Drawing.Point(1071, 6);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(75, 42);
            this.loginBtn.TabIndex = 18;
            this.loginBtn.Text = "Change Password";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // retryWikiLoginBtn
            // 
            this.retryWikiLoginBtn.Location = new System.Drawing.Point(1233, 6);
            this.retryWikiLoginBtn.Name = "retryWikiLoginBtn";
            this.retryWikiLoginBtn.Size = new System.Drawing.Size(75, 42);
            this.retryWikiLoginBtn.TabIndex = 17;
            this.retryWikiLoginBtn.Text = "Retry";
            this.retryWikiLoginBtn.UseVisualStyleBackColor = true;
            this.retryWikiLoginBtn.Visible = false;
            this.retryWikiLoginBtn.Click += new System.EventHandler(this.retryWikiLoginBtn_Click);
            // 
            // uploadToWikiBtn
            // 
            this.uploadToWikiBtn.Enabled = false;
            this.uploadToWikiBtn.Location = new System.Drawing.Point(1152, 6);
            this.uploadToWikiBtn.Name = "uploadToWikiBtn";
            this.uploadToWikiBtn.Size = new System.Drawing.Size(75, 42);
            this.uploadToWikiBtn.TabIndex = 15;
            this.uploadToWikiBtn.Text = "Upload To Wiki";
            this.uploadToWikiBtn.UseVisualStyleBackColor = true;
            this.uploadToWikiBtn.Visible = false;
            this.uploadToWikiBtn.Click += new System.EventHandler(this.uploadToWiki_Click);
            // 
            // exportAllBtn
            // 
            this.exportAllBtn.Location = new System.Drawing.Point(174, 12);
            this.exportAllBtn.Name = "exportAllBtn";
            this.exportAllBtn.Size = new System.Drawing.Size(75, 42);
            this.exportAllBtn.TabIndex = 14;
            this.exportAllBtn.Text = "Export All";
            this.exportAllBtn.UseVisualStyleBackColor = true;
            this.exportAllBtn.Visible = false;
            this.exportAllBtn.Click += new System.EventHandler(this.exportAllBtn_Click);
            // 
            // searchGroupBox
            // 
            this.searchGroupBox.Controls.Add(this.clearSearchBtn);
            this.searchGroupBox.Controls.Add(this.searchBoxTxt);
            this.searchGroupBox.Location = new System.Drawing.Point(12, 60);
            this.searchGroupBox.Name = "searchGroupBox";
            this.searchGroupBox.Size = new System.Drawing.Size(228, 45);
            this.searchGroupBox.TabIndex = 13;
            this.searchGroupBox.TabStop = false;
            this.searchGroupBox.Text = "Search";
            this.searchGroupBox.Visible = false;
            // 
            // scrape1News
            // 
            this.scrape1News.Location = new System.Drawing.Point(1251, 52);
            this.scrape1News.Name = "scrape1News";
            this.scrape1News.Size = new System.Drawing.Size(93, 48);
            this.scrape1News.TabIndex = 22;
            this.scrape1News.Text = "Scrape First Page Only of Dx2 News";
            this.scrape1News.UseVisualStyleBackColor = true;
            this.scrape1News.Visible = false;
            this.scrape1News.Click += new System.EventHandler(this.scrape1News_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1357, 664);
            this.Controls.Add(this.tabPages);
            this.Controls.Add(this.topPanel);
            this.Name = "MainForm";
            this.Text = "Dx2 Wiki Writer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabPages.ResumeLayout(false);
            this.demonTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.demonGrid)).EndInit();
            this.allContextMenu.ResumeLayout(false);
            this.skillTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.skillGrid)).EndInit();
            this.logTab.ResumeLayout(false);
            this.demonGroupBox.ResumeLayout(false);
            this.skillGroupBox.ResumeLayout(false);
            this.liberatorGroupBox.ResumeLayout(false);
            this.topPanel.ResumeLayout(false);
            this.searchGroupBox.ResumeLayout(false);
            this.searchGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabPages;
        private System.Windows.Forms.TabPage demonTab;
        private System.Windows.Forms.DataGridView demonGrid;
        private System.Windows.Forms.TabPage skillTab;
        private System.Windows.Forms.DataGridView skillGrid;
        private System.Windows.Forms.ContextMenuStrip allContextMenu;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectNoneToolStripMenuItem;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.GroupBox demonGroupBox;
        private System.Windows.Forms.Button exportIndividualDemonBtn;
        private System.Windows.Forms.Button exportDemonAllBtn;
        private System.Windows.Forms.GroupBox skillGroupBox;
        private System.Windows.Forms.Button exportIndividualSkillBtn;
        private System.Windows.Forms.Button exportSkillAllBtn;
        private System.Windows.Forms.GroupBox liberatorGroupBox;
        private System.Windows.Forms.Button exportAllLiberatorsBtn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button saveAllBtn;
        private System.Windows.Forms.TextBox searchBoxTxt;
        private System.Windows.Forms.Button clearSearchBtn;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.GroupBox searchGroupBox;
        private System.Windows.Forms.Button exportAllBtn;
        private System.Windows.Forms.Button uploadToWikiBtn;
        private System.Windows.Forms.Button retryWikiLoginBtn;
        private System.Windows.Forms.TabPage logTab;
        private System.Windows.Forms.RichTextBox logRTB;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.Button migratorBtn;
        private System.Windows.Forms.Button scrapeAllNews;
        private System.Windows.Forms.Button scrape5News;
        private System.Windows.Forms.Button scrape1News;
    }
}

