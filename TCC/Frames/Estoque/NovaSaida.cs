﻿using System;
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

namespace TCC.Frames
{
    public partial class NovaSaida : Form
    {
        FormCollection fc = Application.OpenForms;

        public static NovaSaida SaidaFrame;
        public Guna.UI2.WinForms.Guna2TextBox CodigoText;
        public string NumberType;

        bool FormLoaded;
        bool CloseOpen;
        bool EncontrarProdutoOpen;
        bool TextoChanged;

        decimal CustoUnitario;

        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;

        string TipoDeNumero = "";

        List<Guna.UI2.WinForms.Guna2Button> GunaButtons;
        List<Guna.UI2.WinForms.Guna2Button> GunaBorderButtons;
        List<Guna.UI2.WinForms.Guna2TextBox> GunaTextBox;
        List<Guna.UI2.WinForms.Guna2ComboBox> GunaComboBox;
        List<Guna.UI2.WinForms.Guna2HtmlLabel> GunaHints;

        List<Label> NormalLabels;

        public NovaSaida()
        {
            InitializeComponent();

            AddControlsToList();
            SetColor();

            SaidaFrame = this;
            CodigoText = Codigo;
            NumberType = TipoDeNumero;

            if (AnimateFrames)
                guna2BorderlessForm1.AnimateWindow = true;
            else
                guna2BorderlessForm1.AnimateWindow = false;
        }

        private void NovoCliente_Load(object sender, EventArgs e)
        {
            foreach (Control ct in Outros.Controls)
            {
                ct.TabStop = false;
            }

            Observaçoes.ForeColor = Color.Black;

            Data.Value = Convert.ToDateTime(DateTime.Today);

            foreach (Form frm in fc)
            {
                if (frm.Name == "EncontrarProduto")
                {
                    SearchProduct.Visible = false;
                }
            }

            string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";
            OleDbConnection con = new OleDbConnection(strcon);

            con.Open();

            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT PRODUTO FROM Estoque";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            adapter.Fill(dt);

            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

            foreach (DataRow dtr in dt.Rows)
            {
                collection.Add(dtr["PRODUTO"].ToString());
            }

            Produto.AutoCompleteMode = AutoCompleteMode.Suggest;
            Produto.AutoCompleteSource = AutoCompleteSource.CustomSource;

            Produto.AutoCompleteCustomSource = collection;

            con.Close();

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
                        Frames.CloseConfirm.CloseFrame.TopText.Text = "Cancelar operação";
                        Frames.CloseConfirm.CloseFrame.LblText.Text = "Você deseja mesmo cancelar a operação?";
                    }
                }

