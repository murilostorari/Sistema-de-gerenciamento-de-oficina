using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCC
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            SetColor();
        }

        private void SetColor()
        {
            this.BackColor = ThemeManager.FormBackColor;

            Nome.ForeColor = ThemeManager.FontColor;
            Fone.ForeColor = ThemeManager.FontColor;
            Endereco.ForeColor = ThemeManager.FontColor;
            Aberto.ForeColor = ThemeManager.PresetFontColor;
        }
    }
}
