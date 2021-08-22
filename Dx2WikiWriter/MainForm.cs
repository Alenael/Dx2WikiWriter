using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Dx2WikiWriter
{
    public partial class MainForm : Form
    {
        #region Properties

        public DBManager DBManager = new DBManager();
        public string LoadedPath;
        public WikiManager WikiManager;
        public NewsScrapper NewsScrapper;

        public bool isDirty = false;
        private string beginValue;

        public DataTable NewsDb;

        #endregion

        #region Constructor

        //Entry way
        public MainForm() => InitializeComponent();

        #endregion

        #region Methods

        public void AppendTextBox(string value)
        {
            if(InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(AppendTextBox), new object[] {value});
                return;
            }
            value = GetTime() + value;
            logRTB.Text += value;
        }

        public void SetTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetTextBox), new object[] { value });
                return;
            }
            value = GetTime() + value;
            logRTB.Text = value;
        }

        public string GetTime()
        {
            var dateTime = DateTime.Now;
            return $"[{dateTime.ToString("MM-dd-yy hh:mm:ss")}] ";
        }

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

        public Dictionary<string, News> LoadNewsDataTable(DataTable dataTable)
        {
            var loadedDb = new Dictionary<string, News>();

            foreach(DataRow row in dataTable.Rows)
            {
                var news = new News();
                news.LoadByDataRow(row);
            }

            return loadedDb;
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
                }
                else
                {
                    logRTB.AppendText(skillDbPath + ": Could not find file to open.\n");
                    dbStatus = false;
                }

                //Load Swords
                var swordDbPath = Path.Combine(LoadedPath, "SMT Dx2 Database - Swords.csv");
                if (File.Exists(swordDbPath))
                {
                    swordGrid.DataSource = DBManager.LoadDB(swordDbPath);
                    AddCheckBox(swordGrid);
                    swordGrid.Sort(swordGrid.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
                    swordGrid.Columns[1].Frozen = true;
                }
                else
                {
                    logRTB.AppendText(swordDbPath + ": Could not find file to open.\n");
                    dbStatus = false;
                }

                //Load Shields
                var shieldDbPath = Path.Combine(LoadedPath, "SMT Dx2 Database - Shields.csv");
                if (File.Exists(shieldDbPath))
                {
                    shieldGrid.DataSource = DBManager.LoadDB(shieldDbPath);
                    AddCheckBox(shieldGrid);
                    shieldGrid.Sort(shieldGrid.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
                    shieldGrid.Columns[1].Frozen = true;
                }
                else
                {
                    logRTB.AppendText(shieldDbPath + ": Could not find file to open.\n");
                    dbStatus = false;
                }

                //Load Shields
                var ArmSkillDbPath = Path.Combine(LoadedPath, "SMT Dx2 Database - Armaments Skills.csv");
                if (File.Exists(ArmSkillDbPath))
                {
                    ArmSkillsGrid.DataSource = DBManager.LoadDB(ArmSkillDbPath);
                    AddCheckBox(ArmSkillsGrid);
                    ArmSkillsGrid.Sort(ArmSkillsGrid.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
                    ArmSkillsGrid.Columns[1].Frozen = true;
                }
                else
                {
                    logRTB.AppendText(ArmSkillDbPath + ": Could not find file to open.\n");
                    dbStatus = false;
                }

                //Load News
                var newsDbPath = Path.Combine(LoadedPath, "SMT Dx2 Database - News.csv");
                if (File.Exists(newsDbPath))
                {
                    NewsDb = DBManager.LoadDB(newsDbPath);
                }
                else
                {
                    logRTB.AppendText(newsDbPath + ": Could not find file to open.\n");
                    dbStatus = false;
                }

                #region Change To Loaded State

                demonGroupBox.Visible = true;
                skillGroupBox.Visible = true;
                searchGroupBox.Visible = true;
                swordGroupBox.Visible = true;
                shieldGroupBox.Visible = true;
                armSkillGroup.Visible = true;
                selectAllBtn.Visible = true;
                migratorBtn.Visible = true;
                loadBtn.Enabled = false;
                saveAllBtn.Visible = true;
                exportAllBtn.Visible = true;
                scrape1News.Visible = true;
                scrape5News.Visible = true;
                scrapeAllNews.Visible = true;
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
            if (swordGrid.DataSource != null)
                DBManager.SaveDB(swordGrid.DataSource as DataTable, Path.Combine(LoadedPath, "SMT Dx2 Database - Swords.csv"));
            if (shieldGrid.DataSource != null)
                DBManager.SaveDB(shieldGrid.DataSource as DataTable, Path.Combine(LoadedPath, "SMT Dx2 Database - Shields.csv"));
            if (ArmSkillsGrid.DataSource != null)
                DBManager.SaveDB(ArmSkillsGrid.DataSource as DataTable, Path.Combine(LoadedPath, "SMT Dx2 Database - Armaments Skills.csv"));

            Text = "Dx2 Wiki Writer";
            isDirty = false;
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

        private void selectAllGrids_Click(object sender, EventArgs e)
        {

            tabPages.SelectedTab = skillTab;
            foreach (DataGridViewRow row in skillGrid.Rows)
                row.Cells["Export"].Value = true;

            tabPages.SelectedTab = shieldTab;
            foreach (DataGridViewRow row in shieldGrid.Rows)
                row.Cells["Export"].Value = true;

            tabPages.SelectedTab = swordTab;
            foreach (DataGridViewRow row in swordGrid.Rows)
                row.Cells["Export"].Value = true;

            tabPages.SelectedTab = armSkillTab;
            foreach (DataGridViewRow row in ArmSkillsGrid.Rows)
                row.Cells["Export"].Value = true;

            tabPages.SelectedTab = demonTab;

            foreach (DataGridViewRow row in demonGrid.Rows)
                row.Cells["Export"].Value = true;
        }

        //Exports all Demons
        private void exportDemonAllBtn_Click(object sender, EventArgs e)
        {
            var ranks = DemonHelper.GetRanks(demonGrid.Rows.Cast<DataGridViewRow>());
            DemonHelper.ExportDemons(demonGrid.Rows.Cast<DataGridViewRow>(), ranks, demonGrid.Rows.Cast<DataGridViewRow>(), true, LoadedPath);
        }

        //Exports Selected Demons
        private void exportIndividualDemonBtn_Click(object sender, EventArgs e)
        {
            var ranks = getRanks();
            
            var selectedDemons = demonGrid.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Export"].Value != null && (bool)r.Cells["Export"].Value == true);

            DemonHelper.ExportDemons(selectedDemons, ranks, demonGrid.Rows.Cast<DataGridViewRow>(), false, LoadedPath);
        }
        
        //Exports all Skills
        private void exportSkillAllBtn_Click(object sender, EventArgs e)
        {
            var selectedSkills = skillGrid.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value != null);
            SkillHelper.ExportSkills(selectedSkills, demonGrid.Rows.Cast<DataGridViewRow>(), skillGrid.Rows.Cast<DataGridViewRow>(), true, LoadedPath);
        }

        //Exports Selected Skills
        private void exportIndividualSkillBtn_Click(object sender, EventArgs e)
        {
            var selectedSkills = skillGrid.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Export"].Value != null && (bool)r.Cells["Export"].Value == true && r.Cells[0].Value != null).ToList();
            
            SkillHelper.ExportSkills(selectedSkills, demonGrid.Rows.Cast<DataGridViewRow>(), skillGrid.Rows.Cast<DataGridViewRow>(), false, LoadedPath);
        }

        //Exports all Swords
        private void exportSwordAllBtn_Click(object sender, EventArgs e)
        {
            SwordHelper.ExportSwords(swordGrid.Rows.Cast<DataGridViewRow>(), swordGrid.Rows.Cast<DataGridViewRow>(), true, LoadedPath);
        }

        //Exports Selected Swords
        private void exportIndividualSwordBtn_Click(object sender, EventArgs e)
        {
            var selectedSwords = swordGrid.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Export"].Value != null && (bool)r.Cells["Export"].Value == true);
            SwordHelper.ExportSwords(selectedSwords, swordGrid.Rows.Cast<DataGridViewRow>(), false, LoadedPath);
        }

        //Exports all Shields
        private void exportShieldAllBtn_Click(object sender, EventArgs e)
        {
            ShieldHelper.ExportShields(shieldGrid.Rows.Cast<DataGridViewRow>(), shieldGrid.Rows.Cast<DataGridViewRow>(), true, LoadedPath);
        }

        //Exports Selected Swords
        private void exportIndividualShieldBtn_Click(object sender, EventArgs e)
        {
            var selectedShields = shieldGrid.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Export"].Value != null && (bool)r.Cells["Export"].Value == true);
            ShieldHelper.ExportShields(selectedShields, shieldGrid.Rows.Cast<DataGridViewRow>(), false, LoadedPath);
        }

        //Exports all Arm Skills
        private void exportArmSkillsAllBtn_Click(object sender, EventArgs e)
        {
            ArmSkillsHelper.ExportArmSkillss(ArmSkillsGrid.Rows.Cast<DataGridViewRow>(), ArmSkillsGrid.Rows.Cast<DataGridViewRow>(), true, LoadedPath);
        }

        //Exports Selected Arm Skills
        private void exportIndividualArmSkillsBtn_Click(object sender, EventArgs e)
        {
            var selectedArmSkills = ArmSkillsGrid.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Export"].Value != null && (bool)r.Cells["Export"].Value == true);
            ArmSkillsHelper.ExportArmSkillss(selectedArmSkills, demonGrid.Rows.Cast<DataGridViewRow>(), false, LoadedPath);
        }

        //Exports Everything
        private void exportAllBtn_Click(object sender, EventArgs e)
        {
            clearSearchBtn.PerformClick();
            exportIndividualDemonBtn.PerformClick();
            exportIndividualSkillBtn.PerformClick();
            exportIndividualSwordsBtn.PerformClick();
            exportIndividualShieldsBtn.PerformClick();
            exportIndividualArmSkillsBtn.PerformClick();
            exportDemonAllBtn.PerformClick();
            exportSkillAllBtn.PerformClick();
            exportAllSwordsBtn.PerformClick();
            exportAllShieldsBtn.PerformClick();
            exportAllArmSkillsBtn.PerformClick();
        }

        List<string> demonState;
        List<string> skillState;
        List<string> shieldState;
        List<string> swordState;
        List<string> armSkillState;

        private List<Rank> getRanks()
        {
            saveExportState();
            clearSearchBtn.PerformClick();
            restoreExportState();
            return DemonHelper.GetRanks(demonGrid.Rows.Cast<DataGridViewRow>());
        }

        private void saveExportState()
        {
            demonState = new List<string>();
            foreach (var d in demonGrid.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Export"].Value != null && (bool)r.Cells["Export"].Value == true).ToList())
                demonState.Add((string)d.Cells[0].Value);

            skillState = new List<string>();
            foreach (var s in skillGrid.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Export"].Value != null && (bool)r.Cells["Export"].Value == true && r.Cells[0].Value != null).ToList())
                skillState.Add((string)s.Cells[1].Value);

            shieldState = new List<string>();
            foreach (var s in shieldGrid.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Export"].Value != null && (bool)r.Cells["Export"].Value == true && r.Cells[0].Value != null).ToList())
                shieldState.Add((string)s.Cells[1].Value);

            swordState = new List<string>();
            foreach (var s in swordGrid.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Export"].Value != null && (bool)r.Cells["Export"].Value == true && r.Cells[0].Value != null).ToList())
                swordState.Add((string)s.Cells[1].Value);

            armSkillState = new List<string>();
            foreach (var s in ArmSkillsGrid.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Export"].Value != null && (bool)r.Cells["Export"].Value == true && r.Cells[0].Value != null).ToList())
                armSkillState.Add((string)s.Cells[1].Value);
        }

        private void restoreExportState()
        {
            foreach(var dgr in demonGrid.Rows.Cast<DataGridViewRow>().ToList())            
                foreach(var demon in demonState)                
                    if (dgr.Cells[0].Value != null && demon != null)                    
                        if ((string)dgr.Cells[1].Value == demon)                        
                            dgr.Cells["Export"].Value = true;

            foreach (var dgr in skillGrid.Rows.Cast<DataGridViewRow>().ToList())
                foreach (var skill in skillState)
                    if (dgr.Cells[0].Value != null && skill != null)
                        if ((string)dgr.Cells[1].Value == skill)
                            dgr.Cells["Export"].Value = true;

            foreach (var dgr in swordGrid.Rows.Cast<DataGridViewRow>().ToList())
                foreach (var sword in swordState)
                    if (dgr.Cells[0].Value != null && sword != null)
                        if ((string)dgr.Cells[1].Value == sword)
                            dgr.Cells["Export"].Value = true;

            foreach (var dgr in shieldGrid.Rows.Cast<DataGridViewRow>().ToList())
                foreach (var shield in shieldState)
                    if (dgr.Cells[0].Value != null && shield != null)
                        if ((string)dgr.Cells[1].Value == shield)
                            dgr.Cells["Export"].Value = true;

            foreach (var dgr in ArmSkillsGrid.Rows.Cast<DataGridViewRow>().ToList())
                foreach (var armSkill in armSkillState)
                    if (dgr.Cells[0].Value != null && armSkill != null)
                        if ((string)dgr.Cells[1].Value == armSkill)
                            dgr.Cells["Export"].Value = true;
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
                case 2:
                    if (string.IsNullOrEmpty(searchBoxTxt.Text))
                        (swordGrid.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                    else if (searchBoxTxt.Text.StartsWith(".."))
                        (swordGrid.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '%{0}%'", searchBoxTxt.Text.Replace("..", ""));
                    else
                        (swordGrid.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '{0}%'", searchBoxTxt.Text);
                    break;
                case 3:
                    if (string.IsNullOrEmpty(searchBoxTxt.Text))
                        (shieldGrid.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                    else if (searchBoxTxt.Text.StartsWith(".."))
                        (shieldGrid.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '%{0}%'", searchBoxTxt.Text.Replace("..", ""));
                    else
                        (shieldGrid.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '{0}%'", searchBoxTxt.Text);
                    break;
                case 4:
                    if (string.IsNullOrEmpty(searchBoxTxt.Text))
                        (ArmSkillsGrid.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                    else if (searchBoxTxt.Text.StartsWith(".."))
                        (ArmSkillsGrid.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '%{0}%'", searchBoxTxt.Text.Replace("..", ""));
                    else
                        (ArmSkillsGrid.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '{0}%'", searchBoxTxt.Text);
                    break;
            }            
        }

        //Clears the Search Txt
        private void clearSearchBtn_Click(object sender, EventArgs e)
        {
            searchBoxTxt.Text = "";
            searchBoxTxt.TextChanged -= searchBoxTxt_TextChanged;
            if (demonGrid.DataSource != null)
                (demonGrid.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
            if (skillGrid.DataSource != null)
                (skillGrid.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
            if (swordGrid.DataSource != null)
                (swordGrid.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
            if (shieldGrid.DataSource != null)
                (shieldGrid.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
            if (ArmSkillsGrid.DataSource != null)
                (ArmSkillsGrid.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
            demonGrid.Refresh();
            searchBoxTxt.TextChanged += searchBoxTxt_TextChanged;
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
            WikiManager = new WikiManager(uploadToWikiBtn, this, retryWikiLoginBtn);
            NewsScrapper = new NewsScrapper(this);
        }

        //Create new instance of our Wiki Object
        private void retryWikiLoginBtn_Click(object sender, EventArgs e)
        {
            WikiManager = new WikiManager(uploadToWikiBtn, this, retryWikiLoginBtn);
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

        private void loginBtn_Click(object sender, EventArgs e)
        {
            var password = Interaction.InputBox("Enter Your Password. Remember this is stored in plain text in your Environment Variables.", "Enter Your Password", "Enter Your Password");
            Environment.SetEnvironmentVariable("dx2WikiPassword", password, EnvironmentVariableTarget.User);
            logRTB.AppendText("Password has been changed succesfully.\n");
        }

        private void migratorBtn_Click(object sender, EventArgs e)
        {            
            DBMigrator.Migrate(LoadedPath, demonGrid.Rows.Cast<DataGridViewRow>(), skillGrid.Rows.Cast<DataGridViewRow>());
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isDirty)
            {
                var button = MessageBox.Show("You have unsaved changes do you really wish to close?", "Unsaved Changes!", MessageBoxButtons.YesNoCancel);

                if (button != DialogResult.Yes)
                    e.Cancel = true;
            }
        }

        private void Grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            bool isChanging = false;

            if (sender == demonGrid)
                if (demonGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    if (beginValue != demonGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
                        isChanging = true;            
            else if (sender == skillGrid)
                if (skillGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    if (beginValue != skillGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
                        isChanging = true;

            if (isChanging)
            {
                isDirty = true;
                Text = "Dx2 Wiki Writer - You have unsaved changes.";
            }
        }

        private void Grid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (sender == demonGrid)
                    if (demonGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                        beginValue = demonGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                else if (sender == skillGrid)
                    if (skillGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                        beginValue = skillGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
            catch
            {

            }
        }
        
        private void scrapeNews_Click(object sender, EventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            NewsScrapper.ScrapePages(-1);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
        private void scrape5News_Click(object sender, EventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            NewsScrapper.ScrapePages(5);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        private void scrape1News_Click(object sender, EventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            NewsScrapper.ScrapePages(1);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        #endregion
    }
}