using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                if (row.Cells["Name"].Value is string)
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
            }
            
            lines += "#Skills \n\n";

            foreach (var row in skills)
            {
                if (row.Cells["Name"].Value is string)
                {
                    var skill = SkillHelper.LoadSkill(row, demons);

                    lines += "# " + skill.Name + "\n";

                    var desc = ReadDescription(skill.Description);
                    var sp = skill.Sp;
                        
                    if (sp == "")
                        sp = "null";

                    //Insert Skills
                    lines += "Insert Into dx2.skills Values (" +
                        "null, " +
                        "\"" + skill.Name + "\"," +
                        " (Select skill_type_id from dx2.skill_types where skill_type_name = \"" + skill.Element.Substring(0, 1).ToUpper() + skill.Element.Substring(1, skill.Element.Length -1) + "\")," +
                        " (Select skill_target_id from dx2.skill_target where skill_target_name = \"" + skill.Target + "\")," +
                        "null, null, " + skill.Cost.Replace(" MP", "").ToLower().Replace("passive", "null") + ", " + sp + ", " + skill.DuelExclusive + ", " + skill.ExtractExclusive + ",null, null); \n";

                    lines += "Insert Into dx2.skill_levels Values (" +
                        "(Select skill_id from dx2.skills where skill_name = \"" + skill.Name + "\"), \n" +
                        desc[0] + "," +
                        desc[1] + "," +
                        desc[2] + "," +
                        desc[3] + "," +
                        desc[4] + "," +
                        desc[5] + "," +
                        desc[6] + "," +
                        desc[7] + ");\n";

                    lines += "\n";
                }
            }
                        
            File.WriteAllText(loadedPath + "/Migration.sql", lines);
        }

        private static string[] ReadDescription(string desc)
        {
            var newDesc = new string[8] { "null", "null", "null", "null", "null", "null", "null", "null" } ;

            var descCount = desc.IndexOf("\\");
                        
            if (descCount != -1)
            {
                //Base Desc
                newDesc[0] = FixStringValue(desc.Substring(0, descCount));
                
                //Lvl 2-7
                var endingDesc = desc.Substring(descCount, desc.Length - 1 - descCount).Replace("\\n", "");                                
                var lastDesc = Regex.Split(endingDesc, "(Lv.:)");

                if (lastDesc.Count() > 1)
                    newDesc[1] = FixStringValue(lastDesc[2]);
                if (lastDesc.Count() > 3)
                    newDesc[2] = FixStringValue(lastDesc[4]);
                if (lastDesc.Count() > 5)
                    newDesc[3] = FixStringValue(lastDesc[6]);
                if (lastDesc.Count() > 7)
                    newDesc[4] = FixStringValue(lastDesc[8]);
                if (lastDesc.Count() > 9)
                    newDesc[5] = FixStringValue(lastDesc[10]);
                if (lastDesc.Count() > 11)
                    newDesc[6] = FixStringValue(lastDesc[12]);
                if (lastDesc.Count() > 13)
                    newDesc[7] = FixStringValue(lastDesc[14]);
            }
            else
                newDesc[0] = "\"" + desc + "\"";

            return newDesc;
        }

        private static string FixStringValue(string input)
        {
            if (input == "")
                return "null";

            return "\"" + input.Trim() + "\"";
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
