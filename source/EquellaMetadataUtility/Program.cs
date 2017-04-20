using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EquellaMetadataUtility
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
            MainForm mainForm = new MainForm();
            if(args.Length > 0)
            {
                mainForm.launchProfile = args[0];
            }
            Application.Run(mainForm);
        }
    }
}
