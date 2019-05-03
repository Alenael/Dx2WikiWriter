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
                    lines += "Insert Into dx2.demons Values (" +
                        "null, " +
                        "\"" + demon.Name + "\"," +
                        " (Select race_id from dx2.demon_races where race_name = \"" + demon.Race + "\")," +
                        " (Select ai_id from dx2.demon_ais where ai_name = \"" + demon.Ai + "\")," +
                        " " + demon.Grade + "," +
                        " " + demon.Rarity + "); \n";

                    //Insert Resistances
                    lines += "Insert Into dx2.demon_resistances Values (" +
                        "(Select demon_id from dx2.demons where demon_name = \"" + demon.Name + "\"), ";
                    lines += CreateInsertResist(FixResist(demon.Phys)) + ", ";
                    lines += CreateInsertResist(FixResist(demon.Fire)) + ", ";
                    lines += CreateInsertResist(FixResist(demon.Ice)) + ", ";
                    lines += CreateInsertResist(FixResist(demon.Force)) + ", ";
                    lines += CreateInsertResist(FixResist(demon.Elec)) + ", ";
                    lines += CreateInsertResist(FixResist(demon.Light)) + ", ";
                    lines += CreateInsertResist(FixResist(demon.Dark)) + "); \n";

                    //Insert Stats
                    lines += "Insert Into dx2.demon_stats Values (" +
                        "(Select demon_id from dx2.demons where demon_name = \"" + demon.Name + "\"), " +
                        "50," + demon.HP + "," + demon.Str + "," + demon.Mag + "," + demon.Vit + "," + demon.Agi + "," + demon.Luck + "," + true + "); \n";

                    lines += "\n";
                }
            }
                        
            File.WriteAllText(loadedPath + "/Migration.sql", lines);
        }

        private static string CreateInsertResist(string resist)
        {
            if (resist != "")
                return "(Select resist_id from dx2.demon_resistance_type where resist_type = \"" + resist + "\")";

            return "null";
        }

        private static string FixResist(string resist)
        {
            var fixedResist = resist.Replace("{{ResistColor|1=", "");
            fixedResist = fixedResist.Replace("}}", "");
            fixedResist = fixedResist.Replace("<nowiki>-</nowiki>", "");

            return fixedResist;
        }
    }
}
