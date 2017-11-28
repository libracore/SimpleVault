using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleVault
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // read first command-line parameter
            string filename = string.Empty;
            if (args.Length > 0)
            {
                filename = args[0];
            }

            Application.Run(new MainForm(filename));
        }
    }
}
