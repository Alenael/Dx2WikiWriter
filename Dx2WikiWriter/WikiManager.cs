using System;
using System.IO;
using System.Threading.Tasks;
using WikiClientLibrary;
using WikiClientLibrary.Client;
using WikiClientLibrary.Pages;
using WikiClientLibrary.Sites;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Dx2WikiWriter
{
    public class WikiManager
    {
        #region Properties

        private WikiSite Site;
        private bool Connected;
        private System.Windows.Forms.RichTextBox Callback;

        #endregion

        #region Constructor

        //Entry way to class
        public WikiManager(System.Windows.Forms.Button uploadToWikiBtn, System.Windows.Forms.RichTextBox logRTB, System.Windows.Forms.Button retryWikiLoginBtn)
        {
            Callback = logRTB;
            Connect(uploadToWikiBtn, retryWikiLoginBtn);
        }

        #endregion

        #region Methods

        //Creates our initial connection and login to the Wiki
        private async void Connect(System.Windows.Forms.Button uploadToWikiBtn, System.Windows.Forms.Button retryWikiLoginBtn)
        {
            Callback.Text = "Attempting to Login to Wiki..\n";

            try
            {
                var client = new WikiClient() { ClientUserAgent = "Dx2WikiWriter/1.0" };
                Site = new WikiSite(client, await WikiSite.SearchApiEndpointAsync(client, "dx2wiki.com"));
                await Site.Initialization;

                await Site.LoginAsync(ConfigurationManager.AppSettings["username"], Environment.GetEnvironmentVariable("password", EnvironmentVariableTarget.User));
                Connected = true;
                uploadToWikiBtn.Visible = true;
                Callback.AppendText( "Succesfully Logged Into Wiki!\n");
                retryWikiLoginBtn.Visible = false;
            }
            catch(Exception e)
            {
                Callback.AppendText("Login Failed. " + e.Message + "\n");
                retryWikiLoginBtn.Visible = true;
            }
        }

        //Uploads all files in our directories
        public async Task UploadAllFilesAsync(string rootPath, IEnumerable<DataGridViewRow> demons)
        {
            Callback.AppendText("Started Uploading Files\n");

            if (Directory.Exists(rootPath))
            {
                if (Directory.Exists(rootPath + "/SkillData"))
                    foreach (var file in new DirectoryInfo(rootPath + "/SkillData").GetFiles())
                        await UploadFile(file.FullName, demons);

                if (Directory.Exists(rootPath + "/DemonData"))
                    foreach (var file in new DirectoryInfo(rootPath + "/DemonData").GetFiles())
                        await UploadFile(file.FullName, null);                
            }

            Callback.AppendText("Completed Uploading Files\n");
        }

        //Uploads a file to the Wiki
        private async Task UploadFile(string fileName, IEnumerable<DataGridViewRow> demons)
        {

            if (Connected && File.Exists(fileName))
            {
                //Generate our Page Name
                var pageName = Path.GetFileNameWithoutExtension(fileName);
                pageName = pageName.Replace("-Demons", "/Demons").Replace("[", "(").Replace("]", ")");
                 
                //If we are a skill check if demon shares our name
                if (demons != null)
                {
                    if (pageName.Contains("/Demons"))
                    {
                        if (demons.Any(d => (string)d.Cells["Name"].Value + "/Demons" == pageName))
                            pageName = pageName.Replace("/Demons", "") + " (Skill)/Demons";
                    }
                    else
                    {
                        if (demons.Any(d => (string)d.Cells["Name"].Value == pageName))
                            pageName = pageName + " (Skill)";
                    }
                }

                Callback.AppendText("Processing.. " + pageName + "\n");

                var page = new WikiPage(Site, pageName);
                await page.RefreshAsync(PageQueryOptions.FetchContent);

                var content = File.ReadAllText(fileName);
                if (page.Content != content.Replace("\r", ""))
                {
                    page.Content = content;
                    await page.UpdateContentAsync("Updated by Alenael(bot).", false, true);                    
                    Callback.AppendText("Updated: <https://dx2wiki.com/index.php/" + Uri.EscapeUriString(pageName) + "> \n");
                    Callback.AppendText("File Removed: " + fileName + "\n");
                    File.Delete(fileName);
                }
                else
                    Callback.AppendText("No Change Required: <https://dx2wiki.com/index.php/" + Uri.EscapeUriString(pageName) + "> \n");
            }
        }

        #endregion
    }
}
