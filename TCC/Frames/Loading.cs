using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace TCC.Frames
{
    public partial class Loading : Form
    {
        public static Loading LoadingFrame;
        public Label LblText;

        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;

        public Loading()
        {
            InitializeComponent();

            LoadingFrame = this;
            LblText = Texto;

            if (AnimateFrames)
                guna2BorderlessForm1.AnimateWindow = true;
            else
                guna2BorderlessForm1.AnimateWindow = false;
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            GifTimer.Start();
            LoadImages();
        }

        /*
         * --------------------------------------------------------------------------------------------------------------------------
         */

        // Funçoes

        // Gif speed
        Image[] images = new Image[29];

        private void LoadImages()
        {
            for (int i = 1; i <= 29; i++)
            {
                string path = $@"{Application.StartupPath}\Gifs\Loading\frame_{i:00}_delay-0.04s.gif";
                Image image = Image.FromFile(path);
                images[i - 1] = image;
            }
        }

        int i = 0;

        private void GifTimer_Tick(object sender, EventArgs e)
        {
            i %= 29;
            Gif.Image = images[i];
            i += 1;

            if (i == 29)
                GifTimer.Stop();
        }
    }
}
