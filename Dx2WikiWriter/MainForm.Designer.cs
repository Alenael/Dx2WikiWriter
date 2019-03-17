﻿namespace Dx2WikiWriter
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
            this.liberatorTab = new System.Windows.Forms.TabPage();
            this.liberatorGrid = new System.Windows.Forms.DataGridView();
            this.loadBtn = new System.Windows.Forms.Button();
            this.demonGroupBox = new System.Windows.Forms.GroupBox();
            this.exportDemonAllBtn = new System.Windows.Forms.Button();
            this.exportIndividualDemonBtn = new System.Windows.Forms.Button();
            this.skillGroupBox = new System.Windows.Forms.GroupBox();
            this.exportSkillAllBtn = new System.Windows.Forms.Button();
            this.exportIndividualSkillBtn = new System.Windows.Forms.Button();
            this.liberatorGroupBox = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.exportAllLiberatorsBtn = new System.Windows.Forms.Button();
            this.saveAllBtn = new System.Windows.Forms.Button();
            this.searchBoxTxt = new System.Windows.Forms.TextBox();
            this.clearSearchBtn = new System.Windows.Forms.Button();
            this.topPanel = new System.Windows.Forms.Panel();
            this.searchGroupBox = new System.Windows.Forms.GroupBox();
            this.tabPages.SuspendLayout();
            this.demonTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.demonGrid)).BeginInit();
            this.allContextMenu.SuspendLayout();
            this.skillTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skillGrid)).BeginInit();
            this.liberatorTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.liberatorGrid)).BeginInit();
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
            this.tabPages.Controls.Add(this.liberatorTab);
            this.tabPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPages.Location = new System.Drawing.Point(0, 107);
            this.tabPages.Name = "tabPages";
            this.tabPages.SelectedIndex = 0;
            this.tabPages.Size = new System.Drawing.Size(1357, 557);
            this.tabPages.TabIndex = 7;
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
            this.skillTab.Size = new System.Drawing.Size(1349, 530);
            this.skillTab.TabIndex = 1;
            this.skillTab.Text = "Skills";
            this.skillTab.UseVisualStyleBackColor = true;
            // 
            // skillGrid
            // 
            this.skillGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.skillGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skillGrid.Location = new System.Drawing.Point(3, 3);
            this.skillGrid.Name = "skillGrid";
            this.skillGrid.Size = new System.Drawing.Size(1343, 524);
            this.skillGrid.TabIndex = 5;
            // 
            // liberatorTab
            // 
            this.liberatorTab.Controls.Add(this.liberatorGrid);
            this.liberatorTab.Location = new System.Drawing.Point(4, 22);
            this.liberatorTab.Name = "liberatorTab";
            this.liberatorTab.Size = new System.Drawing.Size(1349, 530);
            this.liberatorTab.TabIndex = 2;
            this.liberatorTab.Text = "Liberators";
            this.liberatorTab.UseVisualStyleBackColor = true;
            // 
            // liberatorGrid
            // 
            this.liberatorGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.liberatorGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.liberatorGrid.Location = new System.Drawing.Point(0, 0);
            this.liberatorGrid.Name = "liberatorGrid";
            this.liberatorGrid.Size = new System.Drawing.Size(1349, 530);
            this.liberatorGrid.TabIndex = 6;
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
            this.demonGroupBox.Location = new System.Drawing.Point(174, 12);
            this.demonGroupBox.Name = "demonGroupBox";
            this.demonGroupBox.Size = new System.Drawing.Size(345, 42);
            this.demonGroupBox.TabIndex = 6;
            this.demonGroupBox.TabStop = false;
            this.demonGroupBox.Text = "Demon Export";
            this.demonGroupBox.Visible = false;
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
            // skillGroupBox
            // 
            this.skillGroupBox.Controls.Add(this.exportIndividualSkillBtn);
            this.skillGroupBox.Controls.Add(this.exportSkillAllBtn);
            this.skillGroupBox.Location = new System.Drawing.Point(525, 12);
            this.skillGroupBox.Name = "skillGroupBox";
            this.skillGroupBox.Size = new System.Drawing.Size(345, 42);
            this.skillGroupBox.TabIndex = 7;
            this.skillGroupBox.TabStop = false;
            this.skillGroupBox.Text = "Skill Export";
            this.skillGroupBox.Visible = false;
            // 
            // exportSkillAllBtn
            // 
            this.exportSkillAllBtn.Location = new System.Drawing.Point(160, 13);
            this.exportSkillAllBtn.Name = "exportSkillAllBtn";
            this.exportSkillAllBtn.Size = new System.Drawing.Size(179, 23);
            this.exportSkillAllBtn.TabIndex = 4;
            this.exportSkillAllBtn.Text = "Export Checked Skills Individually";
            this.exportSkillAllBtn.UseVisualStyleBackColor = true;
            // 
            // exportIndividualSkillBtn
            // 
            this.exportIndividualSkillBtn.Location = new System.Drawing.Point(6, 13);
            this.exportIndividualSkillBtn.Name = "exportIndividualSkillBtn";
            this.exportIndividualSkillBtn.Size = new System.Drawing.Size(148, 23);
            this.exportIndividualSkillBtn.TabIndex = 5;
            this.exportIndividualSkillBtn.Text = "Export All Skills";
            this.exportIndividualSkillBtn.UseVisualStyleBackColor = true;
            // 
            // liberatorGroupBox
            // 
            this.liberatorGroupBox.Controls.Add(this.exportAllLiberatorsBtn);
            this.liberatorGroupBox.Controls.Add(this.button2);
            this.liberatorGroupBox.Location = new System.Drawing.Point(876, 12);
            this.liberatorGroupBox.Name = "liberatorGroupBox";
            this.liberatorGroupBox.Size = new System.Drawing.Size(345, 42);
            this.liberatorGroupBox.TabIndex = 8;
            this.liberatorGroupBox.TabStop = false;
            this.liberatorGroupBox.Text = "Liberator Export";
            this.liberatorGroupBox.Visible = false;
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
            // exportAllLiberatorsBtn
            // 
            this.exportAllLiberatorsBtn.Location = new System.Drawing.Point(6, 13);
            this.exportAllLiberatorsBtn.Name = "exportAllLiberatorsBtn";
            this.exportAllLiberatorsBtn.Size = new System.Drawing.Size(148, 23);
            this.exportAllLiberatorsBtn.TabIndex = 5;
            this.exportAllLiberatorsBtn.Text = "Export All Liberators";
            this.exportAllLiberatorsBtn.UseVisualStyleBackColor = true;
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1357, 664);
            this.Controls.Add(this.tabPages);
            this.Controls.Add(this.topPanel);
            this.Name = "MainForm";
            this.Text = "Dx2 Wiki Writer";
            this.tabPages.ResumeLayout(false);
            this.demonTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.demonGrid)).EndInit();
            this.allContextMenu.ResumeLayout(false);
            this.skillTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.skillGrid)).EndInit();
            this.liberatorTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.liberatorGrid)).EndInit();
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
        private System.Windows.Forms.TabPage liberatorTab;
        private System.Windows.Forms.DataGridView liberatorGrid;
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
    }
}

