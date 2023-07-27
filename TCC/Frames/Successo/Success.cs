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
    public partial class Success : Form
    {
        FormCollection fc = Application.OpenForms;

        public static Success SuccessFrame;
        public Label LblText;

        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;

        public Success()
        {
            InitializeComponent();
            SetColor();
            
            SuccessFrame = this;
            LblText = Texto;

            if (AnimateFrames)
                guna2BorderlessForm1.AnimateWindow = true;
            else
                guna2BorderlessForm1.AnimateWindow = false;
        }
      
        private void Success_Load(object sender, EventArgs e)
        {
            GifTimer.Start();
            LoadImages();

            foreach (Form frm in fc)
            {
                if (frm.Name != "DeleteConfirmation")
                    Properties.Settings.Default.CanUpdateGrid = true;
                else if (frm.Name != "DeleteConfirmation2")
                    Properties.Settings.Default.CanUpdateGrid = true;
            }
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

        private void FormAnim_Tick(object sender, EventArgs e)
        {
            foreach (Form frm in fc)
            {
                if (frm.Name == "NovoServico")
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
                foreach (Form frm in fc)
                {
                    if (frm.Name == "NovoServico")
                    {
                        if (frm.Opacity < 1.0)
                            FormAnim.Start();
                    }

                    else if (frm.Name == "EditCliente")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        Properties.Settings.Default.CanUpdateGrid = false;
                        frm.Close();
                    }

                    else if (frm.Name == "EditProduto")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        Properties.Settings.Default.CanUpdateGrid = false;
                        frm.Close();
                    }

                    else if (frm.Name == "EditFornecedor")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        Properties.Settings.Default.CanUpdateGrid = false;
                        frm.Close();
                    }

                    else if (frm.Name == "EditFuncionario")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        Properties.Settings.Default.CanUpdateGrid = false;
                        frm.Close();
                    }

                    /*--------------------------*/

                    else if (frm.Name == "DeleteSelected")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        Properties.Settings.Default.CanUpdateGrid = false;
                        frm.Close();
                    }

                    else if (frm.Name == "DeleteSelected2")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        Properties.Settings.Default.CanUpdateGrid = false;
                        frm.Close();
                    }
                    
                    else if (frm.Name == "DeleteAllSelected")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        Properties.Settings.Default.CanUpdateGrid = false;
                        frm.Close();
                    }

                    else if (frm.Name == "DeleteConfirmation")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        Properties.Settings.Default.CanUpdateGrid = false;
                        frm.Close();
                    }

                    else if (frm.Name == "DeleteConfirmation2")
                    {
                        TimerAnim.Start();
                        await TaskDelay(200);
                        Properties.Settings.Default.CanUpdateGrid = false;
                        frm.Close();
                    }
                }

                TimerAnim.Start();
                await TaskDelay(200);
                Properties.Settings.Default.CanUpdateGrid = false;
                Close();
            }
            else
            {
                foreach (Form frm in fc)
                {
                    if (frm.Name == "DeleteAllSelected")
                    {
                        Properties.Settings.Default.CanUpdateGrid = false;
                        frm.Close();
                    }

                    if (frm.Name == "DeleteAllSelected2")
                    {
                        Properties.Settings.Default.CanUpdateGrid = false;
                        frm.Close();
                    }

                    else if (frm.Name == "DeleteSelected")
                    {
                        Properties.Settings.Default.CanUpdateGrid = false;
                        frm.Close();
                    }

                    else if (frm.Name == "DeleteConfirmation")
                    {
                        Properties.Settings.Default.CanUpdateGrid = false;
                        frm.Close();
                    }

                    else if (frm.Name == "EditCliente")
                    {
                        Properties.Settings.Default.CanUpdateGrid = false;
                        frm.Close();
                    }

                    else if (frm.Name == "EditProduto")
                    {
                        Properties.Settings.Default.CanUpdateGrid = false;
                        frm.Close();
                    }

                    else if (frm.Name == "EditFornecedor")
                    {
                        Properties.Settings.Default.CanUpdateGrid = false;
                        frm.Close();
                    }

                    else if (frm.Name == "EditFuncionario")
                    {
                        Properties.Settings.Default.CanUpdateGrid = false;
                        frm.Close();
                    }
                }

                TimerAnim.Start();
                await TaskDelay(200);
                Properties.Settings.Default.CanUpdateGrid = false;
                Close();
            }
            
            Properties.Settings.Default.CanUpdateGrid = false;
        }
    }
}
