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
    public partial class FileExport : Form
    {
        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;

        public FileExport()
        {
            InitializeComponent();
            SetColor();

            if (AnimateFrames)
                guna2BorderlessForm1.AnimateWindow = true;
            else
                guna2BorderlessForm1.AnimateWindow = false;
        }

        private void FileExport_Load(object sender, EventArgs e)
        {
            GifTimer.Start();
            LoadImages();
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Funçoes */

        // Gif speed
        Image[] images = new Image[67];

        private void LoadImages()
        {
            for (int i = 1; i <= 67; i++)
            {
                string path = $@"{Application.StartupPath}\Gifs\Export\frame_{i:00}_delay-0.02s.gif";
                Image image = Image.FromFile(path);
                images[i - 1] = image;
            }
        }

        int i = 0;

        private void GifTimer_Tick(object sender, EventArgs e)
        {
            Gif.Image = images[i];
            i += 1;

            if (i == 67)
                i = 0;
        }

        private void SetColor()
        {
            this.BackColor = ThemeManager.FormBackColor;

            label1.ForeColor = ThemeManager.FontColor;
            Texto.ForeColor = ThemeManager.PresetLabelColor;
        }
    }
}
