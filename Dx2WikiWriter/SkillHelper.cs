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

            if (oneFile)
            {
                data += GetSkillDataByElement("phys", "Physical", skills, learnedBy, transferableFrom);
                data += GetSkillDataByElement("fire", "Fire", skills, learnedBy, transferableFrom);
                data += GetSkillDataByElement("ice", "Ice", skills, learnedBy, transferableFrom);
                data += GetSkillDataByElement("elec", "Electricity", skills, learnedBy, transferableFrom);
                data += GetSkillDataByElement("force", "Force", skills, learnedBy, transferableFrom);
                data += GetSkillDataByElement("light", "Light", skills, learnedBy, transferableFrom);
                data += GetSkillDataByElement("dark", "Dark", skills, learnedBy, transferableFrom);
                data += GetSkillDataByElement("almighty", "Almighty", skills, learnedBy, transferableFrom);
                data += GetSkillDataByElement("ailment", "Status Ailment", skills, learnedBy, transferableFrom);
                data += GetSkillDataByElement("support", "Support", skills, learnedBy, transferableFrom);
                data += GetSkillDataByElement("passive", "Passive", skills, learnedBy, transferableFrom);
            }
            else
            {
                foreach (var skillRow in skills)
                {
                    var skill = LoadSkill(skillRow, learnedBy, transferableFrom);

                    if (skill.Name == null)
                        continue;

                    data = skill.CreateWikiStringIndividual();

                    File.WriteAllText(filePath + "\\" + skill.Name + ".txt", data, Encoding.UTF8);


                    var data2 = skill.CreateWikiStringDemons();

                    File.WriteAllText(filePath + "\\" + skill.Name + "-Demons.txt", data2, Encoding.UTF8);
                }
            }

            if (oneFile)
            {
                File.WriteAllText(filePath + "\\Skill Comp.txt", data, Encoding.UTF8);
            }
        }

        #endregion

        #region Private Methods

        //Creates and returns a set of Skill Data based on Element Type passed
        private static string GetSkillDataByElement(string elementType, string elementName, IEnumerable<DataGridViewRow> skills, Dictionary<string, string> learnedBy, Dictionary<string, string> transferableFrom)
        {
            var selectedSkills = skills.Where(s => (string)s.Cells["Element"].Value == elementType);

            var skillData = "";

            GetSkillDataByTarget(ref skillData, "Single Enemy", selectedSkills, learnedBy, transferableFrom);
            GetSkillDataByTarget(ref skillData, "All Enemies", selectedSkills, learnedBy, transferableFrom);
            GetSkillDataByTarget(ref skillData, "Random Enemies", selectedSkills, learnedBy, transferableFrom);
            GetSkillDataByTarget(ref skillData, "Self", selectedSkills, learnedBy, transferableFrom);

            return SkillCompSections(skillData, elementName);
        }

        //Loops through list of skills and extracts data for the specific target type
        private static void GetSkillDataByTarget(ref string skillData, string targetType, IEnumerable<DataGridViewRow> selectedSkills, Dictionary<string, string> learnedBy, Dictionary<string, string> transferableFrom)
        {
            foreach (var skillRow in selectedSkills.Where(s => !(s.Cells["Target"].Value is DBNull) && (string)s.Cells["Target"].Value == targetType).OrderBy(o => (string)o.Cells["Cost"].Value))
            {
                var skill = LoadSkill(skillRow, learnedBy, transferableFrom);

                if (skill.Name == null)
                    continue;

                skillData += skill.CreateWikiStringComp();
            }
        }
        
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
                }
            }

            foreach (var t in trans.Keys.ToList())
            {
                if (trans[t].EndsWith(", "))
                {
                    trans[t] = trans[t].Remove(trans[t].Length - 2, 2);
                    trans[t] += " ";
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
                }
            }

            foreach (var t in trans.Keys.ToList())
            {
                if (trans[t].EndsWith(", "))
                {
                    trans[t] = trans[t].Remove(trans[t].Length - 2, 2);
                    trans[t] += " ";
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
                LearnedBy = lb.Trim(),
                TransferableFrom = tf.Trim()
            };
        }

        //Adds the beganning and ending sections to a Skill Comp
        private static string SkillCompSections(string data, string type)
        {
            return "===" + type + " Skills===" + Environment.NewLine +
                   "{| class=\"wikitable sortable\" style=\"width: 100%;\"" + Environment.NewLine +
                   "|-" + Environment.NewLine + 
                   Environment.NewLine +
                   "!Name !!MP Cost !!Effect !!Target !!Skill Points !!Learned By !!Transferable From" + Environment.NewLine +
                   "|- style=\"vertical-align:middle;\"" + Environment.NewLine +
                   data +
                   "|}" + Environment.NewLine +
                   "<br>" + Environment.NewLine +
                   Environment.NewLine;
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
            return "|-" + Environment.NewLine +
                   "|[[" + Name.Replace("[", "(").Replace("]", ")") + "]]" + Environment.NewLine +
                   "|" + Cost + Environment.NewLine +
                   "|" + "<nowiki>" + Description.Replace("\\n", "</nowiki><br>" + Environment.NewLine + "<nowiki>") + "</nowiki>" + Environment.NewLine +
                   "|" + Target + Environment.NewLine +
                   "|" + Sp + Environment.NewLine +
                   "|style=\"width:15%\"|" + LearnedBy + Environment.NewLine +
                   "|style=\"width:15%\"|" + TransferableFrom + Environment.NewLine;
        }

        //Creates a Wiki string for Individual by themselves
        public string CreateWikiStringIndividual()
        {
            return "{{SkillTable\r\n" +
                    "|skill=" + Name.Replace("[", "(").Replace("]", ")") + Environment.NewLine +
                    "|type=" + Element + Environment.NewLine +
                    "|cost=" + Cost + Environment.NewLine +
                    "|sp=" + Sp + Environment.NewLine +
                    "|target=" + Target + Environment.NewLine +
                    "|description=" + "<nowiki>" + Description.Replace("\\n", "</nowiki><br>" + Environment.NewLine + "<nowiki>") + "</nowiki>" + Environment.NewLine + 
                    "}}";
        }

        //Converts a string array into a string
        private static string ConvertStringArrayToString(string[] array)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value.Trim());
                builder.Append(", ");
            }

            //Remove ', ' from end
            if (array.Length >= 1)
                builder = builder.Remove(builder.Length - 2, 2);

            return builder.ToString();
        }

        //Returns a list of demons who have learned or have skills as transfers
        private static string GetDemonsWithSkill(string data, string type)
        {
            return ConvertStringArrayToString(data.Split(',').Where(s => s.Contains(type)).ToArray()).Replace(" " + type, "");
        }

        //Creates a Wiki string for Individual skills and what demons can transfer/learn them
        public string CreateWikiStringDemons()
        {
            var clearTransfer = "";
            var redTransfer = "";
            var yellowTransfer = "";
            var tealTransfer = "";
            var purpleTransfer = "";

            var clearLearnedBy = "";
            var redLearnedBy = "";
            var yellowLearnedBy = "";
            var tealLearnedBy = "";
            var purpleLearnedBy = "";

            clearTransfer = GetDemonsWithSkill(TransferableFrom, "(Clear Archetype)");
            redTransfer = GetDemonsWithSkill(TransferableFrom, "(Red Archetype)");
            yellowTransfer = GetDemonsWithSkill(TransferableFrom, "(Yellow Archetype)");
            purpleTransfer = GetDemonsWithSkill(TransferableFrom, "(Purple Archetype)");
            tealTransfer = GetDemonsWithSkill(TransferableFrom, "(Teal Archetype)");

            clearLearnedBy = GetDemonsWithSkill(LearnedBy, "(Clear Archetype)");
            redLearnedBy = GetDemonsWithSkill(LearnedBy, "(Red Archetype)");
            yellowLearnedBy = GetDemonsWithSkill(LearnedBy, "(Yellow Archetype)");
            purpleLearnedBy = GetDemonsWithSkill(LearnedBy, "(Purple Archetype)");
            tealLearnedBy = GetDemonsWithSkill(LearnedBy, "(Teal Archetype)");
            
            return "{{TransferTable\r\n" +
                   "|title=Demons to transfer skill from\r\n" +
                   "|type=Default / Gacha Archetype\r\n" +
                   "|d0c=" + clearTransfer + "\r\n" +
                   "|d0r=" + redTransfer + "\r\n" +
                   "|d0y=" + yellowTransfer + "\r\n" +
                   "|d0p=" + purpleTransfer + "\r\n" +
                   "|d0t=" + tealTransfer + "\r\n" +
                   "|}}" + "\r\n" +
                   "\r\n" +
                   "{{OwnedTable\r\n" +
                   "|title=Demons with skill\r\n" +
                   "|type=Awakened Archetype\r\n" +
                   "|d0c=" + clearLearnedBy + "\r\n" +
                   "|d0r=" + redLearnedBy + "\r\n" +
                   "|d0y=" + yellowLearnedBy + "\r\n" +
                   "|d0p=" + purpleLearnedBy + "\r\n" +
                   "|d0t=" + tealLearnedBy + "\r\n" +
                   "|}}";
        }
    }

    #endregion
}
