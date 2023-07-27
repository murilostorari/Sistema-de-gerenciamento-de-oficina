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
    public partial class CloseConfirm : Form
    {
        FormCollection fc = Application.OpenForms;

        public static CloseConfirm CloseFrame;
        public Label TopText;
        public Label LblText;

        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;

        public CloseConfirm()
        {
            InitializeComponent();
            SetColor();

            CloseFrame = this;
            TopText = TextoCima;
            LblText = Texto;

            if (AnimateFrames)
                guna2BorderlessForm1.AnimateWindow = true;
            else
                guna2BorderlessForm1.AnimateWindow = false;
        }

        private void CloseConfirm_Load(object sender, EventArgs e)
        {
            GifTimer.Start();
            LoadImages();
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Funcoes */

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
            if (Opacity > 0.0)
            {
                Opacity -= 0.2;
            }
            else
            {
                TimerAnim.Stop();
                Close();
            }
        }

        private void FormAnim_Tick(object sender, EventArgs e)
        {
            foreach (Form frm in fc)
            {
                if (frm.Name == "NovoCliente")
                {
                    if (frm.Opacity < 1.0)
                        frm.Opacity += 0.2;
                    else
                        FormAnim.Stop();
                }
                else if (frm.Name == "NovoProduto")
                {
                    if (frm.Opacity < 1.0)
                        frm.Opacity += 0.2;
                    else
                        FormAnim.Stop();
                }
                else if (frm.Name == "NovoFornecedor")
                {
                    if (frm.Opacity < 1.0)
                        frm.Opacity += 0.2;
                    else
                        FormAnim.Stop();
                }
                else if (frm.Name == "NovoFuncionario")
                {
                    if (frm.Opacity < 1.0)
                        frm.Opacity += 0.2;
                    else
                        FormAnim.Stop();
                }
                else if (frm.Name == "NovoServico")
                {
                    if (frm.Opacity < 1.0)
                        frm.Opacity += 0.2;
                    else
                        FormAnim.Stop();
                }
                else if (frm.Name == "NovaEntrada")
                {
                    if (frm.Opacity < 1.0)
                        frm.Opacity += 0.2;
                    else
                        FormAnim.Stop();
                }
                else if (frm.Name == "NovaSaida")
                {
                    if (frm.Opacity < 1.0)
                        frm.Opacity += 0.2;
                    else
                        FormAnim.Stop();
                }
                else if (frm.Name == "EditCliente")
                {
                    if (frm.Opacity < 1.0)
                        frm.Opacity += 0.2;
                    else
                        FormAnim.Stop();
                }
                else if (frm.Name == "EditProduto")
                {
                    if (frm.Opacity < 1.0)
                        frm.Opacity += 0.2;
                    else
                        FormAnim.Stop();
                }
                else if (frm.Name == "EditFornecedor")
                {
                    if (frm.Opacity < 1.0)
                        frm.Opacity += 0.2;
                    else
                        FormAnim.Stop();
                }
                else if (frm.Name == "EditServico")
                {
                    if (frm.Opacity < 1.0)
                        frm.Opacity += 0.2;
                    else
                        FormAnim.Stop();
                }
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

            TextoCima.ForeColor = ThemeManager.FontColor;
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
        private async void Ok_Click_1(object sender, EventArgs e)
        {
            if (AnimateFrames)
            {
                TimerAnim.Start();
                await TaskDelay(100);
            }

            foreach (Form frm in fc)
            {
                if (frm.Name == "NovoCliente")
                    frm.Close();
                else if (frm.Name == "NovoProduto")
                    frm.Close();
                else if (frm.Name == "NovoFornecedor")
                    frm.Close();
                else if (frm.Name == "NovoFuncionario")
                    frm.Close();
                else if (frm.Name == "NovoServico")
                    frm.Close();
                else if (frm.Name == "NovaEntrada")
                    frm.Close();
                else if (frm.Name == "NovaSaida")
                    frm.Close();
                else if (frm.Name == "EditCliente")
                    frm.Close();
                else if (frm.Name == "EditProduto")
                    frm.Close();
                else if (frm.Name == "EditFornecedor")
                    frm.Close();
                else if (frm.Name == "EditFuncionario")
                    frm.Close();
                else if (frm.Name == "EditServico")
                    frm.Close();
            }
        }

        // Cancelar
        private async void Cancelar_Click_1(object sender, EventArgs e)
        {
            if (AnimateFrames)
            {
                TimerAnim.Start();
                FormAnim.Start();
                await TaskDelay(100);
                Close();
            }
            else
            {
                this.Opacity = .0d;

                foreach (Form frm in fc)
                {
                    if (frm.Name == "NovoCliente")
                    {
                        if (frm.Opacity < 1.0)
                            frm.Opacity = 1.0;
                    }
                    else if (frm.Name == "NovoProduto")
                    {
                        if (frm.Opacity < 1.0)
                            frm.Opacity = 1.0;
                    }
                    else if (frm.Name == "NovoFornecedor")
                    {
                        if (frm.Opacity < 1.0)
                            frm.Opacity = 1.0;
                    }
                    else if (frm.Name == "NovoFuncionario")
                    {
                        if (frm.Opacity < 1.0)
                            frm.Opacity = 1.0;
                    }
                    else if (frm.Name == "NovoServico")
                    {
                        if (frm.Opacity < 1.0)
                            frm.Opacity = 1.0;
                    }
                    else if (frm.Name == "NovaEntrada")
                    {
                        if (frm.Opacity < 1.0)
                            frm.Opacity = 1.0;
                    }
                    else if (frm.Name == "NovaSaida")
                    {
                        if (frm.Opacity < 1.0)
                            frm.Opacity = 1.0;
                    }
                    else if (frm.Name == "EditCliente")
                    {
                        if (frm.Opacity < 1.0)
                            frm.Opacity = 1.0;
                    }
                    else if (frm.Name == "EditProduto")
                    {
                        if (frm.Opacity < 1.0)
                            frm.Opacity = 1.0;
                    }
                    else if (frm.Name == "EditFornecedor")
                    {
                        if (frm.Opacity < 1.0)
                            frm.Opacity = 1.0;
                    }
                    else if (frm.Name == "EditFuncionario")
                    {
                        if (frm.Opacity < 1.0)
                            frm.Opacity = 1.0;
                    }
                    else if (frm.Name == "EditServico")
                    {
                        if (frm.Opacity < 1.0)
                            frm.Opacity = 1.0;
                    }
                }

                Close();
            }
        }
    }
}
