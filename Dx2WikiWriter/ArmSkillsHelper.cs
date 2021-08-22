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
    public static class ArmSkillsHelper
    {
        #region Public Methods

        //Exports a list of ArmSkillss as files
        public static void ExportArmSkillss(IEnumerable<DataGridViewRow> selected, IEnumerable<DataGridViewRow> allArmSkillss, bool oneFile, string path)
        {
            var sortedArmSkillss = selected.OrderBy(c => c.Cells["Skill name"].Value);

            var filePath = Path.Combine(path, "ArmSkillsData");
            Directory.CreateDirectory(filePath);

            var data = "";

            foreach (var s in sortedArmSkillss)
            {
                var d = LoadArmSkills(s, allArmSkillss);

                if (d.Name == null)
                    continue;

                if (oneFile)
                {
                    if (sortedArmSkillss.ElementAt(0) != s)
                        data = data + Environment.NewLine;

                    data = data + d.CreateWikiStringComp();
                }
                else
                {
                    data = d.CreateWikiStringIndividual();

                    File.WriteAllText(filePath + "\\" + d.Name + ".txt", data, Encoding.UTF8);
                }
            }

            if (oneFile)
            {
                data = "{| class=\"wikitable sortable\" style=\"text - align:center; width: 100 %;\"" + Environment.NewLine +
                            "|-" + Environment.NewLine +
                            "! Name !!Affinity !!MP !!Effect !!Target !!Use Limit" + Environment.NewLine +
                            "|- style = \"vertical-align:middle;\"" + Environment.NewLine +
                            data + "}";

                File.WriteAllText(filePath + "\\" + ConfigurationManager.AppSettings["armSkillWikiPageName"] + ".txt", data, Encoding.UTF8);
            }
        }

        //Creates a ArmSkills object from a data grid view row
        public static ArmSkills LoadArmSkills(DataGridViewRow row, IEnumerable<DataGridViewRow> ArmSkillss)
        {
            return new ArmSkills
            {
                Name = row.Cells["Skill name"].Value is DBNull ? "" : (string)row.Cells["Skill name"].Value,
                Affinity = row.Cells["Affinity"].Value is DBNull ? "" : (string)row.Cells["Affinity"].Value,
                MP = row.Cells["mp/passive"].Value is DBNull ? "" : (string)row.Cells["mp/passive"].Value,
                Effect = row.Cells["effect"].Value is DBNull ? "" : (string)row.Cells["effect"].Value,
                Target = row.Cells["target"].Value is DBNull ? "" : (string)row.Cells["target"].Value,
                UseLimit = row.Cells["UseLimit"].Value is DBNull ? "" : (string)row.Cells["UseLimit"].Value,
                Levels = row.Cells["Levels"].Value is DBNull ? "" : (string)row.Cells["Levels"].Value,
            };
        }

        #endregion
    }

    #region Structs

    //Object to hold ArmSkills Data
    public struct ArmSkills
    {
        public string Name;
        public string Affinity;
        public string MP;
        public string Effect;
        public string Target;
        public string UseLimit;
        public string Levels;

        //Creates a Wiki String for Individuals in a Comp
        public string CreateWikiStringComp()
        {
            return "|{{ListArmSkills|name=[[" + Name.Replace("[", "(").Replace("]", ")") + "]]|affinity=" + Affinity + "|mp=" + MP.Replace(" MP", "") + "|effect=<nowiki>" + Effect + "</nowiki>|target=" + Target +
                     "|uselimit=" + UseLimit + "}}\r\n |- style=\"vertical-align:middle;\"";
        }

        //Creates a Wiki string for Individual by themselves
        public string CreateWikiStringIndividual()
        {
            //Generate Fusion and Fission URL
            var newName = Name;

            //Return the data
            return "{{ArmSkillTable\r\n" +
                   "|skill=" + Name.Replace("[", "(").Replace("]", ")") + Environment.NewLine +
                   "|type=" + Affinity + Environment.NewLine +
                   "|cost=" + MP.Replace(" MP", "") + Environment.NewLine +
                   "|uselimit=" + UseLimit + Environment.NewLine +
                   "|target=" + Target + Environment.NewLine +
                   "|description=" + "<nowiki>" + Effect + "</nowiki>" + Environment.NewLine +
                   "|levels=" + "<nowiki>" + Levels + "</nowiki>" + Environment.NewLine +
                   "|icon=" + "{{{icon}}}" + Environment.NewLine +
                   "}}" + Environment.NewLine + Environment.NewLine;
        }
    }

    #endregion
}