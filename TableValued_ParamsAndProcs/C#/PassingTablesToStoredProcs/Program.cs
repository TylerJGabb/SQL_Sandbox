using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PolyPrintUtilities;

namespace PassingTablesToStoredProcs
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PPDataAccess.SetToTylersDB();
            Application.Run(new TablesToProcs());
        }
    }
}
