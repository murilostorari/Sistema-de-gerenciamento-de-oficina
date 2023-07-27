using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCC.Frames
{
    public partial class DeleteConfirmation : Form
    {
        FormCollection fc = Application.OpenForms;

        Success SuccessForm = new Success();
        Erro ErrorForm = new Erro();

        public static DeleteConfirmation DeleteConfirmFrame;
        public Label TopText;
        public Label LblText;

        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;

        public static List<int> IDs = new List<int>();
        string DeleteType = "";

        public DeleteConfirmation(string Tipo, List<int> AllIDs)
        {
            InitializeComponent();
            SetColor();

            DeleteConfirmFrame = this;
            TopText = TextoCima;
            LblText = Texto;

            IDs = AllIDs;
            DeleteType = Tipo;

            if (AnimateFrames)
                guna2BorderlessForm1.AnimateWindow = true;
            else
                guna2BorderlessForm1.AnimateWindow = false;
        }

        private void DeleteConfirmation_Load(object sender, EventArgs e)
        {
            GifTimer.Start();
            LoadImages();

            if (DeleteType == "Clientes")
                Texto.Text = "Por motivos de segurança, insira a senha para excluir estes clientes.";
            else if (DeleteType == "Fornecedores")
                Texto.Text = "Por motivos de segurança, insira a senha para excluir estes fornecedores.";
            else if (DeleteType == "Funcionarios")
                Texto.Text = "Por motivos de segurança, insira a senha para excluir estes funcionários.";
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Funcoes */

        // Gif speed
        Image[] images = new Image[46];
        Image[] images2 = new Image[37];

        private void LoadImages()
        {
            for (int i = 1; i <= 46; i++)
            {
                string path = $@"{Application.StartupPath}\Gifs\LockClose\frame_{i:00}_delay-0.03s.gif";
                Image image = Image.FromFile(path);
                images[i - 1] = image;
            }
        }

        private void LoadImages2()
        {
            for (int i = 1; i <= 37; i++)
            {
                string path = $@"{Application.StartupPath}\Gifs\LockOpen\frame_{i:00}_delay-0.03s.gif";
                Image image = Image.FromFile(path);
                images2[i - 1] = image;
            }
        }

        int i = 0;
        int i2 = 0;

        private void GifTimer_Tick(object sender, EventArgs e)
        {
            i %= 46;
            Gif.Image = images[i];
            i += 1;

            if (i == 46)
                GifTimer.Stop();
        }

        private void GifTimer2_Tick(object sender, EventArgs e)
        {
            i2 %= 37;
            Gif.Image = images2[i2];
            i2 += 1;

            if (i2 == 37)
                GifTimer2.Stop();
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

            TextoCima.ForeColor = ThemeManager.FontColor;
            Texto.ForeColor = ThemeManager.PresetLabelColor;

            label6.ForeColor = ThemeManager.DarkGrayLabelsFontColor;
            label6.BackColor = ThemeManager.FormBackColor;

            PasswordHint.ForeColor = ThemeManager.RedFontColor;

            Password.FillColor = ThemeManager.TextBoxFillColor;
            Password.ForeColor = ThemeManager.TextBoxForeColor;
            Password.BorderColor = ThemeManager.TextBoxBorderColor;
            Password.HoverState.BorderColor = ThemeManager.TextBoxHoverBorderColor;
            Password.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
            Password.FocusedState.ForeColor = ThemeManager.TextBoxForeColor;

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
        private async void Ok_Click(object sender, EventArgs e)
        {
            DeleteConfirmation DeleteConfirmationForm = (DeleteConfirmation)Application.OpenForms["DeleteConfirmation"];

            if (DeleteType == "Clientes")
            {
                if (Password.Text == "DeleteClientes")
                {
                    GifTimer2.Start();
                    LoadImages2();

                    await TaskDelay(1000);

                    OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

                    for (int i = 0; i < IDs.Count; i++)
                    {
                        OleDbCommand cmd = new OleDbCommand("DELETE * FROM Clientes WHERE ID = " + IDs[i], con);

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();

                            foreach (Form frm in fc)
                            {
                                if (frm.Name == "DeleteConfirmation")
                                    frm.Opacity = .0d;
                            }

                            Success.SuccessFrame.LblText.Text = "Clientes excluídos com sucesso!";
                            SuccessForm.Text = "Clientes excluídos com sucesso!";

                            SuccessForm.Show();
                            DeleteConfirmFrame.Opacity = .0d;
                        }
                        catch (Exception)
                        {
                            foreach (Form frm in fc)
                            {
                                if (frm.Name == "DeleteConfirmation")
                                    frm.Opacity = .0d;
                            }

                            Erro.ErrorFrame.LblText.Text = "Erro ao excluir clientes!";
                            ErrorForm.Text = "Erro ao excluir clientes!";

                            ErrorForm.Show();
                            DeleteConfirmFrame.Opacity = .0d;
                        }
                        finally
                        {
                            con.Close();

                            Properties.Settings.Default.CanUpdateGrid = true;
                        }
                    }
                }

                else if (Password.Text == "")
                {
                    if (IsDarkModeEnabled)
                    {
                        PasswordHint.Text = "Insira a senha correta";
                        PasswordHint.Visible = true;
                        Password.BorderColor = Color.FromArgb(255, 33, 0);
                        Password.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }
                    else
                    {
                        PasswordHint.Visible = false;
                        Password.BorderColor = Color.FromArgb(255, 3, 0);
                        Password.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                }

                else
                {
                    if (IsDarkModeEnabled)
                    {
                        PasswordHint.Text = "Senha incorreta";
                        PasswordHint.Visible = true;
                        Password.BorderColor = Color.FromArgb(255, 33, 0);
                        Password.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }
                    else
                    {
                        PasswordHint.Visible = false;
                        Password.BorderColor = Color.FromArgb(255, 3, 0);
                        Password.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                }
            }

            else if (DeleteType == "Fornecedores")
            {
                if (Password.Text == "DeleteFornecedores")
                {
                    GifTimer2.Start();
                    LoadImages2();

                    await TaskDelay(1000);

                    OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

                    for (int i = 0; i < IDs.Count; i++)
                    {
                        OleDbCommand cmd = new OleDbCommand("DELETE * FROM Fornecedores WHERE ID = " + IDs[i], con);

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();

                            foreach (Form frm in fc)
                            {
                                if (frm.Name == "DeleteConfirmation")
                                    frm.Opacity = .0d;
                            }

                            Success.SuccessFrame.LblText.Text = "Fornecedores excluídos com sucesso!";
                            SuccessForm.Text = "Fornecedores excluídos com sucesso!";

                            SuccessForm.Show();
                            DeleteConfirmFrame.Opacity = .0d;
                        }
                        catch (Exception)
                        {
                            foreach (Form frm in fc)
                            {
                                if (frm.Name == "DeleteConfirmation")
                                    frm.Opacity = .0d;
                            }

                            Erro.ErrorFrame.LblText.Text = "Erro ao excluir fornecedores!";
                            ErrorForm.Text = "Erro ao excluir fornecedores!";

                            ErrorForm.Show();
                            DeleteConfirmFrame.Opacity = .0d;
                        }
                        finally
                        {
                            con.Close();

                            Properties.Settings.Default.CanUpdateGrid = true;
                        }
                    }
                }

                else if (Password.Text == "")
                {
                    if (IsDarkModeEnabled)
                    {
                        PasswordHint.Text = "Insira a senha correta";
                        PasswordHint.Visible = true;
                        Password.BorderColor = Color.FromArgb(255, 33, 0);
                        Password.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }
                    else
                    {
                        PasswordHint.Visible = false;
                        Password.BorderColor = Color.FromArgb(255, 3, 0);
                        Password.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                }

                else
                {
                    if (IsDarkModeEnabled)
                    {
                        PasswordHint.Text = "Senha incorreta";
                        PasswordHint.Visible = true;
                        Password.BorderColor = Color.FromArgb(255, 33, 0);
                        Password.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }
                    else
                    {
                        PasswordHint.Visible = false;
                        Password.BorderColor = Color.FromArgb(255, 3, 0);
                        Password.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                }
            }

            else if (DeleteType == "Funcionarios")
            {
                if (Password.Text == "DeleteFuncionarios")
                {
                    GifTimer2.Start();
                    LoadImages2();

                    await TaskDelay(1000);

                    OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

                    for (int i = 0; i < IDs.Count; i++)
                    {
                        OleDbCommand cmd = new OleDbCommand("DELETE * FROM Funcionarios WHERE ID = " + IDs[i], con);

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();

                            foreach (Form frm in fc)
                            {
                                if (frm.Name == "DeleteConfirmation")
                                    frm.Opacity = .0d;
                            }

                            Success.SuccessFrame.LblText.Text = "Funcionários excluídos com sucesso!";
                            SuccessForm.Text = "Funcionários excluídos com sucesso!";

                            SuccessForm.Show();
                            DeleteConfirmFrame.Opacity = .0d;
                        }
                        catch (Exception)
                        {
                            foreach (Form frm in fc)
                            {
                                if (frm.Name == "DeleteConfirmation")
                                    frm.Opacity = .0d;
                            }

                            Erro.ErrorFrame.LblText.Text = "Erro ao excluir funcionários!";
                            ErrorForm.Text = "Erro ao excluir funcionários!";

                            ErrorForm.Show();
                            DeleteConfirmFrame.Opacity = .0d;
                        }
                        finally
                        {
                            con.Close();

                            Properties.Settings.Default.CanUpdateGrid = true;
                        }
                    }
                }

                else if (Password.Text == "")
                {
                    if (IsDarkModeEnabled)
                    {
                        PasswordHint.Text = "Insira a senha correta";
                        PasswordHint.Visible = true;
                        Password.BorderColor = Color.FromArgb(255, 33, 0);
                        Password.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }
                    else
                    {
                        PasswordHint.Visible = false;
                        Password.BorderColor = Color.FromArgb(255, 3, 0);
                        Password.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                }

                else
                {
                    if (IsDarkModeEnabled)
                    {
                        PasswordHint.Text = "Senha incorreta";
                        PasswordHint.Visible = true;
                        Password.BorderColor = Color.FromArgb(255, 33, 0);
                        Password.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }
                    else
                    {
                        PasswordHint.Visible = false;
                        Password.BorderColor = Color.FromArgb(255, 3, 0);
                        Password.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                }
            }
        }

        private async void Cancelar_Click(object sender, EventArgs e)
        {
            if (AnimateFrames)
            {
                TimerAnim.Start();
                await TaskDelay(200);
            }

            foreach (Form frm in fc)
            {
                if (frm.Name == "DeleteAllSelected")
                    frm.Close();
            }

            Close();
        }

        private void Password_TextChanged(object sender, EventArgs e)
        {
            PasswordHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                Password.BorderColor = Color.FromArgb(80, 80, 80);
                Password.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                Password.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }
            else
            {
                Password.BorderColor = Color.FromArgb(210, 210, 210);
                Password.FocusedState.BorderColor = Color.Black;
                Password.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }
        }

        private async void Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                DeleteConfirmation DeleteConfirmationForm = (DeleteConfirmation)Application.OpenForms["DeleteConfirmation"];

                if (DeleteType == "Clientes")
                {
                    if (Password.Text == "DeleteClientes")
                    {
                        GifTimer2.Start();
                        LoadImages2();

                        await TaskDelay(1000);

                        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

                        for (int i = 0; i < IDs.Count; i++)
                        {
                            OleDbCommand cmd = new OleDbCommand("DELETE * FROM Clientes WHERE ID = " + IDs[i], con);

                            try
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();

                                foreach (Form frm in fc)
                                {
                                    if (frm.Name == "DeleteConfirmation")
                                        frm.Opacity = .0d;
                                }

                                Success.SuccessFrame.LblText.Text = "Clientes excluídos com sucesso!";
                                SuccessForm.Text = "Clientes excluídos com sucesso!";

                                SuccessForm.Show();
                                DeleteConfirmFrame.Opacity = .0d;
                            }
                            catch (Exception)
                            {
                                foreach (Form frm in fc)
                                {
                                    if (frm.Name == "DeleteConfirmation")
                                        frm.Opacity = .0d;
                                }

                                Erro.ErrorFrame.LblText.Text = "Erro ao excluir clientes!";
                                ErrorForm.Text = "Erro ao excluir clientes!";

                                ErrorForm.Show();
                                DeleteConfirmFrame.Opacity = .0d;
                            }
                            finally
                            {
                                con.Close();

                                Properties.Settings.Default.CanUpdateGrid = true;
                            }
                        }
                    }

                    else if (Password.Text == "")
                    {
                        if (IsDarkModeEnabled)
                        {
                            PasswordHint.Text = "Insira a senha correta";
                            PasswordHint.Visible = true;
                            Password.BorderColor = Color.FromArgb(255, 33, 0);
                            Password.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                        }
                        else
                        {
                            PasswordHint.Visible = false;
                            Password.BorderColor = Color.FromArgb(255, 3, 0);
                            Password.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                        }
                    }

                    else
                    {
                        if (IsDarkModeEnabled)
                        {
                            PasswordHint.Text = "Senha incorreta";
                            PasswordHint.Visible = true;
                            Password.BorderColor = Color.FromArgb(255, 33, 0);
                            Password.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                        }
                        else
                        {
                            PasswordHint.Visible = false;
                            Password.BorderColor = Color.FromArgb(255, 3, 0);
                            Password.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                        }
                    }
                }

                else if (DeleteType == "Fornecedores")
                {
                    if (Password.Text == "DeleteFornecedores")
                    {
                        GifTimer2.Start();
                        LoadImages2();

                        await TaskDelay(1000);

                        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

                        for (int i = 0; i < IDs.Count; i++)
                        {
                            OleDbCommand cmd = new OleDbCommand("DELETE * FROM Fornecedores WHERE ID = " + IDs[i], con);

                            try
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();

                                foreach (Form frm in fc)
                                {
                                    if (frm.Name == "DeleteConfirmation")
                                        frm.Opacity = .0d;
                                }

                                Success.SuccessFrame.LblText.Text = "Fornecedores excluídos com sucesso!";
                                SuccessForm.Text = "Fornecedores excluídos com sucesso!";

                                SuccessForm.Show();
                                DeleteConfirmFrame.Opacity = .0d;
                            }
                            catch (Exception)
                            {
                                foreach (Form frm in fc)
                                {
                                    if (frm.Name == "DeleteConfirmation")
                                        frm.Opacity = .0d;
                                }

                                Erro.ErrorFrame.LblText.Text = "Erro ao excluir fornecedores!";
                                ErrorForm.Text = "Erro ao excluir fornecedores!";

                                ErrorForm.Show();
                                DeleteConfirmFrame.Opacity = .0d;
                            }
                            finally
                            {
                                con.Close();

                                Properties.Settings.Default.CanUpdateGrid = true;
                            }
                        }
                    }

                    else if (Password.Text == "")
                    {
                        if (IsDarkModeEnabled)
                        {
                            PasswordHint.Text = "Insira a senha correta";
                            PasswordHint.Visible = true;
                            Password.BorderColor = Color.FromArgb(255, 33, 0);
                            Password.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                        }
                        else
                        {
                            PasswordHint.Visible = false;
                            Password.BorderColor = Color.FromArgb(255, 3, 0);
                            Password.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                        }
                    }

                    else
                    {
                        if (IsDarkModeEnabled)
                        {
                            PasswordHint.Text = "Senha incorreta";
                            PasswordHint.Visible = true;
                            Password.BorderColor = Color.FromArgb(255, 33, 0);
                            Password.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                        }
                        else
                        {
                            PasswordHint.Visible = false;
                            Password.BorderColor = Color.FromArgb(255, 3, 0);
                            Password.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                        }
                    }
                }

                else if (DeleteType == "Funcionarios")
                {
                    if (Password.Text == "DeleteFuncionarios")
                    {
                        GifTimer2.Start();
                        LoadImages2();

                        await TaskDelay(1000);

                        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

                        for (int i = 0; i < IDs.Count; i++)
                        {
                            OleDbCommand cmd = new OleDbCommand("DELETE * FROM Funcionarios WHERE ID = " + IDs[i], con);

                            try
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();

                                foreach (Form frm in fc)
                                {
                                    if (frm.Name == "DeleteConfirmation")
                                        frm.Opacity = .0d;
                                }

                                Success.SuccessFrame.LblText.Text = "Funcionários excluídos com sucesso!";
                                SuccessForm.Text = "Funcionários excluídos com sucesso!";

                                SuccessForm.Show();
                                DeleteConfirmFrame.Opacity = .0d;
                            }
                            catch (Exception)
                            {
                                foreach (Form frm in fc)
                                {
                                    if (frm.Name == "DeleteConfirmation")
                                        frm.Opacity = .0d;
                                }

                                Erro.ErrorFrame.LblText.Text = "Erro ao excluir funcionários!";
                                ErrorForm.Text = "Erro ao excluir funcionários!";

                                ErrorForm.Show();
                                DeleteConfirmFrame.Opacity = .0d;
                            }
                            finally
                            {
                                con.Close();

                                Properties.Settings.Default.CanUpdateGrid = true;
                            }
                        }
                    }

                    else if (Password.Text == "")
                    {
                        if (IsDarkModeEnabled)
                        {
                            PasswordHint.Text = "Insira a senha correta";
                            PasswordHint.Visible = true;
                            Password.BorderColor = Color.FromArgb(255, 33, 0);
                            Password.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                        }
                        else
                        {
                            PasswordHint.Visible = false;
                            Password.BorderColor = Color.FromArgb(255, 3, 0);
                            Password.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                        }
                    }

                    else
                    {
                        if (IsDarkModeEnabled)
                        {
                            PasswordHint.Text = "Senha incorreta";
                            PasswordHint.Visible = true;
                            Password.BorderColor = Color.FromArgb(255, 33, 0);
                            Password.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                        }
                        else
                        {
                            PasswordHint.Visible = false;
                            Password.BorderColor = Color.FromArgb(255, 3, 0);
                            Password.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                        }
                    }
                }
            }
        }
    }
}
