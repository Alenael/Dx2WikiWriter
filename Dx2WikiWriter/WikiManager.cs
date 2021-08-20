using System;
using System.IO;
using System.Threading.Tasks;
using WikiClientLibrary.Client;
using WikiClientLibrary.Pages;
using WikiClientLibrary.Sites;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;

namespace Dx2WikiWriter
{
    public class WikiManager
    {
        #region Properties

        private WikiSite Site;
        private bool Connected;
        private MainForm Callback;

        #endregion

        #region Constructor

        //Entry way to class
        public WikiManager(Button uploadToWikiBtn, MainForm logRTB, Button retryWikiLoginBtn)
        {
            Callback = logRTB;
            Connect(uploadToWikiBtn, retryWikiLoginBtn);
        }

        #endregion

        #region Methods

        //Creates our initial connection and login to the Wiki
        private async void Connect(Button uploadToWikiBtn, Button retryWikiLoginBtn)
        {
            Callback.SetTextBox("Attempting to Login to Wiki..\n");

            try
            {
                var client = new WikiClient() { ClientUserAgent = "Dx2WikiWriter/1.0", MaxRetries = 5, Timeout = new TimeSpan(0, 5, 0), RetryDelay = new TimeSpan(0, 0, 10), };
                Site = new WikiSite(client, await WikiSite.SearchApiEndpointAsync(client, "dx2wiki.com"));
                await Site.Initialization;

                await Site.LoginAsync(ConfigurationManager.AppSettings["username"], Environment.GetEnvironmentVariable("dx2WikiPassword", EnvironmentVariableTarget.User));                Connected = true;
                uploadToWikiBtn.Visible = true;
                Callback.AppendTextBox("Succesfully Logged Into Wiki!\n");
                retryWikiLoginBtn.Visible = false;
            }
            catch(Exception e)
            {
                Callback.AppendTextBox("Login Failed. " + e.Message + "\n");
                retryWikiLoginBtn.Visible = true;
            }
        }

        //Uploads all files in our directories
        public async Task UploadAllFilesAsync(string rootPath, IEnumerable<DataGridViewRow> demons)
        {
            Callback.AppendTextBox("Started Uploading Files\n");

            try
            {

                if (Directory.Exists(rootPath))
                {
                    if (Directory.Exists(rootPath + "/SkillData"))
                        foreach (var file in new DirectoryInfo(rootPath + "/SkillData").GetFiles())
                        {
                            await UploadFile(file.FullName, demons);
                            await Task.Delay(2000);
                        }

                    if (Directory.Exists(rootPath + "/DemonData"))
                        foreach (var file in new DirectoryInfo(rootPath + "/DemonData").GetFiles())
                        {
                            await UploadFile(file.FullName, null);
                            await Task.Delay(2000);
                        }

                    if (Directory.Exists(rootPath + "/SwordData"))
                        foreach (var file in new DirectoryInfo(rootPath + "/SwordData").GetFiles())
                        {
                            await UploadFile(file.FullName, null);
                            await Task.Delay(2000);
                        }

                    if (Directory.Exists(rootPath + "/ShieldData"))
                        foreach (var file in new DirectoryInfo(rootPath + "/ShieldData").GetFiles())
                        {
                            await UploadFile(file.FullName, null);
                            await Task.Delay(2000);
                        }

                    if (Directory.Exists(rootPath + "/ArmSkillsData"))
                        foreach (var file in new DirectoryInfo(rootPath + "/ArmSkillsData").GetFiles())
                        {
                            await UploadFile(file.FullName, null);
                            await Task.Delay(2000);
                        }
                }

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message + " " + e.StackTrace);
            }

            Callback.AppendTextBox("Completed Uploading Files\n");
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
                        if (demons.Any(d => (string)d.Cells[0].Value + "/Demons" == pageName))
                            pageName = pageName.Replace("/Demons", "") + " (Skill)/Demons";
                    }
                    else
                    {
                        if (demons.Any(d => (string)d.Cells[0].Value == pageName))
                            pageName = pageName + " (Skill)";
                    }
                }

                Callback.AppendTextBox("Processing.. " + pageName + "\n");

                var page = new WikiPage(Site, pageName);
                await page.RefreshAsync(PageQueryOptions.FetchContent).ConfigureAwait(false);

                var content = File.ReadAllText(fileName);
                if (page.Content == null || page.Content.Trim() != content.Replace("\r", "").Trim())
                {
                    bool repeat = true;
                    var count = 0;
                    while (repeat)
                    {
                        count++;
                        if (count >= 5)
                        {
                            Callback.AppendTextBox("Can't update demon. Skipping: <https://dx2wiki.com/index.php/" + Uri.EscapeUriString(pageName) + "> \n");
                            repeat = false;
                        }
                        else
                        {
                            try
                            {
                                page.Content = content;
                                bool worked = await page.UpdateContentAsync("Updated by " + ConfigurationManager.AppSettings["username"] + ". This was done by a bot.", false, true, AutoWatchBehavior.Default).ConfigureAwait(false);

                                if (worked)
                                {
                                    Callback.AppendTextBox("Updated: <https://dx2wiki.com/index.php/" + Uri.EscapeUriString(pageName) + "> \n");
                                    File.Delete(fileName);
                                    Callback.AppendTextBox("File Removed: " + fileName + "\n");
                                }
                                else
                                {
                                    Callback.AppendTextBox("Could not upload: <https://dx2wiki.com/index.php/" + Uri.EscapeUriString(pageName) + "> \n");
                                }

                                repeat = false;
                            }
                            catch (Exception e)
                            {
                                Callback.AppendTextBox(e.Message + "\n" + e.StackTrace + "\n");
                                Callback.AppendTextBox("Retrying.. <https://dx2wiki.com/index.php/" + Uri.EscapeUriString(pageName) + ">\n");
                            }
                        }
                    }
                }
                else
                {
                    Callback.AppendTextBox("No Change Required: <https://dx2wiki.com/index.php/" + Uri.EscapeUriString(pageName) + "> \n");
                    File.Delete(fileName);
                    Callback.AppendTextBox("File Removed: " + fileName + "\n");
                }
            }
        }

        #endregion
    }
}
