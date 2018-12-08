using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;

namespace Display_EV3_G_My_Block
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

            string file;
            string ev3p;

            if (args.Length < 1)
                file = null;
            else
                file = args[0];

            if (args.Length < 2)
                ev3p = null;
            else
                ev3p = args[1];

            using(OpenFileDialog load = new OpenFileDialog())
            {
                while (true)
                {
                    if (!System.IO.File.Exists(file))
                    {
                        load.Title = "Open EV3-G Project";
                        load.Filter = "EV3 Project Files|*.ev3";
                        load.ShowDialog();
                        file = load.FileName;
                    }
                    if (!System.IO.File.Exists(file))
                        MessageBox.Show("Please enter a valid file.", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        break;
                }
            }

            Application.Run(new mbTools(file, ev3p));
        }

        public static string GetEV3PFromArgs(string arg)
        {

        }
    }
}
