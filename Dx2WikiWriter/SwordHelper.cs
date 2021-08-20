using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Dx2WikiWriter
{
    public static class SwordHelper
    {
        #region Public Methods

        //Exports a list of Swords as files
        public static void ExportSwords(IEnumerable<DataGridViewRow> selected, IEnumerable<DataGridViewRow> allSwords, bool oneFile, string path)
        {
            var sortedSwords = selected.OrderBy(c => c.Cells["demon name"].Value);

            var filePath = Path.Combine(path, "SwordData");
            Directory.CreateDirectory(filePath);

            var data = "";

            foreach (var s in sortedSwords)
            {
                var d = LoadSword(s, allSwords);

                if (d.DemonName == null)
                    continue;

                if (oneFile)
                {
                    if (sortedSwords.ElementAt(0) != s)
                        data = data + Environment.NewLine;

                    data = data + d.CreateWikiStringComp();
                }
                else
                {
                    data = d.CreateWikiStringIndividual();

                    File.WriteAllText(filePath + "\\" + d.DemonName + "-Sword.txt", data, Encoding.UTF8);
                }
            }

            if (oneFile)
            {
                data = "{| class=\"wikitable sortable\" style=\"text - align:center; width: 100 %;\"" + Environment.NewLine +
                            "|-" +
                            Environment.NewLine +
                            "! Name !!Sword Name !!Talent and Effect !!Skills !!Atk Stat !!Attribute Increase !!Accuracy !!Crtical !!Panel 1 !!Panel 2 !!Panel 3" + Environment.NewLine +
                            "|- style = \"vertical-align:middle;\"" + Environment.NewLine +
                            data + "}";

                File.WriteAllText(filePath + "\\" + ConfigurationManager.AppSettings["SwordWikiPageName"] + ".txt", data, Encoding.UTF8);
            }
        }

        //Creates a Sword object from a data grid view row
        public static Sword LoadSword(DataGridViewRow row, IEnumerable<DataGridViewRow> Swords)
        {
            return new Sword
            {
                DemonName = row.Cells["demon name"].Value is DBNull ? "" : (string)row.Cells["demon name"].Value,
                SwordName = row.Cells["sword Name"].Value is DBNull ? "" : (string)row.Cells["sword Name"].Value,
                Talent = row.Cells["Talent name"].Value is DBNull ? "" : (string)row.Cells["Talent name"].Value,
                Effect = row.Cells["Talent effect"].Value is DBNull ? "" : (string)row.Cells["Talent effect"].Value,
                Skill1 = row.Cells["skill 1"].Value is DBNull ? "" : (string)row.Cells["skill 1"].Value,
                Skill2 = row.Cells["skill 2"].Value is DBNull ? "" : (string)row.Cells["skill 2"].Value,
                AwakenSkill = row.Cells["awaken skill"].Value is DBNull ? "" : (string)row.Cells["awaken skill"].Value,
                AtkStat = row.Cells["Phys/mag atk %"].Value is DBNull ? "" : (string)row.Cells["Phys/mag atk %"].Value,
                Attribute = row.Cells["Atribute increase"].Value is DBNull ? "" : (string)row.Cells["Atribute increase"].Value,
                Accuracy = row.Cells["Phys ACC%"].Value is DBNull ? "" : (string)row.Cells["Phys ACC%"].Value,
                Critical = row.Cells["Critical %"].Value is DBNull ? "" : (string)row.Cells["Critical %"].Value,
                Panel1 = row.Cells["panel 1"].Value is DBNull ? "" : (string)row.Cells["panel 1"].Value,
                Panel2 = row.Cells["panel 2"].Value is DBNull ? "" : (string)row.Cells["panel 2"].Value,
                Panel3 = row.Cells["panel 3"].Value is DBNull ? "" : (string)row.Cells["panel 3"].Value,
                Panel1Stats = row.Cells["Panel 1 Step"].Value is DBNull ? "" : (string)row.Cells["Panel 1 Step"].Value,
                Panel2Stats = row.Cells["panel 2 Step"].Value is DBNull ? "" : (string)row.Cells["panel 2 Step"].Value,
                Panel3Stats = row.Cells["panel 3 Step"].Value is DBNull ? "" : (string)row.Cells["panel 3 Step"].Value
            };
        }

        #endregion
    }

    #region Structs

    //Object to hold Sword Data
    public struct Sword
    {
        public string DemonName;
        public string SwordName;
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

        public string AtkStat;
        public string Attribute;
        public string Accuracy;
        public string Critical;

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

            return "|{{ListSword|sword=" + DemonName + "|swordname=" + SwordName + "|talent=" + Talent + ": " + Effect + "|skills=" + mySkills +
                   "|attack=" + AtkStat + "|attribute=" + Attribute + "|accuracy=" + Accuracy + "|critical=" + Critical +
                   "|panel1= <nowiki>" + Panel1 + "</nowiki>|panel2= <nowiki>" + Panel2 + "</nowiki>|panel3= <nowiki>" + Panel3 +
                   "</nowiki>}}\r\n" +
                   "|- style=\"vertical-align:middle;\"";
        }

        //Creates a Wiki string for Individual by themselves
        public string CreateWikiStringIndividual()
        {
            return "{{DemonSword" + Environment.NewLine +
            "|swordname= [[" + SwordName + "]]" + Environment.NewLine +
            "|talentname=" + Talent + Environment.NewLine +
            "|talenteffect=" + Effect + Environment.NewLine +
            "|pmincrease="+ AtkStat + Environment.NewLine +
            "|elementincrease=" + Attribute + Environment.NewLine +
            "|physacc=" + Accuracy + Environment.NewLine +
            "|critchance=" + Critical + Environment.NewLine +
            "|innate_skill1=" + Skill1 + Environment.NewLine +
            "|innate_skill2=" + Skill2 + Environment.NewLine +
            "|armament_awaken=" + AwakenSkill + Environment.NewLine +
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