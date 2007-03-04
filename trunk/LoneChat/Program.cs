using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LoneChat
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Load configurations
            try
            {
                Configuration.Load();
            }
            catch (Exception)
            {
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoneChat());
        }
    }
}