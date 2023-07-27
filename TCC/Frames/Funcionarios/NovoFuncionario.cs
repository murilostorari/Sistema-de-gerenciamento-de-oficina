using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;
using Newtonsoft.Json;
using System.Net.Http;

namespace TCC.Frames.Estoque
{
    public partial class NovoFuncionario : Form
    {
        FormCollection fc = Application.OpenForms;

        bool FormLoaded;
        bool CloseOpen;
        bool BackspacePressed;
        bool TextoChanged;

        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;
        bool AutoCompleteValues = Properties.Settings.Default.AutoCompleteCurrencyValues;

        List<Guna.UI2.WinForms.Guna2Button> GunaButtons;
        List<Guna.UI2.WinForms.Guna2Button> GunaBorderButtons;
        List<Guna.UI2.WinForms.Guna2TextBox> GunaTextBox;
        List<Guna.UI2.WinForms.Guna2ComboBox> GunaComboBox;
        List<Guna.UI2.WinForms.Guna2HtmlLabel> GunaHints;

        List<Label> NormalLabels;

        public NovoFuncionario()
        {
            InitializeComponent();

            AddControlsToList();
            SetColor();

            if (AnimateFrames)
                guna2BorderlessForm1.AnimateWindow = true;
            else
                guna2BorderlessForm1.AnimateWindow = false;
        }

