using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Dx2WikiWriter
{
    public class DBManager
    {
        //Loads our DB from the path into a data grid view
        public DataTable LoadDB(string path)
        {
            return ReadCSV(path);
        }
        
        public DataTable ReadCSV(string filePath)
        {
            var dt = new DataTable();

            try
            {
                // Creating the columns
                File.ReadLines(filePath).Take(1)
                    .SelectMany(x => x.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    .ToList()
                    .ForEach(x => dt.Columns.Add(x.Trim()));

                // Adding the rows
                File.ReadLines(filePath).Skip(1)
                    .Select(x => x.Split(','))
                    .ToList()
                    .ForEach(line => dt.Rows.Add(line));

                return dt;
            }
            catch (Exception e)
            {
                MessageBox.Show("Something is wrong with the input file. Contact Alenael with the message below if required.\r\n\r\n" + e.Message, "Error");
                return null;
            }            
        }

        //Creates a demon object from a data grid view row
        public Demon LoadDemon(DataGridViewRow row)
        {
            return new Demon
            {
                Name = (string)row.Cells["Name"].Value,
                Rarity = (string)row.Cells["Rarity"].Value,
                Race = (string)row.Cells["Race"].Value,
                Ai = (string)row.Cells["Type"].Value,
                Grade = (string)row.Cells["Grade"].Value,

                Str = Convert.ToInt32(row.Cells["6★ Strength"].Value),
                Mag = Convert.ToInt32(row.Cells["6★ Magic"].Value),
                Vit = Convert.ToInt32(row.Cells["6★ Vitality"].Value),
                Agi = Convert.ToInt32(row.Cells["6★ Agility"].Value),
                Luck = Convert.ToInt32(row.Cells["6★ Luck"].Value),

                Fire = LoadResist((string)row.Cells["Fire"].Value),
                Dark = LoadResist((string)row.Cells["Dark"].Value),
                Light = LoadResist((string)row.Cells["Light"].Value),
                Elec = LoadResist((string)row.Cells["Elec"].Value),
                Ice = LoadResist((string)row.Cells["Ice"].Value),
                Force = LoadResist((string)row.Cells["Force"].Value),
                Phys = LoadResist((string)row.Cells["Phys"].Value),

                Skill1 = (string)row.Cells["Skill 1"].Value,
                Skill2 = (string)row.Cells["Skill 2"].Value,
                Skill3 = (string)row.Cells["Skill 3"].Value,

                AwakenC = (string)row.Cells["Clear Awaken"].Value,
                AwakenR = (string)row.Cells["Red Awaken"].Value,
                AwakenP = (string)row.Cells["Purple Awaken"].Value,
                AwakenY = (string)row.Cells["Yellow Awaken"].Value,
                AwakenT = (string)row.Cells["Teal Awaken"].Value,

                GachaR = (string)row.Cells["Red Gacha"].Value,
                GachaP = (string)row.Cells["Purple Gacha"].Value,
                GachaY = (string)row.Cells["Yellow Gacha"].Value,
                GachaT = (string)row.Cells["Teal Gacha"].Value,


            };
        }

        //Returns a value based on what its passed
        public string LoadResist(string value)
        {
            if (value == "" || value == null)
                return "<nowiki>-</nowiki>";
            else
            {
                var type = "";

                switch(value)
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

        //Exports a list of demons as files
        public void ExportDemons(IEnumerable<DataGridViewRow> selectedDemons, bool oneFile, string path)
        {
            var sortedDemons = selectedDemons.OrderByDescending(c => Convert.ToInt32(c.Cells["Grade"].Value));

            var filePath = Path.Combine(path, "DemonData");
            Directory.CreateDirectory(filePath);

            var demonData = "";

            foreach (var demon in sortedDemons)
            {
                var d = LoadDemon(demon);

                if (d.Name == null)
                    continue;

                if (oneFile)
                {
                    if (sortedDemons.ElementAt(0) != demon)
                        demonData = demonData + "\r\n";

                    demonData = demonData + d.CreateWikiStringComp();
                }
                else
                {
                    demonData = d.CreateWikiStringIndividual();

                    File.WriteAllText(filePath + "\\" + d.Name + ".txt", demonData);
                }
            }

            if (oneFile)
            {
                demonData = "{| class=\"wikitable sortable\" style=\"text - align:center; width: 100 %;\"\r\n" +
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
                            demonData + "}";

                File.WriteAllText(filePath + "\\Demon Comp.txt", demonData);                
            }
        }
    }

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
        
        //Creates a Wiki String based on the info in this object and returns it
        public string CreateWikiStringComp()
        {
            return "|{{ListDemon|demon=" + Name + "|race= " + Race + "|grade= " + Grade + "|rarity= " + Rarity + "|ai= " + Ai + "|" +
                     "hp= " + HP + "|str= " + Str + "|mag= " + Mag + "|vit= " + Vit + "|agi= " + Agi + "|luck= " + Luck + "" +
                     "|phys= " + Phys + "|fire= " + Fire + "|ice= " + Ice + "|elec= " + Elec + "" +
                     "|force= " + Force + "|light= " + Light + "|dark= " + Dark +
                     "|patk= " + PAtk + "|pdef= " + PDef + "|matk= " + MAtk + "|mdef= " + MDef + "}}\r\n" +
                     "|- style=\"vertical-align:middle;\"";
        }

        public string CreateWikiStringIndividual()
        {
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
                "|max_hp= " + HP + "\r\n" +
                "|max_str= " + Str + "\r\n" +
                "|max_mag= " + Mag + "\r\n" +
                "|max_vit= " + Vit + "\r\n" +
                "|max_agi= " + Agi + "\r\n" +
                "|max_luck= " + Luck + "\r\n" +
                "|patk= " + PAtk + "\r\n" +
                "|pdef= " + PDef + "\r\n" +
                "|matk= " + MAtk + "\r\n" +
                "|mdef= " + MDef + "\r\n" +
                "|transfer_skill= " + Skill1 + "\r\n" +
                "|innate_skill1= " + Skill2 + "\r\n" +
                "|innate_skill2= " + Skill3 + "\r\n" +
                "|a_clear= " + AwakenC + "\r\n" +
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
}
