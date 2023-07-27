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
    public partial class NovaMaoDeObra : Form
    {
        FormCollection fc = Application.OpenForms;

        NovoServico NovoServicoFrame;

        bool FormLoaded;
        bool CloseOpen;
        bool BackspacePressed;
        bool TextoChanged;

        decimal ValorHoras;

        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;
        bool AutoCompleteValues = Properties.Settings.Default.AutoCompleteCurrencyValues;

        List<Guna.UI2.WinForms.Guna2Button> GunaButtons;
        List<Guna.UI2.WinForms.Guna2Button> GunaBorderButtons;
        List<Guna.UI2.WinForms.Guna2TextBox> GunaTextBox;
        List<Guna.UI2.WinForms.Guna2ComboBox> GunaComboBox;
        List<Guna.UI2.WinForms.Guna2HtmlLabel> GunaHints;

        List<Label> NormalLabels;

        string NumeroText;

        public NovaMaoDeObra(NovoServico NovoServicoForm, string NumeroServico)
        {
            InitializeComponent();

            AddControlsToList();
            SetColor();

            this.NovoServicoFrame = NovoServicoForm;
            NumeroText = NumeroServico;

            if (AnimateFrames)
                guna2BorderlessForm1.AnimateWindow = true;
            else
                guna2BorderlessForm1.AnimateWindow = false;
        }

        private void NovoProduto_Load(object sender, EventArgs e)
        {
            string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";
            OleDbConnection con = new OleDbConnection(strcon);

            con.Open();

            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT NOME FROM Funcionarios";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            adapter.Fill(dt);

            foreach (DataRow dtr in dt.Rows)
            {
                Tecnico.Items.Add(dtr["NOME"].ToString());
            }

            con.Close();

            DataInicio.Value = Convert.ToDateTime(DateTime.Now);
            DataFim.Value = Convert.ToDateTime(DateTime.Now.AddMinutes(1));

            Tecnico.Text = "Não definido";

            NumeroDoServico.Text = NumeroText;

            FormLoaded = true;
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Funcoes */

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

        private void CalculateValues()
        {
            if (ValorPorHoraOuServico.Text != "")
            {
                if (ValorPorHoraOuServico.Text.Contains("R") && ValorPorHoraOuServico.Text.Contains("$"))
                    ValorPorHoraOuServico.Text = ValorPorHoraOuServico.Text;
                else
                {
                    ValorPorHoraOuServico.Text = ValorPorHoraOuServico.Text.Insert(0, "R");
                    ValorPorHoraOuServico.Text = ValorPorHoraOuServico.Text.Insert(1, "$");
                    ValorPorHoraOuServico.Text = ValorPorHoraOuServico.Text.Insert(2, " ");
                }

                if (FormaDeCobranca.SelectedIndex == 0)
                {
                    if (QuantidadeHoras.Text != "")
                    {
                        string TextNumbers = Regex.Replace(QuantidadeHoras.Text, "[^0-9]", string.Empty);

                        decimal ConvertToDecimal;

                        bool ConvertBool = decimal.TryParse(TextNumbers, NumberStyles.Currency,
                        CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal);

                        if (QuantidadeHoras.Text.Contains(","))
                            ValorHoras = ConvertToDecimal / 10;
                        else
                            ValorHoras = ConvertToDecimal;

                        string TextNumbers2 = Regex.Replace(ValorPorHoraOuServico.Text, "[^0-9]", string.Empty);

                        var ValorTT = ((decimal)Convert.ToDecimal(TextNumbers2) * ValorHoras);

                        ValorTotal.Text = Convert.ToString(ValorTT);

                        ValorTotal.Text = string.Format("{0:#,##0.00}", Double.Parse(ValorTotal.Text) / 100);
                        ValorTotal.Text = Convert.ToString("R$ " + ValorTotal.Text);

                        DataFim.Value = Convert.ToDateTime(DateTime.Now.AddMinutes(Convert.ToInt32(QuantidadeHoras.Text) * 60));
                    }
                }
                else if (FormaDeCobranca.SelectedIndex == 1)
                {
                    string TextNumbers = Regex.Replace(ValorPorHoraOuServico.Text, "[^0-9]", string.Empty);

                    ValorTotal.Text = Convert.ToString(TextNumbers);

                    ValorTotal.Text = string.Format("{0:#,##0.00}", Double.Parse(ValorTotal.Text) / 100);
                    ValorTotal.Text = Convert.ToString("R$ " + ValorTotal.Text);
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
            Label[] Labels = new Label[8]
            {
                label1, label2, label3, label4, label5, label6, label32, label33
            };

            // Botoes normais
            Guna.UI2.WinForms.Guna2Button[] RedButtons = new Guna.UI2.WinForms.Guna2Button[1]
            {
                Inserir
            };

            // Botoes bordas
            Guna.UI2.WinForms.Guna2Button[] BorderButtons = new Guna.UI2.WinForms.Guna2Button[1]
            {
                Cancelar
            };

            // Textbox
            Guna.UI2.WinForms.Guna2TextBox[] TextBox = new Guna.UI2.WinForms.Guna2TextBox[4]
            {
                Servico, ValorPorHoraOuServico, QuantidadeHoras, ValorTotal
            };

            // Combobox
            Guna.UI2.WinForms.Guna2ComboBox[] Combobox = new Guna.UI2.WinForms.Guna2ComboBox[2]
            {
                FormaDeCobranca, Tecnico
            };

            // Hints
            Guna.UI2.WinForms.Guna2HtmlLabel[] Hints = new Guna.UI2.WinForms.Guna2HtmlLabel[4]
            {
                ServicoHint, TecnicoHint, ValorPorHoraHint, QuantidadeDeHorasHint
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

            DataInicio.BackColor = ThemeManager.FormBackColor;
            DataInicio.FillColor = ThemeManager.FormBackColor;
            DataInicio.ForeColor = ThemeManager.WhiteFontColor;
            DataInicio.BorderColor = ThemeManager.SeparatorAndBorderColor;
            DataInicio.HoverState.BorderColor = ThemeManager.DateTimePickerHoverBorderColor;
            DataInicio.CheckedState.BorderColor = ThemeManager.SeparatorAndBorderColor;
            DataInicio.CheckedState.FillColor = ThemeManager.FormBackColor;

            DataFim.BackColor = ThemeManager.FormBackColor;
            DataFim.FillColor = ThemeManager.FormBackColor;
            DataFim.ForeColor = ThemeManager.WhiteFontColor;
            DataFim.BorderColor = ThemeManager.SeparatorAndBorderColor;
            DataFim.HoverState.BorderColor = ThemeManager.DateTimePickerHoverBorderColor;
            DataFim.CheckedState.BorderColor = ThemeManager.SeparatorAndBorderColor;
            DataFim.CheckedState.FillColor = ThemeManager.FormBackColor;

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

        // Concluir
        private async void Inserir_Click(object sender, EventArgs e)
        {
            NovoServico ServicoForm = new NovoServico();

            if (Servico.Text != "" && Tecnico.SelectedIndex != -1 && ValorPorHoraOuServico.Text != "") 
            {
                if (FormaDeCobranca.SelectedIndex == 0 && QuantidadeHoras.Text != "" || FormaDeCobranca.SelectedIndex == 1)
                {
                    string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";

                    NovoServicoFrame.Illustration1.Visible = false;
                    NovoServicoFrame.Desc1.Visible = false;
                    NovoServicoFrame.MaoDeObraGrid.Rows.Add(Servico.Text, DataInicio.Value, DataFim.Value, ValorTotal.Text, Tecnico.Text);
                    NovoServicoFrame.MaoDeObraGrid.Visible = true;
                    NovoServicoFrame.guna2Separator2.Visible = true;

                    string comando2 =

                    // Campos
                    "INSERT INTO MaoDeObra (NUMEROSERVICO, SERVICOREALIZADO, INICIO, FIM, VALOR, TECNICO) \n" +


                    // Valores
                    "values (@NUMEROSERVICO, @SERVICOREALIZADO, @INICIO, @FIM, @VALOR, @TECNICO)";

                    OleDbConnection con = new OleDbConnection(strcon);
                    OleDbCommand com = new OleDbCommand(comando2, con);

                    com.Parameters.Add("@NUMEROSERVICO", OleDbType.VarChar).Value = NumeroDoServico.Text;

                    com.Parameters.Add("@SERVICOREALIZADO", OleDbType.VarChar).Value = Servico.Text;
                    com.Parameters.Add("@INICIO", OleDbType.Date).Value = DataInicio.Value;

                    if (FormaDeCobranca.SelectedIndex == 0)
                    {
                        com.Parameters.Add("@FIM", OleDbType.Date).Value = DataFim.Value;
                    }
                    else
                    {
                        com.Parameters.Add("@FIM", OleDbType.Date).Value = DataInicio.Value;
                    }

                    com.Parameters.Add("@VALOR", OleDbType.VarChar).Value = ValorTotal.Text;
                    com.Parameters.Add("@TECNICO", OleDbType.VarChar).Value = Tecnico.Text;

                    try
                    {
                        con.Open();
                        com.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        con.Close();
                    }

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
                            if (frm.Name == "NovoServico")
                            {
                                if (frm.Opacity < 1.0)
                                    frm.Opacity = 1.0;
                            }
                        }

                        Close();
                    }
                }
            }
            else
            {
                if (IsDarkModeEnabled)
                {
                    // Servico
                    if (Servico.Text == "")
                    {
                        Servico.BorderColor = Color.FromArgb(255, 33, 0);
                        ServicoHint.Visible = true;
                        Servico.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }
                    else
                    {
                        Servico.BorderColor = Color.FromArgb(80, 80, 80);
                        ServicoHint.Visible = false;
                        Servico.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                    }

                    // Tecnico
                    if (Tecnico.SelectedIndex == -1)
                    {
                        Tecnico.BorderColor = Color.FromArgb(255, 33, 0);
                        TecnicoHint.Visible = true;
                        Tecnico.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }
                    else
                    {
                        Tecnico.BorderColor = Color.FromArgb(80, 80, 80);
                        TecnicoHint.Visible = false;
                        Tecnico.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                    }

                    // Valor por hora
                    if (ValorPorHoraOuServico.Text == "")
                    {
                        ValorPorHoraOuServico.BorderColor = Color.FromArgb(255, 33, 0);
                        ValorPorHoraHint.Visible = true;
                        ValorPorHoraOuServico.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }
                    else
                    {
                        ValorPorHoraOuServico.BorderColor = Color.FromArgb(80, 80, 80);
                        ValorPorHoraHint.Visible = false;
                        ValorPorHoraOuServico.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                    }

                    if (FormaDeCobranca.SelectedIndex == 0)
                    {
                        // Valor por hora
                        if (QuantidadeHoras.Text == "")
                        {
                            QuantidadeHoras.BorderColor = Color.FromArgb(255, 33, 0);
                            QuantidadeDeHorasHint.Visible = true;
                            QuantidadeHoras.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                        }
                        else
                        {
                            QuantidadeHoras.BorderColor = Color.FromArgb(80, 80, 80);
                            QuantidadeDeHorasHint.Visible = false;
                            QuantidadeHoras.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                        }
                    }
                }

                else
                {
                    // Servico
                    if (Servico.Text == "")
                    {
                        Servico.BorderColor = Color.FromArgb(255, 3, 0);
                        ServicoHint.Visible = true;
                        Servico.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                    else
                    {
                        Servico.BorderColor = Color.FromArgb(210, 210, 210);
                        ServicoHint.Visible = false;
                        Servico.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                    }

                    // Tecnico
                    if (Tecnico.SelectedIndex == -1)
                    {
                        Tecnico.BorderColor = Color.FromArgb(255, 3, 0);
                        TecnicoHint.Visible = true;
                        Tecnico.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                    else
                    {
                        Tecnico.BorderColor = Color.FromArgb(210, 210, 210);
                        TecnicoHint.Visible = false;
                        Tecnico.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                    }

                    // Valor por hora
                    if (ValorPorHoraOuServico.Text == "")
                    {
                        ValorPorHoraOuServico.BorderColor = Color.FromArgb(255, 3, 0);
                        ValorPorHoraHint.Visible = true;
                        ValorPorHoraOuServico.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                    else
                    {
                        ValorPorHoraOuServico.BorderColor = Color.FromArgb(210, 210, 210);
                        ValorPorHoraHint.Visible = false;
                        ValorPorHoraOuServico.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                    }

                    if (FormaDeCobranca.SelectedIndex == 0)
                    {
                        // Valor por hora
                        if (QuantidadeHoras.Text == "")
                        {
                            QuantidadeHoras.BorderColor = Color.FromArgb(255, 3, 0);
                            QuantidadeDeHorasHint.Visible = true;
                            QuantidadeHoras.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                        }
                        else
                        {
                            QuantidadeHoras.BorderColor = Color.FromArgb(210, 210, 210);
                            QuantidadeDeHorasHint.Visible = false;
                            QuantidadeHoras.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                        }
                    }
                }
            }
        }

        // Cancelar
        private async void Cancelar1_Click(object sender, EventArgs e)
        {
            if (AnimateFrames)
            {
                TimerAnim.Start();
                await TaskDelay(100);

                foreach (Form frm in fc)
                {
                    if (frm.Name == "NovoServico")
                    {
                        if (frm.Opacity < 1.0)
                            frm.Opacity = 1.0;
                    }
                }

                Close();
            }
            else
            {
                foreach (Form frm in fc)
                {
                    if (frm.Name == "NovoServico")
                    {
                        if (frm.Opacity < 1.0)
                            frm.Opacity = 1.0;
                    }
                }

                Close();
            }
        }

        // Fechar
        private async void Close_Click(object sender, EventArgs e)
        {
            if (AnimateFrames)
            {
                TimerAnim.Start();
                await TaskDelay(100);

                foreach (Form frm in fc)
                {
                    if (frm.Name == "NovoServico")
                    {
                        if (frm.Opacity < 1.0)
                            frm.Opacity = 1.0;
                    }
                }

                Close();
            }
            else
            {
                foreach (Form frm in fc)
                {
                    if (frm.Name == "NovoServico")
                    {
                        if (frm.Opacity < 1.0)
                            frm.Opacity = 1.0;
                    }
                }

                Close();
            }
        }


        /*--------------------------------------------------------------------------------------------*/

        /* Text changed */

        private void DescricaoServico_TextChanged(object sender, EventArgs e)
        {
            ServicoHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                Servico.BorderColor = Color.FromArgb(80, 80, 80);

                Servico.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                Servico.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }

            else
            {
                Servico.BorderColor = Color.FromArgb(210, 210, 210);

                Servico.FocusedState.BorderColor = Color.Black;
                Servico.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }
        }

        private void FormaDeCobranca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormaDeCobranca.SelectedIndex == 0)
            {
                label2.Text = "VALOR COBRADO POR HORA";
                label33.Text = "DATA DE INÍCIO";

                QuantidadeHoras.Enabled = true;
                DataInicio.Enabled = true;
                DataFim.Enabled = true;
            }
            else if (FormaDeCobranca.SelectedIndex == 1)
            {
                label2.Text = "VALOR COBRADO POR SERVIÇO";
                label33.Text = "DATA DA PRESTAÇÃO DO SERVIÇO";

                QuantidadeHoras.Text = "";
                QuantidadeHoras.Enabled = false;
                DataInicio.Enabled = true;
                DataFim.Enabled = false;
            }
        }

        private void ValorPorHoraOuServico_Leave(object sender, EventArgs e)
        {
            CalculateValues();
        }

        private void ValorPorHoraOuServico_TextChanged(object sender, EventArgs e)
        {
            ValorPorHoraHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                ValorPorHoraOuServico.BorderColor = Color.FromArgb(80, 80, 80);

                ValorPorHoraOuServico.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                ValorPorHoraOuServico.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }

            else
            {
                ValorPorHoraOuServico.BorderColor = Color.FromArgb(210, 210, 210);

                ValorPorHoraOuServico.FocusedState.BorderColor = Color.Black;
                ValorPorHoraOuServico.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
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
        }

        private void QuantidadeHoras_Leave(object sender, EventArgs e)
        {
            CalculateValues();
        }

        private void QuantidadeHoras_TextChanged(object sender, EventArgs e)
        {
            QuantidadeDeHorasHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                QuantidadeHoras.BorderColor = Color.FromArgb(80, 80, 80);

                QuantidadeHoras.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                QuantidadeHoras.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }

            else
            {
                QuantidadeHoras.BorderColor = Color.FromArgb(210, 210, 210);

                QuantidadeHoras.FocusedState.BorderColor = Color.Black;
                QuantidadeHoras.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }
        }

        private void DataFim_ValueChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                int Horas = (DataFim.Value - DataInicio.Value).Hours;

                QuantidadeHoras.Text = Convert.ToString(Horas);

                CalculateValues();
            }
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Key press */

        // Converter texto pra moeda
        private void TextKeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar.Equals((char)Keys.Back))
            {
                Guna.UI2.WinForms.Guna2TextBox TextBox = (Guna.UI2.WinForms.Guna2TextBox)sender;

                string TextNumbers = Regex.Replace(TextBox.Text, "[^0-9]", string.Empty);

                if (TextNumbers == string.Empty)
                    TextNumbers = "00";

                if (e.KeyChar.Equals((char)Keys.Back))
                    TextNumbers = TextNumbers.Substring(0, TextNumbers.Length - 1);
                else
                    TextNumbers += e.KeyChar;

                TextBox.Text = string.Format("{0:#,##0.00}", Double.Parse(TextNumbers) / 100);

                TextBox.SelectionStart = TextBox.Text.Length;
            }

            e.Handled = true;
            TextoChanged = true;
        }

        // Apagar texto
        private void TextKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Guna.UI2.WinForms.Guna2TextBox TextBox = (Guna.UI2.WinForms.Guna2TextBox)sender;

                TextBox.Text = string.Format("{0:#,##0.00}", 0d);
                TextBox.Select(TextBox.Text.Length, 0);
                e.Handled = true;
            }

            TextoChanged = true;
        }
    }
}
