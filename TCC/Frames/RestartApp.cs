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
    public partial class RestartApp : Form
    {
        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;

        bool IsShowPopupsChanged;
        bool IsDoubleClickChanged;
        bool IsAutoCompleteCurrencyChanged;

        bool IsDarkModeChanged;
        bool IsLightModeChanged;

        bool IsAnimateButtonsChanged;
        bool IsAnimateFramesChanged;


        public RestartApp(bool DarkModeChanged, bool LightModeChanged, bool AnimateButtonsChanged, bool AnimateFramesChanged,
                        bool ShowPopupsChanged, bool DoubleClickChanged, bool AutoCompleteCurrencyChanged)
        {
            InitializeComponent();
            SetColor();

            if (DarkModeChanged)
                IsDarkModeChanged = true;

            if (LightModeChanged)
                IsLightModeChanged = true;

            if (AnimateButtonsChanged)
                IsAnimateButtonsChanged = true;

            if (AnimateFramesChanged)
                IsAnimateFramesChanged = true;

            if (ShowPopupsChanged)
                IsShowPopupsChanged = true;

            if (DoubleClickChanged)
                IsDoubleClickChanged = true;

            if (AutoCompleteCurrencyChanged)
                IsAutoCompleteCurrencyChanged = true;

            if (AnimateFrames)
                guna2BorderlessForm1.AnimateWindow = true;
            else
                guna2BorderlessForm1.AnimateWindow = false;
        }

        private void RestartApp_Load(object sender, EventArgs e)
        {
            GifTimer.Start();
            LoadImages();
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Funçoes */

        // Gif speed
        Image[] images = new Image[41];

        private void LoadImages()
        {
            for (int i = 1; i <= 41; i++)
            {
                string path = $@"{Application.StartupPath}\Gifs\Sucesso\frame_{i:00}_delay-0.05s.gif";
                Image image = Image.FromFile(path);
                images[i - 1] = image;
            }
        }

        int i = 0;

        private void GifTimer_Tick(object sender, EventArgs e)
        {
            i %= 41;
            Gif.Image = images[i];
            i += 1;

            if (i == 41)
                GifTimer.Stop();
        }

        // Delay
        async Task TaskDelay(int valor)
        {
            await Task.Delay(valor);
        }

        // Ativar/desativar o dark mode
        private void SetColor()
        {
            this.BackColor = ThemeManager.FormBackColor;

            label1.ForeColor = ThemeManager.FontColor;
            Texto.ForeColor = ThemeManager.PresetLabelColor;

            Ok1.FillColor = ThemeManager.FullGreenButtonColor;
            Ok1.BorderColor = ThemeManager.FullGreenButtonColor;
            Ok1.HoverState.FillColor = ThemeManager.FullGreenButtonHoverColor;
            Ok1.HoverState.BorderColor = ThemeManager.FullGreenButtonHoverColor;
            Ok1.CheckedState.FillColor = ThemeManager.FullGreenButtonCheckedColor;
            Ok1.CheckedState.BorderColor = ThemeManager.FullGreenButtonCheckedColor;
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Outros */

        // Ok 1
        private void Ok1_Click(object sender, EventArgs e)
        {
            if (IsDarkModeChanged)
            {
                Properties.Settings.Default.DarkModeEnabled = true;
                ThemeManager.ChangeMode();
            }

            if (IsLightModeChanged)
            {
                Properties.Settings.Default.DarkModeEnabled = false;
                ThemeManager.ChangeMode();
            }

            if (IsAnimateButtonsChanged)
                Properties.Settings.Default.AnimarBotoes = true;
            else
                Properties.Settings.Default.AnimarBotoes = false;

            if (IsAnimateFramesChanged)
                Properties.Settings.Default.AnimarFrames = true;
            else
                Properties.Settings.Default.AnimarFrames = false;

            if (IsShowPopupsChanged)
                Properties.Settings.Default.ShowPopups = true;
            else
                Properties.Settings.Default.ShowPopups = false;

            if (IsDoubleClickChanged)
                Properties.Settings.Default.DoubleClickInGridEnabled = true;
            else
                Properties.Settings.Default.DoubleClickInGridEnabled = false;

            if (IsAutoCompleteCurrencyChanged)
                Properties.Settings.Default.AutoCompleteCurrencyValues = true;
            else
                Properties.Settings.Default.AutoCompleteCurrencyValues = false;

            Application.Restart();
            Environment.Exit(0);
        }
    }
}
