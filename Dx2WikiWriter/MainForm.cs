using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Dx2WikiWriter
{
    public partial class MainForm : Form
    {
        #region Properties

        public DBManager DBManager = new DBManager();
        public string LoadedPath;
        public WikiManager WikiManager;

        #endregion

        #region Constructor

        //Entry way
        public MainForm() => InitializeComponent();

        #endregion

        #region Methods

        //Adds a Check Box column to the first slot of a datagridview
        private void AddCheckBox(DataGridView dgv)
        {
            if (dgv.Columns["Export"] != null) return;

            var checkBoxCol = new DataGridViewCheckBoxColumn()
            { Name = "Export", DisplayIndex = 0, AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells };
            dgv.Columns.Add(checkBoxCol);
        }

        //Clears all files out of a directory
        private void ClearDirectory(string path)
        {
            if (Directory.Exists(path))
                foreach (var file in new DirectoryInfo(path).GetFiles())
                    file.Delete();
        }

        #endregion

        #region Events
        
        //Load our DB
        private void loadBtn_Click(object sender, EventArgs e)
        {
            var folderDir = new CommonOpenFileDialog { IsFolderPicker = true };
            var result = folderDir.ShowDialog();
            var dbStatus = true;

            if (result == CommonFileDialogResult.Ok && Directory.Exists(folderDir.FileName))
            {
                //Save folder path for later
                LoadedPath = folderDir.FileName;

                //Load Demons
                var demonDbPath = Path.Combine(LoadedPath, "SMT Dx2 Database - Demons.csv");
                if (File.Exists(demonDbPath))
                {
                    demonGrid.DataSource = DBManager.LoadDB(demonDbPath);
                    AddCheckBox(demonGrid);
                    demonGrid.Sort(demonGrid.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
                    demonGrid.Columns[0].Frozen = true;
                    ClearDirectory(Path.Combine(LoadedPath, "DemonData"));
                }
                else
                {
                    logRTB.AppendText(demonDbPath + ": Could not find file to open.\n");
                    dbStatus = false;
                }

                //Load Skills
                var skillDbPath = Path.Combine(LoadedPath, "SMT Dx2 Database - Skills.csv");
                if (File.Exists(skillDbPath))
                {
                    skillGrid.DataSource = DBManager.LoadDB(skillDbPath);
                    AddCheckBox(skillGrid);
                    skillGrid.Sort(skillGrid.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
                    skillGrid.Columns[1].Frozen = true;
                    ClearDirectory(Path.Combine(LoadedPath, "SkillData"));
                }
                else
                {
                    logRTB.AppendText(skillDbPath + ": Could not find file to open.\n");
                    dbStatus = false;
                }


                #region Change To Loaded State

                demonGroupBox.Visible = true;
                skillGroupBox.Visible = true;
                searchGroupBox.Visible = true;
                loadBtn.Enabled = false;
                saveAllBtn.Visible = true;
                exportAllBtn.Visible = true;
                uploadToWikiBtn.Enabled = dbStatus;
                
                #endregion
            }
        }

        //Saves All DB's
        private void saveAllBtn_Click(object sender, EventArgs e)
        {
            if (demonGrid.DataSource != null)
                DBManager.SaveDB(demonGrid.DataSource as DataTable, Path.Combine(LoadedPath, "SMT Dx2 Database - Demons.csv"));
            if (skillGrid.DataSource != null)
                DBManager.SaveDB(skillGrid.DataSource as DataTable, Path.Combine(LoadedPath, "SMT Dx2 Database - Skills.csv"));
        }

        //Selects all from a grid
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem menuItem)
                if (menuItem.Owner is ContextMenuStrip owner)
                    foreach (DataGridViewRow row in (owner.SourceControl as DataGridView).Rows)
                        row.Cells["Export"].Value = true;
        }

        //Deselects all from a grid
        private void selectNoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem menuItem)
                if (menuItem.Owner is ContextMenuStrip owner)
                    foreach (DataGridViewRow row in (owner.SourceControl as DataGridView).Rows)
                        row.Cells["Export"].Value = false;
        }

        //Exports all Demons
        private void exportDemonAllBtn_Click(object sender, EventArgs e)
        {
            clearSearchBtn.PerformClick();
            DemonHelper.ExportDemons(demonGrid.Rows.Cast<DataGridViewRow>(), demonGrid.Rows.Cast<DataGridViewRow>(), true, LoadedPath);
        }

        //Exports Selected Demons
        private void exportIndividualDemonBtn_Click(object sender, EventArgs e)
        {
            var selectedDemons = demonGrid.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Export"].Value != null && (bool)r.Cells["Export"].Value == true);

            DemonHelper.ExportDemons(selectedDemons, demonGrid.Rows.Cast<DataGridViewRow>(), false, LoadedPath);
        }
        
        //Exports all Skills
        private void exportSkillAllBtn_Click(object sender, EventArgs e)
        {
            clearSearchBtn.PerformClick();
            var selectedSkills = skillGrid.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value != null);
            SkillHelper.ExportSkills(selectedSkills, demonGrid.Rows.Cast<DataGridViewRow>(), skillGrid.Rows.Cast<DataGridViewRow>(), true, LoadedPath);
        }

        //Exports Selected Skills
        private void exportIndividualSkillBtn_Click(object sender, EventArgs e)
        {
            var selectedSkills = skillGrid.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Export"].Value != null && (bool)r.Cells["Export"].Value == true && r.Cells[0].Value != null).ToList();
            
            SkillHelper.ExportSkills(selectedSkills, demonGrid.Rows.Cast<DataGridViewRow>(), skillGrid.Rows.Cast<DataGridViewRow>(), false, LoadedPath);
        }

        //Exports Everything
        private void exportAllBtn_Click(object sender, EventArgs e)
        {
            exportIndividualDemonBtn.PerformClick();
            exportIndividualSkillBtn.PerformClick();
            exportDemonAllBtn.PerformClick();            
            exportSkillAllBtn.PerformClick();            
        }

        //Show only demons in Search Txt
        private void searchBoxTxt_TextChanged(object sender, EventArgs e)
        {
            switch(tabPages.SelectedIndex)
            {
                case 0:
                    if (string.IsNullOrEmpty(searchBoxTxt.Text))
                        (demonGrid.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                    else if (searchBoxTxt.Text.StartsWith(".."))
                        (demonGrid.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '%{0}%'", searchBoxTxt.Text.Replace("..", ""));
                    else
                        (demonGrid.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '{0}%'", searchBoxTxt.Text);
                    break;
                case 1:
                    if (string.IsNullOrEmpty(searchBoxTxt.Text))
                        (skillGrid.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                    else if (searchBoxTxt.Text.StartsWith(".."))
                        (skillGrid.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '%{0}%'", searchBoxTxt.Text.Replace("..", ""));
                    else
                        (skillGrid.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '{0}%'", searchBoxTxt.Text);
                    break;
            }            
        }

        //Clears the Search Txt
        private void clearSearchBtn_Click(object sender, EventArgs e)
        {
            searchBoxTxt.Text = "";
            if (demonGrid.DataSource != null)
                (demonGrid.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
            if (skillGrid.DataSource != null)
                (skillGrid.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
        }

        //Pushes all files up to the Wiki
        private void uploadToWiki_Click(object sender, EventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            WikiManager.UploadAllFilesAsync(LoadedPath, demonGrid.Rows.Cast<DataGridViewRow>());
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        //On Form Load
        private void MainForm_Load(object sender, EventArgs e)
        {
            WikiManager = new WikiManager(uploadToWikiBtn, logRTB, retryWikiLoginBtn);
        }

        //Create new instance of our Wiki Object
        private void retryWikiLoginBtn_Click(object sender, EventArgs e)
        {
            WikiManager = new WikiManager(uploadToWikiBtn, logRTB, retryWikiLoginBtn);
            retryWikiLoginBtn.Visible = false;
        }

        //Allows easy access to visit site
        private void logRTB_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        //Scroll automatically
        private void logRTB_TextChanged(object sender, EventArgs e)
        {
            logRTB.SelectionStart = logRTB.Text.Length;
            logRTB.ScrollToCaret();
        }

        #endregion
    }
}