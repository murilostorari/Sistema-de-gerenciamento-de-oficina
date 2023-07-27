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
    public partial class EditFuncionario : Form
    {
        FormCollection fc = Application.OpenForms;

        Success SuccessForm = new Success();
        Erro ErrorForm = new Erro();

        bool FormLoaded;
        bool CloseOpen;
        bool BackspacePressed;
        bool TextoChanged;

        int FuncionarioID;
        byte[] FuncionarioFoto;

        string NomeText, TelefoneText, TipoDeNumeroText, CpfText, CTPSText, FuncaoText, GeneroText, EnderecoText, CIText,
                    CidadeText, EntradaText, CnhText, NumeroCatText, EstadoCivilText, BairroText, CepText, EstadoText, ComplementoText,
                    BancoText, AgenciaText, ContaText, TecnicoText, VendedorText, DemitidoText, ObservacoesText;

        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;
        bool AutoCompleteValues = Properties.Settings.Default.AutoCompleteCurrencyValues;

        List<Guna.UI2.WinForms.Guna2TextBox> GunaTextBox;
        List<Guna.UI2.WinForms.Guna2ComboBox> GunaComboBox;
        List<Guna.UI2.WinForms.Guna2Separator> GunaSeparators;

        List<Label> NormalLabels;

        public EditFuncionario(int ID, string Nome, string Telefone, string TipoDeNumero, string Cpf, string CTPS, string Funcao, string Genero, string Endereco, string CI,
                    string Cidade, string Entrada, string Cnh, string NumeroCat, string EstadoCivil, string Bairro, string Cep, string Estado, string Complemento,
                    string Banco, string Agencia, string Conta, string Tecnico, string Vendedor, string Demitido, string Observacoes, byte[] Foto)
        {
            InitializeComponent();
            AddControlsToList();
            SetColor();

            FuncionarioID = ID;
            NomeText = Nome;
            TelefoneText = Telefone;
            TipoDeNumeroText = TipoDeNumero;
            CpfText = Cpf;
            CTPSText = CTPS;
            FuncaoText = Funcao;
            GeneroText = Genero;
            EnderecoText = Endereco;
            CIText = CI;
            CidadeText = Cidade;
            EntradaText = Entrada;
            CnhText = Cnh;
            NumeroCatText = NumeroCat;
            EstadoCivilText = EstadoCivil;
            BairroText = Bairro;
            CepText = Cep;
            EstadoText = Estado;
            ComplementoText = Complemento;
            BancoText = Banco;
            AgenciaText = Agencia;
            ContaText = Conta;
            TecnicoText = Tecnico;
            VendedorText = Vendedor;
            DemitidoText = Demitido;
            ObservacoesText = Observacoes;
            FuncionarioFoto = Foto;
        }

        private void EditProduto_Load(object sender, EventArgs e)
        {
            IDInfo.Text = "ID: " + FuncionarioID;
            IDInt.Text = Convert.ToString(FuncionarioID);
            FuncionarioName.Text = NomeText;
            Nome.Text = NomeText;
            Telefone.Text = TelefoneText;
            CPF.Text = CpfText;
            TipoDeNumero.Text = TipoDeNumeroText;
            Funcao.Text = FuncaoText;
            EstadoCivil.Text = EstadoCivilText;
            Genero.Text = GeneroText;
            CTPS.Text = CTPSText;
            CNH.Text = CnhText;
            Endereço.Text = EnderecoText;
            Bairro.Text = BairroText;
            Cidade.Text = CidadeText;
            CEP.Text = CepText;
            Estado.Text = EstadoText;
            Complemento.Text = ComplementoText;
            Banco.Text = BancoText;
            Agencia.Text = AgenciaText;
            Conta.Text = ContaText;
            Tecnico.Text = TecnicoText;
            Vendedor.Text = VendedorText;
            NumeroCAT.Text = NumeroCatText;
            CITextBox.Text = CIText;
            Observaçoes.Text = ObservacoesText;

            if (DemitidoText == "Sim")
                Demitido.Checked = true;
            else
                Demitido.Checked = false;

            // Foto
            byte[] content = FuncionarioFoto;
            MemoryStream stream = new MemoryStream(content);
            FuncionarioPicture.Image = Image.FromStream(stream);

            if (IsDarkModeEnabled)
                EntradaInfo.Image = Properties.Resources.data_branco;
            else
                EntradaInfo.Image = Properties.Resources.entrada_claro;

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

                if (frm.Name == "EditFuncionario")
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

        private void SaveChanges()
        {
            EditFuncionario EditForm = (EditFuncionario)Application.OpenForms["EditFuncionario"];

            string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";

            OleDbConnection con = new OleDbConnection(strcon);
            OleDbCommand cmd = con.CreateCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText =

                // Dados pessoais
                "UPDATE Funcionarios SET NOME = @NOME, TELEFONE = @TELEFONE, CPFCNPJ = @CPFCNPJ, GENERO = @GENERO, CI = @CI, CARTEIRADETRABALHO = @CARTEIRADETRABALHO, CNH = @CNH, \n" +
                "NUMEROCAT = @NUMEROCAT, ESTADOCIVIL = @ESTADOCIVIL, FUNCAO = @FUNCAO, ENDERECO = @ENDERECO, BAIRRO = @BAIRRO, CIDADE = @CIDADE, CEP = @CEP, ESTADO = @ESTADO, COMPLEMENTO = @COMPLEMENTO, BANCO = @BANCO, AGENCIA = @AGENCIA, CONTADOBANCO = @CONTADOBANCO," +
                "FUNCIONARIODESDE = @FUNCIONARIODESDE, TECNICO = @TECNICO, VENDEDOR = @VENDEDOR, DEMITIDODESLIGADO = @DEMITIDODESLIGADO, OBSERVACOES = @OBSERVACOES, FOTO = @FOTO WHERE ID = " + IDInt.Text;

            cmd.Parameters.Add("@NOME", OleDbType.VarChar).Value = Nome.Text;
            cmd.Parameters.Add("@TELEFONE", OleDbType.VarChar).Value = Telefone.Text;
            cmd.Parameters.Add("@CPFCNPJ", OleDbType.VarChar).Value = CPF.Text;

            if (Genero.SelectedIndex == -1)
                cmd.Parameters.Add("@GENERO", OleDbType.VarChar).Value = "-";
            else
                cmd.Parameters.Add("@GENERO", OleDbType.VarChar).Value = Genero.Text;

            cmd.Parameters.Add("@CI", OleDbType.VarChar).Value = "-";
            cmd.Parameters.Add("@CARTEIRADETRABALHO", OleDbType.VarChar).Value = CTPS.Text;
            cmd.Parameters.Add("@CNH", OleDbType.VarChar).Value = CNH.Text;
            cmd.Parameters.Add("@NUMEROCAT", OleDbType.VarChar).Value = "-";

            if (EstadoCivil.SelectedIndex == -1)
                cmd.Parameters.Add("@ESTADOCIVIL", OleDbType.VarChar).Value = "-";
            else
                cmd.Parameters.Add("@ESTADOCIVIL", OleDbType.VarChar).Value = EstadoCivil.Text;

            cmd.Parameters.Add("@FUNCAO", OleDbType.VarChar).Value = Funcao.Text;
            cmd.Parameters.Add("@ENDERECO", OleDbType.VarChar).Value = Endereço.Text;
            cmd.Parameters.Add("@BAIRRO", OleDbType.VarChar).Value = Bairro.Text;
            cmd.Parameters.Add("@CIDADE", OleDbType.VarChar).Value = Cidade.Text;
            cmd.Parameters.Add("@CEP", OleDbType.VarChar).Value = CEP.Text;
            cmd.Parameters.Add("@ESTADO", OleDbType.VarChar).Value = Estado.Text;
            cmd.Parameters.Add("@COMPLEMENTO", OleDbType.VarChar).Value = Complemento.Text;
            cmd.Parameters.Add("@BANCO", OleDbType.VarChar).Value = Banco.Text;
            cmd.Parameters.Add("@AGENCIA", OleDbType.VarChar).Value = Agencia.Text;
            cmd.Parameters.Add("@CONTADOBANCO", OleDbType.VarChar).Value = Conta.Text;
            cmd.Parameters.Add("@FUNCIONARIODESDE", OleDbType.Date).Value = Convert.ToDateTime(EntradaInfo.Text).ToShortDateString();

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

            cmd.Parameters.Add("@TECNICO", OleDbType.Boolean).Value = IsT;
            cmd.Parameters.Add("@VENDEDOR", OleDbType.Boolean).Value = IsT2;
            cmd.Parameters.Add("@DEMITIDODESLIGADO", OleDbType.Boolean).Value = Demitido.Checked;
            cmd.Parameters.Add("@OBSERVACOES", OleDbType.VarChar).Value = Observaçoes.Text;

            if (FuncionarioPicture.Image != null)
            {
                Bitmap BitmapImg = new Bitmap(FuncionarioPicture.Image);
                byte[] picture = ImageToByte(BitmapImg, System.Drawing.Imaging.ImageFormat.Jpeg);

                cmd.Parameters.Add("@FOTO", OleDbType.Binary).Value = picture;
            }

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

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
            Label[] Labels = new Label[24]
            {
                label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13,
                label14, label15, label18, label19, label24, label27, label28, label29, label30, label31, label32
            };

            //------------------//

            // Textbox
            Guna.UI2.WinForms.Guna2TextBox[] TextBox = new Guna.UI2.WinForms.Guna2TextBox[17]
            {
                Nome, Telefone, CPF, Funcao, CTPS, CNH, Endereço, Bairro, Cidade, CEP, Complemento, Banco, Agencia, Conta, 
                NumeroCAT, CITextBox, Observaçoes
            };

            // Combobox
            Guna.UI2.WinForms.Guna2ComboBox[] Combobox = new Guna.UI2.WinForms.Guna2ComboBox[6]
            {
                TipoDeNumero, EstadoCivil, Genero, Estado, Tecnico, Vendedor
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

            FuncionarioName.ForeColor = ThemeManager.WhiteFontColor;
            IDInfo.ForeColor = ThemeManager.FontColor;
            EntradaInfo.ForeColor = ThemeManager.FontColor;

            DadosBtn.FillColor = ThemeManager.FormBackColor;
            DadosBtn.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            DadosBtn.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;

            EnderecoBtn.FillColor = ThemeManager.FormBackColor;
            EnderecoBtn.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            EnderecoBtn.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;

            DadosBancariosBtn.FillColor = ThemeManager.FormBackColor;
            DadosBancariosBtn.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            DadosBancariosBtn.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;

            OutrosBtn.FillColor = ThemeManager.FormBackColor;
            OutrosBtn.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            OutrosBtn.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;

            FuncionarioPicture.BackColor = ThemeManager.FormBackColor;

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
                EnderecoBtn.ForeColor = Color.FromArgb(180, 180, 180);
                DadosBancariosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);
            }

            else
            {
                DadosBtn.ForeColor = Color.FromArgb(255, 3, 0);
                EnderecoBtn.ForeColor = Color.FromArgb(180, 180, 180);
                DadosBancariosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);
            }

            InformacoesGerais.Location = new Point(InformacoesGerais.Location.X, 232);
            EnderecoPanel.Location = new Point(EnderecoPanel.Location.X, 7732);
            DadosBancariosPanel.Location = new Point(DadosBancariosPanel.Location.X, 7732);
            OutrosPanel.Location = new Point(OutrosPanel.Location.X, 7732);

            foreach (Control ct in InformacoesGerais.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in EnderecoPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in DadosBancariosPanel.Controls)
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
            MovingBar.Size = new Size(86, 3);
            MovingBar.Location = new Point(198, 209);

            if (IsDarkModeEnabled)
            {
                DadosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                EnderecoBtn.ForeColor = Color.FromArgb(255, 33, 0);
                DadosBancariosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);
            }

            else
            {
                DadosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                EnderecoBtn.ForeColor = Color.FromArgb(255, 3, 0);
                DadosBancariosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);
            }

            InformacoesGerais.Location = new Point(InformacoesGerais.Location.X, 7732);
            EnderecoPanel.Location = new Point(EnderecoPanel.Location.X, 232);
            DadosBancariosPanel.Location = new Point(DadosBancariosPanel.Location.X, 7732);
            OutrosPanel.Location = new Point(OutrosPanel.Location.X, 7732);

            foreach (Control ct in InformacoesGerais.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in EnderecoPanel.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in DadosBancariosPanel.Controls)
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
            MovingBar.Size = new Size(136, 3);
            MovingBar.Location = new Point(315, 209);

            if (IsDarkModeEnabled)
            {
                DadosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                EnderecoBtn.ForeColor = Color.FromArgb(180, 180, 180);
                DadosBancariosBtn.ForeColor = Color.FromArgb(255, 33, 0);
                OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);
            }

            else
            {
                DadosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                EnderecoBtn.ForeColor = Color.FromArgb(180, 180, 180);
                DadosBancariosBtn.ForeColor = Color.FromArgb(255, 3, 0);
                OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);
            }

            InformacoesGerais.Location = new Point(InformacoesGerais.Location.X, 7732);
            EnderecoPanel.Location = new Point(EnderecoPanel.Location.X, 7732);
            DadosBancariosPanel.Location = new Point(DadosBancariosPanel.Location.X, 232);
            OutrosPanel.Location = new Point(OutrosPanel.Location.X, 7732);

            foreach (Control ct in InformacoesGerais.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in EnderecoPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in DadosBancariosPanel.Controls)
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
            MovingBar.Location = new Point(460, 209);

            if (IsDarkModeEnabled)
            {
                DadosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                EnderecoBtn.ForeColor = Color.FromArgb(180, 180, 180);
                DadosBancariosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                OutrosBtn.ForeColor = Color.FromArgb(255, 33, 0);
            }

            else
            {
                DadosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                EnderecoBtn.ForeColor = Color.FromArgb(180, 180, 180);
                DadosBancariosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                OutrosBtn.ForeColor = Color.FromArgb(255, 3, 0);
            }

            InformacoesGerais.Location = new Point(InformacoesGerais.Location.X, 7732);
            EnderecoPanel.Location = new Point(EnderecoPanel.Location.X, 7732);
            DadosBancariosPanel.Location = new Point(DadosBancariosPanel.Location.X, 7732);
            OutrosPanel.Location = new Point(OutrosPanel.Location.X, 232);

            foreach (Control ct in InformacoesGerais.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in EnderecoPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in DadosBancariosPanel.Controls)
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
                FuncionarioPicture.Image = new Bitmap(PictureDialog.FileName);
                TextoChanged = true;
            }
        }

        // Remover foto
        private void DeletarFoto_Click(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                FuncionarioPicture.Image = Properties.Resources.DefaultAvatar;
                TextoChanged = true;
            }
        }

        // Salvar alteraçoes
        private void SalvarAlteracoes_Click(object sender, EventArgs e)
        {
            if (Nome.Text != "" && Telefone.Text != "" && TipoDeNumero.SelectedIndex != -1 && CPF.Text != "" && CPF.Text.Length == 14 && Funcao.Text != "")
            {
                if (Endereço.Text != "" && Cidade.Text != "" && Estado.SelectedIndex != -1)
                {
                    if (Telefone.Text != "" && TipoDeNumero.SelectedIndex == 0 && Telefone.Text.Length == 15 || Telefone.Text != "" && TipoDeNumero.SelectedIndex == 1 && Telefone.Text.Length == 9)
                    {
                        if (CEP.Text != "")
                        {
                            if (CEP.Text != "-" && CEP.Text.Length < 9)
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

                            else if (CEP.Text != "-" && CEP.Text.Length == 9)
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

                                SaveChanges();
                            }

                            else if (CEP.Text == "-")
                                SaveChanges();
                        }
                        else
                            SaveChanges();
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

            if (FormLoaded)
                TextoChanged = true;
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
                TipoDeNumeroHint.Visible = false;

                if (TipoDeNumero.SelectedIndex != -1)
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
                        if (TipoDeNumero.SelectedIndex == 0)
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

                        else if (TipoDeNumero.SelectedIndex == 1)
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

                    TipoDeNumeroHint.Visible = true;

                    if (IsDarkModeEnabled)
                    {
                        Telefone.BorderColor = Color.FromArgb(255, 33, 0);
                        Telefone.FocusedState.BorderColor = Color.FromArgb(255, 33, 0);
                        Telefone.HoverState.BorderColor = Color.FromArgb(255, 33, 0);

                        TipoDeNumero.BorderColor = Color.FromArgb(255, 33, 0);
                    }

                    else
                    {
                        Telefone.BorderColor = Color.FromArgb(255, 3, 0);
                        Telefone.FocusedState.BorderColor = Color.FromArgb(255, 3, 0);
                        Telefone.HoverState.BorderColor = Color.FromArgb(255, 3, 0);

                        TipoDeNumero.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                }

                TextoChanged = true;
            }
        }

        private void TipoDeNumero_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoDeNumeroHint.Visible = false;
            TelefoneHint.Visible = false;

            if (IsDarkModeEnabled)
                TipoDeNumero.BorderColor = Color.FromArgb(80, 80, 80);
            else
                TipoDeNumero.BorderColor = Color.FromArgb(210, 210, 210);

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

            if (TipoDeNumero.SelectedIndex == 0)
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

            else if (TipoDeNumero.SelectedIndex == 1)
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
                TipoDeNumero.FocusedState.BorderColor = ThemeManager.ComboBoxFocusedBorderColor;
                TipoDeNumero.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }
            else
            {
                TipoDeNumero.FocusedState.BorderColor = Color.Black;
                TipoDeNumero.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }
        }

        private void CPF_TextChanged(object sender, EventArgs e)
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
        }
    }
}
