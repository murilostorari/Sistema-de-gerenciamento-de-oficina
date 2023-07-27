using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCC.Frames.Estoque
{
    public partial class EditProduto : Form
    {
        FormCollection fc = Application.OpenForms;

        Success SuccessForm = new Success();
        Erro ErrorForm = new Erro();

        bool FormLoaded;
        bool CloseOpen;
        bool BackspacePressed;
        bool TextoChanged;

        decimal CustoEmReais;
        decimal VendaEmReais;
        decimal LucroEmReais;
        decimal LucroEmPorcentagem;
        decimal ValorEmDolar;

        byte[] ProdutoFoto;

        string CurvaText, CodigoText, ProdutoText, NFabricanteText, NOriginalText, MarcaText, GrupoText, SubgrupoText, TipoText, UnidadeText, DisponivelText, MinimaText,
            IdealText, CustoText, VendaConsumidorText, RevendaText, VendaOutrosText, CustoDolarText, LucroDolarText, LucroPorcentoText, LucroReaisText, UltimaVendaText,
            LocalizacaoText, PrateleiraText, ObservacoesText, Ean13TextValue, Code128TextValue, FornecedorText, StatusText;

        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;
        bool AutoCompleteValues = Properties.Settings.Default.AutoCompleteCurrencyValues;

        List<Guna.UI2.WinForms.Guna2TextBox> GunaTextBox;
        List<Guna.UI2.WinForms.Guna2ComboBox> GunaComboBox;
        List<Guna.UI2.WinForms.Guna2Separator> GunaSeparators;

        List<Label> NormalLabels;

        public EditProduto(string Curva, string Codigo, string Produto, string NFabricante, string NOriginal, string Marca, string Grupo, string Subgrupo, string Tipo, string Unidade, string Disponivel, string Minima,
                    string Ideal, string Custo, string VendaConsumidor, string Revenda, string VendaOutros, string CustoDolar, string LucroDolar, string LucroPorcento, string LucroReais, string UltimaVenda,
                    string Localizacao, string Prateleira, string Observacoes, string Ean13Text, string Code128Text, string Fornecedor, string Status, byte[] Foto)
        {
            InitializeComponent();
            AddControlsToList();
            SetColor();

            CurvaText = Curva;
            CodigoText = Codigo;
            ProdutoText = Produto;
            NFabricanteText = NFabricante;
            NOriginalText = NOriginal;
            MarcaText = Marca;
            GrupoText = Grupo;
            SubgrupoText = Subgrupo;
            TipoText = Tipo;
            UnidadeText = Unidade;
            DisponivelText = Disponivel;
            MinimaText = Minima;
            IdealText = Ideal;
            CustoText = Custo;
            VendaConsumidorText = VendaConsumidor;
            RevendaText = Revenda;
            VendaOutrosText = VendaOutros;
            CustoDolarText = CustoDolar;
            LucroDolarText = LucroDolar;
            LucroPorcentoText = LucroPorcento;
            LucroReaisText = LucroReais;
            UltimaVendaText = UltimaVenda;
            LocalizacaoText = Localizacao;
            PrateleiraText = Prateleira;
            ObservacoesText = Observacoes;
            Ean13TextValue = Ean13Text;
            Code128TextValue = Code128Text;
            FornecedorText = Fornecedor;
            StatusText = Status;
            ProdutoFoto = Foto;
        }

        private void EditProduto_Load(object sender, EventArgs e)
        {
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

            Curva.Text = CurvaText;
            Codigo.Text = CodigoText;
            CodigoInfo.Text = CodigoText;
            Produto.Text = ProdutoText;
            ProdutoName.Text = ProdutoText;
            NumeroFabricante.Text = NFabricanteText;
            NumeroOriginal.Text = NOriginalText;
            Fabricante.Text = MarcaText;
            Grupo.Text = GrupoText;
            SubGrupo.Text = SubgrupoText;
            TipoProduto.Text = TipoText;
            Unidade.Text = UnidadeText;
            Disponivel.Text = DisponivelText;
            Minima.Text = MinimaText;
            Ideal.Text = IdealText;
            CustoProduto.Text = CustoText;
            VendaConsumidor.Text = VendaConsumidorText;
            Revenda.Text = RevendaText;
            VendaOutros.Text = VendaOutrosText;
            CustoDolar.Text = CustoDolarText;
            LucroDolar.Text = LucroDolarText;
            LucroPorcento.Text = LucroPorcentoText;
            LucroReais.Text = LucroReaisText;
            UltimaVenda.Value = Convert.ToDateTime(UltimaVendaText);
            Localizacao.Text = LocalizacaoText;
            Prateleira.Text = PrateleiraText;
            Observaçoes.Text = ObservacoesText;
            EAN13Insert.Text = Ean13TextValue;
            CODE128Insert.Text = Code128TextValue;
            Fornecedor.Text = FornecedorText;
            Status.Text = StatusText;

            // Foto
            byte[] content = ProdutoFoto;
            MemoryStream stream = new MemoryStream(content);
            ProductPicture.Image = Image.FromStream(stream);

            UltimaVenda.Value = Convert.ToDateTime(UltimaVenda.Value.ToLongDateString());

            if (IsDarkModeEnabled)
                Status.Image = Properties.Resources.status_branco;
            else
                Status.Image = Properties.Resources.status_preto;

            string StringNumbers = VendaConsumidor.Text;
            string StringNumbers2 = CustoProduto.Text;

            decimal ConvertToDecimal;
            decimal ConvertToDecimal2;

            bool ConvertBool = decimal.TryParse(StringNumbers, NumberStyles.Currency,
            CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal);

            bool ConvertBool2 = decimal.TryParse(StringNumbers2, NumberStyles.Currency,
            CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal2);

            VendaEmReais = ConvertToDecimal;
            CustoEmReais = ConvertToDecimal2;

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
                        Frames.CloseConfirm.CloseFrame.TopText.Text = "Descartar alterações";
                        Frames.CloseConfirm.CloseFrame.LblText.Text = "Você deseja mesmo descartar as alterações feitas?";
                    }
                }

                if (frm.Name == "EditProduto")
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
            BackspacePressed = false;
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

            var ConvertoToPercentage = ((decimal)VendaEmReais / CustoEmReais) * 50;

            LucroEmPorcentagem = Convert.ToInt32(Math.Round(ConvertoToPercentage, 0));
            LucroPorcento.Text = Convert.ToString(LucroEmPorcentagem + "%");

            if (VendaEmReais < CustoEmReais)
                LucroPorcento.Text = Convert.ToString("-" + LucroEmPorcentagem + "%");

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

                        if (VendaConsumidor.Text != "" || Revenda.Text != "" || VendaOutros.Text != "")
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

        private void SaveChanges()
        {
            EditProduto EditForm = (EditProduto)Application.OpenForms["EditProduto"];

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

            OleDbConnection con = new OleDbConnection(strcon);
            OleDbCommand cmd = con.CreateCommand();

            // Atualizar status
            OleDbConnection con2 = new OleDbConnection(strcon);
            OleDbCommand cmd2 = con2.CreateCommand();

            string Barcode, Check12Digits;

            Check12Digits = Codigo.Text.PadLeft(12, '0');

            Barcode = EAN13Class.EAN13(Check12Digits);

            EAN13Insert.Text = Barcode;
            CODE128Insert.Text = Codigo.Text;

            cmd.CommandType = CommandType.Text;
            cmd.CommandText =

                // Dados pessoais
                "UPDATE Estoque SET CODIGO = @CODIGO, PRODUTO = @PRODUTO, NUMEROFABRICANTE = @NUMEROFABRICANTE, NUMEROORIGINAL = @NUMEROORIGINAL, MARCA = @MARCA, GRUPO = @GRUPO, SUBGRUPO = @SUBGRUPO," +
                "TIPO = @TIPO, UNIDADE = @UNIDADE, QNTDDISPONIVEL = @QNTDDISPONIVEL, QNTDMINIMA = @QNTDMINIMA, QNTDIDEAL = @QNTDIDEAL, CUSTOCOMPRA = @CUSTOCOMPRA, VALORVENDACONSUMIDOR = @VALORVENDACONSUMIDOR, VALORREVENDA = @VALORREVENDA," +
                "VALORVENDAOUTROS = @VALORVENDAOUTROS, CUSTOEMDOLAR = @CUSTOEMDOLAR, LUCROEMDOLAR = @LUCROEMDOLAR, LUCROPORCENTAGEM = @LUCROPORCENTAGEM, LUCROREAIS = @LUCROREAIS, ULTIMAVENDA = @ULTIMAVENDA, FORNECEDOR = @FORNECEDOR, LOCALIZACAOPRODUTO = @LOCALIZACAOPRODUTO," +
                "PRATELEIRA = @PRATELEIRA, OBSERVACOES = @OBSERVACOES, CURVA = @CURVA, EAN13BARCODETEXT = @EAN13BARCODETEXT, CODE128BARCODETEXT = @CODE128BARCODETEXT, STATUS = @STATUS, FOTO = @FOTO WHERE PRODUTO = @PRODUTOTEST";

            cmd.Parameters.Add("@CODIGO", OleDbType.VarChar).Value = Codigo.Text;
            cmd.Parameters.Add("@PRODUTO", OleDbType.VarChar).Value = Produto.Text;
            cmd.Parameters.Add("@NUMEROFABRICANTE", OleDbType.VarChar).Value = NumeroFabricante.Text;
            cmd.Parameters.Add("@NUMEROORIGINAL", OleDbType.VarChar).Value = NumeroOriginal.Text;
            cmd.Parameters.Add("@MARCA", OleDbType.VarChar).Value = Fabricante.Text;

            if (Grupo.SelectedIndex == -1)
                cmd.Parameters.Add("@GRUPO", OleDbType.VarChar).Value = "Indiferente";
            else
                cmd.Parameters.Add("@GRUPO", OleDbType.VarChar).Value = Grupo.Text;

            if (SubGrupo.SelectedIndex == -1)
                cmd.Parameters.Add("@SUBGRUPO", OleDbType.VarChar).Value = "Indiferente";
            else
                cmd.Parameters.Add("@SUBGRUPO", OleDbType.VarChar).Value = SubGrupo.Text;

            cmd.Parameters.Add("@TIPO", OleDbType.VarChar).Value = TipoProduto.Text;

            if (Unidade.SelectedIndex == -1)
                cmd.Parameters.Add("@UNIDADE", OleDbType.VarChar).Value = "Indiferente";
            else
                cmd.Parameters.Add("@UNIDADE", OleDbType.VarChar).Value = Unidade.Text;

            cmd.Parameters.Add("@QNTDDISPONIVEL", OleDbType.Numeric).Value = Convert.ToInt32(Disponivel.Text);
            cmd.Parameters.Add("@QNTDMINIMA", OleDbType.Numeric).Value = Convert.ToInt32(Minima.Text);
            cmd.Parameters.Add("@QNTDIDEAL", OleDbType.Numeric).Value = Convert.ToInt32(Ideal.Text);
            cmd.Parameters.Add("@CUSTOCOMPRA", OleDbType.VarChar).Value = CustoProduto.Text;
            cmd.Parameters.Add("@VALORVENDACONSUMIDOR", OleDbType.VarChar).Value = VendaConsumidor.Text;
            cmd.Parameters.Add("@VALORREVENDA", OleDbType.VarChar).Value = Revenda.Text;
            cmd.Parameters.Add("@VALORVENDAOUTROS", OleDbType.VarChar).Value = VendaOutros.Text;
            cmd.Parameters.Add("@CUSTOEMDOLAR", OleDbType.VarChar).Value = CustoDolar.Text;
            cmd.Parameters.Add("@LUCROEMDOLAR", OleDbType.VarChar).Value = LucroDolar.Text;
            cmd.Parameters.Add("@LUCROPORCENTAGEM", OleDbType.VarChar).Value = LucroPorcento.Text;
            cmd.Parameters.Add("@LUCROREAIS", OleDbType.VarChar).Value = LucroReais.Text;
            cmd.Parameters.Add("@ULTIMAVENDA", OleDbType.Date).Value = UltimaVenda.Value;
            cmd.Parameters.Add("@FORNECEDOR", OleDbType.VarChar).Value = Fornecedor.Text;
            cmd.Parameters.Add("@LOCALIZACAOPRODUTO", OleDbType.VarChar).Value = Localizacao.Text;
            cmd.Parameters.Add("@PRATELEIRA", OleDbType.VarChar).Value = Prateleira.Text;
            cmd.Parameters.Add("@OBSERVACOES", OleDbType.VarChar).Value = Observaçoes.Text;
            cmd.Parameters.Add("@CURVA", OleDbType.VarChar).Value = Curva.Text;
            cmd.Parameters.Add("@EAN13BARCODETEXT", OleDbType.VarChar).Value = EAN13Insert.Text;
            cmd.Parameters.Add("@CODE128BARCODETEXT", OleDbType.VarChar).Value = CodigoCODE128.Text;
            cmd.Parameters.Add("@STATUS", OleDbType.VarChar).Value = Status.Text;

            if (ProductPicture.Image != null)
            {
                Bitmap BitmapImg = new Bitmap(ProductPicture.Image);
                byte[] picture = ImageToByte(BitmapImg, System.Drawing.Imaging.ImageFormat.Jpeg);
                cmd.Parameters.Add("@FOTO", OleDbType.Binary).Value = picture;
            }

            cmd.Parameters.Add("@PRODUTOTEST", OleDbType.VarChar).Value = ProdutoName.Text;

            // Atualizar status
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "UPDATE Estoque SET STATUS = '" + Status.Text + "' WHERE CODIGO = @CODIGO";

            cmd2.Parameters.Add("@CODIGO", OleDbType.VarChar).Value = Codigo.Text;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

                con2.Open();
                cmd2.ExecuteNonQuery();

                EditForm.Opacity = .0d;

                Success.SuccessFrame.LblText.Text = "Alterações salvas com sucesso!";

                SuccessForm.Show();
            }
            catch (Exception exc)
            {
                EditForm.Opacity = .0d;

                Erro.ErrorFrame.LblText.Text = "Erro ao salvar alterações!";

                ErrorForm.Show();

                MessageBox.Show(exc.ToString());
            }
            finally
            {
                con.Close();
                con2.Close();
            }
        }

        // Adicionar itens a lista pra poder usar o dark/light mode
        private void AddControlsToList()
        {
            GunaTextBox = new List<Guna.UI2.WinForms.Guna2TextBox>();
            GunaComboBox = new List<Guna.UI2.WinForms.Guna2ComboBox>();
            GunaSeparators = new List<Guna.UI2.WinForms.Guna2Separator>();

            NormalLabels = new List<Label>();

            //------------------//

            // Labelss
            Label[] Labels = new Label[26]
            {
                label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13,
                label14, label15, label16, label17, label18, label19, label20, label21, label22, label23, label24, label25, 
                label26
            };

            //------------------//

            // Textbox
            Guna.UI2.WinForms.Guna2TextBox[] TextBox = new Guna.UI2.WinForms.Guna2TextBox[19]
            {
                Produto, Codigo, NumeroFabricante, NumeroOriginal, Fabricante, CustoProduto, VendaConsumidor, Revenda, VendaOutros,
                CustoDolar, LucroDolar, LucroReais, LucroPorcento, Disponivel, Ideal, Minima, Localizacao, Prateleira, Observaçoes
            };

            // Combobox
            Guna.UI2.WinForms.Guna2ComboBox[] Combobox = new Guna.UI2.WinForms.Guna2ComboBox[6]
            {
                Unidade, TipoProduto, Grupo, SubGrupo, Fornecedor, Curva
            };

            //------------------//

            // Separators
            Guna.UI2.WinForms.Guna2Separator[] Separadores = new Guna.UI2.WinForms.Guna2Separator[3]
            {
                Separator, guna2Separator1, guna2Separator2
            };

            //------------------//

            NormalLabels.AddRange(Labels);
            GunaTextBox.AddRange(TextBox);
            GunaComboBox.AddRange(Combobox);
            GunaSeparators.AddRange(Separadores);
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

            ProdutoName.ForeColor = ThemeManager.WhiteFontColor;
            CodigoInfo.ForeColor = ThemeManager.FontColor;
            Status.ForeColor = ThemeManager.FontColor;

            UltimaVenda.BackColor = ThemeManager.FormBackColor;
            UltimaVenda.FillColor = ThemeManager.FormBackColor;
            UltimaVenda.ForeColor = ThemeManager.WhiteFontColor;
            UltimaVenda.BorderColor = ThemeManager.SeparatorAndBorderColor;
            UltimaVenda.HoverState.BorderColor = ThemeManager.DateTimePickerHoverBorderColor;
            UltimaVenda.CheckedState.BorderColor = ThemeManager.SeparatorAndBorderColor;
            UltimaVenda.CheckedState.FillColor = ThemeManager.FormBackColor;

            DadosBtn.FillColor = ThemeManager.FormBackColor;
            DadosBtn.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            DadosBtn.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;

            ValoresBtn.FillColor = ThemeManager.FormBackColor;
            ValoresBtn.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            ValoresBtn.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;

            QuantidadesBtn.FillColor = ThemeManager.FormBackColor;
            QuantidadesBtn.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            QuantidadesBtn.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;

            OutrosBtn.FillColor = ThemeManager.FormBackColor;
            OutrosBtn.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            OutrosBtn.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;

            ProductPicture.BackColor = ThemeManager.FormBackColor;

            ChoosePicture.BorderColor = ThemeManager.ChoosePictureBorderColor;
            DeletarFoto.BorderColor = ThemeManager.ChoosePictureBorderColor;

            Cancelar.FillColor = ThemeManager.BorderRedButtonFillColor;
            Cancelar.ForeColor = ThemeManager.BorderRedButtonForeColor;
            Cancelar.BorderColor = ThemeManager.BorderRedButtonBorderColor;
            Cancelar.HoverState.BorderColor = ThemeManager.BorderRedButtonHoverBorderColor;
            Cancelar.HoverState.FillColor = ThemeManager.BorderRedButtonHoverFillColor;
            Cancelar.HoverState.ForeColor = ThemeManager.BorderRedButtonHoverForeColor;
            Cancelar.PressedColor = ThemeManager.BorderRedButtonPressedColor;

            ToolTip.ForeColor = ThemeManager.GunaToolTipForeColor;
            ToolTip.BorderColor = ThemeManager.GunaToolTipBorderColor;
            ToolTip.BackColor = ThemeManager.GunaToolTipBackColor;

            // Labels normais
            foreach (Label ct in NormalLabels)
            {
                ct.ForeColor = ThemeManager.DarkGrayLabelsFontColor;
                ct.BackColor = ThemeManager.FormBackColor;
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

            // Separators
            foreach (Guna.UI2.WinForms.Guna2Separator ct in GunaSeparators)
            {
                ct.FillColor = ThemeManager.SeparatorAndBorderColor;
            }
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Outros */

        // Botoes
        private void DadosBtn_Click(object sender, EventArgs e)
        {
            MovingBar.Size = new Size(156, 3);
            MovingBar.Location = new Point(11, 209);

            if (IsDarkModeEnabled)
            {
                DadosBtn.ForeColor = Color.FromArgb(255, 33, 0);
                ValoresBtn.ForeColor = Color.FromArgb(180, 180, 180);
                QuantidadesBtn.ForeColor = Color.FromArgb(180, 180, 180);
                OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);
            }

            else
            {
                DadosBtn.ForeColor = Color.FromArgb(255, 3, 0);
                ValoresBtn.ForeColor = Color.FromArgb(180, 180, 180);
                QuantidadesBtn.ForeColor = Color.FromArgb(180, 180, 180);
                OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);
            }

            InformacoesGerais.Location = new Point(InformacoesGerais.Location.X, 232);
            ValoresPanel.Location = new Point(ValoresPanel.Location.X, 7732);
            QuantidadesPanel.Location = new Point(QuantidadesPanel.Location.X, 7732);
            OutrosPanel.Location = new Point(OutrosPanel.Location.X, 7732);

            foreach (Control ct in InformacoesGerais.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in ValoresPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in QuantidadesPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in OutrosPanel.Controls)
            {
                ct.TabStop = false;
            }
        }

        private void ValoresBtn_Click(object sender, EventArgs e)
        {
            MovingBar.Size = new Size(136, 3);
            MovingBar.Location = new Point(198, 209);

            if (IsDarkModeEnabled)
            {
                DadosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                ValoresBtn.ForeColor = Color.FromArgb(255, 33, 0);
                QuantidadesBtn.ForeColor = Color.FromArgb(180, 180, 180);
                OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);
            }

            else
            {
                DadosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                ValoresBtn.ForeColor = Color.FromArgb(255, 3, 0);
                QuantidadesBtn.ForeColor = Color.FromArgb(180, 180, 180);
                OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);
            }

            InformacoesGerais.Location = new Point(InformacoesGerais.Location.X, 7732);
            ValoresPanel.Location = new Point(ValoresPanel.Location.X, 232);
            QuantidadesPanel.Location = new Point(QuantidadesPanel.Location.X, 7732);
            OutrosPanel.Location = new Point(OutrosPanel.Location.X, 7732);

            foreach (Control ct in InformacoesGerais.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in ValoresPanel.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in QuantidadesPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in OutrosPanel.Controls)
            {
                ct.TabStop = false;
            }
        }

        private void QuantidadesBtn_Click(object sender, EventArgs e)
        {
            MovingBar.Size = new Size(116, 3);
            MovingBar.Location = new Point(365, 209);

            if (IsDarkModeEnabled)
            {
                DadosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                ValoresBtn.ForeColor = Color.FromArgb(180, 180, 180);
                QuantidadesBtn.ForeColor = Color.FromArgb(255, 33, 0);
                OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);
            }

            else
            {
                DadosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                ValoresBtn.ForeColor = Color.FromArgb(180, 180, 180);
                QuantidadesBtn.ForeColor = Color.FromArgb(255, 3, 0);
                OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);
            }

            InformacoesGerais.Location = new Point(InformacoesGerais.Location.X, 7732);
            ValoresPanel.Location = new Point(ValoresPanel.Location.X, 7732);
            QuantidadesPanel.Location = new Point(QuantidadesPanel.Location.X, 232);
            OutrosPanel.Location = new Point(OutrosPanel.Location.X, 7732);

            foreach (Control ct in InformacoesGerais.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in ValoresPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in QuantidadesPanel.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in OutrosPanel.Controls)
            {
                ct.TabStop = false;
            }
        }

        private void OutrosBtn_Click(object sender, EventArgs e)
        {
            MovingBar.Size = new Size(116, 3);
            MovingBar.Location = new Point(510, 209);

            if (IsDarkModeEnabled)
            {
                DadosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                ValoresBtn.ForeColor = Color.FromArgb(180, 180, 180);
                QuantidadesBtn.ForeColor = Color.FromArgb(180, 180, 180);
                OutrosBtn.ForeColor = Color.FromArgb(255, 33, 0);
            }

            else
            {
                DadosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                ValoresBtn.ForeColor = Color.FromArgb(180, 180, 180);
                QuantidadesBtn.ForeColor = Color.FromArgb(180, 180, 180);
                OutrosBtn.ForeColor = Color.FromArgb(255, 3, 0);
            }

            InformacoesGerais.Location = new Point(InformacoesGerais.Location.X, 7732);
            ValoresPanel.Location = new Point(ValoresPanel.Location.X, 7732);
            QuantidadesPanel.Location = new Point(QuantidadesPanel.Location.X, 7732);
            OutrosPanel.Location = new Point(OutrosPanel.Location.X, 232);

            foreach (Control ct in InformacoesGerais.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in ValoresPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in QuantidadesPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in OutrosPanel.Controls)
            {
                ct.TabStop = true;
            }
        }


        // Escolher foto
        private void ChoosePicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog PictureDialog = new OpenFileDialog();
            PictureDialog.Title = "Selecione uma foto";
            PictureDialog.Filter = "Arquivo JPG|*.jpg|Arquivo JPEG|*.jpeg|Arquivo PNG|*.png";

            if (PictureDialog.ShowDialog() == DialogResult.OK)
            {
                ProductPicture.Image = new Bitmap(PictureDialog.FileName);
                TextoChanged = true;
            }
        }

        // Remover foto
        private void DeletarFoto_Click(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                ProductPicture.Image = Properties.Resources.DefaultAvatar;
                TextoChanged = true;
            }
        }

        // Salvar alteraçoes
        private void SalvarAlteracoes_Click(object sender, EventArgs e)
        {
            Alerta AlertForm = new Alerta();

            if (Produto.Text != "" && Codigo.Text != "" && NumeroFabricante.Text != "" && TipoProduto.SelectedIndex != -1)
            {
                if (CustoProduto.Text != "" && VendaConsumidor.Text != "" && Revenda.Text != "" && VendaOutros.Text != "")
                {
                    if (Disponivel.Text != "" && Ideal.Text != "" && Minima.Text != "" && Curva.SelectedIndex != -1 && Fornecedor.Text != "")
                    {
                        if (VendaEmReais > CustoEmReais)
                            SaveChanges();
                        else
                        {
                            foreach (Form frm in fc)
                            {
                                if (frm.Name == "EditProduto")
                                    frm.Opacity = .0d;
                            }

                            Alerta.AlertaFrame.LblText.Text = "O valor do custo é maior que o valor do lucro!";

                            AlertForm.Show();
                        }
                    }
                }
            }
        }

        // Cancelar
        private async void Cancelar_Click(object sender, EventArgs e)
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

        // Fechar form
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

        // Mudar foto pra alguma foto padrao
        private void ProductPicture_Click(object sender, EventArgs e)
        {
            if (TipoProduto.SelectedIndex == 0)
            {
                string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsFuncionario\\produto", "*.png");
                List<string> Icones = Pasta.ToList();
                Random RandomIcon = new Random();
                ProductPicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
            }
            else
            {
                string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsFuncionario\\kit", "*.png");
                List<string> Icones = Pasta.ToList();
                Random RandomIcon = new Random();
                ProductPicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
            }

            if (FormLoaded)
                TextoChanged = true;
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Text changed */

        private void Produto_TextChanged(object sender, EventArgs e)
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
                    CustoProduto.BorderColor = Color.FromArgb(80, 80, 80);
                    CustoProduto.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                    CustoProduto.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
                else
                {
                    CustoProduto.BorderColor = Color.FromArgb(210, 210, 210);
                    CustoProduto.FocusedState.BorderColor = Color.Black;
                    CustoProduto.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        private void VendaConsumidor_TextChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                VendaConsumidorHint.Visible = false;

                if (IsDarkModeEnabled)
                {
                    VendaConsumidor.BorderColor = Color.FromArgb(80, 80, 80);
                    VendaConsumidor.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                    VendaConsumidor.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
                else
                {
                    VendaConsumidor.BorderColor = Color.FromArgb(210, 210, 210);
                    VendaConsumidor.FocusedState.BorderColor = Color.Black;
                    VendaConsumidor.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        private void Revenda_TextChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                RevendaHint.Visible = false;

                if (IsDarkModeEnabled)
                {
                    Revenda.BorderColor = Color.FromArgb(80, 80, 80);
                    Revenda.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                    Revenda.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
                else
                {
                    Revenda.BorderColor = Color.FromArgb(210, 210, 210);
                    Revenda.FocusedState.BorderColor = Color.Black;
                    Revenda.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        private void VendaOutros_TextChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                VendaOutrosHint.Visible = false;

                if (IsDarkModeEnabled)
                {
                    VendaOutros.BorderColor = Color.FromArgb(80, 80, 80);
                    VendaOutros.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                    VendaOutros.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
                else
                {
                    VendaOutros.BorderColor = Color.FromArgb(210, 210, 210);
                    VendaOutros.FocusedState.BorderColor = Color.Black;
                    VendaOutros.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
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

            TextoChanged = true;
        }

        private void Disponivel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }

            TextoChanged = true;
        }

        private void Ideal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }

            TextoChanged = true;
        }

        private void Minima_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }

            TextoChanged = true;
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
            if (CustoProduto.Text != "")
            {
                string StringNumbers = CustoProduto.Text;

                decimal ConvertToDecimal;

                bool ConvertBool = decimal.TryParse(StringNumbers, NumberStyles.Currency,
                CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal);

                CustoEmReais = ConvertToDecimal;

                if (CustoProduto.Text.Contains("R") && CustoProduto.Text.Contains("$"))
                    CustoProduto.Text = CustoProduto.Text;
                else
                {
                    CustoProduto.Text = CustoProduto.Text.Insert(0, "R");
                    CustoProduto.Text = CustoProduto.Text.Insert(1, "$");
                    CustoProduto.Text = CustoProduto.Text.Insert(2, " ");
                }

                if (AutoCompleteValues)
                {
                    if (VendaConsumidor.Text != "" && Revenda.Text != "" && VendaOutros.Text != "")
                        CalculateValues();
                }
                else
                {
                    if (VendaConsumidor.Text != "" || Revenda.Text != "" || VendaOutros.Text != "")
                        CalculateValues();
                }
            }
        }

        private void VendaConsumidor_Leave(object sender, EventArgs e)
        {
            if (VendaConsumidor.Text != "")
            {
                string StringNumbers = VendaConsumidor.Text;

                decimal ConvertToDecimal;

                bool ConvertBool = decimal.TryParse(StringNumbers, NumberStyles.Currency,
                CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal);

                VendaEmReais = ConvertToDecimal;

                if (VendaConsumidor.Text.Contains("R") && VendaConsumidor.Text.Contains("$"))
                    VendaConsumidor.Text = VendaConsumidor.Text;
                else
                {
                    VendaConsumidor.Text = VendaConsumidor.Text.Insert(0, "R");
                    VendaConsumidor.Text = VendaConsumidor.Text.Insert(1, "$");
                    VendaConsumidor.Text = VendaConsumidor.Text.Insert(2, " ");
                }

                if (AutoCompleteValues)
                {
                    Revenda.Text = VendaConsumidor.Text;
                    VendaOutros.Text = VendaConsumidor.Text;

                    CalculateValues();
                }
                else
                {
                    if (VendaConsumidor.Text != "" && CustoProduto.Text != "")
                        CalculateValues();
                }
            }
        }

        private void Revenda_Leave(object sender, EventArgs e)
        {
            if (Revenda.Text != "")
            {
                string StringNumbers = Revenda.Text;

                decimal ConvertToDecimal;

                bool ConvertBool = decimal.TryParse(StringNumbers, NumberStyles.Currency,
                CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal);

                VendaEmReais = ConvertToDecimal;

                if (Revenda.Text.Contains("R") && Revenda.Text.Contains("$"))
                    Revenda.Text = Revenda.Text;
                else
                {
                    Revenda.Text = Revenda.Text.Insert(0, "R");
                    Revenda.Text = Revenda.Text.Insert(1, "$");
                    Revenda.Text = Revenda.Text.Insert(2, " ");
                }

                if (AutoCompleteValues)
                {
                    VendaConsumidor.Text = Revenda.Text;
                    VendaOutros.Text = Revenda.Text;

                    CalculateValues();
                }
                else
                {
                    if (Revenda.Text != "" && CustoProduto.Text != "")
                        CalculateValues();
                }
            }
        }

        private void VendaOutros_Leave(object sender, EventArgs e)
        {
            if (VendaOutros.Text != "")
            {
                string StringNumbers = VendaOutros.Text;

                decimal ConvertToDecimal;

                bool ConvertBool = decimal.TryParse(StringNumbers, NumberStyles.Currency,
                CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal);

                VendaEmReais = ConvertToDecimal;

                if (VendaOutros.Text.Contains("R") && VendaOutros.Text.Contains("$"))
                    VendaOutros.Text = VendaOutros.Text;
                else
                {
                    VendaOutros.Text = VendaOutros.Text.Insert(0, "R");
                    VendaOutros.Text = VendaOutros.Text.Insert(1, "$");
                    VendaOutros.Text = VendaOutros.Text.Insert(2, " ");
                }

                if (AutoCompleteValues)
                {
                    VendaConsumidor.Text = VendaOutros.Text;
                    Revenda.Text = VendaOutros.Text;

                    CalculateValues();
                }
                else
                {
                    if (VendaOutros.Text != "" && CustoProduto.Text != "")
                        CalculateValues();
                }
            }
        }

        private void LucroPorcento_Leave(object sender, EventArgs e)
        {
            if (LucroPorcento.Text != "")
            {
                if (CustoProduto.Text != "")
                {
                    string TextNumbers = Regex.Replace(LucroPorcento.Text, "[^0-9]", string.Empty);

                    decimal ConvertToDecimal;

                    bool ConvertBool = decimal.TryParse(TextNumbers, NumberStyles.Currency,
                    CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal);

                    LucroEmPorcentagem = ConvertToDecimal;

                    var Price = ((decimal)LucroEmPorcentagem * CustoEmReais) / 50;

                    VendaEmReais = Price;

                    if (AutoCompleteValues)
                    {
                        VendaConsumidor.Text = Convert.ToString(VendaEmReais);
                        Revenda.Text = Convert.ToString(VendaEmReais);
                        VendaOutros.Text = Convert.ToString(VendaEmReais);

                        string TextNumbers2 = Regex.Replace(VendaConsumidor.Text, "[^0-9]", string.Empty);

                        if (VendaConsumidor.Text != "")
                        {
                            VendaConsumidor.Text = string.Format("{0:#,##0.00}", Double.Parse(TextNumbers2) / 100);

                            if (VendaConsumidor.Text.Contains("R") && VendaConsumidor.Text.Contains("$"))
                            {
                                VendaConsumidor.Text = VendaConsumidor.Text;
                                Revenda.Text = VendaConsumidor.Text;
                                VendaOutros.Text = VendaConsumidor.Text;
                            }
                            else
                            {
                                VendaConsumidor.Text = string.Format("{0:#,##0.00}", Double.Parse(TextNumbers2) / 100);
                                VendaConsumidor.Text = Convert.ToString("R$ " + VendaConsumidor.Text);
                                VendaConsumidor.SelectionStart = VendaConsumidor.Text.Length;

                                Revenda.Text = string.Format("{0:#,##0.00}", Double.Parse(TextNumbers2) / 100);
                                Revenda.Text = Convert.ToString(VendaConsumidor.Text);
                                Revenda.SelectionStart = Revenda.Text.Length;

                                VendaOutros.Text = string.Format("{0:#,##0.00}", Double.Parse(TextNumbers2) / 100);
                                VendaOutros.Text = Convert.ToString(VendaConsumidor.Text);
                                VendaOutros.SelectionStart = VendaOutros.Text.Length;
                            }
                        }
                    }
                    else
                    {
                        VendaConsumidor.Text = Convert.ToString(VendaEmReais);

                        if (VendaConsumidor.Text.Contains("R") && VendaConsumidor.Text.Contains("$"))
                            VendaConsumidor.Text = VendaConsumidor.Text;
                        else
                        {
                            VendaConsumidor.Text = string.Format("{0:#,##0.00}", Double.Parse(VendaConsumidor.Text) / 100);
                            VendaConsumidor.Text = Convert.ToString("R$ " + VendaConsumidor.Text);
                            VendaConsumidor.SelectionStart = VendaConsumidor.Text.Length;
                        }
                    }

                    if (LucroPorcento.Text.Contains("%"))
                        LucroPorcento.Text = LucroPorcento.Text;
                    else
                        LucroPorcento.Text = Convert.ToString(LucroPorcento.Text + "%");

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
