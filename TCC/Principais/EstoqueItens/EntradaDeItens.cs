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

namespace TCC.Principais
{
    public partial class EntradaDeItens : Form
    {
        FormCollection fc = Application.OpenForms;

        Frames.Success SuccessForm = new Frames.Success();
        Frames.Erro ErrorForm = new Frames.Erro();
        Frames.DeleteSelected2 DeleteSelectedForm = new Frames.DeleteSelected2("EntradaItem", ProductNameString);
        Frames.DeleteAllSelected DeleteAllSelectedForm = new Frames.DeleteAllSelected();

        private int CurrentPage = 0;
        int PagesCount = 0;
        int PageRows = 10;

        public static string ProductNameString;

        int FilteredItens = 0;

        bool NovaEntradaOpen;
        bool DeleteEntradaOpen;

        bool DataFiltered;

        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateButtonsAndPanels = Properties.Settings.Default.AnimarBotoes;
        bool EnableDoubleClickInGrid = Properties.Settings.Default.DoubleClickInGridEnabled;
        bool CanShowProductForm = Properties.Settings.Default.CanShowNewProductForm;

        bool FormLoaded;

        string SortedByItem = "Codigo";
        string SortedByOrder = "Ascending";

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

        public EntradaDeItens()
        {
            InitializeComponent();

            AddControlsToList();
            SetColor();
        }

        private void EntradaDeItens_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'allEntradaItemData.EntradaDeItens'. Você pode movê-la ou removê-la conforme necessário.
            this.entradaDeItensTableAdapter1.Fill(this.allEntradaItemData.EntradaDeItens);
            // TODO: esta linha de código carrega dados na tabela 'entradaItemData.EntradaDeItens'. Você pode movê-la ou removê-la conforme necessário.
            this.entradaDeItensTableAdapter.Fill(this.entradaItemData.EntradaDeItens);

            CodigoEAN13.Text = "123456789012";

            if (IsDarkModeEnabled)
            {
                FilterPesquisaBtn.Image = Properties.Resources.search_branco;
                FilterFornecedoresBtn.Image = Properties.Resources.fornecedor_menor_branco;
                FilterDataBtn.Image = Properties.Resources.filter_data_branco;

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
                FilterFornecedoresBtn.Image = Properties.Resources.fornecedor_preto;
                FilterDataBtn.Image = Properties.Resources.data_preto;

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

                StatusInfo.Image = Properties.Resources.status_cinza;

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

                DisponivelPreset.Image = Properties.Resources.estoque_cinza;
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

            if (entradaItemData.EntradaDeItens.Count == 0)
            {
                EntradaGrid.Visible = false;
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

            if (TodosFornecedores.Checked)
                FornecedoresOptions.Size = new Size(220, 64);
            else
                FornecedoresOptions.Size = new Size(220, 154);

            PagesCount = Convert.ToInt32(Math.Ceiling(entradaItemData.EntradaDeItens.Count * 1.0 / PageRows));
            CurrentPage = 0;
            RefreshPagination();
            ReloadPage();

            foreach (Form frm in fc)
            {
                if (frm.Name == "NovaEntrada")
                {
                    NovaEntrada.Enabled = false;
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

            Properties.Settings.Default.CanUpdateGrid = false;

            FormLoaded = true;
        }

        private void EntradaDeItens_Click(object sender, EventArgs e)
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
            var query = from campos in entradaItemData.EntradaDeItens
                        select new
                        {
                            campos.CODIGO,
                            campos.NUMEROFABRICANTE,
                            campos.PRODUTO,
                            campos.QNTDADQUIRIDA,
                            campos.NUMERONOTAFISCAL,
                            campos.VALORTOTAL,
                            campos.FORNECEDOR,
                            campos.DATA,
                            campos.FUNCIONARIORESPONSAVEL,
                            campos.OBSERVACOES
                        };

            entradaDeItensBindingSource.DataSource = query.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
            OrderByColumn(SortedByItem);
            PagesCount = Convert.ToInt32(Math.Ceiling(entradaItemData.EntradaDeItens.Count * 1.0 / PageRows));
        }

        public void UpdateGrid()
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

            if (SortedByOrder == "Ascending")
            {
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM EntradaDeItens ORDER BY [" + SortedByItem + "] DESC", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    HideNewEntradaItens();

                    DataTable customrows = dt.AsEnumerable()
                    .Skip(PageRows * CurrentPage)
                    .Take(PageRows)
                    .ToList()
                    .CopyToDataTable();

                    // Itens data
                    entradaDeItensBindingSource.DataSource = customrows;
                    entradaDeItensTableAdapter.Fill(entradaItemData.EntradaDeItens);

                    // All itens data
                    entradaDeItensBindingSource1.DataSource = customrows;
                    entradaDeItensTableAdapter1.Fill(allEntradaItemData.EntradaDeItens);

                    OrderByColumn(SortedByItem);
                    PagesCount = Convert.ToInt32(Math.Ceiling(entradaItemData.EntradaDeItens.Count * 1.0 / PageRows));
                    con.Close();
                }
                else
                {
                    EntradaGrid.Visible = false;
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

                    NovaEntrada.Visible = true;
                }
            }

            else if (SortedByOrder == "Descending")
            {
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM EntradaDeItens ORDER BY [" + SortedByItem + "] DESC", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    HideNewEntradaItens();

                    DataTable customrows = dt.AsEnumerable()
                    .Skip(PageRows * CurrentPage)
                    .Take(PageRows)
                    .ToList()
                    .CopyToDataTable();

                    // Itens data
                    entradaDeItensBindingSource.DataSource = customrows;
                    entradaDeItensTableAdapter.Fill(entradaItemData.EntradaDeItens);

                    // All itens data
                    entradaDeItensBindingSource1.DataSource = customrows;
                    entradaDeItensTableAdapter1.Fill(allEntradaItemData.EntradaDeItens);

                    OrderByColumn(SortedByItem);
                    PagesCount = Convert.ToInt32(Math.Ceiling(entradaItemData.EntradaDeItens.Count * 1.0 / PageRows));
                    con.Close();
                }
                else
                {
                    EntradaGrid.Visible = false;
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

                    NovaEntrada.Visible = true;
                }

                OrderByColumn(SortedByItem);
                PagesCount = Convert.ToInt32(Math.Ceiling(entradaItemData.EntradaDeItens.Count * 1.0 / PageRows));
                con.Close();
            }

            RefreshPagination();

            Editar.Location = new Point(Editar.Location.X, 5557);
            Excluir.Location = new Point(Excluir.Location.X, 5557);
            Voltar.Location = new Point(Voltar.Location.X, 5557);

            NovaEntrada.Visible = true;
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
                HideNewEntradaItens();

                DataTable customrows = dt.AsEnumerable()
                .Skip(PageRows * CurrentPage)
                .Take(PageRows)
                .ToList()
                .CopyToDataTable();

                // Itens data
                entradaDeItensBindingSource.DataSource = customrows;
                entradaDeItensTableAdapter.Fill(entradaItemData.EntradaDeItens);

                // All itens data
                entradaDeItensBindingSource1.DataSource = customrows;
                entradaDeItensTableAdapter1.Fill(allEntradaItemData.EntradaDeItens);

                PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                con.Close();

                EntradaGrid.Visible = true;
                Separator2.Visible = true;
                toolStripPaging.Visible = true;

                NotFind.Visible = false;

                NotFindDesc.Visible = false;
            }
            else
            {
                NotFindDesc.Text = "Nenhum resultado que corresponda \n à sua pesquisa atual foi encontrado.";

                EntradaGrid.Visible = false;
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
                ColorFilter(ActiveFilter3);
            else
            {
                if (All.Checked != true)
                    RemoverFiltros.Visible = false;
            }

            OrderByColumn(SortedByItem);
            HideFrames();
        }

