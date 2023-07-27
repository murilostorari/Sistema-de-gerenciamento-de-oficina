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
    public partial class DeleteSelected2 : Form
    {
        FormCollection fc = Application.OpenForms;

        Success SuccessForm = new Success();
        Erro ErrorForm = new Erro();

        public static DeleteSelected2 DeleteSelectedFrame;
        public Label TmplText;
        public Label LblText;

        string ProdutoName;
        string DeleteType = "";

        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;

        public DeleteSelected2(string Tipo, string Nome)
        {
            InitializeComponent();
            SetColor();

            DeleteSelectedFrame = this;
            TmplText = TemplateText;
            LblText = Texto;

            DeleteType = Tipo;
            ProdutoName = Nome;

            if (AnimateFrames)
                guna2BorderlessForm1.AnimateWindow = true;
            else
                guna2BorderlessForm1.AnimateWindow = false;
        }

        private void Delete_Load(object sender, EventArgs e)
        {
            ItemInfoText.Text = Convert.ToString(ProdutoName);

            GifTimer.Start();
            LoadImages();

            if (DeleteType == "Produto")
            {
                TemplateText.Text = "Excluir produto";
                Texto.Text = "Você deseja mesmo excluir este produto?";
            }
            else if (DeleteType == "EntradaItem" || DeleteType == "SaidaItem")
            {
                TemplateText.Text = "Excluir item";
                Texto.Text = "Você deseja mesmo excluir este item?";
            }

            Console.WriteLine(DeleteType);
            Console.WriteLine(ProdutoName);
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Funcoes */

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

        // Ok
        private void Ok_Click(object sender, EventArgs e)
        {
            if (DeleteType == "Produto")
            {
                string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";

                OleDbConnection con = new OleDbConnection(strcon);
                OleDbCommand cmd = con.CreateCommand();

                cmd.Parameters.Add("@PRODUTO", OleDbType.VarChar).Value = ItemInfoText.Text;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM Estoque WHERE PRODUTO = @PRODUTO";

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "DeleteSelected2")
                            frm.Opacity = .0d;
                    }

                    Success.SuccessFrame.LblText.Text = "Produto excluído com sucesso!";

                    SuccessForm.Show();
                    SuccessForm.Text = "Produto excluído com sucesso!";
                }
                catch (Exception)
                {
                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "DeleteSelected2")
                            frm.Opacity = .0d;
                    }

                    ErrorForm.Text = "Erro";
                    Erro.ErrorFrame.LblText.Text = "Erro ao excluir produto!";

                    ErrorForm.Show();
                    ErrorForm.Text = "Erro ao excluir produto!";
                }
                finally
                {
                    con.Close();

                    Properties.Settings.Default.CanUpdateGrid = true;
                }
            }

            else if (DeleteType == "EntradaItem")
            {
                string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";

                OleDbConnection con = new OleDbConnection(strcon);
                OleDbCommand cmd = con.CreateCommand();

                cmd.Parameters.Add("@PRODUTO", OleDbType.VarChar).Value = ItemInfoText.Text;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM EntradaDeItens WHERE PRODUTO = @PRODUTO";

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "DeleteSelected2")
                            frm.Opacity = .0d;
                    }

                    Success.SuccessFrame.LblText.Text = "Item excluído com sucesso!";

                    SuccessForm.Show();
                    SuccessForm.Text = "Item excluído com sucesso!";
                }
                catch (Exception)
                {
                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "DeleteSelected2")
                            frm.Opacity = .0d;
                    }

                    ErrorForm.Text = "Erro";
                    Erro.ErrorFrame.LblText.Text = "Erro ao excluir item!";

                    ErrorForm.Show();
                    ErrorForm.Text = "Erro ao excluir item!";
                }
                finally
                {
                    con.Close();

                    Properties.Settings.Default.CanUpdateGrid = true;
                }
            }

            else if (DeleteType == "SaidaItem")
            {
                string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";

                OleDbConnection con = new OleDbConnection(strcon);
                OleDbCommand cmd = con.CreateCommand();

                cmd.Parameters.Add("@PRODUTO", OleDbType.VarChar).Value = ItemInfoText.Text;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM SaidaDeItens WHERE PRODUTO = @PRODUTO";

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "DeleteSelected2")
                            frm.Opacity = .0d;
                    }

                    Success.SuccessFrame.LblText.Text = "Item excluído com sucesso!";

                    SuccessForm.Show();
                    SuccessForm.Text = "Item excluído com sucesso!";
                }
                catch (Exception)
                {
                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "DeleteSelected2")
                            frm.Opacity = .0d;
                    }

                    ErrorForm.Text = "Erro";
                    Erro.ErrorFrame.LblText.Text = "Erro ao excluir item!";

                    ErrorForm.Show();
                    ErrorForm.Text = "Erro ao excluir item!";
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
