using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dx2WikiWriter
{
    public class DBMigrator
    {        
        public static void Migrate(string loadedPath, IEnumerable<DataGridViewRow> demons, IEnumerable<DataGridViewRow> skills)
        {
            var lines = "";

            //Migrate Demon Table
            foreach (var row in demons)
            {
                var demon = DemonHelper.LoadDemon(row, demons);

                lines += "Insert Into dx2hub.Demons Values (" +
                    "null, " +
                    "\"" + demon.Name + "\"," +
                    " (Select Race_ID from dx2hub.races where Race_Name = \"" + demon.Race + "\")," +
                    " (Select AI_ID from dx2hub.races where AI_Name = \"" + demon.Ai + "\")," +
                    " " + demon.Grade + "," +
                    " " + demon.Rarity + "); \n";
            }


            File.WriteAllText(loadedPath + "/Migration.sql", lines);
        }
    }
}
