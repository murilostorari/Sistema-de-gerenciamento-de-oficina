using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace TCC.Frames
{
    public partial class EditCliente : Form
    {
        FormCollection fc = Application.OpenForms;

        Success SuccessForm = new Success();
        Erro ErrorForm = new Erro();

        bool FormLoaded;
        bool CloseOpen;
        bool BackspacePressed;
        bool TextoChanged;

        int ClienteID;
        byte[] ClienteFoto;

        string NomeCliente, TelefoneText, TipoDeNumeroValue, CpfText, PessoaValue, GeneroValue, AniversarioValue, EntradaValue, EnderecoValue, 
         BairroValue, CidadeValue, CepValue, EstadoValue, ComplementoValue, EmailValue, ObservacoesValue;

        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;

        List<Guna.UI2.WinForms.Guna2TextBox> GunaTextBox;
        List<Guna.UI2.WinForms.Guna2ComboBox> GunaComboBox;
        List<Guna.UI2.WinForms.Guna2Separator> GunaSeparators;

        List<Label> NormalLabels;

        public EditCliente(int ID, string Nome, string Telefone, string TipoDeNumero, string Cpf, string Pessoa, string Genero, string Aniversario, string Entrada, 
            string Endereco, string Bairro, string Cidade, string Cep, string Estado, string Complemento, string Email, string Observacoes, byte[] Foto)
        {
            InitializeComponent();
            AddControlsToList();
            SetColor();

            NomeCliente = Nome;
            ClienteID = ID;

            TelefoneText = Telefone;
            TipoDeNumeroValue = TipoDeNumero;
            CpfText = Cpf;
            PessoaValue = Pessoa;
            GeneroValue = Genero;
            AniversarioValue = Aniversario;
            EntradaValue = Entrada;

            EnderecoValue = Endereco;
            BairroValue = Bairro;
            CidadeValue = Cidade;
            CepValue = Cep;
            EstadoValue = Estado;
            ComplementoValue = Complemento;

            EmailValue = Email;
            ObservacoesValue = Observacoes;

            ClienteFoto = Foto;

            if (AnimateFrames)
                guna2BorderlessForm1.AnimateWindow = true;
            else
                guna2BorderlessForm1.AnimateWindow = false;
        }

        private void EditCliente_Load(object sender, EventArgs e)
        {
            // Dados gerais
            ClienteName.Text = NomeCliente;
            IDInfo.Text = "ID: " + ClienteID;
            IDInt.Text = Convert.ToString(ClienteID);
            ClienteDesde.Text = "Cliente desde: " + EntradaValue;

            // Foto
            byte[] content = ClienteFoto;
            MemoryStream stream = new MemoryStream(content);
            ClientePicture.Image = Image.FromStream(stream);

            // Dados pessoais
            Nome.Text = NomeCliente;
            Telefone.Text = TelefoneText;
            TipoNumero.Text = TipoDeNumeroValue;
            CPFCNPJ.Text = CpfText;
            Pessoa.Text = PessoaValue;
            Genero.Text = GeneroValue;
            Aniversário.Text = AniversarioValue;
            DataEntrada.Text = EntradaValue;

            // Endereco
            Endereço.Text = EnderecoValue;
            Bairro.Text = BairroValue;
            Cidade.Text = CidadeValue;
            CEP.Text = CepValue;
            Estado.Text = EstadoValue;
            Complemento.Text = ComplementoValue;

            // Outros
            Email.Text = EmailValue.Replace("       ", "");
            Observaçoes.Text = ObservacoesValue;

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

                if (frm.Name == "EditCliente")
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
            EditCliente EditForm = (EditCliente)Application.OpenForms["EditCliente"];

            string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";

            OleDbConnection con = new OleDbConnection(strcon);
            OleDbCommand cmd = con.CreateCommand();

            cmd.Parameters.Add("@NOME", OleDbType.VarChar).Value = Nome.Text;
            cmd.Parameters.Add("@TELEFONE", OleDbType.VarChar).Value = Telefone.Text;
            cmd.Parameters.Add("@CPFCNPJ", OleDbType.VarChar).Value = CPFCNPJ.Text;

            if (Genero.SelectedIndex == -1)
                cmd.Parameters.Add("@GENERO", OleDbType.VarChar).Value = "-";
            else
                cmd.Parameters.Add("@GENERO", OleDbType.VarChar).Value = Genero.Text;

            cmd.Parameters.Add("@PESSOA", OleDbType.VarChar).Value = Pessoa.Text;
            cmd.Parameters.Add("@ANIVERSARIO", OleDbType.Date).Value = Aniversário.Value.ToShortDateString();
            cmd.Parameters.Add("@ENTRADA", OleDbType.Date).Value = DataEntrada.Value.ToShortDateString();
            cmd.Parameters.Add("@ENDERECO", OleDbType.VarChar).Value = Endereço.Text;

            if (Bairro.Text == "")
                cmd.Parameters.Add("@BAIRRO", OleDbType.VarChar).Value = "-";
            else
                cmd.Parameters.Add("@BAIRRO", OleDbType.VarChar).Value = Bairro.Text;

            cmd.Parameters.Add("@CIDADE", OleDbType.VarChar).Value = Cidade.Text;

            if (CEP.Text == "")
                cmd.Parameters.Add("@CEP", OleDbType.VarChar).Value = "-";
            else
                cmd.Parameters.Add("@CEP", OleDbType.VarChar).Value = CEP.Text;

            cmd.Parameters.Add("@ESTADO", OleDbType.Char).Value = Estado.Text;

            if (Complemento.Text == "")
                cmd.Parameters.Add("@COMPLEMENTO", OleDbType.VarChar).Value = "-";
            else
                cmd.Parameters.Add("@COMPLEMENTO", OleDbType.VarChar).Value = Complemento.Text;

            if (Email.Text == "")
                cmd.Parameters.Add("@EMAIL", OleDbType.VarChar).Value = "-";
            else
                cmd.Parameters.Add("@EMAIL", OleDbType.VarChar).Value = Email.Text;

            if (Observaçoes.Text == "")
            {
                Observaçoes.ForeColor = Color.White;
                cmd.Parameters.Add("@OBSERVACOES", OleDbType.VarChar).Value = "-";
            }
            else
            {
                Observaçoes.ForeColor = Color.White;
                cmd.Parameters.Add("@OBSERVACOES", OleDbType.VarChar).Value = Observaçoes.Text;
            }

            Bitmap BitmapImg = new Bitmap(ClientePicture.Image);
            byte[] picture = ImageToByte(BitmapImg, System.Drawing.Imaging.ImageFormat.Jpeg);

            cmd.Parameters.Add("@IMG", OleDbType.Binary).Value = picture;

            cmd.CommandType = CommandType.Text;
            cmd.CommandText =

                // Dados pessoais
                "UPDATE Clientes SET NOME = @NOME, TELEFONE = @TELEFONE, CPFCNPJ = @CPFCNPJ," +
                "GENERO = @GENERO, PESSOA = @PESSOA, ANIVERSARIO = @ANIVERSARIO, ENTRADA = @ENTRADA," +

                // Endereço
                "ENDERECO = @ENDERECO, BAIRRO = @BAIRRO, CIDADE = @CIDADE, CEP = @CEP," +
                "ESTADO = @ESTADO, COMPLEMENTO = @COMPLEMENTO," +

                // Outros
                "EMAIL = @EMAIL, OBSERVACOES = @OBSERVACOES, FOTO = @IMG WHERE ID = " + IDInt.Text;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

                EditForm.Opacity = .0d;

                Success.SuccessFrame.LblText.Text = "Alterações salvas com sucesso!";

                SuccessForm.Show();
            }
            catch (Exception)
            {
                Erro.ErrorFrame.LblText.Text = "Erro ao salvar alterações!";

                ErrorForm.Show();
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

            // Labels
            Label[] Labels = new Label[17]
            {
                label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13,
                label14, label15, label16, label21
            };

            //------------------//

            // Textbox
            Guna.UI2.WinForms.Guna2TextBox[] TextBox = new Guna.UI2.WinForms.Guna2TextBox[10]
            {
                Nome, Telefone, CPFCNPJ, Endereço, Bairro, Cidade, CEP, Complemento, Email, Observaçoes
            };

            // Combobox
            Guna.UI2.WinForms.Guna2ComboBox[] Combobox = new Guna.UI2.WinForms.Guna2ComboBox[4]
            {
                TipoNumero, Pessoa, Genero, Estado
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

            ClienteName.ForeColor = ThemeManager.WhiteFontColor;
            IDInfo.ForeColor = ThemeManager.FontColor;
            ClienteDesde.ForeColor = ThemeManager.FontColor;
            
            Aniversário.BackColor = ThemeManager.FormBackColor;
            Aniversário.FillColor = ThemeManager.FormBackColor;
            Aniversário.ForeColor = ThemeManager.WhiteFontColor;
            Aniversário.BorderColor = ThemeManager.SeparatorAndBorderColor;
            Aniversário.HoverState.BorderColor = ThemeManager.DateTimePickerHoverBorderColor;
            Aniversário.CheckedState.BorderColor = ThemeManager.SeparatorAndBorderColor;
            Aniversário.CheckedState.FillColor = ThemeManager.FormBackColor;

            DataEntrada.BackColor = ThemeManager.FormBackColor;
            DataEntrada.FillColor = ThemeManager.FormBackColor;
            DataEntrada.ForeColor = ThemeManager.WhiteFontColor;
            DataEntrada.BorderColor = ThemeManager.SeparatorAndBorderColor;
            DataEntrada.HoverState.BorderColor = ThemeManager.DateTimePickerHoverBorderColor;
            DataEntrada.CheckedState.BorderColor = ThemeManager.SeparatorAndBorderColor;
            DataEntrada.CheckedState.FillColor = ThemeManager.FormBackColor;

            DadosBtn.FillColor = ThemeManager.FormBackColor;
            DadosBtn.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            DadosBtn.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;

            EnderecoBtn.FillColor = ThemeManager.FormBackColor;
            EnderecoBtn.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            EnderecoBtn.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;

            OutrosBtn.FillColor = ThemeManager.FormBackColor;
            OutrosBtn.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            OutrosBtn.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;

            ClientePicture.BackColor = ThemeManager.FormBackColor;

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
            MovingBar.Size = new Size(126, 3);
            MovingBar.Location = new Point(11, 209);

            if (IsDarkModeEnabled)
            {
                DadosBtn.ForeColor = Color.FromArgb(255, 33, 0);
                EnderecoBtn.ForeColor = Color.FromArgb(180, 180, 180);
                OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);
            }

            else
            {
                DadosBtn.ForeColor = Color.FromArgb(255, 3, 0);
                EnderecoBtn.ForeColor = Color.FromArgb(180, 180, 180);
                OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);
            }

            DadosPessoais.Location = new Point(DadosPessoais.Location.X, 232);
            EnderecoLabel.Location = new Point(EnderecoLabel.Location.X, 732);
            Outros.Location = new Point(Outros.Location.X, 732);

            foreach (Control ct in DadosPessoais.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in EnderecoLabel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Outros.Controls)
            {
                ct.TabStop = false;
            }
        }

        private void EnderecoBtn_Click(object sender, EventArgs e)
        {
            MovingBar.Size = new Size(86, 3);
            MovingBar.Location = new Point(168, 209);

            if (IsDarkModeEnabled)
            {
                DadosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                EnderecoBtn.ForeColor = Color.FromArgb(255, 33, 0);
                OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);
            }

            else
            {
                DadosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                EnderecoBtn.ForeColor = Color.FromArgb(255, 3, 0);
                OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);
            }

            DadosPessoais.Location = new Point(DadosPessoais.Location.X, 732);
            EnderecoLabel.Location = new Point(EnderecoLabel.Location.X, 232);
            Outros.Location = new Point(Outros.Location.X, 732);

            foreach (Control ct in DadosPessoais.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in EnderecoLabel.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in Outros.Controls)
            {
                ct.TabStop = false;
            }

        }

        private void OutrosBtn_Click(object sender, EventArgs e)
        {
            MovingBar.Size = new Size(86, 3);
            MovingBar.Location = new Point(275, 209);

            if (IsDarkModeEnabled)
            {
                DadosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                EnderecoBtn.ForeColor = Color.FromArgb(180, 180, 180);
                OutrosBtn.ForeColor = Color.FromArgb(255, 33, 0);
            }

            else
            {
                DadosBtn.ForeColor = Color.FromArgb(180, 180, 180);
                EnderecoBtn.ForeColor = Color.FromArgb(180, 180, 180);
                OutrosBtn.ForeColor = Color.FromArgb(255, 3, 0);
            }

            DadosPessoais.Location = new Point(DadosPessoais.Location.X, 732);
            EnderecoLabel.Location = new Point(EnderecoLabel.Location.X, 732);
            Outros.Location = new Point(Outros.Location.X, 232);

            foreach (Control ct in DadosPessoais.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in EnderecoLabel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Outros.Controls)
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
                ClientePicture.Image = new Bitmap(PictureDialog.FileName);
                TextoChanged = true;
            }
        }

        // Remover foto
        private void DeletarFoto_Click(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                ClientePicture.Image = Properties.Resources.DefaultAvatar;
                TextoChanged = true;
            }
        }

        // Salvar alteraçoes
        private void SalvarAlteracoes_Click(object sender, EventArgs e)
        {
            if (Nome.Text != "" && Telefone.Text != "" && TipoNumero.SelectedIndex != -1 && CPFCNPJ.Text != "" && Pessoa.SelectedIndex != -1 && Endereço.Text != "" && Cidade.Text != "" && Estado.SelectedIndex != -1)
            {
                if (CPFCNPJ.Text != "" && Pessoa.SelectedIndex == 0 && CPFCNPJ.Text.Length == 14 || CPFCNPJ.Text != "" && Pessoa.SelectedIndex == 1 && CPFCNPJ.Text.Length == 18)
                {
                    if (Telefone.Text != "" && TipoNumero.SelectedIndex == 0 && Telefone.Text.Length == 15 || Telefone.Text != "" && TipoNumero.SelectedIndex == 1 && Telefone.Text.Length == 9)
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

            else
            {
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
                    else if (Telefone.Text != "" && TipoNumero.SelectedIndex == 0)
                    {
                        if (Telefone.Text.Length < 15)
                        {
                            TelefoneHint.Visible = true;
                            TelefoneHint.Text = "Insira um número de celular válido";
                            Telefone.BorderColor = Color.FromArgb(255, 33, 0);
                        }
                    }
                    else if (Telefone.Text != "" && TipoNumero.SelectedIndex == 1)
                    {
                        if (Telefone.Text.Length < 9)
                        {
                            TelefoneHint.Visible = true;
                            TelefoneHint.Text = "Insira um número de telefone válido";
                            Telefone.BorderColor = Color.FromArgb(255, 33, 0);
                        }
                    }
                    else
                    {
                        Telefone.BorderColor = Color.FromArgb(80, 80, 80);
                        TelefoneHint.Visible = false;
                        Telefone.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                    }


                    // Tipo de numero
                    if (TipoNumero.SelectedIndex == -1)
                    {
                        TipoNumero.BorderColor = Color.FromArgb(255, 33, 0);
                        TelefoneHint.Text = "Selecione um tipo de número para contato";
                        TipoNumeroHint.Visible = true;
                        TelefoneHint.Visible = true;
                        TipoNumero.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }
                    else
                    {
                        if (Telefone.Text == "")
                        {
                            Telefone.BorderColor = Color.FromArgb(255, 33, 0);

                            if (TipoNumero.SelectedIndex == 0)
                                TelefoneHint.Text = "Insira um número de celular válido";
                            else if (TipoNumero.SelectedIndex == 1)
                                TelefoneHint.Text = "Insira um número de telefone válido";

                            TelefoneHint.Visible = true;
                            Telefone.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                        }

                        else if (Telefone.Text != "" && TipoNumero.SelectedIndex == 0)
                        {
                            if (Telefone.Text.Length < 15)
                            {
                                TelefoneHint.Visible = true;
                                TelefoneHint.Text = "Insira um número de celular válido";
                                Telefone.BorderColor = Color.FromArgb(255, 33, 0);
                            }
                        }

                        else if (Telefone.Text != "" && TipoNumero.SelectedIndex == 1)
                        {
                            if (Telefone.Text.Length < 9)
                            {
                                TelefoneHint.Visible = true;
                                TelefoneHint.Text = "Insira um número de telefone válido";
                                Telefone.BorderColor = Color.FromArgb(255, 33, 0);
                            }
                        }

                        TipoNumero.BorderColor = Color.FromArgb(80, 80, 80);
                        TipoNumeroHint.Visible = false;
                        TipoNumero.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                    }


                    // CPF/CNPJ
                    if (CPFCNPJ.Text == "")
                    {
                        CPFCNPJ.BorderColor = Color.FromArgb(255, 33, 0);
                        CpfHint.Visible = true;
                        CPFCNPJ.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }
                    else if (CPFCNPJ.Text != "" && Pessoa.SelectedIndex == 0)
                    {
                        if (CPFCNPJ.Text.Length < 14)
                        {
                            CpfHint.Visible = true;
                            CpfHint.Text = "Insira um CPF válido";
                            CPFCNPJ.BorderColor = Color.FromArgb(255, 33, 0);
                        }
                    }
                    else if (CPFCNPJ.Text != "" && Pessoa.SelectedIndex == 1)
                    {
                        if (CPFCNPJ.Text.Length < 18)
                        {
                            CpfHint.Visible = true;
                            CpfHint.Text = "Insira um CNPJ válido";
                            CPFCNPJ.BorderColor = Color.FromArgb(255, 33, 0);
                        }
                    }
                    else
                    {
                        CPFCNPJ.BorderColor = Color.FromArgb(80, 80, 80);
                        CpfHint.Visible = false;
                        CPFCNPJ.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                    }


                    // Pessoa
                    if (Pessoa.SelectedIndex == -1)
                    {
                        Pessoa.BorderColor = Color.FromArgb(255, 33, 0);
                        CpfHint.Text = "Selecione um tipo de pessoa";
                        PessoaHint.Visible = true;
                        CpfHint.Visible = true;
                        Pessoa.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }
                    else
                    {
                        if (CPFCNPJ.Text == "")
                        {
                            CPFCNPJ.BorderColor = Color.FromArgb(255, 33, 0);

                            if (Pessoa.SelectedIndex == 0)
                                CpfHint.Text = "Insira um CPF válido";
                            else if (Pessoa.SelectedIndex == 1)
                                CpfHint.Text = "Insira um CNPJ válido";

                            CpfHint.Visible = true;
                            CPFCNPJ.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                        }

                        else if (CPFCNPJ.Text != "" && Pessoa.SelectedIndex == 0)
                        {
                            if (CPFCNPJ.Text.Length < 14)
                            {
                                CpfHint.Visible = true;
                                CpfHint.Text = "Insira um CPF válido";
                                CPFCNPJ.BorderColor = Color.FromArgb(255, 33, 0);
                            }
                        }

                        else if (CPFCNPJ.Text != "" && Pessoa.SelectedIndex == 1)
                        {
                            if (CPFCNPJ.Text.Length < 18)
                            {
                                CpfHint.Visible = true;
                                CpfHint.Text = "Insira um CNPJ válido";
                                CPFCNPJ.BorderColor = Color.FromArgb(255, 33, 0);
                            }
                        }

                        Pessoa.BorderColor = Color.FromArgb(80, 80, 80);
                        PessoaHint.Visible = false;
                        Pessoa.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                    }


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
                    else if (Telefone.Text != "" && TipoNumero.SelectedIndex == 0)
                    {
                        if (Telefone.Text.Length < 15)
                        {
                            TelefoneHint.Visible = true;
                            TelefoneHint.Text = "Insira um número de celular válido";
                            Telefone.BorderColor = Color.FromArgb(255, 3, 0);
                        }
                    }
                    else if (Telefone.Text != "" && TipoNumero.SelectedIndex == 1)
                    {
                        if (Telefone.Text.Length < 18)
                        {
                            TelefoneHint.Visible = true;
                            TelefoneHint.Text = "Insira um número de telefone válido";
                            Telefone.BorderColor = Color.FromArgb(255, 3, 0);
                        }
                    }
                    else
                    {
                        Telefone.BorderColor = Color.FromArgb(210, 210, 210);
                        TelefoneHint.Visible = false;
                        Telefone.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                    }


                    // Tipo de numero
                    if (TipoNumero.SelectedIndex == -1)
                    {
                        TipoNumero.BorderColor = Color.FromArgb(255, 3, 0);
                        TelefoneHint.Text = "Selecione um tipo de número para contato";
                        TipoNumeroHint.Visible = true;
                        TelefoneHint.Visible = true;
                        TipoNumero.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                    else
                    {
                        if (Telefone.Text == "")
                        {
                            Telefone.BorderColor = Color.FromArgb(255, 3, 0);

                            if (TipoNumero.SelectedIndex == 0)
                                TelefoneHint.Text = "Insira um número de celular válido";
                            else if (TipoNumero.SelectedIndex == 1)
                                TelefoneHint.Text = "Insira um número de telefone válido";

                            TelefoneHint.Visible = true;
                            Telefone.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                        }

                        else if (Telefone.Text != "" && TipoNumero.SelectedIndex == 0)
                        {
                            if (Telefone.Text.Length < 15)
                            {
                                TelefoneHint.Visible = true;
                                TelefoneHint.Text = "Insira um número de celular válido";
                                Telefone.BorderColor = Color.FromArgb(255, 3, 0);
                            }
                        }

                        else if (Telefone.Text != "" && TipoNumero.SelectedIndex == 1)
                        {
                            if (Telefone.Text.Length < 9)
                            {
                                TelefoneHint.Visible = true;
                                TelefoneHint.Text = "Insira um número de telefone válido";
                                Telefone.BorderColor = Color.FromArgb(255, 3, 0);
                            }
                        }

                        TipoNumero.BorderColor = Color.FromArgb(210, 210, 210);
                        TipoNumeroHint.Visible = false;
                        TipoNumero.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                    }


                    // CPF/CNPJ
                    if (CPFCNPJ.Text == "")
                    {
                        CPFCNPJ.BorderColor = Color.FromArgb(255, 3, 0);
                        CpfHint.Visible = true;
                        CPFCNPJ.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                    else if (CPFCNPJ.Text != "" && Pessoa.SelectedIndex == 0)
                    {
                        if (CPFCNPJ.Text.Length < 14)
                        {
                            CpfHint.Visible = true;
                            CpfHint.Text = "Insira um CPF válido";
                            CPFCNPJ.BorderColor = Color.FromArgb(255, 3, 0);
                        }
                    }
                    else if (CPFCNPJ.Text != "" && Pessoa.SelectedIndex == 1)
                    {
                        if (CPFCNPJ.Text.Length < 18)
                        {
                            CpfHint.Visible = true;
                            CpfHint.Text = "Insira um CNPJ válido";
                            CPFCNPJ.BorderColor = Color.FromArgb(255, 3, 0);
                        }
                    }
                    else
                    {
                        CPFCNPJ.BorderColor = Color.FromArgb(210, 210, 210);
                        CpfHint.Visible = false;
                        CPFCNPJ.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                    }


                    // Pessoa
                    if (Pessoa.SelectedIndex == -1)
                    {
                        Pessoa.BorderColor = Color.FromArgb(255, 3, 0);
                        CpfHint.Text = "Selecione um tipo de pessoa";
                        PessoaHint.Visible = true;
                        CpfHint.Visible = true;
                        Pessoa.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                    else
                    {
                        if (CPFCNPJ.Text == "")
                        {
                            CPFCNPJ.BorderColor = Color.FromArgb(255, 3, 0);

                            if (Pessoa.SelectedIndex == 0)
                                CpfHint.Text = "Insira um CPF válido";
                            else if (Pessoa.SelectedIndex == 1)
                                CpfHint.Text = "Insira um CNPJ válido";

                            CpfHint.Visible = true;
                            CPFCNPJ.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                        }

                        else if (CPFCNPJ.Text != "" && Pessoa.SelectedIndex == 0)
                        {
                            if (CPFCNPJ.Text.Length < 14)
                            {
                                CpfHint.Visible = true;
                                CpfHint.Text = "Insira um CPF válido";
                                CPFCNPJ.BorderColor = Color.FromArgb(255, 3, 0);
                            }
                        }

                        else if (CPFCNPJ.Text != "" && Pessoa.SelectedIndex == 1)
                        {
                            if (CPFCNPJ.Text.Length < 18)
                            {
                                CpfHint.Visible = true;
                                CpfHint.Text = "Insira um CNPJ válido";
                                CPFCNPJ.BorderColor = Color.FromArgb(255, 3, 0);
                            }
                        }

                        Pessoa.BorderColor = Color.FromArgb(210, 210, 210);
                        PessoaHint.Visible = false;
                        Pessoa.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                    }


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
        private void ClientePicture_Click(object sender, EventArgs e)
        {
            if (Pessoa.SelectedIndex == 0)
            {
                if (Genero.SelectedIndex == 0)
                {
                    string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsFeminino", "*.png");
                    List<string> Icones = Pasta.ToList();
                    Random RandomIcon = new Random();
                    ClientePicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
                }

                else if (Genero.SelectedIndex == 1)
                {
                    string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsMasculino", "*.png");
                    List<string> Icones = Pasta.ToList();
                    Random RandomIcon = new Random();
                    ClientePicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
                }
            }
            else
            {
                if (Pessoa.SelectedIndex == 1)
                {
                    string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsEmpresa", "*.png");
                    List<string> Icones = Pasta.ToList();
                    Random RandomIcon = new Random();
                    ClientePicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
                }
            }

            if (FormLoaded)
                TextoChanged = true;
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Text changed */

        private void Nome_TextChanged(object sender, EventArgs e)
        {
            if (IsDarkModeEnabled)
            {
                NomeHint.Visible = false;
                Nome.BorderColor = Color.FromArgb(80, 80, 80);

                Nome.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                Nome.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }

            else
            {
                NomeHint.Visible = false;
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
                            CPFCNPJ.BorderColor = Color.FromArgb(255, 3, 0);
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

        private void CPFCNPJ_TextChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                CpfHint.Visible = false;

                if (Pessoa.SelectedIndex != -1)
                {
                    PessoaHint.Visible = false;

                    if (IsDarkModeEnabled)
                    {
                        CPFCNPJ.BorderColor = Color.FromArgb(80, 80, 80);
                        CPFCNPJ.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                        CPFCNPJ.HoverState.BorderColor = Color.FromArgb(101, 105, 113);

                        Pessoa.BorderColor = Color.FromArgb(80, 80, 80);
                    }

                    else
                    {
                        CPFCNPJ.BorderColor = Color.FromArgb(210, 210, 210);
                        CPFCNPJ.FocusedState.BorderColor = Color.Black;
                        CPFCNPJ.HoverState.BorderColor = Color.FromArgb(180, 180, 180);

                        Pessoa.BorderColor = Color.FromArgb(210, 210, 210);
                    }

                    if (BackspacePressed != true)
                    {
                        // CPF
                        if (Pessoa.SelectedIndex == 0)
                        {
                            if (CPFCNPJ.TextLength == 3)
                            {
                                CPFCNPJ.Text = CPFCNPJ.Text.Insert(3, ".");
                                CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                            }

                            else if (CPFCNPJ.TextLength == 4)
                            {
                                if (CPFCNPJ.Text.Contains("."))
                                {
                                    CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                                }
                                else
                                {
                                    CPFCNPJ.Text = CPFCNPJ.Text.Insert(3, ".");
                                    CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                                }
                            }


                            else if (CPFCNPJ.TextLength == 7)
                            {
                                CPFCNPJ.Text = CPFCNPJ.Text.Insert(7, ".");
                                CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                            }

                            else if (CPFCNPJ.TextLength == 8)
                            {
                                if (CPFCNPJ.Text.Contains("."))
                                {
                                    CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                                }
                                else
                                {
                                    CPFCNPJ.Text = CPFCNPJ.Text.Insert(7, ".");
                                    CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                                }
                            }


                            else if (CPFCNPJ.TextLength == 11)
                            {
                                CPFCNPJ.Text = CPFCNPJ.Text.Insert(11, "-");
                                CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                            }

                            else if (CPFCNPJ.TextLength == 12)
                            {
                                if (CPFCNPJ.Text.Contains("-"))
                                {
                                    CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                                }
                                else
                                {
                                    CPFCNPJ.Text = CPFCNPJ.Text.Insert(11, "-");
                                    CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                                }
                            }
                        }


                        // CNPJ
                        else if (Pessoa.SelectedIndex == 1)
                        {
                            if (CPFCNPJ.TextLength == 2)
                            {
                                CPFCNPJ.Text = CPFCNPJ.Text.Insert(2, ".");
                                CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                            }

                            else if (CPFCNPJ.TextLength == 3)
                            {
                                if (CPFCNPJ.Text.Contains("."))
                                {
                                    CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                                }
                                else
                                {
                                    CPFCNPJ.Text = CPFCNPJ.Text.Insert(2, ".");
                                    CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                                }
                            }


                            else if (CPFCNPJ.TextLength == 6)
                            {
                                CPFCNPJ.Text = CPFCNPJ.Text.Insert(6, ".");
                                CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                            }

                            else if (CPFCNPJ.TextLength == 7)
                            {
                                if (CPFCNPJ.Text.Contains("."))
                                {
                                    CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                                }
                                else
                                {
                                    CPFCNPJ.Text = CPFCNPJ.Text.Insert(6, ".");
                                    CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                                }
                            }


                            else if (CPFCNPJ.TextLength == 10)
                            {
                                CPFCNPJ.Text = CPFCNPJ.Text.Insert(10, "/");
                                CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                            }

                            else if (CPFCNPJ.TextLength == 11)
                            {
                                if (CPFCNPJ.Text.Contains("/"))
                                {
                                    CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                                }
                                else
                                {
                                    CPFCNPJ.Text = CPFCNPJ.Text.Insert(10, "/");
                                    CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                                }
                            }


                            else if (CPFCNPJ.TextLength == 15)
                            {
                                CPFCNPJ.Text = CPFCNPJ.Text.Insert(15, "-");
                                CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                            }

                            else if (CPFCNPJ.TextLength == 16)
                            {
                                if (CPFCNPJ.Text.Contains("-"))
                                {
                                    CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                                }
                                else
                                {
                                    CPFCNPJ.Text = CPFCNPJ.Text.Insert(15, "-");
                                    CPFCNPJ.SelectionStart = CPFCNPJ.Text.Length + 1;
                                }
                            }
                        }
                    }
                }

                else
                {
                    CpfHint.Text = "Selecione um tipo de pessoa";
                    CPFCNPJ.Text = "";
                    CpfHint.Visible = true;

                    PessoaHint.Visible = true;

                    if (IsDarkModeEnabled)
                    {
                        CPFCNPJ.BorderColor = Color.FromArgb(255, 33, 0);
                        CPFCNPJ.FocusedState.BorderColor = Color.FromArgb(255, 33, 0);
                        CPFCNPJ.HoverState.BorderColor = Color.FromArgb(255, 33, 0);

                        Pessoa.BorderColor = Color.FromArgb(255, 33, 0);
                    }

                    else
                    {
                        CPFCNPJ.BorderColor = Color.FromArgb(255, 3, 0);
                        CPFCNPJ.FocusedState.BorderColor = Color.FromArgb(255, 3, 0);
                        CPFCNPJ.HoverState.BorderColor = Color.FromArgb(255, 3, 0);

                        Pessoa.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                }
            }
        }

        private void Pessoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                PessoaHint.Visible = false;
                CpfHint.Visible = false;

                if (IsDarkModeEnabled)
                    Pessoa.BorderColor = Color.FromArgb(80, 80, 80);
                else
                    Pessoa.BorderColor = Color.FromArgb(210, 210, 210);

                if (CPFCNPJ.Focused == true)
                {
                    if (IsDarkModeEnabled)
                        CPFCNPJ.BorderColor = Color.FromArgb(80, 80, 80);
                    else
                        CPFCNPJ.BorderColor = Color.Black;
                }

                else
                {
                    if (IsDarkModeEnabled)
                        CPFCNPJ.BorderColor = Color.FromArgb(80, 80, 80);
                    else
                        CPFCNPJ.BorderColor = Color.FromArgb(180, 180, 180);
                }

                if (IsDarkModeEnabled)
                {
                    CPFCNPJ.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                    CPFCNPJ.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }

                else
                {
                    CPFCNPJ.FocusedState.BorderColor = Color.Black;
                    CPFCNPJ.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                if (Pessoa.SelectedIndex == 0)
                {
                    Genero.Enabled = true;
                    Aniversário.Enabled = true;
                    CPFCNPJ.MaxLength = 14;

                    if (CPFCNPJ.Text != "")
                    {
                        if (CPFCNPJ.Text.Length < 14)
                        {
                            if (IsDarkModeEnabled)
                            {
                                CpfHint.Visible = true;
                                CpfHint.Text = "Insira um CPF válido";
                                CPFCNPJ.BorderColor = Color.FromArgb(255, 33, 0);
                            }

                            else
                            {
                                CpfHint.Visible = true;
                                CpfHint.Text = "Insira um CPF válido";
                                CPFCNPJ.BorderColor = Color.FromArgb(255, 3, 0);
                            }
                        }

                        else if (CPFCNPJ.Text.Length > 14)
                        {
                            if (IsDarkModeEnabled)
                            {
                                CpfHint.Visible = true;
                                CpfHint.Text = "Insira um CPF válido";
                                CPFCNPJ.BorderColor = Color.FromArgb(255, 33, 0);
                            }

                            else
                            {
                                CpfHint.Visible = true;
                                CpfHint.Text = "Insira um CPF válido";
                                CPFCNPJ.BorderColor = Color.FromArgb(255, 3, 0);
                            }
                        }

                        else
                            CpfHint.Visible = false;
                    }

                    if (Genero.SelectedIndex == -1)
                    {
                        ClientePicture.ImageLocation = Application.StartupPath + "\\DefaultAvatar.png";
                    }

                    else if (Genero.SelectedIndex == 0)
                    {
                        string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsFeminino", "*.png");
                        List<string> Icones = Pasta.ToList();
                        Random RandomIcon = new Random();
                        ClientePicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
                    }

                    else if (Genero.SelectedIndex == 1)
                    {
                        string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsMasculino", "*.png");
                        List<string> Icones = Pasta.ToList();
                        Random RandomIcon = new Random();
                        ClientePicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
                    }
                }

                else if (Pessoa.SelectedIndex == 1)
                {
                    Genero.SelectedIndex = -1;
                    Genero.Enabled = false;
                    Aniversário.Enabled = false;
                    CPFCNPJ.MaxLength = 18;

                    if (CPFCNPJ.Text != "")
                    {
                        if (CPFCNPJ.Text.Length < 18)
                        {
                            if (IsDarkModeEnabled)
                            {
                                CpfHint.Visible = true;
                                CpfHint.Text = "Insira um CNPJ válido";
                                CPFCNPJ.BorderColor = Color.FromArgb(255, 33, 0);
                            }

                            else
                            {
                                CpfHint.Visible = true;
                                CpfHint.Text = "Insira um CNPJ válido";
                                CPFCNPJ.BorderColor = Color.FromArgb(255, 3, 0);
                            }
                        }
                    }

                    string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsEmpresa", "*.png");
                    List<string> Icones = Pasta.ToList();
                    Random random = new Random();
                    ClientePicture.ImageLocation = Pasta[random.Next(0, Icones.Count - 1)];
                }

                if (IsDarkModeEnabled)
                {
                    Pessoa.FocusedState.BorderColor = ThemeManager.ComboBoxFocusedBorderColor;
                    Pessoa.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }

                else
                {
                    Pessoa.FocusedState.BorderColor = Color.Black;
                    Pessoa.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        private void Genero_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                if (Pessoa.SelectedIndex == 0)
                {
                    if (Genero.SelectedIndex == 0)
                    {
                        string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsFeminino", "*.png");
                        List<string> Icones = Pasta.ToList();
                        Random RandomIcon = new Random();
                        ClientePicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
                    }

                    else if (Genero.SelectedIndex == 1)
                    {
                        string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsMasculino", "*.png");
                        List<string> Icones = Pasta.ToList();
                        Random RandomIcon = new Random();
                        ClientePicture.ImageLocation = Pasta[RandomIcon.Next(0, Icones.Count - 1)];
                    }
                }

                else if (Pessoa.SelectedIndex == 1)
                {
                    string[] Pasta = System.IO.Directory.GetFiles(Application.StartupPath + "\\IconsEmpresa", "*.png");
                    List<string> Icones = Pasta.ToList();
                    Random random = new Random();
                    ClientePicture.ImageLocation = Pasta[random.Next(0, Icones.Count - 1)];
                }
            }
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

        /*--------------------------------------------------------------------------------------------*/

        /* Backspace pressed */

        private async void Telefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (FormLoaded)
            {
                if (e.KeyChar == (char)Keys.Back)
                {
                    BackspacePressed = true;
                    await TaskDelay(100);

                    if (Telefone.TextLength == 3)
                    {
                        if (Telefone.Text.Contains(")"))
                        {
                            Telefone.Text.Replace(")", "");
                        }
                    }
                    else if (Telefone.TextLength == 7)
                    {
                        if (Telefone.Text.Contains("("))
                        {
                            Telefone.Text.Replace("(", "");
                        }
                    }
                    else if (Telefone.Text.Length == 11)
                    {
                        if (Telefone.Text.Contains("-"))
                        {
                            Telefone.Text.Replace("-", "");
                        }
                    }
                }

                else if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar))
                {
                    e.Handled = true;
                }

                else if (TipoNumero.SelectedIndex == -1)
                {
                    e.Handled = true;

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
            }

            TextoChanged = true;
        }

        private async void CPFCNPJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (FormLoaded)
            {
                if (e.KeyChar == (char)Keys.Back)
                {
                    BackspacePressed = true;
                    await TaskDelay(50);


                    // CPF
                    if (Pessoa.SelectedIndex == 0)
                    {
                        if (CPFCNPJ.TextLength == 4)
                        {
                            if (CPFCNPJ.Text.Contains("."))
                            {
                                CPFCNPJ.Text.Replace(".", "");
                            }
                        }

                        else if (CPFCNPJ.TextLength == 8)
                        {
                            if (CPFCNPJ.Text.Contains("."))
                            {
                                CPFCNPJ.Text.Replace(".", "");
                            }
                        }

                        else if (CPFCNPJ.Text.Length == 12)
                        {
                            if (CPFCNPJ.Text.Contains("-"))
                            {
                                CPFCNPJ.Text.Replace("-", "");
                            }
                        }
                    }


                    // CNPJ
                    else if (Pessoa.SelectedIndex == 1)
                    {
                        if (CPFCNPJ.TextLength == 3)
                        {
                            if (CPFCNPJ.Text.Contains("."))
                            {
                                CPFCNPJ.Text.Replace(".", "");
                            }
                        }

                        else if (CPFCNPJ.TextLength == 7)
                        {
                            if (CPFCNPJ.Text.Contains("."))
                            {
                                CPFCNPJ.Text.Replace(".", "");
                            }
                        }

                        else if (CPFCNPJ.Text.Length == 11)
                        {
                            if (CPFCNPJ.Text.Contains("/"))
                            {
                                CPFCNPJ.Text.Replace("/", "");
                            }
                        }

                        else if (CPFCNPJ.Text.Length == 16)
                        {
                            if (CPFCNPJ.Text.Contains("-"))
                            {
                                CPFCNPJ.Text.Replace("-", "");
                            }
                        }
                    }
                }

                else if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar))
                {
                    e.Handled = true;
                }

                else if (Pessoa.SelectedIndex == -1)
                {
                    e.Handled = true;

                    CpfHint.Text = "Selecione um tipo de pessoa";
                    CpfHint.Visible = true;

                    PessoaHint.Visible = true;

                    if (IsDarkModeEnabled)
                    {
                        CPFCNPJ.BorderColor = Color.FromArgb(255, 33, 0);
                        CPFCNPJ.FocusedState.BorderColor = Color.FromArgb(255, 33, 0);
                        CPFCNPJ.HoverState.BorderColor = Color.FromArgb(255, 33, 0);

                        Pessoa.BorderColor = Color.FromArgb(255, 33, 0);
                    }

                    else
                    {
                        CPFCNPJ.BorderColor = Color.FromArgb(255, 3, 0);
                        CPFCNPJ.FocusedState.BorderColor = Color.FromArgb(255, 3, 0);
                        CPFCNPJ.HoverState.BorderColor = Color.FromArgb(255, 3, 0);

                        Pessoa.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                }

                TextoChanged = true;
            }
        }

        private async void CEP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (FormLoaded)
            {
                if (e.KeyChar == (char)Keys.Back)
                {
                    BackspacePressed = true;
                    await TaskDelay(100);

                    if (CEP.TextLength == 5)
                    {
                        if (CEP.Text.Contains("-"))
                        {
                            CEP.Text.Replace("-", "");
                        }
                    }
                }

                else if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar))
                {
                    e.Handled = true;
                }

                TextoChanged = true;
            }   
        }
    }
}
