using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dx2WikiWriter
{
    public partial class MainForm : Form
    {
        public DBManager DBManager = new DBManager();

        private bool _demonsLoaded;
        private bool _skillsLoaded;
        private bool _liberatorsLoaded;

        public MainForm()
        {
            InitializeComponent();            
        }

        //Adds a Check Box column to the first slot of a datagridview
        private void AddCheckBox(DataGridView dgv)
        {
            var checkBoxCol = new DataGridViewCheckBoxColumn() { Name = "Export", DisplayIndex = 0, AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells };
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
                //Load Demons
                var demonDbPath = Path.Combine(folderDir.FileName, "SMT Dx2 Database - Demons.csv");
                if (File.Exists(demonDbPath))
                {
                    demonGrid.DataSource = DBManager.LoadDB(demonDbPath);
                    AddCheckBox(demonGrid);
                    _demonsLoaded = true;
                }

                //Load Skills

                //Load Liberators

            }
        }

        //Save our Db
        private void saveBtn_Click(object sender, EventArgs e)
        {
            DBManager.SaveDB(demonGrid.DataSource);
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
    }
}
