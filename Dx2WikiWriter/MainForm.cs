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
        public DBManager DBManager = new DBManager();
        public string LoadedPath;

        public MainForm()
        {
            InitializeComponent();            
        }

        //Adds a Check Box column to the first slot of a datagridview
        private void AddCheckBox(DataGridView dgv)
        {
            if (dgv.Columns["Export"] != null) return;

            var checkBoxCol = new DataGridViewCheckBoxColumn()
                { Name = "Export", DisplayIndex = 0, AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells };
            dgv.Columns.Add(checkBoxCol);
        }

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
                    demonGrid.Sort(demonGrid.Columns["Name"], System.ComponentModel.ListSortDirection.Ascending);
                    demonGrid.Columns["Name"].Frozen = true;
                    demonGroupBox.Visible = true;
                    searchGroupBox.Visible = true;
                    loadBtn.Enabled = false;
                    saveAllBtn.Visible = true;
                }
                else                
                    MessageBox.Show(demonDbPath + ": Could not find file to open.");                
            }
        }

        private void saveAllBtn_Click(object sender, EventArgs e)
        {
            DBManager.SaveDB(demonGrid.DataSource as DataTable, Path.Combine(LoadedPath, "SMT Dx2 Database - Demons.csv"));
        }

        //Selects all from a grid
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem menuItem)
                if (menuItem.Owner is ContextMenuStrip owner)
                    foreach (DataGridViewRow row in (owner.SourceControl as DataGridView).Rows)
                        row.Cells["Export"].Value = true;
        }

        private void selectNoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem menuItem)
                if (menuItem.Owner is ContextMenuStrip owner)
                    foreach (DataGridViewRow row in (owner.SourceControl as DataGridView).Rows)
                        row.Cells["Export"].Value = false;
        }

        private void exportDemonAllBtn_Click(object sender, EventArgs e)
        {
            DBManager.ExportDemons(demonGrid.Rows.Cast<DataGridViewRow>(), demonGrid.Rows.Cast<DataGridViewRow>(), true, LoadedPath);
        }

        private void exportIndividualDemonBtn_Click(object sender, EventArgs e)
        {
            var selectedDemons = demonGrid.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Export"].Value != null && (bool)r.Cells["Export"].Value == true);

            DBManager.ExportDemons(selectedDemons, demonGrid.Rows.Cast<DataGridViewRow>(), false, LoadedPath);
        }

        private void searchBoxTxt_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchBoxTxt.Text))            
                (demonGrid.DataSource as DataTable).DefaultView.RowFilter = string.Empty;            
            else if (searchBoxTxt.Text.StartsWith(".."))
                (demonGrid.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '%{0}%'", searchBoxTxt.Text.Replace("..", ""));
            else
                (demonGrid.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '{0}%'", searchBoxTxt.Text);
        }

        private void clearSearchBtn_Click(object sender, EventArgs e)
        {
            searchBoxTxt.Text = "";
        }
    }
}