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

            lines += "#Demons \n\n";

            //Migrate Demon Table
            foreach (var row in demons)
            {
                var demon = DemonHelper.LoadDemon(row, demons);

                if (demon.Name != "")
                {
                    lines += "# " + demon.Name + "\n";

                    //Insert Demons
                    lines += "Insert Into dx2hub.Demons Values (" +
                        "null, " +
                        "\"" + demon.Name + "\"," +
                        " (Select Race_ID from dx2hub.races where Race_Name = \"" + demon.Race + "\")," +
                        " (Select AI_ID from dx2hub.races where AI_Name = \"" + demon.Ai + "\")," +
                        " " + demon.Grade + "," +
                        " " + demon.Rarity + "); \n";

                    //Insert Resistances
                    lines += "Insert Into dx2hub.Resistances Values (" +
                        " (Select Demon_ID from dx2hub.Demons where Demon_Name = \"" + demon.Name + "\")," +
                        " (Select Resist_ID from dx2hub.Resistance_Type where Resist_Type = \"" + FixResist(demon.Phys) + "\")," +
                        " (Select Resist_ID from dx2hub.Resistance_Type where Resist_Type = \"" + FixResist(demon.Fire) + "\")," +
                        " (Select Resist_ID from dx2hub.Resistance_Type where Resist_Type = \"" + FixResist(demon.Ice) + "\")," +
                        " (Select Resist_ID from dx2hub.Resistance_Type where Resist_Type = \"" + FixResist(demon.Force) + "\")," +
                        " (Select Resist_ID from dx2hub.Resistance_Type where Resist_Type = \"" + FixResist(demon.Elec) + "\")," +
                        " (Select Resist_ID from dx2hub.Resistance_Type where Resist_Type = \"" + FixResist(demon.Light) + "\")," +
                        " (Select Resist_ID from dx2hub.Resistance_Type where Resist_Type = \"" + FixResist(demon.Dark) + "\")); \n";

                    //Insert Stats
                    lines += "Insert Into dx2hub.Stats Values (" +
                        "50," + demon.Str + "," + demon.Mag + "," + demon.Vit + "," + demon.Agi + "," + demon.Luck + "," + true + "); \n";

                    lines += "\n";
                }
            }
                        
            File.WriteAllText(loadedPath + "/Migration.sql", lines);
        }

        private static string FixResist(string resist)
        {
            var fixedResist = resist.Replace("{{ResistColor|1=", "");
            fixedResist = fixedResist.Replace("}}", "");
            fixedResist = fixedResist.Replace("<nowiki>-</nowiki>", "null");

            return fixedResist;
        }
    }
}
