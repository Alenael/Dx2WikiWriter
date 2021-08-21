using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Dx2WikiWriter
{
    public static class ShieldHelper
    {
        #region Public Methods

        //Exports a list of Shields as files
        public static void ExportShields(IEnumerable<DataGridViewRow> selected, IEnumerable<DataGridViewRow> allShields, bool oneFile, string path)
        {
            var sortedShields = selected.OrderBy(c => c.Cells["demon name"].Value);

            var filePath = Path.Combine(path, "ShieldData");
            Directory.CreateDirectory(filePath);

            var data = "";

            foreach (var s in sortedShields)
            {
                var d = LoadShield(s, allShields);

                if (d.DemonName == null)
                    continue;

                if (oneFile)
                {
                    if (sortedShields.ElementAt(0) != s)
                        data = data + Environment.NewLine;

                    data = data + d.CreateWikiStringComp();
                }
                else
                {
                    data = d.CreateWikiStringIndividual();

                    File.WriteAllText(filePath + "\\" + d.DemonName + "-Shield.txt", data, Encoding.UTF8);
                }
            }

            if (oneFile)
            {
                data = "{| class=\"wikitable sortable\" style=\"text - align:center; width: 100 %;\"" + Environment.NewLine +
                            "|-" +
                            Environment.NewLine +
                            "!Name !!Shield Name !!Talent and Effect !!Skills" + Environment.NewLine +
                            "![[File: Physical.png | 20px | link =]] !![[File: Fire.png | 20px | link =]]" + Environment.NewLine +
                            "![[File: Ice.png | 20px | link =]] !![[File: Electricity.png | 20px | link =]]" + Environment.NewLine +
                            "![[File: Force.png | 20px | link =]] !![[File: Light.png | 20px | link =]]" + Environment.NewLine +
                            "![[File: Dark.png | 20px | link =]]" + Environment.NewLine +
                            "!Damage Reduction !!PDef !!MDef !!HP !!Panel 1 !!Panel 2 !!Panel 3" + Environment.NewLine +
                            "|- style = \"vertical-align:middle;\"" + Environment.NewLine +
                            data + "}";

                File.WriteAllText(filePath + "\\" + ConfigurationManager.AppSettings["ShieldWikiPageName"] + ".txt", data, Encoding.UTF8);
            }
        }

        //Returns a value based on what its passed
        private static string LoadResist(string value)
        {
            if (value == "" || value == null)
                return "<nowiki>-</nowiki>";
            else
            {
                var type = "";

                switch (value)
                {
                    case "rs":
                        type = "Resist";
                        break;
                    case "rp":
                        type = "Repel";
                        break;
                    case "wk":
                        type = "Weak";
                        break;
                    case "nu":
                        type = "Null";
                        break;
                    case "ab":
                        type = "Drain";
                        break;
                }

                return "{{ResistColor|1=" + type + "}}";
            }
        }

        //Creates a Shield object from a data grid view row
        public static Shield LoadShield(DataGridViewRow row, IEnumerable<DataGridViewRow> Shields)
        {
            return new Shield
            {
                DemonName = row.Cells["demon name"].Value is DBNull ? "" : (string)row.Cells["demon name"].Value,
                ShieldName = row.Cells["Shield Name"].Value is DBNull ? "" : (string)row.Cells["Shield Name"].Value,
                Talent = row.Cells["talent name"].Value is DBNull ? "" : (string)row.Cells["talent name"].Value,
                Effect = row.Cells["effect"].Value is DBNull ? "" : (string)row.Cells["effect"].Value,
                Skill1 = row.Cells["skill 1"].Value is DBNull ? "" : (string)row.Cells["skill 1"].Value,
                Skill2 = row.Cells["skill 2"].Value is DBNull ? "" : (string)row.Cells["skill 2"].Value,
                AwakenSkill = row.Cells["Awaken Skill"].Value is DBNull ? "" : (string)row.Cells["Awaken Skill"].Value,
                PDef = row.Cells["Defense"].Value is DBNull ? "" : (string)row.Cells["Defense"].Value,
                Attribute = row.Cells["DMG reduction for atribute"].Value is DBNull ? "" : (string)row.Cells["DMG reduction for atribute"].Value,
                MDef = row.Cells["mdef"].Value is DBNull ? "" : (string)row.Cells["mdef"].Value,
                HP = row.Cells["HP%"].Value is DBNull ? "" : (string)row.Cells["HP%"].Value,
                Panel1 = row.Cells["panel 1"].Value is DBNull ? "" : (string)row.Cells["panel 1"].Value,
                Panel2 = row.Cells["panel 2"].Value is DBNull ? "" : (string)row.Cells["panel 2"].Value,
                Panel3 = row.Cells["panel 3"].Value is DBNull ? "" : (string)row.Cells["panel 3"].Value,
                Panel1Stats = row.Cells["Panel 1 Step"].Value is DBNull ? "" : (string)row.Cells["Panel 1 Step"].Value,
                Panel2Stats = row.Cells["panel 2 Step"].Value is DBNull ? "" : (string)row.Cells["panel 2 Step"].Value,
                Panel3Stats = row.Cells["panel 3 Step"].Value is DBNull ? "" : (string)row.Cells["panel 3 Step"].Value,
                Fire = LoadResist(row.Cells["Fire"].Value is DBNull ? "" : (string)row.Cells["Fire"].Value),
                Dark = LoadResist(row.Cells["Dark"].Value is DBNull ? "" : (string)row.Cells["Dark"].Value),
                Light = LoadResist(row.Cells["Light"].Value is DBNull ? "" : (string)row.Cells["Light"].Value),
                Elec = LoadResist(row.Cells["Elec"].Value is DBNull ? "" : (string)row.Cells["Elec"].Value),
                Ice = LoadResist(row.Cells["Ice"].Value is DBNull ? "" : (string)row.Cells["Ice"].Value),
                Force = LoadResist(row.Cells["Force"].Value is DBNull ? "" : (string)row.Cells["Force"].Value),
                Phys = LoadResist(row.Cells["Phys"].Value is DBNull ? "" : (string)row.Cells["Phys"].Value)
            };
        }

        #endregion
    }

    #region Structs

    //Object to hold Shield Data
    public struct Shield
    {
        public string DemonName;
        public string ShieldName;
        public string Talent;
        public string Effect;
        public string Skill1;
        public string Skill2;
        public string AwakenSkill;
        public string Panel1;
        public string Panel2;
        public string Panel3;
        public string Panel1Stats;
        public string Panel2Stats;
        public string Panel3Stats;
        
        public string HP;
        public string PDef;
        public string MDef;
        public string Attribute;

        public string Phys;
        public string Fire;
        public string Ice;
        public string Elec;
        public string Force;
        public string Light;
        public string Dark;

        //Creates a Wiki String for Individuals in a Comp
        public string CreateWikiStringComp()
        {
            var mySkills = "";

            if (Skill1 != "")
                mySkills = "[[" + Skill1 + "]] <br/>";

            if (Skill2 != "")
                mySkills = mySkills + "[[" + Skill2 + "]] <br/>";

            if (AwakenSkill != "")
                mySkills = mySkills + "[[" + AwakenSkill + "]]";

            return "|{{ListShield|shield=" + DemonName + "|shieldname=[[" + DemonName + "/Shield|" + ShieldName + "]]|talent=" + Talent + ": " + Effect + "|skills=" + mySkills +
                     "|phys= " + Phys + "|fire= " + Fire + "|ice= " + Ice + "|elec= " + Elec + "" +
                     "|force= " + Force + "|light= " + Light + "|dark= " + Dark +
                     "|attribute= " + Attribute + "|pdef= " + PDef + "|mdef= " + MDef + "|hp= " + HP +
                     "|panel1= <nowiki>" + Panel1 + "</nowiki>|panel2= <nowiki>" + Panel2 + "</nowiki>|panel3= <nowiki>" + Panel3 +
                     "</nowiki>}}\r\n" +
                     "|- style=\"vertical-align:middle;\"";
        }

        //Creates a Wiki string for Individual by themselves
        public string CreateWikiStringIndividual()
        {
            return "{{DemonShield" + Environment.NewLine +
                "|shieldname=" + ShieldName + Environment.NewLine +
                "|talentname=" + Talent + Environment.NewLine +
                "|talenteffect=" + Effect + Environment.NewLine +
                "|phys=" + Phys + Environment.NewLine +
                "|fire=" + Fire + Environment.NewLine +
                "|ice=" + Ice + Environment.NewLine +
                "|elec=" + Elec + Environment.NewLine +
                "|force=" + Force + Environment.NewLine +
                "|light=" + Light + Environment.NewLine +
                "|dark=" + Dark + Environment.NewLine +
                "|hp=" + HP + Environment.NewLine +
                "|elementreduction=" + Attribute + Environment.NewLine +
                "|def=" + PDef + Environment.NewLine +
                "|magdef=" + MDef + Environment.NewLine +
                "|innate_skill1=" + (Skill1 == "" ? "N/A" : Skill1.Replace("[", "(").Replace("]", ")")) + Environment.NewLine +
                "|innate_skill2=" + (Skill2 == "" ? "N/A" : Skill2.Replace("[", "(").Replace("]", ")")) + Environment.NewLine +
                "|armament_awaken=" + (AwakenSkill == "" ? "N/A" : AwakenSkill.Replace("[", "(").Replace("]", ")")) + Environment.NewLine +
                "|panel1=" + Panel1 + Environment.NewLine +
                "|panel2=" + Panel2 + Environment.NewLine +
                "|panel3=" + Panel3 + Environment.NewLine +
                "|panel1stats=" + Panel1Stats + Environment.NewLine +
                "|panel2stats=" + Panel2Stats + Environment.NewLine +
                "|panel3stats=" + Panel3Stats + Environment.NewLine +
                "}}";
        }
    }

    #endregion
}