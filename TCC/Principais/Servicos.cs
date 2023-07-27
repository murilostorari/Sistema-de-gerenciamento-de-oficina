using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace TCC.Principais
{
    public partial class Servicos : Form
    {
        FormCollection fc = Application.OpenForms;

        Frames.Success SuccessForm = new Frames.Success();
        Frames.Erro ErrorForm = new Frames.Erro();
        Frames.DeleteSelected2 DeleteSelectedForm = new Frames.DeleteSelected2("Produto", ProductNameString);
        Frames.DeleteAllSelected DeleteAllSelectedForm = new Frames.DeleteAllSelected();
        Frames.Delete.DeleteConfirmation2 DeleteConfirmation2Form = new Frames.Delete.DeleteConfirmation2("Serviços", AllNames);

        private int CurrentPage = 0;
        int PagesCount = 0;
        int PageRows = Properties.Settings.Default.ItensPorPagina;

        public static string ProductNameString;

        int FilteredProducts = 0;

        bool NovoServicoOpen;
        bool DeleteServicoOpen;
        bool EditServicoOpen;

        bool DataFiltered;

        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateButtonsAndPanels = Properties.Settings.Default.AnimarBotoes;
        bool EnableDoubleClickInGrid = Properties.Settings.Default.DoubleClickInGridEnabled;
        string TipoDeLista = Properties.Settings.Default.TipoDeLista;

        bool FormLoaded;

        string SortedByItem = "Id";
        string SortedByOrder = "Ascending";

        string CurvaFilteredItem = "";
        string FabricanteFilteredItem = "";
        string GrupoFilteredItem = "";
        string TipoFilteredItem = "";
        string FornecedorFilteredItem = "";
        string StatusFilteredItem = "";

        int FilterByDays = 0;

        public static List<string> AllNames = new List<string>();

        List<Guna.UI2.WinForms.Guna2Panel> GunaPanels;
        List<Guna.UI2.WinForms.Guna2Button> GunaMainButtons;
        List<Guna.UI2.WinForms.Guna2Button> GunaButtons;
        List<Guna.UI2.WinForms.Guna2RadioButton> GunaRadioButtons;
        List<Guna.UI2.WinForms.Guna2Separator> GunaSeparators;

        List<Label> NormalLabels;
        List<Label> CustomerInfoLabels;
        List<Label> CustomerInfoPresetLabels;

        public Servicos()
        {
            InitializeComponent();

            AddControlsToList();
            SetColor();
        }

        private void Servicos_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'allServicosData.Servicos'. Você pode movê-la ou removê-la conforme necessário.
            this.servicosTableAdapter1.Fill(this.allServicosData.Servicos);
            // TODO: esta linha de código carrega dados na tabela 'servicosData.Servicos'. Você pode movê-la ou removê-la conforme necessário.
            this.servicosTableAdapter.Fill(this.servicosData.Servicos);
            // TODO: esta linha de código carrega dados na tabela 'servicosData.Servicos'. Você pode movê-la ou removê-la conforme necessário.
            this.servicosTableAdapter.Fill(this.servicosData.Servicos);
            CodigoEAN13.Text = "123456789012";

            // Estoque Data
            this.servicosTableAdapter.Fill(this.servicosData.Servicos);

            // All estoque data
            this.servicosTableAdapter1.Fill(this.allServicosData.Servicos);

            if (IsDarkModeEnabled)
            {
                FilterPesquisaBtn.Image = Properties.Resources.search_branco;
                FilterCurvaBtn.Image = Properties.Resources.curva_branco;
                FilterFabricanteBtn.Image = Properties.Resources.fabricante_branco;
                FilterGrupoBtn.Image = Properties.Resources.unidade_tipo_grupo_branco;
                FilterTipoItemBtn.Image = Properties.Resources.unidade_tipo_grupo_branco;
                FilterFornecedoresBtn.Image = Properties.Resources.fornecedor_menor_branco;
                FilterStatusBtn.Image = Properties.Resources.status_branco;

                DataEspecifica.Image = Properties.Resources.data_branco;

                SortBtn.Image = Properties.Resources.sort_branco;
                ViewOptionsBtn.Image = Properties.Resources.lista_branco;

                ListaCompacta.Image = Properties.Resources.lista_compacta_branco;

                Descrescente.Image = Properties.Resources.descending_branco;

                ExcelBtn.Image = Properties.Resources.excel_branco;
                PdfBtn.Image = Properties.Resources.pdf_claro;

                ExportCurrentExcel.Image = Properties.Resources.current_page_branco;
                ExportAllExcel.Image = Properties.Resources.all_pages_branco;

                ExportCurrentPdf.Image = Properties.Resources.current_page_branco;
                ExportAllPdf.Image = Properties.Resources.all_pages_branco;

                ExportOptionsBtn.Image = Properties.Resources.settings_branco;

                StatusInfo.Image = Properties.Resources.status_branco;

                NumeroFabricantePreset.Image = Properties.Resources.number_branco;
                NumeroOriginalPreset.Image = Properties.Resources.number_branco;
                MarcaPreset.Image = Properties.Resources.fabricante_branco;
                UnidadePreset.Image = Properties.Resources.unidade_tipo_grupo_branco;
                TipoProdutoPreset.Image = Properties.Resources.unidade_tipo_grupo_branco;
                GrupoPreset.Image = Properties.Resources.unidade_tipo_grupo_branco;
                SubGrupoPreset.Image = Properties.Resources.unidade_tipo_grupo_branco;
                FornecedorPreset.Image = Properties.Resources.fornecedor_branco;
                UltimaVendaPreset.Image = Properties.Resources.data_branco;
                CurvaPreset.Image = Properties.Resources.curva_branco;
                ObservacoesPreset.Image = Properties.Resources.observacoes_branco;

                CustoPreset.Image = Properties.Resources.dinheiro_branco;
                CustoDolarPreset.Image = Properties.Resources.dinheiro_branco;
                VendaConsumidorPreset.Image = Properties.Resources.dinheiro_branco;
                RevendaPreset.Image = Properties.Resources.dinheiro_branco;
                VendaOutrosPreset.Image = Properties.Resources.dinheiro_branco;
                LucroReaisPreset.Image = Properties.Resources.dinheiro_branco;
                LucroDolarPreset.Image = Properties.Resources.dinheiro_branco;
                LucroPorcentoPreset.Image = Properties.Resources.porcento_branco;

                DisponivelPreset.Image = Properties.Resources.estoque_branco;
                IdealPreset.Image = Properties.Resources.ideal_branco;
                MinimaPreset.Image = Properties.Resources.minimo_branco;
                LocalizacaoPreset.Image = Properties.Resources.localizacao_branco;
                PrateleiraPreset.Image = Properties.Resources.prateleira_branco;

                Ean13Preset.Image = Properties.Resources.barcode_branco;
                Code128Preset.Image = Properties.Resources.barcode_branco;

                Voltar.Image = Properties.Resources.voltar_escuro;
                Editar.Image = Properties.Resources.edit_escuro;

                btnFirst.Image = Properties.Resources.seta_esquerda_dupla_branco1;
                btnBackward.Image = Properties.Resources.seta_esquerda_branco1;
                btnForward.Image = Properties.Resources.seta_direita_branco1;
                btnLast.Image = Properties.Resources.seta_direita_dupla_branco1;

                Crescente.ForeColor = Color.FromArgb(255, 33, 0);

                ToolStripDarkPanel.Visible = true;
            }
            else
            {
                FilterPesquisaBtn.Image = Properties.Resources.search_black;
                FilterCurvaBtn.Image = Properties.Resources.curva_preto;
                FilterFabricanteBtn.Image = Properties.Resources.fabricante_preto;
                FilterGrupoBtn.Image = Properties.Resources.unidade_tipo_grupo_preto;
                FilterTipoItemBtn.Image = Properties.Resources.unidade_tipo_grupo_preto;
                FilterFornecedoresBtn.Image = Properties.Resources.fornecedor_preto;
                FilterStatusBtn.Image = Properties.Resources.status_preto;

                DataEspecifica.Image = Properties.Resources.data_preto;

                SortBtn.Image = Properties.Resources.sort_preto;
                ViewOptionsBtn.Image = Properties.Resources.lista_claro;

                ListaCompacta.Image = Properties.Resources.lista_compacta_claro;

                Descrescente.Image = Properties.Resources.descending_claro;

                ExcelBtn.Image = Properties.Resources.excel_preto;
                PdfBtn.Image = Properties.Resources.pdf_preto;

                ExportCurrentExcel.Image = Properties.Resources.current_page;
                ExportAllExcel.Image = Properties.Resources.all_pages;

                ExportCurrentPdf.Image = Properties.Resources.current_page;
                ExportAllPdf.Image = Properties.Resources.all_pages;

                ExportOptionsBtn.Image = Properties.Resources.settings;

                StatusInfo.Image = Properties.Resources.status_preto;

                NumeroFabricantePreset.Image = Properties.Resources.number_cinza;
                NumeroOriginalPreset.Image = Properties.Resources.number_cinza;
                MarcaPreset.Image = Properties.Resources.fabricante_cinza;
                UnidadePreset.Image = Properties.Resources.unidade_tipo_grupo_cinza;
                TipoProdutoPreset.Image = Properties.Resources.unidade_tipo_grupo_cinza;
                GrupoPreset.Image = Properties.Resources.unidade_tipo_grupo_cinza;
                SubGrupoPreset.Image = Properties.Resources.unidade_tipo_grupo_cinza;
                FornecedorPreset.Image = Properties.Resources.fornecedor_cinza;
                UltimaVendaPreset.Image = Properties.Resources.entrada_claro;
                CurvaPreset.Image = Properties.Resources.curva_cinza;
                ObservacoesPreset.Image = Properties.Resources.observacoes_claro;

                CustoPreset.Image = Properties.Resources.dinheiro_cinza;
                CustoDolarPreset.Image = Properties.Resources.dinheiro_cinza;
                VendaConsumidorPreset.Image = Properties.Resources.dinheiro_cinza;
                RevendaPreset.Image = Properties.Resources.dinheiro_cinza;
                VendaOutrosPreset.Image = Properties.Resources.dinheiro_cinza;
                LucroReaisPreset.Image = Properties.Resources.dinheiro_cinza;
                LucroDolarPreset.Image = Properties.Resources.dinheiro_cinza;
                LucroPorcentoPreset.Image = Properties.Resources.porcento_cinza;

                DisponivelPreset.Image = Properties.Resources.disponivel_cinza;
                IdealPreset.Image = Properties.Resources.ideal_cinza;
                MinimaPreset.Image = Properties.Resources.minimo_cinza;
                LocalizacaoPreset.Image = Properties.Resources.localizacao_cinza;
                PrateleiraPreset.Image = Properties.Resources.prateleira_cinza;

                Ean13Preset.Image = Properties.Resources.barcode_cinza;
                Code128Preset.Image = Properties.Resources.barcode_cinza;

                Voltar.Image = Properties.Resources.voltar_claro;
                Editar.Image = Properties.Resources.edit_claro1;

                btnFirst.Image = Properties.Resources.seta_esquerda_dupla_cinza;
                btnBackward.Image = Properties.Resources.seta_esquerda_cinza;
                btnForward.Image = Properties.Resources.seta_direita_cinza;
                btnLast.Image = Properties.Resources.seta_direita_dupla_cinza;

                Crescente.ForeColor = Color.FromArgb(255, 3, 0);

                ToolStripDarkPanel.Visible = false;
            }

            if (servicosData.Servicos.Count == 0)
            {
                ServicosGrid.Visible = false;
                Separator2.Visible = false;
                toolStripPaging.Visible = false;
                Search.Visible = false;
                MostrarText.Visible = false;
                PageItens.Visible = false;
                FilterBtn.Visible = false;
                MoreOptionsBtn.Visible = false;
                ExportBtn.Visible = false;

                Illustration.Visible = true;
                Desc.Visible = true;
            }

            if (AnimateButtonsAndPanels)
            {
                foreach (Guna.UI2.WinForms.Guna2Button ct in GunaButtons)
                {
                    foreach (Guna.UI2.WinForms.Guna2Button ct2 in GunaMainButtons)
                    {
                        foreach (Guna.UI2.WinForms.Guna2RadioButton ct3 in GunaRadioButtons)
                        {
                            ct.Animated = true;
                            ct2.Animated = true;
                            ct3.Animated = true;
                        }
                    }
                }

                Search.Animated = true;
                RemoverFiltros.Animated = true;
                CustomDateBtn.Animated = true;
                Data1.Animated = true;
                Data2.Animated = true;

                Voltar.Animated = true;
                Editar.Animated = true;
                Excluir.Animated = true;

                DeleteAllSelected.Animated = true;
            }
            else
            {
                foreach (Guna.UI2.WinForms.Guna2Button ct in GunaButtons)
                {
                    foreach (Guna.UI2.WinForms.Guna2Button ct2 in GunaMainButtons)
                    {
                        foreach (Guna.UI2.WinForms.Guna2RadioButton ct3 in GunaRadioButtons)
                        {
                            ct.Animated = false;
                            ct2.Animated = false;
                            ct3.Animated = false;
                        }
                    }
                }

                Search.Animated = false;
                RemoverFiltros.Animated = false;
                CustomDateBtn.Animated = false;
                Data1.Animated = false;
                Data2.Animated = false;

                Voltar.Animated = false;
                Editar.Animated = false;
                Excluir.Animated = false;

                DeleteAllSelected.Animated = true;
            }

            if (TipoDeLista == "Normal")
            {
                ListaNormal.ForeColor = ThemeManager.RedFontColor;
                ListaNormal.Image = Properties.Resources.lista_red;

                if (IsDarkModeEnabled)
                {
                    ListaCompacta.ForeColor = Color.FromArgb(250, 250, 250);
                    ListaCompacta.Image = Properties.Resources.lista_compacta_branco;

                    ViewOptionsBtn.Image = Properties.Resources.lista_branco;
                }
                else
                {
                    ListaCompacta.ForeColor = Color.FromArgb(0, 0, 0);
                    ListaCompacta.Image = Properties.Resources.lista_compacta_claro;

                    ViewOptionsBtn.Image = Properties.Resources.lista_claro;
                }

                ServicosGrid.GridColor = ThemeManager.SeparatorAndBorderColor;
                ServicosGrid.RowTemplate.Height = 44;
            }

            else if (TipoDeLista == "Compacta")
            {
                if (IsDarkModeEnabled)
                {
                    ListaNormal.ForeColor = Color.FromArgb(250, 250, 250);
                    ListaNormal.Image = Properties.Resources.lista_branco;

                    ViewOptionsBtn.Image = Properties.Resources.lista_compacta_branco;
                }
                else
                {
                    ListaNormal.ForeColor = Color.FromArgb(0, 0, 0);
                    ListaNormal.Image = Properties.Resources.lista_claro;

                    ViewOptionsBtn.Image = Properties.Resources.lista_compacta_claro;
                }

                ListaCompacta.ForeColor = ThemeManager.RedFontColor;
                ListaCompacta.Image = Properties.Resources.lista_compacta_red;

                ServicosGrid.GridColor = ThemeManager.FormBackColor;
                ServicosGrid.RowTemplate.Height = 32;
            }

            if (TodosTecnicos.Checked)
                FabricanteOptions.Size = new Size(220, 64);
            else
                FabricanteOptions.Size = new Size(220, 154);

            if (TodosFornecedores.Checked)
                FornecedoresOptions.Size = new Size(220, 64);
            else
                FornecedoresOptions.Size = new Size(220, 154);

            PageItens.Text = Convert.ToString(PageRows);

            PagesCount = Convert.ToInt32(Math.Ceiling(servicosData.Servicos.Count * 1.0 / PageRows));
            CurrentPage = 0;
            RefreshPagination();
            ReloadPage();

            foreach (Form frm in fc)
            {
                if (frm.Name == "NovoProduto")
                {
                    NovoServico.Enabled = false;
                }
            }

            Data1.Checked = false;
            Data2.Checked = false;

            NotFind.Visible = false;
            NotFindDesc.Visible = false;

            System.Drawing.Rectangle ToolStripArea = toolStripPaging.Parent.ClientRectangle;
            toolStripPaging.Left = (ToolStripArea.Width - toolStripPaging.Width) / 2;

            System.Drawing.Rectangle DarkToolStripPanelArea = ToolStripDarkPanel.Parent.ClientRectangle;
            ToolStripDarkPanel.Left = (DarkToolStripPanelArea.Width - ToolStripDarkPanel.Width) / 2;

            toolStripPaging.Cursor = Cursors.Hand;

            VerifyTimer.Start();

            string strcon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True";
            OleDbConnection con = new OleDbConnection(strcon);

            con.Open();

            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT MARCA FROM Estoque";
            cmd.ExecuteNonQuery();

            OleDbCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT FORNECEDOR FROM Estoque";
            cmd2.ExecuteNonQuery();

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            OleDbDataAdapter adapter2 = new OleDbDataAdapter(cmd2);
            adapter.Fill(dt);
            adapter2.Fill(dt2);

            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            AutoCompleteStringCollection collection2 = new AutoCompleteStringCollection();

            foreach (DataRow dtr in dt.Rows)
            {
                collection.Add(dtr["MARCA"].ToString());
            }

            foreach (DataRow dtr in dt2.Rows)
            {
                collection2.Add(dtr["FORNECEDOR"].ToString());
            }

            PesquisarTecnico.AutoCompleteMode = AutoCompleteMode.Suggest;
            PesquisarTecnico.AutoCompleteSource = AutoCompleteSource.CustomSource;

            PesquisarFornecedor.AutoCompleteMode = AutoCompleteMode.Suggest;
            PesquisarFornecedor.AutoCompleteSource = AutoCompleteSource.CustomSource;

            PesquisarTecnico.AutoCompleteCustomSource = collection;
            PesquisarFornecedor.AutoCompleteCustomSource = collection2;

            con.Close();

            Properties.Settings.Default.CanUpdateGrid = false;

            FormLoaded = true;
        }

        private void Servicos_Click(object sender, EventArgs e)
        {
            HideFrames();
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Funcoes */

        // Delay
        async Task TaskDelay(int valor)
        {
            await Task.Delay(valor);
        }

        // Carregar dados e ajustar valores
        private void ReloadPage()
        {
            var query = from campos in servicosData.Servicos
                        select new
                        {
                            campos.NUMEROSERVICO,
                            campos.CLIENTE,
                            campos.ENDERECO,
                            campos.TELEFONE,
                            campos.CPFCNPJ,
                            campos.EMAIL,
                            campos.ENTRADA,
                            campos.PRONTO,
                            campos.STATUS,
                            campos.PRIORIDADE,
                            campos.VALORADIANTAMENTO,
                            campos.VALORMAODEOBRA,
                            campos.VALORPECAS,
                            campos.VALORDESLOCAMENTO,
                            campos.VALORSERVICOSTERCEIROS,
                            campos.VALOROUTROS,
                            campos.VALORTOTAL,
                            campos.VEICULOANO,
                            campos.MARCA,
                            campos.KILOMETRAGEM,
                            campos.PLACA,
                            campos.NUMEROCHASSIS,
                            campos.DEFEITO,
                            campos.ACESSORIOS,
                            campos.LAUDO,
                            campos.TECNICO
                        };

            servicosBindingSource.DataSource = query.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
            OrderByColumn(SortedByItem);
            PagesCount = Convert.ToInt32(Math.Ceiling(servicosData.Servicos.Count * 1.0 / PageRows));
        }

        public void UpdateGrid()
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

            if (SortedByOrder == "Ascending")
            {
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM Servicos", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    HideNewCustomerItens();

                    DataTable customrows = dt.AsEnumerable()
                    .Skip(PageRows * CurrentPage)
                    .Take(PageRows)
                    .ToList()
                    .CopyToDataTable();

                    // Serviços data
                    servicosBindingSource.DataSource = customrows;
                    servicosTableAdapter.Fill(servicosData.Servicos);

                    // All serviços data
                    servicosBindingSource1.DataSource = customrows;
                    servicosTableAdapter1.Fill(allServicosData.Servicos);

                    //OrderByColumn(SortedByItem);
                    PagesCount = Convert.ToInt32(Math.Ceiling(servicosData.Servicos.Count * 1.0 / PageRows));
                    con.Close();
                }
                else
                {
                    ServicosGrid.Visible = false;
                    Separator2.Visible = false;
                    toolStripPaging.Visible = false;
                    Search.Visible = false;
                    MostrarText.Visible = false;
                    PageItens.Visible = false;
                    FilterBtn.Visible = false;
                    MoreOptionsBtn.Visible = false;
                    ExportBtn.Visible = false;
                    SelectedOptions.Visible = false;

                    Illustration.Visible = true;
                    Desc.Visible = true;

                    Editar.Location = new Point(Editar.Location.X, 5557);
                    Excluir.Location = new Point(Excluir.Location.X, 5557);
                    Voltar.Location = new Point(Voltar.Location.X, 5557);

                    NovoServico.Visible = true;
                }
            }

            else if (SortedByOrder == "Descending")
            {
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM Servicos", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    HideNewCustomerItens();

                    DataTable customrows = dt.AsEnumerable()
                    .Skip(PageRows * CurrentPage)
                    .Take(PageRows)
                    .ToList()
                    .CopyToDataTable();

                    // Serviços data
                    servicosBindingSource.DataSource = customrows;
                    servicosTableAdapter.Fill(servicosData.Servicos);

                    // All serviços data
                    servicosBindingSource1.DataSource = customrows;
                    servicosTableAdapter1.Fill(allServicosData.Servicos);

                    //OrderByColumn(SortedByItem);
                    PagesCount = Convert.ToInt32(Math.Ceiling(servicosData.Servicos.Count * 1.0 / PageRows));
                    con.Close();
                }
                else
                {
                    ServicosGrid.Visible = false;
                    Separator2.Visible = false;
                    toolStripPaging.Visible = false;
                    Search.Visible = false;
                    MostrarText.Visible = false;
                    PageItens.Visible = false;
                    FilterBtn.Visible = false;
                    MoreOptionsBtn.Visible = false;
                    ExportBtn.Visible = false;
                    SelectedOptions.Visible = false;

                    Illustration.Visible = true;
                    Desc.Visible = true;

                    Editar.Location = new Point(Editar.Location.X, 5557);
                    Excluir.Location = new Point(Excluir.Location.X, 5557);
                    Voltar.Location = new Point(Voltar.Location.X, 5557);

                    NovoServico.Visible = true;
                }

                //OrderByColumn(SortedByItem);
                PagesCount = Convert.ToInt32(Math.Ceiling(servicosData.Servicos.Count * 1.0 / PageRows));
                con.Close();
            }

            RefreshPagination();

            Editar.Location = new Point(Editar.Location.X, 5557);
            Excluir.Location = new Point(Excluir.Location.X, 5557);
            Voltar.Location = new Point(Voltar.Location.X, 5557);

            NovoServico.Visible = true;
        }

        private void FilterFunction(string Field, string Filtro)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

            con.Open();

            OleDbCommand cmd = new OleDbCommand("SELECT * FROM Estoque WHERE [" + Field + "] = '" + Filtro + "'", con);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                HideNewCustomerItens();

                DataTable customrows = dt.AsEnumerable()
                .Skip(PageRows * CurrentPage)
                .Take(PageRows)
                .ToList()
                .CopyToDataTable();

                DataTable customrows2 = dt;

                // Serviços data
                servicosBindingSource.DataSource = customrows;
                servicosTableAdapter.Fill(servicosData.Servicos);

                // All serviços data
                servicosBindingSource1.DataSource = customrows2;
                servicosTableAdapter1.Fill(allServicosData.Servicos);

                PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                con.Close();

                ServicosGrid.Visible = true;
                Separator2.Visible = true;
                toolStripPaging.Visible = true;

                NotFind.Visible = false;

                NotFindDesc.Visible = false;
            }
            else
            {
                NotFindDesc.Text = "Nenhum resultado que corresponda \n ao seu filtro atual foi encontrado.";

                ServicosGrid.Visible = false;
                Separator2.Visible = false;
                toolStripPaging.Visible = false;

                NotFind.Visible = true;

                NotFindDesc.Visible = true;
            }

            RefreshPagination();
        }

        // Criar/Configurar paginas
        public void RefreshPagination()
        {
            ToolStripButton[] items = new ToolStripButton[] { toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripButton5 };

            int pageStartIndex = 0;

            if (PagesCount > 5 && CurrentPage > 2)
                pageStartIndex = CurrentPage - 2;

            if (PagesCount > 5 && CurrentPage > PagesCount - 3)
                pageStartIndex = PagesCount - 5;

            for (int i = pageStartIndex; i < pageStartIndex + 5; i++)
            {
                if (i > PagesCount - 1)
                {
                    items[i - pageStartIndex].Visible = false;
                }
                else
                {
                    items[i - pageStartIndex].Visible = true;
                    items[i - pageStartIndex].Text = i.ToString(CultureInfo.InvariantCulture);

                    if (IsDarkModeEnabled)
                    {
                        if (i == CurrentPage)
                        {
                            items[i - pageStartIndex].BackColor = ThemeManager.FormBackColor;
                            items[i - pageStartIndex].ForeColor = Color.White;
                            items[i - pageStartIndex].BackgroundImage = Properties.Resources.PageBack;
                        }
                        else
                        {
                            items[i - pageStartIndex].BackColor = ThemeManager.FormBackColor;
                            items[i - pageStartIndex].ForeColor = Color.FromArgb(250, 250, 250);
                            items[i - pageStartIndex].BackgroundImage = null;
                        }
                    }
                    else
                    {
                        if (i == CurrentPage)
                        {
                            items[i - pageStartIndex].BackColor = ThemeManager.FormBackColor;
                            items[i - pageStartIndex].ForeColor = Color.White;
                            items[i - pageStartIndex].BackgroundImage = Properties.Resources.PageBack;
                        }
                        else
                        {
                            items[i - pageStartIndex].BackColor = ThemeManager.FormBackColor;
                            items[i - pageStartIndex].ForeColor = Color.FromArgb(60, 60, 60);
                            items[i - pageStartIndex].BackgroundImage = null;
                        }
                    }
                }
            }

            if (CurrentPage == 0)
                btnBackward.Enabled = btnFirst.Enabled = false;
            else
                btnBackward.Enabled = btnFirst.Enabled = true;

            if (CurrentPage == PagesCount - 1)
                btnForward.Enabled = btnLast.Enabled = false;
            else
                btnForward.Enabled = btnLast.Enabled = true;
        }

        // Botoes de pagina
        private void ToolStripButtonClick(object sender, EventArgs e)
        {
            try
            {
                ToolStripButton ToolStripButton = (ToolStripButton)sender;

                if (CurrentPage < 0)
                    CurrentPage = 0;
                else if (CurrentPage > PagesCount)
                    CurrentPage = PagesCount;

                if (ToolStripButton == btnBackward)
                    CurrentPage--;
                else if (ToolStripButton == btnForward)
                    CurrentPage++;
                else if (ToolStripButton == btnLast)
                    CurrentPage = PagesCount - 1;
                else if (ToolStripButton == btnFirst)
                    CurrentPage = 0;
                else
                    CurrentPage = Convert.ToInt32(ToolStripButton.Text, CultureInfo.InvariantCulture);

                if (DataFiltered)
                {
                    DateFunction(DateTime.Now.AddDays(-FilterByDays));
                }
                else
                {
                    ReloadPage();
                    RefreshPagination();
                }
            }
            catch (Exception) { }
        }

        /*--------------------------*/

        // Selecionar data pra filtrar
        private void DateChecked(object sender, EventArgs e)
        {
            if (TodoPeriodo.Checked)
            {
                ReloadPage();
                RefreshPagination();

                DataFiltered = false;
                FilterByDays = 0;
            }

            else if (Hoje.Checked)
            {
                DateFunction(DateTime.Now);

                RemoverFiltros.Visible = true;
                DataFiltered = true;
                FilterByDays = 1;
            }

            else if (Semana.Checked)
            {
                DateFunction(DateTime.Now.AddDays(-8));

                RemoverFiltros.Visible = true;
                DataFiltered = true;
                FilterByDays = 8;
            }

            else if (Mes.Checked)
            {
                DateFunction(DateTime.Now.AddDays(-31));

                RemoverFiltros.Visible = true;
                DataFiltered = true;
                FilterByDays = 31;
            }

            else if (Ano.Checked)
            {
                DateFunction(DateTime.Now.AddDays(-366));

                RemoverFiltros.Visible = true;
                DataFiltered = true;
                FilterByDays = 366;
            }

            DataOptions.Location = new Point(5512, DataOptions.Location.Y);

            if (DataFiltered)
                ColorFilter(ActiveFilter7);
            else
            {
                if (All.Checked != true)
                    RemoverFiltros.Visible = false;
            }

            OrderByColumn(SortedByItem);
            HideFrames();
        }

        // Filtrar data
        private void DateFunction(DateTime data)
        {
            var query = from campos in servicosData.Servicos
                        where campos.ENTRADA >= data
                        orderby campos.ENTRADA <= campos.ENTRADA

                        select campos;

            FilteredProducts = query.Count();

            servicosBindingSource.DataSource = query.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
            servicosBindingSource1.DataSource = query;
            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));

            RefreshPagination();
            NotFinded();
        }

        /*--------------------------*/

        // Selecionar item pra classificar
        private void SortItens(object sender, EventArgs e)
        {
            if (NumeroServicoSort.Checked)
                SortedByItem = "Curva";

            else if (ClienteSort.Checked)
                SortedByItem = "Codigo";

            else if (TelefoneSort.Checked)
                SortedByItem = "Produto";

            else if (VeiculoSort.Checked)
                SortedByItem = "NumeroFabricante";

            else if (EntradaSort.Checked)
                SortedByItem = "Marca";

            else if (ProntoSort.Checked)
                SortedByItem = "Grupo";

            else if (ValorTotalSort.Checked)
                SortedByItem = "Tipo";

            else if (TecnicoSort.Checked)
                SortedByItem = "Disponivel";

            else if (StatusSort.Checked)
                SortedByItem = "Status";
        }

        // Classificar itens por ordem alfabetica
        private void OrderByColumn(string Campo)
        {
            if (DataFiltered)
            {
                if (SortedByOrder == "Descending")
                {
                    switch (Campo)
                    {
                        case "NumeroServico":
                            var CurvaQuery = from campos in servicosData.Servicos
                                          orderby campos.NUMEROSERVICO descending
                                          where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                          select campos;

                            servicosBindingSource.DataSource = CurvaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = CurvaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Cliente":
                            var IDQuery = from campos in servicosData.Servicos
                                          orderby campos.CLIENTE descending
                                          where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                          select campos;

                            servicosBindingSource.DataSource = IDQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = IDQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Telefone":
                            var NomeQuery = from campos in servicosData.Servicos
                                            orderby campos.TELEFONE descending
                                            where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                            select campos;

                            servicosBindingSource.DataSource = NomeQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = NomeQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Veiculo":
                            var TelefoneQuery = from campos in servicosData.Servicos
                                                orderby campos.VEICULOANO descending
                                                where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                select campos;

                            servicosBindingSource.DataSource = TelefoneQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = TelefoneQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Entrada":
                            var CpfQuery = from campos in servicosData.Servicos
                                           orderby campos.ENTRADA descending
                                           where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                           select campos;

                            servicosBindingSource.DataSource = CpfQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = CpfQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Pronto":
                            var PessoaQuery = from campos in servicosData.Servicos
                                              orderby campos.PRONTO descending
                                              where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                              select campos;

                            servicosBindingSource.DataSource = PessoaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = PessoaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "ValorTotal":
                            var EnderecoQuery = from campos in servicosData.Servicos
                                                orderby campos.VALORTOTAL descending
                                                where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                select campos;

                            servicosBindingSource.DataSource = EnderecoQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = EnderecoQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Tecnico":
                            var CidadeQuery = from campos in servicosData.Servicos
                                              orderby campos.TECNICO descending
                                              where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                              select campos;

                            servicosBindingSource.DataSource = CidadeQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = CidadeQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Status":
                            var ENTRADAQuery = from campos in servicosData.Servicos
                                                   orderby campos.STATUS  descending
                                                   where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                   select campos;

                            servicosBindingSource.DataSource = ENTRADAQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = ENTRADAQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;
                    }
                }

                else if (SortedByOrder == "Ascending")
                {
                    switch (Campo)
                    {
                        case "NumeroServico":
                            var CurvaQuery = from campos in servicosData.Servicos
                                             orderby campos.NUMEROSERVICO ascending
                                             where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                             select campos;

                            servicosBindingSource.DataSource = CurvaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = CurvaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Cliente":
                            var IDQuery = from campos in servicosData.Servicos
                                          orderby campos.CLIENTE ascending
                                          where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                          select campos;

                            servicosBindingSource.DataSource = IDQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = IDQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Telefone":
                            var NomeQuery = from campos in servicosData.Servicos
                                            orderby campos.TELEFONE ascending
                                            where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                            select campos;

                            servicosBindingSource.DataSource = NomeQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = NomeQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Veiculo":
                            var TelefoneQuery = from campos in servicosData.Servicos
                                                orderby campos.VEICULOANO ascending
                                                where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                select campos;

                            servicosBindingSource.DataSource = TelefoneQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = TelefoneQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Entrada":
                            var CpfQuery = from campos in servicosData.Servicos
                                           orderby campos.ENTRADA ascending
                                           where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                           select campos;

                            servicosBindingSource.DataSource = CpfQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = CpfQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Pronto":
                            var PessoaQuery = from campos in servicosData.Servicos
                                              orderby campos.PRONTO ascending
                                              where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                              select campos;

                            servicosBindingSource.DataSource = PessoaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = PessoaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "ValorTotal":
                            var EnderecoQuery = from campos in servicosData.Servicos
                                                orderby campos.VALORTOTAL ascending
                                                where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                select campos;

                            servicosBindingSource.DataSource = EnderecoQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = EnderecoQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Tecnico":
                            var CidadeQuery = from campos in servicosData.Servicos
                                              orderby campos.TECNICO ascending
                                              where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                              select campos;

                            servicosBindingSource.DataSource = CidadeQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = CidadeQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Status":
                            var ENTRADAQuery = from campos in servicosData.Servicos
                                               orderby campos.STATUS ascending
                                               where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                               select campos;

                            servicosBindingSource.DataSource = ENTRADAQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = ENTRADAQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;
                    }
                }
            }
            else
            {
                if (SortedByOrder == "Descending")
                {
                    switch (Campo)
                    {
                        case "NumeroServico":
                            var CurvaQuery = from campos in servicosData.Servicos
                                             orderby campos.NUMEROSERVICO descending
                                             where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                             select campos;

                            servicosBindingSource.DataSource = CurvaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = CurvaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Cliente":
                            var IDQuery = from campos in servicosData.Servicos
                                          orderby campos.CLIENTE descending
                                          where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                          select campos;

                            servicosBindingSource.DataSource = IDQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = IDQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Telefone":
                            var NomeQuery = from campos in servicosData.Servicos
                                            orderby campos.TELEFONE descending
                                            where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                            select campos;

                            servicosBindingSource.DataSource = NomeQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = NomeQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Veiculo":
                            var TelefoneQuery = from campos in servicosData.Servicos
                                                orderby campos.VEICULOANO descending
                                                where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                select campos;

                            servicosBindingSource.DataSource = TelefoneQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = TelefoneQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Entrada":
                            var CpfQuery = from campos in servicosData.Servicos
                                           orderby campos.ENTRADA descending
                                           where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                           select campos;

                            servicosBindingSource.DataSource = CpfQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = CpfQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Pronto":
                            var PessoaQuery = from campos in servicosData.Servicos
                                              orderby campos.PRONTO descending
                                              where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                              select campos;

                            servicosBindingSource.DataSource = PessoaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = PessoaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "ValorTotal":
                            var EnderecoQuery = from campos in servicosData.Servicos
                                                orderby campos.VALORTOTAL descending
                                                where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                select campos;

                            servicosBindingSource.DataSource = EnderecoQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = EnderecoQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Tecnico":
                            var CidadeQuery = from campos in servicosData.Servicos
                                              orderby campos.TECNICO descending
                                              where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                              select campos;

                            servicosBindingSource.DataSource = CidadeQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = CidadeQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Status":
                            var ENTRADAQuery = from campos in servicosData.Servicos
                                               orderby campos.STATUS descending
                                               where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                               select campos;

                            servicosBindingSource.DataSource = ENTRADAQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = ENTRADAQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;
                    }
                }
                else if (SortedByOrder == "Ascending")
                {
                    switch (Campo)
                    {
                        case "NumeroServico":
                            var CurvaQuery = from campos in servicosData.Servicos
                                             orderby campos.NUMEROSERVICO ascending
                                             where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                             select campos;

                            servicosBindingSource.DataSource = CurvaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = CurvaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Cliente":
                            var IDQuery = from campos in servicosData.Servicos
                                          orderby campos.CLIENTE ascending
                                          where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                          select campos;

                            servicosBindingSource.DataSource = IDQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = IDQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Telefone":
                            var NomeQuery = from campos in servicosData.Servicos
                                            orderby campos.TELEFONE ascending
                                            where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                            select campos;

                            servicosBindingSource.DataSource = NomeQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = NomeQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Veiculo":
                            var TelefoneQuery = from campos in servicosData.Servicos
                                                orderby campos.VEICULOANO ascending
                                                where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                select campos;

                            servicosBindingSource.DataSource = TelefoneQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = TelefoneQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Entrada":
                            var CpfQuery = from campos in servicosData.Servicos
                                           orderby campos.ENTRADA ascending
                                           where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                           select campos;

                            servicosBindingSource.DataSource = CpfQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = CpfQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Pronto":
                            var PessoaQuery = from campos in servicosData.Servicos
                                              orderby campos.PRONTO ascending
                                              where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                              select campos;

                            servicosBindingSource.DataSource = PessoaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = PessoaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "ValorTotal":
                            var EnderecoQuery = from campos in servicosData.Servicos
                                                orderby campos.VALORTOTAL ascending
                                                where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                select campos;

                            servicosBindingSource.DataSource = EnderecoQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = EnderecoQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Tecnico":
                            var CidadeQuery = from campos in servicosData.Servicos
                                              orderby campos.TECNICO ascending
                                              where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                              select campos;

                            servicosBindingSource.DataSource = CidadeQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = CidadeQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Status":
                            var ENTRADAQuery = from campos in servicosData.Servicos
                                               orderby campos.STATUS ascending
                                               where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                               select campos;

                            servicosBindingSource.DataSource = ENTRADAQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            servicosBindingSource1.DataSource = ENTRADAQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;
                    }
                }
            }

            RefreshPagination();
            NotFinded();
            CheckedStateFunction();
        }

        /*--------------------------*/

        // Mudar filtro de pesquisa
        private void CheckedState(object sender, EventArgs e)
        {

        }

        private void CheckedStateFunction()
        {

        }

        /*--------------------------*/

        // Mudar filtro de curva
        private async void CurvaChecked(object sender, EventArgs e)
        {
            if (TodasCurvas.Checked != true)
            {
                if (CurvaA.Checked)
                {
                    FilterFunction("CURVA", "A");
                    CurvaFilteredItem = "A";
                }
                else if (CurvaB.Checked)
                {
                    FilterFunction("CURVA", "B");
                    CurvaFilteredItem = "B";
                }
                else if (CurvaC.Checked)
                {
                    FilterFunction("CURVA", "C");
                    CurvaFilteredItem = "C";
                }

                RemoverFiltros.Visible = true;
                ColorFilter(ActiveFilter2);
            }
            else
            {
                RemoveFilterColor(ActiveFilter2);

                if (ActiveFilter1.Visible || ActiveFilter3.Visible || ActiveFilter4.Visible || ActiveFilter5.Visible || ActiveFilter6.Visible || ActiveFilter7.Visible)
                {
                    if (ActiveFilter1.Visible)
                        Console.WriteLine("Search filter ativado");
                    else if (ActiveFilter3.Visible)
                        FilterFunction("FABRICANTE", FabricanteFilteredItem);
                    else if (ActiveFilter4.Visible)
                        FilterFunction("GRUPO", GrupoFilteredItem);
                    else if (ActiveFilter5.Visible)
                        FilterFunction("TIPO", TipoFilteredItem);
                    else if (ActiveFilter6.Visible)
                        FilterFunction("FORNECEDOR", FornecedorFilteredItem);
                    else if (ActiveFilter7.Visible)
                        FilterFunction("STATUS", StatusFilteredItem);

                    FilterBtn.BorderColor = ThemeManager.BorderRedButtonBorderColor;
                    FilterBtn.HoverState.FillColor = ThemeManager.BorderRedButtonHoverFillColor;
                    FilterBtn.HoverState.BorderColor = ThemeManager.BorderRedButtonHoverBorderColor;
                    FilterBtn.PressedColor = ThemeManager.BorderRedButtonPressedColor;
                    FilterBtn.Image = Properties.Resources.filtro___red;
                    ActiveFilter2.Visible = false;
                    RemoverFiltros.Visible = true;
                }
                else
                {
                    RemoveFilterColor(ActiveFilter2);

                    ReloadPage();
                    RefreshPagination();
                }
            }

            await TaskDelay(100);

            HideFrames();
        }

        // Mudar filtro de fabricantes
        private async void FabricanteChecked(object sender, EventArgs e)
        {
            if (TodosTecnicos.Checked != true)
            {
                FabricanteOptions.Size = new Size(220, 154);

                if (PesquisarTecnico.Text != "")
                {
                    FilterFunction("MARCA", PesquisarTecnico.Text);
                    FabricanteFilteredItem = PesquisarTecnico.Text;
                }

                RemoverFiltros.Visible = true;
                ColorFilter(ActiveFilter3);
            }
            else
            {
                FabricanteOptions.Size = new Size(220, 64);

                RemoveFilterColor(ActiveFilter3);

                if (ActiveFilter1.Visible || ActiveFilter2.Visible || ActiveFilter4.Visible || ActiveFilter5.Visible || ActiveFilter6.Visible || ActiveFilter7.Visible)
                {
                    if (ActiveFilter1.Visible)
                        Console.WriteLine("Search filter ativado");
                    else if (ActiveFilter2.Visible)
                        FilterFunction("CURVA", CurvaFilteredItem);
                    else if (ActiveFilter4.Visible)
                        FilterFunction("GRUPO", GrupoFilteredItem);
                    else if (ActiveFilter5.Visible)
                        FilterFunction("TIPO", TipoFilteredItem);
                    else if (ActiveFilter6.Visible)
                        FilterFunction("FORNECEDOR", FornecedorFilteredItem);
                    else if (ActiveFilter7.Visible)
                        FilterFunction("STATUS", StatusFilteredItem);

                    FilterBtn.BorderColor = ThemeManager.BorderRedButtonBorderColor;
                    FilterBtn.HoverState.FillColor = ThemeManager.BorderRedButtonHoverFillColor;
                    FilterBtn.HoverState.BorderColor = ThemeManager.BorderRedButtonHoverBorderColor;
                    FilterBtn.PressedColor = ThemeManager.BorderRedButtonPressedColor;
                    FilterBtn.Image = Properties.Resources.filtro___red;
                    ActiveFilter3.Visible = false;
                    RemoverFiltros.Visible = true;
                }
                else
                {
                    RemoveFilterColor(ActiveFilter3);

                    ReloadPage();
                    RefreshPagination();
                }
            }

            await TaskDelay(100);

            HideFrames();
        }

        private void SearchFabricante()
        {
            DataView dv = servicosData.Servicos.DefaultView;

            if (DataFiltered)
            {
                dv.RowFilter = "MARCA LIKE '%" + PesquisarTecnico.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                servicosBindingSource.DataSource = dv;
                servicosBindingSource1.DataSource = dv;
            }
            else
            {
                dv.RowFilter = "MARCA LIKE '%" + PesquisarTecnico.Text + "%'";
                servicosBindingSource.DataSource = dv;
                servicosBindingSource1.DataSource = dv;
            }
        }

        // Mudar filtro de grupo
        private async void GrupoChecked(object sender, EventArgs e)
        {
            if (TodosGrupos.Checked != true)
            {
                if (Mecanica.Checked)
                {
                    FilterFunction("GRUPO", "Mecânica");
                    GrupoFilteredItem = "Mecânica";
                }
                else if (Eletrica.Checked)
                {
                    FilterFunction("GRUPO", "Elétrica");
                    GrupoFilteredItem = "Elétrica";
                }

                RemoverFiltros.Visible = true;
                ColorFilter(ActiveFilter4);
            }
            else
            {
                RemoveFilterColor(ActiveFilter4);

                if (ActiveFilter1.Visible || ActiveFilter2.Visible || ActiveFilter3.Visible || ActiveFilter5.Visible || ActiveFilter6.Visible || ActiveFilter7.Visible)
                {
                    if (ActiveFilter1.Visible)
                        Console.WriteLine("Search filter ativado");
                    else if (ActiveFilter2.Visible)
                        FilterFunction("CURVA", CurvaFilteredItem);
                    else if (ActiveFilter3.Visible)
                        FilterFunction("MARCA", FabricanteFilteredItem);
                    else if (ActiveFilter5.Visible)
                        FilterFunction("TIPO", TipoFilteredItem);
                    else if (ActiveFilter6.Visible)
                        FilterFunction("FORNECEDOR", FornecedorFilteredItem);
                    else if (ActiveFilter7.Visible)
                        FilterFunction("STATUS", StatusFilteredItem);

                    FilterBtn.BorderColor = ThemeManager.BorderRedButtonBorderColor;
                    FilterBtn.HoverState.FillColor = ThemeManager.BorderRedButtonHoverFillColor;
                    FilterBtn.HoverState.BorderColor = ThemeManager.BorderRedButtonHoverBorderColor;
                    FilterBtn.PressedColor = ThemeManager.BorderRedButtonPressedColor;
                    FilterBtn.Image = Properties.Resources.filtro___red;
                    ActiveFilter4.Visible = false;
                    RemoverFiltros.Visible = true;
                }
                else
                {
                    RemoveFilterColor(ActiveFilter4);

                    ReloadPage();
                    RefreshPagination();
                }
            }

            await TaskDelay(100);

            HideFrames();
        }

        // Mudar filtro de tipo de item
        private async void TipoItemChecked(object sender, EventArgs e)
        {
            if (TodosItens.Checked != true)
            {
                if (Comprado.Checked)
                {
                    FilterFunction("TIPO", "Comprado");
                    TipoFilteredItem = "Comprado";
                }
                else if (Kit.Checked)
                {
                    FilterFunction("TIPO", "Kit/Conjunto");
                    TipoFilteredItem = "Kit/Conjunto";
                }

                RemoverFiltros.Visible = true;
                ColorFilter(ActiveFilter5);
            }
            else
            {
                RemoveFilterColor(ActiveFilter5);

                if (ActiveFilter1.Visible || ActiveFilter2.Visible || ActiveFilter3.Visible || ActiveFilter4.Visible || ActiveFilter6.Visible || ActiveFilter7.Visible)
                {
                    if (ActiveFilter1.Visible)
                        Console.WriteLine("Search filter ativado");
                    else if (ActiveFilter2.Visible)
                        FilterFunction("CURVA", CurvaFilteredItem);
                    else if (ActiveFilter3.Visible)
                        FilterFunction("MARCA", FabricanteFilteredItem);
                    else if (ActiveFilter4.Visible)
                        FilterFunction("GRUPO", GrupoFilteredItem);
                    else if (ActiveFilter6.Visible)
                        FilterFunction("FORNECEDOR", FornecedorFilteredItem);
                    else if (ActiveFilter7.Visible)
                        FilterFunction("STATUS", StatusFilteredItem);

                    FilterBtn.BorderColor = ThemeManager.BorderRedButtonBorderColor;
                    FilterBtn.HoverState.FillColor = ThemeManager.BorderRedButtonHoverFillColor;
                    FilterBtn.HoverState.BorderColor = ThemeManager.BorderRedButtonHoverBorderColor;
                    FilterBtn.PressedColor = ThemeManager.BorderRedButtonPressedColor;
                    FilterBtn.Image = Properties.Resources.filtro___red;
                    ActiveFilter5.Visible = false;
                    RemoverFiltros.Visible = true;
                }
                else
                {
                    RemoveFilterColor(ActiveFilter5);

                    ReloadPage();
                    RefreshPagination();
                }
            }

            await TaskDelay(100);

            HideFrames();
        }

        // Mudar filtro de fornecedor
        private async void FornecedorChecked(object sender, EventArgs e)
        {
            if (TodosFornecedores.Checked != true)
            {
                FornecedoresOptions.Size = new Size(220, 154);

                if (PesquisarFornecedor.Text != "")
                {
                    FilterFunction("FORNECEDOR", PesquisarFornecedor.Text);
                    FornecedorFilteredItem = PesquisarFornecedor.Text;
                }

                RemoverFiltros.Visible = true;
                ColorFilter(ActiveFilter6);
            }
            else
            {
                FornecedoresOptions.Size = new Size(220, 64);

                RemoveFilterColor(ActiveFilter6);

                if (ActiveFilter1.Visible || ActiveFilter2.Visible || ActiveFilter3.Visible || ActiveFilter4.Visible || ActiveFilter5.Visible || ActiveFilter7.Visible)
                {
                    if (ActiveFilter1.Visible)
                        Console.WriteLine("Search filter ativado");
                    else if (ActiveFilter2.Visible)
                        FilterFunction("CURVA", CurvaFilteredItem);
                    else if (ActiveFilter3.Visible)
                        FilterFunction("FABRICANTE", FabricanteFilteredItem);
                    else if (ActiveFilter4.Visible)
                        FilterFunction("GRUPO", GrupoFilteredItem);
                    else if (ActiveFilter5.Visible)
                        FilterFunction("TIPO", TipoFilteredItem);
                    else if (ActiveFilter7.Visible)
                        FilterFunction("STATUS", StatusFilteredItem);

                    FilterBtn.BorderColor = ThemeManager.BorderRedButtonBorderColor;
                    FilterBtn.HoverState.FillColor = ThemeManager.BorderRedButtonHoverFillColor;
                    FilterBtn.HoverState.BorderColor = ThemeManager.BorderRedButtonHoverBorderColor;
                    FilterBtn.PressedColor = ThemeManager.BorderRedButtonPressedColor;
                    FilterBtn.Image = Properties.Resources.filtro___red;
                    ActiveFilter6.Visible = false;
                    RemoverFiltros.Visible = true;
                }
                else
                {
                    RemoveFilterColor(ActiveFilter6);

                    ReloadPage();
                    RefreshPagination();
                }

                await TaskDelay(100);

                HideFrames();
            }
        }

        // Mudar filtro de status
        private async void StatusChecked(object sender, EventArgs e)
        {
            if (TodosStatus.Checked != true)
            {
                if (AguardandoOrcamento.Checked)
                {
                    FilterFunction("STATUS", "Aguardando autorização de orçamento");
                    StatusFilteredItem = "Aguardando autorização de orçamento";
                }
                else if (AguardandoAutorizacao.Checked)
                {
                    FilterFunction("STATUS", "Aguardando avaliação do técnico");
                    StatusFilteredItem = "Aguardando avaliação do técnico";
                }
                else if (AguardandoPeca.Checked)
                {
                    FilterFunction("STATUS", "Autorizado, aguardando peça");
                    StatusFilteredItem = "Autorizado, aguardando peça";
                }
                else if (ReparoEmAndamento.Checked)
                {
                    FilterFunction("STATUS", "Autorizado, reparo em andamento");
                    StatusFilteredItem = "Autorizado, reparo em andamento";
                }
                else if (ProntoAvisar.Checked)
                {
                    FilterFunction("STATUS", "Pronto, avisar cliente");
                    StatusFilteredItem = "Pronto, avisar cliente";
                }
                else if (ProntoAvisado.Checked)
                {
                    FilterFunction("STATUS", "Pronto, cliente avisado");
                    StatusFilteredItem = "Pronto, cliente avisado";
                }

                RemoverFiltros.Visible = true;
                ColorFilter(ActiveFilter7);
            }
            else
            {
                RemoveFilterColor(ActiveFilter7);

                if (ActiveFilter1.Visible || ActiveFilter2.Visible || ActiveFilter3.Visible || ActiveFilter4.Visible || ActiveFilter5.Visible || ActiveFilter6.Visible)
                {
                    if (ActiveFilter1.Visible)
                        Console.WriteLine("Search filter ativado");
                    else if (ActiveFilter2.Visible)
                        FilterFunction("CURVA", CurvaFilteredItem);
                    else if (ActiveFilter3.Visible)
                        FilterFunction("MARCA", FabricanteFilteredItem);
                    else if (ActiveFilter4.Visible)
                        FilterFunction("GRUPO", GrupoFilteredItem);
                    else if (ActiveFilter5.Visible)
                        FilterFunction("TIPO", TipoFilteredItem);
                    else if (ActiveFilter6.Visible)
                        FilterFunction("FORNECEDOR", FornecedorFilteredItem);

                    FilterBtn.BorderColor = ThemeManager.BorderRedButtonBorderColor;
                    FilterBtn.HoverState.FillColor = ThemeManager.BorderRedButtonHoverFillColor;
                    FilterBtn.HoverState.BorderColor = ThemeManager.BorderRedButtonHoverBorderColor;
                    FilterBtn.PressedColor = ThemeManager.BorderRedButtonPressedColor;
                    FilterBtn.Image = Properties.Resources.filtro___red;
                    ActiveFilter7.Visible = false;
                    RemoverFiltros.Visible = true;
                }
                else
                {
                    RemoveFilterColor(ActiveFilter7);

                    ReloadPage();
                    RefreshPagination();
                }
            }

            await TaskDelay(100);

            HideFrames();
        }

        /*--------------------------*/

        // Filtro de pesquisa
        private void SearchFilter()
        {
            DataView dv = servicosData.Servicos.DefaultView;

            if (DataFiltered)
            {
                if (All.Checked)
                {
                    dv.RowFilter = string.Format("convert (CODIGO, 'System.String') LIKE '%" + Search.Text + "%' OR CURVA LIKE '%" + Search.Text + "%' OR CODIGO LIKE '%" + Search.Text + "%' OR PRODUTO LIKE '%" + Search.Text + "%' \n" +
                        "OR NUMEROFABRICANTE LIKE '%" + Search.Text + "%' OR MARCA LIKE '%" + Search.Text + "%' OR GRUPO LIKE '%" + Search.Text + "%' OR TIPO LIKE '%" + Search.Text + "%' OR QNTDDISPONIVEL LIKE '%" + Search.Text + "%' \n" +
                        "OR STATUS LIKE '%" + Search.Text + "%' AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'");
                    servicosBindingSource.DataSource = dv;
                    servicosBindingSource1.DataSource = dv;
                }

                else if (NumeroServico.Checked)
                {
                    dv.RowFilter = "CURVA LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    servicosBindingSource.DataSource = dv;
                    servicosBindingSource1.DataSource = dv;
                }

                else if (Cliente.Checked)
                {
                    dv.RowFilter = "convert(CODIGO, 'System.String') LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    servicosBindingSource.DataSource = dv;
                    servicosBindingSource1.DataSource = dv;
                }

                else if (Telefone.Checked)
                {
                    dv.RowFilter = "PRODUTO LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    servicosBindingSource.DataSource = dv;
                    servicosBindingSource1.DataSource = dv;
                }

                else if (Veiculo.Checked)
                {
                    dv.RowFilter = "NUMEROFABRICANTE LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    servicosBindingSource.DataSource = dv;
                    servicosBindingSource1.DataSource = dv;
                }

                else if (ValorTotal.Checked)
                {
                    dv.RowFilter = "MARCA LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    servicosBindingSource.DataSource = dv;
                    servicosBindingSource1.DataSource = dv;
                }

                else if (Tecnico.Checked)
                {
                    dv.RowFilter = "GRUPO LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    servicosBindingSource.DataSource = dv;
                    servicosBindingSource1.DataSource = dv;
                }

                else if (Status.Checked)
                {
                    dv.RowFilter = "STATUS LIKE '%" + Search.Text + "%' " +
                         "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    servicosBindingSource.DataSource = dv;
                    servicosBindingSource1.DataSource = dv;
                }

            }
            else
            {
                if (All.Checked)
                {
                    dv.RowFilter = string.Format("convert (CODIGO, 'System.String') LIKE '%" + Search.Text + "%' OR CURVA LIKE '%" + Search.Text + "%' OR PRODUTO LIKE '%" + Search.Text + "%' OR NUMEROFABRICANTE LIKE '%" + Search.Text + "%' " +
                        "OR MARCA LIKE '%" + Search.Text + "%' OR GRUPO LIKE '%" + Search.Text + "%' OR TIPO LIKE '%" + Search.Text + "%' OR convert (QNTDDISPONIVEL, 'System.String') LIKE '%" + Search.Text + "%' OR STATUS LIKE '%" + Search.Text + "%'");
                    servicosBindingSource.DataSource = dv;
                    servicosBindingSource1.DataSource = dv;
                }

                else if (NumeroServico.Checked)
                {
                    dv.RowFilter = string.Format("CURVA LIKE '%" + Search.Text + "%'");
                    servicosBindingSource.DataSource = dv;
                    servicosBindingSource1.DataSource = dv;
                }

                else if (Cliente.Checked)
                {
                    dv.RowFilter = "convert (CODIGO, 'System.String') LIKE '%" + Search.Text + "%'";
                    servicosBindingSource.DataSource = dv;
                    servicosBindingSource1.DataSource = dv;
                }

                else if (Telefone.Checked)
                {
                    dv.RowFilter = "PRODUTO LIKE '%" + Search.Text + "%'";
                    servicosBindingSource.DataSource = dv;
                    servicosBindingSource1.DataSource = dv;
                }

                else if (Veiculo.Checked)
                {
                    dv.RowFilter = "NUMEROFABRICANTE LIKE '%" + Search.Text + "%'";
                    servicosBindingSource.DataSource = dv;
                    servicosBindingSource1.DataSource = dv;
                }

                else if (ValorTotal.Checked)
                {
                    dv.RowFilter = "MARCA LIKE '%" + Search.Text + "%'";
                    servicosBindingSource.DataSource = dv;
                    servicosBindingSource1.DataSource = dv;
                }

                else if (Tecnico.Checked)
                {
                    dv.RowFilter = "GRUPO LIKE '%" + Search.Text + "%'";
                    servicosBindingSource.DataSource = dv;
                    servicosBindingSource1.DataSource = dv;
                }

                else if (Status.Checked)
                {
                    dv.RowFilter = "STATUS LIKE '%" + Search.Text + "%'";
                    servicosBindingSource.DataSource = dv;
                    servicosBindingSource1.DataSource = dv;
                }
            }
        }

        // Produto nao encontrado
        private void NotFinded()
        {
            if (ServicosGrid.RowCount == 0)
            {
                if (RemoverFiltros.Visible)
                    NotFindDesc.Text = "Nenhum resultado que corresponda à sua pesquisa \n atual foi encontrado. Tente remover algum filtro";
                else
                    NotFindDesc.Text = "Nenhum resultado que corresponda \n à sua pesquisa atual foi encontrado.";

                ServicosGrid.Visible = false;
                Separator2.Visible = false;
                toolStripPaging.Visible = false;

                NotFind.Visible = true;

                NotFindDesc.Visible = true;
            }
            else
            {
                ServicosGrid.Visible = true;
                Separator2.Visible = true;
                toolStripPaging.Visible = true;

                NotFind.Visible = false;
                NotFindDesc.Visible = false;
            }
        }

        // Mudar cor do filtro (ativado)
        private void ColorFilter(Guna.UI2.WinForms.Guna2CirclePictureBox ActiveFilter)
        {
            FilterBtn.BorderColor = ThemeManager.BorderRedButtonBorderColor;
            FilterBtn.HoverState.FillColor = ThemeManager.BorderRedButtonHoverFillColor;
            FilterBtn.HoverState.BorderColor = ThemeManager.BorderRedButtonHoverBorderColor;
            FilterBtn.PressedColor = ThemeManager.BorderRedButtonPressedColor;
            FilterBtn.Image = Properties.Resources.filtro___red;
            ActiveFilter.Visible = true;
            RemoverFiltros.Visible = true;
        }

        // Mudar cor do filtro (desativado)
        private void RemoveFilterColor(Guna.UI2.WinForms.Guna2CirclePictureBox ActiveFilter)
        {
            FilterBtn.BorderColor = ThemeManager.SeparatorAndBorderColor;
            FilterBtn.HoverState.FillColor = ThemeManager.MainButtonHoverFillColor;
            FilterBtn.HoverState.BorderColor = ThemeManager.MainButtonHoverBorderColor;
            FilterBtn.PressedColor = ThemeManager.MainButtonPressedColor;
            FilterBtn.Image = Properties.Resources.filtro___cinza;
            ActiveFilter.Visible = false;
            RemoverFiltros.Visible = false;
        }

        /*--------------------------*/

        // Esconder frames
        private void HideFrames()
        {
            if (ExcelFrame.Visible)
            {
                ExcelFrame.Visible = false;
                ExcelFrame.Location = new Point(ExcelFrame.Location.X - 6, ExcelFrame.Location.Y);
            }

            if (PdfFrame.Visible)
            {
                PdfFrame.Visible = false;
                PdfFrame.Location = new Point(PdfFrame.Location.X - 6, PdfFrame.Location.Y);
            }

            if (ExportOptions.Visible)
            {
                ExportOptions.Visible = false;
                ExportOptions.Location = new Point(ExportOptions.Location.X - 6, ExportOptions.Location.Y);
            }

            if (FilterItens.Visible)
            {
                FilterItens.Visible = false;
                FilterItens.Location = new Point(FilterItens.Location.X, FilterItens.Location.Y - 6);
            }

            if (SearchOptions.Visible)
            {
                if (RemoverFiltros.Visible)
                    SearchOptions.Location = new Point(5512, SearchOptions.Location.Y);
                else
                    SearchOptions.Location = new Point(5512, SearchOptions.Location.Y);
            }

            if (CurvaOptions.Visible)
            {
                if (RemoverFiltros.Visible)
                    CurvaOptions.Location = new Point(5512, CurvaOptions.Location.Y);
                else
                    CurvaOptions.Location = new Point(5512, CurvaOptions.Location.Y);
            }

            if (FabricanteOptions.Visible)
            {
                if (RemoverFiltros.Visible)
                    FabricanteOptions.Location = new Point(5512, FabricanteOptions.Location.Y);
                else
                    FabricanteOptions.Location = new Point(5512, FabricanteOptions.Location.Y);
            }

            if (TipoOptions.Visible)
            {
                if (RemoverFiltros.Visible)
                    TipoOptions.Location = new Point(5512, TipoOptions.Location.Y);
                else
                    TipoOptions.Location = new Point(5512, TipoOptions.Location.Y);
            }

            if (GrupoOptions.Visible)
            {
                if (RemoverFiltros.Visible)
                    GrupoOptions.Location = new Point(5512, GrupoOptions.Location.Y);
                else
                    GrupoOptions.Location = new Point(5512, GrupoOptions.Location.Y);
            }

            if (FornecedoresOptions.Visible)
            {
                if (RemoverFiltros.Visible)
                    FornecedoresOptions.Location = new Point(5512, FornecedoresOptions.Location.Y);
                else
                    FornecedoresOptions.Location = new Point(5512, FornecedoresOptions.Location.Y);
            }

            if (StatusOptions.Visible)
            {
                if (RemoverFiltros.Visible)
                    StatusOptions.Location = new Point(5512, StatusOptions.Location.Y);
                else if (TodoPeriodo.Checked)
                    StatusOptions.Location = new Point(5512, StatusOptions.Location.Y);
                else
                    StatusOptions.Location = new Point(5512, StatusOptions.Location.Y);
            }

            if (SpecificDate.Visible)
            {
                SpecificDate.Visible = false;
                SpecificDate.Location = new Point(SpecificDate.Location.X - 10, SpecificDate.Location.Y);
            }

            if (ExportItens.Visible)
            {
                ExportItens.Visible = false;
                ExportItens.Location = new Point(ExportItens.Location.X, ExportItens.Location.Y - 6);
            }

            if (MoreOptions.Visible)
            {
                MoreOptions.Visible = false;
                MoreOptions.Location = new Point(MoreOptions.Location.X, MoreOptions.Location.Y - 6);
            }

            if (SortOptions.Location.X == MoreOptions.Location.X + 165)
                SortOptions.Location = new Point(SortOptions.Location.X, 8800);

            if (ViewOptions.Visible)
            {
                ViewOptions.Visible = false;
                ViewOptions.Location = new Point(ViewOptions.Location.X - 6, ViewOptions.Location.Y);
            }
        }

        private void HideNewCustomerItens()
        {
            ServicosGrid.Visible = true;
            Separator2.Visible = true;
            toolStripPaging.Visible = true;
            Search.Visible = true;
            MostrarText.Visible = true;
            PageItens.Visible = true;
            FilterBtn.Visible = true;
            MoreOptionsBtn.Visible = true;
            ExportBtn.Visible = true;

            Illustration.Visible = false;
            Desc.Visible = false;
        }

        /*--------------------------*/

        // Fundo preto transparente
        private void DarkBackground(Form form)
        {
            Form formBackground = new Form();

            formBackground.Name = "TransparentBack";
            formBackground.StartPosition = FormStartPosition.Manual;
            formBackground.FormBorderStyle = FormBorderStyle.None;
            formBackground.Opacity = .50d;
            formBackground.BackColor = Color.Black;
            formBackground.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            formBackground.AutoSize = true;
            formBackground.TopMost = false;
            formBackground.Location = this.Location;
            formBackground.ShowInTaskbar = false;
            formBackground.Show();

            form.Owner = formBackground;
            form.ShowDialog();
        }

        /*--------------------------*/

        // Adicionar itens a lista pra poder usar o dark/light mode
        private void AddControlsToList()
        {
            GunaPanels = new List<Guna.UI2.WinForms.Guna2Panel>();
            GunaMainButtons = new List<Guna.UI2.WinForms.Guna2Button>();
            GunaButtons = new List<Guna.UI2.WinForms.Guna2Button>();
            GunaRadioButtons = new List<Guna.UI2.WinForms.Guna2RadioButton>();
            GunaSeparators = new List<Guna.UI2.WinForms.Guna2Separator>();

            NormalLabels = new List<Label>();
            CustomerInfoLabels = new List<Label>();
            CustomerInfoPresetLabels = new List<Label>();

            //------------------//

            // Paineis
            Guna.UI2.WinForms.Guna2Panel[] Panels = new Guna.UI2.WinForms.Guna2Panel[17]
            {
                FilterItens, SearchOptions, DataOptions, CurvaOptions, FabricanteOptions, TipoOptions, GrupoOptions, FornecedoresOptions, StatusOptions, SpecificDate,

                MoreOptions, SortOptions, ViewOptions,

                ExportItens, ExcelFrame, PdfFrame, ExportOptions
            };

            // Botoes normais - principais
            Guna.UI2.WinForms.Guna2Button[] MainButtons = new Guna.UI2.WinForms.Guna2Button[4]
            {
                FilterBtn, MoreOptionsBtn, ExportBtn, Voltar
            };

            // Botoes normais
            Guna.UI2.WinForms.Guna2Button[] PanelButtons = new Guna.UI2.WinForms.Guna2Button[21]
            {
                FilterPesquisaBtn, FilterCurvaBtn, FilterFabricanteBtn, FilterTipoItemBtn, FilterGrupoBtn, FilterFornecedoresBtn, FilterStatusBtn, DataEspecifica,

                SortBtn, ViewOptionsBtn, ListaNormal, ListaCompacta, Crescente, Descrescente,

                ExcelBtn, PdfBtn, ExportOptionsBtn, ExportCurrentExcel, ExportAllExcel, ExportCurrentPdf, ExportAllPdf
            };

            // Botoes de escolha (radio button)
            Guna.UI2.WinForms.Guna2RadioButton[] RadioButtons = new Guna.UI2.WinForms.Guna2RadioButton[43]
            {
                All, NumeroServico, Cliente, Telefone, Veiculo, ValorTotal, Tecnico, Status,

                TodasCurvas, CurvaA, CurvaB, CurvaC,

                TodosTecnicos, FabricanteEspecifico,

                TodosItens, Comprado, Kit,

                TodosGrupos, Mecanica, Eletrica,

                TodosFornecedores, FornecedorEspecifico,

                TodosStatus, AguardandoOrcamento, AguardandoAutorizacao, AguardandoPeca, ReparoEmAndamento,

                TodoPeriodo, Hoje, Semana, Mes, Ano, AllColumns, Principais, NumeroServicoSort, ClienteSort, TelefoneSort, 

                VeiculoSort, EntradaSort, ProntoSort, ValorTotalSort, TecnicoSort, StatusSort
            };

            //------------------//

            // Labels normais
            Label[] Labels = new Label[8]
            {
                FiltrosLabel, MoreOptionsLabel, SortOptionsLabel, ViewOptionsLabel, ExportItensLabel, ExcelLabel, PdfLabel, ExportOptionsLabel
            };

            // Customer info labels info
            Label[] CustomerInfosLabels = new Label[29]
             {
                ProdutoName, CodigoInfo, StatusInfo, NumeroFabricanteInfo, NumeroOriginalInfo, FabricanteInfo, UnidadeInfo, TipoProdutoInfo,
                GrupoInfo, SubGrupoInfo, FornecedorInfo, UltimaVendaInfo, CurvaInfo, ObservacoesInfo,

                CustoInfo, CustoDolarInfo, VendaConsumidorInfo, RevendaInfo, VendaOutrosInfo, LucroInfo, LucroDolarInfo, LucroPorcentoInfo,

                DisponivelInfo, IdealInfo, MinimoInfo, LocalizacaoInfo, PrateleiraInfo, 

                CodigoEAN13, CodigoCODE128
             };

            // Customer info labels
            Label[] CustomerPresetLabels = new Label[27]
            {
                NumeroFabricantePreset, NumeroOriginalPreset, NumeroFabricantePreset, MarcaPreset, UnidadePreset, TipoProdutoPreset, GrupoPreset, SubGrupoPreset, 
                FornecedorPreset, UltimaVendaPreset, CurvaPreset, ObservacoesPreset,

                CustoPreset, CustoDolarPreset, VendaConsumidorPreset, RevendaPreset, VendaOutrosPreset, LucroReaisPreset, LucroDolarPreset, LucroPorcentoPreset,

                DisponivelPreset, IdealPreset, MinimaPreset, LocalizacaoPreset, PrateleiraPreset,

                Ean13Preset, Code128Preset
            };

            //------------------//

            // Separators
            Guna.UI2.WinForms.Guna2Separator[] Separators = new Guna.UI2.WinForms.Guna2Separator[18]
            {
                Separator1, Separator2, guna2Separator1, guna2Separator2, guna2Separator3, guna2Separator4, guna2Separator5,
                guna2Separator6, guna2Separator7, guna2Separator8, guna2Separator9, guna2Separator10, guna2Separator11,
                guna2Separator12, guna2Separator13, guna2Separator14, guna2Separator17, MiniSeparator1
            };

            //------------------//

            GunaPanels.AddRange(Panels);
            GunaMainButtons.AddRange(MainButtons);
            GunaButtons.AddRange(PanelButtons);
            GunaRadioButtons.AddRange(RadioButtons);
            GunaSeparators.AddRange(Separators);
            NormalLabels.AddRange(Labels);
            CustomerInfoLabels.AddRange(CustomerInfosLabels);
            CustomerInfoPresetLabels.AddRange(CustomerPresetLabels);
        }

        // Ativar/desativar o dark mode
        private void SetColor()
        {
            this.BackColor = ThemeManager.FormBackColor;

            FrameName.BackColor = ThemeManager.FormBackColor;
            FrameName.ForeColor = ThemeManager.WhiteFontColor;

            ServicosGrid.BackgroundColor = ThemeManager.FormBackColor;
            ServicosGrid.DefaultCellStyle.BackColor = ThemeManager.FormBackColor;
            ServicosGrid.DefaultCellStyle.SelectionBackColor = ThemeManager.FormBackColor;
            ServicosGrid.DefaultCellStyle.ForeColor = ThemeManager.GridForeColor;
            ServicosGrid.DefaultCellStyle.SelectionForeColor = ThemeManager.FontColor;
            ServicosGrid.GridColor = ThemeManager.SeparatorAndBorderColor;

            ServicosGrid.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.WhiteFontColor;
            ServicosGrid.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemeManager.WhiteFontColor;
            ServicosGrid.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.FormBackColor;
            ServicosGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemeManager.FormBackColor;

            ServicosGrid.RowHeadersDefaultCellStyle.BackColor = ThemeManager.FormBackColor;

            Search.FillColor = ThemeManager.SearchBoxFillColor;
            Search.ForeColor = ThemeManager.SearchBoxForeColor;
            Search.BorderColor = ThemeManager.SeparatorAndBorderColor;
            Search.HoverState.BorderColor = ThemeManager.SearchBoxHoverBorderColor;
            Search.PlaceholderForeColor = ThemeManager.SearchBoxPlaceholderColor;
            Search.FocusedState.BorderColor = ThemeManager.SearchBoxFocusedBorderColor;
            Search.FocusedState.ForeColor = ThemeManager.SearchBoxForeColor;

            PesquisarTecnico.FillColor = ThemeManager.SearchBoxFillColor2;
            PesquisarTecnico.ForeColor = ThemeManager.SearchBoxForeColor;
            PesquisarTecnico.BorderColor = ThemeManager.SeparatorAndBorderColor;
            PesquisarTecnico.HoverState.BorderColor = ThemeManager.SearchBoxHoverBorderColor;
            PesquisarTecnico.PlaceholderForeColor = ThemeManager.SearchBoxPlaceholderColor;
            PesquisarTecnico.FocusedState.BorderColor = ThemeManager.SearchBoxFocusedBorderColor;
            PesquisarTecnico.FocusedState.ForeColor = ThemeManager.SearchBoxForeColor;

            PesquisarFornecedor.FillColor = ThemeManager.SearchBoxFillColor2;
            PesquisarFornecedor.ForeColor = ThemeManager.SearchBoxForeColor;
            PesquisarFornecedor.BorderColor = ThemeManager.SeparatorAndBorderColor;
            PesquisarFornecedor.HoverState.BorderColor = ThemeManager.SearchBoxHoverBorderColor;
            PesquisarFornecedor.PlaceholderForeColor = ThemeManager.SearchBoxPlaceholderColor;
            PesquisarFornecedor.FocusedState.BorderColor = ThemeManager.SearchBoxFocusedBorderColor;
            PesquisarFornecedor.FocusedState.ForeColor = ThemeManager.SearchBoxForeColor;

            PageItens.FillColor = ThemeManager.ComboBoxFillColor;
            PageItens.ForeColor = ThemeManager.ComboBoxForeColor;
            PageItens.BorderColor = ThemeManager.ComboBoxBorderColor;
            PageItens.HoverState.BorderColor = ThemeManager.ComboBoxHoverBorderColor;
            PageItens.FocusedState.BorderColor = ThemeManager.ComboBoxFocusedBorderColor;
            PageItens.ItemsAppearance.ForeColor = ThemeManager.ComboBoxForeColor;
            PageItens.ItemsAppearance.SelectedBackColor = ThemeManager.ComboBoxSelectedItemColor;

            MostrarText.BackColor = ThemeManager.FormBackColor;
            MostrarText.ForeColor = ThemeManager.FontColor;

            NovoServico.BackColor = ThemeManager.FormBackColor;
            NovoServico.FillColor = ThemeManager.FullRedButtonColor;
            NovoServico.BorderColor = ThemeManager.FullRedButtonColor;
            NovoServico.HoverState.FillColor = ThemeManager.FullRedButtonHoverColor;
            NovoServico.HoverState.BorderColor = ThemeManager.FullRedButtonHoverColor;
            NovoServico.CheckedState.FillColor = ThemeManager.FullRedButtonCheckedColor;
            NovoServico.CheckedState.BorderColor = ThemeManager.FullRedButtonCheckedColor;

            RemoverFiltros.FillColor = ThemeManager.BorderRedButtonFillColor;
            RemoverFiltros.ForeColor = ThemeManager.BorderRedButtonForeColor;
            RemoverFiltros.BorderColor = ThemeManager.BorderRedButtonBorderColor;
            RemoverFiltros.HoverState.BorderColor = ThemeManager.BorderRedButtonHoverBorderColor;
            RemoverFiltros.HoverState.FillColor = ThemeManager.BorderRedButtonHoverFillColor;
            RemoverFiltros.HoverState.ForeColor = ThemeManager.BorderRedButtonHoverForeColor;
            RemoverFiltros.PressedColor = ThemeManager.BorderRedButtonPressedColor;

            CustomDateBtn.FillColor = ThemeManager.BorderRedButtonFillColor2;
            CustomDateBtn.ForeColor = ThemeManager.BorderRedButtonForeColor;
            CustomDateBtn.BorderColor = ThemeManager.BorderRedButtonBorderColor;
            CustomDateBtn.HoverState.BorderColor = ThemeManager.BorderRedButtonHoverBorderColor;
            CustomDateBtn.HoverState.FillColor = ThemeManager.BorderRedButtonHoverFillColor2;
            CustomDateBtn.HoverState.ForeColor = ThemeManager.BorderRedButtonHoverForeColor;
            CustomDateBtn.PressedColor = ThemeManager.BorderRedButtonPressedColor;

            CustomFabricanteBtn.FillColor = ThemeManager.BorderRedButtonFillColor2;
            CustomFabricanteBtn.ForeColor = ThemeManager.BorderRedButtonForeColor;
            CustomFabricanteBtn.BorderColor = ThemeManager.BorderRedButtonBorderColor;
            CustomFabricanteBtn.HoverState.BorderColor = ThemeManager.BorderRedButtonHoverBorderColor;
            CustomFabricanteBtn.HoverState.FillColor = ThemeManager.BorderRedButtonHoverFillColor2;
            CustomFabricanteBtn.HoverState.ForeColor = ThemeManager.BorderRedButtonHoverForeColor;
            CustomFabricanteBtn.PressedColor = ThemeManager.BorderRedButtonPressedColor;

            CustomFornecedorBtn.FillColor = ThemeManager.BorderRedButtonFillColor2;
            CustomFornecedorBtn.ForeColor = ThemeManager.BorderRedButtonForeColor;
            CustomFornecedorBtn.BorderColor = ThemeManager.BorderRedButtonBorderColor;
            CustomFornecedorBtn.HoverState.BorderColor = ThemeManager.BorderRedButtonHoverBorderColor;
            CustomFornecedorBtn.HoverState.FillColor = ThemeManager.BorderRedButtonHoverFillColor2;
            CustomFornecedorBtn.HoverState.ForeColor = ThemeManager.BorderRedButtonHoverForeColor;
            CustomFornecedorBtn.PressedColor = ThemeManager.BorderRedButtonPressedColor;

            CustomFabricanteBtn.FillColor = ThemeManager.BorderRedButtonFillColor2;
            CustomFabricanteBtn.ForeColor = ThemeManager.BorderRedButtonForeColor;
            CustomFabricanteBtn.BorderColor = ThemeManager.BorderRedButtonBorderColor;
            CustomFabricanteBtn.HoverState.BorderColor = ThemeManager.BorderRedButtonHoverBorderColor;
            CustomFabricanteBtn.HoverState.FillColor = ThemeManager.BorderRedButtonHoverFillColor2;
            CustomFabricanteBtn.HoverState.ForeColor = ThemeManager.BorderRedButtonHoverForeColor;
            CustomFabricanteBtn.PressedColor = ThemeManager.BorderRedButtonPressedColor;

            Data1.BackColor = ThemeManager.PanelColor;
            Data1.FillColor = ThemeManager.PanelColor;
            Data1.ForeColor = ThemeManager.WhiteFontColor;
            Data1.BorderColor = ThemeManager.SeparatorAndBorderColor;
            Data1.HoverState.BorderColor = ThemeManager.DateTimePickerHoverBorderColor;
            Data1.CheckedState.BorderColor = ThemeManager.SeparatorAndBorderColor;
            Data1.CheckedState.FillColor = ThemeManager.PanelColor;

            Data2.BackColor = ThemeManager.PanelColor;
            Data2.FillColor = ThemeManager.PanelColor;
            Data2.ForeColor = ThemeManager.WhiteFontColor;
            Data2.BorderColor = ThemeManager.SeparatorAndBorderColor;
            Data2.HoverState.BorderColor = ThemeManager.DateTimePickerHoverBorderColor;
            Data2.CheckedState.BorderColor = ThemeManager.SeparatorAndBorderColor;
            Data2.CheckedState.FillColor = ThemeManager.PanelColor;

            InformacoesBtn.FillColor = ThemeManager.FormBackColor;
            InformacoesBtn.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            InformacoesBtn.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;

            ValoresBtn.FillColor = ThemeManager.FormBackColor;
            ValoresBtn.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            ValoresBtn.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;

            QuantidadesBtn.FillColor = ThemeManager.FormBackColor;
            QuantidadesBtn.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            QuantidadesBtn.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;

            OutrosBtn.FillColor = ThemeManager.FormBackColor;
            OutrosBtn.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            OutrosBtn.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;

            Editar.ForeColor = ThemeManager.BorderDarkGrayButtonForeColor;
            Editar.FillColor = ThemeManager.BorderDarkGrayButtonFillColor;
            Editar.BorderColor = ThemeManager.BorderDarkGrayButtonBorderColor;
            Editar.HoverState.ForeColor = ThemeManager.BorderDarkGrayButtonHoverForeColor;
            Editar.HoverState.BorderColor = ThemeManager.BorderDarkGrayButtonHoverBorderColor;
            Editar.HoverState.FillColor = ThemeManager.BorderDarkGrayButtonHoverFillColor;
            Editar.FocusedColor = ThemeManager.BorderDarkGrayButtonPressedFocusedColor;
            Editar.PressedColor = ThemeManager.BorderDarkGrayButtonPressedFocusedColor;

            Excluir.ForeColor = ThemeManager.BorderRedButtonForeColor;
            Excluir.FillColor = ThemeManager.BorderRedButtonFillColor;
            Excluir.BorderColor = ThemeManager.BorderRedButtonBorderColor;
            Excluir.HoverState.BorderColor = ThemeManager.BorderRedButtonHoverBorderColor;
            Excluir.HoverState.FillColor = ThemeManager.BorderRedButtonHoverFillColor;
            Excluir.HoverState.ForeColor = ThemeManager.BorderRedButtonHoverForeColor;
            Excluir.PressedColor = ThemeManager.BorderRedButtonPressedColor;

            ProdutosSelecionados.ForeColor = ThemeManager.WhiteFontColor;

            DeleteAllSelected.ForeColor = ThemeManager.RedFontColor;
            DeleteAllSelected.FillColor = ThemeManager.FormBackColor;
            DeleteAllSelected.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            DeleteAllSelected.PressedColor = ThemeManager.ButtonPressedColor;

            ProductPicture.BackColor = ThemeManager.FormBackColor;

            Desc.ForeColor = ThemeManager.WhiteFontColor;

            NotFindDesc.ForeColor = ThemeManager.WhiteFontColor;

            ToolTip.ForeColor = ThemeManager.GunaToolTipForeColor;
            ToolTip.BorderColor = ThemeManager.GunaToolTipBorderColor;
            ToolTip.BackColor = ThemeManager.GunaToolTipBackColor;

            //------------------//

            // Paineis
            foreach (Guna.UI2.WinForms.Guna2Panel ct in GunaPanels)
            {
                ct.BackColor = ThemeManager.FormBackColor;
                ct.FillColor = ThemeManager.PanelColor;
                ct.BorderColor = ThemeManager.PanelBorderColor;
            }

            // Botoes normais - principais
            foreach (Guna.UI2.WinForms.Guna2Button ct in GunaMainButtons)
            {
                ct.BorderColor = ThemeManager.SeparatorAndBorderColor;

                ct.ForeColor = ThemeManager.MainButtonForeColor;
                ct.FillColor = ThemeManager.MainButtonFillColor;
                ct.PressedColor = ThemeManager.MainButtonPressedColor;
                ct.HoverState.FillColor = ThemeManager.MainButtonHoverFillColor;
                ct.HoverState.BorderColor = ThemeManager.MainButtonHoverBorderColor;

                ct.CheckedState.FillColor = ThemeManager.MainButtonPressedColor;
            }

            // Botoes normais
            foreach (Guna.UI2.WinForms.Guna2Button ct in GunaButtons)
            {
                ct.ForeColor = ThemeManager.FontColor;
                ct.FillColor = ThemeManager.ButtonFillColor;
                ct.PressedColor = ThemeManager.ButtonPressedColor;
                ct.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;
                ct.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            }

            // Botoes de escolha (radio button)
            foreach (Guna.UI2.WinForms.Guna2RadioButton ct in GunaRadioButtons)
            {
                ct.BackColor = ThemeManager.ButtonFillColor;
                ct.ForeColor = ThemeManager.FontColor;

                ct.CheckedState.FillColor = ThemeManager.ButtonFillColor;
                ct.UncheckedState.FillColor = ThemeManager.ButtonFillColor;
            }

            //------------------//

            // Labels normais
            foreach (Label ct in NormalLabels)
            {
                ct.ForeColor = ThemeManager.FontColor;
                ct.BackColor = ThemeManager.PanelColor;
            }

            // Customer info labels
            foreach (Label ct in CustomerInfoLabels)
            {
                ct.ForeColor = ThemeManager.FontColor;
                ct.BackColor = ThemeManager.FormBackColor;
            }


            // Customer info preset labels
            foreach (Label ct in CustomerInfoPresetLabels)
            {
                ct.ForeColor = ThemeManager.PresetLabelColor;
                ct.BackColor = ThemeManager.FormBackColor;
            }

            //------------------//

            // Separators
            foreach (Guna.UI2.WinForms.Guna2Separator ct in GunaSeparators)
            {
                ct.FillColor = ThemeManager.SeparatorAndBorderColor;
            }
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Outros */

        // Novo serviço
        private void NovoServico_Click(object sender, EventArgs e)
        {
            HideFrames();

            foreach (Form frm in fc)
            {
                if (frm.Name != "NovoServico")
                    NovoServicoOpen = false;
                else
                    NovoServicoOpen = true;
            }

            if (NovoServicoOpen != true)
            {
                NovoServicoOpen = true;

                ThreadStart ts = new ThreadStart(() =>
                {
                    DarkBackground(new Frames.Estoque.NovoServico());
                });

                Thread t = new Thread(ts);

                t.SetApartmentState(ApartmentState.STA);

                t.Start();
            }
        }

        // Mais opçoes
        private async void MoreOptionsBtn_Click(object sender, EventArgs e)
        {
            if (MoreOptions.Visible)
            {
                MoreOptions.Visible = false;
                MoreOptions.Location = new Point(MoreOptions.Location.X, 88);

                if (SortOptions.Visible)
                {
                    SortOptions.Visible = false;
                    SortOptions.Location = new Point(SortOptions.Location.X - 6, 88);
                }

                if (ViewOptions.Visible)
                {

                    ViewOptions.Visible = false;
                    ViewOptions.Location = new Point(SortOptions.Location.X - 6, 88);
                }
            }
            else
            {
                HideFrames();

                if (AnimateButtonsAndPanels)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        await TaskDelay(10);
                        MoreOptions.Location = new Point(MoreOptions.Location.X, MoreOptions.Location.Y + 1);
                        MoreOptions.Visible = true;
                    }
                }
                else
                {
                    MoreOptions.Location = new Point(MoreOptions.Location.X, MoreOptions.Location.Y + 6);
                    MoreOptions.Visible = true;
                }
            }
        }

        // Mostrar tooltip do botao de informaçoes
        private void ServicosGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ServicosGrid.Columns[26].Index)
            {
                var cell = ServicosGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = "Opções";
            }
        }

        // Atualizar o grid qnd adicionar/editar/deletar serviço
        private async void VerifyTimer_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.CanUpdateGrid)
            {
                await TaskDelay(500);

                UpdateGrid();
                RefreshPagination();

                ProductInfo.Location = new Point(ProductInfo.Location.X, 11106);
            }

            if (Properties.Settings.Default.CanShowDeleteConfirmation)
            {
                List<string> AllNamesDistinct = AllNames.Distinct().ToList();

                ThreadStart ts = new ThreadStart(() => {
                    DarkBackground(new Frames.Delete.DeleteConfirmation2("Serviços", AllNamesDistinct));
                });

                Thread t = new Thread(ts);

                t.SetApartmentState(ApartmentState.STA);

                t.Start();

                Properties.Settings.Default.CanShowDeleteConfirmation = false;
            }

            /*foreach (Form frm in fc)
            {
                if (frm.Name == "DeleteSelected2")
                {
                    if (frm.WindowState == FormWindowState.Normal)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            frm.WindowState = FormWindowState.Minimized;
                        });
                    }
                }
            }*/
        }

        /*--------------------------------------------------------------------------------------------*/

        // Itens por pagina

        private void MostrarText_Click(object sender, EventArgs e)
        {
            HideFrames();

            if (PageItens.Enabled)
            {
                PageItens.DroppedDown = true;
                PageItens.Focus();
            }
        }

        private void PageItens_Click(object sender, EventArgs e)
        {
            HideFrames();
        }

        private void PageItens_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageRows = int.Parse(PageItens.Text);
            Properties.Settings.Default.ItensPorPagina = int.Parse(PageItens.Text);

            if (CurrentPage > PagesCount - 1)
                CurrentPage = PagesCount - 1;

            if (DataFiltered)
            {
                PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                DateFunction(DateTime.Now.AddDays(-FilterByDays));
            }
            else
            {
                ReloadPage();
                RefreshPagination();
            }
        }

        /*--------------------------------------------------------------------------------------------*/

        // Pesquisa

        private void Search_TextChanged(object sender, EventArgs e)
        {
            HideFrames();

            if (Search.Text != "")
            {
                SearchFilter();
                NotFinded();

                EraseText.Visible = true;
            }
            else
            {
                NotFind.Visible = false;
                NotFindDesc.Visible = false;

                ServicosGrid.Visible = true;
                Separator2.Visible = true;
                EraseText.Visible = false;
                toolStripPaging.Visible = true;

                if (DataFiltered != true)
                {
                    ReloadPage();
                    RefreshPagination();
                }
                else
                {
                    DateFunction(DateTime.Today.AddDays(-FilterByDays + 1));
                }
            }
        }

        private void Search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                HideFrames();

                if (Search.Text != "")
                {
                    SearchFilter();
                    NotFinded();

                    EraseText.Visible = true;
                }
                else
                {
                    NotFind.Visible = false;
                    NotFindDesc.Visible = false;

                    ServicosGrid.Visible = true;
                    Separator2.Visible = true;
                    EraseText.Visible = false;

                    if (DataFiltered)
                    {
                        DateFunction(DateTime.Today.AddDays(-FilterByDays + 1));
                    }
                    else
                    {
                        ReloadPage();
                        RefreshPagination();

                        toolStripPaging.Visible = true;
                    }
                }
            }

            if (Cliente.Checked)
            {
                if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            else if (All.Checked)
            {
                if (char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void EraseText_Click(object sender, EventArgs e)
        {
            Search.Text = "";
            NotFind.Visible = false;
            NotFindDesc.Visible = false;

            if (DataFiltered != true)
            {
                ReloadPage();
                RefreshPagination();
            }
            else
            {
                DateFunction(DateTime.Today.AddDays(-FilterByDays + 1));
            }
        }

        /*--------------------------------------------------------------------------------------------*/

        // Filtros

        private async void FilterBtn_Click(object sender, EventArgs e)
        {
            if (FilterItens.Visible)
            {
                FilterItens.Visible = false;
                FilterItens.Location = new Point(FilterItens.Location.X, 88);

                SearchOptions.Visible = false;
                SearchOptions.Location = new Point(512, SearchOptions.Location.Y);

                CurvaOptions.Visible = false;
                CurvaOptions.Location = new Point(512, CurvaOptions.Location.Y);

                FabricanteOptions.Visible = false;
                FabricanteOptions.Location = new Point(512, FabricanteOptions.Location.Y);

                GrupoOptions.Visible = false;
                GrupoOptions.Location = new Point(512, GrupoOptions.Location.Y);

                TipoOptions.Visible = false;
                TipoOptions.Location = new Point(512, TipoOptions.Location.Y);

                FornecedoresOptions.Visible = false;
                FornecedoresOptions.Location = new Point(512, FornecedoresOptions.Location.Y);

                StatusOptions.Visible = false;
                StatusOptions.Location = new Point(512, StatusOptions.Location.Y);

                DataOptions.Visible = false;
                DataOptions.Location = new Point(512, DataOptions.Location.Y);

                if (SpecificDate.Visible)
                {
                    SpecificDate.Visible = false;
                    SpecificDate.Location = new Point(SpecificDate.Location.X - 8, SpecificDate.Location.Y);
                }
            }
            else
            {
                HideFrames();

                if (AnimateButtonsAndPanels)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        await TaskDelay(10);
                        FilterItens.Location = new Point(FilterItens.Location.X, FilterItens.Location.Y + 1);
                        FilterItens.Visible = true;
                    }
                }
                else
                {
                    FilterItens.Location = new Point(FilterItens.Location.X, FilterItens.Location.Y + 6);
                    FilterItens.Visible = true;
                }
            }
        }

        private async void DataEspecifica_Click(object sender, EventArgs e)
        {
            if (SpecificDate.Visible)
            {
                SpecificDate.Visible = false;
                SpecificDate.Location = new Point(673, SpecificDate.Location.Y);
            }
            else
            {
                if (AnimateButtonsAndPanels)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        await TaskDelay(10);
                        SpecificDate.Location = new Point(SpecificDate.Location.X + 2, SpecificDate.Location.Y);
                        SpecificDate.Visible = true;
                    }
                }
                else
                {
                    SpecificDate.Location = new Point(SpecificDate.Location.X + 10, SpecificDate.Location.Y);
                    SpecificDate.Visible = true;
                }
            }
        }



        private async void FiltroPesquisaBtn_Click(object sender, EventArgs e)
        {
            if (SearchOptions.Visible)
            {
                SearchOptions.Visible = false;
                SearchOptions.Location = new Point(512, SearchOptions.Location.Y);
            }
            else
            {
                CurvaOptions.Visible = false;
                CurvaOptions.Location = new Point(512, CurvaOptions.Location.Y);

                FabricanteOptions.Visible = false;
                FabricanteOptions.Location = new Point(512, FabricanteOptions.Location.Y);

                TipoOptions.Visible = false;
                TipoOptions.Location = new Point(512, TipoOptions.Location.Y);

                GrupoOptions.Visible = false;
                GrupoOptions.Location = new Point(512, GrupoOptions.Location.Y);

                FornecedoresOptions.Visible = false;
                FornecedoresOptions.Location = new Point(512, FornecedoresOptions.Location.Y);

                StatusOptions.Visible = false;
                StatusOptions.Location = new Point(512, StatusOptions.Location.Y);

                if (AnimateButtonsAndPanels)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        await TaskDelay(10);
                        SearchOptions.Location = new Point(SearchOptions.Location.X + 1, SearchOptions.Location.Y);
                        SearchOptions.Visible = true;
                    }
                }
                else
                {
                    SearchOptions.Location = new Point(SearchOptions.Location.X + 6, SearchOptions.Location.Y);
                    SearchOptions.Visible = true;
                }
            }
        }

        private async void FilterCurvaBtn_Click(object sender, EventArgs e)
        {
            if (CurvaOptions.Visible)
            {
                CurvaOptions.Visible = false;
                CurvaOptions.Location = new Point(512, CurvaOptions.Location.Y);
            }
            else
            {
                SearchOptions.Visible = false;
                SearchOptions.Location = new Point(512, SearchOptions.Location.Y);

                FabricanteOptions.Visible = false;
                FabricanteOptions.Location = new Point(512, FabricanteOptions.Location.Y);

                TipoOptions.Visible = false;
                TipoOptions.Location = new Point(512, TipoOptions.Location.Y);

                GrupoOptions.Visible = false;
                GrupoOptions.Location = new Point(512, GrupoOptions.Location.Y);

                FornecedoresOptions.Visible = false;
                FornecedoresOptions.Location = new Point(512, FornecedoresOptions.Location.Y);

                StatusOptions.Visible = false;
                StatusOptions.Location = new Point(512, StatusOptions.Location.Y);

                if (AnimateButtonsAndPanels)
                {
                    if (CurvaOptions.Location == new Point(512, 94))
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            await TaskDelay(10);
                            CurvaOptions.Location = new Point(CurvaOptions.Location.X + 1, CurvaOptions.Location.Y);
                            CurvaOptions.Visible = true;
                        }
                    }
                }
                else
                {
                    CurvaOptions.Location = new Point(CurvaOptions.Location.X + 6, CurvaOptions.Location.Y);
                    CurvaOptions.Visible = true;
                }
            }
        }

        private async void FilterFabricanteBtn_Click(object sender, EventArgs e)
        {
            if (FabricanteOptions.Visible)
            {
                FabricanteOptions.Visible = false;
                FabricanteOptions.Location = new Point(512, FabricanteOptions.Location.Y);
            }
            else
            {
                SearchOptions.Visible = false;
                SearchOptions.Location = new Point(512, SearchOptions.Location.Y);

                CurvaOptions.Visible = false;
                CurvaOptions.Location = new Point(512, CurvaOptions.Location.Y);

                TipoOptions.Visible = false;
                TipoOptions.Location = new Point(512, TipoOptions.Location.Y);

                GrupoOptions.Visible = false;
                GrupoOptions.Location = new Point(512, GrupoOptions.Location.Y);

                FornecedoresOptions.Visible = false;
                FornecedoresOptions.Location = new Point(512, FornecedoresOptions.Location.Y);

                StatusOptions.Visible = false;
                StatusOptions.Location = new Point(512, StatusOptions.Location.Y);

                if (AnimateButtonsAndPanels)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        await TaskDelay(10);
                        FabricanteOptions.Location = new Point(FabricanteOptions.Location.X + 1, FabricanteOptions.Location.Y);
                        FabricanteOptions.Visible = true;
                    }
                }
                else
                {
                    FabricanteOptions.Location = new Point(FabricanteOptions.Location.X + 6, FabricanteOptions.Location.Y);
                    FabricanteOptions.Visible = true;
                }
            }
        }

        private async void FilterGrupoBtn_Click(object sender, EventArgs e)
        {
            if (GrupoOptions.Visible)
            {
                GrupoOptions.Visible = false;
                GrupoOptions.Location = new Point(512, GrupoOptions.Location.Y);
            }
            else
            {
                SearchOptions.Visible = false;
                SearchOptions.Location = new Point(512, SearchOptions.Location.Y);

                CurvaOptions.Visible = false;
                CurvaOptions.Location = new Point(512, CurvaOptions.Location.Y);

                FabricanteOptions.Visible = false;
                FabricanteOptions.Location = new Point(512, FabricanteOptions.Location.Y);

                TipoOptions.Visible = false;
                TipoOptions.Location = new Point(512, TipoOptions.Location.Y);

                FornecedoresOptions.Visible = false;
                FornecedoresOptions.Location = new Point(512, FornecedoresOptions.Location.Y);

                StatusOptions.Visible = false;
                StatusOptions.Location = new Point(512, StatusOptions.Location.Y);

                if (AnimateButtonsAndPanels)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        await TaskDelay(10);
                        GrupoOptions.Location = new Point(GrupoOptions.Location.X + 1, GrupoOptions.Location.Y);
                        GrupoOptions.Visible = true;
                    }
                }
                else
                {
                    GrupoOptions.Location = new Point(GrupoOptions.Location.X + 6, GrupoOptions.Location.Y);
                    GrupoOptions.Visible = true;
                }
            }
        }

        private async void FilterTipoItemBtn_Click(object sender, EventArgs e)
        {
            if (TipoOptions.Visible)
            {
                TipoOptions.Visible = false;
                TipoOptions.Location = new Point(512, TipoOptions.Location.Y);
            }
            else
            {
                SearchOptions.Visible = false;
                SearchOptions.Location = new Point(512, SearchOptions.Location.Y);

                CurvaOptions.Visible = false;
                CurvaOptions.Location = new Point(512, CurvaOptions.Location.Y);

                FabricanteOptions.Visible = false;
                FabricanteOptions.Location = new Point(512, FabricanteOptions.Location.Y);

                GrupoOptions.Visible = false;
                GrupoOptions.Location = new Point(512, GrupoOptions.Location.Y);

                FornecedoresOptions.Visible = false;
                FornecedoresOptions.Location = new Point(512, FornecedoresOptions.Location.Y);

                StatusOptions.Visible = false;
                StatusOptions.Location = new Point(512, StatusOptions.Location.Y);

                if (AnimateButtonsAndPanels)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        await TaskDelay(10);
                        TipoOptions.Location = new Point(TipoOptions.Location.X + 1, TipoOptions.Location.Y);
                        TipoOptions.Visible = true;
                    }
                }
                else
                {
                    TipoOptions.Location = new Point(TipoOptions.Location.X + 6, TipoOptions.Location.Y);
                    TipoOptions.Visible = true;
                }
            }
        }

        private async void FilterFornecedoresBtn_Click(object sender, EventArgs e)
        {
            if (FornecedoresOptions.Visible)
            {
                FornecedoresOptions.Visible = false;
                FornecedoresOptions.Location = new Point(512, FornecedoresOptions.Location.Y);
            }
            else
            {
                SearchOptions.Visible = false;
                SearchOptions.Location = new Point(512, SearchOptions.Location.Y);

                CurvaOptions.Visible = false;
                CurvaOptions.Location = new Point(512, CurvaOptions.Location.Y);

                FabricanteOptions.Visible = false;
                FabricanteOptions.Location = new Point(512, FabricanteOptions.Location.Y);

                TipoOptions.Visible = false;
                TipoOptions.Location = new Point(512, TipoOptions.Location.Y);

                GrupoOptions.Visible = false;
                GrupoOptions.Location = new Point(512, GrupoOptions.Location.Y);

                StatusOptions.Visible = false;
                StatusOptions.Location = new Point(512, StatusOptions.Location.Y);

                if (AnimateButtonsAndPanels)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        await TaskDelay(10);
                        FornecedoresOptions.Location = new Point(FornecedoresOptions.Location.X + 1, FornecedoresOptions.Location.Y);
                        FornecedoresOptions.Visible = true;
                    }
                }
                else
                {
                    FornecedoresOptions.Location = new Point(FornecedoresOptions.Location.X + 6, FornecedoresOptions.Location.Y);
                    FornecedoresOptions.Visible = true;
                }
            }
        }

        private async void FilterStatusBtn_Click(object sender, EventArgs e)
        {
            if (StatusOptions.Visible)
            {
                StatusOptions.Visible = false;
                StatusOptions.Location = new Point(512, StatusOptions.Location.Y);

                if (SpecificDate.Visible)
                {
                    SpecificDate.Visible = false;
                    SpecificDate.Location = new Point(SpecificDate.Location.X - 10, SpecificDate.Location.Y);
                }
            }
            else
            {
                SearchOptions.Visible = false;
                SearchOptions.Location = new Point(512, SearchOptions.Location.Y);

                CurvaOptions.Visible = false;
                CurvaOptions.Location = new Point(512, CurvaOptions.Location.Y);

                FabricanteOptions.Visible = false;
                FabricanteOptions.Location = new Point(512, FabricanteOptions.Location.Y);

                TipoOptions.Visible = false;
                TipoOptions.Location = new Point(512, TipoOptions.Location.Y);

                GrupoOptions.Visible = false;
                GrupoOptions.Location = new Point(512, GrupoOptions.Location.Y);

                FornecedoresOptions.Visible = false;
                FornecedoresOptions.Location = new Point(512, FornecedoresOptions.Location.Y);

                if (AnimateButtonsAndPanels)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        await TaskDelay(10);
                        StatusOptions.Location = new Point(StatusOptions.Location.X + 1, StatusOptions.Location.Y);
                        StatusOptions.Visible = true;
                    }
                }
                else
                {
                    StatusOptions.Location = new Point(StatusOptions.Location.X + 6, StatusOptions.Location.Y);
                    StatusOptions.Visible = true;
                }
            }
        }



        // Selecionar fabricante/marca especifico
        private void CustomFabricanteBtn_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

            con.Open();

            OleDbCommand cmd = new OleDbCommand("SELECT * FROM Funcionarios WHERE NOME = '" + PesquisarTecnico.Text + "'", con);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                HideNewCustomerItens();

                DataTable customrows = dt.AsEnumerable()
                .Skip(PageRows * CurrentPage)
                .Take(PageRows)
                .ToList()
                .CopyToDataTable();

                DataTable customrows2 = dt;

                // Serviços data
                servicosBindingSource.DataSource = customrows;
                servicosTableAdapter.Fill(servicosData.Servicos);

                // All rodutos data
                servicosBindingSource1.DataSource = customrows2;
                servicosTableAdapter1.Fill(allServicosData.Servicos);

                PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));

                if (DataFiltered)
                {
                    DateFunction(DateTime.Now.AddDays(-FilterByDays));
                }
                else
                {
                    ReloadPage();
                    RefreshPagination();
                }

                con.Close();

                ServicosGrid.Visible = true;
                Separator2.Visible = true;
                toolStripPaging.Visible = true;

                NotFind.Visible = false;

                NotFindDesc.Visible = false;
            }
            else
            {
                NotFindDesc.Text = "Nenhum resultado que corresponda \n à sua pesquisa atual foi encontrado.";

                ServicosGrid.Visible = false;
                Separator2.Visible = false;
                toolStripPaging.Visible = false;

                NotFind.Visible = true;

                NotFindDesc.Visible = true;
            }

            RefreshPagination();
            HideFrames();
        }

        // Selecionar fornecedor especifico
        private void CustomFornecedorBtn_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

            con.Open();

            OleDbCommand cmd = new OleDbCommand("SELECT * FROM Estoque WHERE FORNECEDOR = '" + PesquisarFornecedor.Text + "'", con);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                HideNewCustomerItens();

                DataTable customrows = dt.AsEnumerable()
                .Skip(PageRows * CurrentPage)
                .Take(PageRows)
                .ToList()
                .CopyToDataTable();

                DataTable customrows2 = dt;

                // Serviços data
                servicosBindingSource.DataSource = customrows;
                servicosTableAdapter.Fill(servicosData.Servicos);

                // All serviços data
                servicosBindingSource1.DataSource = customrows2;
                servicosTableAdapter1.Fill(allServicosData.Servicos);

                PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));

                if (DataFiltered)
                {
                    DateFunction(DateTime.Now.AddDays(-FilterByDays));
                }
                else
                {
                    ReloadPage();
                    RefreshPagination();
                }

                con.Close();

                ServicosGrid.Visible = true;
                Separator2.Visible = true;
                toolStripPaging.Visible = true;

                NotFind.Visible = false;

                NotFindDesc.Visible = false;
            }
            else
            {
                NotFindDesc.Text = "Nenhum resultado que corresponda \n à sua pesquisa atual foi encontrado.";

                ServicosGrid.Visible = false;
                Separator2.Visible = false;
                toolStripPaging.Visible = false;

                NotFind.Visible = true;

                NotFindDesc.Visible = true;
            }

            RefreshPagination();
            HideFrames();
        }

        // Classificar por data customizada
        private async void CustomDateBtn_Click(object sender, EventArgs e)
        {
            FilterByDays = (int)((Data2.Value - Data1.Value).TotalDays);

            DateFunction(DateTime.Now.AddDays(-FilterByDays));

            RemoverFiltros.Visible = true;
            DataFiltered = true;

            await TaskDelay(100);

            DataOptions.Location = new Point(5512, DataOptions.Location.Y);
            ColorFilter(ActiveFilter7);

            DataEspecifica.ForeColor = ThemeManager.RedFontColor;
            DataEspecifica.Image = Properties.Resources.data_red;

            TodoPeriodo.Checked = false;
            Hoje.Checked = false;
            Semana.Checked = false;
            Mes.Checked = false;
            Ano.Checked = false;

            HideFrames();
        }


        // Todos os filtros de pesquisa ativos ou nao
        private async void All_CheckedChanged(object sender, EventArgs e)
        {
            await TaskDelay(100);

            if (All.Checked)
            {
                FilterBtn.BorderColor = ThemeManager.SeparatorAndBorderColor;
                FilterBtn.HoverState.FillColor = ThemeManager.MainButtonHoverFillColor;
                FilterBtn.HoverState.BorderColor = ThemeManager.MainButtonHoverBorderColor;
                FilterBtn.PressedColor = ThemeManager.MainButtonPressedColor;
                FilterBtn.Image = Properties.Resources.filtro___cinza;
                ActiveFilter1.Visible = false;

                if (ActiveFilter2.Visible || ActiveFilter3.Visible || ActiveFilter4.Visible || ActiveFilter5.Visible || ActiveFilter6.Visible || ActiveFilter7.Visible)
                {
                    FilterBtn.BorderColor = ThemeManager.BorderRedButtonBorderColor;
                    FilterBtn.HoverState.FillColor = ThemeManager.BorderRedButtonHoverFillColor;
                    FilterBtn.HoverState.BorderColor = ThemeManager.BorderRedButtonHoverBorderColor;
                    FilterBtn.PressedColor = ThemeManager.BorderRedButtonPressedColor;
                    FilterBtn.Image = Properties.Resources.filtro___red;
                    ActiveFilter1.Visible = false;
                    RemoverFiltros.Visible = true;
                }
                else
                {
                    FilterBtn.BorderColor = ThemeManager.SeparatorAndBorderColor;
                    FilterBtn.HoverState.FillColor = ThemeManager.MainButtonHoverFillColor;
                    FilterBtn.HoverState.BorderColor = ThemeManager.MainButtonHoverBorderColor;
                    FilterBtn.PressedColor = ThemeManager.MainButtonPressedColor;
                    FilterBtn.Image = Properties.Resources.filtro___cinza;
                    ActiveFilter1.Visible = false;
                    RemoverFiltros.Visible = false;
                }
            }
            else
            {
                FilterBtn.BorderColor = ThemeManager.BorderRedButtonBorderColor;
                FilterBtn.HoverState.FillColor = ThemeManager.BorderRedButtonHoverFillColor;
                FilterBtn.HoverState.BorderColor = ThemeManager.BorderRedButtonHoverBorderColor;
                FilterBtn.PressedColor = ThemeManager.BorderRedButtonPressedColor;
                FilterBtn.Image = Properties.Resources.filtro___red;
                ActiveFilter1.Visible = true;
                RemoverFiltros.Visible = true;
            }

            CheckedStateFunction();
        }



        // Remover todos os filtros
        private async void RemoverFiltros_Click(object sender, EventArgs e)
        {
            All.Checked = true;
            TodasCurvas.Checked = true;
            TodosTecnicos.Checked = true;
            TodosGrupos.Checked = true;
            TodosItens.Checked = true;
            TodosFornecedores.Checked = true;
            TodosStatus.Checked = true;

            NotFind.Visible = false;
            NotFindDesc.Visible = false;

            ServicosGrid.Visible = true;
            Separator2.Visible = true;
            toolStripPaging.Visible = true;

            Search.Text = "";

            TodoPeriodo.Checked = true;
            Hoje.Checked = false;
            Semana.Checked = false;
            Mes.Checked = false;
            Ano.Checked = false;
            DataFiltered = false;
            FilterByDays = 0;

            FilterBtn.BorderColor = ThemeManager.SeparatorAndBorderColor;
            FilterBtn.HoverState.FillColor = ThemeManager.MainButtonHoverFillColor;
            FilterBtn.HoverState.BorderColor = ThemeManager.MainButtonHoverBorderColor;
            FilterBtn.PressedColor = ThemeManager.MainButtonPressedColor;
            FilterBtn.Image = Properties.Resources.filtro___cinza;

            ActiveFilter1.Visible = false;
            ActiveFilter2.Visible = false;
            ActiveFilter3.Visible = false;
            ActiveFilter4.Visible = false;
            ActiveFilter5.Visible = false;
            ActiveFilter6.Visible = false;
            ActiveFilter7.Visible = false;

            DataEspecifica.ForeColor = ThemeManager.FontColor;

            if (IsDarkModeEnabled)
                DataEspecifica.Image = Properties.Resources.data_branco;
            else
                DataEspecifica.Image = Properties.Resources.data_preto;

            RemoverFiltros.Visible = false;
            HideFrames();

            await TaskDelay(100);

            ReloadPage();
            RefreshPagination();

            OrderByColumn(SortedByItem);
            CheckedStateFunction();
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Exportar */

        // Botoes
        private async void ExportBtn_Click(object sender, EventArgs e)
        {
            if (ExportItens.Visible)
            {
                ExportItens.Visible = false;
                ExportItens.Location = new Point(ExportItens.Location.X, 88);

                if (ExcelFrame.Visible)
                {
                    ExcelFrame.Visible = false;
                    ExcelFrame.Location = new Point(ExcelFrame.Location.X - 6, ExcelFrame.Location.Y);
                }

                if (PdfFrame.Visible)
                {
                    PdfFrame.Visible = false;
                    PdfFrame.Location = new Point(PdfFrame.Location.X - 6, PdfFrame.Location.Y);
                }

                if (ExportOptions.Visible)
                {
                    ExportOptions.Visible = false;
                    ExportOptions.Location = new Point(ExportOptions.Location.X - 6, ExportOptions.Location.Y);
                }
            }
            else
            {
                HideFrames();

                if (AnimateButtonsAndPanels)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        await TaskDelay(10);
                        ExportItens.Location = new Point(ExportItens.Location.X, ExportItens.Location.Y + 1);
                        ExportItens.Visible = true;
                    }
                }
                else
                {
                    ExportItens.Location = new Point(ExportItens.Location.X, ExportItens.Location.Y + 6);
                    ExportItens.Visible = true;
                }
            }
        }

        private async void ExcelBtn_Click(object sender, EventArgs e)
        {
            if (ExcelFrame.Visible)
            {
                ExcelFrame.Visible = false;
                ExcelFrame.Location = new Point(ExcelFrame.Location.X - 6, ExcelFrame.Location.Y);
            }
            else
            {
                if (PdfFrame.Location == new Point(ExcelFrame.Location.X + 6, ExcelFrame.Location.Y))
                {
                    PdfFrame.Visible = false;
                    PdfFrame.Location = new Point(PdfFrame.Location.X - 6, PdfFrame.Location.Y);
                }

                if (ExportOptions.Location == new Point(ExcelFrame.Location.X + 6, ExcelFrame.Location.Y))
                {
                    ExportOptions.Visible = false;
                    ExportOptions.Location = new Point(ExportOptions.Location.X - 6, ExportOptions.Location.Y);
                }

                if (AnimateButtonsAndPanels)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        await TaskDelay(10);
                        ExcelFrame.Location = new Point(ExcelFrame.Location.X + 1, ExcelFrame.Location.Y);
                        ExcelFrame.Visible = true;
                    }
                }
                else
                {
                    ExcelFrame.Location = new Point(ExcelFrame.Location.X + 6, ExcelFrame.Location.Y);
                    ExcelFrame.Visible = true;
                }
            }
        }

        private async void PdfBtn_Click(object sender, EventArgs e)
        {
            if (PdfFrame.Visible)
            {
                PdfFrame.Visible = false;
                PdfFrame.Location = new Point(PdfFrame.Location.X - 6, PdfFrame.Location.Y);
            }
            else
            {
                if (ExcelFrame.Location == new Point(PdfFrame.Location.X + 6, PdfFrame.Location.Y))
                {
                    ExcelFrame.Visible = false;
                    ExcelFrame.Location = new Point(ExcelFrame.Location.X - 6, ExcelFrame.Location.Y);
                }

                if (ExportOptions.Location == new Point(PdfFrame.Location.X + 6, PdfFrame.Location.Y))
                {
                    ExportOptions.Visible = false;
                    ExportOptions.Location = new Point(ExportOptions.Location.X - 6, ExportOptions.Location.Y);
                }

                if (AnimateButtonsAndPanels)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        await TaskDelay(10);
                        PdfFrame.Location = new Point(PdfFrame.Location.X + 1, PdfFrame.Location.Y);
                        PdfFrame.Visible = true;
                    }
                }
                else
                {
                    PdfFrame.Location = new Point(PdfFrame.Location.X + 6, PdfFrame.Location.Y);
                    PdfFrame.Visible = true;
                }
            }
        }

        private async void ExportOptionsBtn_Click(object sender, EventArgs e)
        {
            if (ExportOptions.Visible)
            {
                ExportOptions.Visible = false;
                ExportOptions.Location = new Point(ExportOptions.Location.X - 6, ExportOptions.Location.Y);
            }
            else
            {
                if (ExcelFrame.Location == new Point(ExportOptions.Location.X + 6, ExportOptions.Location.Y))
                {
                    ExcelFrame.Visible = false;
                    ExcelFrame.Location = new Point(ExcelFrame.Location.X - 6, ExcelFrame.Location.Y);
                }

                if (PdfFrame.Location == new Point(ExportOptions.Location.X + 6, ExportOptions.Location.Y))
                {
                    PdfFrame.Visible = false;
                    PdfFrame.Location = new Point(PdfFrame.Location.X - 6, PdfFrame.Location.Y);
                }

                if (AnimateButtonsAndPanels)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        await TaskDelay(10);
                        ExportOptions.Location = new Point(ExportOptions.Location.X + 1, ExportOptions.Location.Y);
                        ExportOptions.Visible = true;
                    }
                }
                else
                {
                    ExportOptions.Location = new Point(ExportOptions.Location.X + 6, ExportOptions.Location.Y);
                    ExportOptions.Visible = true;
                }
            }
        }

        // Excel
        private async void ExportCurrentExcel_Click(object sender, EventArgs e)
        {
            ExportExcel.InitialDirectory = "";
            ExportExcel.Title = "Salvar como arquivo Excel";
            ExportExcel.FileName = "Planilha de serviços";
            ExportExcel.Filter = "Arquivo Excel|*.xlsx";

            if (ExportExcel.ShowDialog() != DialogResult.Cancel)
            {
                ThreadStart ts = new ThreadStart(() => {
                    DarkBackground(new Frames.FileExport());
                });

                Thread t = new Thread(ts);

                t.SetApartmentState(ApartmentState.STA);

                t.Start();

                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                ExcelApp.Application.Workbooks.Add(Type.Missing);

                if (Principais.Checked)
                {
                    ExcelApp.Columns[1].ColumnWidth = 10;
                    ExcelApp.Columns[2].ColumnWidth = 15;
                    ExcelApp.Columns[3].ColumnWidth = 30;
                    ExcelApp.Columns[4].ColumnWidth = 15;
                    ExcelApp.Columns[5].ColumnWidth = 20;
                    ExcelApp.Columns[6].ColumnWidth = 10;
                    ExcelApp.Columns[7].ColumnWidth = 10;
                    ExcelApp.Columns[8].ColumnWidth = 10;
                    ExcelApp.Columns[9].ColumnWidth = 20;
                    ExcelApp.Columns[10].ColumnWidth = 15;

                    for (int i = 1; i < ServicosGrid.Columns.Count; i++)
                    {
                        if (SortedByOrder == "Ascending")
                        {
                            ExcelApp.Cells[1] = ServicosGrid.Columns[0].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[2] = ServicosGrid.Columns[1].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[3] = ServicosGrid.Columns[2].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[4] = ServicosGrid.Columns[3].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[5] = ServicosGrid.Columns[5].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[6] = ServicosGrid.Columns[6].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[7] = ServicosGrid.Columns[8].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[8] = ServicosGrid.Columns[10].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[9] = ServicosGrid.Columns[21].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[10] = ServicosGrid.Columns[28].HeaderText.Replace("↑", "");
                        }

                        else if (SortedByOrder == "Descending")
                        {
                            ExcelApp.Cells[1] = ServicosGrid.Columns[0].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[2] = ServicosGrid.Columns[1].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[3] = ServicosGrid.Columns[2].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[4] = ServicosGrid.Columns[3].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[5] = ServicosGrid.Columns[5].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[6] = ServicosGrid.Columns[6].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[7] = ServicosGrid.Columns[8].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[8] = ServicosGrid.Columns[10].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[9] = ServicosGrid.Columns[21].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[10] = ServicosGrid.Columns[28].HeaderText.Replace("↓", "");
                        }
                    }

                    for (int i = 0; i < ServicosGrid.Rows.Count; i++)
                    {
                        for (int j = 1; j < ServicosGrid.Columns.Count; j++)
                        {
                            ExcelApp.Cells[i + 2, 1] = ServicosGrid.Rows[i].Cells[0].Value.ToString();
                            ExcelApp.Cells[i + 2, 2] = ServicosGrid.Rows[i].Cells[1].Value.ToString();
                            ExcelApp.Cells[i + 2, 3] = ServicosGrid.Rows[i].Cells[2].Value.ToString();
                            ExcelApp.Cells[i + 2, 4] = ServicosGrid.Rows[i].Cells[3].Value.ToString();
                            ExcelApp.Cells[i + 2, 5] = ServicosGrid.Rows[i].Cells[5].Value.ToString();
                            ExcelApp.Cells[i + 2, 6] = ServicosGrid.Rows[i].Cells[6].Value.ToString();
                            ExcelApp.Cells[i + 2, 7] = ServicosGrid.Rows[i].Cells[8].Value.ToString();
                            ExcelApp.Cells[i + 2, 8] = ServicosGrid.Rows[i].Cells[10].Value.ToString();
                            ExcelApp.Cells[i + 2, 9] = ServicosGrid.Rows[i].Cells[21].Value.ToString();
                            ExcelApp.Cells[i + 2, 10] = ServicosGrid.Rows[i].Cells[28].Value.ToString();
                        }
                    }
                }

                else if (AllColumns.Checked)
                {
                    ExcelApp.Columns[1].ColumnWidth = 10;
                    ExcelApp.Columns[2].ColumnWidth = 15;
                    ExcelApp.Columns[3].ColumnWidth = 30;
                    ExcelApp.Columns[4].ColumnWidth = 15;
                    ExcelApp.Columns[5].ColumnWidth = 15;
                    ExcelApp.Columns[6].ColumnWidth = 10;
                    ExcelApp.Columns[7].ColumnWidth = 10;
                    ExcelApp.Columns[8].ColumnWidth = 10;
                    ExcelApp.Columns[9].ColumnWidth = 10;
                    ExcelApp.Columns[10].ColumnWidth = 10;
                    ExcelApp.Columns[11].ColumnWidth = 20;
                    ExcelApp.Columns[12].ColumnWidth = 20;
                    ExcelApp.Columns[13].ColumnWidth = 10;
                    ExcelApp.Columns[14].ColumnWidth = 20;
                    ExcelApp.Columns[15].ColumnWidth = 15;
                    ExcelApp.Columns[16].ColumnWidth = 20;
                    ExcelApp.Columns[17].ColumnWidth = 20;
                    ExcelApp.Columns[18].ColumnWidth = 20;
                    ExcelApp.Columns[19].ColumnWidth = 10;
                    ExcelApp.Columns[20].ColumnWidth = 10;
                    ExcelApp.Columns[21].ColumnWidth = 30;
                    ExcelApp.Columns[22].ColumnWidth = 15;
                    ExcelApp.Columns[23].ColumnWidth = 15;
                    ExcelApp.Columns[24].ColumnWidth = 15;
                    ExcelApp.Columns[25].ColumnWidth = 15;
                    ExcelApp.Columns[26].ColumnWidth = 15;
                    ExcelApp.Columns[27].ColumnWidth = 15;

                    for (int i = 1; i < ServicosGrid.Columns.Count; i++)
                    {
                        if (SortedByOrder == "Ascending")
                        {
                            ExcelApp.Cells[1] = ServicosGrid.Columns[0].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[2] = ServicosGrid.Columns[1].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[3] = ServicosGrid.Columns[2].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[4] = ServicosGrid.Columns[3].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[5] = ServicosGrid.Columns[4].HeaderText;
                            ExcelApp.Cells[6] = ServicosGrid.Columns[5].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[7] = ServicosGrid.Columns[6].HeaderText;
                            ExcelApp.Cells[8] = ServicosGrid.Columns[7].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[9] = ServicosGrid.Columns[8].HeaderText;
                            ExcelApp.Cells[10] = ServicosGrid.Columns[9].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[11] = ServicosGrid.Columns[10].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[12] = ServicosGrid.Columns[11].HeaderText;
                            ExcelApp.Cells[13] = ServicosGrid.Columns[12].HeaderText;
                            ExcelApp.Cells[14] = ServicosGrid.Columns[13].HeaderText;
                            ExcelApp.Cells[15] = ServicosGrid.Columns[14].HeaderText;
                            ExcelApp.Cells[16] = ServicosGrid.Columns[15].HeaderText;
                            ExcelApp.Cells[17] = ServicosGrid.Columns[16].HeaderText;
                            ExcelApp.Cells[18] = ServicosGrid.Columns[17].HeaderText;
                            ExcelApp.Cells[19] = ServicosGrid.Columns[18].HeaderText;
                            ExcelApp.Cells[20] = ServicosGrid.Columns[19].HeaderText;
                            ExcelApp.Cells[21] = ServicosGrid.Columns[20].HeaderText;
                            ExcelApp.Cells[22] = ServicosGrid.Columns[21].HeaderText.Replace("↑", "") ;
                            ExcelApp.Cells[23] = ServicosGrid.Columns[22].HeaderText;
                            ExcelApp.Cells[24] = ServicosGrid.Columns[23].HeaderText;
                            ExcelApp.Cells[25] = ServicosGrid.Columns[24].HeaderText;
                            ExcelApp.Cells[26] = ServicosGrid.Columns[25].HeaderText;
                            ExcelApp.Cells[27] = ServicosGrid.Columns[28].HeaderText.Replace("↑", "");
                        }

                        else if (SortedByOrder == "Descending")
                        {
                            ExcelApp.Cells[1] = ServicosGrid.Columns[0].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[2] = ServicosGrid.Columns[1].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[3] = ServicosGrid.Columns[2].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[4] = ServicosGrid.Columns[3].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[5] = ServicosGrid.Columns[4].HeaderText;
                            ExcelApp.Cells[6] = ServicosGrid.Columns[5].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[7] = ServicosGrid.Columns[6].HeaderText;
                            ExcelApp.Cells[8] = ServicosGrid.Columns[7].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[9] = ServicosGrid.Columns[8].HeaderText;
                            ExcelApp.Cells[10] = ServicosGrid.Columns[9].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[11] = ServicosGrid.Columns[10].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[12] = ServicosGrid.Columns[11].HeaderText;
                            ExcelApp.Cells[13] = ServicosGrid.Columns[12].HeaderText;
                            ExcelApp.Cells[14] = ServicosGrid.Columns[13].HeaderText;
                            ExcelApp.Cells[15] = ServicosGrid.Columns[14].HeaderText;
                            ExcelApp.Cells[16] = ServicosGrid.Columns[15].HeaderText;
                            ExcelApp.Cells[17] = ServicosGrid.Columns[16].HeaderText;
                            ExcelApp.Cells[18] = ServicosGrid.Columns[17].HeaderText;
                            ExcelApp.Cells[19] = ServicosGrid.Columns[18].HeaderText;
                            ExcelApp.Cells[20] = ServicosGrid.Columns[19].HeaderText;
                            ExcelApp.Cells[21] = ServicosGrid.Columns[20].HeaderText;
                            ExcelApp.Cells[22] = ServicosGrid.Columns[21].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[23] = ServicosGrid.Columns[22].HeaderText; 
                            ExcelApp.Cells[24] = ServicosGrid.Columns[23].HeaderText;
                            ExcelApp.Cells[25] = ServicosGrid.Columns[24].HeaderText;
                            ExcelApp.Cells[26] = ServicosGrid.Columns[27].HeaderText;
                            ExcelApp.Cells[27] = ServicosGrid.Columns[28].HeaderText.Replace("↓", "");
                        }
                    }

                    for (int i = 0; i < ServicosGrid.Rows.Count; i++)
                    {
                        for (int j = 1; j < ServicosGrid.Columns.Count - 2; j++)
                        {
                            ExcelApp.Cells[i + 2, 1] = ServicosGrid.Rows[i].Cells[0].Value.ToString();
                            ExcelApp.Cells[i + 2, 2] = ServicosGrid.Rows[i].Cells[1].Value.ToString();
                            ExcelApp.Cells[i + 2, 3] = ServicosGrid.Rows[i].Cells[2].Value.ToString();
                            ExcelApp.Cells[i + 2, 4] = ServicosGrid.Rows[i].Cells[3].Value.ToString();
                            ExcelApp.Cells[i + 2, 5] = ServicosGrid.Rows[i].Cells[4].Value.ToString();
                            ExcelApp.Cells[i + 2, 6] = ServicosGrid.Rows[i].Cells[5].Value.ToString();
                            ExcelApp.Cells[i + 2, 7] = ServicosGrid.Rows[i].Cells[6].Value.ToString();
                            ExcelApp.Cells[i + 2, 8] = ServicosGrid.Rows[i].Cells[7].Value.ToString();
                            ExcelApp.Cells[i + 2, 9] = ServicosGrid.Rows[i].Cells[8].Value.ToString();
                            ExcelApp.Cells[i + 2, 10] = ServicosGrid.Rows[i].Cells[9].Value.ToString();
                            ExcelApp.Cells[i + 2, 11] = ServicosGrid.Rows[i].Cells[10].Value.ToString();
                            ExcelApp.Cells[i + 2, 12] = ServicosGrid.Rows[i].Cells[11].Value.ToString();
                            ExcelApp.Cells[i + 2, 13] = ServicosGrid.Rows[i].Cells[12].Value.ToString();
                            ExcelApp.Cells[i + 2, 14] = ServicosGrid.Rows[i].Cells[13].Value.ToString();
                            ExcelApp.Cells[i + 2, 15] = ServicosGrid.Rows[i].Cells[14].Value.ToString();
                            ExcelApp.Cells[i + 2, 16] = ServicosGrid.Rows[i].Cells[15].Value.ToString();
                            ExcelApp.Cells[i + 2, 17] = ServicosGrid.Rows[i].Cells[16].Value.ToString();
                            ExcelApp.Cells[i + 2, 18] = ServicosGrid.Rows[i].Cells[17].Value.ToString();
                            ExcelApp.Cells[i + 2, 19] = ServicosGrid.Rows[i].Cells[18].Value.ToString();
                            ExcelApp.Cells[i + 2, 20] = ServicosGrid.Rows[i].Cells[19].Value.ToString();
                            ExcelApp.Cells[i + 2, 21] = ServicosGrid.Rows[i].Cells[20].Value.ToString();
                            ExcelApp.Cells[i + 2, 22] = ServicosGrid.Rows[i].Cells[21].Value.ToString();
                            ExcelApp.Cells[i + 2, 23] = ServicosGrid.Rows[i].Cells[22].Value.ToString();
                            ExcelApp.Cells[i + 2, 24] = ServicosGrid.Rows[i].Cells[23].Value.ToString();
                            ExcelApp.Cells[i + 2, 25] = ServicosGrid.Rows[i].Cells[24].Value.ToString();
                            ExcelApp.Cells[i + 2, 26] = ServicosGrid.Rows[i].Cells[27].Value.ToString();
                            ExcelApp.Cells[i + 2, 27] = ServicosGrid.Rows[i].Cells[28].Value.ToString();
                        }
                    }
                }

                ExcelApp.ActiveWorkbook.SaveCopyAs(ExportExcel.FileName.ToString());
                ExcelApp.ActiveWorkbook.Saved = true;

                if (ExcelApp.ActiveWorkbook.Saved == true)
                {
                    Frames.Success.SuccessFrame.LblText.Text = "Dados exportados com sucesso!";

                    ThreadStart ts2 = new ThreadStart(() => {
                        DarkBackground(new Frames.Success());
                        SuccessForm.Text = "Dados exportados com sucesso!";
                    });

                    t.Abort();

                    await TaskDelay(800);

                    Thread t2 = new Thread(ts2);

                    t2.SetApartmentState(ApartmentState.STA);

                    t2.Start();

                    ExcelApp.Quit();
                }
            }
        }

        private async void ExportAllExcel_Click(object sender, EventArgs e)
        {
            ExportExcel.InitialDirectory = "";
            ExportExcel.Title = "Salvar como arquivo Excel";
            ExportExcel.FileName = "Planilha geral de serviços";
            ExportExcel.Filter = "Arquivo Excel|*.xlsx";

            if (ExportExcel.ShowDialog() != DialogResult.Cancel)
            {
                ThreadStart ts = new ThreadStart(() => {
                    DarkBackground(new Frames.FileExport());
                });

                Thread t = new Thread(ts);

                t.SetApartmentState(ApartmentState.STA);

                t.Start();

                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                ExcelApp.Application.Workbooks.Add(Type.Missing);

                if (Principais.Checked)
                {
                    ExcelApp.Columns[1].ColumnWidth = 10;
                    ExcelApp.Columns[2].ColumnWidth = 15;
                    ExcelApp.Columns[3].ColumnWidth = 30;
                    ExcelApp.Columns[4].ColumnWidth = 15;
                    ExcelApp.Columns[5].ColumnWidth = 20;
                    ExcelApp.Columns[6].ColumnWidth = 10;
                    ExcelApp.Columns[7].ColumnWidth = 10;
                    ExcelApp.Columns[8].ColumnWidth = 10;
                    ExcelApp.Columns[9].ColumnWidth = 20;
                    ExcelApp.Columns[10].ColumnWidth = 15;

                    for (int i = 1; i < AllDataGrid.Columns.Count; i++)
                    {
                        if (SortedByOrder == "Ascending")
                        {
                            ExcelApp.Cells[1] = ServicosGrid.Columns[0].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[2] = ServicosGrid.Columns[1].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[3] = ServicosGrid.Columns[2].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[4] = ServicosGrid.Columns[3].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[5] = ServicosGrid.Columns[5].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[6] = ServicosGrid.Columns[6].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[7] = ServicosGrid.Columns[8].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[8] = ServicosGrid.Columns[10].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[9] = ServicosGrid.Columns[21].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[10] = ServicosGrid.Columns[28].HeaderText.Replace("↑", "");
                        }

                        else if (SortedByOrder == "Descending")
                        {
                            ExcelApp.Cells[1] = ServicosGrid.Columns[0].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[2] = ServicosGrid.Columns[1].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[3] = ServicosGrid.Columns[2].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[4] = ServicosGrid.Columns[3].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[5] = ServicosGrid.Columns[5].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[6] = ServicosGrid.Columns[6].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[7] = ServicosGrid.Columns[8].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[8] = ServicosGrid.Columns[10].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[9] = ServicosGrid.Columns[21].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[10] = ServicosGrid.Columns[28].HeaderText.Replace("↓", "");
                        }
                    }

                    for (int i = 0; i < AllDataGrid.Rows.Count; i++)
                    {
                        for (int j = 1; j < AllDataGrid.Columns.Count; j++)
                        {
                            ExcelApp.Cells[i + 2, 1] = ServicosGrid.Rows[i].Cells[0].Value.ToString();
                            ExcelApp.Cells[i + 2, 2] = ServicosGrid.Rows[i].Cells[1].Value.ToString();
                            ExcelApp.Cells[i + 2, 3] = ServicosGrid.Rows[i].Cells[2].Value.ToString();
                            ExcelApp.Cells[i + 2, 4] = ServicosGrid.Rows[i].Cells[3].Value.ToString();
                            ExcelApp.Cells[i + 2, 5] = ServicosGrid.Rows[i].Cells[5].Value.ToString();
                            ExcelApp.Cells[i + 2, 6] = ServicosGrid.Rows[i].Cells[6].Value.ToString();
                            ExcelApp.Cells[i + 2, 7] = ServicosGrid.Rows[i].Cells[8].Value.ToString();
                            ExcelApp.Cells[i + 2, 8] = ServicosGrid.Rows[i].Cells[10].Value.ToString();
                            ExcelApp.Cells[i + 2, 9] = ServicosGrid.Rows[i].Cells[21].Value.ToString();
                            ExcelApp.Cells[i + 2, 10] = ServicosGrid.Rows[i].Cells[28].Value.ToString();
                        }
                    }
                }

                else if (AllColumns.Checked)
                {
                    ExcelApp.Columns[1].ColumnWidth = 10;
                    ExcelApp.Columns[2].ColumnWidth = 15;
                    ExcelApp.Columns[3].ColumnWidth = 30;
                    ExcelApp.Columns[4].ColumnWidth = 15;
                    ExcelApp.Columns[5].ColumnWidth = 15;
                    ExcelApp.Columns[6].ColumnWidth = 10;
                    ExcelApp.Columns[7].ColumnWidth = 10;
                    ExcelApp.Columns[8].ColumnWidth = 10;
                    ExcelApp.Columns[9].ColumnWidth = 10;
                    ExcelApp.Columns[10].ColumnWidth = 10;
                    ExcelApp.Columns[11].ColumnWidth = 20;
                    ExcelApp.Columns[12].ColumnWidth = 20;
                    ExcelApp.Columns[13].ColumnWidth = 10;
                    ExcelApp.Columns[14].ColumnWidth = 20;
                    ExcelApp.Columns[15].ColumnWidth = 15;
                    ExcelApp.Columns[16].ColumnWidth = 20;
                    ExcelApp.Columns[17].ColumnWidth = 20;
                    ExcelApp.Columns[18].ColumnWidth = 20;
                    ExcelApp.Columns[19].ColumnWidth = 10;
                    ExcelApp.Columns[20].ColumnWidth = 10;
                    ExcelApp.Columns[21].ColumnWidth = 30;
                    ExcelApp.Columns[22].ColumnWidth = 15;
                    ExcelApp.Columns[23].ColumnWidth = 15;
                    ExcelApp.Columns[24].ColumnWidth = 15;
                    ExcelApp.Columns[25].ColumnWidth = 15;
                    ExcelApp.Columns[26].ColumnWidth = 15;
                    ExcelApp.Columns[27].ColumnWidth = 15;

                    for (int i = 1; i < AllDataGrid.Columns.Count; i++)
                    {
                        if (SortedByOrder == "Ascending")
                        {
                            ExcelApp.Cells[1] = ServicosGrid.Columns[0].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[2] = ServicosGrid.Columns[1].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[3] = ServicosGrid.Columns[2].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[4] = ServicosGrid.Columns[3].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[5] = ServicosGrid.Columns[4].HeaderText;
                            ExcelApp.Cells[6] = ServicosGrid.Columns[5].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[7] = ServicosGrid.Columns[6].HeaderText;
                            ExcelApp.Cells[8] = ServicosGrid.Columns[7].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[9] = ServicosGrid.Columns[8].HeaderText;
                            ExcelApp.Cells[10] = ServicosGrid.Columns[9].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[11] = ServicosGrid.Columns[10].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[12] = ServicosGrid.Columns[11].HeaderText;
                            ExcelApp.Cells[13] = ServicosGrid.Columns[12].HeaderText;
                            ExcelApp.Cells[14] = ServicosGrid.Columns[13].HeaderText;
                            ExcelApp.Cells[15] = ServicosGrid.Columns[14].HeaderText;
                            ExcelApp.Cells[16] = ServicosGrid.Columns[15].HeaderText;
                            ExcelApp.Cells[17] = ServicosGrid.Columns[16].HeaderText;
                            ExcelApp.Cells[18] = ServicosGrid.Columns[17].HeaderText;
                            ExcelApp.Cells[19] = ServicosGrid.Columns[18].HeaderText;
                            ExcelApp.Cells[20] = ServicosGrid.Columns[19].HeaderText;
                            ExcelApp.Cells[21] = ServicosGrid.Columns[20].HeaderText;
                            ExcelApp.Cells[22] = ServicosGrid.Columns[21].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[23] = ServicosGrid.Columns[22].HeaderText;
                            ExcelApp.Cells[24] = ServicosGrid.Columns[23].HeaderText;
                            ExcelApp.Cells[25] = ServicosGrid.Columns[24].HeaderText;
                            ExcelApp.Cells[26] = ServicosGrid.Columns[25].HeaderText;
                            ExcelApp.Cells[27] = ServicosGrid.Columns[28].HeaderText.Replace("↑", "");
                        }

                        else if (SortedByOrder == "Descending")
                        {
                            ExcelApp.Cells[1] = ServicosGrid.Columns[0].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[2] = ServicosGrid.Columns[1].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[3] = ServicosGrid.Columns[2].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[4] = ServicosGrid.Columns[3].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[5] = ServicosGrid.Columns[4].HeaderText;
                            ExcelApp.Cells[6] = ServicosGrid.Columns[5].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[7] = ServicosGrid.Columns[6].HeaderText;
                            ExcelApp.Cells[8] = ServicosGrid.Columns[7].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[9] = ServicosGrid.Columns[8].HeaderText;
                            ExcelApp.Cells[10] = ServicosGrid.Columns[9].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[11] = ServicosGrid.Columns[10].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[12] = ServicosGrid.Columns[11].HeaderText;
                            ExcelApp.Cells[13] = ServicosGrid.Columns[12].HeaderText;
                            ExcelApp.Cells[14] = ServicosGrid.Columns[13].HeaderText;
                            ExcelApp.Cells[15] = ServicosGrid.Columns[14].HeaderText;
                            ExcelApp.Cells[16] = ServicosGrid.Columns[15].HeaderText;
                            ExcelApp.Cells[17] = ServicosGrid.Columns[16].HeaderText;
                            ExcelApp.Cells[18] = ServicosGrid.Columns[17].HeaderText;
                            ExcelApp.Cells[19] = ServicosGrid.Columns[18].HeaderText;
                            ExcelApp.Cells[20] = ServicosGrid.Columns[19].HeaderText;
                            ExcelApp.Cells[21] = ServicosGrid.Columns[20].HeaderText;
                            ExcelApp.Cells[22] = ServicosGrid.Columns[21].HeaderText;
                            ExcelApp.Cells[23] = ServicosGrid.Columns[22].HeaderText.Replace("↓", ""); ;
                            ExcelApp.Cells[24] = ServicosGrid.Columns[23].HeaderText;
                            ExcelApp.Cells[25] = ServicosGrid.Columns[24].HeaderText;
                            ExcelApp.Cells[26] = ServicosGrid.Columns[25].HeaderText;
                            ExcelApp.Cells[27] = ServicosGrid.Columns[26].HeaderText;
                            ExcelApp.Cells[28] = ServicosGrid.Columns[27].HeaderText;
                            ExcelApp.Cells[29] = ServicosGrid.Columns[28].HeaderText.Replace("↓", ""); ;
                        }
                    }

                    for (int i = 0; i < AllDataGrid.Rows.Count; i++)
                    {
                        for (int j = 1; j < AllDataGrid.Columns.Count - 2; j++)
                        {
                            ExcelApp.Cells[i + 2, 1] = ServicosGrid.Rows[i].Cells[0].Value.ToString();
                            ExcelApp.Cells[i + 2, 2] = ServicosGrid.Rows[i].Cells[1].Value.ToString();
                            ExcelApp.Cells[i + 2, 3] = ServicosGrid.Rows[i].Cells[2].Value.ToString();
                            ExcelApp.Cells[i + 2, 4] = ServicosGrid.Rows[i].Cells[3].Value.ToString();
                            ExcelApp.Cells[i + 2, 5] = ServicosGrid.Rows[i].Cells[4].Value.ToString();
                            ExcelApp.Cells[i + 2, 6] = ServicosGrid.Rows[i].Cells[5].Value.ToString();
                            ExcelApp.Cells[i + 2, 7] = ServicosGrid.Rows[i].Cells[6].Value.ToString();
                            ExcelApp.Cells[i + 2, 8] = ServicosGrid.Rows[i].Cells[7].Value.ToString();
                            ExcelApp.Cells[i + 2, 9] = ServicosGrid.Rows[i].Cells[8].Value.ToString();
                            ExcelApp.Cells[i + 2, 10] = ServicosGrid.Rows[i].Cells[9].Value.ToString();
                            ExcelApp.Cells[i + 2, 11] = ServicosGrid.Rows[i].Cells[10].Value.ToString();
                            ExcelApp.Cells[i + 2, 12] = ServicosGrid.Rows[i].Cells[11].Value.ToString();
                            ExcelApp.Cells[i + 2, 13] = ServicosGrid.Rows[i].Cells[12].Value.ToString();
                            ExcelApp.Cells[i + 2, 14] = ServicosGrid.Rows[i].Cells[13].Value.ToString();
                            ExcelApp.Cells[i + 2, 15] = ServicosGrid.Rows[i].Cells[14].Value.ToString();
                            ExcelApp.Cells[i + 2, 16] = ServicosGrid.Rows[i].Cells[15].Value.ToString();
                            ExcelApp.Cells[i + 2, 17] = ServicosGrid.Rows[i].Cells[16].Value.ToString();
                            ExcelApp.Cells[i + 2, 18] = ServicosGrid.Rows[i].Cells[17].Value.ToString();
                            ExcelApp.Cells[i + 2, 19] = ServicosGrid.Rows[i].Cells[18].Value.ToString();
                            ExcelApp.Cells[i + 2, 20] = ServicosGrid.Rows[i].Cells[19].Value.ToString();
                            ExcelApp.Cells[i + 2, 21] = ServicosGrid.Rows[i].Cells[20].Value.ToString();
                            ExcelApp.Cells[i + 2, 22] = ServicosGrid.Rows[i].Cells[21].Value.ToString();
                            ExcelApp.Cells[i + 2, 23] = ServicosGrid.Rows[i].Cells[22].Value.ToString();
                            ExcelApp.Cells[i + 2, 24] = ServicosGrid.Rows[i].Cells[23].Value.ToString();
                            ExcelApp.Cells[i + 2, 25] = ServicosGrid.Rows[i].Cells[24].Value.ToString();
                            ExcelApp.Cells[i + 2, 26] = ServicosGrid.Rows[i].Cells[27].Value.ToString();
                            ExcelApp.Cells[i + 2, 27] = ServicosGrid.Rows[i].Cells[28].Value.ToString();
                        }
                    }
                }

                ExcelApp.ActiveWorkbook.SaveCopyAs(ExportExcel.FileName.ToString());
                ExcelApp.ActiveWorkbook.Saved = true;

                if (ExcelApp.ActiveWorkbook.Saved == true)
                {
                    Frames.Success.SuccessFrame.LblText.Text = "Dados exportados com sucesso!";

                    ThreadStart ts2 = new ThreadStart(() => {
                        DarkBackground(new Frames.Success());
                        SuccessForm.Text = "Dados exportados com sucesso!";
                    });

                    t.Abort();

                    await TaskDelay(800);

                    Thread t2 = new Thread(ts2);

                    t2.SetApartmentState(ApartmentState.STA);

                    t2.Start();

                    ExcelApp.Quit();
                }
            }
        }


        // PDF
        private async void ExportCurrentPdf_Click(object sender, EventArgs e)
        {
            if (ServicosGrid.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Relatório de serviços.pdf";
                bool fileError = false;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }

                        catch (IOException)
                        {
                            fileError = true;

                            Frames.Erro.ErrorFrame.LblText.Text = "Erro ao exportar dados!";

                            ThreadStart ErrorStart1 = new ThreadStart(() => {
                                DarkBackground(new Frames.Erro());
                                ErrorForm.Text = "Erro ao exportar dados!";
                            });

                            await TaskDelay(800);

                            Thread ErrorThread1 = new Thread(ErrorStart1);

                            ErrorThread1.SetApartmentState(ApartmentState.STA);

                            ErrorThread1.Start();
                        }
                    }

                    if (!fileError)
                    {
                        if (Principais.Checked)
                        {
                            try
                            {
                                PdfPTable pdfTable = new PdfPTable(ServicosGrid.Columns.Count - 18);
                                pdfTable.DefaultCell.Padding = 3;
                                pdfTable.WidthPercentage = 100;
                                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                                foreach (DataGridViewColumn column in ServicosGrid.Columns)
                                {
                                    if (column.Index != 7 && column.Index != 9 && column.Index != 11 && column.Index != 12 && column.Index != 13 && column.Index != 14
                                        && column.Index != 15 && column.Index != 16 && column.Index != 17 && column.Index != 18 && column.Index != 19 && column.Index != 20
                                        && column.Index != 22 && column.Index != 23 && column.Index != 24 && column.Index != 25 && column.Index != 26
                                        && column.Index != 30)
                                    {
                                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                        cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                                        cell.Padding = 6;
                                        pdfTable.AddCell(cell);
                                    }
                                }

                                foreach (DataGridViewRow row in ServicosGrid.Rows)
                                {
                                    foreach (DataGridViewCell cell in row.Cells)
                                    {
                                        if (cell.ColumnIndex != 7 && cell.ColumnIndex != 9 && cell.ColumnIndex != 11 && cell.ColumnIndex != 12 && cell.ColumnIndex != 13 && cell.ColumnIndex != 14
                                        && cell.ColumnIndex != 15 && cell.ColumnIndex != 16 && cell.ColumnIndex != 17 && cell.ColumnIndex != 18 && cell.ColumnIndex != 19 && cell.ColumnIndex != 20
                                        && cell.ColumnIndex != 22 && cell.ColumnIndex != 23 && cell.ColumnIndex != 24 && cell.ColumnIndex != 25 && cell.ColumnIndex != 26
                                        && cell.ColumnIndex != 29 && cell.ColumnIndex != 30)
                                            pdfTable.AddCell(cell.Value.ToString());
                                    }
                                }

                                using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                                {
                                    Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);

                                    PdfWriter.GetInstance(pdfDoc, stream);
                                    pdfDoc.Open();

                                    // Logo
                                    var img = Properties.Resources.Logo_250;

                                    iTextSharp.text.Image Logo = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Png);
                                    Logo.ScaleToFit(90, 90);
                                    Logo.Alignment = Element.ALIGN_LEFT;
                                    Logo.SetAbsolutePosition(pdfDoc.LeftMargin + 5, pdfDoc.Top - 85);
                                    pdfDoc.Add(Logo);

                                    // Informaçoes da oficina
                                    var FonteBase = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

                                    var NomeParagraph = new iTextSharp.text.Font(FonteBase, 15, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                                    var Nome = new Paragraph("                           CLÍNICA CAR                                                  " +
                                        "Relatório de serviços", NomeParagraph);
                                    Nome.Alignment = Element.ALIGN_LEFT;
                                    pdfDoc.Add(Nome);

                                    var EnderecoParagraph = new iTextSharp.text.Font(FonteBase, 15, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                                    var Endereco = new Paragraph("                           Rua Lucélia - Colônia Fepasa                                 Data: " + DateTime.Now.ToShortDateString(), EnderecoParagraph);
                                    Endereco.Alignment = Element.ALIGN_LEFT;
                                    pdfDoc.Add(Endereco);

                                    var CidadeParagraph = new iTextSharp.text.Font(FonteBase, 15, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                                    var Cidade = new Paragraph("                           Flórida Paulista - SP                                                   Hora: " + DateTime.Now.ToLongTimeString(), CidadeParagraph);
                                    Cidade.Alignment = Element.ALIGN_LEFT;
                                    pdfDoc.Add(Cidade);

                                    // Linha pra separar
                                    Paragraph Linha = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.25F, 101.25F, BaseColor.LIGHT_GRAY, Element.ALIGN_LEFT, 1)));
                                    pdfDoc.Add(Linha);

                                    // Espaço
                                    pdfDoc.Add(new Chunk("\n"));

                                    // Texto  
                                    var TextoParagraph = new iTextSharp.text.Font(FonteBase, 26, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                                    if (DataFiltered)
                                    {
                                        if (FilterByDays == 1)
                                        {
                                            var Texto = new Paragraph("Tabela de Serviços - Hoje\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        if (FilterByDays == 8)
                                        {
                                            var Texto = new Paragraph("Tabela de Serviços - Últimos 7 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 31)
                                        {
                                            var Texto = new Paragraph("Tabela de Serviços - Últimos 30 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 365)
                                        {
                                            var Texto = new Paragraph("Tabela de Serviços - Últimos 365 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }
                                    }

                                    else
                                    {
                                        var Texto = new Paragraph("Tabela de Serviços\n\n", TextoParagraph);

                                        Texto.Alignment = Element.ALIGN_CENTER;
                                        pdfDoc.Add(Texto);
                                    }

                                    // Criar pdf
                                    pdfDoc.Add(pdfTable);
                                    pdfDoc.Close();
                                    stream.Close();
                                }

                                Frames.Success.SuccessFrame.LblText.Text = "Dados exportados com sucesso!";

                                ThreadStart SuccessStart = new ThreadStart(() => {
                                    DarkBackground(new Frames.Success());
                                    SuccessForm.Text = "Dados exportados com sucesso!";
                                });

                                await TaskDelay(800);

                                Thread SuccessThread = new Thread(SuccessStart);

                                SuccessThread.SetApartmentState(ApartmentState.STA);

                                SuccessThread.Start();
                            }

                            catch (Exception)
                            {
                                Frames.Erro.ErrorFrame.LblText.Text = "Erro ao exportar dados!";

                                ThreadStart ErrorStart2 = new ThreadStart(() => {
                                    DarkBackground(new Frames.Erro());
                                    ErrorForm.Text = "Erro ao exportar dados!";
                                });

                                Thread ErrorThread2 = new Thread(ErrorStart2);

                                ErrorThread2.SetApartmentState(ApartmentState.STA);

                                ErrorThread2.Start();
                            }
                        }

                        else if (AllColumns.Checked)
                        {
                            try
                            {
                                PdfPTable pdfTable = new PdfPTable(ServicosGrid.Columns.Count - 21);
                                pdfTable.DefaultCell.Padding = 6;
                                pdfTable.WidthPercentage = 101.25F;
                                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                                foreach (DataGridViewColumn column in ServicosGrid.Columns)
                                {
                                    if (column.Index != 25 && column.Index != 26 && column.Index != 29 && column.Index != 30)
                                    {
                                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                        cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                                        cell.Padding = 8;
                                        pdfTable.AddCell(cell);
                                    }
                                }

                                foreach (DataGridViewRow row in ServicosGrid.Rows)
                                {
                                    foreach (DataGridViewCell cell in row.Cells)
                                    {
                                        if (cell.ColumnIndex != 25 && cell.ColumnIndex != 25 && cell.ColumnIndex != 30)
                                            pdfTable.AddCell(cell.Value.ToString());
                                    }
                                }

                                using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                                {
                                    Document pdfDoc = new Document(PageSize.A4.Rotate(), 10f, 20f, 20f, 10f);

                                    PdfWriter.GetInstance(pdfDoc, stream);
                                    pdfDoc.Open();

                                    // Logo
                                    var img = Properties.Resources.Logo_250;

                                    iTextSharp.text.Image Logo = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Png);
                                    Logo.ScaleToFit(90, 90);
                                    Logo.Alignment = Element.ALIGN_LEFT;
                                    Logo.SetAbsolutePosition(pdfDoc.LeftMargin + 5, pdfDoc.Top - 85);
                                    pdfDoc.Add(Logo);

                                    // Informaçoes da oficina
                                    var FonteBase = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

                                    var NomeParagraph = new iTextSharp.text.Font(FonteBase, 15, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                                    var Nome = new Paragraph("                           CLÍNICA CAR                                                  " +
                                        "Relatório de serviços", NomeParagraph);
                                    Nome.Alignment = Element.ALIGN_LEFT;
                                    pdfDoc.Add(Nome);

                                    var EnderecoParagraph = new iTextSharp.text.Font(FonteBase, 15, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                                    var Endereco = new Paragraph("                           Rua Lucélia - Colônia Fepasa                                 Data: " + DateTime.Now.ToShortDateString(), EnderecoParagraph);
                                    Endereco.Alignment = Element.ALIGN_LEFT;
                                    pdfDoc.Add(Endereco);

                                    var CidadeParagraph = new iTextSharp.text.Font(FonteBase, 15, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                                    var Cidade = new Paragraph("                           Flórida Paulista - SP                                                   Hora: " + DateTime.Now.ToLongTimeString(), CidadeParagraph);
                                    Cidade.Alignment = Element.ALIGN_LEFT;
                                    pdfDoc.Add(Cidade);

                                    // Linha pra separar
                                    Paragraph Linha = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.25F, 101.25F, BaseColor.LIGHT_GRAY, Element.ALIGN_LEFT, 1)));
                                    pdfDoc.Add(Linha);

                                    // Espaço
                                    pdfDoc.Add(new Chunk("\n"));

                                    // Texto  
                                    var TextoParagraph = new iTextSharp.text.Font(FonteBase, 26, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                                    if (DataFiltered)
                                    {
                                        if (FilterByDays == 1)
                                        {
                                            var Texto = new Paragraph("Tabela de Serviços - Hoje\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        if (FilterByDays == 8)
                                        {
                                            var Texto = new Paragraph("Tabela de Serviços - Últimos 7 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 31)
                                        {
                                            var Texto = new Paragraph("Tabela de Serviços - Últimos 30 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 365)
                                        {
                                            var Texto = new Paragraph("Tabela de Serviços - Últimos 365 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }
                                    }

                                    else
                                    {
                                        var Texto = new Paragraph("Tabela de Serviços\n\n", TextoParagraph);

                                        Texto.Alignment = Element.ALIGN_CENTER;
                                        pdfDoc.Add(Texto);
                                    }

                                    // Criar pdf
                                    pdfDoc.Add(pdfTable);
                                    pdfDoc.Close();
                                    stream.Close();
                                }

                                Frames.Success.SuccessFrame.LblText.Text = "Dados exportados com sucesso!";

                                ThreadStart SuccessStart = new ThreadStart(() => {
                                    DarkBackground(new Frames.Success());
                                    SuccessForm.Text = "Dados exportados com sucesso!";
                                });

                                await TaskDelay(800);

                                Thread SuccessThread = new Thread(SuccessStart);

                                SuccessThread.SetApartmentState(ApartmentState.STA);

                                SuccessThread.Start();
                            }

                            catch (Exception)
                            {
                                Frames.Erro.ErrorFrame.LblText.Text = "Erro ao exportar dados!";

                                ThreadStart ErrorStart2 = new ThreadStart(() => {
                                    DarkBackground(new Frames.Erro());
                                    ErrorForm.Text = "Erro ao exportar dados!";
                                });

                                Thread ErrorThread2 = new Thread(ErrorStart2);

                                ErrorThread2.SetApartmentState(ApartmentState.STA);

                                ErrorThread2.Start();
                            }
                        }
                    }
                }
            }

            else
            {
                Frames.Erro.ErrorFrame.LblText.Text = "Erro ao exportar dados!";

                ThreadStart ErrorStart3 = new ThreadStart(() => {
                    DarkBackground(new Frames.Erro());
                    ErrorForm.Text = "Erro ao exportar dados!";
                });

                Thread ErrorThread3 = new Thread(ErrorStart3);

                ErrorThread3.SetApartmentState(ApartmentState.STA);

                ErrorThread3.Start();
            }
        }

        private async void ExportAllPdf_Click(object sender, EventArgs e)
        {
            if (AllDataGrid.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Relatório geral de serviços.pdf";
                bool fileError = false;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }

                        catch (IOException)
                        {
                            fileError = true;

                            Frames.Erro.ErrorFrame.LblText.Text = "Erro ao exportar dados!";

                            ThreadStart ErrorStart1 = new ThreadStart(() => {
                                DarkBackground(new Frames.Erro());
                                ErrorForm.Text = "Erro ao exportar dados!";
                            });

                            await TaskDelay(800);

                            Thread ErrorThread1 = new Thread(ErrorStart1);

                            ErrorThread1.SetApartmentState(ApartmentState.STA);

                            ErrorThread1.Start();
                        }
                    }

                    if (!fileError)
                    {
                        if (Principais.Checked)
                        {
                            try
                            {
                                PdfPTable pdfTable = new PdfPTable(AllDataGrid.Columns.Count - 18);
                                pdfTable.DefaultCell.Padding = 3;
                                pdfTable.WidthPercentage = 100;
                                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                                foreach (DataGridViewColumn column in AllDataGrid.Columns)
                                {
                                    if (column.Index != 7 && column.Index != 9 && column.Index != 11 && column.Index != 12 && column.Index != 13 && column.Index != 14
                                        && column.Index != 15 && column.Index != 16 && column.Index != 17 && column.Index != 18 && column.Index != 19 && column.Index != 20
                                        && column.Index != 22 && column.Index != 23 && column.Index != 24 && column.Index != 25 && column.Index != 26
                                        && column.Index != 30)
                                    {
                                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                        cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                                        cell.Padding = 8;
                                        pdfTable.AddCell(cell);
                                    }
                                }

                                foreach (DataGridViewRow row in AllDataGrid.Rows)
                                {
                                    foreach (DataGridViewCell cell in row.Cells)
                                    {
                                        if (cell.ColumnIndex != 7 && cell.ColumnIndex != 9 && cell.ColumnIndex != 11 && cell.ColumnIndex != 12 && cell.ColumnIndex != 13 && cell.ColumnIndex != 14
                                        && cell.ColumnIndex != 15 && cell.ColumnIndex != 16 && cell.ColumnIndex != 17 && cell.ColumnIndex != 18 && cell.ColumnIndex != 19 && cell.ColumnIndex != 20
                                        && cell.ColumnIndex != 22 && cell.ColumnIndex != 23 && cell.ColumnIndex != 24 && cell.ColumnIndex != 25 && cell.ColumnIndex != 26
                                        && cell.ColumnIndex != 29 && cell.ColumnIndex != 30)
                                            pdfTable.AddCell(cell.Value.ToString());
                                    }
                                }

                                using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                                {
                                    Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);

                                    PdfWriter.GetInstance(pdfDoc, stream);
                                    pdfDoc.Open();

                                    // Logo
                                    var img = Properties.Resources.Logo_250;

                                    iTextSharp.text.Image Logo = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Png);
                                    Logo.ScaleToFit(90, 90);
                                    Logo.Alignment = Element.ALIGN_LEFT;
                                    Logo.SetAbsolutePosition(pdfDoc.LeftMargin + 5, pdfDoc.Top - 85);
                                    pdfDoc.Add(Logo);

                                    // Informaçoes da oficina
                                    var FonteBase = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

                                    var NomeParagraph = new iTextSharp.text.Font(FonteBase, 15, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                                    var Nome = new Paragraph("                           CLÍNICA CAR                                                   " +
                                        "Relatório de serviços", NomeParagraph);
                                    Nome.Alignment = Element.ALIGN_LEFT;
                                    pdfDoc.Add(Nome);

                                    var EnderecoParagraph = new iTextSharp.text.Font(FonteBase, 15, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                                    var Endereco = new Paragraph("                           Rua Lucélia - Colônia Fepasa                                 Data: " + DateTime.Now.ToShortDateString(), EnderecoParagraph);
                                    Endereco.Alignment = Element.ALIGN_LEFT;
                                    pdfDoc.Add(Endereco);

                                    var CidadeParagraph = new iTextSharp.text.Font(FonteBase, 15, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                                    var Cidade = new Paragraph("                           Flórida Paulista - SP                                                   Hora: " + DateTime.Now.ToLongTimeString(), CidadeParagraph);
                                    Cidade.Alignment = Element.ALIGN_LEFT;
                                    pdfDoc.Add(Cidade);

                                    // Linha pra separar
                                    Paragraph Linha = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.25F, 101.25F, BaseColor.LIGHT_GRAY, Element.ALIGN_LEFT, 1)));
                                    pdfDoc.Add(Linha);

                                    // Espaço
                                    pdfDoc.Add(new Chunk("\n"));

                                    // Texto  
                                    var TextoParagraph = new iTextSharp.text.Font(FonteBase, 26, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                                    if (DataFiltered)
                                    {
                                        if (FilterByDays == 1)
                                        {
                                            var Texto = new Paragraph("Tabela geral de Serviços - Hoje\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        if (FilterByDays == 8)
                                        {
                                            var Texto = new Paragraph("Tabela geral de Serviços - Últimos 7 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 31)
                                        {
                                            var Texto = new Paragraph("Tabela ggeral de Serviços - Últimos 30 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 365)
                                        {
                                            var Texto = new Paragraph("Tabela geral de Serviços - Últimos 365 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }
                                    }

                                    else
                                    {
                                        var Texto = new Paragraph("Tabela de Serviços\n\n", TextoParagraph);

                                        Texto.Alignment = Element.ALIGN_CENTER;
                                        pdfDoc.Add(Texto);
                                    }

                                    // Criar pdf
                                    pdfDoc.Add(pdfTable);
                                    pdfDoc.Close();
                                    stream.Close();
                                }

                                Frames.Success.SuccessFrame.LblText.Text = "Dados exportados com sucesso!";

                                ThreadStart SuccessStart = new ThreadStart(() => {
                                    DarkBackground(new Frames.Success());
                                    SuccessForm.Text = "Dados exportados com sucesso!";
                                });

                                await TaskDelay(800);

                                Thread SuccessThread = new Thread(SuccessStart);

                                SuccessThread.SetApartmentState(ApartmentState.STA);

                                SuccessThread.Start();
                            }

                            catch (Exception)
                            {
                                Frames.Erro.ErrorFrame.LblText.Text = "Erro ao exportar dados!";

                                ThreadStart ErrorStart2 = new ThreadStart(() => {
                                    DarkBackground(new Frames.Erro());
                                    ErrorForm.Text = "Erro ao exportar dados!";
                                });

                                Thread ErrorThread2 = new Thread(ErrorStart2);

                                ErrorThread2.SetApartmentState(ApartmentState.STA);

                                ErrorThread2.Start();
                            }
                        }

                        else if (AllColumns.Checked)
                        {
                            try
                            {
                                PdfPTable pdfTable = new PdfPTable(AllDataGrid.Columns.Count - 20);
                                pdfTable.DefaultCell.Padding = 6;
                                pdfTable.WidthPercentage = 101.25F;
                                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                                foreach (DataGridViewColumn column in AllDataGrid.Columns)
                                {
                                    if (column.Index != 25 && column.Index != 26 && column.Index != 30)
                                    {
                                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                        cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                                        cell.Padding = 8;
                                        pdfTable.AddCell(cell);
                                    }
                                }

                                foreach (DataGridViewRow row in AllDataGrid.Rows)
                                {
                                    foreach (DataGridViewCell cell in row.Cells)
                                    {
                                        if (cell.ColumnIndex != 25 && cell.ColumnIndex != 25 && cell.ColumnIndex != 29 && cell.ColumnIndex != 30)
                                            pdfTable.AddCell(cell.Value.ToString());
                                    }
                                }

                                using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                                {
                                    Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);

                                    PdfWriter.GetInstance(pdfDoc, stream);
                                    pdfDoc.Open();

                                    // Logo
                                    var img = Properties.Resources.Logo_250;

                                    iTextSharp.text.Image Logo = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Png);
                                    Logo.ScaleToFit(90, 90);
                                    Logo.Alignment = Element.ALIGN_LEFT;
                                    Logo.SetAbsolutePosition(pdfDoc.LeftMargin + 5, pdfDoc.Top - 85);
                                    pdfDoc.Add(Logo);

                                    // Informaçoes da oficina
                                    var FonteBase = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

                                    var NomeParagraph = new iTextSharp.text.Font(FonteBase, 15, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                                    var Nome = new Paragraph("                           CLÍNICA CAR                                                   " +
                                        "Relatório de serviços", NomeParagraph);
                                    Nome.Alignment = Element.ALIGN_LEFT;
                                    pdfDoc.Add(Nome);

                                    var EnderecoParagraph = new iTextSharp.text.Font(FonteBase, 15, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                                    var Endereco = new Paragraph("                           Rua Lucélia - Colônia Fepasa                                 Data: " + DateTime.Now.ToShortDateString(), EnderecoParagraph);
                                    Endereco.Alignment = Element.ALIGN_LEFT;
                                    pdfDoc.Add(Endereco);

                                    var CidadeParagraph = new iTextSharp.text.Font(FonteBase, 15, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                                    var Cidade = new Paragraph("                           Flórida Paulista - SP                                                   Hora: " + DateTime.Now.ToLongTimeString(), CidadeParagraph);
                                    Cidade.Alignment = Element.ALIGN_LEFT;
                                    pdfDoc.Add(Cidade);

                                    // Linha pra separar
                                    Paragraph Linha = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.25F, 101.25F, BaseColor.LIGHT_GRAY, Element.ALIGN_LEFT, 1)));
                                    pdfDoc.Add(Linha);

                                    // Espaço
                                    pdfDoc.Add(new Chunk("\n"));

                                    // Texto  
                                    var TextoParagraph = new iTextSharp.text.Font(FonteBase, 26, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                                    if (DataFiltered)
                                    {
                                        if (FilterByDays == 1)
                                        {
                                            var Texto = new Paragraph("Tabela geral de Serviços - Hoje\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        if (FilterByDays == 8)
                                        {
                                            var Texto = new Paragraph("Tabela geral de Serviços - Últimos 7 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 31)
                                        {
                                            var Texto = new Paragraph("Tabela ggeral de Serviços - Últimos 30 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 365)
                                        {
                                            var Texto = new Paragraph("Tabela geral de Serviços - Últimos 365 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }
                                    }

                                    else
                                    {
                                        var Texto = new Paragraph("Tabela de Serviços\n\n", TextoParagraph);

                                        Texto.Alignment = Element.ALIGN_CENTER;
                                        pdfDoc.Add(Texto);
                                    }

                                    // Criar pdf
                                    pdfDoc.Add(pdfTable);
                                    pdfDoc.Close();
                                    stream.Close();
                                }

                                Frames.Success.SuccessFrame.LblText.Text = "Dados exportados com sucesso!";

                                ThreadStart SuccessStart = new ThreadStart(() => {
                                    DarkBackground(new Frames.Success());
                                    SuccessForm.Text = "Dados exportados com sucesso!";
                                });

                                await TaskDelay(800);

                                Thread SuccessThread = new Thread(SuccessStart);

                                SuccessThread.SetApartmentState(ApartmentState.STA);

                                SuccessThread.Start();
                            }

                            catch (Exception)
                            {
                                Frames.Erro.ErrorFrame.LblText.Text = "Erro ao exportar dados!";

                                ThreadStart ErrorStart2 = new ThreadStart(() => {
                                    DarkBackground(new Frames.Erro());
                                    ErrorForm.Text = "Erro ao exportar dados!";
                                });

                                Thread ErrorThread2 = new Thread(ErrorStart2);

                                ErrorThread2.SetApartmentState(ApartmentState.STA);

                                ErrorThread2.Start();
                            }
                        }
                    }
                }
            }

            else
            {
                Frames.Erro.ErrorFrame.LblText.Text = "Erro ao exportar dados!";

                ThreadStart ErrorStart3 = new ThreadStart(() => {
                    DarkBackground(new Frames.Erro());
                    ErrorForm.Text = "Erro ao exportar dados!";
                });

                Thread ErrorThread3 = new Thread(ErrorStart3);

                ErrorThread3.SetApartmentState(ApartmentState.STA);

                ErrorThread3.Start();
            }
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Informaçoes dos serviços */

        // Mostrar pagina de informaçoes e valor das informaçoes de cada serviço
        private void ServicosGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        // Mostrar pagina de informaçoes e valor das informaçoes de cada serviço so q com clique duplo
        private void ServicosGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        // Botoes
        private void InformacoesBtn_Click(object sender, EventArgs e)
        {
            MovingBar.Size = new Size(150, 3);
            MovingBar.Location = new Point(10, 189);

            InformacoesBtn.ForeColor = ThemeManager.RedFontColor;
            ValoresBtn.ForeColor = Color.FromArgb(180, 180, 180);
            QuantidadesBtn.ForeColor = Color.FromArgb(180, 180, 180);
            OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);

            InformacoesPanel.Visible = true;
            ValoresPanel.Visible = false;
            QuantidadesPanel.Visible = false;
            OutrosPanel.Visible = false;
        }

        private void ValoresBtn_Click(object sender, EventArgs e)
        {
            MovingBar.Size = new Size(150, 3);
            MovingBar.Location = new Point(190, 189);

            InformacoesBtn.ForeColor = Color.FromArgb(180, 180, 180);
            ValoresBtn.ForeColor = ThemeManager.RedFontColor;
            QuantidadesBtn.ForeColor = Color.FromArgb(180, 180, 180);
            OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);

            InformacoesPanel.Visible = false;
            ValoresPanel.Visible = true;
            QuantidadesPanel.Visible = false;
            OutrosPanel.Visible = false;
        }

        private void QuantidadesBtn_Click(object sender, EventArgs e)
        {
            MovingBar.Size = new Size(110, 3);
            MovingBar.Location = new Point(375, 189);

            InformacoesBtn.ForeColor = Color.FromArgb(180, 180, 180);
            ValoresBtn.ForeColor = Color.FromArgb(180, 180, 180);
            QuantidadesBtn.ForeColor = ThemeManager.RedFontColor;
            OutrosBtn.ForeColor = Color.FromArgb(180, 180, 180);

            InformacoesPanel.Visible = false;
            ValoresPanel.Visible = false;
            QuantidadesPanel.Visible = true;
            OutrosPanel.Visible = false;
        }

        private void ObservacoesBtn_Click(object sender, EventArgs e)
        {
            MovingBar.Size = new Size(110, 3);
            MovingBar.Location = new Point(534, 189);

            InformacoesBtn.ForeColor = Color.FromArgb(180, 180, 180);
            ValoresBtn.ForeColor = Color.FromArgb(180, 180, 180);
            QuantidadesBtn.ForeColor = Color.FromArgb(180, 180, 180);
            OutrosBtn.ForeColor = ThemeManager.RedFontColor;

            InformacoesPanel.Visible = false;
            ValoresPanel.Visible = false;
            QuantidadesPanel.Visible = false;
            OutrosPanel.Visible = true;
        }

        // Editar
        private void Editar_Click(object sender, EventArgs e)
        {
            foreach (Form frm in fc)
            {
                if (frm.Name != "EditServico")
                    EditServicoOpen = false;
                else
                    EditServicoOpen = true;
            }

            if (EditServicoOpen != true)
            {
                EditServicoOpen = true;

                byte[] Foto;

                string Curva, Codigo, Produto, NFabricante, NOriginal, Marca, Grupo, Subgrupo, Tipo, Unidade, Disponivel, Minima,
                    Ideal, Custo, VendaConsumidor, Revenda, VendaOutros, CustoDolar, LucroDolar, LucroPorcento, LucroReais, UltimaVenda,
                    Localizacao, Prateleira, Observacoes, Ean13Text, Code128Text, Fornecedor, Status;

                var StringNumbers = Regex.Match(CodigoInfo.Text, @"\d+").Value;

                Curva = CurvaInfo.Text;
                Codigo = StringNumbers;
                Produto = ProdutoName.Text;
                NFabricante = NumeroFabricanteInfo.Text;
                NOriginal = NumeroOriginalInfo.Text;
                Marca = FabricanteInfo.Text;
                Grupo = GrupoInfo.Text;
                Subgrupo = SubGrupoInfo.Text;
                Unidade = UnidadeInfo.Text;
                Tipo = TipoProdutoInfo.Text;
                Disponivel = DisponivelInfo.Text;
                Minima = MinimoInfo.Text;
                Ideal = IdealInfo.Text;
                Custo = CustoInfo.Text;
                VendaConsumidor = VendaConsumidorInfo.Text;
                Revenda = RevendaInfo.Text;
                VendaOutros = VendaOutrosInfo.Text;
                CustoDolar = CustoDolarInfo.Text;
                LucroDolar = LucroDolarInfo.Text;
                LucroPorcento = LucroPorcentoInfo.Text;
                LucroReais = LucroInfo.Text;
                UltimaVenda = UltimaVendaInfo.Text;
                Localizacao = LocalizacaoInfo.Text;
                Prateleira = PrateleiraInfo.Text;
                Observacoes = ObservacoesInfo.Text;
                Ean13Text = CodigoEAN13.Text;
                Code128Text = CodigoCODE128.Text;
                Fornecedor = FornecedorInfo.Text;
                Status = StatusInfo.Text;

                byte[] ImageToByteArray(System.Drawing.Image imageIn)
                {
                    using (var ms = new MemoryStream())
                    {
                        imageIn.Save(ms, imageIn.RawFormat);
                        return ms.ToArray();
                    }
                }

                Foto = ImageToByteArray(ProductPicture.Image);

                ThreadStart ts = new ThreadStart(() => {
                    DarkBackground(new Frames.Estoque.EditProduto(Curva, Codigo, Produto, NFabricante, NOriginal, Marca, Grupo, Subgrupo, Tipo, Unidade, Disponivel, Minima,
                    Ideal, Custo, VendaConsumidor, Revenda, VendaOutros, CustoDolar, LucroDolar, LucroPorcento, LucroReais, UltimaVenda,
                    Localizacao, Prateleira, Observacoes, Ean13Text, Code128Text, Fornecedor, Status, Foto));
                });

                Thread t = new Thread(ts);

                t.SetApartmentState(ApartmentState.STA);

                t.Start();
            }
        }

        // Excluir
        private void Excluir_Click(object sender, EventArgs e)
        {
            foreach (Form frm in fc)
            {
                if (frm.Name != "DeleteSelected2")
                    DeleteServicoOpen = false;
                else
                    DeleteServicoOpen = true;
            }

            if (DeleteServicoOpen != true)
            {
                DeleteServicoOpen = true;

                ProductNameString = ProductInfoText.Text;

                Frames.DeleteSelected2 DeleteSelectedForm = new Frames.DeleteSelected2("Servico", ProductNameString);

                DeleteSelectedForm.Text = "Excluir serviço";
                Frames.DeleteSelected2.DeleteSelectedFrame.TmplText.Text = "Excluir serviço";
                Frames.DeleteSelected2.DeleteSelectedFrame.LblText.Text = "Você deseja mesmo excluir esse serviço?";

                ThreadStart ts = new ThreadStart(() => {
                    DarkBackground(DeleteSelectedForm);
                    DeleteSelectedForm.ShowInTaskbar = false;
                });

                Thread t = new Thread(ts);

                t.SetApartmentState(ApartmentState.STA);

                t.Start();
            }
        }

        // Fechar pagina de informaçoes
        private void Voltar_Click(object sender, EventArgs e)
        {
            ProductInfo.Location = new Point(ProductInfo.Location.X, 11106);

            Search.Visible = true; FilterBtn.Visible = true; ExportBtn.Visible = true;
            PageItens.Visible = true; MostrarText.Visible = true; NovoServico.Visible = true;
            MoreOptionsBtn.Visible = true; toolStripPaging.Visible = true;

            UpdateGrid();
            RefreshPagination();

            if (IsDarkModeEnabled)
            {
                if (FilterBtn.BorderColor == Color.FromArgb(255, 23, 0))
                    RemoverFiltros.Visible = true;
                else
                    RemoverFiltros.Visible = false;
            }

            else
            {
                if (FilterBtn.BorderColor == Color.FromArgb(255, 3, 0))
                    RemoverFiltros.Visible = true;
                else
                    RemoverFiltros.Visible = false;
            }


            if (Search.Text != "")
                EraseText.Visible = true;
            else
                EraseText.Visible = false;

            InformacoesPanel.Visible = true;
            ValoresPanel.Visible = false;
            QuantidadesPanel.Visible = false;
            OutrosPanel.Visible = false;

            Editar.Location = new Point(Editar.Location.X, 5557);
            Excluir.Location = new Point(Excluir.Location.X, 5557);
            Voltar.Location = new Point(Voltar.Location.X, 5557);
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Classificar itens por ordem alfabetica */

        private async void SortBtn_Click(object sender, EventArgs e)
        {
            if (SortOptions.Visible)
            {
                SortOptions.Visible = false;
                SortOptions.Location = new Point(SortOptions.Location.X - 6, SortOptions.Location.Y);
            }
            else
            {
                if (ViewOptions.Visible)
                {
                    ViewOptions.Visible = false;
                    ViewOptions.Location = new Point(ViewOptions.Location.X - 6, ViewOptions.Location.Y);
                }

                if (AnimateButtonsAndPanels)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        await TaskDelay(10);
                        SortOptions.Location = new Point(SortOptions.Location.X + 1, 94);
                        SortOptions.Visible = true;
                    }
                }
                else
                {
                    SortOptions.Location = new Point(SortOptions.Location.X + 6, 94);
                    SortOptions.Visible = true;
                }
            }
        }

        // Ordem crescente
        private void Crescente_Click(object sender, EventArgs e)
        {
            Crescente.ForeColor = ThemeManager.RedFontColor;
            Crescente.Image = Properties.Resources.ascending_red;

            if (IsDarkModeEnabled)
            {
                Descrescente.ForeColor = Color.FromArgb(250, 250, 250);
                Descrescente.Image = Properties.Resources.descending_branco;
            }

            else
            {
                Descrescente.ForeColor = Color.FromArgb(0, 0, 0);
                Descrescente.Image = Properties.Resources.descending_claro;
            }

            SortedByOrder = "Ascending";
            OrderByColumn(SortedByItem);
            CheckedStateFunction();
        }

        // Ordem decrescente
        private void Descrescente_Click(object sender, EventArgs e)
        {
            if (IsDarkModeEnabled)
            {
                Crescente.ForeColor = Color.FromArgb(250, 250, 250);
                Crescente.Image = Properties.Resources.ascending_branco;
            }

            else
            {
                Crescente.ForeColor = Color.FromArgb(0, 0, 0);
                Crescente.Image = Properties.Resources.ascending_claro;
            }

            Descrescente.ForeColor = ThemeManager.RedFontColor;
            Descrescente.Image = Properties.Resources.descending_red;

            SortedByOrder = "Descending";
            OrderByColumn(SortedByItem);
            CheckedStateFunction();
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Opçoes de visualizaçao do grid */

        private async void ViewOptionsBtn_Click(object sender, EventArgs e)
        {
            if (ViewOptions.Visible)
            {
                ViewOptions.Visible = false;
                ViewOptions.Location = new Point(ViewOptions.Location.X - 6, ViewOptions.Location.Y);
            }
            else
            {
                if (SortOptions.Location == new Point(ViewOptions.Location.X + 6, ViewOptions.Location.Y))
                {
                    SortOptions.Visible = false;
                    SortOptions.Location = new Point(SortOptions.Location.X - 6, SortOptions.Location.Y);
                }

                if (AnimateButtonsAndPanels)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        await TaskDelay(10);
                        ViewOptions.Location = new Point(ViewOptions.Location.X + 1, ViewOptions.Location.Y);
                        ViewOptions.Visible = true;
                    }
                }
                else
                {
                    ViewOptions.Location = new Point(ViewOptions.Location.X + 6, ViewOptions.Location.Y);
                    ViewOptions.Visible = true;
                }
            }
        }

        // Lista normal
        private void ListaNormal_Click(object sender, EventArgs e)
        {
            ListaNormal.ForeColor = ThemeManager.RedFontColor;
            ListaNormal.Image = Properties.Resources.lista_red;

            if (IsDarkModeEnabled)
            {
                ListaCompacta.ForeColor = Color.FromArgb(250, 250, 250);
                ListaCompacta.Image = Properties.Resources.lista_compacta_branco;

                ViewOptionsBtn.Image = Properties.Resources.lista_branco;
            }

            else
            {
                ListaCompacta.ForeColor = Color.FromArgb(0, 0, 0);
                ListaCompacta.Image = Properties.Resources.lista_compacta_claro;

                ViewOptionsBtn.Image = Properties.Resources.lista_claro;
            }

            ServicosGrid.GridColor = ThemeManager.SeparatorAndBorderColor;
            ServicosGrid.RowTemplate.Height = 44;
            servicosBindingSource.DataSource = null;
            ReloadPage();

            Properties.Settings.Default.TipoDeLista = "Normal";

            HideFrames();
        }

        // Lista compacta
        private void ListaCompacta_Click(object sender, EventArgs e)
        {
            if (IsDarkModeEnabled)
            {
                ListaNormal.ForeColor = Color.FromArgb(250, 250, 250);
                ListaNormal.Image = Properties.Resources.lista_branco;

                ViewOptionsBtn.Image = Properties.Resources.lista_compacta_branco;
            }

            else
            {
                ListaNormal.ForeColor = Color.FromArgb(0, 0, 0);
                ListaNormal.Image = Properties.Resources.lista_claro;

                ViewOptionsBtn.Image = Properties.Resources.lista_compacta_claro;
            }

            ListaCompacta.ForeColor = ThemeManager.RedFontColor;
            ListaCompacta.Image = Properties.Resources.lista_compacta_red;

            ServicosGrid.GridColor = ThemeManager.FormBackColor;
            ServicosGrid.RowTemplate.Height = 32;
            servicosBindingSource.DataSource = null;
            ReloadPage();

            Properties.Settings.Default.TipoDeLista = "Compacta";

            HideFrames();
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Opçoes de seleçao de serviços */

        // Exibir o tanto de serviços selecionados e mostrar as opçoes
        private void ServicosGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                AllNames.Clear();

                foreach (DataGridViewRow dgv in ServicosGrid.SelectedRows)
                {
                    if (dgv.Selected)
                        if (ServicosGrid.SelectedRows.Count >= 2)
                        {
                            AllNames.Add(Convert.ToString(dgv.Cells[2].Value));

                            foreach (string month in AllNames)
                            {
                                Console.WriteLine(month);
                            }
                        }
                        else
                            AllNames.Clear();
                }

                if (ServicosGrid.SelectedRows.Count >= 2)
                {
                    SelectedOptions.Visible = true;
                    ProdutosSelecionados.Text = ServicosGrid.SelectedRows.Count.ToString() + " serviços selecionados";

                    if (ServicosGrid.SelectedRows.Count >= 10)
                    {
                        MiniSeparator1.Location = new Point(158, MiniSeparator1.Location.Y);
                        DeleteAllSelected.Location = new Point(175, DeleteAllSelected.Location.Y);
                    }

                    else
                    {
                        MiniSeparator1.Location = new Point(156, MiniSeparator1.Location.Y);
                        DeleteAllSelected.Location = new Point(171, DeleteAllSelected.Location.Y);
                    }
                }

                else
                    SelectedOptions.Visible = false;
            }
        }

        // Excluir todos os serviços selecionados
        private void DeleteAllSelected_Click(object sender, EventArgs e)
        {
            Frames.DeleteAllSelected DeleteAllForm = new Frames.DeleteAllSelected();

            DeleteAllForm.Text = "Excluir serviços";
            Frames.DeleteAllSelected.DeleteAllFrame.TmplText.Text = "Excluir serviços";
            Frames.DeleteAllSelected.DeleteAllFrame.LblText.Text = "Você deseja mesmo excluir esses serviços?";

            ThreadStart ts = new ThreadStart(() => {
                DarkBackground(DeleteAllForm);
            });

            Thread t = new Thread(ts);

            t.SetApartmentState(ApartmentState.STA);

            t.Start();
        }
    }
}
