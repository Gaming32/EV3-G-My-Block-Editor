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

namespace Display_EV3_G_My_Block
{
    public partial class mbTools : Form
    {
        public mbTools(string file, string ev3p, bool kill)
        {
            if (kill)
                Application.Exit();
            InitializeComponent();
            Focus();
        }
    }
}
