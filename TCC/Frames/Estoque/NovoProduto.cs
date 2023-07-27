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
    public partial class NovoProduto : Form
    {
        FormCollection fc = Application.OpenForms;

        bool FormLoaded;
        bool CloseOpen;
        bool TextoChanged;

        decimal CustoEmReais;
        decimal VendaEmReais;
        decimal LucroEmReais;
        decimal LucroEmPorcentagem;
        decimal ValorEmDolar;

        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;
        bool AutoCompleteValues = Properties.Settings.Default.AutoCompleteCurrencyValues;

        List<Guna.UI2.WinForms.Guna2Button> GunaButtons;
        List<Guna.UI2.WinForms.Guna2Button> GunaBorderButtons;
        List<Guna.UI2.WinForms.Guna2TextBox> GunaTextBox;
        List<Guna.UI2.WinForms.Guna2ComboBox> GunaComboBox;
        List<Guna.UI2.WinForms.Guna2HtmlLabel> GunaHints;

        List<Label> NormalLabels;

        ImageList IconsProduto = new ImageList();

        public NovoProduto()
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
            foreach (Control ct in Valores.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Quantidades.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Outros.Controls)
            {
                ct.TabStop = false;
            }

            Observaçoes.ForeColor = Color.Black;

            IconsProduto.Images.Add(Properties.Resources.produto);
            IconsProduto.Images.Add(Properties.Resources.produto2);

            IconsProduto.ImageSize = new Size(92, 92);

            string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";
            OleDbConnection con = new OleDbConnection(strcon);

            con.Open();

            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT NOME FROM Fornecedores";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            adapter.Fill(dt);

            foreach (DataRow dtr in dt.Rows)
            {
                Fornecedor.Items.Add(dtr["NOME"].ToString());
            }

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
                        Frames.CloseConfirm.CloseFrame.TopText.Text = "Cancelar cadastro";
                        Frames.CloseConfirm.CloseFrame.LblText.Text = "Você deseja mesmo cancelar o cadastro de novo produto?";
                    }
                }

                if (frm.Name == "NovoProduto")
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

            Valores.Visible = true;
            Valores.Location = new Point(12, 134);

            Quantidades.Visible = false;
            Quantidades.Location = new Point(12, 7734);

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

            foreach (Control ct in Valores.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in Quantidades.Controls)
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

            Valores.Visible = false;
            Valores.Location = new Point(12, 7734);

            Quantidades.Visible = true;
            Quantidades.Location = new Point(12, 134);

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

            foreach (Control ct in Valores.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Quantidades.Controls)
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

            Valores.Visible = false;
            Valores.Location = new Point(12, 7734);

            Quantidades.Visible = false;
            Quantidades.Location = new Point(12, 7734);

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

            foreach (Control ct in Valores.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Quantidades.Controls)
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

        private void CalculateValues()
        {
            decimal Price = VendaEmReais - CustoEmReais;

            LucroEmReais = Price;

            LucroReais.Text = Convert.ToString(LucroEmReais);

            string TextNumbers = Regex.Replace(LucroReais.Text, "[^0-9]", string.Empty);

            LucroReais.Text = string.Format("{0:#,##0.00}", Double.Parse(TextNumbers) / 100);
            LucroReais.Text = Convert.ToString("R$ " + LucroEmReais);
            LucroReais.SelectionStart = LucroReais.Text.Length;

            if (VendaEmReais > 0 && CustoEmReais > 0)
            {
                var ConvertoToPercentage = ((decimal)VendaEmReais / CustoEmReais) * 50;

                LucroEmPorcentagem = Convert.ToInt32(Math.Round(ConvertoToPercentage, 0));
            }

            LucroPorcentagem.Text = Convert.ToString(LucroEmPorcentagem + "%");

            if (VendaEmReais < CustoEmReais)
                LucroPorcentagem.Text = Convert.ToString("-" + LucroEmPorcentagem + "%");

            ConvertToDolar();
        }

        private void ConvertToDolar()
        {    
            string strURL = "https://api.hgbrasil.com/finance?array_limit=1&fields=only_results,USD&key=4bade9f3";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = client.GetAsync(strURL).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;

                        CotacaoDolarHG.Market market = JsonConvert.DeserializeObject<CotacaoDolarHG.Market>(result);

                        ValorEmDolar = Convert.ToDecimal(market.Currency.Buy);

                        CustoDolar.Text = Convert.ToString(ValorEmDolar);

                        CustoDolar.Text = "$ " + Convert.ToString(decimal.Round(CustoEmReais / ValorEmDolar, 2, MidpointRounding.AwayFromZero));

                        if (PrecoVendaConsumidor.Text != "" || PrecoRevenda.Text != "" || PrecoVendaOutros.Text != "")
                        {
                            LucroDolar.Text = Convert.ToString(ValorEmDolar);
                            LucroDolar.Text = "$ " + Convert.ToString(decimal.Round(LucroEmReais / ValorEmDolar, 2, MidpointRounding.AwayFromZero));
                        }
                    }
                    else
                    {
                        CustoDolar.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
            Label[] Labels = new Label[26]
            {
                label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13,
                label14, label15, label16, label17, label18, label19, label20, label21, label22, label23, label24, label25,
                label26
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
            Guna.UI2.WinForms.Guna2TextBox[] TextBox = new Guna.UI2.WinForms.Guna2TextBox[19]
            {
                Produto, Codigo, NumeroFabricante, NumeroOriginal, Marca, Custo, PrecoVendaConsumidor, PrecoRevenda, PrecoVendaOutros, CustoDolar, 
                LucroDolar, LucroPorcentagem, LucroReais, Disponivel, Ideal, Minima, Localizacao, Prateleira, Observaçoes
            };

            // Combobox
            Guna.UI2.WinForms.Guna2ComboBox[] Combobox = new Guna.UI2.WinForms.Guna2ComboBox[6]
            {
                Unidade, Grupo, Subgrupo, TipoProduto, Fornecedor, Curva
            };

            // Hints
            Guna.UI2.WinForms.Guna2HtmlLabel[] Hints = new Guna.UI2.WinForms.Guna2HtmlLabel[13]
            {
                NomeHint, CodigoHint, NumFabricanteHint, TipoProdutoHint, CustoHint, PrecoVendaConsumidorHint, PrecoRevendaHint, PrecoVendaOutrosHint,
                DisponivelHint, IdealHint, MinimaHint, CurvaHint, FornecedorHint
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

            CodigoEAN13.BackColor = ThemeManager.FormBackColor;
            CodigoEAN13.ForeColor = ThemeManager.RedFontColor;

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

            if (Valores.Location == new Point(12, 134))
            {
                ProgressText2.ForeColor = ThemeManager.FullRedButtonColor;
                ProgressBar2.BackColor = ThemeManager.FullRedButtonColor;
            }

            if (Outros.Location == new Point(12, 134))
            {
                ProgressText3.ForeColor = ThemeManager.FullRedButtonColor;
                ProgressBar3.BackColor = ThemeManager.FullRedButtonColor;
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
            if (Produto.Text != "" && Codigo.Text != "" && NumeroFabricante.Text != "" && TipoProduto.SelectedIndex != -1)
            {
                CodigoCODE128.Text = Codigo.Text;

                Alerta AlertForm = new Alerta();

                string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";

                OleDbConnection con = new OleDbConnection(strcon);

                con.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT COUNT (*) FROM Estoque WHERE (PRODUTO = @PRODUTO)", con);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                cmd.Parameters.Add("@PRODUTO", OleDbType.VarChar).Value = Produto.Text;

                int ExistItem = (int)cmd.ExecuteScalar();

                if (ExistItem > 0)
                {
                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "NovoProduto")
                            frm.Opacity = .0d;
                    }

                    Alerta.AlertaFrame.LblText.Text = "Já existe um produto com este nome cadastrado no sistema!";

                    AlertForm.Show();
                }
                else
                    ProximoN1();
            }

            // Modo escuro ativado
            if (IsDarkModeEnabled)
            {
                // Nome
                if (Produto.Text == "")
                {
                    Produto.BorderColor = Color.FromArgb(255, 33, 0);
                    NomeHint.Visible = true;
                    Produto.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Produto.BorderColor = Color.FromArgb(80, 80, 80);
                    NomeHint.Visible = false;
                    Produto.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }

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

                // Codigo
                if (NumeroFabricante.Text == "")
                {
                    NumeroFabricante.BorderColor = Color.FromArgb(255, 33, 0);
                    NumFabricanteHint.Visible = true;
                    NumeroFabricante.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    NumeroFabricante.BorderColor = Color.FromArgb(80, 80, 80);
                    NumFabricanteHint.Visible = false;
                    NumeroFabricante.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }

                // Tipo de produto
                if (TipoProduto.SelectedIndex == -1)
                {
                    TipoProduto.BorderColor = Color.FromArgb(255, 33, 0);
                    TipoProdutoHint.Visible = true;
                    TipoProduto.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    TipoProduto.BorderColor = Color.FromArgb(80, 80, 80);
                    TipoProdutoHint.Visible = false;
                    TipoProduto.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
            }

            // Modo escuro desativado
            else
            {
                // Nome
                if (Produto.Text == "")
                {
                    Produto.BorderColor = Color.FromArgb(255, 3, 0);
                    NomeHint.Visible = true;
                    Produto.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Produto.BorderColor = Color.FromArgb(210, 210, 210);
                    NomeHint.Visible = false;
                    Produto.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

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

                // Codigo
                if (NumeroFabricante.Text == "")
                {
                    NumeroFabricante.BorderColor = Color.FromArgb(255, 3, 0);
                    NumFabricanteHint.Visible = true;
                    NumeroFabricante.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    NumeroFabricante.BorderColor = Color.FromArgb(210, 210, 210);
                    NumFabricanteHint.Visible = false;
                    NumeroFabricante.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                // Tipo de produto
                if (TipoProduto.SelectedIndex == -1)
                {
                    TipoProduto.BorderColor = Color.FromArgb(255, 3, 0);
                    TipoProdutoHint.Visible = true;
                    TipoProduto.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    TipoProduto.BorderColor = Color.FromArgb(210, 210, 210);
                    TipoProdutoHint.Visible = false;
                    TipoProduto.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        private void Proximo2_Click(object sender, EventArgs e)
        {
            Alerta AlertForm = new Alerta();

            if (Custo.Text != "" && PrecoVendaConsumidor.Text != "" && PrecoRevenda.Text != "" && PrecoVendaOutros.Text != "")
            {
                if (VendaEmReais > CustoEmReais)
                    ProximoN2();
                else
                {
                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "NovoProduto")
                            frm.Opacity = 0;
                    }

                    Alerta.AlertaFrame.LblText.Text = "O valor do custo é maior que o valor do lucro!";

                    AlertForm.Show();
                }
            }

            // Modo escuro ativado
            if (IsDarkModeEnabled)
            {
                // Custo
                if (Custo.Text == "")
                {
                    Custo.BorderColor = Color.FromArgb(255, 33, 0);
                    CustoHint.Visible = true;
                    Custo.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Custo.BorderColor = Color.FromArgb(80, 80, 80);
                    CustoHint.Visible = false;
                    Custo.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }

                // Preço venda consumidor
                if (PrecoVendaConsumidor.Text == "")
                {
                    PrecoVendaConsumidor.BorderColor = Color.FromArgb(255, 33, 0);
                    PrecoVendaConsumidorHint.Visible = true;
                    PrecoVendaConsumidor.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    PrecoVendaConsumidor.BorderColor = Color.FromArgb(80, 80, 80);
                    PrecoVendaConsumidorHint.Visible = false;
                    PrecoVendaConsumidor.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }

                // Preço revenda
                if (PrecoRevenda.Text == "")
                {
                    PrecoRevenda.BorderColor = Color.FromArgb(255, 33, 0);
                    PrecoRevendaHint.Visible = true;
                    PrecoRevenda.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    PrecoRevenda.BorderColor = Color.FromArgb(80, 80, 80);
                    PrecoRevendaHint.Visible = false;
                    PrecoRevenda.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }

                // Preço venda outros
                if (PrecoVendaOutros.Text == "")
                {
                    PrecoVendaOutros.BorderColor = Color.FromArgb(255, 33, 0);
                    PrecoVendaOutrosHint.Visible = true;
                    PrecoVendaOutros.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    PrecoVendaOutros.BorderColor = Color.FromArgb(80, 80, 80);
                    PrecoVendaOutrosHint.Visible = false;
                    PrecoVendaOutros.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
            }

            // Modo escuro desativado
            else
            {
                // Custo
                if (Custo.Text == "")
                {
                    Custo.BorderColor = Color.FromArgb(255, 3, 0);
                    CustoHint.Visible = true;
                    Custo.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Custo.BorderColor = Color.FromArgb(210, 210, 210);
                    CustoHint.Visible = false;
                    Custo.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                // Preço venda consumidor
                if (PrecoVendaConsumidor.Text == "")
                {
                    PrecoVendaConsumidor.BorderColor = Color.FromArgb(255, 3, 0);
                    PrecoVendaConsumidorHint.Visible = true;
                    PrecoVendaConsumidor.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    PrecoVendaConsumidor.BorderColor = Color.FromArgb(210, 210, 210);
                    PrecoVendaConsumidorHint.Visible = false;
                    PrecoVendaConsumidor.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                // Preço revenda
                if (PrecoRevenda.Text == "")
                {
                    PrecoRevenda.BorderColor = Color.FromArgb(255, 3, 0);
                    PrecoRevendaHint.Visible = true;
                    PrecoRevenda.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    PrecoRevenda.BorderColor = Color.FromArgb(210, 210, 210);
                    PrecoRevendaHint.Visible = false;
                    PrecoRevenda.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                // Preço venda outros
                if (PrecoVendaOutros.Text == "")
                {
                    PrecoVendaOutros.BorderColor = Color.FromArgb(255, 3, 0);
                    PrecoVendaOutrosHint.Visible = true;
                    PrecoVendaOutros.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    PrecoVendaOutros.BorderColor = Color.FromArgb(210, 210, 210);
                    PrecoVendaOutrosHint.Visible = false;
                    PrecoVendaOutros.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        private void Proximo3_Click(object sender, EventArgs e)
        {
            if (Disponivel.Text != "" && Ideal.Text != "" && Minima.Text != "" && Curva.SelectedIndex != -1 && Fornecedor.Text != "")
            {
                ProximoN3();

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
            }

            // Modo escuro ativado
            if (IsDarkModeEnabled)
            {
                // Disponivel
                if (Disponivel.Text == "")
                {
                    Disponivel.BorderColor = Color.FromArgb(255, 33, 0);
                    DisponivelHint.Visible = true;
                    Disponivel.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Disponivel.BorderColor = Color.FromArgb(80, 80, 80);
                    DisponivelHint.Visible = false;
                    Disponivel.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }

                // Ideal
                if (Ideal.Text == "")
                {
                    Ideal.BorderColor = Color.FromArgb(255, 33, 0);
                    IdealHint.Visible = true;
                    Ideal.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Ideal.BorderColor = Color.FromArgb(80, 80, 80);
                    IdealHint.Visible = false;
                    Ideal.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }

                // Minima
                if (Minima.Text == "")
                {
                    Minima.BorderColor = Color.FromArgb(255, 33, 0);
                    MinimaHint.Visible = true;
                    Minima.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Minima.BorderColor = Color.FromArgb(80, 80, 80);
                    MinimaHint.Visible = false;
                    Minima.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }

                // Curva
                if (Curva.SelectedIndex == -1)
                {
                    Curva.BorderColor = Color.FromArgb(255, 33, 0);
                    CurvaHint.Visible = true;
                    Curva.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Curva.BorderColor = Color.FromArgb(80, 80, 80);
                    CurvaHint.Visible = false;
                    Curva.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }

                // Fornecedor
                if (Fornecedor.Text == "")
                {
                    Fornecedor.BorderColor = Color.FromArgb(255, 33, 0);
                    FornecedorHint.Visible = true;
                    Fornecedor.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Fornecedor.BorderColor = Color.FromArgb(80, 80, 80);
                    FornecedorHint.Visible = false;
                    Fornecedor.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
            }

            // Modo escuro ativado
            else
            {
                // Disponivel
                if (Disponivel.Text == "")
                {
                    Disponivel.BorderColor = Color.FromArgb(255, 3, 0);
                    DisponivelHint.Visible = true;
                    Disponivel.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Disponivel.BorderColor = Color.FromArgb(210, 210, 210);
                    DisponivelHint.Visible = false;
                    Disponivel.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                // Ideal
                if (Ideal.Text == "")
                {
                    Ideal.BorderColor = Color.FromArgb(255, 3, 0);
                    IdealHint.Visible = true;
                    Ideal.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Ideal.BorderColor = Color.FromArgb(210, 210, 210);
                    IdealHint.Visible = false;
                    Ideal.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                // Minima
                if (Minima.Text == "")
                {
                    Minima.BorderColor = Color.FromArgb(255, 3, 0);
                    MinimaHint.Visible = true;
                    Minima.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Minima.BorderColor = Color.FromArgb(210, 210, 210);
                    MinimaHint.Visible = false;
                    Minima.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                // Curva
                if (Curva.SelectedIndex == -1)
                {
                    Curva.BorderColor = Color.FromArgb(255, 3, 0);
                    CurvaHint.Visible = true;
                    Curva.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Curva.BorderColor = Color.FromArgb(210, 210, 210);
                    CurvaHint.Visible = false;
                    Curva.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                // Fornecedor
                if (Fornecedor.Text == "")
                {
                    Fornecedor.BorderColor = Color.FromArgb(255, 3, 0);
                    FornecedorHint.Visible = true;
                    Fornecedor.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Fornecedor.BorderColor = Color.FromArgb(210, 210, 210);
                    FornecedorHint.Visible = false;
                    Fornecedor.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        // Concluir
        private void Concluir_Click(object sender, EventArgs e)
        {
            AddSuccess SuccessForm = new AddSuccess();
            Erro ErrorForm = new Erro();

            if (NumeroOriginal.Text == "")
                NumeroOriginal.Text = "-";
            if (Marca.Text == "")
                Marca.Text = "-";
            if (CustoDolar.Text == "")
                CustoDolar.Text = "-";
            if (LucroDolar.Text == "")
                LucroDolar.Text = "-";
            if (Localizacao.Text == "")
                Localizacao.Text = "-";
            if (Prateleira.Text == "")
                Prateleira.Text = "-";
            if (Fornecedor.SelectedIndex == -1)
                Fornecedor.Text = "Indiferente";
            if (Observaçoes.Text == "")
            {
                Observaçoes.ForeColor = Color.White;
                Observaçoes.Text = "-";
            }

            string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";

            string comando =

                // Campos
                "INSERT INTO Estoque (CODIGO, PRODUTO, NUMEROFABRICANTE, NUMEROORIGINAL, MARCA, GRUPO, SUBGRUPO," +
                "TIPO, UNIDADE, QNTDDISPONIVEL, QNTDMINIMA, QNTDIDEAL, CUSTOCOMPRA, VALORVENDACONSUMIDOR, VALORREVENDA," +
                "VALORVENDAOUTROS, CUSTOEMDOLAR, LUCROEMDOLAR, LUCROPORCENTAGEM, LUCROREAIS, ULTIMAVENDA, FORNECEDOR, LOCALIZACAOPRODUTO," +
                "PRATELEIRA, OBSERVACOES, CURVA, EAN13BARCODETEXT, CODE128BARCODETEXT, STATUS, FOTO) \n" +


                // Valores
                "values (@CODIGO, @PRODUTO, @NUMEROFABRICANTE, @NUMEROORIGINAL, @MARCA, @GRUPO, @SUBGRUPO," +
                "@TIPO, @UNIDADE, @QNTDDISPONIVEL, @QNTDMINIMA, @QNTDIDEAL, @CUSTOCOMPRA, @VALORVENDACONSUMIDOR, @VALORREVENDA," +
                "@VALORVENDAOUTROS, @CUSTOEMDOLAR, @LUCROEMDOLAR, @LUCROPORCENTAGEM, @LUCROREAIS, @ULTIMAVENDA, @FORNECEDOR, @LOCALIZACAOPRODUTO," +
                "@PRATELEIRA, @OBSERVACOES, @CURVA, @EAN13BARCODETEXT, @CODE128BARCODETEXT, @STATUS, @FOTO)";

            OleDbConnection con = new OleDbConnection(strcon);
            OleDbCommand com = new OleDbCommand(comando, con);

            com.Parameters.Add("@CODIGO", OleDbType.VarChar).Value = Codigo.Text;
            com.Parameters.Add("@PRODUTO", OleDbType.VarChar).Value = Produto.Text;
            com.Parameters.Add("@NUMEROFABRICANTE", OleDbType.VarChar).Value = NumeroFabricante.Text;
            com.Parameters.Add("@NUMEROORIGINAL", OleDbType.VarChar).Value = NumeroOriginal.Text;
            com.Parameters.Add("@MARCA", OleDbType.VarChar).Value = Marca.Text;

            if (Grupo.SelectedIndex == -1)
                com.Parameters.Add("@GRUPO", OleDbType.VarChar).Value = "Indiferente";
            else
                com.Parameters.Add("@GRUPO", OleDbType.VarChar).Value = Grupo.Text;

            if (Subgrupo.SelectedIndex == -1)
                com.Parameters.Add("@SUBGRUPO", OleDbType.VarChar).Value = "Indiferente";
            else
                com.Parameters.Add("@SUBGRUPO", OleDbType.VarChar).Value = Subgrupo.Text;

            com.Parameters.Add("@TIPO", OleDbType.VarChar).Value = TipoProduto.Text;

            if (Unidade.SelectedIndex == -1)
                com.Parameters.Add("@UNIDADE", OleDbType.VarChar).Value = "Indiferente";
            else
                com.Parameters.Add("@UNIDADE", OleDbType.VarChar).Value = Unidade.Text;

            com.Parameters.Add("@QNTDDISPONIVEL", OleDbType.Numeric).Value = Convert.ToInt32(Disponivel.Text);
            com.Parameters.Add("@QNTDMINIMA", OleDbType.Numeric).Value = Convert.ToInt32(Minima.Text);
            com.Parameters.Add("@QNTDIDEAL", OleDbType.Numeric).Value = Convert.ToInt32(Ideal.Text);
            com.Parameters.Add("@CUSTOCOMPRA", OleDbType.VarChar).Value = Custo.Text;
            com.Parameters.Add("@VALORVENDACONSUMIDOR", OleDbType.VarChar).Value = PrecoVendaConsumidor.Text;
            com.Parameters.Add("@VALORREVENDA", OleDbType.VarChar).Value = PrecoRevenda.Text;
            com.Parameters.Add("@VALORVENDAOUTROS", OleDbType.VarChar).Value = PrecoVendaOutros.Text;
            com.Parameters.Add("@CUSTOEMDOLAR", OleDbType.VarChar).Value = CustoDolar.Text;
            com.Parameters.Add("@LUCROEMDOLAR", OleDbType.VarChar).Value = LucroDolar.Text;
            com.Parameters.Add("@LUCROPORCENTAGEM", OleDbType.VarChar).Value = LucroPorcentagem.Text;
            com.Parameters.Add("@LUCROREAIS", OleDbType.VarChar).Value = LucroReais.Text;
            com.Parameters.Add("@ULTIMAVENDA", OleDbType.Date).Value = UltimaVenda.Value.ToLongDateString();
            com.Parameters.Add("@FORNECEDOR", OleDbType.VarChar).Value = Fornecedor.Text;
            com.Parameters.Add("@LOCALIZACAOPRODUTO", OleDbType.VarChar).Value = Localizacao.Text;
            com.Parameters.Add("@PRATELEIRA", OleDbType.VarChar).Value = Prateleira.Text;
            com.Parameters.Add("@OBSERVACOES", OleDbType.VarChar).Value = Observaçoes.Text;
            com.Parameters.Add("@CURVA", OleDbType.VarChar).Value = Curva.Text;
            com.Parameters.Add("@EAN13BARCODETEXT", OleDbType.VarChar).Value = EAN13Insert.Text;
            com.Parameters.Add("@CODE128BARCODTEXT", OleDbType.VarChar).Value = CodigoCODE128.Text;
            com.Parameters.Add("@STATUS", OleDbType.VarChar).Value = Status.Text;

            if (ProductPicture.Image != null)
            {
                Bitmap BitmapImg = new Bitmap(ProductPicture.Image);
                byte[] picture = ImageToByte(BitmapImg, System.Drawing.Imaging.ImageFormat.Jpeg);

                com.Parameters.Add("@FOTO", OleDbType.Binary).Value = picture;
            }

            try
            {
                con.Open();
                com.ExecuteNonQuery();

                foreach (Form frm in fc)
                {
                    if (frm.Name == "NovoProduto")
                        frm.Opacity = .0d;
                }

                AddSuccess.SuccessAdd.LblText.Text = "Produto cadastrado com sucesso!";

                SuccessForm.Show();

                Properties.Settings.Default.CanUpdateGrid = true;
            }
            catch (Exception)
            {
                foreach(Form frm in fc)
                {
                    if (frm.Name == "NovoProduto")
                        frm.Opacity = .0d;
                }

                Erro.ErrorFrame.LblText.Text = "Erro ao cadastrar produto!";

                ErrorForm.Show();
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

            Valores.Visible = false;
            Valores.Location = new Point(12, 7734);

            Quantidades.Visible = false;
            Quantidades.Location = new Point(12, 7734);

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

            foreach (Control ct in Valores.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Quantidades.Controls)
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

            Valores.Visible = true;
            Valores.Location = new Point(12, 134);

            Quantidades.Visible = false;
            Quantidades.Location = new Point(12, 7734);

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

            foreach (Control ct in Valores.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in Quantidades.Controls)
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

            Valores.Visible = false;
            Valores.Location = new Point(12, 7734);

            Quantidades.Visible = true;
            Quantidades.Location = new Point(12, 134);

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

            foreach (Control ct in Valores.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Quantidades.Controls)
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
            if (TipoProduto.SelectedIndex == 0)
            {
                Random rand = new Random();

                int index = rand.Next(IconsProduto.Images.Count);

                ProductPicture.Image = IconsProduto.Images[index];
            }
            else
            {
                ProductPicture.Image = Properties.Resources.kit;
            }
        }

        private void ChoosePicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog PictureDialog = new OpenFileDialog();
            PictureDialog.Title = "Selecione uma foto";
            PictureDialog.Filter = "Arquivo JPG|*.jpg|Arquivo JPEG|*.jpeg|Arquivo PNG|*.png";

            if (PictureDialog.ShowDialog() == DialogResult.OK)
            {
                ProductPicture.Image = new Bitmap(PictureDialog.FileName);
            }
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Text changed */

        private void Nome_TextChanged(object sender, EventArgs e)
        {
            NomeHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                Produto.BorderColor = Color.FromArgb(80, 80, 80);
                Produto.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                Produto.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }
            else
            {
                Produto.BorderColor = Color.FromArgb(210, 210, 210);
                Produto.FocusedState.BorderColor = Color.Black;
                Produto.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }
        }

        private void Codigo_TextChanged(object sender, EventArgs e)
        {
            CodigoHint.Visible = false;
            string Barcode, Check12Digits;

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

            if (Codigo.Text != "")
            {
                Check12Digits = Codigo.Text.PadLeft(12, '0');

                Barcode = EAN13Class.EAN13(Check12Digits);

                EAN13Insert.Text = Barcode;
                CODE128Insert.Text = Codigo.Text;

                if (!String.Equals(EAN13Class.Barcode13Digits, "") || (EAN13Class.Barcode13Digits != ""))
                {
                    CodigoEAN13.Text = EAN13Class.Barcode13Digits.ToString();
                }
            }
        }

        private void NumeroFabricante_TextChanged(object sender, EventArgs e)
        {
            NumFabricanteHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                NumeroFabricante.BorderColor = Color.FromArgb(80, 80, 80);
                NumeroFabricante.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                NumeroFabricante.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }
            else
            {
                NumeroFabricante.BorderColor = Color.FromArgb(210, 210, 210);
                NumeroFabricante.FocusedState.BorderColor = Color.Black;
                NumeroFabricante.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }
        }

        private void TipoProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoProdutoHint.Visible = false;

            if (IsDarkModeEnabled)
                TipoProduto.BorderColor = Color.FromArgb(80, 80, 80);
            else
                TipoProduto.BorderColor = Color.FromArgb(210, 210, 210);

            if (TipoProduto.SelectedIndex == 0)
            {
                string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsEstoque\\produto", "*.png");
                List<string> Icones = Pasta.ToList();
                Random RandomIcon = new Random();
                ProductPicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
            }

            else if (TipoProduto.SelectedIndex == 1)
            {
                string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsEstoque\\kit", "*.png");
                List<string> Icones = Pasta.ToList();
                Random RandomIcon = new Random();
                ProductPicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
            }
        }

        private void Custo_TextChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                CustoHint.Visible = false;

                if (IsDarkModeEnabled)
                {
                    Custo.BorderColor = Color.FromArgb(80, 80, 80);
                    Custo.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                    Custo.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
                else
                {
                    Custo.BorderColor = Color.FromArgb(210, 210, 210);
                    Custo.FocusedState.BorderColor = Color.Black;
                    Custo.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        private void PrecoVendaConsumidor_TextChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                PrecoVendaConsumidorHint.Visible = false;

                if (IsDarkModeEnabled)
                {
                    PrecoVendaConsumidor.BorderColor = Color.FromArgb(80, 80, 80);
                    PrecoVendaConsumidor.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                    PrecoVendaConsumidor.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
                else
                {
                    PrecoVendaConsumidor.BorderColor = Color.FromArgb(210, 210, 210);
                    PrecoVendaConsumidor.FocusedState.BorderColor = Color.Black;
                    PrecoVendaConsumidor.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }       
        }

        private void PrecoRevenda_TextChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                PrecoRevendaHint.Visible = false;

                if (IsDarkModeEnabled)
                {
                    PrecoRevenda.BorderColor = Color.FromArgb(80, 80, 80);
                    PrecoRevenda.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                    PrecoRevenda.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
                else
                {
                    PrecoRevenda.BorderColor = Color.FromArgb(210, 210, 210);
                    PrecoRevenda.FocusedState.BorderColor = Color.Black;
                    PrecoRevenda.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        private void PrecoVendaOutros_TextChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                PrecoVendaOutrosHint.Visible = false;

                if (IsDarkModeEnabled)
                {
                    PrecoVendaOutros.BorderColor = Color.FromArgb(80, 80, 80);
                    PrecoVendaOutros.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                    PrecoVendaOutros.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
                else
                {
                    PrecoVendaOutros.BorderColor = Color.FromArgb(210, 210, 210);
                    PrecoVendaOutros.FocusedState.BorderColor = Color.Black;
                    PrecoVendaOutros.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        private void Disponivel_TextChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                DisponivelHint.Visible = false;

                if (IsDarkModeEnabled)
                {
                    Disponivel.BorderColor = Color.FromArgb(80, 80, 80);
                    Disponivel.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                    Disponivel.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
                else
                {
                    Disponivel.BorderColor = Color.FromArgb(210, 210, 210);
                    Disponivel.FocusedState.BorderColor = Color.Black;
                    Disponivel.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        private void Ideal_TextChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                IdealHint.Visible = false;

                if (IsDarkModeEnabled)
                {
                    Ideal.BorderColor = Color.FromArgb(80, 80, 80);
                    Ideal.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                    Ideal.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
                else
                {
                    Ideal.BorderColor = Color.FromArgb(210, 210, 210);
                    Ideal.FocusedState.BorderColor = Color.Black;
                    Ideal.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        private void Minima_TextChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                MinimaHint.Visible = false;

                if (IsDarkModeEnabled)
                {
                    Minima.BorderColor = Color.FromArgb(80, 80, 80);
                    Minima.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                    Minima.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
                else
                {
                    Minima.BorderColor = Color.FromArgb(210, 210, 210);
                    Minima.FocusedState.BorderColor = Color.Black;
                    Minima.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        private void Curva_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                CurvaHint.Visible = false;

                if (IsDarkModeEnabled)
                {
                    Curva.BorderColor = Color.FromArgb(80, 80, 80);
                    Curva.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                    Curva.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
                else
                {
                    Curva.BorderColor = Color.FromArgb(210, 210, 210);
                    Curva.FocusedState.BorderColor = Color.Black;
                    Curva.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        private void Fornecedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                FornecedorHint.Visible = false;

                if (IsDarkModeEnabled)
                {
                    Fornecedor.BorderColor = Color.FromArgb(80, 80, 80);
                    Fornecedor.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                    Fornecedor.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
                else
                {
                    Fornecedor.BorderColor = Color.FromArgb(210, 210, 210);
                    Fornecedor.FocusedState.BorderColor = Color.Black;
                    Fornecedor.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Key press */

        private void Codigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void LucroPorcentagem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Custo.Text == "")
                e.Handled = true;
            else
                e.Handled = false;

            if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Disponivel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Ideal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Minima_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }
        }

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

        /*--------------------------------------------------------------------------------------------*/

        /* Converter texto pra moeda */

        private void Custo_Leave(object sender, EventArgs e)
        {
            if (Custo.Text != "")
            {
                string StringNumbers = Custo.Text;

                decimal ConvertToDecimal;

                bool ConvertBool = decimal.TryParse(StringNumbers, NumberStyles.Currency,
                CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal);

                CustoEmReais = ConvertToDecimal;

                if (Custo.Text.Contains("R") && Custo.Text.Contains("$"))
                    Custo.Text = Custo.Text;
                else
                {
                    Custo.Text = Custo.Text.Insert(0, "R");
                    Custo.Text = Custo.Text.Insert(1, "$");
                    Custo.Text = Custo.Text.Insert(2, " ");
                }

                if (AutoCompleteValues)
                {
                    if (PrecoVendaConsumidor.Text != "" && PrecoRevenda.Text != "" && PrecoVendaOutros.Text != "")
                        CalculateValues();
                }
                else
                {
                    if (PrecoVendaConsumidor.Text != "" || PrecoRevenda.Text != "" || PrecoVendaOutros.Text != "")
                        CalculateValues();
                }

                TextoChanged = true;
            }
        }

        private void PrecoVendaConsumidor_Leave(object sender, EventArgs e)
        {     
            if (PrecoVendaConsumidor.Text != "")
            {
                string StringNumbers = PrecoVendaConsumidor.Text;

                decimal ConvertToDecimal;

                bool ConvertBool = decimal.TryParse(StringNumbers, NumberStyles.Currency,
                CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal);

                VendaEmReais = ConvertToDecimal;

                if (PrecoVendaConsumidor.Text.Contains("R") && PrecoVendaConsumidor.Text.Contains("$"))
                    PrecoVendaConsumidor.Text = PrecoVendaConsumidor.Text;
                else
                {
                    PrecoVendaConsumidor.Text = PrecoVendaConsumidor.Text.Insert(0, "R");
                    PrecoVendaConsumidor.Text = PrecoVendaConsumidor.Text.Insert(1, "$");
                    PrecoVendaConsumidor.Text = PrecoVendaConsumidor.Text.Insert(2, " ");
                }

                if (AutoCompleteValues)
                {
                    PrecoRevenda.Text = PrecoVendaConsumidor.Text;
                    PrecoVendaOutros.Text = PrecoVendaConsumidor.Text;

                    CalculateValues();
                }
                else
                {
                    if (PrecoVendaConsumidor.Text != "" && Custo.Text != "")
                        CalculateValues();
                }
            }
        }

        private void PrecoRevenda_Leave(object sender, EventArgs e)
        {
            if (PrecoRevenda.Text != "")
            {
                string StringNumbers = PrecoRevenda.Text;

                decimal ConvertToDecimal;

                bool ConvertBool = decimal.TryParse(StringNumbers, NumberStyles.Currency,
                CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal);

                VendaEmReais = ConvertToDecimal;

                if (PrecoRevenda.Text.Contains("R") && PrecoRevenda.Text.Contains("$"))
                    PrecoRevenda.Text = PrecoRevenda.Text;
                else
                {
                    PrecoRevenda.Text = PrecoRevenda.Text.Insert(0, "R");
                    PrecoRevenda.Text = PrecoRevenda.Text.Insert(1, "$");
                    PrecoRevenda.Text = PrecoRevenda.Text.Insert(2, " ");
                }

                if (AutoCompleteValues)
                {
                    PrecoVendaConsumidor.Text = PrecoRevenda.Text;
                    PrecoVendaOutros.Text = PrecoRevenda.Text;

                    CalculateValues();
                }
                else
                {
                    if (PrecoRevenda.Text != "" && Custo.Text != "")
                        CalculateValues();
                }
            }
        }

        private void PrecoVendaOutros_Leave(object sender, EventArgs e)
        {
            if (PrecoVendaOutros.Text != "")
            {
                string StringNumbers = PrecoVendaOutros.Text;

                decimal ConvertToDecimal;

                bool ConvertBool = decimal.TryParse(StringNumbers, NumberStyles.Currency,
                CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal);

                VendaEmReais = ConvertToDecimal;

                if (PrecoVendaOutros.Text.Contains("R") && PrecoVendaOutros.Text.Contains("$"))
                    PrecoVendaOutros.Text = PrecoVendaOutros.Text;
                else
                {
                    PrecoVendaOutros.Text = PrecoVendaOutros.Text.Insert(0, "R");
                    PrecoVendaOutros.Text = PrecoVendaOutros.Text.Insert(1, "$");
                    PrecoVendaOutros.Text = PrecoVendaOutros.Text.Insert(2, " ");
                }

                if (AutoCompleteValues)
                {
                    PrecoVendaConsumidor.Text = PrecoVendaOutros.Text;
                    PrecoRevenda.Text = PrecoVendaOutros.Text;

                    CalculateValues();
                }
                else
                {
                    if (PrecoVendaOutros.Text != "" && Custo.Text != "")
                        CalculateValues();
                }

                TextoChanged = true;
            }
        }

        private void LucroPorcentagem_Leave(object sender, EventArgs e)
        {
            if (LucroPorcentagem.Text != "")
            {
                if (Custo.Text != "")
                {
                    string TextNumbers = Regex.Replace(LucroPorcentagem.Text, "[^0-9]", string.Empty);

                    decimal ConvertToDecimal;

                    bool ConvertBool = decimal.TryParse(TextNumbers, NumberStyles.Currency,
                    CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal);

                    LucroEmPorcentagem = ConvertToDecimal;

                    var Price = ((decimal)LucroEmPorcentagem * CustoEmReais) / 50;

                    VendaEmReais = Price;

                    if (AutoCompleteValues)
                    {
                        PrecoVendaConsumidor.Text = Convert.ToString(VendaEmReais);
                        PrecoRevenda.Text = Convert.ToString(VendaEmReais);
                        PrecoVendaOutros.Text = Convert.ToString(VendaEmReais);

                        string TextNumbers2 = Regex.Replace(PrecoVendaConsumidor.Text, "[^0-9]", string.Empty);

                        if (PrecoVendaConsumidor.Text != "")
                        {
                            PrecoVendaConsumidor.Text = string.Format("{0:#,##0.00}", Double.Parse(TextNumbers2) / 100);

                            if (PrecoVendaConsumidor.Text.Contains("R") && PrecoVendaConsumidor.Text.Contains("$"))
                            {
                                PrecoVendaConsumidor.Text = PrecoVendaConsumidor.Text;
                                PrecoRevenda.Text = PrecoVendaConsumidor.Text;
                                PrecoVendaOutros.Text = PrecoVendaConsumidor.Text;
                            }
                            else
                            {
                                PrecoVendaConsumidor.Text = string.Format("{0:#,##0.00}", Double.Parse(TextNumbers2) / 100);
                                PrecoVendaConsumidor.Text = Convert.ToString("R$ " + PrecoVendaConsumidor.Text);
                                PrecoVendaConsumidor.SelectionStart = PrecoVendaConsumidor.Text.Length;

                                PrecoRevenda.Text = string.Format("{0:#,##0.00}", Double.Parse(TextNumbers2) / 100);
                                PrecoRevenda.Text = Convert.ToString(PrecoVendaConsumidor.Text);
                                PrecoRevenda.SelectionStart = PrecoRevenda.Text.Length;

                                PrecoVendaOutros.Text = string.Format("{0:#,##0.00}", Double.Parse(TextNumbers2) / 100);
                                PrecoVendaOutros.Text = Convert.ToString(PrecoVendaConsumidor.Text);
                                PrecoVendaOutros.SelectionStart = PrecoVendaOutros.Text.Length;
                            }
                        }
                    }
                    else
                    {
                        PrecoVendaConsumidor.Text = Convert.ToString(VendaEmReais);

                        if (PrecoVendaConsumidor.Text.Contains("R") && PrecoVendaConsumidor.Text.Contains("$"))
                            PrecoVendaConsumidor.Text = PrecoVendaConsumidor.Text;
                        else
                        {
                            PrecoVendaConsumidor.Text = string.Format("{0:#,##0.00}", Double.Parse(PrecoVendaConsumidor.Text) / 100);
                            PrecoVendaConsumidor.Text = Convert.ToString("R$ " + PrecoVendaConsumidor.Text);
                            PrecoVendaConsumidor.SelectionStart = PrecoVendaConsumidor.Text.Length;
                        }
                    }

                    if (LucroPorcentagem.Text.Contains("%"))
                        LucroPorcentagem.Text = LucroPorcentagem.Text;
                    else
                        LucroPorcentagem.Text = Convert.ToString(LucroPorcentagem.Text + "%");

                    // Lucro
                    decimal Lucro = VendaEmReais - CustoEmReais;

                    LucroEmReais = Lucro;

                    LucroReais.Text = Convert.ToString(LucroEmReais);

                    string TextNumbers3 = Regex.Replace(LucroReais.Text, "[^0-9]", string.Empty);

                    LucroReais.Text = string.Format("{0:#,##0.00}", Double.Parse(TextNumbers3) / 100);
                    LucroReais.Text = Convert.ToString("R$ " + LucroEmReais);
                    LucroReais.SelectionStart = LucroReais.Text.Length;
                }

                TextoChanged = true;
            }
        }
    }
}
