using Microsoft.WindowsAPICodePack.Dialogs;
using System;
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

        #endregion

        #region Events
        
        //Load our DB
        private void loadBtn_Click(object sender, EventArgs e)
        {
            var folderDir = new CommonOpenFileDialog { IsFolderPicker = true };
            var result = folderDir.ShowDialog();

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
                }
                else                
                    MessageBox.Show(demonDbPath + ": Could not find file to open.");

                //Load Skills
                var skillDbPath = Path.Combine(LoadedPath, "SMT Dx2 Database - Skills.csv");
                if (File.Exists(skillDbPath))
                {
                    skillGrid.DataSource = DBManager.LoadDB(skillDbPath);
                    AddCheckBox(skillGrid);
                    skillGrid.Sort(skillGrid.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
                    skillGrid.Columns[1].Frozen = true;
                }
                else
                    MessageBox.Show(skillDbPath + ": Could not find file to open.");


                #region Change To Loaded State

                demonGroupBox.Visible = true;
                skillGroupBox.Visible = true;
                searchGroupBox.Visible = true;
                loadBtn.Enabled = false;
                saveAllBtn.Visible = true;
                exportAllBtn.Visible = true;

                #endregion
            }
        }

        //Saves All DB's
        private void saveAllBtn_Click(object sender, EventArgs e)
        {
            DBManager.SaveDB(demonGrid.DataSource as DataTable, Path.Combine(LoadedPath, "SMT Dx2 Database - Demons.csv"));
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
            SkillHelper.ExportSkills(skillGrid.Rows.Cast<DataGridViewRow>(), demonGrid.Rows.Cast<DataGridViewRow>(), true, LoadedPath);
        }

        //Exports Selected Skills
        private void exportIndividualSkillBtn_Click(object sender, EventArgs e)
        {
            var selectedSkills = skillGrid.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Export"].Value != null && (bool)r.Cells["Export"].Value == true);

            SkillHelper.ExportSkills(selectedSkills, demonGrid.Rows.Cast<DataGridViewRow>(), false, LoadedPath);
        }

        //Exports Everything
        private void exportAllBtn_Click(object sender, EventArgs e)
        {
            exportDemonAllBtn.PerformClick();
            exportIndividualDemonBtn.PerformClick();
            exportSkillAllBtn.PerformClick();
            exportIndividualSkillBtn.PerformClick();
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
            (demonGrid.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
            (skillGrid.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
        }

        #endregion
    }
}