        private void NovoProduto_Load(object sender, EventArgs e)
        {
            foreach (Control ct in EndereçoLabel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in DadosBancarios.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Outros.Controls)
            {
                ct.TabStop = false;
            }

            Observaçoes.ForeColor = Color.Black;

            string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\InfoDTB.mdb;Persist Security Info=True";
            OleDbConnection con = new OleDbConnection(strcon);

            con.Open();

            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT NOME FROM BancosInfo";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            adapter.Fill(dt);

            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

            foreach (DataRow dtr in dt.Rows)
            {
                collection.Add(dtr["NOME"].ToString());
            }

            Banco.AutoCompleteMode = AutoCompleteMode.Suggest;
            Banco.AutoCompleteSource = AutoCompleteSource.CustomSource;

            Banco.AutoCompleteCustomSource = collection;

            con.Close();

            DataEntrada.Value = Convert.ToDateTime(DateTime.Today);

            FormLoaded = true;
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Funcoes */

        // Cancelar cadastro 
        private void Cancel()
        {
            CloseConfirm CloseForm = new CloseConfirm();

            foreach (Form frm in fc)
            {
                if (frm.Name == "CloseConfirm")
                {
                    CloseOpen = true;
                    Console.WriteLine("Close confirm frame aberto");
                }
                else
                {
                    CloseOpen = false;

                    if (TextoChanged)
                    {
                        Frames.CloseConfirm.CloseFrame.TopText.Text = "Cancelar cadastro";
                        Frames.CloseConfirm.CloseFrame.LblText.Text = "Você deseja mesmo cancelar o cadastro de novo funcionário?";
                    }
                }

                if (frm.Name == "NovoFuncionario")
                {
                    frm.Opacity = 0;
                }
            }

            if (CloseOpen != true && TextoChanged == true)
            {
                if (Properties.Settings.Default.ShowPopups)
                    CloseForm.Show();
                else
                    Close();
            }
            else if (TextoChanged != true)
                Close();
        }

        // Delay
        async Task TaskDelay(int valor)
        {
            await Task.Delay(valor);
            //BackspacePressed = false;
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

        // Proxima etapa 1
        private void ProximoN1()
        {
            InformacoesPrincipais.Visible = false;
            InformacoesPrincipais.Location = new Point(12, 7734);

            EndereçoLabel.Visible = true;
            EndereçoLabel.Location = new Point(12, 134);

            DadosBancarios.Visible = false;
            DadosBancarios.Location = new Point(12, 7734);

            Outros.Visible = false;
            Outros.Location = new Point(12, 7734);

            if (IsDarkModeEnabled)
            {
                ProgressText1.ForeColor = Color.FromArgb(255, 33, 0);
                ProgressBar1.BackColor = Color.FromArgb(255, 33, 0);

                ProgressText2.ForeColor = Color.FromArgb(255, 33, 0);
                ProgressBar2.BackColor = Color.FromArgb(255, 33, 0);

                ProgressText3.ForeColor = Color.FromArgb(200, 200, 200);
                ProgressBar3.BackColor = Color.FromArgb(230, 230, 230);

                ProgressText4.ForeColor = Color.FromArgb(200, 200, 200);
                ProgressBar4.BackColor = Color.FromArgb(230, 230, 230);
            }
            else
            {
                ProgressText1.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar1.BackColor = Color.FromArgb(255, 3, 0);

                ProgressText2.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar2.BackColor = Color.FromArgb(255, 3, 0);

                ProgressText3.ForeColor = Color.FromArgb(200, 200, 200);
                ProgressBar3.BackColor = Color.FromArgb(230, 230, 230);

                ProgressText4.ForeColor = Color.FromArgb(200, 200, 200);
                ProgressBar4.BackColor = Color.FromArgb(230, 230, 230);
            }

            foreach (Control ct in InformacoesPrincipais.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in EndereçoLabel.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in DadosBancarios.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Outros.Controls)
            {
                ct.TabStop = false;
            }
        }

        // Proxima etapa 2
        private void ProximoN2()
        {
            InformacoesPrincipais.Visible = false;
            InformacoesPrincipais.Location = new Point(12, 7734);

            EndereçoLabel.Visible = false;
            EndereçoLabel.Location = new Point(12, 7734);

            DadosBancarios.Visible = true;
            DadosBancarios.Location = new Point(12, 134);

            Outros.Visible = false;
            Outros.Location = new Point(12, 7734);

            if (IsDarkModeEnabled)
            {
                ProgressText1.ForeColor = Color.FromArgb(255, 33, 0);
                ProgressBar1.BackColor = Color.FromArgb(255, 33, 0);

                ProgressText2.ForeColor = Color.FromArgb(255, 33, 0);
                ProgressBar2.BackColor = Color.FromArgb(255, 33, 0);

                ProgressText3.ForeColor = Color.FromArgb(255, 33, 0);
                ProgressBar3.BackColor = Color.FromArgb(255, 33, 0);

                ProgressText4.ForeColor = Color.FromArgb(200, 200, 200);
                ProgressBar4.BackColor = Color.FromArgb(230, 230, 230);
            }
            else
            {
                ProgressText1.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar1.BackColor = Color.FromArgb(255, 3, 0);

                ProgressText2.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar2.BackColor = Color.FromArgb(255, 3, 0);

                ProgressText3.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar3.BackColor = Color.FromArgb(255, 3, 0);

                ProgressText4.ForeColor = Color.FromArgb(200, 200, 200);
                ProgressBar4.BackColor = Color.FromArgb(230, 230, 230);
            }

            foreach (Control ct in InformacoesPrincipais.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in EndereçoLabel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in DadosBancarios.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in Outros.Controls)
            {
                ct.TabStop = false;
            }
        }

        // Proxima etapa 3
        private void ProximoN3()
        {
            InformacoesPrincipais.Visible = false;
            InformacoesPrincipais.Location = new Point(12, 7734);

            EndereçoLabel.Visible = false;
            EndereçoLabel.Location = new Point(12, 7734);

            DadosBancarios.Visible = false;
            DadosBancarios.Location = new Point(12, 7734);

            Outros.Visible = true;
            Outros.Location = new Point(12, 134);

            if (IsDarkModeEnabled)
            {
                ProgressText1.ForeColor = Color.FromArgb(255, 33, 0);
                ProgressBar1.BackColor = Color.FromArgb(255, 33, 0);

                ProgressText2.ForeColor = Color.FromArgb(255, 33, 0);
                ProgressBar2.BackColor = Color.FromArgb(255, 33, 0);

                ProgressText3.ForeColor = Color.FromArgb(255, 33, 0);
                ProgressBar3.BackColor = Color.FromArgb(255, 33, 0);

                ProgressText4.ForeColor = Color.FromArgb(255, 33, 0);
                ProgressBar4.BackColor = Color.FromArgb(255, 33, 0);
            }
            else
            {
                ProgressText1.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar1.BackColor = Color.FromArgb(255, 3, 0);

                ProgressText2.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar2.BackColor = Color.FromArgb(255, 3, 0);

                ProgressText3.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar3.BackColor = Color.FromArgb(255, 3, 0);

                ProgressText4.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar4.BackColor = Color.FromArgb(255, 3, 0);
            }

            foreach (Control ct in InformacoesPrincipais.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in EndereçoLabel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in DadosBancarios.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Outros.Controls)
            {
                ct.TabStop = true;
            }
        }

        // Imagem pra bytes
        private byte[] ImageToByte(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (System.IO.MemoryStream MemoryStream = new System.IO.MemoryStream())
            {
                image.Save(MemoryStream, format);
                return MemoryStream.ToArray();
            }
        }

        // Localizar cep
        private void LocalizarCEP()
        {
            if (!string.IsNullOrWhiteSpace(CEP.Text))
            {
                using (var SC = new SCCorreios.AtendeClienteClient())
                {
                    try
                    {
                        var CEPDados = SC.consultaCEP(CEP.Text.Trim());

                        Endereço.Text = CEPDados.end;
                        Bairro.Text = CEPDados.bairro;
                        Cidade.Text = CEPDados.cidade;
                        Estado.Text = CEPDados.uf;
                    }
                    catch (Exception)
                    {
                        Endereço.Text = "";
                        Bairro.Text = "";
                        Cidade.Text = "";
                        Estado.Text = "";
                    }
                }
            }
        }

        // Texto changed (outros)
        private void ChangedToTrue(object sender, EventArgs e)
        {
            if (FormLoaded)
                TextoChanged = true;
        }

        private void ChangedToTrueKeyPress(object sender, KeyPressEventArgs e)
        {
            if (FormLoaded)
                TextoChanged = true;
        }

        // Adicionar itens a lista pra poder usar o dark/light mode
        private void AddControlsToList()
        {
            GunaButtons = new List<Guna.UI2.WinForms.Guna2Button>();
            GunaBorderButtons = new List<Guna.UI2.WinForms.Guna2Button>();
            GunaTextBox = new List<Guna.UI2.WinForms.Guna2TextBox>();
            GunaComboBox = new List<Guna.UI2.WinForms.Guna2ComboBox>();
            GunaHints = new List<Guna.UI2.WinForms.Guna2HtmlLabel>();

            NormalLabels = new List<Label>();

            // Labels
            Label[] Labels = new Label[23]
            {
                label1, label2, label3, label4, label5, label6, label7, label8, label9, label16, label18, label19, label20, label22,
                label24, label25, label27, label28, label29, label30, label31, label32, label33
            };

            // Botoes normais
            Guna.UI2.WinForms.Guna2Button[] RedButtons = new Guna.UI2.WinForms.Guna2Button[4]
            {
                Proximo1, Proximo2, Proximo3, Concluir
            };

            // Botoes bordas
            Guna.UI2.WinForms.Guna2Button[] BorderButtons = new Guna.UI2.WinForms.Guna2Button[7]
            {
                Cancelar1, Cancelar2, Cancelar3, Cancelar4, Anterior1, Anterior2, Anterior3
            };

            // Textbox
            Guna.UI2.WinForms.Guna2TextBox[] TextBox = new Guna.UI2.WinForms.Guna2TextBox[15]
            {
                Nome, Telefone, CPF, Funcao, CTPS, CNH, Endereço, Bairro, Cidade, 
                CEP, Complemento, Banco, Agencia, Conta, Observaçoes
            };

            // Combobox
            Guna.UI2.WinForms.Guna2ComboBox[] Combobox = new Guna.UI2.WinForms.Guna2ComboBox[6]
            {
                Genero, TipoNumero, EstadoCivil, Estado, Tecnico, Vendedor
            };

            // Hints
            Guna.UI2.WinForms.Guna2HtmlLabel[] Hints = new Guna.UI2.WinForms.Guna2HtmlLabel[11]
            {
                NomeHint, TelefoneHint, TipoNumeroHint, CpfHint, FuncaoHint, EnderecoHint, CidadeHint,
                CepHint, EstadoHint, TecnicoHint, VendedorHint
            };

            GunaButtons.AddRange(RedButtons);
            GunaBorderButtons.AddRange(BorderButtons);
            GunaTextBox.AddRange(TextBox);
            GunaComboBox.AddRange(Combobox);
            NormalLabels.AddRange(Labels);
            GunaHints.AddRange(Hints);
        }

        // Ativar/desativar o dark mode
        private void SetColor()
        {
            this.BackColor = ThemeManager.FormBackColor;

            FrameName.ForeColor = ThemeManager.WhiteFontColor;

            Separator.FillColor = ThemeManager.SeparatorAndBorderColor;

            Minimize.IconColor = ThemeManager.CloseMinimizeIconColor;
            Minimize.HoverState.IconColor = ThemeManager.CloseMinimizeHoverIconColor;

            CloseBtn.IconColor = ThemeManager.CloseMinimizeIconColor;
            CloseBtn.HoverState.IconColor = ThemeManager.CloseMinimizeHoverIconColor;

            ToolTip.ForeColor = ThemeManager.GunaToolTipForeColor;
            ToolTip.BorderColor = ThemeManager.GunaToolTipBorderColor;
            ToolTip.BackColor = ThemeManager.GunaToolTipBackColor;

            if (InformacoesPrincipais.Location == new Point(12, 134))
            {
                ProgressText1.ForeColor = ThemeManager.FullRedButtonColor;
                ProgressBar1.BackColor = ThemeManager.FullRedButtonColor;
            }

            if (EndereçoLabel.Location == new Point(12, 134))
            {
                ProgressText2.ForeColor = ThemeManager.FullRedButtonColor;
                ProgressBar2.BackColor = ThemeManager.FullRedButtonColor;
            }

            if (DadosBancarios.Location == new Point(12, 134))
            {
                ProgressText3.ForeColor = ThemeManager.FullRedButtonColor;
                ProgressBar3.BackColor = ThemeManager.FullRedButtonColor;
            }

            if (Outros.Location == new Point(12, 134))
            {
                ProgressText4.ForeColor = ThemeManager.FullRedButtonColor;
                ProgressBar4.BackColor = ThemeManager.FullRedButtonColor;
            }

            ChoosePicture.BorderColor = ThemeManager.ChoosePictureBorderColor;

            // Labels normais
            foreach (Label ct in NormalLabels)
            {
                ct.ForeColor = ThemeManager.DarkGrayLabelsFontColor;
                ct.BackColor = ThemeManager.FormBackColor;
            }

            // Botoes normais
            foreach (Guna.UI2.WinForms.Guna2Button ct in GunaButtons)
            {
                ct.FillColor = ThemeManager.FullRedButtonColor;
                ct.BorderColor = ThemeManager.FullRedButtonColor;
                ct.HoverState.FillColor = ThemeManager.FullRedButtonHoverColor;
                ct.HoverState.BorderColor = ThemeManager.FullRedButtonHoverColor;
                ct.CheckedState.FillColor = ThemeManager.FullRedButtonCheckedColor;
                ct.CheckedState.BorderColor = ThemeManager.FullRedButtonCheckedColor;
            }

            // Botoes bordas
            foreach (Guna.UI2.WinForms.Guna2Button ct in GunaBorderButtons)
            {
                ct.FillColor = ThemeManager.BorderRedButtonFillColor;
                ct.ForeColor = ThemeManager.BorderRedButtonForeColor;
                ct.BorderColor = ThemeManager.BorderRedButtonBorderColor;
                ct.HoverState.BorderColor = ThemeManager.BorderRedButtonHoverBorderColor;
                ct.HoverState.FillColor = ThemeManager.BorderRedButtonHoverFillColor;
                ct.HoverState.ForeColor = ThemeManager.BorderRedButtonHoverForeColor;
                ct.PressedColor = ThemeManager.BorderRedButtonPressedColor;
            }

            // Combobox
            foreach (Guna.UI2.WinForms.Guna2ComboBox ct in GunaComboBox)
            {
                ct.FillColor = ThemeManager.ComboBoxFillColor;
                ct.ForeColor = ThemeManager.ComboBoxForeColor;
                ct.BorderColor = ThemeManager.ComboBoxBorderColor;
                ct.HoverState.BorderColor = ThemeManager.ComboBoxHoverBorderColor;
                ct.FocusedState.BorderColor = ThemeManager.ComboBoxFocusedBorderColor;
                ct.ItemsAppearance.ForeColor = ThemeManager.ComboBoxForeColor;
                ct.ItemsAppearance.SelectedBackColor = ThemeManager.ComboBoxSelectedItemColor;
            }

            // TextBox
            foreach (Guna.UI2.WinForms.Guna2TextBox ct in GunaTextBox)
            {
                ct.FillColor = ThemeManager.TextBoxFillColor;
                ct.ForeColor = ThemeManager.TextBoxForeColor;
                ct.BorderColor = ThemeManager.TextBoxBorderColor;
                ct.HoverState.BorderColor = ThemeManager.TextBoxHoverBorderColor;
                ct.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                ct.FocusedState.ForeColor = ThemeManager.TextBoxForeColor;
            }

            // Hints
            foreach (Guna.UI2.WinForms.Guna2HtmlLabel ct in GunaHints)
            {
                ct.ForeColor = ThemeManager.RedFontColor;
            }
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Outros */

        // Proximo
        private void Proximo1_Click(object sender, EventArgs e)
        {
            if (Nome.Text != "" && Telefone.Text != "" && TipoNumero.SelectedIndex != -1 && CPF.Text != "" && CPF.Text.Length == 14 && Funcao.Text != "")
            {
                Alerta AlertForm = new Alerta();

                string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";

                OleDbConnection con = new OleDbConnection(strcon);

                con.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT COUNT (*) FROM Funcionarios WHERE (NOME = @NOME)", con);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                cmd.Parameters.Add("@NOME", OleDbType.VarChar).Value = Nome.Text;

                int ExistItem = (int)cmd.ExecuteScalar();

                if (ExistItem > 0)
                {
                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "NovoFuncionario")
                            frm.Opacity = .0d;
                    }

                    Alerta.AlertaFrame.LblText.Text = "Já existe um funcionário com este nome cadastrado no sistema!";

                    AlertForm.Show();
                }
                else
                    ProximoN1();
            }

            // Modo escuro ativado
            if (IsDarkModeEnabled)
            {
                // Nome
                if (Nome.Text == "")
                {
                    Nome.BorderColor = Color.FromArgb(255, 33, 0);
                    NomeHint.Visible = true;
                    Nome.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Nome.BorderColor = Color.FromArgb(80, 80, 80);
                    NomeHint.Visible = false;
                    Nome.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }

                // Telefone
                if (Telefone.Text == "")
                {
                    Telefone.BorderColor = Color.FromArgb(255, 33, 0);
                    TelefoneHint.Visible = true;
                    Telefone.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Telefone.BorderColor = Color.FromArgb(80, 80, 80);
                    TelefoneHint.Visible = false;
                    Telefone.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }

                // Tipo de produto
                if (TipoNumero.SelectedIndex == -1)
                {
                    TipoNumero.BorderColor = Color.FromArgb(255, 33, 0);
                    TipoNumeroHint.Visible = true;
                    TipoNumero.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    TipoNumero.BorderColor = Color.FromArgb(80, 80, 80);
                    TipoNumeroHint.Visible = false;
                    TipoNumero.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }

                // CPF
                if (CPF.Text == "")
                {
                    CPF.BorderColor = Color.FromArgb(255, 33, 0);
                    CpfHint.Visible = true;
                    CPF.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    CPF.BorderColor = Color.FromArgb(80, 80, 80);
                    CpfHint.Visible = false;
                    CPF.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }

                // Funcao
                if (Funcao.Text == "")
                {
                    Funcao.BorderColor = Color.FromArgb(255, 33, 0);
                    FuncaoHint.Visible = true;
                    Funcao.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Funcao.BorderColor = Color.FromArgb(80, 80, 80);
                    FuncaoHint.Visible = false;
                    Funcao.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
            }

            // Modo escuro desativado
            else
            {
                // Nome
                if (Nome.Text == "")
                {
                    Nome.BorderColor = Color.FromArgb(255, 3, 0);
                    NomeHint.Visible = true;
                    Nome.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Nome.BorderColor = Color.FromArgb(210, 210, 210);
                    NomeHint.Visible = false;
                    Nome.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                // Telefone
                if (Telefone.Text == "")
                {
                    Telefone.BorderColor = Color.FromArgb(255, 3, 0);
                    TelefoneHint.Visible = true;
                    Telefone.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Telefone.BorderColor = Color.FromArgb(210, 210, 210);
                    TelefoneHint.Visible = false;
                    Telefone.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                // Tipo de produto
                if (TipoNumero.SelectedIndex == -1)
                {
                    TipoNumero.BorderColor = Color.FromArgb(255, 3, 0);
                    TipoNumeroHint.Visible = true;
                    TipoNumero.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    TipoNumero.BorderColor = Color.FromArgb(210, 210, 210);
                    TipoNumeroHint.Visible = false;
                    TipoNumero.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                // CPF
                if (CPF.Text == "")
                {
                    CPF.BorderColor = Color.FromArgb(255, 3, 0);
                    CpfHint.Visible = true;
                    CPF.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    CPF.BorderColor = Color.FromArgb(210, 210, 210);
                    CpfHint.Visible = false;
                    CPF.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                // Funcao
                if (Funcao.Text == "")
                {
                    Funcao.BorderColor = Color.FromArgb(255, 3, 0);
                    FuncaoHint.Visible = true;
                    Funcao.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Funcao.BorderColor = Color.FromArgb(210, 210, 210);
                    FuncaoHint.Visible = false;
                    Funcao.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        private void Proximo2_Click(object sender, EventArgs e)
        {
            if (Endereço.Text != "" && Cidade.Text != "" && Estado.SelectedIndex != -1)
            {
                // CEP
                if (CEP.Text != "" && CEP.Text.Length < 9)
                {
                    if (IsDarkModeEnabled)
                    {
                        CEP.BorderColor = Color.FromArgb(255, 33, 0);
                        CepHint.Visible = true;
                        CEP.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }

                    else
                    {
                        CEP.BorderColor = Color.FromArgb(255, 3, 0);
                        CepHint.Visible = true;
                        CEP.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                }

                else if (CEP.Text != "" && CEP.Text.Length == 9)
                {
                    if (IsDarkModeEnabled)
                    {
                        CEP.BorderColor = Color.FromArgb(80, 80, 80);
                        CepHint.Visible = false;
                        CEP.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                    }

                    else
                    {
                        CEP.BorderColor = Color.FromArgb(210, 210, 210);
                        CepHint.Visible = false;
                        CEP.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                    }

                    ProximoN2();
                }

                else if (CEP.Text == "")
                {
                    if (IsDarkModeEnabled)
                    {
                        CEP.BorderColor = Color.FromArgb(80, 80, 80);
                        CepHint.Visible = false;
                        CEP.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                    }

                    else
                    {
                        CEP.BorderColor = Color.FromArgb(210, 210, 210);
                        CepHint.Visible = false;
                        CEP.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                    }

                    ProximoN2();
                }
            }

            // Modo escuro ativado
            if (IsDarkModeEnabled)
            {
                // Endereço
                if (Endereço.Text == "")
                {
                    Endereço.BorderColor = Color.FromArgb(255, 33, 0);
                    EnderecoHint.Visible = true;
                    Endereço.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Endereço.BorderColor = Color.FromArgb(80, 80, 80);
                    EnderecoHint.Visible = false;
                    Endereço.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }


                // Cidade
                if (Cidade.Text == "")
                {
                    Cidade.BorderColor = Color.FromArgb(255, 33, 0);
                    CidadeHint.Visible = true;
                    Cidade.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Cidade.BorderColor = Color.FromArgb(80, 80, 80);
                    CidadeHint.Visible = false;
                    Cidade.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }


                // Estado
                if (Estado.SelectedIndex == -1)
                {
                    Estado.BorderColor = Color.FromArgb(255, 33, 0);
                    EstadoHint.Visible = true;
                    Estado.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Estado.BorderColor = Color.FromArgb(80, 80, 80);
                    EstadoHint.Visible = false;
                    Estado.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
            }

            // Modo escuro desativado
            else
            {
                // Endereço
                if (Endereço.Text == "")
                {
                    Endereço.BorderColor = Color.FromArgb(255, 3, 0);
                    EnderecoHint.Visible = true;
                    Endereço.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Endereço.BorderColor = Color.FromArgb(210, 210, 210);
                    EnderecoHint.Visible = false;
                    Endereço.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }


                // Cidade
                if (Cidade.Text == "")
                {
                    Cidade.BorderColor = Color.FromArgb(255, 3, 0);
                    CidadeHint.Visible = true;
                    Cidade.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Cidade.BorderColor = Color.FromArgb(210, 210, 210);
                    CidadeHint.Visible = false;
                    Cidade.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }


                // Estado
                if (Estado.SelectedIndex == -1)
                {
                    Estado.BorderColor = Color.FromArgb(255, 3, 0);
                    EstadoHint.Visible = true;
                    Estado.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Estado.BorderColor = Color.FromArgb(210, 210, 210);
                    EstadoHint.Visible = false;
                    Estado.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        private void Proximo3_Click(object sender, EventArgs e)
        {
            ProximoN3();
        }

        // Concluir
        private void Concluir_Click(object sender, EventArgs e)
        {
            AddSuccess SuccessForm = new AddSuccess();
            Erro ErrorForm = new Erro();

            if (CTPS.Text == "")
                CTPS.Text = "-";
            if (CNH.Text == "")
                CNH.Text = "-";
            if (Bairro.Text == "")
                Bairro.Text = "-";
            if (CEP.Text == "")
                CEP.Text = "-";
            if (Complemento.Text == "")
                Complemento.Text = "-";
            if (Banco.Text == "")
                Banco.Text = "-";
            if (Agencia.Text == "")
                Agencia.Text = "-";
            if (Conta.Text == "")
                Conta.Text = "-";
            if (Observaçoes.Text == "")
            {
                Observaçoes.ForeColor = Color.White;
                Observaçoes.Text = "-";
            }

            string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";

            string comando =

                // Campos
                "INSERT INTO Funcionarios (NOME, TELEFONE, CPFCNPJ, GENERO, CI, CARTEIRADETRABALHO, CNH, \n" +
                "NUMEROCAT, ESTADOCIVIL, FUNCAO, ENDERECO, BAIRRO, CIDADE, CEP, ESTADO, COMPLEMENTO, BANCO, AGENCIA, CONTADOBANCO," +
                "FUNCIONARIODESDE, TECNICO, VENDEDOR, DEMITIDODESLIGADO, OBSERVACOES, FOTO) \n" +


                // Valores
                "values (@NOME, @TELEFONE, @CPFCNPJ, @GENERO, @CI, @CARTEIRADETRABALHO, @CNH, \n" +
                "@NUMEROCAT, @ESTADOCIVIL, @FUNCAO, @ENDERECO, @BAIRRO, @CIDADE, @CEP, @ESTADO, @COMPLEMENTO, @BANCO, @AGENCIA, @CONTADOBANCO," +
                "@FUNCIONARIODESDE, @TECNICO, @VENDEDOR, @DEMITIDODESLIGADO, @OBSERVACOES, @FOTO)";

            OleDbConnection con = new OleDbConnection(strcon);
            OleDbCommand com = new OleDbCommand(comando, con);

            com.Parameters.Add("@NOME", OleDbType.VarChar).Value = Nome.Text;
            com.Parameters.Add("@TELEFONE", OleDbType.VarChar).Value = Telefone.Text;
            com.Parameters.Add("@CPFCNPJ", OleDbType.VarChar).Value = CPF.Text;

            if (Genero.SelectedIndex == -1)
                com.Parameters.Add("@GENERO", OleDbType.VarChar).Value = "-";
            else
                com.Parameters.Add("@GENERO", OleDbType.VarChar).Value = Genero.Text;

            com.Parameters.Add("@CI", OleDbType.VarChar).Value = "-";
            com.Parameters.Add("@CARTEIRADETRABALHO", OleDbType.VarChar).Value = CTPS.Text;
            com.Parameters.Add("@CNH", OleDbType.VarChar).Value = CNH.Text;
            com.Parameters.Add("@NUMEROCAT", OleDbType.VarChar).Value = "-";

            if (EstadoCivil.SelectedIndex == -1)
                com.Parameters.Add("@ESTADOCIVIL", OleDbType.VarChar).Value = "-";
            else
                com.Parameters.Add("@ESTADOCIVIL", OleDbType.VarChar).Value = EstadoCivil.Text;

            com.Parameters.Add("@FUNCAO", OleDbType.VarChar).Value = Funcao.Text;
            com.Parameters.Add("@ENDERECO", OleDbType.VarChar).Value = Endereço.Text;
            com.Parameters.Add("@BAIRRO", OleDbType.VarChar).Value = Bairro.Text;
            com.Parameters.Add("@CIDADE", OleDbType.VarChar).Value = Cidade.Text;
            com.Parameters.Add("@CEP", OleDbType.VarChar).Value = CEP.Text;
            com.Parameters.Add("@ESTADO", OleDbType.VarChar).Value = Estado.Text;
            com.Parameters.Add("@COMPLEMENTO", OleDbType.VarChar).Value = Complemento.Text;
            com.Parameters.Add("@BANCO", OleDbType.VarChar).Value = Banco.Text;
            com.Parameters.Add("@AGENCIA", OleDbType.VarChar).Value = Agencia.Text;
            com.Parameters.Add("@CONTADOBANCO", OleDbType.VarChar).Value = Conta.Text;
            com.Parameters.Add("@FUNCIONARIODESDE", OleDbType.Date).Value = DataEntrada.Value.ToShortDateString();

            bool IsT = false;
            bool IsT2 = false;

            if (Tecnico.SelectedIndex == 0)
                IsT = false;
            else if (Tecnico.SelectedIndex == 1)
                IsT = true;

            if (Vendedor.SelectedIndex == 0)
                IsT2 = false;
            else if (Vendedor.SelectedIndex == 1)
                IsT2 = true;

            com.Parameters.Add("@TECNICO", OleDbType.Boolean).Value = IsT;
            com.Parameters.Add("@VENDEDOR", OleDbType.Boolean).Value = IsT2;
            com.Parameters.Add("@DEMITIDODESLIGADO", OleDbType.Boolean).Value = false;
            com.Parameters.Add("@OBSERVACOES", OleDbType.VarChar).Value = Observaçoes.Text;

            if (FuncionarioPicture.Image != null)
            {
                Bitmap BitmapImg = new Bitmap(FuncionarioPicture.Image);
                byte[] picture = ImageToByte(BitmapImg, System.Drawing.Imaging.ImageFormat.Jpeg);

                com.Parameters.Add("@FOTO", OleDbType.Binary).Value = picture;
            }

            try
            {
                con.Open();
                com.ExecuteNonQuery();

                foreach (Form frm in fc)
                {
                    if (frm.Name == "NovoFuncionario")
                        frm.Opacity = .0d;
                }   

                AddSuccess.SuccessAdd.LblText.Text = "Funcionário cadastrado com sucesso!";

                SuccessForm.Show();

                Properties.Settings.Default.CanUpdateGrid = true;
            }
            catch (Exception exc)
            {
                foreach(Form frm in fc)
                {
                    if (frm.Name == "NovoFuncionario")
                        frm.Opacity = .0d;
                }

                Erro.ErrorFrame.LblText.Text = "Erro ao cadastrar funcionário!";

                ErrorForm.Show();

                MessageBox.Show(exc.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        // Anterior
        private void Anterior1_Click(object sender, EventArgs e)
        {
            InformacoesPrincipais.Visible = true;
            InformacoesPrincipais.Location = new Point(12, 134);

            EndereçoLabel.Visible = false;
            EndereçoLabel.Location = new Point(12, 7734);

            DadosBancarios.Visible = false;
            DadosBancarios.Location = new Point(12, 7734);

            Outros.Visible = false;
            Outros.Location = new Point(12, 7734);

            if (IsDarkModeEnabled)
            {
                ProgressText1.ForeColor = Color.FromArgb(255, 33, 0);
                ProgressBar1.BackColor = Color.FromArgb(255, 33, 0);

                ProgressText2.ForeColor = Color.FromArgb(200, 200, 200);
                ProgressBar2.BackColor = Color.FromArgb(230, 230, 230);

                ProgressText3.ForeColor = Color.FromArgb(200, 200, 200);
                ProgressBar3.BackColor = Color.FromArgb(230, 230, 230);

                ProgressText4.ForeColor = Color.FromArgb(200, 200, 200);
                ProgressBar4.BackColor = Color.FromArgb(230, 230, 230);
            }
            else
            {
                ProgressText1.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar1.BackColor = Color.FromArgb(255, 3, 0);

                ProgressText2.ForeColor = Color.FromArgb(200, 200, 200);
                ProgressBar2.BackColor = Color.FromArgb(230, 230, 230);

                ProgressText3.ForeColor = Color.FromArgb(200, 200, 200);
                ProgressBar3.BackColor = Color.FromArgb(230, 230, 230);

                ProgressText4.ForeColor = Color.FromArgb(200, 200, 200);
                ProgressBar4.BackColor = Color.FromArgb(230, 230, 230);
            }

            foreach (Control ct in InformacoesPrincipais.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in EndereçoLabel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in DadosBancarios.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Outros.Controls)
            {
                ct.TabStop = false;
            }
        }

        private void Anterior2_Click(object sender, EventArgs e)
        {
            InformacoesPrincipais.Visible = false;
            InformacoesPrincipais.Location = new Point(12, 7734);

            EndereçoLabel.Visible = true;
            EndereçoLabel.Location = new Point(12, 134);

            DadosBancarios.Visible = false;
            DadosBancarios.Location = new Point(12, 7734);

            Outros.Visible = false;
            Outros.Location = new Point(12, 7734);

            if (IsDarkModeEnabled)
            {
                ProgressText1.ForeColor = Color.FromArgb(255, 33, 0);
                ProgressBar1.BackColor = Color.FromArgb(255, 33, 0);

                ProgressText2.ForeColor = Color.FromArgb(255, 33, 0);
                ProgressBar2.BackColor = Color.FromArgb(255, 33, 0);

                ProgressText3.ForeColor = Color.FromArgb(200, 200, 200);
                ProgressBar3.BackColor = Color.FromArgb(230, 230, 230);

                ProgressText4.ForeColor = Color.FromArgb(200, 200, 200);
                ProgressBar4.BackColor = Color.FromArgb(230, 230, 230);
            }
            else
            {
                ProgressText1.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar1.BackColor = Color.FromArgb(255, 3, 0);

                ProgressText2.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar2.BackColor = Color.FromArgb(255, 3, 0);

                ProgressText3.ForeColor = Color.FromArgb(200, 200, 200);
                ProgressBar3.BackColor = Color.FromArgb(230, 230, 230);

                ProgressText4.ForeColor = Color.FromArgb(200, 200, 200);
                ProgressBar4.BackColor = Color.FromArgb(230, 230, 230);
            }

            foreach (Control ct in InformacoesPrincipais.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in EndereçoLabel.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in DadosBancarios.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Outros.Controls)
            {
                ct.TabStop = false;
            }
        }

        private void Anterior3_Click(object sender, EventArgs e)
        {
            InformacoesPrincipais.Visible = false;
            InformacoesPrincipais.Location = new Point(12, 7734);

            EndereçoLabel.Visible = false;
            EndereçoLabel.Location = new Point(12, 7734);

            DadosBancarios.Visible = true;
            DadosBancarios.Location = new Point(12, 134);

            Outros.Visible = false;
            Outros.Location = new Point(12, 7734);

            if (IsDarkModeEnabled)
            {
                ProgressText1.ForeColor = Color.FromArgb(255, 33, 0);
                ProgressBar1.BackColor = Color.FromArgb(255, 33, 0);

                ProgressText2.ForeColor = Color.FromArgb(255, 33, 0);
                ProgressBar2.BackColor = Color.FromArgb(255, 33, 0);

                ProgressText3.ForeColor = Color.FromArgb(255, 33, 0);
                ProgressBar3.BackColor = Color.FromArgb(255, 33, 0);

                ProgressText4.ForeColor = Color.FromArgb(200, 200, 200);
                ProgressBar4.BackColor = Color.FromArgb(230, 230, 230);
            }
            else
            {
                ProgressText1.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar1.BackColor = Color.FromArgb(255, 3, 0);

                ProgressText2.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar2.BackColor = Color.FromArgb(255, 3, 0);

                ProgressText3.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar3.BackColor = Color.FromArgb(255, 3, 0);

                ProgressText4.ForeColor = Color.FromArgb(200, 200, 200);
                ProgressBar4.BackColor = Color.FromArgb(230, 230, 230);
            }

            foreach (Control ct in InformacoesPrincipais.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in EndereçoLabel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in DadosBancarios.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in Outros.Controls)
            {
                ct.TabStop = false;
            }
        }

        // Cancelar
        private async void Cancelar1_Click(object sender, EventArgs e)
        {
            if (TextoChanged)
            {
                Cancel();
            }
            else
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

        private async void Cancelar2_Click(object sender, EventArgs e)
        {
            if (TextoChanged)
            {
                Cancel();
            }
            else
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

        private async void Cancelar3_Click(object sender, EventArgs e)
        {
            if (TextoChanged)
            {
                Cancel();
            }
            else
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

        private async void Cancelar4_Click(object sender, EventArgs e)
        {
            if (TextoChanged)
            {
                Cancel();
            }
            else
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

        // Fechar
        private async void Close_Click(object sender, EventArgs e)
        {
            if (TextoChanged)
            {
                Cancel();
            }
            else
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

        private void ProductPicture_Click(object sender, EventArgs e)
        {
            if (Genero.SelectedIndex != -1)
            {
                if (Genero.SelectedIndex == 0)
                {
                    if (Vendedor.SelectedIndex == 1)
                    {
                        string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsFuncionario\\Vendedor\\Feminino", "*.png");
                        List<string> Icones = Pasta.ToList();
                        Random RandomIcon = new Random();
                        FuncionarioPicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
                    }
                }
                else if (Genero.SelectedIndex == 1)
                {
                    if (Tecnico.SelectedIndex == 1 && Vendedor.SelectedIndex == 0)
                        FuncionarioPicture.Image = Properties.Resources._1;
                    else if (Tecnico.SelectedIndex == 1 && Vendedor.SelectedIndex == 1)
                        FuncionarioPicture.Image = Properties.Resources.TecnicoVendedor;
                    else if (Tecnico.SelectedIndex == 0 && Vendedor.SelectedIndex == 1)
                    {
                        string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsFuncionario\\Vendedor\\Masculino", "*.png");
                        List<string> Icones = Pasta.ToList();
                        Random RandomIcon = new Random();
                        FuncionarioPicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
                    }
                }
                else if (Genero.SelectedIndex == 2)
                    FuncionarioPicture.Image = Properties.Resources.DefaultAvatar;
            }
            else
                FuncionarioPicture.Image = Properties.Resources.DefaultAvatar;
        }

        private void ChoosePicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog PictureDialog = new OpenFileDialog();
            PictureDialog.Title = "Selecione uma foto";
            PictureDialog.Filter = "Arquivo JPG|*.jpg|Arquivo JPEG|*.jpeg|Arquivo PNG|*.png";

            if (PictureDialog.ShowDialog() == DialogResult.OK)
            {
                FuncionarioPicture.Image = new Bitmap(PictureDialog.FileName);
            }
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Text changed */

        private void Nome_TextChanged(object sender, EventArgs e)
        {
            NomeHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                Nome.BorderColor = Color.FromArgb(80, 80, 80);
                Nome.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                Nome.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }
            else
            {
                Nome.BorderColor = Color.FromArgb(210, 210, 210);
                Nome.FocusedState.BorderColor = Color.Black;
                Nome.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }
        }

        private void Telefone_TextChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                TelefoneHint.Visible = false;
                TipoNumeroHint.Visible = false;

                if (TipoNumero.SelectedIndex != -1)
                {
                    if (IsDarkModeEnabled)
                    {
                        Telefone.BorderColor = Color.FromArgb(80, 80, 80);
                        Telefone.FocusedState.BorderColor = Color.FromArgb(180, 180, 180);
                        Telefone.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                    }
                    else
                    {
                        Telefone.BorderColor = Color.FromArgb(210, 210, 210);
                        Telefone.FocusedState.BorderColor = Color.Black;
                        Telefone.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                    }

                    if (BackspacePressed != true)
                    {
                        if (TipoNumero.SelectedIndex == 0)
                        {
                            if (Telefone.TextLength == 3)
                            {
                                Telefone.Text = Telefone.Text.Insert(0, "(");
                                Telefone.Text = Telefone.Text.Insert(3, ")");
                                Telefone.Text = Telefone.Text.Insert(4, " ");
                                Telefone.SelectionStart = Telefone.Text.Length + 1;
                            }
                            else if (Telefone.TextLength == 10)
                            {
                                Telefone.Text = Telefone.Text.Insert(10, "-");
                                Telefone.SelectionStart = Telefone.Text.Length + 1;
                            }
                        }

                        else if (TipoNumero.SelectedIndex == 1)
                        {
                            if (Telefone.TextLength == 4)
                            {
                                Telefone.Text = Telefone.Text.Insert(4, "-");
                                Telefone.SelectionStart = Telefone.Text.Length + 1;
                            }
                        }
                    }
                }
                else
                {
                    TelefoneHint.Text = "Selecione um tipo de número para contato";
                    Telefone.Text = "";
                    TelefoneHint.Visible = true;

                    TipoNumeroHint.Visible = true;

                    if (IsDarkModeEnabled)
                    {
                        Telefone.BorderColor = Color.FromArgb(255, 33, 0);
                        Telefone.FocusedState.BorderColor = Color.FromArgb(255, 33, 0);
                        Telefone.HoverState.BorderColor = Color.FromArgb(255, 33, 0);

                        TipoNumero.BorderColor = Color.FromArgb(255, 33, 0);
                    }

                    else
                    {
                        Telefone.BorderColor = Color.FromArgb(255, 3, 0);
                        Telefone.FocusedState.BorderColor = Color.FromArgb(255, 3, 0);
                        Telefone.HoverState.BorderColor = Color.FromArgb(255, 3, 0);

                        TipoNumero.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                }

                TextoChanged = true;
            }
        }

        private void TipoNumero_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoNumeroHint.Visible = false;
            TelefoneHint.Visible = false;

            if (IsDarkModeEnabled)
                TipoNumero.BorderColor = Color.FromArgb(80, 80, 80);
            else
                TipoNumero.BorderColor = Color.FromArgb(210, 210, 210);

            if (Telefone.Focused == true)
            {
                if (IsDarkModeEnabled)
                    Telefone.BorderColor = Color.FromArgb(80, 80, 80);
                else
                    Telefone.BorderColor = Color.Black;
            }
            else
            {
                if (IsDarkModeEnabled)
                    Telefone.BorderColor = Color.FromArgb(80, 80, 80);
                else
                    Telefone.BorderColor = Color.FromArgb(180, 180, 180);
            }

            if (IsDarkModeEnabled)
            {
                Telefone.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                Telefone.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }
            else
            {
                Telefone.FocusedState.BorderColor = Color.Black;
                Telefone.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }

            if (TipoNumero.SelectedIndex == 0)
            {
                Telefone.MaxLength = 15;

                if (Telefone.Text != "")
                {
                    if (Telefone.Text.Length < 15)
                    {
                        Telefone.BorderColor = Color.FromArgb(255, 33, 0);
                        TelefoneHint.Text = "Insira um número de celular válido";
                        TelefoneHint.Visible = true;
                        Telefone.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }
                    else
                    {
                        Telefone.BorderColor = Color.FromArgb(80, 80, 80);
                        TelefoneHint.Text = "Insira um número de celular";
                        TelefoneHint.Visible = false;
                        Telefone.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                    }
                }
            }

            else if (TipoNumero.SelectedIndex == 1)
            {
                Telefone.MaxLength = 9;

                if (Telefone.Text != "")
                {
                    if (Telefone.Text.Length < 9)
                    {
                        if (IsDarkModeEnabled)
                        {
                            TelefoneHint.Visible = true;
                            TelefoneHint.Text = "Insira um número de telefone válido";
                            Telefone.BorderColor = Color.FromArgb(255, 33, 0);
                        }
                        else
                        {
                            TelefoneHint.Visible = true;
                            TelefoneHint.Text = "Insira um número de telefone";
                            Telefone.BorderColor = Color.FromArgb(255, 3, 0);
                        }
                    }

                    else if (Telefone.Text.Length > 9)
                    {
                        if (IsDarkModeEnabled)
                        {
                            TelefoneHint.Visible = true;
                            TelefoneHint.Text = "Insira um número de telefone válido";
                            Telefone.BorderColor = Color.FromArgb(255, 33, 0);
                        }
                        else
                        {
                            TelefoneHint.Visible = true;
                            TelefoneHint.Text = "Insira um número de telefone válido";
                            CPF.BorderColor = Color.FromArgb(255, 3, 0);
                        }
                    }
                    else
                        TelefoneHint.Visible = false;
                }
            }

            if (IsDarkModeEnabled)
            {
                TipoNumero.FocusedState.BorderColor = ThemeManager.ComboBoxFocusedBorderColor;
                TipoNumero.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }
            else
            {
                TipoNumero.FocusedState.BorderColor = Color.Black;
                TipoNumero.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }
        }

        private void Cpf_TextChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                CpfHint.Visible = false;

                if (IsDarkModeEnabled)
                {
                    CPF.BorderColor = Color.FromArgb(80, 80, 80);
                    CPF.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                    CPF.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
                else
                {
                    CPF.BorderColor = Color.FromArgb(210, 210, 210);
                    CPF.FocusedState.BorderColor = Color.Black;
                    CPF.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                if (BackspacePressed != true)
                {
                    if (CPF.TextLength == 3)
                    {
                        CPF.Text = CPF.Text.Insert(3, ".");
                        CPF.SelectionStart = CPF.Text.Length + 1;
                    }

                    else if (CPF.TextLength == 4)
                    {
                        if (CPF.Text.Contains("."))
                        {
                            CPF.SelectionStart = CPF.Text.Length + 1;
                        }
                        else
                        {
                            CPF.Text = CPF.Text.Insert(3, ".");
                            CPF.SelectionStart = CPF.Text.Length + 1;
                        }
                    }


                    else if (CPF.TextLength == 7)
                    {
                        CPF.Text = CPF.Text.Insert(7, ".");
                        CPF.SelectionStart = CPF.Text.Length + 1;
                    }

                    else if (CPF.TextLength == 8)
                    {
                        if (CPF.Text.Contains("."))
                        {
                            CPF.SelectionStart = CPF.Text.Length + 1;
                        }
                        else
                        {
                            CPF.Text = CPF.Text.Insert(7, ".");
                            CPF.SelectionStart = CPF.Text.Length + 1;
                        }
                    }


                    else if (CPF.TextLength == 11)
                    {
                        CPF.Text = CPF.Text.Insert(11, "-");
                        CPF.SelectionStart = CPF.Text.Length + 1;
                    }

                    else if (CPF.TextLength == 12)
                    {
                        if (CPF.Text.Contains("-"))
                        {
                            CPF.SelectionStart = CPF.Text.Length + 1;
                        }
                        else
                        {
                            CPF.Text = CPF.Text.Insert(11, "-");
                            CPF.SelectionStart = CPF.Text.Length + 1;
                        }
                    }
                }

                TextoChanged = true;
            }
        }

        private void Funcao_TextChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                FuncaoHint.Visible = false;

                if (IsDarkModeEnabled)
                {
                    Funcao.BorderColor = Color.FromArgb(80, 80, 80);
                    Funcao.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                    Funcao.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
                else
                {
                    Funcao.BorderColor = Color.FromArgb(210, 210, 210);
                    Funcao.FocusedState.BorderColor = Color.Black;
                    Funcao.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                TextoChanged = true;
            }
        }

        private void Genero_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Genero.SelectedIndex == 0)
            {
                string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsFeminino", "*.png");
                List<string> Icones = Pasta.ToList();
                Random RandomIcon = new Random();
                FuncionarioPicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
            }
            else if (Genero.SelectedIndex == 1)
            {
                string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsMasculino", "*.png");
                List<string> Icones = Pasta.ToList();
                Random RandomIcon = new Random();
                FuncionarioPicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
            }
            else if (Genero.SelectedIndex == 2)
                FuncionarioPicture.Image = Properties.Resources.DefaultAvatar;
        }

        private void Endereço_TextChanged(object sender, EventArgs e)
        {
            EnderecoHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                Endereço.BorderColor = Color.FromArgb(80, 80, 80);

                Endereço.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                Endereço.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }

            else
            {
                Endereço.BorderColor = Color.FromArgb(210, 210, 210);

                Endereço.FocusedState.BorderColor = Color.Black;
                Endereço.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }
        }

        private void Cidade_TextChanged(object sender, EventArgs e)
        {
            CidadeHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                Cidade.BorderColor = Color.FromArgb(80, 80, 80);

                Cidade.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                Cidade.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }

            else
            {
                Cidade.BorderColor = Color.FromArgb(210, 210, 210);

                Cidade.FocusedState.BorderColor = Color.Black;
                Cidade.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }
        }

        private void CEP_TextChanged(object sender, EventArgs e)
        {
            CepHint.Visible = false;

            if (BackspacePressed != true)
            {
                if (CEP.TextLength == 5)
                {
                    CEP.Text = CEP.Text.Insert(5, "-");
                    CEP.SelectionStart = CEP.Text.Length + 1;
                }
                else if (CEP.TextLength == 6)
                {
                    if (CEP.Text.Contains("-"))
                    {
                        CEP.SelectionStart = CEP.Text.Length + 1;
                    }
                    else
                    {
                        CEP.Text = CEP.Text.Insert(5, "-");
                        CEP.SelectionStart = CEP.Text.Length + 1;
                    }
                }
            }

            if (IsDarkModeEnabled)
            {
                CEP.BorderColor = Color.FromArgb(80, 80, 80);

                CEP.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                CEP.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }

            else
            {
                CEP.BorderColor = Color.FromArgb(210, 210, 210);

                CEP.FocusedState.BorderColor = Color.Black;
                CEP.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }
        }

        private void CEP_Leave(object sender, EventArgs e)
        {
            LocalizarCEP();
        }

        private void Estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            EstadoHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                Estado.BorderColor = Color.FromArgb(80, 80, 80);

                Estado.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                Estado.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }

            else
            {
                Estado.BorderColor = Color.FromArgb(210, 210, 210);

                Estado.FocusedState.BorderColor = Color.Black;
                Estado.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }
        }

        private void Tecnico_SelectedIndexChanged(object sender, EventArgs e)
        {
            TecnicoHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                Tecnico.BorderColor = Color.FromArgb(80, 80, 80);

                Tecnico.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                Tecnico.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }

            else
            {
                Tecnico.BorderColor = Color.FromArgb(210, 210, 210);

                Tecnico.FocusedState.BorderColor = Color.Black;
                Tecnico.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }

            if (Genero.SelectedIndex != -1)
            {
                if (Genero.SelectedIndex == 0)
                {
                    if (Vendedor.SelectedIndex == 1)
                    {
                        string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsFuncionario\\Vendedor\\Feminino", "*.png");
                        List<string> Icones = Pasta.ToList();
                        Random RandomIcon = new Random();
                        FuncionarioPicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
                    }
                }
                else if (Genero.SelectedIndex == 1)
                {
                    if (Tecnico.SelectedIndex == 1 && Vendedor.SelectedIndex == 0)
                        FuncionarioPicture.Image = Properties.Resources._1;
                    else if (Tecnico.SelectedIndex == 1 && Vendedor.SelectedIndex == 1)
                        FuncionarioPicture.Image = Properties.Resources.TecnicoVendedor;
                    else if (Tecnico.SelectedIndex == 0 && Vendedor.SelectedIndex == 1)
                    {
                        string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsFuncionario\\Vendedor\\Masculino", "*.png");
                        List<string> Icones = Pasta.ToList();
                        Random RandomIcon = new Random();
                        FuncionarioPicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
                    }
                }
                else if (Genero.SelectedIndex == 2)
                    FuncionarioPicture.Image = Properties.Resources.DefaultAvatar;
            }
            else
                FuncionarioPicture.Image = Properties.Resources.DefaultAvatar;
        }

        private void Vendedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            VendedorHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                Vendedor.BorderColor = Color.FromArgb(80, 80, 80);

                Vendedor.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                Vendedor.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }

            else
            {
                Vendedor.BorderColor = Color.FromArgb(210, 210, 210);

                Vendedor.FocusedState.BorderColor = Color.Black;
                Vendedor.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }

            if (Genero.SelectedIndex != -1)
            {
                if (Genero.SelectedIndex == 0)
                {
                    if (Vendedor.SelectedIndex == 1)
                    {
                        string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsFuncionario\\Vendedor\\Feminino", "*.png");
                        List<string> Icones = Pasta.ToList();
                        Random RandomIcon = new Random();
                        FuncionarioPicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
                    }
                }
                else if (Genero.SelectedIndex == 1)
                {
                    if (Tecnico.SelectedIndex == 1 && Vendedor.SelectedIndex == 0)
                        FuncionarioPicture.Image = Properties.Resources._1;
                    else if (Tecnico.SelectedIndex == 1 && Vendedor.SelectedIndex == 1)
                        FuncionarioPicture.Image = Properties.Resources.TecnicoVendedor;
                    else if (Tecnico.SelectedIndex == 0 && Vendedor.SelectedIndex == 1)
                    {
                        string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsFuncionario\\Vendedor\\Masculino", "*.png");
                        List<string> Icones = Pasta.ToList();
                        Random RandomIcon = new Random();
                        FuncionarioPicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
                    }
                }
                else if (Genero.SelectedIndex == 2)
                    FuncionarioPicture.Image = Properties.Resources.DefaultAvatar;
            }
            else
                FuncionarioPicture.Image = Properties.Resources.DefaultAvatar;
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Key press */

        private void Telefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