        // Filtrar por data especifica
        private void SpecificDateFunction(DateTime data1, DateTime data2)
        {
            var query = from campos in entradaItemData.EntradaDeItens
                        where campos.DATA >= data1 && campos.DATA <= data2
                        orderby campos.DATA <= campos.DATA

                        select campos;

            FilteredItens = query.Count();

            entradaDeItensBindingSource.DataSource = query.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
            entradaDeItensBindingSource1.DataSource = query;
            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));

            RefreshPagination();
            NotFinded();
        }

        // Filtrar data
        private void DateFunction(DateTime data)
        {
            var query = from campos in entradaItemData.EntradaDeItens
                        where campos.DATA >= data
                        orderby campos.DATA <= campos.DATA

                        select campos;

            FilteredItens = query.Count();

            entradaDeItensBindingSource.DataSource = query.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
            entradaDeItensBindingSource1.DataSource = query;
            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));

            RefreshPagination();
            NotFinded();
        }

        /*--------------------------*/

        // Selecionar item pra classificar
        private void SortItens(object sender, EventArgs e)
        {
            if (CodigoSort.Checked)
            {
                OrderByColumn("Codigo");
                SortedByItem = "Codigo";
            }

            else if (ProdutoSort.Checked)
            {
                OrderByColumn("Produto");
                SortedByItem = "Produto";
            }

            else if (NumeroFabricanteSort.Checked)
            {
                OrderByColumn("NumeroFabricante");
                SortedByItem = "NumeroFabricante";
            }

            else if (AdquiridaSort.Checked)
            {
                OrderByColumn("Adquirida");
                SortedByItem = "Adquirida";
            }

            else if (NotaFiscalSort.Checked)
            {
                OrderByColumn("NotaFiscal");
                SortedByItem = "NotaFiscal";
            }

            else if (ValorSort.Checked)
            {
                OrderByColumn("ValorTotal");
                SortedByItem = "ValorTotal";
            }

            else if (ResponsavelSort.Checked)
            {
                OrderByColumn("Responsavel");
                SortedByItem = "Responsavel";
            }

            else if (DataSort.Checked)
            {
                OrderByColumn("Data");
                SortedByItem = "Data";
            }

            else if (FornecedorSort.Checked)
            {
                OrderByColumn("Fornecedor");
                SortedByItem = "Fornecedor";
            }

            CheckedStateFunction();
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
                        case "Codigo":
                            var CodigoQuery = from campos in entradaItemData.EntradaDeItens
                                          orderby campos.CODIGO descending
                                          where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                          select campos;

                            entradaDeItensBindingSource.DataSource = CodigoQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = CodigoQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "NumeroFabricante":
                            var FabricanteQuery = from campos in entradaItemData.EntradaDeItens
                                                orderby campos.NUMEROFABRICANTE descending
                                                where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                select campos;

                            entradaDeItensBindingSource.DataSource = FabricanteQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = FabricanteQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Produto":
                            var ProdutoQuery = from campos in entradaItemData.EntradaDeItens
                                            orderby campos.PRODUTO descending
                                            where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                            select campos;

                            entradaDeItensBindingSource.DataSource = ProdutoQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = ProdutoQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Adquirida":
                            var AdquiridaQuery = from campos in entradaItemData.EntradaDeItens
                                           orderby campos.QNTDADQUIRIDA descending
                                           where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                           select campos;

                            entradaDeItensBindingSource.DataSource = AdquiridaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = AdquiridaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "NotaFiscal":
                            var NotaQuery = from campos in entradaItemData.EntradaDeItens
                                              orderby campos.NUMERONOTAFISCAL descending
                                              where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                              select campos;

                            entradaDeItensBindingSource.DataSource = NotaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = NotaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "ValorTotal":
                            var ValorQuery = from campos in entradaItemData.EntradaDeItens
                                                orderby campos.VALORTOTAL descending
                                                where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                select campos;

                            entradaDeItensBindingSource.DataSource = ValorQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = ValorQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Fornecedor":
                            var FornecedorQuery = from campos in entradaItemData.EntradaDeItens
                                                   orderby campos.FORNECEDOR descending
                                                   where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                   select campos;

                            entradaDeItensBindingSource.DataSource = FornecedorQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = FornecedorQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Responsavel":
                            var ResponsavelQuery = from campos in entradaItemData.EntradaDeItens
                                               orderby campos.FUNCIONARIORESPONSAVEL descending
                                               where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                               select campos;

                            entradaDeItensBindingSource.DataSource = ResponsavelQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = ResponsavelQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Observacoes":
                            var ObservacoesQuery = from campos in entradaItemData.EntradaDeItens
                                               orderby campos.OBSERVACOES descending
                                               where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                               select campos;

                            entradaDeItensBindingSource.DataSource = ObservacoesQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = ObservacoesQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Entrada":
                            var EntradaQuery = from campos in entradaItemData.EntradaDeItens
                                               orderby campos.DATA >= campos.DATA
                                               where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                               select campos;

                            entradaDeItensBindingSource.DataSource = EntradaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = EntradaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;
                    }
                }

                else if (SortedByOrder == "Ascending")
                {
                    switch (Campo)
                    {
                        case "Codigo":
                            var CodigoQuery = from campos in entradaItemData.EntradaDeItens
                                              orderby campos.CODIGO ascending
                                              where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                              select campos;

                            entradaDeItensBindingSource.DataSource = CodigoQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = CodigoQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "NumeroFabricante":
                            var FabricanteQuery = from campos in entradaItemData.EntradaDeItens
                                                  orderby campos.NUMEROFABRICANTE ascending
                                                  where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                  select campos;

                            entradaDeItensBindingSource.DataSource = FabricanteQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = FabricanteQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Produto":
                            var ProdutoQuery = from campos in entradaItemData.EntradaDeItens
                                               orderby campos.PRODUTO ascending
                                               where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                               select campos;

                            entradaDeItensBindingSource.DataSource = ProdutoQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = ProdutoQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Adquirida":
                            var AdquiridaQuery = from campos in entradaItemData.EntradaDeItens
                                                 orderby campos.QNTDADQUIRIDA ascending
                                                 where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                 select campos;

                            entradaDeItensBindingSource.DataSource = AdquiridaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = AdquiridaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "NotaFiscal":
                            var NotaQuery = from campos in entradaItemData.EntradaDeItens
                                            orderby campos.NUMERONOTAFISCAL ascending
                                            where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                            select campos;

                            entradaDeItensBindingSource.DataSource = NotaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = NotaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "ValorTotal":
                            var ValorQuery = from campos in entradaItemData.EntradaDeItens
                                             orderby campos.VALORTOTAL ascending
                                             where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                             select campos;

                            entradaDeItensBindingSource.DataSource = ValorQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = ValorQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Fornecedor":
                            var FornecedorQuery = from campos in entradaItemData.EntradaDeItens
                                                  orderby campos.FORNECEDOR ascending
                                                  where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                  select campos;

                            entradaDeItensBindingSource.DataSource = FornecedorQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = FornecedorQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Responsavel":
                            var ResponsavelQuery = from campos in entradaItemData.EntradaDeItens
                                                   orderby campos.FUNCIONARIORESPONSAVEL ascending
                                                   where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                   select campos;

                            entradaDeItensBindingSource.DataSource = ResponsavelQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = ResponsavelQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Observacoes":
                            var ObservacoesQuery = from campos in entradaItemData.EntradaDeItens
                                                   orderby campos.OBSERVACOES ascending
                                                   where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                   select campos;

                            entradaDeItensBindingSource.DataSource = ObservacoesQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = ObservacoesQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Entrada":
                            var EntradaQuery = from campos in entradaItemData.EntradaDeItens
                                               orderby campos.DATA <= campos.DATA
                                               where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                               select campos;

                            entradaDeItensBindingSource.DataSource = EntradaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = EntradaQuery;
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
                        case "Codigo":
                            var CodigoQuery = from campos in entradaItemData.EntradaDeItens
                                              orderby campos.CODIGO descending

                                              select campos;

                            entradaDeItensBindingSource.DataSource = CodigoQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = CodigoQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "NumeroFabricante":
                            var FabricanteQuery = from campos in entradaItemData.EntradaDeItens
                                                  orderby campos.NUMEROFABRICANTE descending

                                                  select campos;

                            entradaDeItensBindingSource.DataSource = FabricanteQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = FabricanteQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Produto":
                            var ProdutoQuery = from campos in entradaItemData.EntradaDeItens
                                               orderby campos.PRODUTO descending

                                               select campos;

                            entradaDeItensBindingSource.DataSource = ProdutoQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = ProdutoQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Adquirida":
                            var AdquiridaQuery = from campos in entradaItemData.EntradaDeItens
                                                 orderby campos.QNTDADQUIRIDA descending

                                                 select campos;

                            entradaDeItensBindingSource.DataSource = AdquiridaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = AdquiridaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "NotaFiscal":
                            var NotaQuery = from campos in entradaItemData.EntradaDeItens
                                            orderby campos.NUMERONOTAFISCAL descending

                                            select campos;

                            entradaDeItensBindingSource.DataSource = NotaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = NotaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "ValorTotal":
                            var ValorQuery = from campos in entradaItemData.EntradaDeItens
                                             orderby campos.VALORTOTAL descending

                                             select campos;

                            entradaDeItensBindingSource.DataSource = ValorQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = ValorQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Fornecedor":
                            var FornecedorQuery = from campos in entradaItemData.EntradaDeItens
                                                  orderby campos.FORNECEDOR descending

                                                  select campos;

                            entradaDeItensBindingSource.DataSource = FornecedorQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = FornecedorQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Responsavel":
                            var ResponsavelQuery = from campos in entradaItemData.EntradaDeItens
                                                   orderby campos.FUNCIONARIORESPONSAVEL descending

                                                   select campos;

                            entradaDeItensBindingSource.DataSource = ResponsavelQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = ResponsavelQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Observacoes":
                            var ObservacoesQuery = from campos in entradaItemData.EntradaDeItens
                                                   orderby campos.OBSERVACOES descending

                                                   select campos;

                            entradaDeItensBindingSource.DataSource = ObservacoesQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = ObservacoesQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Entrada":
                            var EntradaQuery = from campos in entradaItemData.EntradaDeItens
                                               orderby campos.DATA >= campos.DATA
                                               where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                               select campos;

                            entradaDeItensBindingSource.DataSource = EntradaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = EntradaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;
                    }
                }
                else if (SortedByOrder == "Ascending")
                {
                    switch (Campo)
                    {
                        case "Codigo":
                            var CodigoQuery = from campos in entradaItemData.EntradaDeItens
                                              orderby campos.CODIGO ascending

                                              select campos;

                            entradaDeItensBindingSource.DataSource = CodigoQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = CodigoQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "NumeroFabricante":
                            var FabricanteQuery = from campos in entradaItemData.EntradaDeItens
                                                  orderby campos.NUMEROFABRICANTE ascending

                                                  select campos;

                            entradaDeItensBindingSource.DataSource = FabricanteQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = FabricanteQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Produto":
                            var ProdutoQuery = from campos in entradaItemData.EntradaDeItens
                                               orderby campos.PRODUTO ascending

                                               select campos;

                            entradaDeItensBindingSource.DataSource = ProdutoQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = ProdutoQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Adquirida":
                            var AdquiridaQuery = from campos in entradaItemData.EntradaDeItens
                                                 orderby campos.QNTDADQUIRIDA ascending

                                                 select campos;

                            entradaDeItensBindingSource.DataSource = AdquiridaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = AdquiridaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "NotaFiscal":
                            var NotaQuery = from campos in entradaItemData.EntradaDeItens
                                            orderby campos.NUMERONOTAFISCAL ascending

                                            select campos;

                            entradaDeItensBindingSource.DataSource = NotaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = NotaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "ValorTotal":
                            var ValorQuery = from campos in entradaItemData.EntradaDeItens
                                             orderby campos.VALORTOTAL ascending

                                             select campos;

                            entradaDeItensBindingSource.DataSource = ValorQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = ValorQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Fornecedor":
                            var FornecedorQuery = from campos in entradaItemData.EntradaDeItens
                                                  orderby campos.FORNECEDOR ascending

                                                  select campos;

                            entradaDeItensBindingSource.DataSource = FornecedorQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = FornecedorQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Responsavel":
                            var ResponsavelQuery = from campos in entradaItemData.EntradaDeItens
                                                   orderby campos.FUNCIONARIORESPONSAVEL ascending

                                                   select campos;

                            entradaDeItensBindingSource.DataSource = ResponsavelQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = ResponsavelQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Observacoes":
                            var ObservacoesQuery = from campos in entradaItemData.EntradaDeItens
                                                   orderby campos.OBSERVACOES ascending

                                                   select campos;

                            entradaDeItensBindingSource.DataSource = ObservacoesQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = ObservacoesQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Entrada":
                            var EntradaQuery = from campos in entradaItemData.EntradaDeItens
                                               orderby campos.DATA <= campos.DATA
                                               where campos.DATA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.DATA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                               select campos;

                            entradaDeItensBindingSource.DataSource = EntradaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            entradaDeItensBindingSource1.DataSource = EntradaQuery;
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
            if (SortedByOrder == "Ascending")
            {
                if (All.Checked)
                {
                    foreach (DataGridViewColumn column in EntradaGrid.Columns)
                    {
                        if (CodigoSort.Checked)
                        {
                            EntradaGrid.Columns[0].HeaderText = "CÓDIGO ↑";
                            EntradaGrid.Columns[0].ToolTipText = "Classificado: menor para o maior";
                        }
                        else if (ProdutoSort.Checked)
                        {
                            EntradaGrid.Columns[1].HeaderText = "PRODUTO ↑";
                            EntradaGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                        }
                        else if (NumeroFabricanteSort.Checked)
                        {
                            EntradaGrid.Columns[2].HeaderText = "Nº FABRICANTE ↑";
                            EntradaGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                        }
                        else if (AdquiridaSort.Checked)
                        {
                            EntradaGrid.Columns[3].HeaderText = "QNTD. ADQUIRIDA ↑";
                            EntradaGrid.Columns[3].ToolTipText = "Classificado: Menor para a maior";
                        }
                        else if (NotaFiscalSort.Checked)
                        {
                            EntradaGrid.Columns[4].HeaderText = "Nº NOTA FISCAL ↑";
                            EntradaGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                        }
                        else if (ValorSort.Checked)
                        {
                            EntradaGrid.Columns[5].HeaderText = "VALOR (R$) ↑";
                            EntradaGrid.Columns[5].ToolTipText = "Classificado: Menor para o maior";
                        }
                        else if (ResponsavelSort.Checked)
                        {
                            EntradaGrid.Columns[6].HeaderText = "RESPONSÁVEL ↑";
                            EntradaGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                        }
                        else if (FornecedorSort.Checked)
                        {
                            EntradaGrid.Columns[7].HeaderText = "FORNECEDOR ↑";
                            EntradaGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                        }
                        else if (DataSort.Checked)
                        {
                            EntradaGrid.Columns[8].HeaderText = "DATA ↑";
                            EntradaGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                        }
                    }
                }

                else if (Codigo.Checked)
                {
                    if (CodigoSort.Checked)
                    {
                        EntradaGrid.Columns[0].HeaderText = "CÓDIGO* ↑";
                        EntradaGrid.Columns[0].HeaderCell.Style.ForeColor = Color.FromArgb(255, 3, 0);
                        EntradaGrid.Columns[0].ToolTipText = "Classificado: menor para o maior";
                    }
                    else
                    {
                        EntradaGrid.Columns[0].HeaderText = "CÓDIGO*";
                        EntradaGrid.Columns[0].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Produto.Checked)
                {
                    if (ProdutoSort.Checked)
                    {
                        EntradaGrid.Columns[1].HeaderText = "PRODUTO* ↑";
                        EntradaGrid.Columns[1].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        EntradaGrid.Columns[1].HeaderText = "PRODUTO*";
                        EntradaGrid.Columns[1].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (NumeroFabricante.Checked)
                {
                    if (NumeroFabricanteSort.Checked)
                    {
                        EntradaGrid.Columns[2].HeaderText = "Nº FABRICANTE* ↑";
                        EntradaGrid.Columns[2].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        EntradaGrid.Columns[2].HeaderText = "Nº FABRICANTE*";
                        EntradaGrid.Columns[2].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Adquirida.Checked)
                {
                    if (AdquiridaSort.Checked)
                    {
                        EntradaGrid.Columns[3].HeaderText = "QNTD. ADQUIRIDA* ↑";
                        EntradaGrid.Columns[3].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[3].ToolTipText = "Classificado: Menor para a maior";
                    }
                    else
                    {
                        EntradaGrid.Columns[3].HeaderText = "QNTD. ADQUIRIDA*";
                        EntradaGrid.Columns[3].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (NotaFiscal.Checked)
                {
                    if (NotaFiscalSort.Checked)
                    {
                        EntradaGrid.Columns[4].HeaderText = "Nº NOTA FISCAL* ↑";
                        EntradaGrid.Columns[4].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        EntradaGrid.Columns[4].HeaderText = "Nº NOTA FISCAL*";
                        EntradaGrid.Columns[4].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Valor.Checked)
                {
                    if (ValorSort.Checked)
                    {
                        EntradaGrid.Columns[5].HeaderText = "VALOR (R$)* ↑";
                        EntradaGrid.Columns[5].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[5].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else
                    {
                        EntradaGrid.Columns[5].HeaderText = "VALOR (R$)*";
                        EntradaGrid.Columns[5].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Fornecedor.Checked)
                {
                    if (FornecedorSort.Checked)
                    {
                        EntradaGrid.Columns[6].HeaderText = "FORNECEDOR* ↑";
                        EntradaGrid.Columns[6].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        EntradaGrid.Columns[6].HeaderText = "FORNECEDOR*";
                        EntradaGrid.Columns[6].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Responsavel.Checked)
                {
                    if (ResponsavelSort.Checked)
                    {
                        EntradaGrid.Columns[7].HeaderText = "RESPONSÁVEL* ↑";
                        EntradaGrid.Columns[7].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        EntradaGrid.Columns[7].HeaderText = "RESPONSÁVEL*";
                        EntradaGrid.Columns[7].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Data.Checked)
                {
                    if (DataSort.Checked)
                    {
                        EntradaGrid.Columns[8].HeaderText = "DATA* ↑";
                        EntradaGrid.Columns[8].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                    else
                    {
                        EntradaGrid.Columns[8].HeaderText = "DATA*";
                        EntradaGrid.Columns[8].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }
            }

            if (SortedByOrder == "Descending")
            {
                if (All.Checked)
                {
                    foreach (DataGridViewColumn column in EntradaGrid.Columns)
                    {
                        if (IsDarkModeEnabled != true)
                            column.HeaderCell.Style.ForeColor = Color.FromArgb(80, 80, 80);
                        else
                            column.HeaderCell.Style.ForeColor = Color.FromArgb(255, 255, 255);

                        if (CodigoSort.Checked)
                        {
                            EntradaGrid.Columns[0].HeaderText = "CÓDIGO ↓";
                            EntradaGrid.Columns[0].ToolTipText = "Classificado: maior para o menor";
                        }
                        else if (ProdutoSort.Checked)
                        {
                            EntradaGrid.Columns[1].HeaderText = "PRODUTO ↓";
                            EntradaGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                        }
                        else if (NumeroFabricanteSort.Checked)
                        {
                            EntradaGrid.Columns[2].HeaderText = "Nº FABRICANTE ↓";
                            EntradaGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                        }
                        else if (AdquiridaSort.Checked)
                        {
                            EntradaGrid.Columns[3].HeaderText = "QNTD. ADQUIRIDA ↓";
                            EntradaGrid.Columns[3].ToolTipText = "Classificado: Maior para a menor";
                        }
                        else if (NotaFiscalSort.Checked)
                        {
                            EntradaGrid.Columns[4].HeaderText = "Nº NOTA FISCAL ↓";
                            EntradaGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                        }
                        else if (ValorSort.Checked)
                        {
                            EntradaGrid.Columns[5].HeaderText = "VALOR (R$) ↓";
                            EntradaGrid.Columns[5].ToolTipText = "Classificado: Maior para o menor";
                        }
                        else if (FornecedorSort.Checked)
                        {
                            EntradaGrid.Columns[6].HeaderText = "FORNECEDOR ↓";
                            EntradaGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                        }
                        else if (ResponsavelSort.Checked)
                        {
                            EntradaGrid.Columns[7].HeaderText = "RESPONSÁVEL ↓";
                            EntradaGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                        }
                        else if (DataSort.Checked)
                        {
                            EntradaGrid.Columns[8].HeaderText = "DATA ↓";
                            EntradaGrid.Columns[8].ToolTipText = "Classificado: Mais nova para a mais antiga";
                        }
                    }
                }

                else if (Codigo.Checked)
                {
                    if (CodigoSort.Checked)
                    {
                        EntradaGrid.Columns[0].HeaderText = "CÓDIGO* ↓";
                        EntradaGrid.Columns[0].HeaderCell.Style.ForeColor = Color.FromArgb(255, 3, 0);
                        EntradaGrid.Columns[0].ToolTipText = "Classificado: menor para o maior";
                    }
                    else
                    {
                        EntradaGrid.Columns[0].HeaderText = "CÓDIGO*";
                        EntradaGrid.Columns[0].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Produto.Checked)
                {
                    if (ProdutoSort.Checked)
                    {
                        EntradaGrid.Columns[1].HeaderText = "PRODUTO* ↓";
                        EntradaGrid.Columns[1].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        EntradaGrid.Columns[1].HeaderText = "PRODUTO*";
                        EntradaGrid.Columns[1].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (NumeroFabricante.Checked)
                {
                    if (NumeroFabricanteSort.Checked)
                    {
                        EntradaGrid.Columns[2].HeaderText = "Nº FABRICANTE* ↓";
                        EntradaGrid.Columns[2].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        EntradaGrid.Columns[2].HeaderText = "Nº FABRICANTE*";
                        EntradaGrid.Columns[2].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Adquirida.Checked)
                {
                    if (AdquiridaSort.Checked)
                    {
                        EntradaGrid.Columns[3].HeaderText = "QNTD. ADQUIRIDA* ↓";
                        EntradaGrid.Columns[3].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[3].ToolTipText = "Classificado: Maior para a menor";
                    }
                    else
                    {
                        EntradaGrid.Columns[3].HeaderText = "QNTD. ADQUIRIDA*";
                        EntradaGrid.Columns[3].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (NotaFiscal.Checked)
                {
                    if (NotaFiscalSort.Checked)
                    {
                        EntradaGrid.Columns[4].HeaderText = "Nº NOTA FISCAL* ↓";
                        EntradaGrid.Columns[4].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        EntradaGrid.Columns[4].HeaderText = "Nº NOTA FISCAL*";
                        EntradaGrid.Columns[4].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Valor.Checked)
                {
                    if (ValorSort.Checked)
                    {
                        EntradaGrid.Columns[5].HeaderText = "VALOR (R$)* ↓";
                        EntradaGrid.Columns[5].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[5].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else
                    {
                        EntradaGrid.Columns[5].HeaderText = "VALOR (R$)*";
                        EntradaGrid.Columns[5].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Fornecedor.Checked)
                {
                    if (FornecedorSort.Checked)
                    {
                        EntradaGrid.Columns[6].HeaderText = "FORNECEDOR* ↓";
                        EntradaGrid.Columns[6].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        EntradaGrid.Columns[6].HeaderText = "FORNECEDOR*";
                        EntradaGrid.Columns[6].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Responsavel.Checked)
                {
                    if (ResponsavelSort.Checked)
                    {
                        EntradaGrid.Columns[7].HeaderText = "RESPONSÁVEL* ↓";
                        EntradaGrid.Columns[7].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        EntradaGrid.Columns[7].HeaderText = "RESPONSÁVEL*";
                        EntradaGrid.Columns[7].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Data.Checked)
                {
                    if (DataSort.Checked)
                    {
                        EntradaGrid.Columns[8].HeaderText = "DATA* ↓";
                        EntradaGrid.Columns[8].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[8].ToolTipText = "Classificado: Mais nova para a mais antiga";
                    }
                    else
                    {
                        EntradaGrid.Columns[8].HeaderText = "DATA*";
                        EntradaGrid.Columns[8].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }
            }

            if (FormLoaded)
                HideFrames();
        }

        private void CheckedStateFunction()
        {
            if (SortedByOrder == "Ascending")
            {
                if (All.Checked)
                {
                    foreach (DataGridViewColumn column in EntradaGrid.Columns)
                    {
                        if (CodigoSort.Checked)
                        {
                            EntradaGrid.Columns[0].HeaderText = "CÓDIGO ↑";
                            EntradaGrid.Columns[0].ToolTipText = "Classificado: menor para o maior";
                        }
                        else if (ProdutoSort.Checked)
                        {
                            EntradaGrid.Columns[1].HeaderText = "PRODUTO ↑";
                            EntradaGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                        }
                        else if (NumeroFabricanteSort.Checked)
                        {
                            EntradaGrid.Columns[2].HeaderText = "Nº FABRICANTE ↑";
                            EntradaGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                        }
                        else if (AdquiridaSort.Checked)
                        {
                            EntradaGrid.Columns[3].HeaderText = "QNTD. ADQUIRIDA ↑";
                            EntradaGrid.Columns[3].ToolTipText = "Classificado: Menor para a maior";
                        }
                        else if (NotaFiscalSort.Checked)
                        {
                            EntradaGrid.Columns[4].HeaderText = "Nº NOTA FISCAL ↑";
                            EntradaGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                        }
                        else if (ValorSort.Checked)
                        {
                            EntradaGrid.Columns[5].HeaderText = "VALOR (R$) ↑";
                            EntradaGrid.Columns[5].ToolTipText = "Classificado: Menor para o maior";
                        }
                        else if (ResponsavelSort.Checked)
                        {
                            EntradaGrid.Columns[6].HeaderText = "RESPONSÁVEL ↑";
                            EntradaGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                        }
                        else if (FornecedorSort.Checked)
                        {
                            EntradaGrid.Columns[7].HeaderText = "FORNECEDOR ↑";
                            EntradaGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                        }
                        else if (DataSort.Checked)
                        {
                            EntradaGrid.Columns[8].HeaderText = "DATA ↑";
                            EntradaGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                        }
                    }
                }

                else if (Codigo.Checked)
                {
                    if (CodigoSort.Checked)
                    {
                        EntradaGrid.Columns[0].HeaderText = "CÓDIGO* ↑";
                        EntradaGrid.Columns[0].HeaderCell.Style.ForeColor = Color.FromArgb(255, 3, 0);
                        EntradaGrid.Columns[0].ToolTipText = "Classificado: menor para o maior";
                    }
                    else
                    {
                        EntradaGrid.Columns[0].HeaderText = "CÓDIGO*";
                        EntradaGrid.Columns[0].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Produto.Checked)
                {
                    if (ProdutoSort.Checked)
                    {
                        EntradaGrid.Columns[1].HeaderText = "PRODUTO* ↑";
                        EntradaGrid.Columns[1].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        EntradaGrid.Columns[1].HeaderText = "PRODUTO*";
                        EntradaGrid.Columns[1].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (NumeroFabricante.Checked)
                {
                    if (NumeroFabricanteSort.Checked)
                    {
                        EntradaGrid.Columns[2].HeaderText = "Nº FABRICANTE* ↑";
                        EntradaGrid.Columns[2].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        EntradaGrid.Columns[2].HeaderText = "Nº FABRICANTE*";
                        EntradaGrid.Columns[2].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Adquirida.Checked)
                {
                    if (AdquiridaSort.Checked)
                    {
                        EntradaGrid.Columns[3].HeaderText = "QNTD. ADQUIRIDA* ↑";
                        EntradaGrid.Columns[3].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[3].ToolTipText = "Classificado: Menor para a maior";
                    }
                    else
                    {
                        EntradaGrid.Columns[3].HeaderText = "QNTD. ADQUIRIDA*";
                        EntradaGrid.Columns[3].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (NotaFiscal.Checked)
                {
                    if (NotaFiscalSort.Checked)
                    {
                        EntradaGrid.Columns[4].HeaderText = "Nº NOTA FISCAL* ↑";
                        EntradaGrid.Columns[4].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        EntradaGrid.Columns[4].HeaderText = "Nº NOTA FISCAL*";
                        EntradaGrid.Columns[4].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Valor.Checked)
                {
                    if (ValorSort.Checked)
                    {
                        EntradaGrid.Columns[5].HeaderText = "VALOR (R$)* ↑";
                        EntradaGrid.Columns[5].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[5].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else
                    {
                        EntradaGrid.Columns[5].HeaderText = "VALOR (R$)*";
                        EntradaGrid.Columns[5].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Fornecedor.Checked)
                {
                    if (FornecedorSort.Checked)
                    {
                        EntradaGrid.Columns[6].HeaderText = "FORNECEDOR* ↑";
                        EntradaGrid.Columns[6].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        EntradaGrid.Columns[6].HeaderText = "FORNECEDOR*";
                        EntradaGrid.Columns[6].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Responsavel.Checked)
                {
                    if (ResponsavelSort.Checked)
                    {
                        EntradaGrid.Columns[7].HeaderText = "RESPONSÁVEL* ↑";
                        EntradaGrid.Columns[7].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        EntradaGrid.Columns[7].HeaderText = "RESPONSÁVEL*";
                        EntradaGrid.Columns[7].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Data.Checked)
                {
                    if (DataSort.Checked)
                    {
                        EntradaGrid.Columns[8].HeaderText = "DATA* ↑";
                        EntradaGrid.Columns[8].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                    else
                    {
                        EntradaGrid.Columns[8].HeaderText = "DATA*";
                        EntradaGrid.Columns[8].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }
            }

            if (SortedByOrder == "Descending")
            {
                if (All.Checked)
                {
                    foreach (DataGridViewColumn column in EntradaGrid.Columns)
                    {
                        if (IsDarkModeEnabled != true)
                            column.HeaderCell.Style.ForeColor = Color.FromArgb(80, 80, 80);
                        else
                            column.HeaderCell.Style.ForeColor = Color.FromArgb(255, 255, 255);

                        if (CodigoSort.Checked)
                        {
                            EntradaGrid.Columns[0].HeaderText = "CÓDIGO ↓";
                            EntradaGrid.Columns[0].ToolTipText = "Classificado: maior para o menor";
                        }
                        else if (ProdutoSort.Checked)
                        {
                            EntradaGrid.Columns[1].HeaderText = "PRODUTO ↓";
                            EntradaGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                        }
                        else if (NumeroFabricanteSort.Checked)
                        {
                            EntradaGrid.Columns[2].HeaderText = "Nº FABRICANTE ↓";
                            EntradaGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                        }
                        else if (AdquiridaSort.Checked)
                        {
                            EntradaGrid.Columns[3].HeaderText = "QNTD. ADQUIRIDA ↓";
                            EntradaGrid.Columns[3].ToolTipText = "Classificado: Maior para a menor";
                        }
                        else if (NotaFiscalSort.Checked)
                        {
                            EntradaGrid.Columns[4].HeaderText = "Nº NOTA FISCAL ↓";
                            EntradaGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                        }
                        else if (ValorSort.Checked)
                        {
                            EntradaGrid.Columns[5].HeaderText = "VALOR (R$) ↓";
                            EntradaGrid.Columns[5].ToolTipText = "Classificado: Maior para o menor";
                        }
                        else if (FornecedorSort.Checked)
                        {
                            EntradaGrid.Columns[6].HeaderText = "FORNECEDOR ↓";
                            EntradaGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                        }
                        else if (ResponsavelSort.Checked)
                        {
                            EntradaGrid.Columns[7].HeaderText = "RESPONSÁVEL ↓";
                            EntradaGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                        }
                        else if (DataSort.Checked)
                        {
                            EntradaGrid.Columns[8].HeaderText = "DATA ↓";
                            EntradaGrid.Columns[8].ToolTipText = "Classificado: Mais nova para a mais antiga";
                        }
                    }
                }

                else if (Codigo.Checked)
                {
                    if (CodigoSort.Checked)
                    {
                        EntradaGrid.Columns[0].HeaderText = "CÓDIGO* ↓";
                        EntradaGrid.Columns[0].HeaderCell.Style.ForeColor = Color.FromArgb(255, 3, 0);
                        EntradaGrid.Columns[0].ToolTipText = "Classificado: menor para o maior";
                    }
                    else
                    {
                        EntradaGrid.Columns[0].HeaderText = "CÓDIGO*";
                        EntradaGrid.Columns[0].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Produto.Checked)
                {
                    if (ProdutoSort.Checked)
                    {
                        EntradaGrid.Columns[1].HeaderText = "PRODUTO* ↓";
                        EntradaGrid.Columns[1].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        EntradaGrid.Columns[1].HeaderText = "PRODUTO*";
                        EntradaGrid.Columns[1].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (NumeroFabricante.Checked)
                {
                    if (NumeroFabricanteSort.Checked)
                    {
                        EntradaGrid.Columns[2].HeaderText = "Nº FABRICANTE* ↓";
                        EntradaGrid.Columns[2].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        EntradaGrid.Columns[2].HeaderText = "Nº FABRICANTE*";
                        EntradaGrid.Columns[2].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Adquirida.Checked)
                {
                    if (AdquiridaSort.Checked)
                    {
                        EntradaGrid.Columns[3].HeaderText = "QNTD. ADQUIRIDA* ↓";
                        EntradaGrid.Columns[3].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[3].ToolTipText = "Classificado: Maior para a menor";
                    }
                    else
                    {
                        EntradaGrid.Columns[3].HeaderText = "QNTD. ADQUIRIDA*";
                        EntradaGrid.Columns[3].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (NotaFiscal.Checked)
                {
                    if (NotaFiscalSort.Checked)
                    {
                        EntradaGrid.Columns[4].HeaderText = "Nº NOTA FISCAL* ↓";
                        EntradaGrid.Columns[4].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        EntradaGrid.Columns[4].HeaderText = "Nº NOTA FISCAL*";
                        EntradaGrid.Columns[4].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Valor.Checked)
                {
                    if (ValorSort.Checked)
                    {
                        EntradaGrid.Columns[5].HeaderText = "VALOR (R$)* ↓";
                        EntradaGrid.Columns[5].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[5].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else
                    {
                        EntradaGrid.Columns[5].HeaderText = "VALOR (R$)*";
                        EntradaGrid.Columns[5].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Fornecedor.Checked)
                {
                    if (FornecedorSort.Checked)
                    {
                        EntradaGrid.Columns[6].HeaderText = "FORNECEDOR* ↓";
                        EntradaGrid.Columns[6].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        EntradaGrid.Columns[6].HeaderText = "FORNECEDOR*";
                        EntradaGrid.Columns[6].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Responsavel.Checked)
                {
                    if (ResponsavelSort.Checked)
                    {
                        EntradaGrid.Columns[7].HeaderText = "RESPONSÁVEL* ↓";
                        EntradaGrid.Columns[7].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        EntradaGrid.Columns[7].HeaderText = "RESPONSÁVEL*";
                        EntradaGrid.Columns[7].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }

                else if (Data.Checked)
                {
                    if (DataSort.Checked)
                    {
                        EntradaGrid.Columns[8].HeaderText = "DATA* ↓";
                        EntradaGrid.Columns[8].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        EntradaGrid.Columns[8].ToolTipText = "Classificado: Mais nova para a mais antiga";
                    }
                    else
                    {
                        EntradaGrid.Columns[8].HeaderText = "DATA*";
                        EntradaGrid.Columns[8].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }
                }
            }

            if (FormLoaded)
                HideFrames();
        }

        /*--------------------------*/

        // Mudar filtro de fornecedor
        private async void FornecedorChecked(object sender, EventArgs e)
        {
            if (TodosFornecedores.Checked != true)
            {
                FornecedoresOptions.Size = new Size(220, 154);

                if (PesquisarFornecedor.Text != "")
                {
                    FilterFunction("FORNECEDOR", PesquisarFornecedor.Text);
                }

                RemoverFiltros.Visible = true;
                ColorFilter(ActiveFilter2);
            }
            else
            {
                FornecedoresOptions.Size = new Size(220, 64);

                RemoveFilterColor(ActiveFilter2);

                if (ActiveFilter1.Visible || ActiveFilter3.Visible)
                {
                    if (ActiveFilter1.Visible)
                        Console.WriteLine("Search filter ativado");

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

                await TaskDelay(100);

                HideFrames();
            }
        }

        /*--------------------------*/

        // Filtro de pesquisa
        private void SearchFilter()
        {
            DataView dv = entradaItemData.EntradaDeItens.DefaultView;

            if (DataFiltered)
            {
                if (All.Checked)
                {
                    dv.RowFilter = string.Format("convert (CODIGO, 'System.String') LIKE '%" + Search.Text + "%' OR PRODUTO LIKE '%" + Search.Text + "%' OR NUMEROFABRICANTE LIKE '%" + Search.Text + "%' OR convert (QNTDADQUIRIDA, 'System.String') LIKE '%" + Search.Text + "%' \n" +
                        "OR convert (NUMERONOTAFISCAL, 'System.String') LIKE '%" + Search.Text + "%' OR VALORTOTAL LIKE '%" + Search.Text + "%' OR FUNCIONARIORESPONSAVEL LIKE '%" + Search.Text + "%' OR FORNECEDOR LIKE '%" + Search.Text + "%' OR DATA LIKE '%" + Search.Text + "%' \n" +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'");
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }

                else if (Codigo.Checked)
                {
                    dv.RowFilter = "convert(CODIGO, 'System.String') LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }

                else if (Produto.Checked)
                {
                    dv.RowFilter = "PRODUTO LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }

                else if (NumeroFabricante.Checked)
                {
                    dv.RowFilter = "NUMEROFABRICANTE LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }

                else if (Adquirida.Checked)
                {
                    dv.RowFilter = "convert(QNTDADQUIRIDA, 'System.String') LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }

                else if (NotaFiscal.Checked)
                {
                    dv.RowFilter = "convert(NUMERONOTAFISCAL, 'System.String') LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }

                else if (Valor.Checked)
                {
                    dv.RowFilter = "VALORTOTAL LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }

                else if (Responsavel.Checked)
                {
                    dv.RowFilter = "FUNCIONARIORESPONSAVEL LIKE '%" + Search.Text + "%' " +
                         "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }

                else if (Fornecedor.Checked)
                {
                    dv.RowFilter = "FORNECEDOR LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }

                else if (Data.Checked)
                {
                    dv.RowFilter = "DATA LIKE '%" + Search.Text + "%' " +
                         "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }
            }
            else
            {
                if (All.Checked)
                {
                    dv.RowFilter = string.Format("convert (CODIGO, 'System.String') LIKE '%" + Search.Text + "%' OR PRODUTO LIKE '%" + Search.Text + "%' OR NUMEROFABRICANTE LIKE '%" + Search.Text + "%' OR convert (QNTDADQUIRIDA, 'System.String') LIKE '%" + Search.Text + "%' \n" +
                        "OR convert (NUMERONOTAFISCAL, 'System.String') LIKE '%" + Search.Text + "%' OR VALORTOTAL LIKE '%" + Search.Text + "%' OR FUNCIONARIORESPONSAVEL LIKE '%" + Search.Text + "%' OR FORNECEDOR LIKE '%" + Search.Text + "%' OR DATA LIKE '%" + Search.Text + "%'");
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }

                else if (Codigo.Checked)
                {
                    dv.RowFilter = "convert (CODIGO, 'System.String') LIKE '%" + Search.Text + "%'";
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }

                else if (Produto.Checked)
                {
                    dv.RowFilter = "PRODUTO LIKE '%" + Search.Text + "%'";
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }

                else if (NumeroFabricante.Checked)
                {
                    dv.RowFilter = "NUMEROFABRICANTE LIKE '%" + Search.Text + "%'";
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }

                else if (Adquirida.Checked)
                {
                    dv.RowFilter = "convert (QNTDADQUIRIDA, 'System.String') LIKE '%" + Search.Text + "%'";
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }

                else if (NotaFiscal.Checked)
                {
                    dv.RowFilter = "convert (NUMERONOTAFISCAL, 'System.String') LIKE '%" + Search.Text + "%'";
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }

                else if (Valor.Checked)
                {
                    dv.RowFilter = "VALORTOTAL LIKE '%" + Search.Text + "%'";
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }

                else if (Responsavel.Checked)
                {
                    dv.RowFilter = "FUNCIONARIORESPONSAVEL LIKE '%" + Search.Text + "%'";
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }

                else if (Fornecedor.Checked)
                {
                    dv.RowFilter = "FORNECEDOR LIKE '%" + Search.Text + "%'";
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }

                else if (Data.Checked)
                {
                    dv.RowFilter = "DATA LIKE '" + Convert.ToDateTime(Search.Text).ToString("dd-MM-yyyy") + "'";
                    entradaDeItensBindingSource.DataSource = dv;
                    entradaDeItensBindingSource1.DataSource = dv;
                }
            }
        }

        // Item nao encontrado
        private void NotFinded()
        {
            if (EntradaGrid.RowCount == 0)
            {
                if (RemoverFiltros.Visible)
                    NotFindDesc.Text = "Nenhum resultado que corresponda à sua pesquisa \n atual foi encontrado. Tente remover algum filtro";
                else
                    NotFindDesc.Text = "Nenhum resultado que corresponda \n à sua pesquisa atual foi encontrado.";

                EntradaGrid.Visible = false;
                Separator2.Visible = false;
                toolStripPaging.Visible = false;

                NotFind.Visible = true;

                NotFindDesc.Visible = true;
            }
            else
            {
                EntradaGrid.Visible = true;
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

            if (DataOptions.Visible)
            {
                DataOptions.Visible = false;
                DataOptions.Location = new Point(DataOptions.Location.X, DataOptions.Location.Y - 6);
            }

            if (SearchOptions.Visible)
            {
                if (RemoverFiltros.Visible)
                    SearchOptions.Location = new Point(5512, SearchOptions.Location.Y);
                else
                    SearchOptions.Location = new Point(5512, SearchOptions.Location.Y);
            }

            if (FornecedoresOptions.Visible)
            {
                if (RemoverFiltros.Visible)
                    FornecedoresOptions.Location = new Point(5512, FornecedoresOptions.Location.Y);
                else
                    FornecedoresOptions.Location = new Point(5512, FornecedoresOptions.Location.Y);
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

        private void HideNewEntradaItens()
        {
            EntradaGrid.Visible = true;
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
            Guna.UI2.WinForms.Guna2Panel[] Panels = new Guna.UI2.WinForms.Guna2Panel[12]
            {
                FilterItens, SearchOptions, DataOptions, FornecedoresOptions, SpecificDate,

                MoreOptions, SortOptions, ViewOptions,

                ExportItens, ExcelFrame, PdfFrame, ExportOptions
            };

            // Botoes normais - principais
            Guna.UI2.WinForms.Guna2Button[] MainButtons = new Guna.UI2.WinForms.Guna2Button[4]
            {
                FilterBtn, MoreOptionsBtn, ExportBtn, Voltar
            };

            // Botoes normais
            Guna.UI2.WinForms.Guna2Button[] PanelButtons = new Guna.UI2.WinForms.Guna2Button[17]
            {
                FilterPesquisaBtn, FilterFornecedoresBtn, FilterDataBtn, DataEspecifica,

                SortBtn, ViewOptionsBtn, ListaNormal, ListaCompacta, Crescente, Descrescente,

                ExcelBtn, PdfBtn, ExportOptionsBtn, ExportCurrentExcel, ExportAllExcel, ExportCurrentPdf, ExportAllPdf
            };

            // Botoes de escolha (radio button)
            Guna.UI2.WinForms.Guna2RadioButton[] RadioButtons = new Guna.UI2.WinForms.Guna2RadioButton[28]
            {
                All, Codigo, Produto, NumeroFabricante, Adquirida, NotaFiscal, Valor, Responsavel, Data, Fornecedor,

                TodosFornecedores, FornecedorEspecifico,

                TodoPeriodo, Hoje, Semana, Mes, Ano, AllColumns, Principais, CodigoSort, ProdutoSort, 

                NumeroFabricanteSort, AdquiridaSort, NotaFiscalSort, ValorSort, ResponsavelSort, DataSort, FornecedorSort
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
            Guna.UI2.WinForms.Guna2Separator[] Separators = new Guna.UI2.WinForms.Guna2Separator[17]
            {
                Separator1, Separator2, guna2Separator1, guna2Separator3, guna2Separator4, guna2Separator5,
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

            EntradaGrid.BackgroundColor = ThemeManager.FormBackColor;
            EntradaGrid.DefaultCellStyle.BackColor = ThemeManager.FormBackColor;
            EntradaGrid.DefaultCellStyle.SelectionBackColor = ThemeManager.FormBackColor;
            EntradaGrid.DefaultCellStyle.ForeColor = ThemeManager.GridForeColor;
            EntradaGrid.DefaultCellStyle.SelectionForeColor = ThemeManager.FontColor;
            EntradaGrid.GridColor = ThemeManager.SeparatorAndBorderColor;

            EntradaGrid.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.WhiteFontColor;
            EntradaGrid.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemeManager.WhiteFontColor;
            EntradaGrid.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.FormBackColor;
            EntradaGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemeManager.FormBackColor;

            EntradaGrid.RowHeadersDefaultCellStyle.BackColor = ThemeManager.FormBackColor;

            Search.FillColor = ThemeManager.SearchBoxFillColor;
            Search.ForeColor = ThemeManager.SearchBoxForeColor;
            Search.BorderColor = ThemeManager.SeparatorAndBorderColor;
            Search.HoverState.BorderColor = ThemeManager.SearchBoxHoverBorderColor;
            Search.PlaceholderForeColor = ThemeManager.SearchBoxPlaceholderColor;
            Search.FocusedState.BorderColor = ThemeManager.SearchBoxFocusedBorderColor;
            Search.FocusedState.ForeColor = ThemeManager.SearchBoxForeColor;

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

            NovaEntrada.BackColor = ThemeManager.FormBackColor;
            NovaEntrada.FillColor = ThemeManager.FullRedButtonColor;
            NovaEntrada.BorderColor = ThemeManager.FullRedButtonColor;
            NovaEntrada.HoverState.FillColor = ThemeManager.FullRedButtonHoverColor;
            NovaEntrada.HoverState.BorderColor = ThemeManager.FullRedButtonHoverColor;
            NovaEntrada.CheckedState.FillColor = ThemeManager.FullRedButtonCheckedColor;
            NovaEntrada.CheckedState.BorderColor = ThemeManager.FullRedButtonCheckedColor;

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

            CustomFornecedorBtn.FillColor = ThemeManager.BorderRedButtonFillColor2;
            CustomFornecedorBtn.ForeColor = ThemeManager.BorderRedButtonForeColor;
            CustomFornecedorBtn.BorderColor = ThemeManager.BorderRedButtonBorderColor;
            CustomFornecedorBtn.HoverState.BorderColor = ThemeManager.BorderRedButtonHoverBorderColor;
            CustomFornecedorBtn.HoverState.FillColor = ThemeManager.BorderRedButtonHoverFillColor2;
            CustomFornecedorBtn.HoverState.ForeColor = ThemeManager.BorderRedButtonHoverForeColor;
            CustomFornecedorBtn.PressedColor = ThemeManager.BorderRedButtonPressedColor;

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

            EntradasSelecionadas.ForeColor = ThemeManager.WhiteFontColor;

            DeleteAllSelected.ForeColor = ThemeManager.RedFontColor;
            DeleteAllSelected.FillColor = ThemeManager.FormBackColor;
            DeleteAllSelected.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            DeleteAllSelected.PressedColor = ThemeManager.ButtonPressedColor;

            ClientePicture.BackColor = ThemeManager.FormBackColor;

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

        // Novo produto
        private void NovoItem_Click(object sender, EventArgs e)
        {
            HideFrames();

            foreach (Form frm in fc)
            {
                if (frm.Name != "NovaEntrada")
                    NovaEntradaOpen = false;
                else
                    NovaEntradaOpen = true;
            }

            if (NovaEntradaOpen != true)
            {
                NovaEntradaOpen = true;

                ThreadStart ts = new ThreadStart(() =>
                {
                    DarkBackground(new Frames.NovaEntrada());
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
        private void EntradaGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == EntradaGrid.Columns[10].Index)
            {
                var cell = EntradaGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = "Excluir";
            }
        }

        // Atualizar o grid qnd adicionar/editar/deletar item
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
                    DarkBackground(new Frames.Delete.DeleteConfirmation2("EntradaItem", AllNamesDistinct));
                });

                Thread t = new Thread(ts);

                t.SetApartmentState(ApartmentState.STA);

                t.Start();

                Properties.Settings.Default.CanShowDeleteConfirmation = false;
            }

            if (Properties.Settings.Default.CanShowNewProductForm)
            {
                ThreadStart ts = new ThreadStart(() => {
                    DarkBackground(new Frames.Estoque.NovoProduto());
                });

                Thread t = new Thread(ts);

                t.SetApartmentState(ApartmentState.STA);

                t.Start();

                Properties.Settings.Default.CanShowNewProductForm = false;
            }
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

                EntradaGrid.Visible = true;
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

                    EntradaGrid.Visible = true;
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

            if (Codigo.Checked || Responsavel.Checked)
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

                FornecedoresOptions.Visible = false;
                FornecedoresOptions.Location = new Point(512, FornecedoresOptions.Location.Y);

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

        private async void FiltroPesquisaBtn_Click(object sender, EventArgs e)
        {
            if (SearchOptions.Visible)
            {
                SearchOptions.Visible = false;
                SearchOptions.Location = new Point(512, SearchOptions.Location.Y);
            }
            else
            {
                FornecedoresOptions.Visible = false;
                FornecedoresOptions.Location = new Point(512, FornecedoresOptions.Location.Y);

                DataOptions.Visible = false;
                DataOptions.Location = new Point(512, DataOptions.Location.Y);

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

                DataOptions.Visible = false;
                DataOptions.Location = new Point(512, DataOptions.Location.Y);

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

        private async void FilterDataBtn_Click(object sender, EventArgs e)
        {
            if (DataOptions.Visible)
            {
                DataOptions.Visible = false;
                DataOptions.Location = new Point(512, DataOptions.Location.Y);

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

                FornecedoresOptions.Visible = false;
                FornecedoresOptions.Location = new Point(512, FornecedoresOptions.Location.Y);

                if (AnimateButtonsAndPanels)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        await TaskDelay(10);
                        DataOptions.Location = new Point(DataOptions.Location.X + 1, DataOptions.Location.Y);
                        DataOptions.Visible = true;
                    }
                }
                else
                {
                    DataOptions.Location = new Point(DataOptions.Location.X + 6, DataOptions.Location.Y);
                    DataOptions.Visible = true;
                }
            }
        }

        // Selecionar fornecedor especifico
        private void CustomFornecedorBtn_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

            con.Open();

            OleDbCommand cmd = new OleDbCommand("SELECT * FROM EntradaDeItens WHERE FORNECEDOR = '" + PesquisarFornecedor.Text + "'", con);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                HideNewEntradaItens();

                DataTable customrows = dt.AsEnumerable()
                .Skip(PageRows * CurrentPage)
                .Take(PageRows)
                .ToList()
                .CopyToDataTable();

                DataTable customrows2 = dt;

                // Produtos data
                entradaDeItensBindingSource.DataSource = customrows;
                entradaDeItensTableAdapter.Fill(entradaItemData.EntradaDeItens);

                // All produtos data
                entradaDeItensBindingSource1.DataSource = customrows2;
                entradaDeItensTableAdapter1.Fill(allEntradaItemData.EntradaDeItens);

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

                EntradaGrid.Visible = true;
                Separator2.Visible = true;
                toolStripPaging.Visible = true;

                NotFind.Visible = false;

                NotFindDesc.Visible = false;
            }
            else
            {
                NotFindDesc.Text = "Nenhum resultado que corresponda \n à sua pesquisa atual foi encontrado.";

                EntradaGrid.Visible = false;
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
            SpecificDateFunction(Data1.Value, Data2.Value);

            RemoverFiltros.Visible = true;
            DataFiltered = true;

            await TaskDelay(100);

            DataOptions.Location = new Point(5512, DataOptions.Location.Y);
            ColorFilter(ActiveFilter3);

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

                if (ActiveFilter2.Visible || ActiveFilter3.Visible)
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
            TodosFornecedores.Checked = true;

            NotFind.Visible = false;
            NotFindDesc.Visible = false;

            EntradaGrid.Visible = true;
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
            ExportExcel.FileName = "Planilha de entrada de itens";
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
                    ExcelApp.Columns[1].ColumnWidth = 15;
                    ExcelApp.Columns[2].ColumnWidth = 30;
                    ExcelApp.Columns[3].ColumnWidth = 15;
                    ExcelApp.Columns[4].ColumnWidth = 15;
                    ExcelApp.Columns[5].ColumnWidth = 15;
                    ExcelApp.Columns[6].ColumnWidth = 15;
                    ExcelApp.Columns[7].ColumnWidth = 20;
                    ExcelApp.Columns[8].ColumnWidth = 20;
                    ExcelApp.Columns[9].ColumnWidth = 20;
                    ExcelApp.Columns[10].ColumnWidth = 20;

                    for (int i = 1; i < EntradaGrid.Columns.Count; i++)
                    {
                        if (SortedByOrder == "Ascending")
                        {
                            ExcelApp.Cells[1] = EntradaGrid.Columns[0].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[2] = EntradaGrid.Columns[1].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[3] = EntradaGrid.Columns[2].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[4] = EntradaGrid.Columns[3].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[5] = EntradaGrid.Columns[4].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[6] = EntradaGrid.Columns[5].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[7] = EntradaGrid.Columns[6].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[8] = EntradaGrid.Columns[7].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[9] = EntradaGrid.Columns[8].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[10] = EntradaGrid.Columns[9].HeaderText.Replace("↑", "");
                        }

                        else if (SortedByOrder == "Descending")
                        {
                            ExcelApp.Cells[1] = EntradaGrid.Columns[0].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[2] = EntradaGrid.Columns[1].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[3] = EntradaGrid.Columns[2].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[4] = EntradaGrid.Columns[3].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[5] = EntradaGrid.Columns[4].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[6] = EntradaGrid.Columns[5].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[7] = EntradaGrid.Columns[6].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[8] = EntradaGrid.Columns[7].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[9] = EntradaGrid.Columns[8].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[10] = EntradaGrid.Columns[9].HeaderText.Replace("↓", "");
                        }
                    }

                    for (int i = 0; i < EntradaGrid.Rows.Count; i++)
                    {
                        for (int j = 1; j < EntradaGrid.Columns.Count; j++)
                        {
                            ExcelApp.Cells[i + 2, 1] = EntradaGrid.Rows[i].Cells[0].Value.ToString();
                            ExcelApp.Cells[i + 2, 2] = EntradaGrid.Rows[i].Cells[1].Value.ToString();
                            ExcelApp.Cells[i + 2, 3] = EntradaGrid.Rows[i].Cells[2].Value.ToString();
                            ExcelApp.Cells[i + 2, 4] = EntradaGrid.Rows[i].Cells[3].Value.ToString();
                            ExcelApp.Cells[i + 2, 5] = EntradaGrid.Rows[i].Cells[4].Value.ToString();
                            ExcelApp.Cells[i + 2, 6] = EntradaGrid.Rows[i].Cells[5].Value.ToString();
                            ExcelApp.Cells[i + 2, 7] = EntradaGrid.Rows[i].Cells[6].Value.ToString();
                            ExcelApp.Cells[i + 2, 8] = EntradaGrid.Rows[i].Cells[7].Value.ToString();
                            ExcelApp.Cells[i + 2, 9] = EntradaGrid.Rows[i].Cells[8].Value.ToString();
                            ExcelApp.Cells[i + 2, 10] = EntradaGrid.Rows[i].Cells[9].Value.ToString();
                        }
                    }
                }

                /*else if (AllColumns.Checked)
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

                    for (int i = 1; i < EntradaGrid.Columns.Count; i++)
                    {
                        if (SortedByOrder == "Ascending")
                        {
                            ExcelApp.Cells[1] = EntradaGrid.Columns[0].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[2] = EntradaGrid.Columns[1].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[3] = EntradaGrid.Columns[2].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[4] = EntradaGrid.Columns[3].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[5] = EntradaGrid.Columns[4].HeaderText;
                            ExcelApp.Cells[6] = EntradaGrid.Columns[5].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[7] = EntradaGrid.Columns[6].HeaderText;
                            ExcelApp.Cells[8] = EntradaGrid.Columns[7].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[9] = EntradaGrid.Columns[8].HeaderText;
                            ExcelApp.Cells[10] = EntradaGrid.Columns[9].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[11] = EntradaGrid.Columns[10].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[12] = EntradaGrid.Columns[11].HeaderText;
                            ExcelApp.Cells[13] = EntradaGrid.Columns[12].HeaderText;
                            ExcelApp.Cells[14] = EntradaGrid.Columns[13].HeaderText;
                            ExcelApp.Cells[15] = EntradaGrid.Columns[14].HeaderText;
                            ExcelApp.Cells[16] = EntradaGrid.Columns[15].HeaderText;
                            ExcelApp.Cells[17] = EntradaGrid.Columns[16].HeaderText;
                            ExcelApp.Cells[18] = EntradaGrid.Columns[17].HeaderText;
                            ExcelApp.Cells[19] = EntradaGrid.Columns[18].HeaderText;
                            ExcelApp.Cells[20] = EntradaGrid.Columns[19].HeaderText;
                            ExcelApp.Cells[21] = EntradaGrid.Columns[20].HeaderText;
                            ExcelApp.Cells[22] = EntradaGrid.Columns[21].HeaderText.Replace("↑", "") ;
                            ExcelApp.Cells[23] = EntradaGrid.Columns[22].HeaderText;
                            ExcelApp.Cells[24] = EntradaGrid.Columns[23].HeaderText;
                            ExcelApp.Cells[25] = EntradaGrid.Columns[24].HeaderText;
                            ExcelApp.Cells[26] = EntradaGrid.Columns[25].HeaderText;
                            ExcelApp.Cells[27] = EntradaGrid.Columns[28].HeaderText.Replace("↑", "");
                        }

                        else if (SortedByOrder == "Descending")
                        {
                            ExcelApp.Cells[1] = EntradaGrid.Columns[0].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[2] = EntradaGrid.Columns[1].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[3] = EntradaGrid.Columns[2].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[4] = EntradaGrid.Columns[3].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[5] = EntradaGrid.Columns[4].HeaderText;
                            ExcelApp.Cells[6] = EntradaGrid.Columns[5].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[7] = EntradaGrid.Columns[6].HeaderText;
                            ExcelApp.Cells[8] = EntradaGrid.Columns[7].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[9] = EntradaGrid.Columns[8].HeaderText;
                            ExcelApp.Cells[10] = EntradaGrid.Columns[9].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[11] = EntradaGrid.Columns[10].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[12] = EntradaGrid.Columns[11].HeaderText;
                            ExcelApp.Cells[13] = EntradaGrid.Columns[12].HeaderText;
                            ExcelApp.Cells[14] = EntradaGrid.Columns[13].HeaderText;
                            ExcelApp.Cells[15] = EntradaGrid.Columns[14].HeaderText;
                            ExcelApp.Cells[16] = EntradaGrid.Columns[15].HeaderText;
                            ExcelApp.Cells[17] = EntradaGrid.Columns[16].HeaderText;
                            ExcelApp.Cells[18] = EntradaGrid.Columns[17].HeaderText;
                            ExcelApp.Cells[19] = EntradaGrid.Columns[18].HeaderText;
                            ExcelApp.Cells[20] = EntradaGrid.Columns[19].HeaderText;
                            ExcelApp.Cells[21] = EntradaGrid.Columns[20].HeaderText;
                            ExcelApp.Cells[22] = EntradaGrid.Columns[21].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[23] = EntradaGrid.Columns[22].HeaderText; 
                            ExcelApp.Cells[24] = EntradaGrid.Columns[23].HeaderText;
                            ExcelApp.Cells[25] = EntradaGrid.Columns[24].HeaderText;
                            ExcelApp.Cells[26] = EntradaGrid.Columns[27].HeaderText;
                            ExcelApp.Cells[27] = EntradaGrid.Columns[28].HeaderText.Replace("↓", "");
                        }
                    }

                    for (int i = 0; i < EntradaGrid.Rows.Count; i++)
                    {
                        for (int j = 1; j < EntradaGrid.Columns.Count - 2; j++)
                        {
                            ExcelApp.Cells[i + 2, 1] = EntradaGrid.Rows[i].Cells[0].Value.ToString();
                            ExcelApp.Cells[i + 2, 2] = EntradaGrid.Rows[i].Cells[1].Value.ToString();
                            ExcelApp.Cells[i + 2, 3] = EntradaGrid.Rows[i].Cells[2].Value.ToString();
                            ExcelApp.Cells[i + 2, 4] = EntradaGrid.Rows[i].Cells[3].Value.ToString();
                            ExcelApp.Cells[i + 2, 5] = EntradaGrid.Rows[i].Cells[4].Value.ToString();
                            ExcelApp.Cells[i + 2, 6] = EntradaGrid.Rows[i].Cells[5].Value.ToString();
                            ExcelApp.Cells[i + 2, 7] = EntradaGrid.Rows[i].Cells[6].Value.ToString();
                            ExcelApp.Cells[i + 2, 8] = EntradaGrid.Rows[i].Cells[7].Value.ToString();
                            ExcelApp.Cells[i + 2, 9] = EntradaGrid.Rows[i].Cells[8].Value.ToString();
                            ExcelApp.Cells[i + 2, 10] = EntradaGrid.Rows[i].Cells[9].Value.ToString();
                            ExcelApp.Cells[i + 2, 11] = EntradaGrid.Rows[i].Cells[10].Value.ToString();
                            ExcelApp.Cells[i + 2, 12] = EntradaGrid.Rows[i].Cells[11].Value.ToString();
                            ExcelApp.Cells[i + 2, 13] = EntradaGrid.Rows[i].Cells[12].Value.ToString();
                            ExcelApp.Cells[i + 2, 14] = EntradaGrid.Rows[i].Cells[13].Value.ToString();
                            ExcelApp.Cells[i + 2, 15] = EntradaGrid.Rows[i].Cells[14].Value.ToString();
                            ExcelApp.Cells[i + 2, 16] = EntradaGrid.Rows[i].Cells[15].Value.ToString();
                            ExcelApp.Cells[i + 2, 17] = EntradaGrid.Rows[i].Cells[16].Value.ToString();
                            ExcelApp.Cells[i + 2, 18] = EntradaGrid.Rows[i].Cells[17].Value.ToString();
                            ExcelApp.Cells[i + 2, 19] = EntradaGrid.Rows[i].Cells[18].Value.ToString();
                            ExcelApp.Cells[i + 2, 20] = EntradaGrid.Rows[i].Cells[19].Value.ToString();
                            ExcelApp.Cells[i + 2, 21] = EntradaGrid.Rows[i].Cells[20].Value.ToString();
                            ExcelApp.Cells[i + 2, 22] = EntradaGrid.Rows[i].Cells[21].Value.ToString();
                            ExcelApp.Cells[i + 2, 23] = EntradaGrid.Rows[i].Cells[22].Value.ToString();
                            ExcelApp.Cells[i + 2, 24] = EntradaGrid.Rows[i].Cells[23].Value.ToString();
                            ExcelApp.Cells[i + 2, 25] = EntradaGrid.Rows[i].Cells[24].Value.ToString();
                            ExcelApp.Cells[i + 2, 26] = EntradaGrid.Rows[i].Cells[27].Value.ToString();
                            ExcelApp.Cells[i + 2, 27] = EntradaGrid.Rows[i].Cells[28].Value.ToString();
                        }
                    }
                }*/

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
            ExportExcel.FileName = "Planilha geral de entrada de itens";
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
                    ExcelApp.Columns[1].ColumnWidth = 15;
                    ExcelApp.Columns[2].ColumnWidth = 30;
                    ExcelApp.Columns[3].ColumnWidth = 15;
                    ExcelApp.Columns[4].ColumnWidth = 15;
                    ExcelApp.Columns[5].ColumnWidth = 15;
                    ExcelApp.Columns[6].ColumnWidth = 15;
                    ExcelApp.Columns[7].ColumnWidth = 20;
                    ExcelApp.Columns[8].ColumnWidth = 20;
                    ExcelApp.Columns[9].ColumnWidth = 20;
                    ExcelApp.Columns[10].ColumnWidth = 20;

                    for (int i = 1; i < AllDataGrid.Columns.Count; i++)
                    {
                        if (SortedByOrder == "Ascending")
                        {
                            ExcelApp.Cells[1] = AllDataGrid.Columns[0].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[2] = AllDataGrid.Columns[1].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[3] = AllDataGrid.Columns[2].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[4] = AllDataGrid.Columns[3].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[5] = AllDataGrid.Columns[4].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[6] = AllDataGrid.Columns[5].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[7] = AllDataGrid.Columns[6].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[8] = AllDataGrid.Columns[7].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[9] = AllDataGrid.Columns[8].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[10] = AllDataGrid.Columns[9].HeaderText.Replace("↑", "");
                        }

                        else if (SortedByOrder == "Descending")
                        {
                            ExcelApp.Cells[1] = AllDataGrid.Columns[0].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[2] = AllDataGrid.Columns[1].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[3] = AllDataGrid.Columns[2].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[4] = AllDataGrid.Columns[3].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[5] = AllDataGrid.Columns[4].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[6] = AllDataGrid.Columns[5].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[7] = AllDataGrid.Columns[6].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[8] = AllDataGrid.Columns[7].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[9] = AllDataGrid.Columns[8].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[10] = AllDataGrid.Columns[9].HeaderText.Replace("↓", "");
                        }
                    }

                    for (int i = 0; i < EntradaGrid.Rows.Count; i++)
                    {
                        for (int j = 1; j < EntradaGrid.Columns.Count; j++)
                        {
                            ExcelApp.Cells[i + 2, 1] = AllDataGrid.Rows[i].Cells[0].Value.ToString();
                            ExcelApp.Cells[i + 2, 2] = AllDataGrid.Rows[i].Cells[1].Value.ToString();
                            ExcelApp.Cells[i + 2, 3] = AllDataGrid.Rows[i].Cells[2].Value.ToString();
                            ExcelApp.Cells[i + 2, 4] = AllDataGrid.Rows[i].Cells[3].Value.ToString();
                            ExcelApp.Cells[i + 2, 5] = AllDataGrid.Rows[i].Cells[4].Value.ToString();
                            ExcelApp.Cells[i + 2, 6] = AllDataGrid.Rows[i].Cells[5].Value.ToString();
                            ExcelApp.Cells[i + 2, 7] = AllDataGrid.Rows[i].Cells[6].Value.ToString();
                            ExcelApp.Cells[i + 2, 8] = AllDataGrid.Rows[i].Cells[7].Value.ToString();
                            ExcelApp.Cells[i + 2, 9] = AllDataGrid.Rows[i].Cells[8].Value.ToString();
                            ExcelApp.Cells[i + 2, 10] = AllDataGrid.Rows[i].Cells[9].Value.ToString();
                        }
                    }
                }

                /*else if (AllColumns.Checked)
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
                            ExcelApp.Cells[1] = EntradaGrid.Columns[0].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[2] = EntradaGrid.Columns[1].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[3] = EntradaGrid.Columns[2].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[4] = EntradaGrid.Columns[3].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[5] = EntradaGrid.Columns[4].HeaderText;
                            ExcelApp.Cells[6] = EntradaGrid.Columns[5].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[7] = EntradaGrid.Columns[6].HeaderText;
                            ExcelApp.Cells[8] = EntradaGrid.Columns[7].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[9] = EntradaGrid.Columns[8].HeaderText;
                            ExcelApp.Cells[10] = EntradaGrid.Columns[9].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[11] = EntradaGrid.Columns[10].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[12] = EntradaGrid.Columns[11].HeaderText;
                            ExcelApp.Cells[13] = EntradaGrid.Columns[12].HeaderText;
                            ExcelApp.Cells[14] = EntradaGrid.Columns[13].HeaderText;
                            ExcelApp.Cells[15] = EntradaGrid.Columns[14].HeaderText;
                            ExcelApp.Cells[16] = EntradaGrid.Columns[15].HeaderText;
                            ExcelApp.Cells[17] = EntradaGrid.Columns[16].HeaderText;
                            ExcelApp.Cells[18] = EntradaGrid.Columns[17].HeaderText;
                            ExcelApp.Cells[19] = EntradaGrid.Columns[18].HeaderText;
                            ExcelApp.Cells[20] = EntradaGrid.Columns[19].HeaderText;
                            ExcelApp.Cells[21] = EntradaGrid.Columns[20].HeaderText;
                            ExcelApp.Cells[22] = EntradaGrid.Columns[21].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[23] = EntradaGrid.Columns[22].HeaderText;
                            ExcelApp.Cells[24] = EntradaGrid.Columns[23].HeaderText;
                            ExcelApp.Cells[25] = EntradaGrid.Columns[24].HeaderText;
                            ExcelApp.Cells[26] = EntradaGrid.Columns[25].HeaderText;
                            ExcelApp.Cells[27] = EntradaGrid.Columns[28].HeaderText.Replace("↑", "");
                        }

                        else if (SortedByOrder == "Descending")
                        {
                            ExcelApp.Cells[1] = EntradaGrid.Columns[0].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[2] = EntradaGrid.Columns[1].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[3] = EntradaGrid.Columns[2].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[4] = EntradaGrid.Columns[3].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[5] = EntradaGrid.Columns[4].HeaderText;
                            ExcelApp.Cells[6] = EntradaGrid.Columns[5].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[7] = EntradaGrid.Columns[6].HeaderText;
                            ExcelApp.Cells[8] = EntradaGrid.Columns[7].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[9] = EntradaGrid.Columns[8].HeaderText;
                            ExcelApp.Cells[10] = EntradaGrid.Columns[9].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[11] = EntradaGrid.Columns[10].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[12] = EntradaGrid.Columns[11].HeaderText;
                            ExcelApp.Cells[13] = EntradaGrid.Columns[12].HeaderText;
                            ExcelApp.Cells[14] = EntradaGrid.Columns[13].HeaderText;
                            ExcelApp.Cells[15] = EntradaGrid.Columns[14].HeaderText;
                            ExcelApp.Cells[16] = EntradaGrid.Columns[15].HeaderText;
                            ExcelApp.Cells[17] = EntradaGrid.Columns[16].HeaderText;
                            ExcelApp.Cells[18] = EntradaGrid.Columns[17].HeaderText;
                            ExcelApp.Cells[19] = EntradaGrid.Columns[18].HeaderText;
                            ExcelApp.Cells[20] = EntradaGrid.Columns[19].HeaderText;
                            ExcelApp.Cells[21] = EntradaGrid.Columns[20].HeaderText;
                            ExcelApp.Cells[22] = EntradaGrid.Columns[21].HeaderText;
                            ExcelApp.Cells[23] = EntradaGrid.Columns[22].HeaderText.Replace("↓", ""); ;
                            ExcelApp.Cells[24] = EntradaGrid.Columns[23].HeaderText;
                            ExcelApp.Cells[25] = EntradaGrid.Columns[24].HeaderText;
                            ExcelApp.Cells[26] = EntradaGrid.Columns[25].HeaderText;
                            ExcelApp.Cells[27] = EntradaGrid.Columns[26].HeaderText;
                            ExcelApp.Cells[28] = EntradaGrid.Columns[27].HeaderText;
                            ExcelApp.Cells[29] = EntradaGrid.Columns[28].HeaderText.Replace("↓", ""); ;
                        }
                    }

                    for (int i = 0; i < AllDataGrid.Rows.Count; i++)
                    {
                        for (int j = 1; j < AllDataGrid.Columns.Count - 2; j++)
                        {
                            ExcelApp.Cells[i + 2, 1] = EntradaGrid.Rows[i].Cells[0].Value.ToString();
                            ExcelApp.Cells[i + 2, 2] = EntradaGrid.Rows[i].Cells[1].Value.ToString();
                            ExcelApp.Cells[i + 2, 3] = EntradaGrid.Rows[i].Cells[2].Value.ToString();
                            ExcelApp.Cells[i + 2, 4] = EntradaGrid.Rows[i].Cells[3].Value.ToString();
                            ExcelApp.Cells[i + 2, 5] = EntradaGrid.Rows[i].Cells[4].Value.ToString();
                            ExcelApp.Cells[i + 2, 6] = EntradaGrid.Rows[i].Cells[5].Value.ToString();
                            ExcelApp.Cells[i + 2, 7] = EntradaGrid.Rows[i].Cells[6].Value.ToString();
                            ExcelApp.Cells[i + 2, 8] = EntradaGrid.Rows[i].Cells[7].Value.ToString();
                            ExcelApp.Cells[i + 2, 9] = EntradaGrid.Rows[i].Cells[8].Value.ToString();
                            ExcelApp.Cells[i + 2, 10] = EntradaGrid.Rows[i].Cells[9].Value.ToString();
                            ExcelApp.Cells[i + 2, 11] = EntradaGrid.Rows[i].Cells[10].Value.ToString();
                            ExcelApp.Cells[i + 2, 12] = EntradaGrid.Rows[i].Cells[11].Value.ToString();
                            ExcelApp.Cells[i + 2, 13] = EntradaGrid.Rows[i].Cells[12].Value.ToString();
                            ExcelApp.Cells[i + 2, 14] = EntradaGrid.Rows[i].Cells[13].Value.ToString();
                            ExcelApp.Cells[i + 2, 15] = EntradaGrid.Rows[i].Cells[14].Value.ToString();
                            ExcelApp.Cells[i + 2, 16] = EntradaGrid.Rows[i].Cells[15].Value.ToString();
                            ExcelApp.Cells[i + 2, 17] = EntradaGrid.Rows[i].Cells[16].Value.ToString();
                            ExcelApp.Cells[i + 2, 18] = EntradaGrid.Rows[i].Cells[17].Value.ToString();
                            ExcelApp.Cells[i + 2, 19] = EntradaGrid.Rows[i].Cells[18].Value.ToString();
                            ExcelApp.Cells[i + 2, 20] = EntradaGrid.Rows[i].Cells[19].Value.ToString();
                            ExcelApp.Cells[i + 2, 21] = EntradaGrid.Rows[i].Cells[20].Value.ToString();
                            ExcelApp.Cells[i + 2, 22] = EntradaGrid.Rows[i].Cells[21].Value.ToString();
                            ExcelApp.Cells[i + 2, 23] = EntradaGrid.Rows[i].Cells[22].Value.ToString();
                            ExcelApp.Cells[i + 2, 24] = EntradaGrid.Rows[i].Cells[23].Value.ToString();
                            ExcelApp.Cells[i + 2, 25] = EntradaGrid.Rows[i].Cells[24].Value.ToString();
                            ExcelApp.Cells[i + 2, 26] = EntradaGrid.Rows[i].Cells[27].Value.ToString();
                            ExcelApp.Cells[i + 2, 27] = EntradaGrid.Rows[i].Cells[28].Value.ToString();
                        }
                    }
                }*/

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
            if (EntradaGrid.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Relatório de entrada de itens.pdf";
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
                                PdfPTable pdfTable = new PdfPTable(EntradaGrid.Columns.Count - 2);
                                pdfTable.DefaultCell.Padding = 3;
                                pdfTable.WidthPercentage = 100;
                                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                                foreach (DataGridViewColumn column in EntradaGrid.Columns)
                                {
                                    if (column.Index != 8 && column.Index != 10)
                                    {
                                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                        cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                                        cell.Padding = 6;
                                        pdfTable.AddCell(cell);
                                    }
                                }

                                foreach (DataGridViewRow row in EntradaGrid.Rows)
                                {
                                    foreach (DataGridViewCell cell in row.Cells)
                                    {
                                        if (cell.ColumnIndex != 8 && cell.ColumnIndex != 10)
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
                                    var Nome = new Paragraph("                           CLÍNICA CAR                                       " +
                                        "Relatório de entrada de itens", NomeParagraph);
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
                                            var Texto = new Paragraph("Relatório de entrada de itens - Hoje\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        if (FilterByDays == 8)
                                        {
                                            var Texto = new Paragraph("Relatório de entrada de itens - Últimos 7 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 31)
                                        {
                                            var Texto = new Paragraph("Relatório de entrada de itens - Últimos 30 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 365)
                                        {
                                            var Texto = new Paragraph("Relatório de entrada de itens - Últimos 365 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }
                                    }

                                    else
                                    {
                                        var Texto = new Paragraph("Relatório de entrada de itens\n\n", TextoParagraph);

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
                                PdfPTable pdfTable = new PdfPTable(EntradaGrid.Columns.Count - 1);
                                pdfTable.DefaultCell.Padding = 6;
                                pdfTable.WidthPercentage = 101.25F;
                                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                                foreach (DataGridViewColumn column in EntradaGrid.Columns)
                                {
                                    if (column.Index != 10)
                                    {
                                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                        cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                                        cell.Padding = 8;
                                        pdfTable.AddCell(cell);
                                    }
                                }

                                foreach (DataGridViewRow row in EntradaGrid.Rows)
                                {
                                    foreach (DataGridViewCell cell in row.Cells)
                                    {
                                        if (cell.ColumnIndex != 10)
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
                                    var Nome = new Paragraph("                           CLÍNICA CAR                                       " +
                                        "Relatório de entrada de itens", NomeParagraph);
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
                                            var Texto = new Paragraph("Relatório de entrada de itens - Hoje\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        if (FilterByDays == 8)
                                        {
                                            var Texto = new Paragraph("Relatório de entrada de itens - Últimos 7 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 31)
                                        {
                                            var Texto = new Paragraph("Relatório de entrada de itens - Últimos 30 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 365)
                                        {
                                            var Texto = new Paragraph("Relatório de entrada de itens - Últimos 365 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }
                                    }

                                    else
                                    {
                                        var Texto = new Paragraph("Relatório de entrada de itens\n\n", TextoParagraph);

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
                sfd.FileName = "Relatório geral de entrada de itens.pdf";
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
                                PdfPTable pdfTable = new PdfPTable(AllDataGrid.Columns.Count - 1);
                                pdfTable.DefaultCell.Padding = 3;
                                pdfTable.WidthPercentage = 100;
                                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                                foreach (DataGridViewColumn column in AllDataGrid.Columns)
                                {
                                    if (column.Index != 8)
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
                                        if (cell.ColumnIndex != 8)
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
                                    var Nome = new Paragraph("                           CLÍNICA CAR                                       " +
                                        "Relatório de entrada de itens", NomeParagraph);
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
                                            var Texto = new Paragraph("Relatório geral de entrada de itens - Hoje\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        if (FilterByDays == 8)
                                        {
                                            var Texto = new Paragraph("Relatório geral de entrada de itens - Últimos 7 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 31)
                                        {
                                            var Texto = new Paragraph("Relatório geral de entrada de itens - Últimos 30 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 365)
                                        {
                                            var Texto = new Paragraph("Relatório geral de entrada de itens - Últimos 365 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }
                                    }

                                    else
                                    {
                                        var Texto = new Paragraph("Relatório geral de entrada de itens\n\n", TextoParagraph);

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
                                PdfPTable pdfTable = new PdfPTable(AllDataGrid.Columns.Count - 1);
                                pdfTable.DefaultCell.Padding = 6;
                                pdfTable.WidthPercentage = 101.25F;
                                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                                foreach (DataGridViewColumn column in AllDataGrid.Columns)
                                {
                                    if (column.Index != 10)
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
                                        if (cell.ColumnIndex != 10)
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
                                    var Nome = new Paragraph("                           CLÍNICA CAR                                       " +
                                        "Relatório de entrada de itens", NomeParagraph);
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
                                            var Texto = new Paragraph("Relatório geral de entrada de itens - Hoje\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        if (FilterByDays == 8)
                                        {
                                            var Texto = new Paragraph("Relatório geral de entrada de itens - Últimos 7 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 31)
                                        {
                                            var Texto = new Paragraph("Tabela ggeral de Produtos - Últimos 30 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 365)
                                        {
                                            var Texto = new Paragraph("Relatório geral de entrada de itens - Últimos 365 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }
                                    }

                                    else
                                    {
                                        var Texto = new Paragraph("Relatório geral de entrada de itens\n\n", TextoParagraph);

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

        /* Informaçoes dos itens */

        // Botoes
        private void InformacoesBtn_Click(object sender, EventArgs e)
        {
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
            InformacoesBtn.ForeColor = Color.FromArgb(180, 180, 180);
            ValoresBtn.ForeColor = Color.FromArgb(180, 180, 180);
            QuantidadesBtn.ForeColor = Color.FromArgb(180, 180, 180);
            OutrosBtn.ForeColor = ThemeManager.RedFontColor;

            InformacoesPanel.Visible = false;
            ValoresPanel.Visible = false;
            QuantidadesPanel.Visible = false;
            OutrosPanel.Visible = true;
        }

        // Editars
        /*private void Editar_Click(object sender, EventArgs e)
        {
            foreach (Form frm in fc)
            {
                if (frm.Name != "EditProduto")
                    EditProdutoOpen = false;
                else
                    EditProdutoOpen = true;
            }

            if (EditProdutoOpen != true)
            {
                EditProdutoOpen = true;

                byte[] Foto;

                string Curva, Codigo, Produto, NFabricante, NOriginal, Marca, Grupo, Subgrupo, Tipo, Unidade, Disponivel, Minima,
                    Ideal, Custo, VendaConsumidor, Revenda, VendaOutros, CustoDolar, LucroDolar, LucroPorcento, LucroReais, UltimaVenda,
                    Localizacao, Prateleira, Observacoes, Ean13Text, Code128Text, Fornecedor, Status;

                var StringNumbers = Regex.Match(CodigoInfo.Text, @"\d+").Value;
        
                Curva = CurvaInfo.Text;
                Codigo = StringNumbers;
                Produto = ProductName.Text;
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

                Foto = ImageToByteArray(ClientePicture.Image);

                ThreadStart ts = new ThreadStart(() => {
                    DarkBackground(new Frames.Estoque.EditProduto(Curva, Codigo, Produto, NFabricante, NOriginal, Marca, Grupo, Subgrupo, Tipo, Unidade, Disponivel, Minima,
                    Ideal, Custo, VendaConsumidor, Revenda, VendaOutros, CustoDolar, LucroDolar, LucroPorcento, LucroReais, UltimaVenda,
                    Localizacao, Prateleira, Observacoes, Ean13Text, Code128Text, Fornecedor, Status, Foto));
                });

                Thread t = new Thread(ts);

                t.SetApartmentState(ApartmentState.STA);

                t.Start();
            }
        }*/

        // Excluir
        private void Excluir_Click(object sender, EventArgs e)
        {
            foreach (Form frm in fc)
            {
                if (frm.Name != "DeleteSelected")
                    DeleteEntradaOpen = false;
                else
                    DeleteEntradaOpen = true;
            }

            if (DeleteEntradaOpen != true)
            {
                DeleteEntradaOpen = true;

                ProductNameString = ProdutoName.Text;

                Frames.DeleteSelected2 DeleteSelectedForm = new Frames.DeleteSelected2("EntradaItem", ProductNameString);

                DeleteSelectedForm.Text = "Excluir item";
                Frames.DeleteSelected2.DeleteSelectedFrame.TmplText.Text = "Excluir item";
                Frames.DeleteSelected2.DeleteSelectedFrame.LblText.Text = "Você deseja mesmo excluir esse item?";

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
        /*private void Voltar_Click(object sender, EventArgs e)
        {
            ProductInfo.Location = new Point(ProductInfo.Location.X, 11106);

            Search.Visible = true; FilterBtn.Visible = true; ExportBtn.Visible = true;
            PageItens.Visible = true; MostrarText.Visible = true; NovaEntrada.Visible = true;
            MoreOptionsBtn.Visible = true;

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
        }*/

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

        // Mostrar frame pra excluir item
        private void EntradaGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (entradaItemData.EntradaDeItens.Count > 0)
            {
                if (EntradaGrid.CurrentCell.ColumnIndex == 10)
                {
                    ProdutoName.Text = EntradaGrid.CurrentRow.Cells[1].Value.ToString();

                    Console.WriteLine(CodigoIntText.Text);

                    foreach (Form frm in fc)
                    {
                        if (frm.Name != "DeleteSelected2")
                            DeleteEntradaOpen = false;
                        else
                            DeleteEntradaOpen = true;
                    }

                    if (DeleteEntradaOpen != true)
                    {
                        DeleteEntradaOpen = true;

                        ProductNameString = ProdutoName.Text;

                        Frames.DeleteSelected2.DeleteSelectedFrame.TmplText.Text = "Excluir item";
                        Frames.DeleteSelected2.DeleteSelectedFrame.LblText.Text = "Você deseja mesmo excluir esse item?";

                        ThreadStart ts = new ThreadStart(() => {
                            DarkBackground(new Frames.DeleteSelected2("EntradaItem", ProductNameString));
                        });

                        Thread t = new Thread(ts);

                        t.SetApartmentState(ApartmentState.STA);

                        t.Start();
                    }
                }
            }
        }

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

            EntradaGrid.GridColor = ThemeManager.SeparatorAndBorderColor;
            EntradaGrid.RowTemplate.Height = 44;
            entradaDeItensBindingSource.DataSource = null;
            ReloadPage();

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

            EntradaGrid.GridColor = ThemeManager.FormBackColor;
            EntradaGrid.RowTemplate.Height = 32;
            entradaDeItensBindingSource.DataSource = null;
            ReloadPage();

            HideFrames();
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Opçoes de seleçao de itens */

        // Exibir o tanto de itens selecionados e mostrar as opçoes
        private void EstoqueGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                AllNames.Clear();

                foreach (DataGridViewRow dgv in EntradaGrid.SelectedRows)
                {
                    if (dgv.Selected)
                        if (EntradaGrid.SelectedRows.Count >= 2)
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

                if (EntradaGrid.SelectedRows.Count >= 2)
                {
                    SelectedOptions.Visible = true;
                    EntradasSelecionadas.Text = EntradaGrid.SelectedRows.Count.ToString() + " itens selecionados";

                    if (EntradaGrid.SelectedRows.Count >= 10)
                    {
                        MiniSeparator1.Location = new Point(139, MiniSeparator1.Location.Y);
                        DeleteAllSelected.Location = new Point(155, DeleteAllSelected.Location.Y);
                    }

                    else
                    {
                        MiniSeparator1.Location = new Point(134, MiniSeparator1.Location.Y);
                        DeleteAllSelected.Location = new Point(149, DeleteAllSelected.Location.Y);
                    }
                }

                else
                    SelectedOptions.Visible = false;
            }
        }

        // Excluir todos os itens selecionados
        private void DeleteAllSelected_Click(object sender, EventArgs e)
        {
            Frames.DeleteAllSelected.DeleteAllFrame.TmplText.Text = "Excluir itens";
            Frames.DeleteAllSelected.DeleteAllFrame.LblText.Text = "Você deseja mesmo excluir esses itens?";

            List<string> AllNamesDistinct = AllNames.Distinct().ToList();

            ThreadStart ts = new ThreadStart(() => {
                DarkBackground(new Frames.DeleteAllSelected());
            });

            Thread t = new Thread(ts);

            t.SetApartmentState(ApartmentState.STA);

            t.Start();
        }
    }
}
