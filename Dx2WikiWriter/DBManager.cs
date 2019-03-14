using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void SaveDB(object dataSource)
        {

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
                MessageBox.Show("Something is wrong with the input file. Contact Alenael with the message below if required.\n\n" + e.Message, "Error");
                return null;
            }            
        }

        //Creates a demon object from a data grid view row
        public Demon LoadDemon(string line)
        {
            var demon = new Demon();



            return demon;
        }
    }

    public struct Demon
    {
        public string Name;


        //Creates a Wiki String based on the info in this object and returns it
        public string CreateWikiString()
        {


            //Else reutnr nothing
            return "";
        }
    }
}
