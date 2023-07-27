using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCC.Frames
{
    public partial class LoginError : Form
    {
        public LoginError()
        {
            InitializeComponent();
        }

        private void Ok_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
