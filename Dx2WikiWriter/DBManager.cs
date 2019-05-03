using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Dx2WikiWriter
{
    public class DBManager
    {
        #region Properties

        //Loads our DB from the path into a data grid view
        public DataTable LoadDB(string path) => ReadCSV(path);
        
        //Saves our DB to the loaded Path
        public void SaveDB(DataTable dataTable, string path) => SaveCSV(dataTable, path);

        #endregion

        #region Methods

        //Method that Saves our Data Table to our CSV
        public bool SaveCSV(DataTable dataTable, string path)
        {
            try
            {
                var fileContent = new StringBuilder();

                foreach (var col in dataTable.Columns)
                {
                    //Skips our Export Column
                    if (col == dataTable.Columns["Export"])
                        continue;

                    fileContent.Append(col.ToString() + ",");
                }
                
                //Replace last , from previous step with a new line identifier
                fileContent.Replace(",", Environment.NewLine, fileContent.Length - 1, 1);

                foreach (DataRow dr in dataTable.Rows)
                {
                    foreach (var column in dr.ItemArray)
                        fileContent.Append("\"" + column.ToString() + "\",");

                    //Same as before replace our last , with a new line identifier
                    fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
                }

                File.WriteAllText(path, fileContent.ToString());
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show(path + "\r\nCould not save file.\r\n\r\n" + e.Message, "Error");
                return false;
            }
        }

        //Method that loads our CSV Data into our Data Table
        public DataTable ReadCSV(string filePath)
        {
            try
            {
                var dt = new DataTable();

                using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + Path.GetDirectoryName(filePath) + ";Extended Properties=\"Text;HDR=YES;FMT=Delimited;CharacterSet=65001\""))
                {
                    conn.Open();
                    string strQuery = "SELECT * FROM [" + Path.GetFileName(filePath) + "]";
                    OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, conn);
                    adapter.Fill(dt);
                }

                return dt;
            }
            catch (Exception e)
            {
                MessageBox.Show("Something is wrong with. " + filePath + "\r\nLook into the error below.\r\n\r\n" + e.Message, "Error");
                return null;
            }
        }

        #endregion
    }
}