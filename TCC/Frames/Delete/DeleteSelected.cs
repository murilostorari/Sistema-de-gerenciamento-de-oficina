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
    public partial class DeleteSelected : Form
    {
        FormCollection fc = Application.OpenForms;

        Success SuccessForm = new Success();
        Erro ErrorForm = new Erro();

        public static DeleteSelected DeleteSelectedFrame;
        public Label TmplText;
        public Label LblText;

        int TextID;
        string DeleteType = "";

        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;

        public DeleteSelected(string Tipo, int ID)
        {
            InitializeComponent();
            SetColor();

            DeleteSelectedFrame = this;
            TmplText = TemplateText;
            LblText = Texto;

            TextID = ID;
            DeleteType = Tipo;

            if (AnimateFrames)
                guna2BorderlessForm1.AnimateWindow = true;
            else
                guna2BorderlessForm1.AnimateWindow = false;
        }

        private void Delete_Load(object sender, EventArgs e)
        {
            IDInt.Text = Convert.ToString(TextID);

            GifTimer.Start();
            LoadImages();

            if (DeleteType == "Cliente")
            {
                TemplateText.Text = "Excluir cliente";
                Texto.Text = "Você deseja mesmo excluir este cliente?";
            }
            else if (DeleteType == "Fornecedor")
            {
                TemplateText.Text = "Excluir fornecedor";
                Texto.Text = "Você deseja mesmo excluir este fornecedor?";
            }
            else if (DeleteType == "Funcionario")
            {
                TemplateText.Text = "Excluir funcionário";
                Texto.Text = "Você deseja mesmo excluir este funcionário?";
            }
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
            if (DeleteType == "Cliente")
            {
                string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";

                OleDbConnection con = new OleDbConnection(strcon);
                OleDbCommand cmd = con.CreateCommand();

                cmd.Parameters.Add("@ID", OleDbType.VarChar).Value = IDInt.Text;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM Clientes WHERE ID = @ID";

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "DeleteSelected")
                            frm.Opacity = .0d;
                    }

                    Success.SuccessFrame.LblText.Text = "Cliente excluído com sucesso!";

                    SuccessForm.Show();
                    SuccessForm.Text = "Cliente excluído com sucesso!";
                }
                catch (Exception)
                {
                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "DeleteSelected")
                            frm.Opacity = .0d;
                    }

                    ErrorForm.Text = "Erro";
                    Erro.ErrorFrame.LblText.Text = "Erro ao excluir cliente!";

                    ErrorForm.Show();
                    ErrorForm.Text = "Erro ao excluir cliente!";
                }
                finally
                {
                    con.Close();

                    Properties.Settings.Default.CanUpdateGrid = true;
                }
            }

            else if (DeleteType == "Fornecedor")
            {
                string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";

                OleDbConnection con = new OleDbConnection(strcon);
                OleDbCommand cmd = con.CreateCommand();

                cmd.Parameters.Add("@ID", OleDbType.VarChar).Value = IDInt.Text;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM Fornecedores WHERE ID = @ID";

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "DeleteSelected")
                            frm.Opacity = .0d;
                    }

                    Success.SuccessFrame.LblText.Text = "Fornecedor excluído com sucesso!";

                    SuccessForm.Show();
                    SuccessForm.Text = "Cliente excluído com sucesso!";
                }
                catch (Exception)
                {
                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "DeleteSelected")
                            frm.Opacity = .0d;
                    }

                    ErrorForm.Text = "Erro";
                    Erro.ErrorFrame.LblText.Text = "Erro ao excluir fornecedor!";

                    ErrorForm.Show();
                    ErrorForm.Text = "Erro ao excluir fornecedor!";
                }
                finally
                {
                    con.Close();

                    Properties.Settings.Default.CanUpdateGrid = true;
                }
            }

            else if (DeleteType == "Funcionario")
            {
                string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";

                OleDbConnection con = new OleDbConnection(strcon);
                OleDbCommand cmd = con.CreateCommand();

                cmd.Parameters.Add("@ID", OleDbType.VarChar).Value = IDInt.Text;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM Funcionarios WHERE ID = @ID";

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "DeleteSelected")
                            frm.Opacity = .0d;
                    }

                    Success.SuccessFrame.LblText.Text = "Funcionário excluído com sucesso!";

                    SuccessForm.Show();
                    SuccessForm.Text = "Funcionário excluído com sucesso!";
                }
                catch (Exception)
                {
                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "DeleteSelected")
                            frm.Opacity = .0d;
                    }

                    ErrorForm.Text = "Erro";
                    Erro.ErrorFrame.LblText.Text = "Erro ao excluir funcionário!";

                    ErrorForm.Show();
                    ErrorForm.Text = "Erro ao excluir funcionário!";
                }
                finally
                {
                    con.Close();

                    Properties.Settings.Default.CanUpdateGrid = true;
                }
            }
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
