using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            string file = args[0];
            string myBlock = args[1];
            using(OpenFileDialog load = new OpenFileDialog())
            {
                if (file == null)
                {
                    load.Filter = "EV3 Project Files|*.ev3";
                    load.ShowDialog();
                    file = load.FileName;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(file, myBlock));
        }
    }
}
