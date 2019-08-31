using System;
using System.Collections.Generic;
using System.Configuration;
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
        public static void ExportSkills(IEnumerable<DataGridViewRow> skills, IEnumerable<DataGridViewRow> demons, IEnumerable<DataGridViewRow> enumerable, bool oneFile, string path)
        {
            var transferableFrom = GetTransferableFrom(skills, demons);
            var learnedBy = GetLearnedBy(skills, demons);
            

            var filePath = Path.Combine(path, "SkillData");
            Directory.CreateDirectory(filePath);

            var data = "";

            if (oneFile)
            {
                data += GetSkillDataByElement("physical", "Physical", skills, learnedBy, transferableFrom, demons);
                data += GetSkillDataByElement("fire", "Fire", skills, learnedBy, transferableFrom, demons);
                data += GetSkillDataByElement("ice", "Ice", skills, learnedBy, transferableFrom, demons);
                data += GetSkillDataByElement("electricity", "Electricity", skills, learnedBy, transferableFrom, demons);
                data += GetSkillDataByElement("force", "Force", skills, learnedBy, transferableFrom, demons);
                data += GetSkillDataByElement("light", "Light", skills, learnedBy, transferableFrom, demons);
                data += GetSkillDataByElement("dark", "Dark", skills, learnedBy, transferableFrom, demons);
                data += GetSkillDataByElement("almighty", "Almighty", skills, learnedBy, transferableFrom, demons);
                data += GetSkillDataByElement("ailment", "Status Ailment", skills, learnedBy, transferableFrom, demons);
                data += GetSkillDataByElement("recovery", "Recovery", skills, learnedBy, transferableFrom, demons);
                data += GetSkillDataByElement("support", "Support", skills, learnedBy, transferableFrom, demons);
                data += GetSkillDataByElement("passive", "Passive", skills, learnedBy, transferableFrom, demons);
            }
            else
            {
                foreach (var skillRow in skills)
                {
                    if (skillRow.Cells["Name"].Value == null)
                        continue;

                    var skill = LoadSkill(skillRow, learnedBy, transferableFrom, demons);

                    data = skill.CreateWikiStringIndividual();

                    foreach(var s in skills)
                    {
                        var skillName = (string)s.Cells["Name"].Value;
                        if (skillName == null)
                            continue;
                        data.Replace(skillName, "[[" + skillName + "]]");
                    }

                    File.WriteAllText(filePath + "\\" + skill.Name + ".txt", data, Encoding.UTF8);


                    var data2 = skill.CreateWikiStringDemons();

                    File.WriteAllText(filePath + "\\" + skill.Name + "-Demons.txt", data2, Encoding.UTF8);
                }
            }

            if (oneFile)
            {
                File.WriteAllText(filePath + "\\" + ConfigurationManager.AppSettings["skillWikiPageName"] + ".txt", data, Encoding.UTF8);
            }
        }

        #endregion

        #region Private Methods

        //Creates and returns a set of Skill Data based on Element Type passed
        private static string GetSkillDataByElement(string elementType, string elementName, IEnumerable<DataGridViewRow> skills, Dictionary<string, string> learnedBy, Dictionary<string, string> transferableFrom, IEnumerable<DataGridViewRow> demons)
        {
            var selectedSkills = skills.Where(s => (string)s.Cells["Element"].Value == elementType);

            var skillData = "";

            GetSkillDataByTarget(ref skillData, "Single Party Member", selectedSkills, learnedBy, transferableFrom, demons);
            GetSkillDataByTarget(ref skillData, "All Party Members", selectedSkills, learnedBy, transferableFrom, demons);
            GetSkillDataByTarget(ref skillData, "Single Enemy", selectedSkills, learnedBy, transferableFrom, demons);            
            GetSkillDataByTarget(ref skillData, "All Enemies", selectedSkills, learnedBy, transferableFrom, demons);
            GetSkillDataByTarget(ref skillData, "Random Enemies", selectedSkills, learnedBy, transferableFrom, demons);
            GetSkillDataByTarget(ref skillData, "Self", selectedSkills, learnedBy, transferableFrom, demons);
            GetSkillDataByTarget(ref skillData, "Universal", selectedSkills, learnedBy, transferableFrom, demons);            

            return SkillCompSections(skillData, elementName);
        }

        //Loops through list of skills and extracts data for the specific target type
        private static void GetSkillDataByTarget(ref string skillData, string targetType, IEnumerable<DataGridViewRow> selectedSkills, Dictionary<string, string> learnedBy, Dictionary<string, string> transferableFrom, IEnumerable<DataGridViewRow> demons)
        {
            foreach (var skillRow in selectedSkills.Where(s => !(s.Cells["Target"].Value is DBNull) && (string)s.Cells["Target"].Value == targetType).OrderBy(o => (string)o.Cells["Cost"].Value))
            {
                var skill = LoadSkill(skillRow, learnedBy, transferableFrom, demons);

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
                        trans[skillName] += "[[" + demonName + "]] (Clear Gacha),";
                    
                    if (!(d.Cells["Red Gacha"].Value is DBNull) && (string)d.Cells["Red Gacha"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Red Gacha), ";

                    if (!(d.Cells["Teal Gacha"].Value is DBNull) && (string)d.Cells["Teal Gacha"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Teal Gacha), ";

                    if (!(d.Cells["Yellow Gacha"].Value is DBNull) && (string)d.Cells["Yellow Gacha"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Yellow Gacha), ";

                    if (!(d.Cells["Purple Gacha"].Value is DBNull) && (string)d.Cells["Purple Gacha"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Purple Gacha), ";
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
                    
                    if (!(d.Cells["Skill 2"].Value is DBNull) && (string)d.Cells["Skill 2"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Innate), ";
                    
                    if (!(d.Cells["Skill 3"].Value is DBNull) && (string)d.Cells["Skill 3"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Innate), ";

                    if (!(d.Cells["Clear Awaken"].Value is DBNull) && (string)d.Cells["Clear Awaken"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Clear Archetype), ";

                    if (!(d.Cells["Red Awaken"].Value is DBNull) && (string)d.Cells["Red Awaken"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Red Archetype), ";

                    if (!(d.Cells["Teal Awaken"].Value is DBNull) && (string)d.Cells["Teal Awaken"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Teal Archetype), ";

                    if (!(d.Cells["Yellow Awaken"].Value is DBNull) && (string)d.Cells["Yellow Awaken"].Value == skillName)
                        trans[skillName] += "[[" + demonName + "]] (Yellow Archetype), ";

                    if (!(d.Cells["Purple Awaken"].Value is DBNull) && (string)d.Cells["Purple Awaken"].Value == skillName)
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

        public static Skill LoadSkill(DataGridViewRow row, IEnumerable<DataGridViewRow> demons)
        {
            return LoadSkill(row, new Dictionary<string, string>(), new Dictionary<string, string>(), demons);
        }

        //Loads our Skill from a DataGridRow
        private static Skill LoadSkill(DataGridViewRow row, Dictionary<string, string> learnedBy, Dictionary<string, string> transferableFrom, IEnumerable<DataGridViewRow> demons)
        {
            var lb = "";
            var tf = "";

            if (row.Cells["Name"].Value != null)
            {
                if (learnedBy.Count > 0 && transferableFrom.Count > 0)
                {
                    lb = learnedBy[(string)row.Cells["Name"].Value];
                    tf = transferableFrom[(string)row.Cells["Name"].Value];
                }
            }            

            var name = row.Cells["Name"].Value is DBNull ? "" : (string)row.Cells["Name"].Value;

            if (demons.Any(d => (string)d.Cells["Name"].Value == name))            
                name = name + " (Skill)";            

            return new Skill
            {
                Name = name,
                Element = row.Cells["Element"].Value is DBNull ? "" : (string)row.Cells["Element"].Value,
                Cost = row.Cells["Cost"].Value is DBNull ? "" : (string)row.Cells["Cost"].Value,
                Description = row.Cells["Description"].Value is DBNull ? "" : (string)row.Cells["Description"].Value,
                Target = row.Cells["Target"].Value is DBNull ? "" : (string)row.Cells["Target"].Value,
                Sp = row.Cells["Skill Points"].Value is DBNull ? "" : (string)row.Cells["Skill Points"].Value,
                LearnedBy = lb.Trim(),
                TransferableFrom = tf.Trim(),
                ExtractExclusive = row.Cells["ExtractExclusive"].Value != null ? false : (bool)row.Cells["ExtractExclusive"].Value,
                DuelExclusive = row.Cells["DuelExclusive"].Value != null ? false : (bool)row.Cells["DuelExclusive"].Value,
                ExtractTransfer = row.Cells["ExtractTransfer"].Value != null ? false : (bool)row.Cells["ExtractTransfer"].Value,
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
        public bool ExtractExclusive;
        public bool DuelExclusive;
        public bool ExtractTransfer;

        //Creates a Wiki String for Individuals in a Comp
        public string CreateWikiStringComp()
        {
            var sp = Sp == "" ? "<nowiki>-</nowiki>" : Sp;
            var description = Description.Replace("\\n\\n", "\\n").Replace("\\n", "</nowiki><br>" + Environment.NewLine + "<nowiki>");
            return "|-" + Environment.NewLine +
                   "|[[" + Name.Replace("[", "(").Replace("]", ")") + "]]" + Environment.NewLine +
                   "|" + Cost + Environment.NewLine +
                   "|" + "<nowiki>" + description + "</nowiki>" + Environment.NewLine +
                   "|" + Target + Environment.NewLine +
                   "|" + sp + Environment.NewLine +
                   "|style=\"width:15%\"|" + LearnedBy.Replace(" (Innate)", "") + Environment.NewLine +
                   "|style=\"width:15%\"|" + TransferableFrom.Replace(" (Clear Gacha)", "") + Environment.NewLine;
        }

        //Creates a Wiki string for Individual by themselves
        public string CreateWikiStringIndividual()
        {
            var sp = Sp == "" ? "<nowiki>-</nowiki>" : Sp;

            //Split Levels and Description
            var description = "";
            var levels = "";
            var splitPoint = Description.IndexOf("\\n\\n");
            if(splitPoint == -1)
            {
                description = Description;
            }
            else
            {
                levels = Description.Substring(Description.IndexOf("\\n\\n"));
                description = Description.Substring(0, Description.IndexOf("\\n\\n"));
            }


            //var levels = Description.Substring(Description.IndexOf("\\n\\n"));
            //var description = Description.Substring(0, Description.IndexOf("\\n\\n"));
            description = description.Replace("\\n", "</nowiki><br>" + Environment.NewLine + "<nowiki>");
            levels = levels.Replace("\\n", "</nowiki><br>" + Environment.NewLine + "<nowiki>");

            return "{{SkillTable\r\n" +
                    "|skill=" + Name.Replace("[", "(").Replace("]", ")") + Environment.NewLine +
                    "|type=" + Element + Environment.NewLine +
                    "|cost=" + Cost.Replace(" MP", "") + Environment.NewLine +
                    "|sp=" + sp + Environment.NewLine +
                    "|target=" + Target + Environment.NewLine +
                    "|description=" + "<nowiki>" + description + "</nowiki>" + Environment.NewLine +
                    "|levels=" + "<nowiki>" + levels + "</nowiki>" + Environment.NewLine +
                    "|icon=" + "{{{icon}}}" + Environment.NewLine +
                    "}}" + Environment.NewLine + Environment.NewLine +
                    "{{ " +
                    "#ifeq: {{{icon}}} " +
                    "| yes " +
                    "| " +
                    "| " +
                    "{{:" + Name.Replace("[", "(").Replace("]", ")") + "/Demons}}" +
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

            var inateSkills = GetDemonsWithSkill(LearnedBy, "(Innate)");

            clearTransfer = GetDemonsWithSkill(TransferableFrom, "(Clear Gacha)");
            redTransfer = GetDemonsWithSkill(TransferableFrom, "(Red Gacha)");
            yellowTransfer = GetDemonsWithSkill(TransferableFrom, "(Yellow Gacha)");
            purpleTransfer = GetDemonsWithSkill(TransferableFrom, "(Purple Gacha)");
            tealTransfer = GetDemonsWithSkill(TransferableFrom, "(Teal Gacha)");

            clearLearnedBy = GetDemonsWithSkill(LearnedBy, "(Clear Archetype)");
            redLearnedBy = GetDemonsWithSkill(LearnedBy, "(Red Archetype)");
            yellowLearnedBy = GetDemonsWithSkill(LearnedBy, "(Yellow Archetype)");
            purpleLearnedBy = GetDemonsWithSkill(LearnedBy, "(Purple Archetype)");
            tealLearnedBy = GetDemonsWithSkill(LearnedBy, "(Teal Archetype)");
            
            return "{{TransferTable\r\n" +
                   "|title=Demons to transfer skill from\r\n" +
                   "|type=Default / Gacha Archetype\r\n" +
                   "|d0=" + clearTransfer + "\r\n" +
                   "|d0r=" + redTransfer + "\r\n" +
                   "|d0y=" + yellowTransfer + "\r\n" +
                   "|d0p=" + purpleTransfer + "\r\n" +
                   "|d0t=" + tealTransfer + "\r\n" +
                   "|}}" + "\r\n" +
                   "\r\n" +
                   "{{OwnedTable\r\n" +
                   "|title=Demons with skill\r\n" +
                   "|type=Awakened Archetype\r\n" +
                   "|d0i=" + inateSkills.Replace(" (Innate)", "") + "\r\n" + 
                   "|d0=" + clearLearnedBy + "\r\n" +
                   "|d0r=" + redLearnedBy + "\r\n" +
                   "|d0y=" + yellowLearnedBy + "\r\n" +
                   "|d0p=" + purpleLearnedBy + "\r\n" +
                   "|d0t=" + tealLearnedBy + "\r\n" +
                   "|}}";
        }
    }

    #endregion
}
