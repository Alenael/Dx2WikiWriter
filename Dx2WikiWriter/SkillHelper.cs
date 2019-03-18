using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dx2WikiWriter
{
    public static class SkillHelper
    {
        #region public Methods

        //Exports a list of skills as files
        public static void ExportSkills(IEnumerable<DataGridViewRow> skills, IEnumerable<DataGridViewRow> demons, bool oneFile, string path)
        {
            var learnedBy = GetLearnedBy(skills, demons);
            var transferableFrom = GetTransferableFrom(skills, demons);

            var filePath = Path.Combine(path, "SkillData");
            Directory.CreateDirectory(filePath);

            var data = "";

            foreach (var s in skills)
            {
                var d = LoadSkill(s, learnedBy, transferableFrom);

                if (d.Name == null)
                    continue;

                if (oneFile)
                {
                    if (skills.ElementAt(0) != s)
                        data = data + "\r\n";

                    data = data + d.CreateWikiStringComp();
                }
                else
                {
                    data = d.CreateWikiStringIndividual();

                    File.WriteAllText(filePath + "\\" + d.Name + ".txt", data, System.Text.Encoding.UTF8);
                }

                if (oneFile)
                {
                    File.WriteAllText(filePath + "\\Skill Comp.txt", data, Encoding.UTF8);
                }
            }
        }

        #endregion

        #region Private Methods

        //Gets list of skills and what can transfer each of them
        private static Dictionary<string, string> GetTransferableFrom(IEnumerable<DataGridViewRow> skills, IEnumerable<DataGridViewRow> demons)
        {
            var trans = new Dictionary<string, string>();

            foreach (var s in skills)
            {
                if (s.Cells["Name"].Value == null)
                    continue;

                trans.Add((string)s.Cells["Name"].Value, "");
            }

            foreach (var d in demons)
            {
                var demonName = d.Cells["Name"].Value;

                foreach (var s in skills)
                {
                    var skillName = (string)s.Cells["Name"].Value;

                    if (skillName == null)
                        continue;

                    if (!(d.Cells["Skill 1"].Value is DBNull) && (string)d.Cells["Skill 1"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]], ";

                    if (!(d.Cells["Red Gacha"].Value is DBNull) && (string)d.Cells["Red Gacha"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Red Archetype), ";

                    if (!(d.Cells["Teal Gacha"].Value is DBNull) && (string)d.Cells["Teal Gacha"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Teal Archetype), ";

                    if (!(d.Cells["Yellow Gacha"].Value is DBNull) && (string)d.Cells["Yellow Gacha"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Yellow Archetype), ";

                    if (!(d.Cells["Purple Gacha"].Value is DBNull) && (string)d.Cells["Purple Gacha"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Purple Archetype), ";

                    if (trans[skillName].EndsWith(", "))
                    {
                        trans[skillName].Remove(trans[skillName].Length - 2, 2);
                        trans[skillName] += " ";
                    }
                }
            }

            return trans;
        }
    
        //Gets list of skills and what can learn each of them
        private static Dictionary<string, string> GetLearnedBy(IEnumerable<DataGridViewRow> skills, IEnumerable<DataGridViewRow> demons)
        {
            var trans = new Dictionary<string, string>();

            foreach (var s in skills)
            {
                if (s.Cells["Name"].Value == null)
                    continue;

                trans.Add((string)s.Cells["Name"].Value, "");
            }

            foreach (var d in demons)
            {
                var demonName = d.Cells["Name"].Value;

                foreach (var s in skills)
                {
                    var skillName = (string)s.Cells["Name"].Value;

                    if (skillName == null)
                        continue;

                    if (!(d.Cells["Skill 1"].Value is DBNull) && (string)d.Cells["Skill 1"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]], ";
                    
                    if (!(d.Cells["Skill 2"].Value is DBNull) && (string)d.Cells["Skill 2"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]], ";
                    
                    if (!(d.Cells["Skill 3"].Value is DBNull) && (string)d.Cells["Skill 3"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]], ";

                    if (!(d.Cells["Red Awaken"].Value is DBNull) && (string)d.Cells["Red Awaken"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Red Archetype), ";

                    if (!(d.Cells["Teal Awaken"].Value is DBNull) && (string)d.Cells["Teal Awaken"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Teal Archetype), ";

                    if (!(d.Cells["Yellow Awaken"].Value is DBNull) && (string)d.Cells["Yellow Awaken"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Yellow Archetype), ";

                    if (!(d.Cells["Purple Awaken"].Value is DBNull) && (string)d.Cells["Purple Awaken"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Purple Archetype), ";

                    if (!(d.Cells["Red Gacha"].Value is DBNull) && (string)d.Cells["Red Gacha"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Red Archetype), ";

                    if (!(d.Cells["Teal Gacha"].Value is DBNull) && (string)d.Cells["Teal Gacha"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Teal Archetype), ";

                    if (!(d.Cells["Yellow Gacha"].Value is DBNull) && (string)d.Cells["Yellow Gacha"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Yellow Archetype), ";

                    if (!(d.Cells["Purple Gacha"].Value is DBNull) && (string)d.Cells["Purple Gacha"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Purple Archetype), ";

                    if (trans[skillName].EndsWith(", "))
                    {
                        trans[skillName].Remove(trans[skillName].Length - 2, 2);
                        trans[skillName] += " ";
                    }
                }
            }

            return trans;
        }

        //Loads our Skill from a DataGridRow
        private static Skill LoadSkill(DataGridViewRow row, Dictionary<string, string> learnedBy, Dictionary<string, string> transferableFrom)
        {
            var lb = "";
            var tf = "";

            if (row.Cells["Name"].Value != null)
            {
                lb = learnedBy[(string)row.Cells["Name"].Value];
                tf = transferableFrom[(string)row.Cells["Name"].Value];
            }

            return new Skill
            {
                Name = row.Cells["Name"].Value is DBNull ? "" : (string)row.Cells["Name"].Value,
                Element = row.Cells["Element"].Value is DBNull ? "" : (string)row.Cells["Element"].Value,
                Cost = row.Cells["Cost"].Value is DBNull ? "" : (string)row.Cells["Cost"].Value,
                Description = row.Cells["Description"].Value is DBNull ? "" : (string)row.Cells["Description"].Value,
                Target = row.Cells["Target"].Value is DBNull ? "" : (string)row.Cells["Target"].Value,
                Sp = row.Cells["Skill Points"].Value is DBNull ? "" : (string)row.Cells["Skill Points"].Value,
                LearnedBy = lb,
                TransferableFrom = tf
            };
        }

        #endregion
    }

    #region Structs

    //Struct to hold our Skill Data
    public struct Skill
    {
        public string Name;
        public string Element;
        public string Cost;
        public string Description;
        public string Target;
        public string Sp;
        public string LearnedBy;
        public string TransferableFrom;

        //Creates a Wiki String for Individuals in a Comp
        public string CreateWikiStringComp()
        {
            return "|-" + "\r\n" +
                   "|[[" + Name + "]]" + "\r\n" +
                   "|" + Cost + "\r\n" +
                   "|" + Description + "\r\n" +
                   "|" + Target + "\r\n" +
                   "|" + Sp + "\r\n" +
                   "|" + LearnedBy + "\r\n" +
                   "|" + TransferableFrom + "\r\n";
        }

        //Creates a Wiki string for Individual by themselves
        public string CreateWikiStringIndividual()
        {
            return "{{SkillTable\r\n" +
                    "|skill=" + Name + "\r\n" +
                    "|type=" + Element + "\r\n" +
                    "|cost=" + Cost + "\r\n" +
                    "|sp=" + Sp + "\r\n" +
                    "|target=" + Target + "\r\n" +
                    "|description" + Description + "\r\n" +
                    "}}";
        }
    }

    #endregion
}
