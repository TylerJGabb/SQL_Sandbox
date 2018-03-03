using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PolyPrintUtilities;

namespace PassingTablesToStoredProcs
{
    public partial class TablesToProcs : Form
    {
        public TablesToProcs()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Declare a datatable
            var dt = new DataTable();

            //add columns representative of the user defined type to be used
            dt.Columns.Add("Integers", typeof(Int32));
            dt.Columns.Add("Strings", typeof(string));

            //Add some play data
            dt.Rows.Add(1, "Tyler");
            dt.Rows.Add(2, "Regan");
            dt.Rows.Add(3, "Jedi");

            //Create a sqlparam with the datatable as the value
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@tableValuedParam",dt)
            };

            //execute
            PPDataAccess.ExecuteNonQuery("learning_TableValuedProc", parameters);
        }



        DataTable makeFatTable()
        {
            var rnd = new Random();
            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            string newWord()
            {
                var str = "";
                for(int i = 0; i < 10; i++)
                {
                    var letter = alphabet[rnd.Next(0, 26)].ToString();
                    letter = rnd.Next(0, 2) == 1 ? letter.ToUpper() : letter;
                    str += letter.ToString();
                }
                return str;
            }



            var table = new DataTable();
            table.Columns.Add("Integers", typeof(Int32));
            table.Columns.Add("Strings", typeof(string));

            for (int i = 0; i < 600; i++)
            {
                var word = newWord();
                var integer = rnd.Next(0, 33);
                table.Rows.Add(integer, word);
            }
            return table;
        }

        private void buttonSendBig_Click(object sender, EventArgs e)
        {
            var table = makeFatTable();
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@tableValuedParam",table)
            };

            var sw = new Stopwatch();
            sw.Start();
            //execute
            PPDataAccess.ExecuteNonQuery("learning_TableValuedProc", parameters);
            sw.Stop();
            bigSetLabel.Text = (sw.ElapsedMilliseconds / (float)1000).ToString() + "s";
        }

        private void buttonSendRowsOneByOne_Click(object sender, EventArgs e)
        {
            var sw = new Stopwatch();
            sw.Start();
            var table = makeFatTable();
            foreach(DataRow row in table.Rows)
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@integer",(int)row["Integers"]),
                    new SqlParameter("@string",(string)row["Strings"])
                };
                PPDataAccess.ExecuteNonQuery("learning_ScalarValuedProc", parameters);
            }
            sw.Stop();
            rowsLabel.Text = (sw.ElapsedMilliseconds / (float)1000).ToString() + "s";
        }
    }
}
