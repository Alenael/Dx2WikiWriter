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
            this.loadBtn = new System.Windows.Forms.Button();
            this.exportDemonAllBtn = new System.Windows.Forms.Button();
            this.exportIndividualDemonBtn = new System.Windows.Forms.Button();
            this.topPanel = new System.Windows.Forms.Panel();
            this.tabPages = new System.Windows.Forms.TabControl();
            this.demonTab = new System.Windows.Forms.TabPage();
            this.demonGrid = new System.Windows.Forms.DataGridView();
            this.skillTab = new System.Windows.Forms.TabPage();
            this.skillGrid = new System.Windows.Forms.DataGridView();
            this.liberatorTab = new System.Windows.Forms.TabPage();
            this.liberatorGrid = new System.Windows.Forms.DataGridView();
            this.demonGroupBox = new System.Windows.Forms.GroupBox();
            this.skillGroup = new System.Windows.Forms.GroupBox();
            this.exportIndividualSkillBtn = new System.Windows.Forms.Button();
            this.exportSkillAllBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.exportAllLiberatorsBtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.allContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topPanel.SuspendLayout();
            this.tabPages.SuspendLayout();
            this.demonTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.demonGrid)).BeginInit();
            this.skillTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skillGrid)).BeginInit();
            this.liberatorTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.liberatorGrid)).BeginInit();
            this.demonGroupBox.SuspendLayout();
            this.skillGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.allContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(12, 12);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(75, 61);
            this.loadBtn.TabIndex = 0;
            this.loadBtn.Text = "Load All";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // exportDemonAllBtn
            // 
            this.exportDemonAllBtn.Location = new System.Drawing.Point(6, 19);
            this.exportDemonAllBtn.Name = "exportDemonAllBtn";
            this.exportDemonAllBtn.Size = new System.Drawing.Size(126, 23);
            this.exportDemonAllBtn.TabIndex = 4;
            this.exportDemonAllBtn.Text = "Export All Demons";
            this.exportDemonAllBtn.UseVisualStyleBackColor = true;
            // 
            // exportIndividualDemonBtn
            // 
            this.exportIndividualDemonBtn.Location = new System.Drawing.Point(138, 19);
            this.exportIndividualDemonBtn.Name = "exportIndividualDemonBtn";
            this.exportIndividualDemonBtn.Size = new System.Drawing.Size(201, 23);
            this.exportIndividualDemonBtn.TabIndex = 5;
            this.exportIndividualDemonBtn.Text = "Export Checked Demons Individually";
            this.exportIndividualDemonBtn.UseVisualStyleBackColor = true;
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.groupBox1);
            this.topPanel.Controls.Add(this.skillGroup);
            this.topPanel.Controls.Add(this.demonGroupBox);
            this.topPanel.Controls.Add(this.loadBtn);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1357, 79);
            this.topPanel.TabIndex = 6;
            // 
            // tabPages
            // 
            this.tabPages.Controls.Add(this.demonTab);
            this.tabPages.Controls.Add(this.skillTab);
            this.tabPages.Controls.Add(this.liberatorTab);
            this.tabPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPages.Location = new System.Drawing.Point(0, 79);
            this.tabPages.Name = "tabPages";
            this.tabPages.SelectedIndex = 0;
            this.tabPages.Size = new System.Drawing.Size(1357, 585);
            this.tabPages.TabIndex = 7;
            // 
            // demonTab
            // 
            this.demonTab.Controls.Add(this.demonGrid);
            this.demonTab.Location = new System.Drawing.Point(4, 22);
            this.demonTab.Name = "demonTab";
            this.demonTab.Padding = new System.Windows.Forms.Padding(3);
            this.demonTab.Size = new System.Drawing.Size(1349, 559);
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
            this.demonGrid.Size = new System.Drawing.Size(1343, 553);
            this.demonGrid.TabIndex = 4;
            // 
            // skillTab
            // 
            this.skillTab.Controls.Add(this.skillGrid);
            this.skillTab.Location = new System.Drawing.Point(4, 22);
            this.skillTab.Name = "skillTab";
            this.skillTab.Padding = new System.Windows.Forms.Padding(3);
            this.skillTab.Size = new System.Drawing.Size(1349, 559);
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
            this.skillGrid.Size = new System.Drawing.Size(1343, 553);
            this.skillGrid.TabIndex = 5;
            // 
            // liberatorTab
            // 
            this.liberatorTab.Controls.Add(this.liberatorGrid);
            this.liberatorTab.Location = new System.Drawing.Point(4, 22);
            this.liberatorTab.Name = "liberatorTab";
            this.liberatorTab.Size = new System.Drawing.Size(1349, 559);
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
            this.liberatorGrid.Size = new System.Drawing.Size(1349, 559);
            this.liberatorGrid.TabIndex = 6;
            // 
            // demonGroupBox
            // 
            this.demonGroupBox.Controls.Add(this.exportIndividualDemonBtn);
            this.demonGroupBox.Controls.Add(this.exportDemonAllBtn);
            this.demonGroupBox.Location = new System.Drawing.Point(202, 12);
            this.demonGroupBox.Name = "demonGroupBox";
            this.demonGroupBox.Size = new System.Drawing.Size(345, 52);
            this.demonGroupBox.TabIndex = 6;
            this.demonGroupBox.TabStop = false;
            this.demonGroupBox.Text = "Demon Export";
            // 
            // skillGroup
            // 
            this.skillGroup.Controls.Add(this.exportIndividualSkillBtn);
            this.skillGroup.Controls.Add(this.exportSkillAllBtn);
            this.skillGroup.Location = new System.Drawing.Point(558, 12);
            this.skillGroup.Name = "skillGroup";
            this.skillGroup.Size = new System.Drawing.Size(345, 52);
            this.skillGroup.TabIndex = 7;
            this.skillGroup.TabStop = false;
            this.skillGroup.Text = "Skill Export";
            // 
            // exportIndividualSkillBtn
            // 
            this.exportIndividualSkillBtn.Location = new System.Drawing.Point(6, 19);
            this.exportIndividualSkillBtn.Name = "exportIndividualSkillBtn";
            this.exportIndividualSkillBtn.Size = new System.Drawing.Size(148, 23);
            this.exportIndividualSkillBtn.TabIndex = 5;
            this.exportIndividualSkillBtn.Text = "Export All Skills";
            this.exportIndividualSkillBtn.UseVisualStyleBackColor = true;
            // 
            // exportSkillAllBtn
            // 
            this.exportSkillAllBtn.Location = new System.Drawing.Point(160, 19);
            this.exportSkillAllBtn.Name = "exportSkillAllBtn";
            this.exportSkillAllBtn.Size = new System.Drawing.Size(179, 23);
            this.exportSkillAllBtn.TabIndex = 4;
            this.exportSkillAllBtn.Text = "Export Checked Skills Individually";
            this.exportSkillAllBtn.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.exportAllLiberatorsBtn);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(909, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 52);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Liberator Export";
            // 
            // exportAllLiberatorsBtn
            // 
            this.exportAllLiberatorsBtn.Location = new System.Drawing.Point(6, 19);
            this.exportAllLiberatorsBtn.Name = "exportAllLiberatorsBtn";
            this.exportAllLiberatorsBtn.Size = new System.Drawing.Size(148, 23);
            this.exportAllLiberatorsBtn.TabIndex = 5;
            this.exportAllLiberatorsBtn.Text = "Export All Liberators";
            this.exportAllLiberatorsBtn.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(160, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(179, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Export Checked Liberators Individually";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // allContextMenu
            // 
            this.allContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem});
            this.allContextMenu.Name = "allContextMenu";
            this.allContextMenu.Size = new System.Drawing.Size(181, 48);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
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
            this.topPanel.ResumeLayout(false);
            this.tabPages.ResumeLayout(false);
            this.demonTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.demonGrid)).EndInit();
            this.skillTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.skillGrid)).EndInit();
            this.liberatorTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.liberatorGrid)).EndInit();
            this.demonGroupBox.ResumeLayout(false);
            this.skillGroup.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.allContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.Button exportDemonAllBtn;
        private System.Windows.Forms.Button exportIndividualDemonBtn;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.TabControl tabPages;
        private System.Windows.Forms.TabPage demonTab;
        private System.Windows.Forms.DataGridView demonGrid;
        private System.Windows.Forms.TabPage skillTab;
        private System.Windows.Forms.DataGridView skillGrid;
        private System.Windows.Forms.TabPage liberatorTab;
        private System.Windows.Forms.DataGridView liberatorGrid;
        private System.Windows.Forms.GroupBox demonGroupBox;
        private System.Windows.Forms.GroupBox skillGroup;
        private System.Windows.Forms.Button exportIndividualSkillBtn;
        private System.Windows.Forms.Button exportSkillAllBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button exportAllLiberatorsBtn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ContextMenuStrip allContextMenu;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
    }
}

