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
            bool kill = false;

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
                    {
                        DialogResult result = MessageBox.Show("Please enter a valid file.", "Invalid File", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Information);
                        if (result == DialogResult.Abort)
                        {
                            kill = true;
                            break;
                        }
                        else if (result == DialogResult.Ignore)
                            break;
                    }
                    else
                        break;
                }
            }
            ev3p = GetEV3PFromArgs(file, ev3p);
            MessageBox.Show(ev3p);
            Application.Run(new mbTools(file, ev3p, kill));
        }

        public static string GetEV3PFromArgs(string file, string ev3p)
        {
            List<ZipArchiveEntry> possibleEntries = new List<ZipArchiveEntry>();
            using(ZipArchive archive = ZipFile.OpenRead(file))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.Name.EndsWith(".ev3p"))
                        possibleEntries.Add(entry);
                    MessageBox.Show(new EV3P.EV3P(entry).ToString());
                }
            }
            foreach(ZipArchiveEntry entry in possibleEntries)
            {
                if (entry.Name.TrimEnd(".ev3p".ToCharArray()) == ev3p)
                    return entry.FullName;
            }
            return null;
        }
    }
}
