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
    public static class SwordHelper
    {
        #region Public Methods

        //Exports a list of Swords as files
        public static void ExportSwords(IEnumerable<DataGridViewRow> selected, IEnumerable<DataGridViewRow> allSwords, bool oneFile, string path)
        {
            var sortedSwords = selected.OrderByDescending(c => Convert.ToInt32(c.Cells["Grade"].Value));

            var filePath = Path.Combine(path, "SwordData");
            Directory.CreateDirectory(filePath);

            var data = "";

            foreach (var s in sortedSwords)
            {
                var d = LoadSword(s, allSwords);

                if (d.Name == null)
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

                    File.WriteAllText(filePath + "\\" + d.Name + ".txt", data, Encoding.UTF8);
                }
            }

            if (oneFile)
            {
                data = "{| class=\"wikitable sortable\" style=\"text - align:center; width: 100 %;\"" + Environment.NewLine +
                            "|-" +
                            Environment.NewLine +
                            "! Name !!Race !!Grade !!Rarity !!AI !!6★ HP !!6★ Strength !!6★ Magic" + Environment.NewLine +
                            "!6★ Vitality !!6★ Agility !!6★ Luck" + Environment.NewLine +
                            "![[File: Physical.png | 20px | link =]] !![[File: Fire.png | 20px | link =]]" + Environment.NewLine +
                            "![[File: Ice.png | 20px | link =]] !![[File: Electricity.png | 20px | link =]]" + Environment.NewLine +
                            "![[File: Force.png | 20px | link =]] !![[File: Light.png | 20px | link =]]" + Environment.NewLine +
                            "![[File: Dark.png | 20px | link =]] !!6★ PATK" + Environment.NewLine +
                            "!6★ PDEF !!6★ MATK !!6★ MDEF" + Environment.NewLine +
                            "|- style = \"vertical-align:middle;\"" + Environment.NewLine +
                            data + "}";

                File.WriteAllText(filePath + "\\" + ConfigurationManager.AppSettings["SwordWikiPageName"] + ".txt", data, Encoding.UTF8);
            }
        }

        //Creates a Sword object from a data grid view row
        public static Sword LoadSword(DataGridViewRow row, IEnumerable<DataGridViewRow> Swords)
        {
            var SwordVersions = "";
            var name = row.Cells[0].Value is DBNull ? "" : (string)row.Cells[0].Value;


            if (!(row.Cells["Alternate Name"].Value is DBNull))
            {
                var alternateSword = (string)row.Cells["Alternate Name"].Value;
                SwordVersions = "{{SwordVersions|" + name + "|" + alternateSword + "}}\r\n";
            }

            return new Sword
            {
                Name = name,
                Rarity = row.Cells["Rarity"].Value is DBNull ? "" : (string)row.Cells["Rarity"].Value,
                Race = row.Cells["Race"].Value is DBNull ? "" : (string)row.Cells["Race"].Value,
                Ai = row.Cells["Type"].Value is DBNull ? "" : (string)row.Cells["Type"].Value,
                Grade = row.Cells["Grade"].Value is DBNull ? "" : (string)row.Cells["Grade"].Value,

                Str = row.Cells["6★ Strength"].Value is DBNull ? 0 : Convert.ToInt32(row.Cells["6★ Strength"].Value),
                Mag = row.Cells["6★ Magic"].Value is DBNull ? 0 : Convert.ToInt32(row.Cells["6★ Magic"].Value),
                Vit = row.Cells["6★ Vitality"].Value is DBNull ? 0 : Convert.ToInt32(row.Cells["6★ Vitality"].Value),
                Agi = row.Cells["6★ Agility"].Value is DBNull ? 0 : Convert.ToInt32(row.Cells["6★ Agility"].Value),
                Luck = row.Cells["6★ Luck"].Value is DBNull ? 0 : Convert.ToInt32(row.Cells["6★ Luck"].Value),

                SwordVersions = SwordVersions,

                Fire = LoadResist(row.Cells["Fire"].Value is DBNull ? "" : (string)row.Cells["Fire"].Value),
                Dark = LoadResist(row.Cells["Dark"].Value is DBNull ? "" : (string)row.Cells["Dark"].Value),
                Light = LoadResist(row.Cells["Light"].Value is DBNull ? "" : (string)row.Cells["Light"].Value),
                Elec = LoadResist(row.Cells["Elec"].Value is DBNull ? "" : (string)row.Cells["Elec"].Value),
                Ice = LoadResist(row.Cells["Ice"].Value is DBNull ? "" : (string)row.Cells["Ice"].Value),
                Force = LoadResist(row.Cells["Force"].Value is DBNull ? "" : (string)row.Cells["Force"].Value),
                Phys = LoadResist(row.Cells["Phys"].Value is DBNull ? "" : (string)row.Cells["Phys"].Value),

                Skill1 = FixSkillsNamedAsSwords(row.Cells["Skill 1"].Value is DBNull ? "" : (string)row.Cells["Skill 1"].Value, Swords),
                Skill2 = FixSkillsNamedAsSwords(row.Cells["Skill 2"].Value is DBNull ? "" : (string)row.Cells["Skill 2"].Value, Swords),
                Skill3 = FixSkillsNamedAsSwords(row.Cells["Skill 3"].Value is DBNull ? "" : (string)row.Cells["Skill 3"].Value, Swords),

                AwakenC = FixSkillsNamedAsSwords(row.Cells["Clear Awaken"].Value is DBNull ? "" : (string)row.Cells["Clear Awaken"].Value, Swords),
                AwakenR = FixSkillsNamedAsSwords(row.Cells["Red Awaken"].Value is DBNull ? "" : (string)row.Cells["Red Awaken"].Value, Swords),
                AwakenP = FixSkillsNamedAsSwords(row.Cells["Purple Awaken"].Value is DBNull ? "" : (string)row.Cells["Purple Awaken"].Value, Swords),
                AwakenY = FixSkillsNamedAsSwords(row.Cells["Yellow Awaken"].Value is DBNull ? "" : (string)row.Cells["Yellow Awaken"].Value, Swords),
                AwakenT = FixSkillsNamedAsSwords(row.Cells["Teal Awaken"].Value is DBNull ? "" : (string)row.Cells["Teal Awaken"].Value, Swords),

                GachaR = FixSkillsNamedAsSwords(row.Cells["Red Gacha"].Value is DBNull ? "" : (string)row.Cells["Red Gacha"].Value, Swords),
                GachaP = FixSkillsNamedAsSwords(row.Cells["Purple Gacha"].Value is DBNull ? "" : (string)row.Cells["Purple Gacha"].Value, Swords),
                GachaY = FixSkillsNamedAsSwords(row.Cells["Yellow Gacha"].Value is DBNull ? "" : (string)row.Cells["Yellow Gacha"].Value, Swords),
                GachaT = FixSkillsNamedAsSwords(row.Cells["Teal Gacha"].Value is DBNull ? "" : (string)row.Cells["Teal Gacha"].Value, Swords),

                Panel1completion = FixSkillsNamedAsSwords(row.Cells["Panel 1"].Value is DBNull ? "" : (string)row.Cells["Panel 1"].Value, Swords),
                Panel2completion = FixSkillsNamedAsSwords(row.Cells["Panel 2"].Value is DBNull ? "" : (string)row.Cells["Panel 2"].Value, Swords),
                Panel3completion = FixSkillsNamedAsSwords(row.Cells["Panel 3"].Value is DBNull ? "" : (string)row.Cells["Panel 3"].Value, Swords),
                Panel4completion = FixSkillsNamedAsSwords(row.Cells["Panel 4"].Value is DBNull ? "" : (string)row.Cells["Panel 4"].Value, Swords),

                Panel1stats = FixSkillsNamedAsSwords(row.Cells["Panel 1 Stats"].Value is DBNull ? "" : (string)row.Cells["Panel 1 Stats"].Value, Swords),
                Panel2stats = FixSkillsNamedAsSwords(row.Cells["Panel 2 Stats"].Value is DBNull ? "" : (string)row.Cells["Panel 2 Stats"].Value, Swords),
                Panel3stats = FixSkillsNamedAsSwords(row.Cells["Panel 3 Stats"].Value is DBNull ? "" : (string)row.Cells["Panel 3 Stats"].Value, Swords),
                Panel4stats = FixSkillsNamedAsSwords(row.Cells["Panel 4 Stats"].Value is DBNull ? "" : (string)row.Cells["Panel 4 Stats"].Value, Swords),

                Gacha = row.Cells["Gacha"].Value is DBNull ? false : (string)row.Cells["Gacha"].Value == "1",
                Event = row.Cells["Event"].Value is DBNull ? false : (string)row.Cells["Event"].Value == "1",
                MultiFusion = row.Cells["Multi-Fusion"].Value is DBNull ? false : (string)row.Cells["Multi-Fusion"].Value == "1",
                BannerRequired = row.Cells["Banner Required"].Value is DBNull ? false : (string)row.Cells["Banner Required"].Value == "1"
            };
        }

        #endregion
    }

    #region Structs

    //Object to hold Sword Data
    public struct Sword
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
        public string SwordVersions;

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

        public string Panel1completion;
        public string Panel1stats;
        public string Panel2completion;
        public string Panel2stats;
        public string Panel3completion;
        public string Panel3stats;
        public string Panel4completion;
        public string Panel4stats;

        public bool Gacha;
        public bool Event;
        public bool MultiFusion;
        public bool BannerRequired;

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
            return "|{{ListSword|Sword=" + Name + "|race= " + Race + "|grade= " + Grade + "|rarity= " + Rarity + "|ai= " + Ai + "|" +
                     "hp= " + HP + "|str= " + Str + "|mag= " + Mag + "|vit= " + Vit + "|agi= " + Agi + "|luck= " + Luck + "" +
                     "|phys= " + Phys + "|fire= " + Fire + "|ice= " + Ice + "|elec= " + Elec + "" +
                     "|force= " + Force + "|light= " + Light + "|dark= " + Dark +
                     "|patk= " + PAtk + "|pdef= " + PDef + "|matk= " + MAtk + "|mdef= " + MDef +
                     "|panel1= " + Panel1completion + "|panel2= " + Panel2completion + "|panel3= " + Panel3completion + Panel4completion +
                     "|panel1stats= " + Panel1stats + "|panel2stats= " + Panel2stats + "|panel3stats= " + Panel3stats + "|panel4stats= " + Panel4stats +
                     "}}\r\n" +
                     "|- style=\"vertical-align:middle;\"";
        }

        //Creates a Wiki string for Individual by themselves
        public string CreateWikiStringIndividual()
        {
            //Lets generate a handful of useful categories to add on
            var extraCats = "";
            if (Gacha)
                extraCats += "[[Category: Gacha Swords]]\r\n";
            if (Event)
                extraCats += "[[Category: Event Swords]]\r\n";
            if (MultiFusion)
                extraCats += "[[Category: Multi-Fusion Swords]]\r\n";
            if (BannerRequired)
                extraCats += "[[Category: Time Limited Swords]]\r\n";
            if (MultiFusion || (!Gacha && !Event))
                extraCats += "[[Category: Fusible Swords]]\r\n";

            //Generate Fusion and Fission URL
            var newName = Name;

            Regex regex = new Regex(@"\b A\b");
            newName = regex.Replace(newName, " %5BDimensional%5D");
            var fissionLink = "https://oceanxdds.github.io/dx2_fusion/?route=fission&Sword=" + Uri.EscapeUriString(newName) + "#en";
            var fusionLink = "https://oceanxdds.github.io/dx2_fusion/?route=fusion&Sword=" + Uri.EscapeUriString(newName) + "#en";

            //Generate Flag Info
            var flagInfo = "";
            flagInfo += "gacha=" + Gacha.ToString() + "|";
            flagInfo += "multi-fusion=" + MultiFusion.ToString() + "|";
            flagInfo += "event=" + Event.ToString() + "|";
            flagInfo += "banner=" + BannerRequired.ToString() + "|";
            flagInfo += "fusible=" + (MultiFusion || (!Gacha && !Event)).ToString() + "|";
            flagInfo += "fissionLink=" + fissionLink;

            //Return the data
            return
                "__TOC__\r\n" +
                "{{Sword\r\n" +
                "|id=\r\n" +
                "|jpname=\r\n" +
                "|name= " + Name + Environment.NewLine +
                "|release_version= 1.0\r\n" +
                "|link_altema=\r\n" +
                "|art= {{PAGENAME}}.jpg\r\n" +
                "|phys= " + Phys + Environment.NewLine +
                "|fire= " + Fire + Environment.NewLine +
                "|ice= " + Ice + Environment.NewLine +
                "|elec= " + Elec + Environment.NewLine +
                "|force= " + Force + Environment.NewLine +
                "|light= " + Light + Environment.NewLine +
                "|dark= " + Dark + Environment.NewLine +
                "|race= " + Race + Environment.NewLine +
                "|grade= " + Grade + Environment.NewLine +
                "|rarity= " + Rarity + Environment.NewLine +
                "|ai= " + Ai + Environment.NewLine +
                "|max_hp= " + HP + " (" + SwordHelper.GetMyRank(ranks, Name).HP + "/" + total + ")\r\n" +
                "|max_str= " + Str + " (" + SwordHelper.GetMyRank(ranks, Name).Str + "/" + total + ")\r\n" +
                "|max_mag= " + Mag + " (" + SwordHelper.GetMyRank(ranks, Name).Mag + "/" + total + ")\r\n" +
                "|max_vit= " + Vit + " (" + SwordHelper.GetMyRank(ranks, Name).Vit + "/" + total + ")\r\n" +
                "|max_agi= " + Agi + " (" + SwordHelper.GetMyRank(ranks, Name).Agility + "/" + total + ")\r\n" +
                "|max_luck= " + Luck + " (" + SwordHelper.GetMyRank(ranks, Name).Luck + "/" + total + ")\r\n" +
                "|patk= " + PAtk + Environment.NewLine +
                "|pdef= " + PDef + Environment.NewLine +
                "|matk= " + MAtk + Environment.NewLine +
                "|mdef= " + MDef + Environment.NewLine +
                "|panel1= " + Panel1completion + Environment.NewLine +
                "|panel2= " + Panel2completion + Environment.NewLine +
                "|panel3= " + Panel3completion + Environment.NewLine +
                "|panel4= " + Panel4completion + Environment.NewLine +
                "|panel1stats= " + Panel1stats + Environment.NewLine +
                "|panel2stats= " + Panel2stats + Environment.NewLine +
                "|panel3stats= " + Panel3stats + Environment.NewLine +
                "|panel4stats= " + Panel4stats + Environment.NewLine +
                "|transfer_skill= " + (Skill1 == "" ? "N/A" : Skill1.Replace("[", "(").Replace("]", ")")) + Environment.NewLine +
                "|innate_skill1= " + (Skill2 == "" ? "N/A" : Skill2.Replace("[", "(").Replace("]", ")")) + Environment.NewLine +
                "|innate_skill2= " + (Skill3 == "" ? "N/A" : Skill3.Replace("[", "(").Replace("]", ")")) + Environment.NewLine +
                "|a_clear= " + (AwakenC == "" ? "N/A" : AwakenC.Replace("[", "(").Replace("]", ")")) + Environment.NewLine +
                "|a_red= " + (AwakenR == "" ? "N/A" : AwakenR.Replace("[", "(").Replace("]", ")")) + Environment.NewLine +
                "|a_yellow= " + (AwakenY == "" ? "N/A" : AwakenY.Replace("[", "(").Replace("]", ")")) + Environment.NewLine +
                "|a_purple= " + (AwakenP == "" ? "N/A" : AwakenP.Replace("[", "(").Replace("]", ")")) + Environment.NewLine +
                "|a_teal= " + (AwakenT == "" ? "N/A" : AwakenT.Replace("[", "(").Replace("]", ")")) + Environment.NewLine +
                "|g_red= " + (GachaR == "" ? "N/A" : GachaR.Replace("[", "(").Replace("]", ")")) + Environment.NewLine +
                "|g_yellow= " + (GachaY == "" ? "N/A" : GachaY.Replace("[", "(").Replace("]", ")")) + Environment.NewLine +
                "|g_purple= " + (GachaP == "" ? "N/A" : GachaP.Replace("[", "(").Replace("]", ")")) + Environment.NewLine +
                "|g_teal= " + (GachaT == "" ? "N/A" : GachaT.Replace("[", "(").Replace("]", ")")) + Environment.NewLine +
                "|awaken1= " + Awaken1 + "|awaken2=" + Awaken2 + "|awaken3=" + Awaken3 + "|awaken4=" + Awaken4 + "|awaken1amnt=" + Awaken1Amount + "|awaken2amnt=" + Awaken2Amount + "|awaken3amnt=" + Awaken3Amount + "|awaken4amnt=" + Awaken4Amount + Environment.NewLine +
                "|gacha= " + Gacha + Environment.NewLine +
                "|event= " + Event + Environment.NewLine +
                "|multifusion= " + MultiFusion + Environment.NewLine +
                "|bannerrequired= " + BannerRequired + Environment.NewLine +
                "|fissionlink= " + fissionLink + Environment.NewLine +
                "|fusionlink= " + fusionLink + Environment.NewLine +
                "|}}\r\n" +
                "<section begin=gachaFlags/>{{#ifeq:{{PAGENAME}}| Tier List |{{GachaFlag|" + flagInfo + "}}|}}<section end=gachaFlags/>\r\n" +
                "{{:{{PAGENAME}}/Builds}}\r\n" +
                "{{:{{PAGENAME}}/Lore}}\r\n" +
                "[[Category: Swords]]\r\n" +
                "[[Category: " + Race + "]]\r\n" +
                "[[Category: " + Rarity + " Star Swords]]\r\n" +
                "[[Category: " + Ai + " AI]]\r\n" +
                extraCats;
        }
    }

    #endregion
}