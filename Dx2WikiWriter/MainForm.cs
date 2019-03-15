using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections;
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
            var checkBoxCol = new DataGridViewCheckBoxColumn()
                { Name = "Export", DisplayIndex = 0, AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells };
            dgv.Columns.Add(checkBoxCol);
        }

        //Load our DB
        private void loadBtn_Click(object sender, EventArgs e)
        {
            var folderDir = new CommonOpenFileDialog();
            folderDir.IsFolderPicker = true;
            var result = folderDir.ShowDialog();

            if (result == CommonFileDialogResult.Ok && Directory.Exists(folderDir.FileName))
            {
                //Save folder path for later
                LoadedPath = folderDir.FileName;

                //Load Demons
                var demonDbPath = Path.Combine(folderDir.FileName, "SMT Dx2 Database - Demons.csv");
                if (File.Exists(demonDbPath))
                {
                    demonGrid.DataSource = DBManager.LoadDB(demonDbPath);
                    AddCheckBox(demonGrid);
                }
                else                
                    MessageBox.Show(demonDbPath + ": Could not find file to open.");                
            }
        }
        
        //Selects all from a grid
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem menuItem)
            {
                if (menuItem.Owner is ContextMenuStrip owner)
                {
                    foreach (DataGridViewRow row in (owner.SourceControl as DataGridView).SelectedRows)
                    {
                        row.Cells["Export"].Value = true;
                    }
                }
            }
        }

        private void exportDemonAllBtn_Click(object sender, EventArgs e)
        {
            DBManager.ExportDemons(demonGrid.Rows.Cast<DataGridViewRow>(), true, LoadedPath);
        }

        private void exportIndividualDemonBtn_Click(object sender, EventArgs e)
        {
            var selectedDemons = demonGrid.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Export"].Value != null && (bool)r.Cells["Export"].Value == true);

            DBManager.ExportDemons(selectedDemons, false, LoadedPath);
        }
    }
}