                if (frm.Name == "NovaSaida")
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
        }

        private void UpdateCode()
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

            try
            {
                con.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT * FROM Estoque WHERE CODIGO = @CODIGO", con);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                cmd.Parameters.Add("@CODIGO", OleDbType.VarChar).Value = Codigo.Text;

                OleDbDataReader dtr = cmd.ExecuteReader();

                if (dtr.Read())
                {
                    if (Produto.Text == "")
                        Produto.Text = dtr["PRODUTO"].ToString();

                    CodigoInsert.Text = dtr["CODIGO"].ToString();
                    Disponivel.Text = dtr["QNTDDISPONIVEL"].ToString();
                    ValorUnitario.Text = dtr["VALORVENDACONSUMIDOR"].ToString();
                    NumeroFabricanteInsert.Text = dtr["NUMEROFABRICANTE"].ToString();
                    Ideal.Text = dtr["QNTDIDEAL"].ToString();
                    Minima.Text = dtr["QNTDMINIMA"].ToString();
                    Status.Text = dtr["STATUS"].ToString();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                con.Close();
            }

            TextoChanged = true;
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
            InformacoesPanel.Visible = false;
            InformacoesPanel.Location = new Point(12, 634);

            Outros.Visible = true;
            Outros.Location = new Point(12, 134);

            ProgressText1.ForeColor = Color.FromArgb(255, 3, 0);
            ProgressBar1.BackColor = Color.FromArgb(255, 3, 0);

            ProgressText2.ForeColor = Color.FromArgb(255, 3, 0);
            ProgressBar2.BackColor = Color.FromArgb(255, 3, 0);

            foreach (Control ct in InformacoesPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Outros.Controls)
            {
                ct.TabStop = false;
            }
        }

        private void CalculateValues()
        {
            string TextNumbers = Regex.Replace(ValorUnitario.Text, "[^0-9]", string.Empty);

            decimal ConvertToDecimal;

            bool ConvertBool = decimal.TryParse(TextNumbers, NumberStyles.Currency,
            CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal);

            CustoUnitario = ConvertToDecimal;

            if (Retirada.Text != "")
            {
                var ValorTT = ((decimal)Convert.ToDecimal(Retirada.Text) * CustoUnitario);

                ValorTotal.Text = Convert.ToString(ValorTT);

                ValorTotal.Text = string.Format("{0:#,##0.00}", Double.Parse(ValorTotal.Text) / 100);
                ValorTotal.Text = Convert.ToString("R$ " + ValorTotal.Text);
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
            Label[] Labels = new Label[14]
            {
                label1, label2, label3, label4, label5, label6, label7, label8, label9,
                label10, label11, label12, label16, label21
            };

            // Botoes normais
            Guna.UI2.WinForms.Guna2Button[] RedButtons = new Guna.UI2.WinForms.Guna2Button[2]
            {
                Proximo1, Concluir
            };

            // Botoes bordas
            Guna.UI2.WinForms.Guna2Button[] BorderButtons = new Guna.UI2.WinForms.Guna2Button[3]
            {
                Cancelar1, Cancelar2, Anterior1
            };

            // Textbox
            Guna.UI2.WinForms.Guna2TextBox[] TextBox = new Guna.UI2.WinForms.Guna2TextBox[11]
            {
                Produto, Codigo, Retirada, Disponivel, ValorUnitario, ValorTotal, OrdemDeServico, OrdemDeProducao, VendaNumero, NotaFiscal, Observaçoes
            };

            // Combobox
            Guna.UI2.WinForms.Guna2ComboBox[] Combobox = new Guna.UI2.WinForms.Guna2ComboBox[2]
            {
                TipoNumero, Responsável
            };

            // Hints
            Guna.UI2.WinForms.Guna2HtmlLabel[] Hints = new Guna.UI2.WinForms.Guna2HtmlLabel[3]
            {
                CodigoHint, TipoNumeroHint, RetiradaHint
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

            Data.BackColor = ThemeManager.FormBackColor;
            Data.FillColor = ThemeManager.FormBackColor;
            Data.ForeColor = ThemeManager.WhiteFontColor;
            Data.BorderColor = ThemeManager.SeparatorAndBorderColor;
            Data.HoverState.BorderColor = ThemeManager.DateTimePickerHoverBorderColor;
            Data.CheckedState.BorderColor = ThemeManager.SeparatorAndBorderColor;
            Data.CheckedState.FillColor = ThemeManager.FormBackColor;

            guna2VSeparator1.FillColor = ThemeManager.SeparatorAndBorderColor;

            ToolTip.ForeColor = ThemeManager.GunaToolTipForeColor;
            ToolTip.BorderColor = ThemeManager.GunaToolTipBorderColor;
            ToolTip.BackColor = ThemeManager.GunaToolTipBackColor;

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
        private void Proximo1_Click_1(object sender, EventArgs e)
        {
            Alerta AlertaForm = new Alerta();
            Alerta2 AlertForm = new Alerta2();

            if (Codigo.Text != "" && TipoNumero.SelectedIndex != -1)
            {
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

                try
                {
                    con.Open();

                    OleDbCommand cmd = new OleDbCommand("SELECT COUNT (*) FROM Estoque WHERE (CODIGO = @CODIGO)", con);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                    cmd.Parameters.Add("@CODIGO", OleDbType.VarChar).Value = Codigo.Text;

                    int ExistItem = (int)cmd.ExecuteScalar();

                    if (ExistItem > 0)
                    {
                        if (Retirada.Text != "")
                        {
                            if (Convert.ToInt32(Retirada.Text) < Convert.ToInt32(Disponivel.Text))
                                ProximoN1();
                            else
                            {
                                foreach (Form frm in fc)
                                {
                                    if (frm.Name == "NovaSaida")
                                        frm.Opacity = .0d;
                                }

                                Alerta.AlertaFrame.LblText.Text = "A quantidade retirada é maior que a quantidade disponível!";

                                AlertaForm.Show();
                            }
                        }
                    }
                    else
                    {
                        foreach (Form frm in fc)
                        {
                            if (frm.Name == "NovaSaida")
                                frm.Opacity = .0d;
                        }

                        Alerta2.AlertaFrame.LblText.Text = "Produto não encontrado no sistema. Deseja cadastrar um novo?";

                        AlertForm.Show();
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                }
                finally
                {
                    con.Close();
                }
            }

            // Modo escuro ativado
            if (IsDarkModeEnabled)
            {
                // Codigo
                if (Codigo.Text == "")
                {
                    Codigo.BorderColor = Color.FromArgb(255, 33, 0);
                    CodigoHint.Visible = true;
                    Codigo.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Codigo.BorderColor = Color.FromArgb(80, 80, 80);
                    CodigoHint.Visible = false;
                    Codigo.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }


                // Tipo de numero
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


                // Qntd adquirida
                if (Retirada.Text == "")
                {
                    Retirada.BorderColor = Color.FromArgb(255, 33, 0);
                    RetiradaHint.Visible = true;
                    Retirada.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Retirada.BorderColor = Color.FromArgb(80, 80, 80);
                    RetiradaHint.Visible = false;
                    Retirada.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
            }

            // Modo escuro desativado
            else
            {
                // Codigo
                if (Codigo.Text == "")
                {
                    Codigo.BorderColor = Color.FromArgb(255, 3, 0);
                    CodigoHint.Visible = true;
                    Codigo.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Codigo.BorderColor = Color.FromArgb(210, 210, 210);
                    CodigoHint.Visible = false;
                    Codigo.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }


                // Tipo de numero
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


                // Qntd adquirida
                if (Retirada.Text == "")
                {
                    Retirada.BorderColor = Color.FromArgb(255, 3, 0);
                    RetiradaHint.Visible = true;
                    Retirada.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Retirada.BorderColor = Color.FromArgb(210, 210, 210);
                    RetiradaHint.Visible = false;
                    Retirada.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        // Concluir
        private void Concluir_Click_1(object sender, EventArgs e)
        {
            AddSuccess SuccessForm = new AddSuccess();
            Erro ErrorForm = new Erro();

            if (OrdemDeServico.Text == "")
            {
                OrdemDeServico.Text = "-";
            }
            if (OrdemDeProducao.Text == "")
            {
                OrdemDeProducao.Text = "-";
            }
            if (VendaNumero.Text == "")
            {
                VendaNumero.Text = "-";
            }
            if (NotaFiscal.Text == "")
            {
                NotaFiscal.Text = "-";
            }
            if (Responsável.SelectedIndex == -1)
            {
                Responsável.Text = "-";
            }
            if (Observaçoes.Text == "")
            {
                Observaçoes.ForeColor = Color.White;
                Observaçoes.Text = "-";
            }

            Disponivel.Text = Convert.ToString(Convert.ToInt32(Disponivel.Text) - Convert.ToInt32(Retirada.Text));

            if (Convert.ToInt32(Disponivel.Text) >= Convert.ToInt32(Ideal.Text))
                Status.Text = "IDEAL";
            else if (Convert.ToInt32(Disponivel.Text) > Convert.ToInt32(Minima.Text) && Convert.ToInt32(Disponivel.Text) < Convert.ToInt32(Ideal.Text))
                Status.Text = "OK";
            else if (Convert.ToInt32(Disponivel.Text) < Convert.ToInt32(Minima.Text) && Convert.ToInt32(Disponivel.Text) < Convert.ToInt32(Ideal.Text) && Convert.ToInt32(Disponivel.Text) > 0)
                Status.Text = "OK";
            else if (Convert.ToInt32(Disponivel.Text) == Convert.ToInt32(Minima.Text))
                Status.Text = "NO MÍNIMO";
            else if (Convert.ToInt32(Disponivel.Text) <= Convert.ToInt32(Minima.Text) && Convert.ToInt32(Disponivel.Text) > 0)
                Status.Text = "NO MÍNIMO";
            else if (Convert.ToInt32(Disponivel.Text) == 0)
                Status.Text = "ZERADO";

            string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";

            string comando =

                // Campos
                "INSERT INTO SaidaDeItens (CODIGO, NUMEROFABRICANTE, PRODUTO, VALORUNITARIO, QNTDRETIRADA, VALORTOTAL, ORDEMDESERVICO, \n" +
                "ORDEMDEPRODUCAO, VENDANUMERO, NUMERONOTAFISCAL, FUNCIONARIORESPONSAVEL, OBSERVACOES, DATA) \n" +


                // Valores
                "values (@CODIGO, @NUMEROFABRICANTE, @PRODUTO, @VALORUNITARIO, @QNTDRETIRADA, @VALORTOTAL, @ORDEMDESERVICO, \n" +
                "@ORDEMDEPRODUCAO, @VENDANUMERO, @NUMERONOTAFISCAL, @FUNCIONARIORESPONSAVEL, @OBSERVACOES, @DATA)";

            // Inserir dados
            OleDbConnection con = new OleDbConnection(strcon);
            OleDbCommand com = new OleDbCommand(comando, con);

            // Atualizar valor da qntdd
            OleDbConnection con2 = new OleDbConnection(strcon);
            OleDbCommand cmd = con.CreateCommand();

            // Atualizar status
            OleDbConnection con3 = new OleDbConnection(strcon);
            OleDbCommand cmd2 = con.CreateCommand();

            com.Parameters.Add("@CODIGO", OleDbType.VarChar).Value = CodigoInsert.Text;
            com.Parameters.Add("@NUMEROFABRICANTE", OleDbType.VarChar).Value = NumeroFabricanteInsert.Text;
            com.Parameters.Add("@PRODUTO", OleDbType.VarChar).Value = Produto.Text;
            com.Parameters.Add("@VALORUNITARIO", OleDbType.VarChar).Value = ValorUnitario.Text;
            com.Parameters.Add("@QNTDRETIRADA", OleDbType.Numeric).Value = Convert.ToInt32(Retirada.Text);
            com.Parameters.Add("@VALORTOTAL", OleDbType.VarChar).Value = ValorTotal.Text;
            com.Parameters.Add("@ORDEMDESERVICO", OleDbType.VarChar).Value = OrdemDeServico.Text;
            com.Parameters.Add("@ORDEMDEPRODUCAO", OleDbType.VarChar).Value = OrdemDeProducao.Text;
            com.Parameters.Add("@VENDANUMERO", OleDbType.VarChar).Value = VendaNumero.Text;
            com.Parameters.Add("@NUMERONOTAFISCAL", OleDbType.VarChar).Value = NotaFiscal.Text;
            com.Parameters.Add("@FUNCIONARIORESPONSAVEL", OleDbType.VarChar).Value = Responsável.Text;
            com.Parameters.Add("@OBSERVACOES", OleDbType.VarChar).Value = Observaçoes.Text;
            com.Parameters.Add("@DATA", OleDbType.Date).Value = Data.Value;

            // Atualizar quantidades
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Estoque SET QNTDDISPONIVEL = QNTDDISPONIVEL - '" + Convert.ToInt32(Retirada.Text) + "' WHERE CODIGO = @CODIGO";

            cmd.Parameters.Add("@CODIGO", OleDbType.VarChar).Value = CodigoInsert.Text;

            // Atualizar status
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "UPDATE Estoque SET STATUS = '" + Status.Text + "' WHERE CODIGO = @CODIGO";

            cmd2.Parameters.Add("@CODIGO", OleDbType.VarChar).Value = CodigoInsert.Text;

            try
            {
                con.Open();
                com.ExecuteNonQuery();

                con2.Open();
                cmd.ExecuteNonQuery();

                con3.Open();
                cmd2.ExecuteNonQuery();

                foreach (Form frm in fc)
                {
                    if (frm.Name == "NovaSaida")
                        frm.Opacity = .0d;
                }

                AddSuccess.SuccessAdd.LblText.Text = "Dados adicionados ao sistema com sucesso!";

                SuccessForm.Show();

                Properties.Settings.Default.CanUpdateGrid = true;
            }
            catch (Exception exc)
            {
                foreach (Form frm in fc)
                {
                    if (frm.Name == "NovaSaida")
                        frm.Opacity = .0d;
                }

                ErrorForm.Text = "Erro";
                Erro.ErrorFrame.LblText.Text = "Erro ao adicionar dados ao sistema!";

                ErrorForm.Show();

                MessageBox.Show(exc.ToString());
            }
            finally
            {
                con.Close();
                con2.Close();
                con3.Close();
            }
        }

        // Anterior
        private void Anterior1_Click(object sender, EventArgs e)
        {
            InformacoesPanel.Visible = true;
            InformacoesPanel.Location = new Point(12, 134);

            Outros.Visible = false;
            Outros.Location = new Point(12, 634);

            if (IsDarkModeEnabled)
            {
                ProgressText1.ForeColor = Color.FromArgb(255, 33, 0);
                ProgressBar1.BackColor = Color.FromArgb(255, 33, 0);
            }

            else
            {
                ProgressText1.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar1.BackColor = Color.FromArgb(255, 3, 0);
            }

            ProgressText2.ForeColor = Color.FromArgb(200, 200, 200);
            ProgressBar2.BackColor = Color.FromArgb(230, 230, 230);

            ProgressText2.ForeColor = Color.FromArgb(200, 200, 200);
            ProgressBar2.BackColor = Color.FromArgb(230, 230, 230);

            foreach (Control ct in InformacoesPanel.Controls)
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

        // Pesquisar codigo
        private void SearchProduct_DoubleClick(object sender, EventArgs e)
        {
            Estoque.EncontrarProduto FindForm = new Estoque.EncontrarProduto();

            foreach (Form frm in fc)
            {
                if (frm.Name != "EncontrarProduto")
                    EncontrarProdutoOpen = false;
                else
                    EncontrarProdutoOpen = true;
            }

            if (EncontrarProdutoOpen != true)
            {
                EncontrarProdutoOpen = true;

                if (TipoNumero.SelectedIndex != -1)
                {
                    TipoNumeroHint.Visible = false;

                    Opacity = .0d;

                    FindForm.Show();
                }
                else
                {
                    TipoNumeroHint.Visible = true;

                    if (IsDarkModeEnabled)
                    {
                        TipoNumero.BorderColor = Color.FromArgb(255, 33, 0);
                        TipoNumeroHint.Visible = true;
                        TipoNumero.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }
                    else
                    {
                        TipoNumero.BorderColor = Color.FromArgb(255, 3, 0);
                        TipoNumeroHint.Visible = true;
                        TipoNumero.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                }
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

        /*--------------------------------------------------------------------------------------------*/

        /* Text changed */

        private void Produto_TextChanged(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

            try
            {
                con.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT * FROM Estoque WHERE PRODUTO = @PRODUTO", con);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                cmd.Parameters.Add("@PRODUTO", OleDbType.VarChar).Value = Produto.Text;

                OleDbDataReader dtr = cmd.ExecuteReader();

                if (dtr.Read())
                {
                    Produto.Text = dtr["PRODUTO"].ToString();

                    if (TipoNumero.SelectedIndex == 0)
                        Codigo.Text = dtr["NUMEROFABRICANTE"].ToString();
                    else if (TipoNumero.SelectedIndex == 1)
                        Codigo.Text = dtr["CODIGO"].ToString();
                    else if (TipoNumero.SelectedIndex == -1)
                    {
                        TipoNumero.SelectedIndex = 1;
                        Codigo.Text = dtr["CODIGO"].ToString();
                    }

                    CodigoInsert.Text = dtr["CODIGO"].ToString();
                    Disponivel.Text = dtr["QNTDDISPONIVEL"].ToString();
                    ValorUnitario.Text = dtr["VALORVENDACONSUMIDOR"].ToString();
                    NumeroFabricanteInsert.Text = dtr["NUMEROFABRICANTE"].ToString();
                    Ideal.Text = dtr["QNTDIDEAL"].ToString();
                    Minima.Text = dtr["QNTDMINIMA"].ToString();
                    Status.Text = dtr["STATUS"].ToString();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                con.Close();
            }

            TextoChanged = true;
        }

        private void Codigo_TextChanged(object sender, EventArgs e)
        {
            if (IsDarkModeEnabled)
            {
                Codigo.BorderColor = Color.FromArgb(80, 80, 80);

                Codigo.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                Codigo.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }

            else
            {
                Codigo.BorderColor = Color.FromArgb(210, 210, 210);

                Codigo.FocusedState.BorderColor = Color.Black;
                Codigo.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }

            if (TipoNumero.SelectedIndex != -1)
            {
                CodigoHint.Visible = false;
                TipoNumeroHint.Visible = false;

                if (IsDarkModeEnabled)
                {
                    Codigo.BorderColor = Color.FromArgb(80, 80, 80);
                    Codigo.FocusedState.BorderColor = Color.FromArgb(180, 180, 180);
                    Codigo.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
                else
                {
                    Codigo.BorderColor = Color.FromArgb(210, 210, 210);
                    Codigo.FocusedState.BorderColor = Color.Black;
                    Codigo.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
            else
            {
                Codigo.Text = "";
                CodigoHint.Visible = true;

                TipoNumeroHint.Visible = true;

                if (IsDarkModeEnabled)
                {
                    Codigo.BorderColor = Color.FromArgb(255, 33, 0);
                    Codigo.FocusedState.BorderColor = Color.FromArgb(255, 33, 0);
                    Codigo.HoverState.BorderColor = Color.FromArgb(255, 33, 0);

                    TipoNumero.BorderColor = Color.FromArgb(255, 33, 0);
                }

                else
                {
                    Codigo.BorderColor = Color.FromArgb(255, 3, 0);
                    Codigo.FocusedState.BorderColor = Color.FromArgb(255, 3, 0);
                    Codigo.HoverState.BorderColor = Color.FromArgb(255, 3, 0);

                    TipoNumero.BorderColor = Color.FromArgb(255, 3, 0);
                }
            }

            if (TipoDeNumero == "Barcode")
                CodigoInsert.Text = Codigo.Text;
            if (TipoDeNumero == "NumeroFabricacao")
                NumeroFabricanteInsert.Text = Codigo.Text;

            UpdateCode();

            TextoChanged = true;
        }

        private void Codigo_Leave(object sender, EventArgs e)
        {
            UpdateCode();
        }

        private void TipoNumero_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoNumeroHint.Visible = false;
            CodigoHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                TipoNumero.BorderColor = Color.FromArgb(80, 80, 80);

                TipoNumero.FocusedState.BorderColor = ThemeManager.ComboBoxFocusedBorderColor;
                TipoNumero.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }
            else
            {
                TipoNumero.BorderColor = Color.FromArgb(210, 210, 210);

                TipoNumero.FocusedState.BorderColor = Color.Black;
                TipoNumero.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }

            if (TipoNumero.SelectedIndex == 0)
            {
                TipoDeNumero = "NumeroFabricante";
                NumberType = "NumeroFabricante";
            }
            else if (TipoNumero.SelectedIndex == 1)
            {
                TipoDeNumero = "Codigo";
                NumberType = "Codigo";
            }
        }

        private void Retirada_TextChanged(object sender, EventArgs e)
        {
            RetiradaHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                Retirada.BorderColor = Color.FromArgb(80, 80, 80);

                Retirada.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                Retirada.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }
            else
            {
                Retirada.BorderColor = Color.FromArgb(210, 210, 210);

                Retirada.FocusedState.BorderColor = Color.Black;
                Retirada.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }

            if (Retirada.Text != "")
            {
                CalculateValues();
            }
        }

        private void Responsável_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextoChanged = true;
        }

        private void Codigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (TipoNumero.SelectedIndex == -1)
            {
                e.Handled = true;
            }
            else if (TipoDeNumero == "Codigo")
            {
                if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

            TextoChanged = true;
        }

        private void Retirada_Leave(object sender, EventArgs e)
        {
            if (Retirada.Text != "")
            {
                CalculateValues();
            }
        }

        private void ValorUnitario_TextChanged(object sender, EventArgs e)
        {
            CalculateValues();
        }
    }
}
