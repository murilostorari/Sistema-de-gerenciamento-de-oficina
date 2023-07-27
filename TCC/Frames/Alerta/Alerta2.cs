using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCC.Frames
{
    public partial class Alerta2 : Form
    {
        FormCollection fc = Application.OpenForms;

        Estoque.NovoProduto NewProduct = (Estoque.NovoProduto)Application.OpenForms["NovoProduto"];
        NovaEntrada NewEntry = (NovaEntrada)Application.OpenForms["NovaEntrada"];
        NovaSaida NewLeft = (NovaSaida)Application.OpenForms["NovaSaida"];

        public static Alerta2 AlertaFrame;
        public Label LblText;

        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;
        bool CanShowProductForm = Properties.Settings.Default.CanShowNewProductForm;

        public Alerta2()
        {
            InitializeComponent();
            SetColor();

            AlertaFrame = this;
            LblText = Texto;

            if (AnimateFrames)
                guna2BorderlessForm1.AnimateWindow = true;
            else
                guna2BorderlessForm1.AnimateWindow = false;
        }

        private void Alerta2_Load(object sender, EventArgs e)
        {
            GifTimer.Start();
            LoadImages();
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Funçoes */

        // Gif speed
        Image[] images = new Image[90];

        private void LoadImages()
        {
            for (int i = 1; i <= 90; i++)
            {
                string path = $@"{Application.StartupPath}\Gifs\Alerta\frame_{i:00}_delay-0.02s.gif";
                Image image = Image.FromFile(path);
                images[i - 1] = image;
            }
        }

        int i = 0;

        private void GifTimer_Tick(object sender, EventArgs e)
        {
            i %= 90;
            Gif.Image = images[i];
            i += 1;

            if (i == 90)
                GifTimer.Stop();
        }

        private void TimerAnim_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0.0)
            {
                this.Opacity -= 0.2;
            }
            else
            {
                TimerAnim.Stop();
                this.Close();
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

            label1.ForeColor = ThemeManager.FontColor;
            Texto.ForeColor = ThemeManager.PresetLabelColor;

            Ok.FillColor = ThemeManager.FullGreenButtonColor;
            Ok.BorderColor = ThemeManager.FullGreenButtonColor;
            Ok.HoverState.FillColor = ThemeManager.FullGreenButtonHoverColor;
            Ok.HoverState.BorderColor = ThemeManager.FullGreenButtonHoverColor;
            Ok.CheckedState.FillColor = ThemeManager.FullGreenButtonCheckedColor;
            Ok.CheckedState.BorderColor = ThemeManager.FullGreenButtonCheckedColor;
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Outros */

        // Ok
        private async void Ok_Click(object sender, EventArgs e)
        {
            if (AnimateFrames)
            {
                TimerAnim.Start();
                await TaskDelay(100);

                foreach (Form frm in fc)
                {
                    if (frm.Name == "NovaEntrada")
                        NewEntry.Close();
                    else if (frm.Name == "NovaSaida")
                        NewLeft.Close();
                }

                Properties.Settings.Default.CanShowNewProductForm = true;

                Close();
            }
            else
            {

                foreach (Form frm in fc)
                {
                    if (frm.Name == "NovaEntrada")
                        NewEntry.Close();
                    if (frm.Name == "NovaSaida")
                        NewLeft.Close();
                }

                await TaskDelay(100);

                Properties.Settings.Default.CanShowNewProductForm = true;

                Close();
            }
        }
    }
}
