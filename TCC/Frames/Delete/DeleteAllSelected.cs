using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace TCC.Frames
{
    public partial class DeleteAllSelected : Form
    {
        Success SuccessForm = new Success();
        Erro ErrorForm = new Erro();

        public static DeleteAllSelected DeleteAllFrame;
        public Label TmplText;
        public Label LblText;

        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;

        public DeleteAllSelected()
        {
            InitializeComponent();
            SetColor();

            DeleteAllFrame = this;
            TmplText = TemplateText;
            LblText = Texto;

            if (AnimateFrames)
                guna2BorderlessForm1.AnimateWindow = true;
            else
                guna2BorderlessForm1.AnimateWindow = false;
        }

        private void DeleteAllSelected_Load(object sender, EventArgs e)
        {
            GifTimer.Start();
            LoadImages();
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Funcoes */


        // Gif speed
        Image[] images = new Image[35];

        private void LoadImages()
        {
            if (IsDarkModeEnabled)
            {
                for (int i = 1; i <= 35; i++)
                {
                    string path = $@"{Application.StartupPath}\Gifs\Delete-Dark\frame_{i:00}_delay-0.04s.gif";
                    Image image = Image.FromFile(path);
                    images[i - 1] = image;
                }
            }
            else
            {
                for (int i = 1; i <= 35; i++)
                {
                    string path = $@"{Application.StartupPath}\Gifs\Delete-White\frame_{i:00}_delay-0.04s.gif";
                    Image image = Image.FromFile(path);
                    images[i - 1] = image;
                }
            }
        }

        int i = 0;

        private void GifTimer_Tick(object sender, EventArgs e)
        {
            i %= 35;
            Gif.Image = images[i];
            i += 1;

            if (i == 35)
                GifTimer.Stop();
        }

        private void TimerAnim_Tick(object sender, EventArgs e)
        {
            if (Opacity > 0.0)
            {
                Opacity -= 0.2;
            }
            else
            {
                TimerAnim.Stop();
            }
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

            TemplateText.ForeColor = ThemeManager.FontColor;
            Texto.ForeColor = ThemeManager.PresetLabelColor;

            Ok.FillColor = ThemeManager.FullRedButtonColor;
            Ok.BorderColor = ThemeManager.FullRedButtonColor;
            Ok.HoverState.FillColor = ThemeManager.FullRedButtonHoverColor;
            Ok.HoverState.BorderColor = ThemeManager.FullRedButtonHoverColor;
            Ok.CheckedState.FillColor = ThemeManager.FullRedButtonCheckedColor;
            Ok.CheckedState.BorderColor = ThemeManager.FullRedButtonCheckedColor;

            Cancelar.FillColor = ThemeManager.BorderDarkGrayButtonFillColor;
            Cancelar.ForeColor = ThemeManager.BorderDarkGrayButtonForeColor;
            Cancelar.BorderColor = ThemeManager.BorderDarkGrayButtonBorderColor;
            Cancelar.HoverState.ForeColor = ThemeManager.BorderDarkGrayButtonHoverForeColor;
            Cancelar.HoverState.BorderColor = ThemeManager.BorderDarkGrayButtonHoverBorderColor;
            Cancelar.HoverState.FillColor = ThemeManager.BorderDarkGrayButtonHoverFillColor;
            Cancelar.FocusedColor = ThemeManager.BorderDarkGrayButtonPressedFocusedColor;
            Cancelar.PressedColor = ThemeManager.BorderDarkGrayButtonPressedFocusedColor;
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Outros */

        // Ok
        private void Ok_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CanShowDeleteConfirmation = true;
            this.Close();
        }

        private async void Cancelar_Click(object sender, EventArgs e)
        {
            if (AnimateFrames)
            {
                TimerAnim.Start();
                await TaskDelay(100);
                Close();
            }
            else
                Close();
        }
    }
}
