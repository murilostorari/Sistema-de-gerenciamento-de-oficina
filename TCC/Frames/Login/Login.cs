using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace TCC
{
    public partial class Login : Form
    {
        FormCollection fc = Application.OpenForms;

        Thread nt;

        bool PassHided;
        bool ErrorOpen;

        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;
        //bool LembrarSenha = Properties.Settings.Default.RememberPassword;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (AnimateFrames)
            {
                Usuario.Animated = true;
                Senha.Animated = true;
                LoginBtn.Animated = true;
                Remember.Animated = true;
            }
            else
            {
                Usuario.Animated = false;
                Senha.Animated = false;
                LoginBtn.Animated = false;
                Remember.Animated = false;
            }

            if (IsDarkModeEnabled)
            {
                this.BackColor = Color.FromArgb(38, 38, 38);

                label1.ForeColor = Color.FromArgb(250, 250, 250);
                label3.ForeColor = Color.FromArgb(250, 250, 250);

                Usuario.FillColor = Color.FromArgb(38, 38, 38);
                Usuario.ForeColor = Color.FromArgb(245, 245, 245);
                Usuario.PlaceholderForeColor = Color.FromArgb(210, 210, 210);
                Usuario.BorderColor = Color.FromArgb(80, 80, 80);
                Usuario.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                Usuario.FocusedState.BorderColor = Color.FromArgb(180, 180, 180);
                Usuario.FocusedState.ForeColor = Color.FromArgb(245, 245, 245);

                Senha.FillColor = Color.FromArgb(38, 38, 38);
                Senha.ForeColor = Color.FromArgb(245, 245, 245);
                Senha.PlaceholderForeColor = Color.FromArgb(210, 210, 210);
                Senha.BorderColor = Color.FromArgb(80, 80, 80);
                Senha.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                Senha.FocusedState.BorderColor = Color.FromArgb(180, 180, 180);
                Senha.FocusedState.ForeColor = Color.FromArgb(245, 245, 245);

                LoginBtn.FillColor = Color.FromArgb(255, 33, 0);
                LoginBtn.HoverState.FillColor = Color.FromArgb(255, 63, 0);
                LoginBtn.CheckedState.FillColor = Color.FromArgb(255, 43, 0);

                Remember.CheckedState.FillColor = Color.FromArgb(255, 33, 0);
                Remember.CheckedState.BorderColor = Color.FromArgb(255, 33, 0);

                ToolTip.ForeColor = Color.FromArgb(250, 250, 250);
                ToolTip.BorderColor = Color.FromArgb(78, 78, 78);
                ToolTip.BackColor = Color.FromArgb(65, 65, 65);

                ToolTip2.ForeColor = Color.FromArgb(250, 250, 250);
                ToolTip2.BorderColor = Color.FromArgb(78, 78, 78);
                ToolTip2.BackColor = Color.FromArgb(65, 65, 65);
            }
            else
            {
                this.BackColor = Color.FromArgb(255, 255, 255);

                label1.ForeColor = Color.FromArgb(0, 0, 0);
                label3.ForeColor = Color.FromArgb(0, 0, 0);

                Usuario.FillColor = Color.FromArgb(255, 255, 255);
                Usuario.ForeColor = Color.FromArgb(0, 0, 0);
                Usuario.PlaceholderForeColor = Color.FromArgb(130, 130, 130);
                Usuario.BorderColor = Color.FromArgb(210, 210 , 210);
                Usuario.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                Usuario.FocusedState.BorderColor = Color.FromArgb(0, 0, 0);
                Usuario.FocusedState.ForeColor = Color.FromArgb(0, 0, 0);

                Senha.FillColor = Color.FromArgb(255, 255, 255);
                Senha.ForeColor = Color.FromArgb(0, 0, 0);
                Senha.PlaceholderForeColor = Color.FromArgb(130, 130, 130);
                Senha.BorderColor = Color.FromArgb(210, 210, 210);
                Senha.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                Senha.FocusedState.BorderColor = Color.FromArgb(0, 0, 0);
                Senha.FocusedState.ForeColor = Color.FromArgb(0, 0, 0);

                LoginBtn.FillColor = Color.FromArgb(255, 3, 0);
                LoginBtn.HoverState.FillColor = Color.FromArgb(195, 3, 0);
                LoginBtn.CheckedState.FillColor = Color.FromArgb(175, 3, 0);

                Remember.CheckedState.FillColor = Color.FromArgb(255, 3, 0);
                Remember.CheckedState.BorderColor = Color.FromArgb(255, 3, 0);

                ToolTip.ForeColor = Color.FromArgb(100, 100, 100);
                ToolTip.BorderColor = Color.FromArgb(210, 210, 210);
                ToolTip.BackColor = Color.FromArgb(255, 255, 255);

                ToolTip2.ForeColor = Color.FromArgb(100, 100, 100);
                ToolTip2.BorderColor = Color.FromArgb(210, 210, 210);
                ToolTip2.BackColor = Color.FromArgb(255, 255, 255);
            }
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Funcoes */

        /*--------------------------------------------------------------------------------------------*/

        /* Outros */

        // Hide pass
        private void Hide_Click(object sender, EventArgs e)
        {
            if (PassHided)
            {
                if (Senha.Text != "")
                {
                    PassHided = false;
                    Senha.UseSystemPasswordChar = false;
                    HidePassword.Image = Properties.Resources.show_pass;
                    ToolTip.Active = true;
                    ToolTip2.Active = false;
                }
            }
            else
            {
                if (Senha.Text != "")
                {
                    PassHided = true;
                    Senha.UseSystemPasswordChar = true;
                    HidePassword.Image = Properties.Resources.hide_pass;
                    ToolTip.Active = false; 
                    ToolTip2.Active = true; 
                }
            }
        }

        // Login button
        private void LoginBtn_Click_1(object sender, EventArgs e)
        {
            if (Usuario.Text == "Admin" && Senha.Text == "123456")
            {
                Main MainForm = new Main();

                MainForm.Show();

                Close();

                //Properties.Settings.Default.RememberPassword = Remember.Checked;
            }
            else
            {
                TCC.Frames.LoginError ErrorForm = new TCC.Frames.LoginError();

                foreach (Form frm in fc)
                {
                    if (frm.Name == "Erro")
                    {
                        ErrorOpen = true;
                        Console.WriteLine("Erro frame aberto");
                    }
                    else
                    {
                        ErrorOpen = false;
                    }
                }

                if (!ErrorOpen)
                {
                    ErrorForm.Show();
                }
            }
        }

        private void Usuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (Usuario.Text == "Admin" && Senha.Text == "123456")
                {
                    Main MainForm = new Main();

                    MainForm.Show();

                    Hide();
                }
                else
                {
                    TCC.Frames.LoginError ErrorForm = new TCC.Frames.LoginError();

                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "Erro")
                        {
                            ErrorOpen = true;
                            Console.WriteLine("Erro frame aberto");
                        }
                        else
                        {
                            ErrorOpen = false;
                        }
                    }

                    if (!ErrorOpen)
                    {
                        ErrorForm.Show();
                    }
                }
            }
        }

        private void Senha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (Usuario.Text == "Admin" && Senha.Text == "123456")
                {
                    Main MainForm = new Main();

                    MainForm.Show();

                    Hide();
                }
                else
                {
                    TCC.Frames.LoginError ErrorForm = new TCC.Frames.LoginError();

                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "Erro")
                        {
                            ErrorOpen = true;
                            Console.WriteLine("Erro frame aberto");
                        }
                        else
                        {
                            ErrorOpen = false;
                        }
                    }

                    if (!ErrorOpen)
                    {
                        ErrorForm.Show();
                    }
                }
            }
        }

        // Lembrar senha
        private void Remember_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RememberPassword = Remember.Checked;
        }
    }
}