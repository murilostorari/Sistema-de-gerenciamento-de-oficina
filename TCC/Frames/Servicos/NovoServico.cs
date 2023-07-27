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
    public partial class NovoServico : Form
    {
        FormCollection fc = Application.OpenForms;

        bool FormLoaded;
        bool CloseOpen;
        bool BackspacePressed;
        bool TextoChanged;

        bool NovaMaoDeObraOpen;
        bool PecasOpen;

        decimal ValorAdiantamentoEmReais;
        decimal ValorMaoDeObraEmReais;
        decimal ValorPecasEmReais;
        decimal ValorDeslocamentoEmReais;
        decimal ValorServicoTerceirosEmReais;
        decimal ValorOutrosEmReais;
        decimal ValorTotalEmReais;

        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;
        bool AutoCompleteValues = Properties.Settings.Default.AutoCompleteCurrencyValues;

        List<Guna.UI2.WinForms.Guna2Button> GunaButtons;
        List<Guna.UI2.WinForms.Guna2Button> GunaBorderButtons;
        List<Guna.UI2.WinForms.Guna2TextBox> GunaTextBox;
        List<Guna.UI2.WinForms.Guna2ComboBox> GunaComboBox;
        List<Guna.UI2.WinForms.Guna2HtmlLabel> GunaHints;

        List<Label> NormalLabels;

        public NovoServico()
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
            foreach (Control ct in VeiculoLabel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in MaoDeObraPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Valores.Controls)
            {
                ct.TabStop = false;
            }

            string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";
            OleDbConnection con = new OleDbConnection(strcon);
            OleDbConnection con2 = new OleDbConnection(strcon);

            con.Open();
            con2.Open();

            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT NOME FROM Clientes";
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
                collection.Add(dtr["NOME"].ToString());
            }

            foreach (DataRow dtr in dt2.Rows)
            {
                Tecnico.Items.Add(dtr["NOME"].ToString());
            }

            Nome.AutoCompleteMode = AutoCompleteMode.Suggest;
            Nome.AutoCompleteSource = AutoCompleteSource.CustomSource;

            Nome.AutoCompleteCustomSource = collection;

            con.Close();
            con2.Close();

            Tecnico.Text = "Não definido";

            Entrada.Value = Convert.ToDateTime(DateTime.Now);
            Pronto.Value = Convert.ToDateTime(DateTime.Now.AddMinutes(1));

            Random Number = new Random();
            int NumberValue = Number.Next(1, 100000);
            NumeroServico.Text = "000" + NumberValue.ToString();

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
                        Frames.CloseConfirm.CloseFrame.TopText.Text = "Cancelar serviço";
                        Frames.CloseConfirm.CloseFrame.LblText.Text = "Você deseja mesmo cancelar o cadastro de novo serviço?";
                    }
                }

                if (frm.Name == "NovoServico")
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
            InformacoesCliente.Visible = false;
            InformacoesCliente.Location = new Point(12, 7734);

            VeiculoLabel.Visible = true;
            VeiculoLabel.Location = new Point(12, 134);

            MaoDeObraPanel.Visible = false;
            MaoDeObraPanel.Location = new Point(12, 7734);

            PecasPanel.Visible = false;
            PecasPanel.Location = new Point(12, 7734);

            Valores.Visible = false;
            Valores.Location = new Point(12, 7734);

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

            foreach (Control ct in InformacoesCliente.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in VeiculoLabel.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in MaoDeObraPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in PecasPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Valores.Controls)
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
            InformacoesCliente.Visible = false;
            InformacoesCliente.Location = new Point(12, 7734);

            VeiculoLabel.Visible = false;
            VeiculoLabel.Location = new Point(12, 7734);

            MaoDeObraPanel.Visible = true;
            MaoDeObraPanel.Location = new Point(12, 134);

            PecasPanel.Visible = false;
            PecasPanel.Location = new Point(12, 7734);

            Valores.Visible = false;
            Valores.Location = new Point(12, 7734);

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

            foreach (Control ct in InformacoesCliente.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in VeiculoLabel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in MaoDeObraPanel.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in PecasPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Valores.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Outros.Controls)
            {
                ct.TabStop = false;
            }
        }

        // Proxima etapa 3
        private void ProximoN3()
        {
            InformacoesCliente.Visible = false;
            InformacoesCliente.Location = new Point(12, 7734);

            VeiculoLabel.Visible = false;
            VeiculoLabel.Location = new Point(12, 7734);

            MaoDeObraPanel.Visible = false;
            MaoDeObraPanel.Location = new Point(12, 7734);

            PecasPanel.Visible = true;
            PecasPanel.Location = new Point(12, 134);

            Valores.Visible = false;
            Valores.Location = new Point(12, 7734);

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

            foreach (Control ct in InformacoesCliente.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in VeiculoLabel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in MaoDeObraPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in PecasPanel.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in Valores.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Outros.Controls)
            {
                ct.TabStop = false;
            }
        }

        // Proxima etapa 4
        private void ProximoN4()
        {
            InformacoesCliente.Visible = false;
            InformacoesCliente.Location = new Point(12, 7734);

            VeiculoLabel.Visible = false;
            VeiculoLabel.Location = new Point(12, 7734);

            MaoDeObraPanel.Visible = false;
            MaoDeObraPanel.Location = new Point(12, 7734);

            PecasPanel.Visible = false;
            PecasPanel.Location = new Point(12, 7734);

            Valores.Visible = true;
            Valores.Location = new Point(12, 134);

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

            foreach (Control ct in InformacoesCliente.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in VeiculoLabel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in MaoDeObraPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in PecasPanel.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in Valores.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in Outros.Controls)
            {
                ct.TabStop = false;
            }
        }

        // Proxima etapa 5
        private void ProximoN5()
        {
            InformacoesCliente.Visible = false;
            InformacoesCliente.Location = new Point(12, 7734);

            VeiculoLabel.Visible = false;
            VeiculoLabel.Location = new Point(12, 7734);

            MaoDeObraPanel.Visible = false;
            MaoDeObraPanel.Location = new Point(12, 7734);

            PecasPanel.Visible = false;
            PecasPanel.Location = new Point(12, 7734);

            Valores.Visible = false;
            Valores.Location = new Point(12, 7734);

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

            foreach (Control ct in InformacoesCliente.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in VeiculoLabel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in MaoDeObraPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in PecasPanel.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in Valores.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Outros.Controls)
            {
                ct.TabStop = true;
            }
        }

        private void ChangedToTrueKeyPress(object sender, KeyPressEventArgs e)
        {
            if (FormLoaded)
                TextoChanged = true;
        }
            
        private void GridChangedToTrue(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (FormLoaded)
                TextoChanged = true;
        }

        private void MoneyTextBoxLeave()
        {
            if (ValorAdiantamento.Text != "")
            {
                string StringNumbers1 = Regex.Replace(ValorAdiantamento.Text, "[^0-9]", string.Empty);

                decimal ConvertToDecimal;

                bool ConvertBool = decimal.TryParse(StringNumbers1, NumberStyles.Currency,
                CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal);

                if (ValorAdiantamento.Text.Contains("R") && ValorAdiantamento.Text.Contains("$"))
                    ValorAdiantamento.Text = ValorAdiantamento.Text;
                else
                {
                    ValorAdiantamento.Text = ValorAdiantamento.Text.Insert(0, "R");
                    ValorAdiantamento.Text = ValorAdiantamento.Text.Insert(1, "$");
                    ValorAdiantamento.Text = ValorAdiantamento.Text.Insert(2, " ");
                }

                ValorAdiantamentoEmReais = ConvertToDecimal;
            }

            //----------//

            if (ValorMaoDeObra.Text != "")
            {
                string StringNumbers2 = Regex.Replace(ValorMaoDeObra.Text, "[^0-9]", string.Empty);

                decimal ConvertToDecimal2;

                bool ConvertBool2 = decimal.TryParse(StringNumbers2, NumberStyles.Currency,
                CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal2);

                if (ValorMaoDeObra.Text.Contains("R") && ValorMaoDeObra.Text.Contains("$"))
                    ValorMaoDeObra.Text = ValorMaoDeObra.Text;
                else
                {
                    ValorMaoDeObra.Text = ValorMaoDeObra.Text.Insert(0, "R");
                    ValorMaoDeObra.Text = ValorMaoDeObra.Text.Insert(1, "$");
                    ValorMaoDeObra.Text = ValorMaoDeObra.Text.Insert(2, " ");
                }

                ValorMaoDeObraEmReais = ConvertToDecimal2;
            }

            //----------//

            if (ValorPecas.Text != "")
            {
                string StringNumbers3 = Regex.Replace(ValorPecas.Text, "[^0-9]", string.Empty);

                decimal ConvertToDecimal3;

                bool ConvertBool3 = decimal.TryParse(StringNumbers3, NumberStyles.Currency,
                CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal3);

                if (ValorPecas.Text.Contains("R") && ValorPecas.Text.Contains("$"))
                    ValorPecas.Text = ValorPecas.Text;
                else
                {
                    ValorPecas.Text = ValorPecas.Text.Insert(0, "R");
                    ValorPecas.Text = ValorPecas.Text.Insert(1, "$");
                    ValorPecas.Text = ValorPecas.Text.Insert(2, " ");
                }

                ValorPecasEmReais = ConvertToDecimal3;
            }

            //----------//

            if (ValorDeslocamento.Text != "")
            {
                string StringNumbers4 = Regex.Replace(ValorDeslocamento.Text, "[^0-9]", string.Empty);

                decimal ConvertToDecimal4;

                bool ConvertBool4 = decimal.TryParse(StringNumbers4, NumberStyles.Currency,
                CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal4);

                if (ValorDeslocamento.Text.Contains("R") && ValorDeslocamento.Text.Contains("$"))
                    ValorDeslocamento.Text = ValorDeslocamento.Text;
                else
                {
                    ValorDeslocamento.Text = ValorDeslocamento.Text.Insert(0, "R");
                    ValorDeslocamento.Text = ValorDeslocamento.Text.Insert(1, "$");
                    ValorDeslocamento.Text = ValorDeslocamento.Text.Insert(2, " ");
                }

                ValorDeslocamentoEmReais = ConvertToDecimal4;
            }

            //----------//

            if (ValorServicoTerceiros.Text != "")
            {
                string StringNumbers5 = Regex.Replace(ValorServicoTerceiros.Text, "[^0-9]", string.Empty);

                decimal ConvertToDecimal5;

                bool ConvertBool5 = decimal.TryParse(StringNumbers5, NumberStyles.Currency,
                CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal5);

                if (ValorServicoTerceiros.Text.Contains("R") && ValorServicoTerceiros.Text.Contains("$"))
                    ValorServicoTerceiros.Text = ValorServicoTerceiros.Text;
                else
                {
                    ValorServicoTerceiros.Text = ValorServicoTerceiros.Text.Insert(0, "R");
                    ValorServicoTerceiros.Text = ValorServicoTerceiros.Text.Insert(1, "$");
                    ValorServicoTerceiros.Text = ValorServicoTerceiros.Text.Insert(2, " ");
                }

                ValorServicoTerceirosEmReais = ConvertToDecimal5;
            }

            //----------//

            if (ValorOutros.Text != "")
            {
                string StringNumbers6 = Regex.Replace(ValorOutros.Text, "[^0-9]", string.Empty);

                decimal ConvertToDecimal6;

                bool ConvertBool6 = decimal.TryParse(StringNumbers6, NumberStyles.Currency,
                CultureInfo.CurrentCulture.NumberFormat, out ConvertToDecimal6);

                if (ValorOutros.Text.Contains("R") && ValorOutros.Text.Contains("$"))
                    ValorOutros.Text = ValorOutros.Text;
                else
                {
                    ValorOutros.Text = ValorOutros.Text.Insert(0, "R");
                    ValorOutros.Text = ValorOutros.Text.Insert(1, "$");
                    ValorOutros.Text = ValorOutros.Text.Insert(2, " ");
                }

                ValorOutrosEmReais = ConvertToDecimal6;
            }

            ValorTotalEmReais = ValorAdiantamentoEmReais + ValorMaoDeObraEmReais + ValorPecasEmReais + ValorDeslocamentoEmReais + ValorServicoTerceirosEmReais + ValorOutrosEmReais;

            if (ValorTotalEmReais > 0)
            {
                ValorTotal.Text = Convert.ToString(ValorTotalEmReais);

                string TextNumbers = Regex.Replace(ValorTotal.Text, "[^0-9]", string.Empty);

                ValorTotal.Text = string.Format("{0:#,##0.00}", Double.Parse(TextNumbers) / 100);
                ValorTotal.Text = Convert.ToString("R$ " + ValorTotal.Text);
                ValorTotal.SelectionStart = ValorTotal.Text.Length;
            }
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
            Label[] Labels = new Label[28]
            {
                label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, 
                label14, label15, label16, label17, label18, label19, label20, label21, label23, label25, label28, label30, 
                label31, label32, label33
            };

            // Botoes normais
            Guna.UI2.WinForms.Guna2Button[] RedButtons = new Guna.UI2.WinForms.Guna2Button[6]
            {
                Proximo1, Proximo2, Proximo3, Proximo4, Proximo5, Concluir
            };

            // Botoes bordas
            Guna.UI2.WinForms.Guna2Button[] BorderButtons = new Guna.UI2.WinForms.Guna2Button[13]
            {
                Cancelar1, Cancelar2, Cancelar3, Cancelar5, Cancelar4,Cancelar5, Anterior1, Anterior2, Anterior4, Anterior3, 
                Anterior4, NovaMaoDeObraBtn, NovasPecasBtn
            };

            // Textbox
            Guna.UI2.WinForms.Guna2TextBox[] TextBox = new Guna.UI2.WinForms.Guna2TextBox[23]
            {
                Nome, Endereco, Bairro, Cidade, Email, Telefone, CPF, Telefone, Veiculo, Marca, Kilometragem, 
                Placa, Chassi, Defeito, Acessorios, ValorAdiantamento, ValorMaoDeObra, ValorPecas, ValorDeslocamento, 
                ValorServicoTerceiros, ValorOutros, ValorTotal, Laudo
            };

            // Combobox
            Guna.UI2.WinForms.Guna2ComboBox[] Combobox = new Guna.UI2.WinForms.Guna2ComboBox[3]
            {
                Status, Prioridade, Tecnico
            };

            // Hints
            Guna.UI2.WinForms.Guna2HtmlLabel[] Hints = new Guna.UI2.WinForms.Guna2HtmlLabel[7]
            {
                NomeHint, VeiculoHint, KilometragemHint, PlacaHint, DefeitoHint, ValorTotalHint, LaudoHint
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
            guna2Separator2.FillColor = ThemeManager.SeparatorAndBorderColor;
            guna2Separator1.FillColor = ThemeManager.SeparatorAndBorderColor;

            Minimize.IconColor = ThemeManager.CloseMinimizeIconColor;
            Minimize.HoverState.IconColor = ThemeManager.CloseMinimizeHoverIconColor;

            CloseBtn.IconColor = ThemeManager.CloseMinimizeIconColor;
            CloseBtn.HoverState.IconColor = ThemeManager.CloseMinimizeHoverIconColor;

            MaoDeObraText.ForeColor = ThemeManager.WhiteFontColor;
            PecasUtilizadasText.ForeColor = ThemeManager.WhiteFontColor;
            ValoresText.ForeColor = ThemeManager.WhiteFontColor;
            OutrosText.ForeColor = ThemeManager.WhiteFontColor;

            MaoDeObraGrid.BackgroundColor = ThemeManager.FormBackColor;
            MaoDeObraGrid.DefaultCellStyle.BackColor = ThemeManager.FormBackColor;
            MaoDeObraGrid.DefaultCellStyle.SelectionBackColor = ThemeManager.FormBackColor;
            MaoDeObraGrid.DefaultCellStyle.ForeColor = ThemeManager.GridForeColor;
            MaoDeObraGrid.DefaultCellStyle.SelectionForeColor = ThemeManager.FontColor;
            MaoDeObraGrid.GridColor = ThemeManager.SeparatorAndBorderColor;

            MaoDeObraGrid.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.WhiteFontColor;
            MaoDeObraGrid.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemeManager.WhiteFontColor;
            MaoDeObraGrid.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.FormBackColor;
            MaoDeObraGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemeManager.FormBackColor;

            MaoDeObraGrid.RowHeadersDefaultCellStyle.BackColor = ThemeManager.FormBackColor;

            PecasUtilizadasGrid.BackgroundColor = ThemeManager.FormBackColor;
            PecasUtilizadasGrid.DefaultCellStyle.BackColor = ThemeManager.FormBackColor;
            PecasUtilizadasGrid.DefaultCellStyle.SelectionBackColor = ThemeManager.FormBackColor;
            PecasUtilizadasGrid.DefaultCellStyle.ForeColor = ThemeManager.GridForeColor;
            PecasUtilizadasGrid.DefaultCellStyle.SelectionForeColor = ThemeManager.FontColor;
            PecasUtilizadasGrid.GridColor = ThemeManager.SeparatorAndBorderColor;

            PecasUtilizadasGrid.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.WhiteFontColor;
            PecasUtilizadasGrid.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemeManager.WhiteFontColor;
            PecasUtilizadasGrid.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.FormBackColor;
            PecasUtilizadasGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemeManager.FormBackColor;

            PecasUtilizadasGrid.RowHeadersDefaultCellStyle.BackColor = ThemeManager.FormBackColor;

            guna2Separator1.FillColor = ThemeManager.SeparatorAndBorderColor;
            guna2Separator2.FillColor = ThemeManager.SeparatorAndBorderColor;

            Entrada.BackColor = ThemeManager.FormBackColor;
            Entrada.FillColor = ThemeManager.FormBackColor;
            Entrada.ForeColor = ThemeManager.WhiteFontColor;
            Entrada.BorderColor = ThemeManager.SeparatorAndBorderColor;
            Entrada.HoverState.BorderColor = ThemeManager.DateTimePickerHoverBorderColor;
            Entrada.CheckedState.BorderColor = ThemeManager.SeparatorAndBorderColor;
            Entrada.CheckedState.FillColor = ThemeManager.FormBackColor;

            Pronto.BackColor = ThemeManager.FormBackColor;
            Pronto.FillColor = ThemeManager.FormBackColor;
            Pronto.ForeColor = ThemeManager.WhiteFontColor;
            Pronto.BorderColor = ThemeManager.SeparatorAndBorderColor;
            Pronto.HoverState.BorderColor = ThemeManager.DateTimePickerHoverBorderColor;
            Pronto.CheckedState.BorderColor = ThemeManager.SeparatorAndBorderColor;
            Pronto.CheckedState.FillColor = ThemeManager.FormBackColor;

            Desc1.ForeColor = ThemeManager.WhiteFontColor;
            Desc2.ForeColor = ThemeManager.WhiteFontColor;

            ToolTip.ForeColor = ThemeManager.GunaToolTipForeColor;
            ToolTip.BorderColor = ThemeManager.GunaToolTipBorderColor;
            ToolTip.BackColor = ThemeManager.GunaToolTipBackColor;

            if (InformacoesCliente.Location == new Point(12, 134))
            {
                ProgressText1.ForeColor = ThemeManager.FullRedButtonColor;
                ProgressBar1.BackColor = ThemeManager.FullRedButtonColor;
            }

            if (VeiculoLabel.Location == new Point(12, 134))
            {
                ProgressText2.ForeColor = ThemeManager.FullRedButtonColor;
                ProgressBar2.BackColor = ThemeManager.FullRedButtonColor;
            }

            if (MaoDeObraPanel.Location == new Point(12, 134))
            {
                ProgressText3.ForeColor = ThemeManager.FullRedButtonColor;
                ProgressBar3.BackColor = ThemeManager.FullRedButtonColor;
            }

            if (Valores.Location == new Point(12, 134))
            {
                ProgressText4.ForeColor = ThemeManager.FullRedButtonColor;
                ProgressBar4.BackColor = ThemeManager.FullRedButtonColor;
            }

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

        private void MaoDeObraGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MaoDeObraGrid.CurrentCell.ColumnIndex == 5)
            {
                MaoDeObraGrid.Rows.Remove(MaoDeObraGrid.CurrentRow);
                MaoDeObraGrid.Refresh();
            }

            if (MaoDeObraGrid.Rows.Count == 0)
            {
                Illustration1.Visible = true;
                Desc1.Visible = true;
                MaoDeObraGrid.Visible = false;
                guna2Separator2.Visible = false;
            }
            else
            {
                guna2Separator2.Visible = true;
            }
        }

        private void PecasUtilizadasGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (PecasUtilizadasGrid.CurrentCell.ColumnIndex == 6)
            {
                PecasUtilizadasGrid.Rows.Remove(PecasUtilizadasGrid.CurrentRow);
                PecasUtilizadasGrid.Refresh();
            }

            if (MaoDeObraGrid.Rows.Count == 0)
            {
                Illustration2.Visible = true;
                Desc2.Visible = true;
                PecasUtilizadasGrid.Visible = false;
                guna2Separator1.Visible = false;
            }
        }

        private void VerifyTimer_Tick(object sender, EventArgs e)
        {
            
        }

        // Proximo
        private void Proximo1_Click(object sender, EventArgs e)
        {
            if (Nome.Text != "")
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
            }
        }

        private void Proximo2_Click(object sender, EventArgs e)
        {
            if (Veiculo.Text != "" && Marca.Text != "" && Kilometragem.Text != "" && Placa.Text != "" && Defeito.Text != "")
            {
                ProximoN2();
            }

            // Modo escuro ativado
            if (IsDarkModeEnabled)
            {
                // Veiculo
                if (Veiculo.Text == "")
                {
                    Veiculo.BorderColor = Color.FromArgb(255, 33, 0);
                    VeiculoHint.Visible = true;
                    Veiculo.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Veiculo.BorderColor = Color.FromArgb(80, 80, 80);
                    VeiculoHint.Visible = false;
                    Veiculo.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }

                // Marca
                if (Marca.Text == "")
                {
                    Marca.BorderColor = Color.FromArgb(255, 33, 0);
                    MarcaHint.Visible = true;
                    Marca.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Marca.BorderColor = Color.FromArgb(80, 80, 80);
                    MarcaHint.Visible = false;
                    Marca.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }


                // Km
                if (Kilometragem.Text == "")
                {
                    Kilometragem.BorderColor = Color.FromArgb(255, 33, 0);
                    KilometragemHint.Visible = true;
                    Kilometragem.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Kilometragem.BorderColor = Color.FromArgb(80, 80, 80);
                    KilometragemHint.Visible = false;
                    Kilometragem.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }

                // Placa
                if (Placa.Text == "")
                {
                    Placa.BorderColor = Color.FromArgb(255, 33, 0);
                    PlacaHint.Visible = true;
                    Placa.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Placa.BorderColor = Color.FromArgb(80, 80, 80);
                    PlacaHint.Visible = false;
                    Placa.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }

                // Defeito
                if (Defeito.Text == "")
                {
                    Defeito.BorderColor = Color.FromArgb(255, 33, 0);
                    DefeitoHint.Visible = true;
                    Defeito.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                }
                else
                {
                    Defeito.BorderColor = Color.FromArgb(80, 80, 80);
                    DefeitoHint.Visible = false;
                    Defeito.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                }
            }

            // Modo escuro desativado
            else
            {
                // Veiculo
                if (Veiculo.Text == "")
                {
                    Veiculo.BorderColor = Color.FromArgb(255, 3, 0);
                    VeiculoHint.Visible = true;
                    Veiculo.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Veiculo.BorderColor = Color.FromArgb(210, 210, 210);
                    VeiculoHint.Visible = false;
                    Veiculo.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                // Marca
                if (Marca.Text == "")
                {
                    Marca.BorderColor = Color.FromArgb(255, 3, 0);
                    MarcaHint.Visible = true;
                    Marca.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Marca.BorderColor = Color.FromArgb(210, 210, 210);
                    MarcaHint.Visible = false;
                    Marca.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                // Km
                if (Kilometragem.Text == "")
                {
                    Kilometragem.BorderColor = Color.FromArgb(255, 3, 0);
                    KilometragemHint.Visible = true;
                    Kilometragem.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Kilometragem.BorderColor = Color.FromArgb(210, 210, 210);
                    KilometragemHint.Visible = false;
                    Kilometragem.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                // Placa
                if (Placa.Text == "")
                {
                    Placa.BorderColor = Color.FromArgb(255, 3, 0);
                    PlacaHint.Visible = true;
                    Placa.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Placa.BorderColor = Color.FromArgb(210, 210, 210);
                    PlacaHint.Visible = false;
                    Placa.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }

                // Defeito
                if (Defeito.Text == "")
                {
                    Defeito.BorderColor = Color.FromArgb(255, 3, 0);
                    DefeitoHint.Visible = true;
                    Defeito.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                }
                else
                {
                    Defeito.BorderColor = Color.FromArgb(210, 210, 210);
                    DefeitoHint.Visible = false;
                    Defeito.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                }
            }
        }

        private void Proximo3_Click(object sender, EventArgs e)
        {
            if (MaoDeObraGrid.Rows.Count > 0)
            {
                if (MaoDeObraGrid.Rows.Count > 1)
                {
                    int Valor1 = 0;
                    int Valor2 = 0;
                    int Valor3 = 0;
                    int Valor4 = 0;
                    int Valor5 = 0;
                    int Valor6 = 0;
                    int Valor7 = 0;
                    int Valor8 = 0;
                    int Valor9 = 0;
                    int Valor10 = 0;

                    int Total = 0;

                    if (MaoDeObraGrid.Rows.Count >= 1)
                    {
                        string v = Regex.Replace((string)MaoDeObraGrid.Rows[0].Cells[3].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor1 = Convert.ToInt32(v);
                    }

                    if (MaoDeObraGrid.Rows.Count >= 2)
                    {
                        string v = Regex.Replace((string)MaoDeObraGrid.Rows[1].Cells[3].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor2 = Convert.ToInt32(v);
                    }

                    if (MaoDeObraGrid.Rows.Count >= 3)
                    {
                        string v = Regex.Replace((string)MaoDeObraGrid.Rows[2].Cells[3].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor3 = Convert.ToInt32(v);
                    }

                    if (MaoDeObraGrid.Rows.Count >= 4)
                    {
                        string v = Regex.Replace((string)MaoDeObraGrid.Rows[3].Cells[3].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor4 = Convert.ToInt32(v);
                    }

                    if (MaoDeObraGrid.Rows.Count >= 5)
                    {
                        string v = Regex.Replace((string)MaoDeObraGrid.Rows[4].Cells[3].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor5 = Convert.ToInt32(v);
                    }

                    if (MaoDeObraGrid.Rows.Count >= 6)
                    {
                        string v = Regex.Replace((string)MaoDeObraGrid.Rows[5].Cells[3].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor6 = Convert.ToInt32(v);
                    }

                    if (MaoDeObraGrid.Rows.Count >= 7)
                    {
                        string v = Regex.Replace((string)MaoDeObraGrid.Rows[6].Cells[3].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor7 = Convert.ToInt32(v);
                    }

                    if (MaoDeObraGrid.Rows.Count >= 8)
                    {
                        string v = Regex.Replace((string)MaoDeObraGrid.Rows[7].Cells[3].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor8 = Convert.ToInt32(v);
                    }

                    if (MaoDeObraGrid.Rows.Count >= 9)
                    {
                        string v = Regex.Replace((string)MaoDeObraGrid.Rows[8].Cells[3].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor9 = Convert.ToInt32(v);
                    }

                    if (MaoDeObraGrid.Rows.Count >= 10)
                    {
                        string v = Regex.Replace((string)MaoDeObraGrid.Rows[9].Cells[3].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor10 = Convert.ToInt32(v);
                    }

                    Total = Valor1 + Valor2 + Valor3 + Valor4 + Valor5 + Valor6 + Valor7 + Valor8 + Valor9 + Valor10 / 100;

                    ValorMaoDeObra.Text = Convert.ToString(Total);

                    ValorMaoDeObra.Text = string.Format("{0:#,##0.00}", Double.Parse(ValorMaoDeObra.Text) / 100);
                    ValorMaoDeObra.Text = Convert.ToString("R$ " + ValorMaoDeObra.Text);
                    ValorMaoDeObra.SelectionStart = ValorMaoDeObra.Text.Length;
                }
                else
                {
                    int Valor1 = 0;
                    int Total = 0;

                    if (MaoDeObraGrid.Rows.Count == 1)
                    {
                        string v = Regex.Replace((string)MaoDeObraGrid.Rows[0].Cells[3].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor1 = Convert.ToInt32(v);

                        Total = Valor1;

                        ValorMaoDeObra.Text = Convert.ToString(Total);

                        ValorMaoDeObra.Text = string.Format("{0:#,##0.00}", Double.Parse(ValorMaoDeObra.Text) / 100);
                        ValorMaoDeObra.Text = Convert.ToString("R$ " + ValorMaoDeObra.Text);
                        ValorMaoDeObra.SelectionStart = ValorMaoDeObra.Text.Length;
                    }

                }

                MoneyTextBoxLeave();

                ProximoN3();
            }
            else
                ProximoN3();
        }

        private void Proximo4_Click(object sender, EventArgs e)
        {
            if (PecasUtilizadasGrid.Rows.Count > 0)
            {
                if (PecasUtilizadasGrid.Rows.Count > 1)
                {
                    int Valor1 = 0;
                    int Valor2 = 0;
                    int Valor3 = 0;
                    int Valor4 = 0;
                    int Valor5 = 0;
                    int Valor6 = 0;
                    int Valor7 = 0;
                    int Valor8 = 0;
                    int Valor9 = 0;
                    int Valor10 = 0;

                    int Total = 0;

                    if (PecasUtilizadasGrid.Rows.Count >= 1)
                    {
                        string v = Regex.Replace((string)PecasUtilizadasGrid.Rows[0].Cells[4].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor1 = Convert.ToInt32(v);
                    }

                    if (PecasUtilizadasGrid.Rows.Count >= 2)
                    {
                        string v = Regex.Replace((string)PecasUtilizadasGrid.Rows[1].Cells[4].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor2 = Convert.ToInt32(v);
                    }

                    if (PecasUtilizadasGrid.Rows.Count >= 3)
                    {
                        string v = Regex.Replace((string)PecasUtilizadasGrid.Rows[2].Cells[4].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor3 = Convert.ToInt32(v);
                    }

                    if (PecasUtilizadasGrid.Rows.Count >= 4)
                    {
                        string v = Regex.Replace((string)PecasUtilizadasGrid.Rows[3].Cells[4].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor4 = Convert.ToInt32(v);
                    }

                    if (PecasUtilizadasGrid.Rows.Count >= 5)
                    {
                        string v = Regex.Replace((string)PecasUtilizadasGrid.Rows[4].Cells[4].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor5 = Convert.ToInt32(v);
                    }

                    if (PecasUtilizadasGrid.Rows.Count >= 6)
                    {
                        string v = Regex.Replace((string)PecasUtilizadasGrid.Rows[5].Cells[4].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor6 = Convert.ToInt32(v);
                    }

                    if (PecasUtilizadasGrid.Rows.Count >= 7)
                    {
                        string v = Regex.Replace((string)PecasUtilizadasGrid.Rows[6].Cells[4].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor7 = Convert.ToInt32(v);
                    }

                    if (PecasUtilizadasGrid.Rows.Count >= 8)
                    {
                        string v = Regex.Replace((string)PecasUtilizadasGrid.Rows[7].Cells[4].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor8 = Convert.ToInt32(v);
                    }

                    if (PecasUtilizadasGrid.Rows.Count >= 9)
                    {
                        string v = Regex.Replace((string)PecasUtilizadasGrid.Rows[8].Cells[4].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor9 = Convert.ToInt32(v);
                    }

                    if (PecasUtilizadasGrid.Rows.Count >= 10)
                    {
                        string v = Regex.Replace((string)PecasUtilizadasGrid.Rows[9].Cells[4].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor10 = Convert.ToInt32(v);
                    }

                    Total = Valor1 + Valor2 + Valor3 + Valor4 + Valor5 + Valor6 + Valor7 + Valor8 + Valor9 + Valor10 / 100;

                    ValorPecas.Text = Convert.ToString(Total);

                    ValorPecas.Text = string.Format("{0:#,##0.00}", Double.Parse(ValorPecas.Text) / 100);
                    ValorPecas.Text = Convert.ToString("R$ " + ValorPecas.Text);
                    ValorPecas.SelectionStart = ValorPecas.Text.Length;
                }
                else
                {
                    int Valor1 = 0;
                    int Total = 0;

                    if (PecasUtilizadasGrid.Rows.Count == 1)
                    {
                        string v = Regex.Replace((string)PecasUtilizadasGrid.Rows[0].Cells[4].Value, "[^0-9]", string.Empty);
                        string TextNumbers = v;

                        Valor1 = Convert.ToInt32(v);

                        Total = Valor1;

                        ValorPecas.Text = Convert.ToString(Total);

                        ValorPecas.Text = string.Format("{0:#,##0.00}", Double.Parse(ValorPecas.Text) / 10);
                        ValorPecas.Text = Convert.ToString("R$ " + ValorPecas.Text);
                        ValorPecas.SelectionStart = ValorPecas.Text.Length;
                    }

                }

                MoneyTextBoxLeave();

                ProximoN4();
            }
            else
                ProximoN4();
        }    

        private void Proximo5_Click(object sender, EventArgs e)
        {
            if (ValorTotalEmReais > 0)
                ProximoN5();
            else
            {
                // Modo escuro ativado
                if (IsDarkModeEnabled)
                {
                    // Nome
                    if (ValorTotal.Text == "")
                    {
                        ValorTotal.BorderColor = Color.FromArgb(255, 33, 0);
                        ValorTotalHint.Visible = true;
                        ValorTotal.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }
                    else
                    {
                        ValorTotal.BorderColor = Color.FromArgb(80, 80, 80);
                        ValorTotalHint.Visible = false;
                        ValorTotal.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                    }
                }

                // Modo escuro desativado
                else
                {
                    // Nome
                    if (ValorTotal.Text == "")
                    {
                        ValorTotal.BorderColor = Color.FromArgb(255, 3, 0);
                        ValorTotalHint.Visible = true;
                        ValorTotal.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                    else
                    {
                        ValorTotal.BorderColor = Color.FromArgb(210, 210, 210);
                        ValorTotalHint.Visible = false;
                        ValorTotal.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                    }
                }
            }
        }

        // Concluir
        private void Concluir_Click(object sender, EventArgs e)
        {
            AddSuccess SuccessForm = new AddSuccess();
            Erro ErrorForm = new Erro();

            if (Endereco.Text == "")
                Endereco.Text = "-";
            if (Bairro.Text == "")
                Bairro.Text = "-";
            if (Cidade.Text == "")
                Cidade.Text = "-";
            if (CPF.Text == "")
                CPF.Text = "-";
            if (Telefone.Text == "")
                Telefone.Text = "-";
            if (Email.Text == "")
                Email.Text = "-";
            if (Chassi.Text == "")
                Chassi.Text = "-";
            if (Acessorios.Text == "")
                Acessorios.Text = "-";
            if (ValorAdiantamento.Text == "")
                ValorAdiantamento.Text = "-";
            if (ValorMaoDeObra.Text == "")
                ValorMaoDeObra.Text = "-";
            if (ValorPecas.Text == "")
                ValorPecas.Text = "-";
            if (ValorDeslocamento.Text == "")
                ValorDeslocamento.Text = "-";
            if (ValorServicoTerceiros.Text == "")
                ValorServicoTerceiros.Text = "-";
            if (ValorOutros.Text == "")
                ValorOutros.Text = "-";
            if (Laudo.Text == "")
                Laudo.Text = "-";

            /*--------------------------------------------------------------------------------------------*/

            if (Laudo.Text != "")
            {
                string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";

                string comando =

                    // Campos
                    "INSERT INTO Servicos (NUMEROSERVICO, CLIENTE, ENDERECO, TELEFONE, CPFCNPJ, EMAIL, ENTRADA, PRONTO, STATUS, \n" +
                    "PRIORIDADE, VALORADIANTAMENTO, VALORMAODEOBRA, VALORPECAS, VALORDESLOCAMENTO, VALORSERVICOSTERCEIROS, VALOROUTROS, " +
                    "VALORTOTAL, VEICULOANO, MARCA, KILOMETRAGEM, PLACA, NUMEROCHASSIS, DEFEITO, ACESSORIOS, LAUDO, TECNICO) \n" +


                    // Valores
                    "values (@NUMEROSERVICO, @CLIENTE, @ENDERECO, @TELEFONE, @CPFCNPJ, @EMAIL, @ENTRADA, @PRONTO, @STATUS, \n" +
                    "@PRIORIDADE, @VALORADIANTAMENTO, @VALORMAODEOBRA, @VALORPECAS, @VALORDESLOCAMENTO, @VALORSERVICOSTERCEIROS, @VALOROUTROS, " +
                    "@VALORTOTAL, @VEICULOANO, @MARCA, @KILOMETRAGEM, @PLACA, @NUMEROCHASSIS, @DEFEITO, @ACESSORIOS, @LAUDO, @TECNICO)";

                OleDbConnection con = new OleDbConnection(strcon);
                OleDbCommand com = new OleDbCommand(comando, con);

                com.Parameters.Add("@NUMEROSERVICO", OleDbType.VarChar).Value = NumeroServico.Text;
                com.Parameters.Add("@CLIENTE", OleDbType.VarChar).Value = Nome.Text;
                com.Parameters.Add("@ENDERECO", OleDbType.VarChar).Value = Endereco.Text;
                com.Parameters.Add("@TELEFONE", OleDbType.VarChar).Value = Telefone.Text;
                com.Parameters.Add("@CPFCNPJ", OleDbType.VarChar).Value = CPF.Text;
                com.Parameters.Add("@EMAIL", OleDbType.VarChar).Value = Email.Text;
                com.Parameters.Add("@ENTRADA", OleDbType.Date).Value = Entrada.Value;
                com.Parameters.Add("@PRONTO", OleDbType.Date).Value = Pronto.Value;
                com.Parameters.Add("@STATUS", OleDbType.VarChar).Value = Status.Text;
                com.Parameters.Add("@PRIORIDADE", OleDbType.VarChar).Value = Prioridade.Text;
                com.Parameters.Add("@VALORADIANTAMENTO", OleDbType.VarChar).Value = ValorAdiantamento.Text;
                com.Parameters.Add("@VALORMAODEOBRA", OleDbType.VarChar).Value = ValorMaoDeObra.Text;
                com.Parameters.Add("@VALORPECAS", OleDbType.VarChar).Value = ValorPecas.Text;
                com.Parameters.Add("@VALORDESLOCAMENTO", OleDbType.VarChar).Value = ValorDeslocamento.Text;
                com.Parameters.Add("@VALORSERVICOSTERCEIROS", OleDbType.VarChar).Value = ValorServicoTerceiros.Text;
                com.Parameters.Add("@VALOROUTROS", OleDbType.VarChar).Value = ValorOutros.Text;
                com.Parameters.Add("@VALORTOTAL", OleDbType.VarChar).Value = ValorTotal.Text;
                com.Parameters.Add("@VEICULOANO", OleDbType.VarChar).Value = Veiculo.Text;
                com.Parameters.Add("@MARCA", OleDbType.VarChar).Value = Marca.Text;
                com.Parameters.Add("@KILOMETRAGEM", OleDbType.VarChar).Value = Kilometragem.Text;
                com.Parameters.Add("@PLACA", OleDbType.VarChar).Value = Placa.Text;
                com.Parameters.Add("@NUMEROCHASSIS", OleDbType.VarChar).Value = Chassi.Text;
                com.Parameters.Add("@DEFEITO", OleDbType.VarChar).Value = Defeito.Text;
                com.Parameters.Add("@ACESSORIOS", OleDbType.VarChar).Value = Acessorios.Text;
                com.Parameters.Add("@LAUDO", OleDbType.VarChar).Value = Laudo.Text;
                com.Parameters.Add("@TECNICO", OleDbType.VarChar).Value = Tecnico.Text;

                try
                {
                    con.Open();

                    com.ExecuteNonQuery();

                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "NovoServico")
                            frm.Opacity = .0d;
                    }

                    AddSuccess.SuccessAdd.LblText.Text = "Serviço adicionado com sucesso!";

                    SuccessForm.Show();

                    Properties.Settings.Default.CanUpdateGrid = true;
                }
                catch (Exception exc)
                {
                    foreach (Form frm in fc)
                    {
                        if (frm.Name == "NovoServico")
                            frm.Opacity = .0d;
                    }

                    Erro.ErrorFrame.LblText.Text = "Erro ao adicionar serviço!";

                    ErrorForm.Show();

                    MessageBox.Show(exc.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                // Modo escuro ativado
                if (IsDarkModeEnabled)
                {
                    // Nome
                    if (Laudo.Text == "")
                    {
                        Laudo.BorderColor = Color.FromArgb(255, 33, 0);
                        LaudoHint.Visible = true;
                        Laudo.HoverState.BorderColor = Color.FromArgb(255, 33, 0);
                    }
                    else
                    {
                        Laudo.BorderColor = Color.FromArgb(80, 80, 80);
                        LaudoHint.Visible = false;
                        Laudo.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
                    }
                }

                // Modo escuro desativado
                else
                {
                    // Nome
                    if (Laudo.Text == "")
                    {
                        Laudo.BorderColor = Color.FromArgb(255, 3, 0);
                        LaudoHint.Visible = true;
                        Laudo.HoverState.BorderColor = Color.FromArgb(255, 3, 0);
                    }
                    else
                    {
                        Laudo.BorderColor = Color.FromArgb(210, 210, 210);
                        LaudoHint.Visible = false;
                        Laudo.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
                    }
                }
            }
        }

        // Anterior
        private void Anterior1_Click(object sender, EventArgs e)
        {
            InformacoesCliente.Visible = true;
            InformacoesCliente.Location = new Point(12, 134);

            VeiculoLabel.Visible = false;
            VeiculoLabel.Location = new Point(12, 7734);

            MaoDeObraPanel.Visible = false;
            MaoDeObraPanel.Location = new Point(12, 7734);

            PecasPanel.Visible = false;
            PecasPanel.Location = new Point(12, 7734);

            Valores.Visible = false;
            Valores.Location = new Point(12, 7734);

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

            foreach (Control ct in InformacoesCliente.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in VeiculoLabel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in MaoDeObraPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in PecasPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Valores.Controls)
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
            InformacoesCliente.Visible = false;
            InformacoesCliente.Location = new Point(12, 7734);

            VeiculoLabel.Visible = true;
            VeiculoLabel.Location = new Point(12, 134);

            MaoDeObraPanel.Visible = false;
            MaoDeObraPanel.Location = new Point(12, 7734);

            PecasPanel.Visible = false;
            PecasPanel.Location = new Point(12, 7734);

            Valores.Visible = false;
            Valores.Location = new Point(12, 7734);

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

            foreach (Control ct in InformacoesCliente.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in VeiculoLabel.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in MaoDeObraPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in PecasPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Valores.Controls)
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
            InformacoesCliente.Visible = false;
            InformacoesCliente.Location = new Point(12, 7734);

            VeiculoLabel.Visible = false;
            VeiculoLabel.Location = new Point(12, 7734);

            MaoDeObraPanel.Visible = true;
            MaoDeObraPanel.Location = new Point(12, 134);

            PecasPanel.Visible = false;
            PecasPanel.Location = new Point(12, 7734);

            Valores.Visible = false;
            Valores.Location = new Point(12, 7734);

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

            foreach (Control ct in InformacoesCliente.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in VeiculoLabel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in MaoDeObraPanel.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in PecasPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Valores.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Outros.Controls)
            {
                ct.TabStop = false;
            }
        }

        private void Anterior4_Click(object sender, EventArgs e)
        {
            InformacoesCliente.Visible = false;
            InformacoesCliente.Location = new Point(12, 7734);

            VeiculoLabel.Visible = false;
            VeiculoLabel.Location = new Point(12, 7734);

            MaoDeObraPanel.Visible = false;
            MaoDeObraPanel.Location = new Point(12, 7734);

            PecasPanel.Visible = true;
            PecasPanel.Location = new Point(12, 134);

            Valores.Visible = false;
            Valores.Location = new Point(12, 7734);

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

            foreach (Control ct in InformacoesCliente.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in VeiculoLabel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in MaoDeObraPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in PecasPanel.Controls)
            {
                ct.TabStop = true;
            }

            foreach (Control ct in Valores.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Outros.Controls)
            {
                ct.TabStop = false;
            }
        }

        private void Anterior5_Click(object sender, EventArgs e)
        {
            InformacoesCliente.Visible = false;
            InformacoesCliente.Location = new Point(12, 7734);

            VeiculoLabel.Visible = false;
            VeiculoLabel.Location = new Point(12, 7734);

            MaoDeObraPanel.Visible = false;
            MaoDeObraPanel.Location = new Point(12, 7734);

            PecasPanel.Visible = false;
            PecasPanel.Location = new Point(12, 7734);

            Valores.Visible = true;
            Valores.Location = new Point(12, 134);

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

                ProgressText3.ForeColor = Color.FromArgb(255, 33, 0);
                ProgressBar3.BackColor = Color.FromArgb(255, 33, 0);
            }
            else
            {
                ProgressText1.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar1.BackColor = Color.FromArgb(255, 3, 0);

                ProgressText2.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar2.BackColor = Color.FromArgb(255, 3, 0);

                ProgressText3.ForeColor = Color.FromArgb(255, 3, 0);
                ProgressBar3.BackColor = Color.FromArgb(255, 3, 0);

                ProgressText3.ForeColor = Color.FromArgb(255, 33, 0);
                ProgressBar3.BackColor = Color.FromArgb(255, 33, 0);
            }

            foreach (Control ct in InformacoesCliente.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in VeiculoLabel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in MaoDeObraPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in PecasPanel.Controls)
            {
                ct.TabStop = false;
            }

            foreach (Control ct in Valores.Controls)
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

        private async void Cancelar5_Click(object sender, EventArgs e)
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

        private async void Cancelar6_Click(object sender, EventArgs e)
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

        private void NovaMaoDeObraBtn_Click(object sender, EventArgs e)
        {
            string Cliente;
            DateTime UltimoServico;

            Cliente = Nome.Text;
            UltimoServico = DataServico.Value;

            NovaMaoDeObra NewMaoDeObraForm = new NovaMaoDeObra(this, NumeroServico.Text);

            foreach (Form frm in fc)
            {
                if (frm.Name != "NovaMaoDeObra")
                    NovaMaoDeObraOpen = false;
                else
                    NovaMaoDeObraOpen = true;
            }

            this.Opacity = .0d;

            NewMaoDeObraForm.Show();
        }

        private void PecasBtn_Click(object sender, EventArgs e)
        {
            string Cliente;
            DateTime UltimoServico;

            Cliente = Nome.Text;
            UltimoServico = DataServico.Value;

            PecasUtilizadas PecasForm = new PecasUtilizadas(this, NumeroServico.Text);

            foreach (Form frm in fc)
            {
                if (frm.Name != "PecasUtilizadas")
                    PecasOpen = false;
                else
                    PecasOpen = true;
            }

            this.Opacity = .0d;

            PecasForm.Show();
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

        private void Nome_Leave(object sender, EventArgs e)
        {
            if (Nome.Text != "")
            {
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

                try
                {
                    con.Open();

                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM Clientes WHERE NOME = @NOME", con);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                    cmd.Parameters.Add("@NOME", OleDbType.VarChar).Value = Nome.Text;

                    OleDbDataReader dtr = cmd.ExecuteReader();

                    if (dtr.Read())
                    {
                        Nome.Text = dtr["NOME"].ToString();
                        Endereco.Text = dtr["ENDERECO"].ToString();
                        Email.Text = dtr["EMAIL"].ToString();
                        Bairro.Text = dtr["BAIRRO"].ToString();
                        Cidade.Text = dtr["CIDADE"].ToString();
                        Telefone.Text = dtr["TELEFONE"].ToString();
                        CPF.Text = dtr["CPFCNPJ"].ToString();
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
                Endereco.Text = "";
                Email.Text = "";
                Bairro.Text = "";
                Cidade.Text = "";
                Telefone.Text = "";
                CPF.Text = "";
            }
            
            TextoChanged = true;
        }

        private void Veiculo_TextChanged(object sender, EventArgs e)
        {
            VeiculoHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                Veiculo.BorderColor = Color.FromArgb(80, 80, 80);

                Veiculo.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                Veiculo.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }

            else
            {
                Veiculo.BorderColor = Color.FromArgb(210, 210, 210);

                Veiculo.FocusedState.BorderColor = Color.Black;
                Veiculo.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }
        }

        private void Kilometragem_TextChanged(object sender, EventArgs e)
        {
            KilometragemHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                Kilometragem.BorderColor = Color.FromArgb(80, 80, 80);

                Kilometragem.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                Kilometragem.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }

            else
            {
                Kilometragem.BorderColor = Color.FromArgb(210, 210, 210);

                Kilometragem.FocusedState.BorderColor = Color.Black;
                Kilometragem.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }
        }

        private void Placa_TextChanged(object sender, EventArgs e)
        {
            PlacaHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                Placa.BorderColor = Color.FromArgb(80, 80, 80);

                Placa.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                Placa.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }

            else
            {
                Placa.BorderColor = Color.FromArgb(210, 210, 210);

                Placa.FocusedState.BorderColor = Color.Black;
                Placa.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }
        }

        private void Marca_TextChanged(object sender, EventArgs e)
        {
            MarcaHint.Visible = false;

            if (IsDarkModeEnabled)
            {
                Marca.BorderColor = Color.FromArgb(80, 80, 80);
                Marca.FocusedState.BorderColor = ThemeManager.TextBoxFocusedBorderColor;
                Marca.HoverState.BorderColor = Color.FromArgb(101, 105, 113);
            }
            else
            {
                Marca.BorderColor = Color.FromArgb(210, 210, 210);
                Marca.FocusedState.BorderColor = Color.Black;
                Marca.HoverState.BorderColor = Color.FromArgb(180, 180, 180);
            }
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Key press */

        private void Kilometragem_KeyPress(object sender, KeyPressEventArgs e)
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

        /* Leave */

        private void ValorAdiantamento_Leave(object sender, EventArgs e)
        {
            MoneyTextBoxLeave();
        }

        private void ValorMaoDeObra_Leave(object sender, EventArgs e)
        {
            MoneyTextBoxLeave();
        }

        private void ValorPecas_Leave(object sender, EventArgs e)
        {
            MoneyTextBoxLeave();
        }

        private void ValorDeslocamento_Leave(object sender, EventArgs e)
        {
            MoneyTextBoxLeave();
        }

        private void ValorServicoTerceiros_Leave(object sender, EventArgs e)
        {
            MoneyTextBoxLeave();
        }

        private void ValorOutros_Leave(object sender, EventArgs e)
        {
            MoneyTextBoxLeave();
        }
    }
}
