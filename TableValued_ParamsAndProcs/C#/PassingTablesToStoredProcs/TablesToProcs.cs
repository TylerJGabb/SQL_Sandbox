using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
    }
}
