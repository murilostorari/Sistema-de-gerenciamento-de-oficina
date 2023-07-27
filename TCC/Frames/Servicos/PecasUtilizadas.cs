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
    public partial class PecasUtilizadas : Form
    {
        FormCollection fc = Application.OpenForms;

        NovoServico NovoServicoFrame;

        bool FormLoaded;
        bool CloseOpen;
        bool BackspacePressed;
        bool TextoChanged;

        decimal ValorHoras;
        decimal CustoUnitario;

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

        public PecasUtilizadas(NovoServico NovoServicoForm, string NumeroServico)
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
            OleDbConnection con2 = new OleDbConnection(strcon);

            con.Open();
            con2.Open();

            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT PRODUTO FROM Estoque";
            cmd.ExecuteNonQuery();

            OleDbCommand cmd2 = con2.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT NOME FROM Funcionarios";
            cmd2.ExecuteNonQuery();

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            OleDbDataAdapter adapter2 = new OleDbDataAdapter(cmd2);
            adapter.Fill(dt);
            adapter2.Fill(dt2);

            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

            foreach (DataRow dtr in dt.Rows)
            {
                collection.Add(dtr["PRODUTO"].ToString());
            }

            Produto.AutoCompleteMode = AutoCompleteMode.Suggest;
            Produto.AutoCompleteSource = AutoCompleteSource.CustomSource;

            Produto.AutoCompleteCustomSource = collection;

            foreach (DataRow dtr in dt2.Rows)
            {
                Tecnico.Items.Add(dtr["NOME"].ToString());
            }

            con.Close();
            con2.Close();

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

        private void UpdateCode()
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
                    if (Produto.Text == "")
                        Produto.Text = dtr["PRODUTO"].ToString();

                    Codigo.Text = dtr["CODIGO"].ToString();
                    Custo.Text = dtr["CUSTOCOMPRA"].ToString();
                    NumeroFabricante.Text = dtr["NUMEROFABRICANTE"].ToString();
                    Disponivel.Text = dtr["QNTDDISPONIVEL"].ToString();
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
            string TextNumbers = Regex.Replace(Custo.Text, "[^0-9]", string.Empty);

            decimal ConvertToDecimal;

            bool ConvertBool = decimal.TryParse(TextNumbers, NumberStyles.Currency,
            CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal);

            CustoUnitario = ConvertToDecimal;

            if (Usada.Text != "")
            {
                var ValorTT = ((decimal)Convert.ToDecimal(Usada.Text) * CustoUnitario);

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
            Label[] Labels = new Label[8]
            {
                label1, label2, label3, label4, label5, label6, label7, label9
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
            Guna.UI2.WinForms.Guna2TextBox[] TextBox = new Guna.UI2.WinForms.Guna2TextBox[7]
            {
                Produto, Codigo, Custo, Usada, ValorTotal, Disponivel, NumeroFabricante
            };

            // Combobox
            Guna.UI2.WinForms.Guna2ComboBox[] Combobox = new Guna.UI2.WinForms.Guna2ComboBox[1]
            {
               Tecnico
            };

            // Hints
            Guna.UI2.WinForms.Guna2HtmlLabel[] Hints = new Guna.UI2.WinForms.Guna2HtmlLabel[3]
            {
                ProdutoHint, TecnicoHint, UsadaHint
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
            Alerta AlertaForm = new Alerta();   

            if (Produto.Text != "" && Tecnico.SelectedIndex != -1 && Usada.Text != "")
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
                        if (Usada.Text != "")
                        {
                            if (Convert.ToInt32(Usada.Text) < Convert.ToInt32(Disponivel.Text))
                            {
                                string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";

                                NovoServicoFrame.Illustration2.Visible = false;
                                NovoServicoFrame.Desc2.Visible = false;
                                NovoServicoFrame.PecasUtilizadasGrid.Rows.Add(Codigo.Text, Produto.Text, Usada.Text, Custo.Text, ValorTotal.Text, Tecnico.Text);
                                NovoServicoFrame.PecasUtilizadasGrid.Visible = true;
                                NovoServicoFrame.guna2Separator1.Visible = true;

                                string comando2 =

                                // Campos
                                "INSERT INTO MaoDeObra (NUMEROSERVICO, CODIGO, NOME, VALORUNITARIO, QUANTIDADE, VALORTOTAL, TECNICO) \n" +


                               // Valores
                               "values (@NUMEROSERVICO, @CODIGO, @NOME, @VALORUNITARIO, @QUANTIDADE, @VALORTOTAL, @TECNICO)";

                                OleDbConnection con2 = new OleDbConnection(strcon);
                                OleDbCommand com = new OleDbCommand(comando2, con2);

                                com.Parameters.Add("@NUMEROSERVICO", OleDbType.VarChar).Value = NumeroDoServico.Text;

                                com.Parameters.Add("@CODIGO", OleDbType.VarChar).Value = Codigo.Text;
                                com.Parameters.Add("@NOME", OleDbType.VarChar).Value = Produto.Text;
                                com.Parameters.Add("@VALORUNITARIO", OleDbType.VarChar).Value = Custo.Text;
                                com.Parameters.Add("@QUANTIDADE", OleDbType.VarChar).Value = Usada.Text;
                                com.Parameters.Add("@VALORTOTAL", OleDbType.VarChar).Value = ValorTotal.Text;
                                com.Parameters.Add("@TECNICO", OleDbType.VarChar).Value = Tecnico.Text;

                                try
                                {
                                    con2.Open();
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
                            else
                            {
                                foreach (Form frm in fc)
                                {
                                    if (frm.Name == "PecasUtilizadas")
                                        frm.Opacity = .0d;
                                }

                                Alerta.AlertaFrame.LblText.Text = "A quantidade retirada é maior que a quantidade disponível!";

                                AlertaForm.Show();
                            }
                        }
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
            else
            {
                if (IsDarkModeEnabled)
                {
                    // Produto
                    if (Produto.Text == "")
                    {
                        Produto.BorderColor = Color.FromArgb(255, 33, 0);
                        ProdutoHint.Visible = true;
                        Produto.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }
                    else
                    {
                        Produto.BorderColor = Color.FromArgb(80, 80, 80);
                        ProdutoHint.Visible = false;
                        Produto.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
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

                    // Usada
                    if (Usada.Text == "")
                    {
                        Usada.BorderColor = Color.FromArgb(255, 33, 0);
                        UsadaHint.Visible = true;
                        Usada.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }
                    else
                    {
                        Usada.BorderColor = Color.FromArgb(80, 80, 80);
                        UsadaHint.Visible = false;
                        Usada.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                    }
                }

                else
                {
                    // Servico
                    if (Produto.Text == "")
                    {
                        Produto.BorderColor = Color.FromArgb(255, 3, 0);
                        ProdutoHint.Visible = true;
                        Produto.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                    else
                    {
                        Produto.BorderColor = Color.FromArgb(210, 210, 210);
                        ProdutoHint.Visible = false;
                        Produto.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
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

                    // Usada
                    if (Usada.Text == "")
                    {
                        Usada.BorderColor = Color.FromArgb(255, 3, 0);
                        UsadaHint.Visible = true;
                        Usada.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                    else
                    {
                        Usada.BorderColor = Color.FromArgb(210, 210, 210);
                        UsadaHint.Visible = false;
                        Usada.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
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

        private void Produto_TextChanged(object sender, EventArgs e)
        {
            UpdateCode();
        }

        private void Produto_Leave(object sender, EventArgs e)
        {
            UpdateCode();
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Key press */

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

        private void Usada_TextChanged(object sender, EventArgs e)
        {
            UsadaHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                Usada.BorderColor = Color.FromArgb(80, 80, 80);

                Usada.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                Usada.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }
            else
            {
                Usada.BorderColor = Color.FromArgb(210, 210, 210);

                Usada.FocusedState.BorderColor = Color.Black;
                Usada.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }

            if (Usada.Text != "")
            {
                CalculateValues();
            }
        }
    }
}
