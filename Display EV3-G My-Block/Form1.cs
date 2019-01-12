using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using CLS_Tools;

namespace Display_EV3_G_My_Block
{
    public partial class mbTools : Form
    {
        EV3P.EV3P loadedFile;

        public mbTools(string file, string ev3p, bool kill)
        {
            if (kill)
                Application.Exit();
            InitializeComponent();
            Focus();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save to File";
            save.Filter = "All Files|*.*|EV3-G Program Binary|*.ev3pbin";
            save.FilterIndex = 1;

            save.ShowDialog();
            Serialization.Save<EV3P.EV3P>(loadedFile, save.FileName);
        }
    }
}
