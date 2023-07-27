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

namespace TCC.Frames
{
    public partial class AddSuccess : Form
    {
        FormCollection fc = Application.OpenForms;

        public static AddSuccess SuccessAdd;
        public Label LblText;

        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;

        public AddSuccess()
        {
            InitializeComponent();
            SetColor();

            SuccessAdd = this;
            LblText = Texto;

            if (AnimateFrames)
                guna2BorderlessForm1.AnimateWindow = true;
            else
                guna2BorderlessForm1.AnimateWindow = false;
        }

        private void Sucesso_Load(object sender, EventArgs e)
        {
            GifTimer.Start();
            LoadImages();

            Properties.Settings.Default.CanUpdateGrid = true;
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Funçoes */

        // Gif speed
        Image[] images = new Image[71];

        private void LoadImages()
        {
            for (int i = 1; i <= 71; i++)
            {
                string path = $@"{Application.StartupPath}\Gifs\Happy\frame_{i:00}_delay-0.02s.gif";
                Image image = Image.FromFile(path);
                images[i - 1] = image;
            }
        }

        int i = 0;

        private void GifTimer_Tick(object sender, EventArgs e)
        {
            i %= 71;
            Gif.Image = images[i];
            i += 1;

            if (i == 71)
                GifTimer.Stop();
        }

        // Fade
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

        private async void Ok_Click_1(object sender, EventArgs e)
        {
            Properties.Settings.Default.CanUpdateGrid = false;

            foreach (Form frm in fc)
            {
                if (frm.Name == "NovoCliente")
                {
                    if (AnimateFrames)
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }
                    else
                        frm.Close();
                }
                else if (frm.Name == "NovoProduto")
                {
                    if (AnimateFrames)
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }
                    else
                        frm.Close();
                }
                else if (frm.Name == "NovoFornecedor")
                {
                    if (AnimateFrames)
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }
                    else
                        frm.Close();
                }
                else if (frm.Name == "NovoFuncionario")
                {
                    if (AnimateFrames)
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }
                    else
                        frm.Close();
                }
                else if (frm.Name == "NovoServico")
                {
                    if (AnimateFrames)
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }
                    else
                        frm.Close();
                }
                else if (frm.Name == "NovaEntrada")
                {
                    if (AnimateFrames)
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }
                    else
                        frm.Close();
                }
                else if (frm.Name == "NovaSaida")
                {
                    if (AnimateFrames)
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }
                    else
                        frm.Close();
                }
            }
        }
    }
}
