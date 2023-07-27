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
    public partial class Erro : Form
    {
        FormCollection fc = Application.OpenForms;

        public static Erro ErrorFrame;
        public Label LblText;

        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;

        public Erro()
        {
            InitializeComponent();
            SetColor();

            ErrorFrame = this;
            LblText = Texto;

            if (AnimateFrames)
                guna2BorderlessForm1.AnimateWindow = true;
            else
                guna2BorderlessForm1.AnimateWindow = false;
        }

        private void Erro_Load(object sender, EventArgs e)
        {
            GifTimer.Start();
            LoadImages();
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Funçoes */

        // Gif speed
        Image[] images = new Image[43];

        private void LoadImages()
        {
            for (int i = 1; i <= 43; i++)
            {
                string path = $@"{Application.StartupPath}\Gifs\Erro\frame_{i:00}_delay-0.03s.gif";
                Image image = Image.FromFile(path);
                images[i - 1] = image;
            }
        }

        int i = 0;

        private void GifTimer_Tick(object sender, EventArgs e)
        {
            i %= 43;
            Gif.Image = images[i];
            i += 1;

            if (i == 43)
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

            Ok.FillColor = ThemeManager.FullRedButtonColor;
            Ok.BorderColor = ThemeManager.FullRedButtonColor;
            Ok.HoverState.FillColor = ThemeManager.FullRedButtonHoverColor;
            Ok.HoverState.BorderColor = ThemeManager.FullRedButtonHoverColor;
            Ok.CheckedState.FillColor = ThemeManager.FullRedButtonCheckedColor;
            Ok.CheckedState.BorderColor = ThemeManager.FullRedButtonCheckedColor;
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Outros */

        // Ok
        private async void Ok_Click(object sender, EventArgs e)
        {
            if (AnimateFrames)
            {
                foreach (Form frm in fc)
                {
                    if (frm.Name == "NovoCliente")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }

                    else if (frm.Name == "NovoProduto")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }

                    else if (frm.Name == "NovoFornecedor")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }

                    else if (frm.Name == "NovoFuncionario")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }

                    else if (frm.Name == "NovoServico")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }

                    /*--------------------------*/

                    else if (frm.Name == "EditCliente")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }

                    else if (frm.Name == "EditProduto")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }

                    else if (frm.Name == "EditFornecedor")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }


                    else if (frm.Name == "EditFuncionario")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }

                    /*--------------------------*/

                    else if (frm.Name == "DeleteSelected")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }

                    else if (frm.Name == "DeleteSelected2")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }

                    else if (frm.Name == "DeleteAllSelected")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }

                    else if (frm.Name == "DeleteConfirmation")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }

                    else if (frm.Name == "DeleteConfirmation2")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        frm.Close();
                    }
                }

                TimerAnim.Start();
                await TaskDelay(200);
                Close();
            }
            else
            {
                foreach (Form frm in fc)
                {
                    if (frm.Name == "NovoCliente")
                    {
                        frm.Close();
                    }
                    else if (frm.Name == "NovoProduto")
                    {
                        frm.Close();
                    }
                    else if (frm.Name == "NovoFornecedor")
                    {
                        frm.Close();
                    }
                    else if (frm.Name == "NovoFuncionario")
                    {
                        frm.Close();
                    }
                    else if (frm.Name == "NovoServico")
                    {
                        frm.Close();
                    }
                    else if (frm.Name == "NovaEntrada")
                    {
                        frm.Close();
                    }
                    else if (frm.Name == "NovaSaida")
                    {
                        frm.Close();
                    }
                    else if (frm.Name == "EditCliente")
                    {
                        frm.Close();
                    }
                    else if (frm.Name == "EditProduto")
                    {
                        frm.Close();
                    }
                    else if (frm.Name == "EditFornecedor")
                    {
                        frm.Close();
                    }
                    else if (frm.Name == "EditFuncionario")
                    {
                        frm.Close();
                    }
                    else if (frm.Name == "DeleteSelected")
                    {
                        frm.Close();
                    }
                    else if (frm.Name == "DeleteSelected2")
                    {
                        frm.Close();
                    }
                    else if (frm.Name == "DeleteAllSelected")
                    {
                        frm.Close();
                    }
                    else if (frm.Name == "DeleteConfirmation")
                    {
                        frm.Close();
                    }
                    else if (frm.Name == "DeleteConfirmation2")
                    {
                        frm.Close();
                    }
                }

                await TaskDelay(100);
                Close();
            }
        }
    }
}
