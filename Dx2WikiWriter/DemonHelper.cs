using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dx2WikiWriter
{
    public static class DemonHelper
    {
        #region Public Methods

        //Exports a list of demons as files
        public static void ExportDemons(IEnumerable<DataGridViewRow> selected, IEnumerable<DataGridViewRow> all, bool oneFile, string path)
        {
            var ranks = GetRanks(selected, all);

            var sortedDemons = selected.OrderByDescending(c => Convert.ToInt32(c.Cells["Grade"].Value));

            var filePath = Path.Combine(path, "DemonData");
            Directory.CreateDirectory(filePath);

            var data = "";

            foreach (var s in sortedDemons)
            {
                var d = LoadDemon(s);
                var aether = GetAetherCosts(d, s);
                d.SetAetherCosts(aether);

                if (d.Name == null)
                    continue;

                if (oneFile)
                {
                    if (sortedDemons.ElementAt(0) != s)
                        data = data + "\r\n";

                    data = data + d.CreateWikiStringComp();
                }
                else
                {
                    data = d.CreateWikiStringIndividual(ranks);

                    File.WriteAllText(filePath + "\\" + d.Name + ".txt", data, System.Text.Encoding.UTF8);
                }
            }

            if (oneFile)
            {
                data = "{| class=\"wikitable sortable\" style=\"text - align:center; width: 100 %;\"\r\n" +
                            "|- \r\n" +
                            "\r\n" +
                            "! Name !!Race !!Grade !!Rarity !!AI !!6★ HP !!6★ Strength !!6★ Magic\r\n" +
                            "!6★ Vitality !!6★ Agility !!6★ Luck\r\n" +
                            "![[File: Physical.png | 20px | link =]] !![[File: Fire.png | 20px | link =]]\r\n" +
                            "![[File: Ice.png | 20px | link =]] !![[File: Electricity.png | 20px | link =]]\r\n" +
                            "![[File: Force.png | 20px | link =]] !![[File: Light.png | 20px | link =]]\r\n" +
                            "![[File: Dark.png | 20px | link =]] !!6★ PATK\r\n" +
                            "!6★ PDEF !!6★ MATK !!6★ MDEF\r\n" +
                            "|- style = \"vertical-align:middle;\"\r\n" +
                            data + "}";

                File.WriteAllText(filePath + "\\Demon Comp.txt", data, Encoding.UTF8);
            }
        }

        //Creates a demon object from a data grid view row
        public static Demon LoadDemon(DataGridViewRow row)
        {
            return new Demon
            {
                Name = row.Cells["Name"].Value is DBNull ? "" : (string)row.Cells["Name"].Value,
                Rarity = row.Cells["Rarity"].Value is DBNull ? "" : (string)row.Cells["Rarity"].Value,
                Race = row.Cells["Race"].Value is DBNull ? "" : (string)row.Cells["Race"].Value,
                Ai = row.Cells["Type"].Value is DBNull ? "" : (string)row.Cells["Type"].Value,
                Grade = row.Cells["Grade"].Value is DBNull ? "" : (string)row.Cells["Grade"].Value,

                Str = row.Cells["6★ Strength"].Value is DBNull ? 0 : Convert.ToInt32(row.Cells["6★ Strength"].Value),
                Mag = row.Cells["6★ Magic"].Value is DBNull ? 0 : Convert.ToInt32(row.Cells["6★ Magic"].Value),
                Vit = row.Cells["6★ Vitality"].Value is DBNull ? 0 : Convert.ToInt32(row.Cells["6★ Vitality"].Value),
                Agi = row.Cells["6★ Agility"].Value is DBNull ? 0 : Convert.ToInt32(row.Cells["6★ Agility"].Value),
                Luck = row.Cells["6★ Luck"].Value is DBNull ? 0 : Convert.ToInt32(row.Cells["6★ Luck"].Value),

                Fire = LoadResist(row.Cells["Fire"].Value is DBNull ? "" : (string)row.Cells["Fire"].Value),
                Dark = LoadResist(row.Cells["Dark"].Value is DBNull ? "" : (string)row.Cells["Dark"].Value),
                Light = LoadResist(row.Cells["Light"].Value is DBNull ? "" : (string)row.Cells["Light"].Value),
                Elec = LoadResist(row.Cells["Elec"].Value is DBNull ? "" : (string)row.Cells["Elec"].Value),
                Ice = LoadResist(row.Cells["Ice"].Value is DBNull ? "" : (string)row.Cells["Ice"].Value),
                Force = LoadResist(row.Cells["Force"].Value is DBNull ? "" : (string)row.Cells["Force"].Value),
                Phys = LoadResist(row.Cells["Phys"].Value is DBNull ? "" : (string)row.Cells["Phys"].Value),

                Skill1 = row.Cells["Skill 1"].Value is DBNull ? "" : (string)row.Cells["Skill 1"].Value,
                Skill2 = row.Cells["Skill 2"].Value is DBNull ? "" : (string)row.Cells["Skill 2"].Value,
                Skill3 = row.Cells["Skill 3"].Value is DBNull ? "" : (string)row.Cells["Skill 3"].Value,

                AwakenC = row.Cells["Clear Awaken"].Value is DBNull ? "" : (string)row.Cells["Clear Awaken"].Value,
                AwakenR = row.Cells["Red Awaken"].Value is DBNull ? "" : (string)row.Cells["Red Awaken"].Value,
                AwakenP = row.Cells["Purple Awaken"].Value is DBNull ? "" : (string)row.Cells["Purple Awaken"].Value,
                AwakenY = row.Cells["Yellow Awaken"].Value is DBNull ? "" : (string)row.Cells["Yellow Awaken"].Value,
                AwakenT = row.Cells["Teal Awaken"].Value is DBNull ? "" : (string)row.Cells["Teal Awaken"].Value,

                GachaR = row.Cells["Red Gacha"].Value is DBNull ? "" : (string)row.Cells["Red Gacha"].Value,
                GachaP = row.Cells["Purple Gacha"].Value is DBNull ? "" : (string)row.Cells["Purple Gacha"].Value,
                GachaY = row.Cells["Yellow Gacha"].Value is DBNull ? "" : (string)row.Cells["Yellow Gacha"].Value,
                GachaT = row.Cells["Teal Gacha"].Value is DBNull ? "" : (string)row.Cells["Teal Gacha"].Value,
            };
        }

        //Cheat to allow Linq in struct
        public static Rank GetMyRank(List<Rank> ranks, string name)
        {
            return ranks.Find(r => r.Name == name);
        }

        #endregion

        #region Private Methods
               
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

        //Gets ranks for selected demons
        private static List<Rank> GetRanks(IEnumerable<DataGridViewRow> selectedDemons, IEnumerable<DataGridViewRow> allDemons)
        {
            var ranks = new List<Rank>();

            if (selectedDemons.Count() > 0)
            {
                var sortedDemons = allDemons.OrderByDescending(c => Convert.ToInt32(c.Cells["6★ Strength"].Value)).ToList();

                for (var i = 0; i < selectedDemons.Count(); i++)
                {
                    var demon = LoadDemon(selectedDemons.ToList()[i]);

                    var r = new Rank() { Name = demon.Name };
                    r.Str = sortedDemons.FindIndex(a => (string)a.Cells["Name"].Value == demon.Name);

                    sortedDemons = allDemons.OrderByDescending(c => Convert.ToInt32(c.Cells["6★ Magic"].Value)).ToList();
                    r.Mag = sortedDemons.FindIndex(a => (string)a.Cells["Name"].Value == demon.Name);

                    sortedDemons = allDemons.OrderByDescending(c => Convert.ToInt32(c.Cells["6★ Vitality"].Value)).ToList();
                    r.Vit = sortedDemons.FindIndex(a => (string)a.Cells["Name"].Value == demon.Name);

                    sortedDemons = allDemons.OrderByDescending(c => Convert.ToInt32(c.Cells["6★ Luck"].Value)).ToList();
                    r.Luck = sortedDemons.FindIndex(a => (string)a.Cells["Name"].Value == demon.Name);

                    sortedDemons = allDemons.OrderByDescending(c => Convert.ToInt32(c.Cells["6★ HP"].Value)).ToList();
                    r.HP = sortedDemons.FindIndex(a => (string)a.Cells["Name"].Value == demon.Name);

                    sortedDemons = allDemons.OrderByDescending(c => Convert.ToInt32(c.Cells["6★ Agility"].Value)).ToList();
                    r.Agility = sortedDemons.FindIndex(a => (string)a.Cells["Name"].Value == demon.Name);

                    ranks.Add(r);
                }
            }

            return ranks;
        }

        //Gets all Ather Costs for a demon
        private static string[][] GetAetherCosts(Demon demon, DataGridViewRow row)
        {
            var aetherAmount = new string[4];
            var aetherType = new string[4];
            var found = 0;

            GetAether("Light", "S", row, ref aetherType, ref aetherAmount, ref found);
            GetAether("Light", "M", row, ref aetherType, ref aetherAmount, ref found);
            GetAether("Light", "L", row, ref aetherType, ref aetherAmount, ref found);
            GetAether("Lawful", "S", row, ref aetherType, ref aetherAmount, ref found);
            GetAether("Lawful", "M", row, ref aetherType, ref aetherAmount, ref found);
            GetAether("Lawful", "L", row, ref aetherType, ref aetherAmount, ref found);
            GetAether("Neutral", "S", row, ref aetherType, ref aetherAmount, ref found);
            GetAether("Neutral", "M", row, ref aetherType, ref aetherAmount, ref found);
            GetAether("Neutral", "L", row, ref aetherType, ref aetherAmount, ref found);
            GetAether("Dark", "S", row, ref aetherType, ref aetherAmount, ref found);
            GetAether("Dark", "M", row, ref aetherType, ref aetherAmount, ref found);
            GetAether("Dark", "L", row, ref aetherType, ref aetherAmount, ref found);
            GetAether("Chaotic", "S", row, ref aetherType, ref aetherAmount, ref found);
            GetAether("Chaotic", "M", row, ref aetherType, ref aetherAmount, ref found);
            GetAether("Chaotic", "L", row, ref aetherType, ref aetherAmount, ref found);
            GetAether("Witch", "S", row, ref aetherType, ref aetherAmount, ref found);
            GetAether("Witch", "L", row, ref aetherType, ref aetherAmount, ref found);
            GetAether("Soul", "S", row, ref aetherType, ref aetherAmount, ref found);
            GetAether("Soul", "L", row, ref aetherType, ref aetherAmount, ref found);

            return new[] { aetherType, aetherAmount };
        }

        //Shortcut that allows us to add different Aethers dynanimcally
        private static void GetAether(string aetherType, string aetherSize, DataGridViewRow row, ref string[] aetherTypeCol, ref string[] aetherAmountCol, ref int found)
        {
            //Return if we have all 4 Aether already
            if (found >= 4) return;

            if (!(row.Cells[aetherSize + " " + aetherType].Value is DBNull))
            {
                aetherTypeCol[found] = aetherSize + "-" + aetherType;
                aetherAmountCol[found] = (string)row.Cells[aetherSize + " " + aetherType].Value + aetherSize;
                found = found + 1;
            }
        }

        #endregion
    }

    #region Structs

    //Object to hold Rank Data
    public struct Rank
    {
        public string Name;
        public int Str;
        public int Mag;
        public int Vit;
        public int Luck;
        public int Agility;
        public int HP;
    }

    //Object to hold Demon Data
    public struct Demon
    {
        public string Name;
        public string Race;
        public string Grade;
        public string Rarity;
        public string Ai;
        public int Str;
        public int Mag;
        public int Vit;
        public int Agi;
        public int Luck;
        public string Phys;
        public string Fire;
        public string Ice;
        public string Elec;
        public string Force;
        public string Light;
        public string Dark;
        public int HP { get { return (int)(Vit * 4.7 + 50 * 7.4); } }
        public int PAtk { get { return (int)(Str * 2.1 + 50 * 5.6 + 50); } }
        public int MAtk { get { return (int)(Mag * 2.1 + 50 * 5.6 + 50); } }
        public int PDef { get { return (int)(Vit * 1.1 + Str * 0.5 + 50 * 5.6 + 50); } }
        public int MDef { get { return (int)(Vit * 1.1 + Mag * 0.5 + 50 * 5.6 + 50); } }

        public string Skill1;
        public string Skill2;
        public string Skill3;

        public string AwakenC;
        public string AwakenR;
        public string AwakenT;
        public string AwakenP;
        public string AwakenY;

        public string GachaR;
        public string GachaP;
        public string GachaT;
        public string GachaY;

        public string Awaken1;
        public string Awaken2;
        public string Awaken3;
        public string Awaken4;

        public string Awaken1Amount;
        public string Awaken2Amount;
        public string Awaken3Amount;
        public string Awaken4Amount;

        public void SetAetherCosts(string[][] aether)
        {
            Awaken1 = aether[0][0];
            Awaken2 = aether[0][1];
            Awaken3 = aether[0][2];
            Awaken4 = aether[0][3];

            Awaken1Amount = aether[1][0];
            Awaken2Amount = aether[1][1];
            Awaken3Amount = aether[1][2];
            Awaken4Amount = aether[1][3];
        }

        //Creates a Wiki String for Individuals in a Comp
        public string CreateWikiStringComp()
        {
            return "|{{ListDemon|demon=" + Name + "|race= " + Race + "|grade= " + Grade + "|rarity= " + Rarity + "|ai= " + Ai + "|" +
                     "hp= " + HP + "|str= " + Str + "|mag= " + Mag + "|vit= " + Vit + "|agi= " + Agi + "|luck= " + Luck + "" +
                     "|phys= " + Phys + "|fire= " + Fire + "|ice= " + Ice + "|elec= " + Elec + "" +
                     "|force= " + Force + "|light= " + Light + "|dark= " + Dark +
                     "|patk= " + PAtk + "|pdef= " + PDef + "|matk= " + MAtk + "|mdef= " + MDef + "}}\r\n" +
                     "|- style=\"vertical-align:middle;\"";
        }

        //Creates a Wiki string for Individual by themselves
        public string CreateWikiStringIndividual(List<Rank> ranks)
        {
            var total = ranks.Count - 1;

            return "{{DemonTabs|base{{BASENAME}} }}\r\n" +
                "{{Demon\r\n" +
                "|id=\r\n" +
                "|jpname=\r\n" +
                "|name= " + Name + "\r\n" +
                "|release_version= 1.0\r\n" +
                "|link_altema=\r\n" +
                "|art= {{PAGENAME}}.jpg\r\n" +
                "|phys= " + Phys + "\r\n" +
                "|fire= " + Fire + "\r\n" +
                "|ice= " + Ice + "\r\n" +
                "|elec= " + Elec + "\r\n" +
                "|force= " + Force + "\r\n" +
                "|light= " + Light + "\r\n" +
                "|dark= " + Dark + "\r\n" +
                "|race= " + Race + "\r\n" +
                "|grade= " + Grade + "\r\n" +
                "|rarity= " + Rarity + "\r\n" +
                "|ai= " + Ai + "\r\n" +
                "|max_hp= " + HP + " (" + DemonHelper.GetMyRank(ranks, Name).HP + "/" + total + ")\r\n" +
                "|max_str= " + Str + " (" + DemonHelper.GetMyRank(ranks, Name).Str + "/" + total + ")\r\n" +
                "|max_mag= " + Mag + " (" + DemonHelper.GetMyRank(ranks, Name).Mag + "/" + total + ")\r\n" +
                "|max_vit= " + Vit + " (" + DemonHelper.GetMyRank(ranks, Name).Vit + "/" + total + ")\r\n" +
                "|max_agi= " + Agi + " (" + DemonHelper.GetMyRank(ranks, Name).Agility + "/" + total + ")\r\n" +
                "|max_luck= " + Luck + " (" + DemonHelper.GetMyRank(ranks, Name).Luck + "/" + total + ")\r\n" +
                "|patk= " + PAtk + "\r\n" +
                "|pdef= " + PDef + "\r\n" +
                "|matk= " + MAtk + "\r\n" +
                "|mdef= " + MDef + "\r\n" +
                "|transfer_skill= " + Skill1 + "\r\n" +
                "|innate_skill1= " + Skill2 + "\r\n" +
                "|innate_skill2= " + Skill3 + "\r\n" +
                "|a_clear= " + (AwakenC == "" ? "N/A" : AwakenC) + "\r\n" +
                "|a_red= " + AwakenR + "\r\n" +
                "|a_yellow= " + AwakenY + "\r\n" +
                "|a_purple= " + AwakenP + "\r\n" +
                "|a_teal= " + AwakenT + "\r\n" +
                "|g_red= " + GachaR + "\r\n" +
                "|g_yellow= " + GachaY + "\r\n" +
                "|g_purple= " + GachaP + "\r\n" +
                "|g_teal= " + GachaT + "\r\n" +
                "|awaken1=" + Awaken1 + "|awaken2=" + Awaken2 + "|awaken3=" + Awaken3 + "|awaken4=" + Awaken4 + "|awaken1amnt=" + Awaken1Amount + "|awaken2amnt=" + Awaken2Amount + "|awaken3amnt=" + Awaken3Amount + "|awaken4amnt=" + Awaken4Amount + "\r\n" +
                "|}}\r\n" +
                "[[Category: Demons]]\r\n" +
                "[[Category: " + Race + "]]\r\n" +
                "[[Category: " + Rarity + " Star Demons]]\r\n" +
                "[[Category: " + Ai + " AI]]\r\n";
        }
    }
    
    #endregion
}
