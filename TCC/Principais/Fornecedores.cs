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

namespace TCC
{
    public partial class Fornecedores : Form
    {
        FormCollection fc = Application.OpenForms;

        Frames.Success SuccessForm = new Frames.Success();
        Frames.Erro ErrorForm = new Frames.Erro();
        Frames.DeleteSelected DeleteSelectedForm = new Frames.DeleteSelected("Fornecedor", ID);
        Frames.DeleteAllSelected DeleteAllSelectedForm = new Frames.DeleteAllSelected();
        Frames.DeleteConfirmation DeleteConfirmationForm = new Frames.DeleteConfirmation("Fornecedores", AllIDs);

        private int CurrentPage = 0;
        int PagesCount = 0;
        int PageRows = Properties.Settings.Default.ItensPorPagina;

        public static int ID;

        int FilteredFornecedores = 0;

        bool NovoFornecedorOpen;
        bool DeleteFornecedorOpen;
        bool EditFornecedorOpen;

        bool ForneceAlgumProduto;

        bool DataFiltered;

        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateButtonsAndPanels = Properties.Settings.Default.AnimarBotoes;
        bool EnableDoubleClickInGrid = Properties.Settings.Default.DoubleClickInGridEnabled;

        bool FormLoaded;

        string SortedByItem = "ID";
        string SortedByOrder = "Ascending";

        int FilterByDays = 0;

        public static List<int> AllIDs = new List<int>();

        List<Guna.UI2.WinForms.Guna2Panel> GunaPanels;
        List<Guna.UI2.WinForms.Guna2Button> GunaMainButtons;
        List<Guna.UI2.WinForms.Guna2Button> GunaButtons;
        List<Guna.UI2.WinForms.Guna2RadioButton> GunaRadioButtons;
        List<Guna.UI2.WinForms.Guna2Separator> GunaSeparators;

        List<Label> NormalLabels;
        List<Label> CustomerInfoLabels;
        List<Label> CustomerInfoPresetLabels;

        public Fornecedores()
        {
            InitializeComponent();

            AddControlsToList();
            SetColor();
        }

        private void Fornecedores_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'allFornecedoresData.Fornecedores'. Você pode movê-la ou removê-la conforme necessário.
            this.fornecedoresTableAdapter1.Fill(this.allFornecedoresData.Fornecedores);
            // TODO: esta linha de código carrega dados na tabela 'fornecedoresData.Fornecedores'. Você pode movê-la ou removê-la conforme necessário.
            this.fornecedoresTableAdapter.Fill(this.fornecedoresData.Fornecedores);

            if (IsDarkModeEnabled)
            {
                FiltroPesquisaBtn.Image = Properties.Resources.search_branco;
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

                TelefonePreset.Image = Properties.Resources.phone_branco;
                CpfPreset.Image = Properties.Resources.cpfcnpj_branco;
                PessoaPreset.Image = Properties.Resources.pessoa_branco;
                TransportadoraPreset.Image = Properties.Resources.fornecedor_branco;
                EntradaPreset.Image = Properties.Resources.data_branco;
                InscEstadualPreset.Image = Properties.Resources.number_branco;
                UltimaCompraPreset.Image = Properties.Resources.data_branco;
                EnderecoPreset.Image = Properties.Resources.home_branco;
                BairroPreset.Image = Properties.Resources.bairro_branco;
                CidadePreset.Image = Properties.Resources.bairro_branco;
                CepPreset.Image = Properties.Resources.cep_branco;
                EstadoPreset.Image = Properties.Resources.estado_branco;
                ComplementoPreset.Image = Properties.Resources.complemento_branco;
                ObservacoesPreset.Image = Properties.Resources.observacoes_branco;
                EmailInfo.Image = Properties.Resources.email_branco;

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
                FiltroPesquisaBtn.Image = Properties.Resources.search_black;
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

                TelefonePreset.Image = Properties.Resources.phone_claro;
                CpfPreset.Image = Properties.Resources.cpfcnpj_claro;
                PessoaPreset.Image = Properties.Resources.pessoa_claro1;
                TransportadoraPreset.Image = Properties.Resources.fornecedor_cinza;
                EntradaPreset.Image = Properties.Resources.entrada_claro;
                InscEstadualPreset.Image = Properties.Resources.number_cinza;
                UltimaCompraPreset.Image = Properties.Resources.entrada_claro;
                EnderecoPreset.Image = Properties.Resources.home_claro;
                BairroPreset.Image = Properties.Resources.bairro_claro;
                CidadePreset.Image = Properties.Resources.bairro_claro;
                CepPreset.Image = Properties.Resources.cep_claro;
                EstadoPreset.Image = Properties.Resources.estado_cinza;
                ComplementoPreset.Image = Properties.Resources.complemento_claro1;
                ObservacoesPreset.Image = Properties.Resources.observacoes_claro;
                EmailInfo.Image = Properties.Resources.email_claro;

                Voltar.Image = Properties.Resources.voltar_claro;
                Editar.Image = Properties.Resources.edit_claro1;

                btnFirst.Image = Properties.Resources.seta_esquerda_dupla_cinza;
                btnBackward.Image = Properties.Resources.seta_esquerda_cinza;
                btnForward.Image = Properties.Resources.seta_direita_cinza;
                btnLast.Image = Properties.Resources.seta_direita_dupla_cinza;

                Crescente.ForeColor = Color.FromArgb(255, 3, 0);

                ToolStripDarkPanel.Visible = false;
            }

            if (fornecedoresData.Fornecedores.Count == 0)
            {
                FornecedoresGrid.Visible = false;
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
                foreach(Guna.UI2.WinForms.Guna2Button ct in GunaButtons)
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

                DadosBtn.Animated = true;
                EnderecoBtn.Animated = true;

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

                DadosBtn.Animated = false;
                EnderecoBtn.Animated = false;

                Voltar.Animated = false;
                Editar.Animated = false;
                Excluir.Animated = false;

                DeleteAllSelected.Animated = true;
            }

            PageItens.Text = Convert.ToString(PageRows);

            PagesCount = Convert.ToInt32(Math.Ceiling(fornecedoresData.Fornecedores.Count * 1.0 / PageRows));
            CurrentPage = 0;
            RefreshPagination();
            ReloadPage();

            foreach (Form frm in fc)
            {
                if (frm.Name == "NovoFornecedor")
                {
                    NovoFornecedor.Enabled = false;
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

        // Esconder frames quando clicar em um espaço no form
        private void Fornecedores_Click(object sender, EventArgs e)
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
            var query = from campos in fornecedoresData.Fornecedores
                        select new
                        {
                            campos.ID,
                            campos.NOME,
                            campos.TELEFONE,
                            campos.CPFCNPJ,
                            campos.ENDERECO,
                            campos.BAIRRO,
                            campos.CEP,
                            campos.ESTADO,
                            campos.COMPLEMENTO,
                            campos.OBSERVACOES,
                            campos.CIDADE,
                            campos.ENTRADA,
                            campos.EMAIL,
                            campos.FOTO
                        };

            fornecedoresBindingSource.DataSource = query.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
            OrderByColumn(SortedByItem);
            PagesCount = Convert.ToInt32(Math.Ceiling(fornecedoresData.Fornecedores.Count * 1.0 / PageRows));
        }

        public void UpdateGrid()
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

            if (SortedByOrder == "Ascending")
            {
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM Fornecedores ORDER BY [" + SortedByItem + "] ASC", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    HideNewFornecedorItens();

                    DataTable customrows = dt.AsEnumerable()
                    .Skip(PageRows * CurrentPage)
                    .Take(PageRows)
                    .ToList()
                    .CopyToDataTable();

                    // Fornecedores data
                    fornecedoresBindingSource.DataSource = customrows;
                    fornecedoresTableAdapter.Fill(fornecedoresData.Fornecedores);

                    // All fornecedores data
                    fornecedoresBindingSource1.DataSource = customrows;
                    fornecedoresTableAdapter1.Fill(allFornecedoresData.Fornecedores);

                    OrderByColumn(SortedByItem);
                    PagesCount = Convert.ToInt32(Math.Ceiling(fornecedoresData.Fornecedores.Count * 1.0 / PageRows));
                    con.Close();
                }
                else
                {
                    FornecedoresGrid.Visible = false;
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

                    NovoFornecedor.Visible = true;
                }
            }

            else if (SortedByOrder == "Descending")
            {
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM Fornecedores ORDER BY [" + SortedByItem + "] DESC", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    HideNewFornecedorItens();

                    DataTable customrows = dt.AsEnumerable()
                    .Skip(PageRows * CurrentPage)
                    .Take(PageRows)
                    .ToList()
                    .CopyToDataTable();

                    // Fornecedores data
                    fornecedoresBindingSource.DataSource = customrows;
                    fornecedoresTableAdapter.Fill(fornecedoresData.Fornecedores);

                    // All fornecedores data
                    fornecedoresBindingSource1.DataSource = customrows;
                    fornecedoresTableAdapter1.Fill(allFornecedoresData.Fornecedores);

                    OrderByColumn(SortedByItem);
                    PagesCount = Convert.ToInt32(Math.Ceiling(fornecedoresData.Fornecedores.Count * 1.0 / PageRows));
                    con.Close();
                }
                else
                {
                    FornecedoresGrid.Visible = false;
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

                    NovoFornecedor.Visible = true;
                }

                OrderByColumn(SortedByItem);
                PagesCount = Convert.ToInt32(Math.Ceiling(fornecedoresData.Fornecedores.Count * 1.0 / PageRows));
                con.Close();
            }

            RefreshPagination();

            Editar.Location = new Point(Editar.Location.X, 5557);
            Excluir.Location = new Point(Excluir.Location.X, 5557);
            Voltar.Location = new Point(Voltar.Location.X, 5557);

            NovoFornecedor.Visible = true;
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

        // Filtrar data
        private void DateFunction(DateTime data)
        {
            var query = from campos in fornecedoresData.Fornecedores
                        where campos.ENTRADA >= data
                        orderby campos.ENTRADA <= campos.ENTRADA

                        select campos;

            FilteredFornecedores = query.Count();

            fornecedoresBindingSource.DataSource = query.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
            fornecedoresBindingSource1.DataSource = query;
            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));

            RefreshPagination();
            NotFinded();
        }

        // Filtrar por data especifica
        private void SpecificDateFunction(DateTime data1, DateTime data2)
        {
            var query = from campos in fornecedoresData.Fornecedores
                        where campos.ENTRADA >= data1 && campos.ENTRADA <= data2
                        orderby campos.ENTRADA <= campos.ENTRADA

                        select campos;

            FilteredFornecedores = query.Count();

            fornecedoresBindingSource.DataSource = query.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
            fornecedoresBindingSource1.DataSource = query;
            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));

            RefreshPagination();
            NotFinded();
        }

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
                ColorFilter(ActiveFilter2);
            else
            {
                if (All.Checked != true)
                    RemoverFiltros.Visible = false;
            }

            OrderByColumn(SortedByItem);
            HideFrames();
        }

        /*--------------------------*/

        // Selecionar item pra classificar
        private void SortItens(object sender, EventArgs e)
        {
            if (IDSort.Checked)
                SortedByItem = "ID";

            else if (NameSort.Checked)
                SortedByItem = "Nome";

            else if (TelefoneSort.Checked)
                SortedByItem = "Telefone";

            else if (CPFSort.Checked)
                SortedByItem = "CpfCnpj";

            else if (InscEstadualSort.Checked)
                SortedByItem = "InscEstadual";

            else if (EnderecoSort.Checked)
                SortedByItem = "Endereco";

            else if (CidadeSort.Checked)
                SortedByItem = "Cidade";

            else if (EntradaSort.Checked)
                SortedByItem = "Entrada";
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
                        case "ID":
                            var IDQuery = from campos in fornecedoresData.Fornecedores
                                          orderby campos.ID descending
                                          where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                          select campos;

                            fornecedoresBindingSource.DataSource = IDQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = IDQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Nome":
                            var NomeQuery = from campos in fornecedoresData.Fornecedores
                                            orderby campos.NOME descending
                                            where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                            select campos;

                            fornecedoresBindingSource.DataSource = NomeQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = NomeQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Telefone":
                            var TelefoneQuery = from campos in fornecedoresData.Fornecedores
                                                orderby campos.TELEFONE descending
                                                where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                select campos;

                            fornecedoresBindingSource.DataSource = TelefoneQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = TelefoneQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "CpfCnpj":
                            var CpfQuery = from campos in fornecedoresData.Fornecedores
                                           orderby campos.NOME descending
                                           where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                           select campos;

                            fornecedoresBindingSource.DataSource = CpfQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = CpfQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "InscEstadual":
                            var InscEstadualQuery = from campos in fornecedoresData.Fornecedores
                                              orderby campos.INSCESTADUAL descending
                                              where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                              select campos;

                            fornecedoresBindingSource.DataSource = InscEstadualQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = InscEstadualQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Endereco":
                            var EnderecoQuery = from campos in fornecedoresData.Fornecedores
                                                orderby campos.ENDERECO descending
                                                where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                select campos;

                            fornecedoresBindingSource.DataSource = EnderecoQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = EnderecoQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Cidade":
                            var CidadeQuery = from campos in fornecedoresData.Fornecedores
                                              orderby campos.CIDADE descending
                                              where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                              select campos;

                            fornecedoresBindingSource.DataSource = CidadeQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = CidadeQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Entrada":
                            var EntradaQuery = from campos in fornecedoresData.Fornecedores
                                               orderby campos.ENTRADA >= campos.ENTRADA
                                               where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                               select campos;

                            fornecedoresBindingSource.DataSource = EntradaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = EntradaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;
                    }
                }

                else if (SortedByOrder == "Ascending")
                {
                    switch (Campo)
                    {
                        case "ID":
                            var IDQuery = from campos in fornecedoresData.Fornecedores
                                          orderby campos.ID ascending
                                          where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                          select campos;

                            fornecedoresBindingSource.DataSource = IDQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = IDQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Nome":
                            var NomeQuery = from campos in fornecedoresData.Fornecedores
                                            orderby campos.NOME ascending
                                            where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                            select campos;

                            fornecedoresBindingSource.DataSource = NomeQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = NomeQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Telefone":
                            var TelefoneQuery = from campos in fornecedoresData.Fornecedores
                                                orderby campos.TELEFONE ascending
                                                where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                select campos;

                            fornecedoresBindingSource.DataSource = TelefoneQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = TelefoneQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "CpfCnpj":
                            var CpfQuery = from campos in fornecedoresData.Fornecedores
                                           orderby campos.NOME ascending
                                           where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                           select campos;

                            fornecedoresBindingSource.DataSource = CpfQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = CpfQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "InscEstadual":
                            var InscEstadualQuery = from campos in fornecedoresData.Fornecedores
                                              orderby campos.INSCESTADUAL ascending
                                              where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                              select campos;

                            fornecedoresBindingSource.DataSource = InscEstadualQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = InscEstadualQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Endereco":
                            var EnderecoQuery = from campos in fornecedoresData.Fornecedores
                                                orderby campos.ENDERECO ascending
                                                where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                                select campos;

                            fornecedoresBindingSource.DataSource = EnderecoQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = EnderecoQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Cidade":
                            var CidadeQuery = from campos in fornecedoresData.Fornecedores
                                              orderby campos.CIDADE ascending
                                              where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                              select campos;

                            fornecedoresBindingSource.DataSource = CidadeQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = CidadeQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Entrada":
                            var EntradaQuery = from campos in fornecedoresData.Fornecedores
                                               orderby campos.ENTRADA <= campos.ENTRADA
                                               where campos.ENTRADA >= Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)) && campos.ENTRADA <= Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"))

                                               select campos;

                            fornecedoresBindingSource.DataSource = EntradaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = EntradaQuery;
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
                        case "ID":
                            var IDQuery = from campos in fornecedoresData.Fornecedores
                                          orderby campos.ID descending

                                          select campos;

                            fornecedoresBindingSource.DataSource = IDQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = IDQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Nome":
                            var NomeQuery = from campos in fornecedoresData.Fornecedores
                                            orderby campos.NOME descending

                                            select campos;

                            fornecedoresBindingSource.DataSource = NomeQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = NomeQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Telefone":
                            var TelefoneQuery = from campos in fornecedoresData.Fornecedores
                                                orderby campos.TELEFONE descending

                                                select campos;

                            fornecedoresBindingSource.DataSource = TelefoneQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = TelefoneQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Cpf":
                            var CpfQuery = from campos in fornecedoresData.Fornecedores
                                           orderby campos.NOME descending

                                           select campos;

                            fornecedoresBindingSource.DataSource = CpfQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = CpfQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "InscEstadual":
                            var InscEstadualQuery = from campos in fornecedoresData.Fornecedores
                                              orderby campos.INSCESTADUAL descending

                                              select campos;

                            fornecedoresBindingSource.DataSource = InscEstadualQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = InscEstadualQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Endereço":
                            var EnderecoQuery = from campos in fornecedoresData.Fornecedores
                                                orderby campos.ENDERECO descending

                                                select campos;

                            fornecedoresBindingSource.DataSource = EnderecoQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = EnderecoQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Cidade":
                            var CidadeQuery = from campos in fornecedoresData.Fornecedores
                                              orderby campos.CIDADE descending

                                              select campos;

                            fornecedoresBindingSource.DataSource = CidadeQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = CidadeQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Entrada":
                            var EntradaQuery = from campos in fornecedoresData.Fornecedores
                                               orderby campos.ENTRADA >= campos.ENTRADA

                                               select campos;

                            fornecedoresBindingSource.DataSource = EntradaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = EntradaQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;
                    }
                }
                else if (SortedByOrder == "Ascending")
                {
                    switch (Campo)
                    {
                        case "ID":
                            var IDQuery = from campos in fornecedoresData.Fornecedores
                                          orderby campos.ID ascending

                                          select campos;

                            fornecedoresBindingSource.DataSource = IDQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = IDQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Nome":
                            var NomeQuery = from campos in fornecedoresData.Fornecedores
                                            orderby campos.NOME ascending

                                            select campos;

                            fornecedoresBindingSource.DataSource = NomeQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = NomeQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Telefone":
                            var TelefoneQuery = from campos in fornecedoresData.Fornecedores
                                                orderby campos.TELEFONE ascending

                                                select campos;

                            fornecedoresBindingSource.DataSource = TelefoneQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = TelefoneQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Cpf":
                            var CpfQuery = from campos in fornecedoresData.Fornecedores
                                           orderby campos.NOME ascending

                                           select campos;

                            fornecedoresBindingSource.DataSource = CpfQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = CpfQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "InscEstadual":
                            var InscEstadualQuery = from campos in fornecedoresData.Fornecedores
                                              orderby campos.INSCESTADUAL ascending

                                              select campos;

                            fornecedoresBindingSource.DataSource = InscEstadualQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = InscEstadualQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Endereço":
                            var EnderecoQuery = from campos in fornecedoresData.Fornecedores
                                                orderby campos.ENDERECO ascending

                                                select campos;

                            fornecedoresBindingSource.DataSource = EnderecoQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = EnderecoQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Cidade":
                            var CidadeQuery = from campos in fornecedoresData.Fornecedores
                                              orderby campos.CIDADE ascending

                                              select campos;

                            fornecedoresBindingSource.DataSource = CidadeQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = CidadeQuery;
                            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));
                            break;

                        case "Entrada":
                            var EntradaQuery = from campos in fornecedoresData.Fornecedores
                                               orderby campos.ENTRADA <= campos.ENTRADA

                                               select campos;

                            fornecedoresBindingSource.DataSource = EntradaQuery.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
                            fornecedoresBindingSource1.DataSource = EntradaQuery;
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
            foreach (DataGridViewColumn column in FornecedoresGrid.Columns)
            {
                if (IsDarkModeEnabled != true)
                    column.HeaderCell.Style.ForeColor = Color.FromArgb(80, 80, 80);
                else
                    column.HeaderCell.Style.ForeColor = Color.FromArgb(255, 255, 255);

                column.ToolTipText = "";

                FornecedoresGrid.Columns[0].HeaderText = "ID";
                FornecedoresGrid.Columns[1].HeaderText = "NOME";
                FornecedoresGrid.Columns[2].HeaderText = "TELEFONE";
                FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ";
                FornecedoresGrid.Columns[4].HeaderText = "PESSOA";
                FornecedoresGrid.Columns[5].HeaderText = "INSC. EST.";
                FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO";
                FornecedoresGrid.Columns[8].HeaderText = "CIDADE";
                FornecedoresGrid.Columns[9].HeaderText = "ENTRADA";
            }

            if (SortedByOrder == "Ascending")
            {
                if (All.Checked)
                {
                    foreach (DataGridViewColumn column in FornecedoresGrid.Columns)
                    {
                        if (IsDarkModeEnabled != true)
                            column.HeaderCell.Style.ForeColor = Color.FromArgb(80, 80, 80);
                        else
                            column.HeaderCell.Style.ForeColor = Color.FromArgb(255, 255, 255);

                        if (IDSort.Checked)
                        {
                            FornecedoresGrid.Columns[0].HeaderText = "ID ↑";
                            FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                        }
                        else if (NameSort.Checked)
                        {
                            FornecedoresGrid.Columns[1].HeaderText = "NOME ↑";
                            FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                        }
                        else if (TelefoneSort.Checked)
                        {
                            FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↑";
                            FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                        }
                        else if (CPFSort.Checked)
                        {
                            FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↑";
                            FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o maior";
                        }
                        else if (PessoaSort.Checked)
                        {
                            FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↑";
                            FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                        }
                        else if (InscEstadualSort.Checked)
                        {
                            FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↑";
                            FornecedoresGrid.Columns[5].ToolTipText = "Classificado: A-Z";
                        }
                        else if (EnderecoSort.Checked)
                        {
                            FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↑";
                            FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                        }
                        else if (CidadeSort.Checked)
                        {
                            FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↑";
                            FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                        }
                        else if (EntradaSort.Checked)
                        {
                            FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↑";
                            FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                        }
                    }
                }

                else if (Id.Checked)
                {
                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID* ↑";
                        FornecedoresGrid.Columns[0].HeaderCell.Style.ForeColor = Color.FromArgb(255, 3, 0);
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID*";
                        FornecedoresGrid.Columns[0].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↑";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↑";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↑";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↑";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↑";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↑";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↑";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↑";
                        FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                }

                else if (Nome.Checked)
                {
                    if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME* ↑";
                        FornecedoresGrid.Columns[1].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME*";
                        FornecedoresGrid.Columns[1].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↑";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↑";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↑";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↑";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↑";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↑";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↑";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↑";
                        FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                }

                else if (Telefone.Checked)
                {
                    if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE* ↑";
                        FornecedoresGrid.Columns[2].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE*";
                        FornecedoresGrid.Columns[2].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↑";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↑";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↑";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↑";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↑";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↑";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↑";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↑";
                        FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                }

                else if (Cpf.Checked)
                {
                    if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ* ↑";
                        FornecedoresGrid.Columns[3].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ*";
                        FornecedoresGrid.Columns[3].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↑";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↑";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↑";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↑";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↑";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↑";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↑";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↑";
                        FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                }

                else if (Pessoa.Checked)
                {
                    if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA* ↑";
                        FornecedoresGrid.Columns[4].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA*";
                        FornecedoresGrid.Columns[4].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↑";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↑";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↑";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↑";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↑";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↑";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↑";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↑";
                        FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                }

                else if (InscEstadual.Checked)
                {
                    if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST.* ↑";
                        FornecedoresGrid.Columns[5].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST.*";
                        FornecedoresGrid.Columns[5].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↑";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↑";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↑";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↑";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↑";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↑";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↑";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↑";
                        FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                }

                else if (Endereco.Checked)
                {
                    if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO* ↑";
                        FornecedoresGrid.Columns[6].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO*";
                        FornecedoresGrid.Columns[6].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↑";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↑";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↑";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↑";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↑";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↑";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↑";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↑";
                        FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                }

                else if (Cidade.Checked)
                {
                    if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "CIDADE* ↑";
                        FornecedoresGrid.Columns[8].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "CIDADE*";
                        FornecedoresGrid.Columns[8].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↑";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↑";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↑";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↑";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↑";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↑";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↑";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↑";
                        FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                }

                else if (Entrada.Checked)
                {
                    if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[9].HeaderText = "ENTRADA* ↑";
                        FornecedoresGrid.Columns[9].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[9].HeaderText = "ENTRADA*";
                        FornecedoresGrid.Columns[9].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↑";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↑";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↑";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↑";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↑";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↑";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↑";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↑";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                }
            }

            if (SortedByOrder == "Descending")
            {
                if (All.Checked)
                {
                    foreach (DataGridViewColumn column in FornecedoresGrid.Columns)
                    {
                        if (IsDarkModeEnabled != true)
                            column.HeaderCell.Style.ForeColor = Color.FromArgb(80, 80, 80);
                        else
                            column.HeaderCell.Style.ForeColor = Color.FromArgb(255, 255, 255);

                        if (IDSort.Checked)
                        {
                            FornecedoresGrid.Columns[0].HeaderText = "ID ↓";
                            FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                        }
                        else if (NameSort.Checked)
                        {
                            FornecedoresGrid.Columns[1].HeaderText = "NOME ↓";
                            FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                        }
                        else if (TelefoneSort.Checked)
                        {
                            FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↓";
                            FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                        }
                        else if (CPFSort.Checked)
                        {
                            FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↓";
                            FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Maior para o menor";
                        }
                        else if (PessoaSort.Checked)
                        {
                            FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↓";
                            FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                        }
                        else if (InscEstadualSort.Checked)
                        {
                            FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↓";
                            FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Z-A";
                        }
                        else if (EnderecoSort.Checked)
                        {
                            FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↓";
                            FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                        }
                        else if (CidadeSort.Checked)
                        {
                            FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↓";
                            FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                        }
                        else if (EntradaSort.Checked)
                        {
                            FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↓";
                            FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                        }
                    }
                }

                else if (Id.Checked)
                {
                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID* ↓";
                        FornecedoresGrid.Columns[0].HeaderCell.Style.ForeColor = Color.FromArgb(255, 3, 0);
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID*";
                        FornecedoresGrid.Columns[0].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↓";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↓";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↓";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↓";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↓";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↓";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↓";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↓";
                        FornecedoresGrid.Columns[9].ToolTipText = "Classificado: Mais nova para a mais entrada";
                    }
                }

                else if (Nome.Checked)
                {
                    if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME* ↓";
                        FornecedoresGrid.Columns[1].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME*";
                        FornecedoresGrid.Columns[1].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↓";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↓";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↓";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↓";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↓";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↓";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↓";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↓";
                        FornecedoresGrid.Columns[9].ToolTipText = "Classificado: Mais nova para a mais entrada";
                    }
                }

                else if (Telefone.Checked)
                {
                    if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE* ↓";
                        FornecedoresGrid.Columns[2].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE*";
                        FornecedoresGrid.Columns[2].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↓";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↓";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↓";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↓";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↓";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↓";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↓";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↓";
                        FornecedoresGrid.Columns[9].ToolTipText = "Classificado: Mais nova para a mais entrada";
                    }
                }

                else if (Cpf.Checked)
                {
                    if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ* ↓";
                        FornecedoresGrid.Columns[3].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ*";
                        FornecedoresGrid.Columns[3].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↓";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↓";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↓";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↓";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↓";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↓";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↓";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↓";
                        FornecedoresGrid.Columns[9].ToolTipText = "Classificado: Mais nova para a mais entrada";
                    }
                }

                else if (Pessoa.Checked)
                {
                    if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA* ↓";
                        FornecedoresGrid.Columns[4].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA*";
                        FornecedoresGrid.Columns[4].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↓";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↓";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↓";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↓";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↓";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↓";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↓";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↓";
                        FornecedoresGrid.Columns[9].ToolTipText = "Classificado: Mais nova para a mais entrada";
                    }
                }

                else if (InscEstadual.Checked)
                {
                    if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST.* ↓";
                        FornecedoresGrid.Columns[5].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST.*";
                        FornecedoresGrid.Columns[5].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↓";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↓";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↓";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↓";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↓";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↓";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↓";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↓";
                        FornecedoresGrid.Columns[9].ToolTipText = "Classificado: Mais nova para a mais entrada";
                    }
                }

                else if (Endereco.Checked)
                {
                    if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO* ↓";
                        FornecedoresGrid.Columns[6].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO*";
                        FornecedoresGrid.Columns[6].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↓";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↓";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↓";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↓";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↓";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↓";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↓";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↓";
                        FornecedoresGrid.Columns[9].ToolTipText = "Classificado: Mais nova para a mais entrada";
                    }
                }

                else if (Cidade.Checked)
                {
                    if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "CIDADE* ↓";
                        FornecedoresGrid.Columns[8].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "CIDADE*";
                        FornecedoresGrid.Columns[8].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↓";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↓";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↓";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↓";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↓";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↓";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↓";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↓";
                        FornecedoresGrid.Columns[9].ToolTipText = "Classificado: Mais nova para a mais entrada";
                    }
                }

                else if (Entrada.Checked)
                {
                    if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[9].HeaderText = "ENTRADA* ↓";
                        FornecedoresGrid.Columns[9].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[9].ToolTipText = "Classificado: Mais nova para a mais entrada";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[9].HeaderText = "ENTRADA*";
                        FornecedoresGrid.Columns[9].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↓";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↓";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↓";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↓";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↓";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↓";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↓";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↓";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                }
            }

            if (FormLoaded)
                SearchOptions.Location = new Point(5512, SearchOptions.Location.Y);

            HideFrames();
        }

        private void CheckedStateFunction()
        {
            foreach (DataGridViewColumn column in FornecedoresGrid.Columns)
            {
                if (IsDarkModeEnabled != true)
                    column.HeaderCell.Style.ForeColor = Color.FromArgb(80, 80, 80);
                else
                    column.HeaderCell.Style.ForeColor = Color.FromArgb(255, 255, 255);

                column.ToolTipText = "";

                FornecedoresGrid.Columns[0].HeaderText = "ID";
                FornecedoresGrid.Columns[1].HeaderText = "NOME";
                FornecedoresGrid.Columns[2].HeaderText = "TELEFONE";
                FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ";
                FornecedoresGrid.Columns[4].HeaderText = "PESSOA";
                FornecedoresGrid.Columns[5].HeaderText = "INSC. EST.";
                FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO";
                FornecedoresGrid.Columns[7].HeaderText = "CIDADE";
                FornecedoresGrid.Columns[8].HeaderText = "ENTRADA";
            }

            if (SortedByOrder == "Ascending")
            {
                if (All.Checked)
                {
                    foreach (DataGridViewColumn column in FornecedoresGrid.Columns)
                    {
                        if (IsDarkModeEnabled != true)
                            column.HeaderCell.Style.ForeColor = Color.FromArgb(80, 80, 80);
                        else
                            column.HeaderCell.Style.ForeColor = Color.FromArgb(255, 255, 255);

                        if (IDSort.Checked)
                        {
                            FornecedoresGrid.Columns[0].HeaderText = "ID ↑";
                            FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                        }
                        else if (NameSort.Checked)
                        {
                            FornecedoresGrid.Columns[1].HeaderText = "NOME ↑";
                            FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                        }
                        else if (TelefoneSort.Checked)
                        {
                            FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↑";
                            FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                        }
                        else if (CPFSort.Checked)
                        {
                            FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↑";
                            FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                        }
                        else if (PessoaSort.Checked)
                        {
                            FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↑";
                            FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                        }
                        else if (InscEstadualSort.Checked)
                        {
                            FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↑";
                            FornecedoresGrid.Columns[5].ToolTipText = "Classificado: A-Z";
                        }
                        else if (EnderecoSort.Checked)
                        {
                            FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↑";
                            FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                        }
                        else if (CidadeSort.Checked)
                        {
                            FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↑";
                            FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                        }
                        else if (EntradaSort.Checked)
                        {
                            FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↑";
                            FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                        }
                    }
                }

                else if (Id.Checked)
                {
                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID* ↑";
                        FornecedoresGrid.Columns[0].HeaderCell.Style.ForeColor = Color.FromArgb(255, 3, 0);
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID*";
                        FornecedoresGrid.Columns[0].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↑";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↑";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↑";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↑";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↑";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↑";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↑";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↑";
                        FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                }

                else if (Nome.Checked)
                {
                    if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME* ↑";
                        FornecedoresGrid.Columns[1].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME*";
                        FornecedoresGrid.Columns[1].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↑";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↑";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↑";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↑";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↑";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↑";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↑";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↑";
                        FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                }

                else if (Telefone.Checked)
                {
                    if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE* ↑";
                        FornecedoresGrid.Columns[2].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE*";
                        FornecedoresGrid.Columns[2].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↑";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↑";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↑";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↑";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↑";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↑";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↑";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↑";
                        FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                }

                else if (Cpf.Checked)
                {
                    if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ* ↑";
                        FornecedoresGrid.Columns[3].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ*";
                        FornecedoresGrid.Columns[3].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↑";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↑";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↑";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↑";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↑";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↑";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↑";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↑";
                        FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                }

                else if (Pessoa.Checked)
                {
                    if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA* ↑";
                        FornecedoresGrid.Columns[4].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA*";
                        FornecedoresGrid.Columns[4].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↑";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↑";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↑";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↑";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↑";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↑";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↑";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↑";
                        FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                }

                else if (InscEstadual.Checked)
                {
                    if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST.* ↑";
                        FornecedoresGrid.Columns[5].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST.*";
                        FornecedoresGrid.Columns[5].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↑";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↑";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↑";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↑";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↑";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↑";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↑";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↑";
                        FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                }

                else if (Endereco.Checked)
                {
                    if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO* ↑";
                        FornecedoresGrid.Columns[6].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO*";
                        FornecedoresGrid.Columns[6].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↑";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↑";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↑";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↑";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↑";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↑";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↑";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↑";
                        FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                }

                else if (Cidade.Checked)
                {
                    if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "CIDADE* ↑";
                        FornecedoresGrid.Columns[8].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "CIDADE*";
                        FornecedoresGrid.Columns[8].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↑";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↑";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↑";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↑";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↑";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↑";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↑";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↑";
                        FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                }

                else if (Entrada.Checked)
                {
                    if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[9].HeaderText = "ENTRADA* ↑";
                        FornecedoresGrid.Columns[9].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[9].HeaderText = "ENTRADA*";
                        FornecedoresGrid.Columns[9].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↑";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Menor para o maior";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↑";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: A-Z";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↑";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↑";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↑";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: A-Z";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↑";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: A-Z";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↑";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: A-Z";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↑";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: A-Z";
                    }
                }
            }

            else if (SortedByOrder == "Descending")
            {
                if (All.Checked)
                {
                    foreach (DataGridViewColumn column in FornecedoresGrid.Columns)
                    {
                        if (IsDarkModeEnabled != true)
                            column.HeaderCell.Style.ForeColor = Color.FromArgb(80, 80, 80);
                        else
                            column.HeaderCell.Style.ForeColor = Color.FromArgb(255, 255, 255);

                        if (IDSort.Checked)
                        {
                            FornecedoresGrid.Columns[0].HeaderText = "ID ↓";
                            FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                        }
                        else if (NameSort.Checked)
                        {
                            FornecedoresGrid.Columns[1].HeaderText = "NOME ↓";
                            FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                        }
                        else if (TelefoneSort.Checked)
                        {
                            FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↓";
                            FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                        }
                        else if (CPFSort.Checked)
                        {
                            FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↓";
                            FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                        }
                        else if (PessoaSort.Checked)
                        {
                            FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↓";
                            FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                        }
                        else if (InscEstadualSort.Checked)
                        {
                            FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↓";
                            FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Z-A";
                        }
                        else if (EnderecoSort.Checked)
                        {
                            FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↓";
                            FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                        }
                        else if (CidadeSort.Checked)
                        {
                            FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↓";
                            FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                        }
                        else if (EntradaSort.Checked)
                        {
                            FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↓";
                            FornecedoresGrid.Columns[8].ToolTipText = "Classificado: Mais antiga para a mais nova";
                        }
                    }
                }

                else if (Id.Checked)
                {
                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID* ↓";
                        FornecedoresGrid.Columns[0].HeaderCell.Style.ForeColor = Color.FromArgb(255, 3, 0);
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID*";
                        FornecedoresGrid.Columns[0].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↓";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↓";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↓";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↓";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↓";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↓";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↓";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↓";
                        FornecedoresGrid.Columns[9].ToolTipText = "Classificado: Mais nova para a mais entrada";
                    }
                }

                else if (Nome.Checked)
                {
                    if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME* ↓";
                        FornecedoresGrid.Columns[1].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME*";
                        FornecedoresGrid.Columns[1].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↓";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↓";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↓";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↓";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↓";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↓";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↓";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↓";
                        FornecedoresGrid.Columns[9].ToolTipText = "Classificado: Mais nova para a mais entrada";
                    }
                }

                else if (Telefone.Checked)
                {
                    if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE* ↓";
                        FornecedoresGrid.Columns[2].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE*";
                        FornecedoresGrid.Columns[2].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↓";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↓";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↓";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↓";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↓";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↓";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↓";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↓";
                        FornecedoresGrid.Columns[9].ToolTipText = "Classificado: Mais nova para a mais entrada";
                    }
                }

                else if (Cpf.Checked)
                {
                    if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ* ↓";
                        FornecedoresGrid.Columns[3].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ*";
                        FornecedoresGrid.Columns[3].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↓";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↓";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↓";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↓";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↓";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↓";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↓";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↓";
                        FornecedoresGrid.Columns[9].ToolTipText = "Classificado: Mais nova para a mais entrada";
                    }
                }

                else if (Pessoa.Checked)
                {
                    if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA* ↓";
                        FornecedoresGrid.Columns[4].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA*";
                        FornecedoresGrid.Columns[4].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↓";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↓";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↓";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↓";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↓";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↓";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↓";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↓";
                        FornecedoresGrid.Columns[9].ToolTipText = "Classificado: Mais nova para a mais entrada";
                    }
                }

                else if (InscEstadual.Checked)
                {
                    if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST.* ↓";
                        FornecedoresGrid.Columns[5].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST.*";
                        FornecedoresGrid.Columns[5].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↓";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↓";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↓";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↓";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↓";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↓";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↓";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↓";
                        FornecedoresGrid.Columns[9].ToolTipText = "Classificado: Mais nova para a mais entrada";
                    }
                }

                else if (Endereco.Checked)
                {
                    if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO* ↓";
                        FornecedoresGrid.Columns[6].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO*";
                        FornecedoresGrid.Columns[6].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↓";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↓";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↓";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↓";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↓";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↓";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↓";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↓";
                        FornecedoresGrid.Columns[9].ToolTipText = "Classificado: Mais nova para a mais entrada";
                    }
                }

                else if (Cidade.Checked)
                {
                    if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "CIDADE* ↓";
                        FornecedoresGrid.Columns[8].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "CIDADE*";
                        FornecedoresGrid.Columns[8].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↓";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↓";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↓";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↓";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↓";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↓";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↓";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[8].HeaderText = "ENTRADA ↓";
                        FornecedoresGrid.Columns[9].ToolTipText = "Classificado: Mais nova para a mais entrada";
                    }
                }

                else if (Entrada.Checked)
                {
                    if (EntradaSort.Checked)
                    {
                        FornecedoresGrid.Columns[9].HeaderText = "ENTRADA* ↓";
                        FornecedoresGrid.Columns[9].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                        FornecedoresGrid.Columns[9].ToolTipText = "Classificado: Mais nova para a mais entrada";
                    }
                    else
                    {
                        FornecedoresGrid.Columns[9].HeaderText = "ENTRADA*";
                        FornecedoresGrid.Columns[9].HeaderCell.Style.ForeColor = ThemeManager.RedFontColor;
                    }

                    if (IDSort.Checked)
                    {
                        FornecedoresGrid.Columns[0].HeaderText = "ID ↓";
                        FornecedoresGrid.Columns[0].ToolTipText = "Classificado: Maior para o menor";
                    }
                    else if (NameSort.Checked)
                    {
                        FornecedoresGrid.Columns[1].HeaderText = "NOME ↓";
                        FornecedoresGrid.Columns[1].ToolTipText = "Classificado: Z-A";
                    }
                    else if (TelefoneSort.Checked)
                    {
                        FornecedoresGrid.Columns[2].HeaderText = "TELEFONE ↓";
                        FornecedoresGrid.Columns[2].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CPFSort.Checked)
                    {
                        FornecedoresGrid.Columns[3].HeaderText = "CPF/CNPJ ↓";
                        FornecedoresGrid.Columns[3].ToolTipText = "Classificado: Menor para o menor";
                    }
                    else if (PessoaSort.Checked)
                    {
                        FornecedoresGrid.Columns[4].HeaderText = "PESSOA ↓";
                        FornecedoresGrid.Columns[4].ToolTipText = "Classificado: Z-A";
                    }
                    else if (InscEstadualSort.Checked)
                    {
                        FornecedoresGrid.Columns[5].HeaderText = "INSC. EST. ↓";
                        FornecedoresGrid.Columns[5].ToolTipText = "Classificado: Z-A";
                    }
                    else if (EnderecoSort.Checked)
                    {
                        FornecedoresGrid.Columns[6].HeaderText = "ENDEREÇO ↓";
                        FornecedoresGrid.Columns[6].ToolTipText = "Classificado: Z-A";
                    }
                    else if (CidadeSort.Checked)
                    {
                        FornecedoresGrid.Columns[7].HeaderText = "CIDADE ↓";
                        FornecedoresGrid.Columns[7].ToolTipText = "Classificado: Z-A";
                    }
                }
            }

            if (FormLoaded)
                SearchOptions.Location = new Point(5512, SearchOptions.Location.Y);

            HideFrames();
        }

        /*--------------------------*/

        // Filtro de pesquisa
        private void SearchFilter()
        {
            DataView dv = fornecedoresData.Fornecedores.DefaultView;

            if (DataFiltered)
            {
                if (All.Checked)
                {
                    dv.RowFilter = string.Format("convert (ID, 'System.String') LIKE '%" + Search.Text + "%' OR NOME LIKE '%" + Search.Text + "%' OR TELEFONE LIKE '%" + Search.Text + "%' OR CPFCNPJ LIKE '%" + Search.Text + "%' \n" +
                        "OR PESSOA LIKE '%" + Search.Text + "%' OR ENDERECO LIKE '%" + Search.Text + "%' OR CIDADE LIKE '%" + Search.Text + "%' AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'");
                    fornecedoresBindingSource.DataSource = dv;
                    fornecedoresBindingSource1.DataSource = dv;
                }

                else if (Id.Checked)
                {
                    dv.RowFilter = "convert(ID, 'System.String') LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    fornecedoresBindingSource.DataSource = dv;
                    fornecedoresBindingSource1.DataSource = dv;
                }

                else if (Nome.Checked)
                {
                    dv.RowFilter = "NOME LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    fornecedoresBindingSource.DataSource = dv;
                    fornecedoresBindingSource1.DataSource = dv;
                }

                else if (Telefone.Checked)
                {
                    dv.RowFilter = "TELEFONE LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    fornecedoresBindingSource.DataSource = dv;
                    fornecedoresBindingSource1.DataSource = dv;
                }

                else if (Cpf.Checked)
                {
                    dv.RowFilter = "CPFCNPJ LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    fornecedoresBindingSource.DataSource = dv;
                    fornecedoresBindingSource1.DataSource = dv;
                }

                else if (InscEstadual.Checked)
                {
                    dv.RowFilter = "PESSOA LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    fornecedoresBindingSource.DataSource = dv;
                    fornecedoresBindingSource1.DataSource = dv;
                }

                else if (Endereco.Checked)
                {
                    dv.RowFilter = "ENDERECO LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    fornecedoresBindingSource.DataSource = dv;
                    fornecedoresBindingSource1.DataSource = dv;
                }

                else if (Cidade.Checked)
                {
                    dv.RowFilter = "CIDADE LIKE '%" + Search.Text + "%' " +
                        "AND ENTRADA >= '" + Convert.ToDateTime(DateTime.Now.AddDays(-FilterByDays)).ToString("dd-MM-yyyy") + "' AND ENTRADA <= '" + Convert.ToDateTime(DateTime.Today).ToString("dd-MM-yyyy") + "'";
                    fornecedoresBindingSource.DataSource = dv;
                    fornecedoresBindingSource1.DataSource = dv;
                }

                else if (Entrada.Checked)
                {
                    dv.RowFilter = "ENTRADA LIKE '" + Convert.ToDateTime(Search.Text).ToString("dd-MM-yyyy") + "'";
                    fornecedoresBindingSource.DataSource = dv;
                    fornecedoresBindingSource1.DataSource = dv;
                }

            }
            else
            {
                if (All.Checked)
                {
                    dv.RowFilter = string.Format("convert (ID, 'System.String') LIKE '%" + Search.Text + "%' OR NOME LIKE '%" + Search.Text + "%' OR TELEFONE LIKE '%" + Search.Text + "%' OR CPFCNPJ LIKE '%" + Search.Text + "%' \n" +
                        "OR PESSOA LIKE '%" + Search.Text + "%' OR ENDERECO LIKE '%" + Search.Text + "%' OR CIDADE LIKE '%" + Search.Text + "%'");
                    fornecedoresBindingSource.DataSource = dv;
                    fornecedoresBindingSource1.DataSource = dv;
                }

                else if (Id.Checked)
                {
                    dv.RowFilter = string.Format("convert (ID, 'System.String') LIKE '%" + Search.Text + "%'");
                    fornecedoresBindingSource.DataSource = dv;
                    fornecedoresBindingSource1.DataSource = dv;
                }

                else if (Nome.Checked)
                {
                    dv.RowFilter = "NOME LIKE '%" + Search.Text + "%'";
                    fornecedoresBindingSource.DataSource = dv;
                    fornecedoresBindingSource1.DataSource = dv;
                }

                else if (Telefone.Checked)
                {
                    dv.RowFilter = "TELEFONE LIKE '%" + Search.Text + "%'";
                    fornecedoresBindingSource.DataSource = dv;
                    fornecedoresBindingSource1.DataSource = dv;
                }

                else if (Cpf.Checked)
                {
                    dv.RowFilter = "CPFCNPJ LIKE '%" + Search.Text + "%'";
                    fornecedoresBindingSource.DataSource = dv;
                    fornecedoresBindingSource1.DataSource = dv;
                }

                else if (InscEstadual.Checked)
                {
                    dv.RowFilter = "PESSOA LIKE '%" + Search.Text + "%'";
                    fornecedoresBindingSource.DataSource = dv;
                    fornecedoresBindingSource1.DataSource = dv;
                }

                else if (Endereco.Checked)
                {
                    dv.RowFilter = "ENDERECO LIKE '%" + Search.Text + "%'";
                    fornecedoresBindingSource.DataSource = dv;
                    fornecedoresBindingSource1.DataSource = dv;
                }

                else if (Cidade.Checked)
                {
                    dv.RowFilter = "CIDADE LIKE '%" + Search.Text + "%'";
                    fornecedoresBindingSource.DataSource = dv;
                    fornecedoresBindingSource1.DataSource = dv;
                }

                else if (Entrada.Checked)
                {
                    dv.RowFilter = "ENTRADA LIKE '" + Convert.ToDateTime(Search.Text).ToString("dd-MM-yyyy") + "'";
                    fornecedoresBindingSource.DataSource = dv;
                    fornecedoresBindingSource1.DataSource = dv;
                }
            }
        }

        // Fornecedor nao encontrado
        private void NotFinded()
        {
            if (FornecedoresGrid.RowCount == 0)
            {
                if (DataFiltered)
                    NotFindDesc.Text = "Nenhum resultado que corresponda \n ao seu filtro atual foi encontrado.";
                else
                {
                    if (RemoverFiltros.Visible)
                        NotFindDesc.Text = "Nenhum resultado que corresponda a sua pesquisa \n atual foi encontrado. Tente remover algum filtro.";
                    else
                        NotFindDesc.Text = "Nenhum resultado que corresponda \n a sua pesquisa atual foi encontrado.";
                }

                FornecedoresGrid.Visible = false;
                Separator2.Visible = false;
                toolStripPaging.Visible = false;

                NotFind.Visible = true;

                NotFindDesc.Visible = true;
            }
            else
            {
                FornecedoresGrid.Visible = true;
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
                SearchOptions.Location = new Point(5512, SearchOptions.Location.Y);

            if (SearchOptions.Visible)
            {
                if (RemoverFiltros.Visible)
                    SearchOptions.Location = new Point(5512, SearchOptions.Location.Y);
                else
                    SearchOptions.Location = new Point(5512, SearchOptions.Location.Y);
            }

            if (DataOptions.Visible)
            {
                if (RemoverFiltros.Visible)
                    DataOptions.Location = new Point(5512, DataOptions.Location.Y);
                else if (TodoPeriodo.Checked)
                    DataOptions.Location = new Point(5512, DataOptions.Location.Y);
                else
                    DataOptions.Location = new Point(5512, DataOptions.Location.Y);
            }

            if (SpecificDate.Visible)
            {
                SpecificDate.Visible = false;
                SpecificDate.Location = new Point(SpecificDate.Location.X - 10, SpecificDate.Location.Y);
            }

            if (FilterItens.Visible)
            {
                FilterItens.Visible = false;
                FilterItens.Location = new Point(FilterItens.Location.X, FilterItens.Location.Y - 6);
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

        private void HideNewFornecedorItens()
        {
            FornecedoresGrid.Visible = true;
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
            Guna.UI2.WinForms.Guna2Panel[] Panels = new Guna.UI2.WinForms.Guna2Panel[11]
            {
                FilterItens, SearchOptions, DataOptions, SpecificDate,
                MoreOptions, SortOptions, ViewOptions, 
                ExportItens, ExcelFrame, PdfFrame, ExportOptions
            };

            // Botoes normais - principais
            Guna.UI2.WinForms.Guna2Button[] MainButtons = new Guna.UI2.WinForms.Guna2Button[4]
            {
                FilterBtn, MoreOptionsBtn, ExportBtn, Voltar
            };

            // Botoes normais
            Guna.UI2.WinForms.Guna2Button[] PanelButtons = new Guna.UI2.WinForms.Guna2Button[16]
            {
                FiltroPesquisaBtn, FilterDataBtn, DataEspecifica,
                SortBtn, ViewOptionsBtn, ListaNormal, ListaCompacta, Crescente, Descrescente,
                ExcelBtn, PdfBtn, ExportOptionsBtn, ExportCurrentExcel, ExportAllExcel, ExportCurrentPdf, ExportAllPdf
            };

            // Botoes de escolha (radio button)
            Guna.UI2.WinForms.Guna2RadioButton[] RadioButtons = new Guna.UI2.WinForms.Guna2RadioButton[26]
            {
                All, Id, Nome, Telefone, Cpf, Pessoa, InscEstadual, Endereco, Cidade, Entrada,
                TodoPeriodo, Hoje, Semana, Mes, Ano,
                AllColumns, Principais,
                IDSort, NameSort, TelefoneSort, CPFSort, PessoaSort, InscEstadualSort, EnderecoSort, CidadeSort, EntradaSort
            };

            //------------------//

            // Labels normais
            Label[] Labels = new Label[8]
            {
                FiltrosLabel, MoreOptionsLabel, SortOptionsLabel, ViewOptionsLabel, ExportItenssLabel, ExcelLabel, PdfLabel, ExportOptionsLabel
            };

            // Customer info labels info
            Label[] CustomerInfosLabels = new Label[17]
             {
                IDInfo, FornecedorName, TelefoneInfo, CpfInfo, InscEstadualInfo, PessoaInfo, TransportadoraInfo, EntradaInfo,
                EnderecoInfo, BairroInfo, CidadeInfo, CepInfo, EstadoInfo, ComplementoInfo, ObservacoesInfo, EmailInfo, UltimaCompraInfo
             };

            // Customer info labels
            Label[] CustomerPresetLabels = new Label[14]
            {
                TelefonePreset, CpfPreset, ComplementoPreset, EnderecoPreset, InscEstadualPreset, PessoaPreset, TransportadoraPreset, EstadoPreset, EntradaPreset, CepPreset,
                CidadePreset, BairroPreset, ObservacoesPreset, UltimaCompraPreset
            };

            //------------------//

            // Separators
            Guna.UI2.WinForms.Guna2Separator[] Separators = new Guna.UI2.WinForms.Guna2Separator[19]
            {
                Separator1, Separator2, guna2Separator1, guna2Separator2, guna2Separator3, guna2Separator4, guna2Separator5,
                guna2Separator6, guna2Separator7, guna2Separator8, guna2Separator9, guna2Separator10, guna2Separator11,
                guna2Separator11, guna2Separator12, guna2Separator13, guna2Separator17, guna2Separator13, MiniSeparator1
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

            FornecedoresGrid.BackgroundColor = ThemeManager.FormBackColor;
            FornecedoresGrid.DefaultCellStyle.BackColor = ThemeManager.FormBackColor;
            FornecedoresGrid.DefaultCellStyle.SelectionBackColor = ThemeManager.FormBackColor;
            FornecedoresGrid.DefaultCellStyle.ForeColor = ThemeManager.GridForeColor;
            FornecedoresGrid.DefaultCellStyle.SelectionForeColor = ThemeManager.FontColor;
            FornecedoresGrid.GridColor = ThemeManager.SeparatorAndBorderColor;

            FornecedoresGrid.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.WhiteFontColor;
            FornecedoresGrid.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemeManager.WhiteFontColor;
            FornecedoresGrid.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.FormBackColor;
            FornecedoresGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemeManager.FormBackColor;

            FornecedoresGrid.RowHeadersDefaultCellStyle.BackColor = ThemeManager.FormBackColor;

            ProdutosGrid.BackgroundColor = ThemeManager.FormBackColor;
            ProdutosGrid.DefaultCellStyle.BackColor = ThemeManager.FormBackColor;
            ProdutosGrid.DefaultCellStyle.SelectionBackColor = ThemeManager.FormBackColor;
            ProdutosGrid.DefaultCellStyle.ForeColor = ThemeManager.GridForeColor;
            ProdutosGrid.DefaultCellStyle.SelectionForeColor = ThemeManager.FontColor;
            ProdutosGrid.GridColor = ThemeManager.SeparatorAndBorderColor;

            ProdutosGrid.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.WhiteFontColor;
            ProdutosGrid.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemeManager.WhiteFontColor;
            ProdutosGrid.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.FormBackColor;
            ProdutosGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemeManager.FormBackColor;

            ProdutosGrid.RowHeadersDefaultCellStyle.BackColor = ThemeManager.FormBackColor;

            Search.FillColor = ThemeManager.SearchBoxFillColor;
            Search.ForeColor = ThemeManager.SearchBoxForeColor;
            Search.BorderColor = ThemeManager.SeparatorAndBorderColor;
            Search.HoverState.BorderColor = ThemeManager.SearchBoxHoverBorderColor;
            Search.PlaceholderForeColor = ThemeManager.SearchBoxPlaceholderColor;
            Search.FocusedState.BorderColor = ThemeManager.SearchBoxFocusedBorderColor;
            Search.FocusedState.ForeColor = ThemeManager.SearchBoxForeColor;

            PageItens.FillColor = ThemeManager.ComboBoxFillColor;
            PageItens.ForeColor = ThemeManager.ComboBoxForeColor;
            PageItens.BorderColor = ThemeManager.ComboBoxBorderColor;
            PageItens.HoverState.BorderColor = ThemeManager.ComboBoxHoverBorderColor;
            PageItens.FocusedState.BorderColor = ThemeManager.ComboBoxFocusedBorderColor;
            PageItens.ItemsAppearance.ForeColor = ThemeManager.ComboBoxForeColor;
            PageItens.ItemsAppearance.SelectedBackColor = ThemeManager.ComboBoxSelectedItemColor;

            MostrarText.BackColor = ThemeManager.FormBackColor;
            MostrarText.ForeColor = ThemeManager.FontColor;

            NovoFornecedor.BackColor = ThemeManager.FormBackColor;
            NovoFornecedor.FillColor = ThemeManager.FullRedButtonColor;
            NovoFornecedor.BorderColor = ThemeManager.FullRedButtonColor;
            NovoFornecedor.HoverState.FillColor = ThemeManager.FullRedButtonHoverColor;
            NovoFornecedor.HoverState.BorderColor = ThemeManager.FullRedButtonHoverColor;
            NovoFornecedor.CheckedState.FillColor = ThemeManager.FullRedButtonCheckedColor;
            NovoFornecedor.CheckedState.BorderColor = ThemeManager.FullRedButtonCheckedColor;

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

            DadosBtn.FillColor = ThemeManager.FormBackColor;
            DadosBtn.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            DadosBtn.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;

            EnderecoBtn.FillColor = ThemeManager.FormBackColor;
            EnderecoBtn.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            EnderecoBtn.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;

            ProdutosBtn.FillColor = ThemeManager.FormBackColor;
            ProdutosBtn.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            ProdutosBtn.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;

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

            ClientesSelecionados.ForeColor = ThemeManager.WhiteFontColor;
            NotForneceDesc.ForeColor = ThemeManager.WhiteFontColor;

            DeleteAllSelected.ForeColor = ThemeManager.RedFontColor;
            DeleteAllSelected.FillColor = ThemeManager.FormBackColor;
            DeleteAllSelected.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            DeleteAllSelected.PressedColor = ThemeManager.ButtonPressedColor;

            FornecedorPicture.BackColor = ThemeManager.FormBackColor;

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

        // Novo fornecedor
        private void NovoFornecedor_Click(object sender, EventArgs e)
        {
            HideFrames();

            foreach (Form frm in fc)
            {
                if (frm.Name != "NovoFornecedor")
                    NovoFornecedorOpen = false;
                else
                    NovoFornecedorOpen = true;
            }

            if (NovoFornecedorOpen != true)
            {
                NovoFornecedorOpen = true;

                ThreadStart ts = new ThreadStart(() => {
                    DarkBackground(new Frames.NovoFornecedor());
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
        private void FornecedoresGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == FornecedoresGrid.Columns[18].Index)
            {
                var cell = FornecedoresGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = "Opções";
            }
        }

        // Atualizar o grid qnd adicionar/editar/deletar fornecedor
        private async void VerifyTimer_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.CanUpdateGrid)
            {
                await TaskDelay(500);
                
                UpdateGrid();
                RefreshPagination();

                FornecedorInfo.Location = new Point(FornecedorInfo.Location.X, 11106);
            }

            if (Properties.Settings.Default.CanShowDeleteConfirmation)
            {
                List<int> AllIDsDistinct = AllIDs.Distinct().ToList();

                ThreadStart ts = new ThreadStart(() => {
                    DarkBackground(new Frames.DeleteConfirmation("Fornecedores", AllIDsDistinct));
                });

                Thread t = new Thread(ts);

                t.SetApartmentState(ApartmentState.STA);

                t.Start();

                Properties.Settings.Default.CanShowDeleteConfirmation = false;
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

                FornecedoresGrid.Visible = true;
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

                    FornecedoresGrid.Visible = true;
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
                if (DataOptions.Location == new Point(SearchOptions.Location.X + 6, SearchOptions.Location.Y))
                {
                    DataOptions.Visible = false;
                    DataOptions.Location = new Point(DataOptions.Location.X - 6, DataOptions.Location.Y);
                }

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
                if (SearchOptions.Location == new Point(DataOptions.Location.X + 6, DataOptions.Location.Y))
                {
                    SearchOptions.Visible = false;
                    SearchOptions.Location = new Point(SearchOptions.Location.X - 6, SearchOptions.Location.Y);
                }

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

        // Classificar por data customizada
        private async void CustomDateBtn_Click(object sender, EventArgs e)
        {
            SpecificDateFunction(Data1.Value, Data2.Value);

            RemoverFiltros.Visible = true;
            DataFiltered = true;

            await TaskDelay(100);

            DataOptions.Location = new Point(5512, DataOptions.Location.Y);
            ColorFilter(ActiveFilter2);

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
            SearchOptions.Location = new Point(5512, SearchOptions.Location.Y);

            if (All.Checked)
            {
                FilterBtn.BorderColor = ThemeManager.SeparatorAndBorderColor;
                FilterBtn.HoverState.FillColor = ThemeManager.MainButtonHoverFillColor;
                FilterBtn.HoverState.BorderColor = ThemeManager.MainButtonHoverBorderColor;
                FilterBtn.PressedColor = ThemeManager.MainButtonPressedColor;
                FilterBtn.Image = Properties.Resources.filtro___cinza;
                ActiveFilter1.Visible = false;

                if (ActiveFilter2.Visible)
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

            NotFind.Visible = false;
            NotFindDesc.Visible = false;

            FornecedoresGrid.Visible = true;
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

            ReloadPage();
            RefreshPagination();

            FilterBtn.BorderColor = ThemeManager.SeparatorAndBorderColor;
            FilterBtn.HoverState.FillColor = ThemeManager.MainButtonHoverFillColor;
            FilterBtn.HoverState.BorderColor = ThemeManager.MainButtonHoverBorderColor;
            FilterBtn.PressedColor = ThemeManager.MainButtonPressedColor;
            FilterBtn.Image = Properties.Resources.filtro___cinza;
            ActiveFilter1.Visible = false;
            ActiveFilter2.Visible = false;

            DataEspecifica.ForeColor = ThemeManager.FontColor;

            if (IsDarkModeEnabled)
                DataEspecifica.Image = Properties.Resources.data_branco;
            else
                DataEspecifica.Image = Properties.Resources.data_preto;

            RemoverFiltros.Visible = false;
            HideFrames();

            await TaskDelay(100);
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
            ExportExcel.FileName = "Planilha de fornecedores";
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
                    ExcelApp.Columns[1].ColumnWidth = 5;
                    ExcelApp.Columns[2].ColumnWidth = 30;
                    ExcelApp.Columns[3].ColumnWidth = 15;
                    ExcelApp.Columns[4].ColumnWidth = 20;
                    ExcelApp.Columns[5].ColumnWidth = 10;
                    ExcelApp.Columns[6].ColumnWidth = 30;
                    ExcelApp.Columns[7].ColumnWidth = 15;
                    ExcelApp.Columns[8].ColumnWidth = 20;
                    ExcelApp.Columns[9].ColumnWidth = 20;

                    for (int i = 1; i < FornecedoresGrid.Columns.Count; i++)
                    {
                        if (SortedByOrder == "Ascending")
                        {
                            ExcelApp.Cells[1] = FornecedoresGrid.Columns[0].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[2] = FornecedoresGrid.Columns[1].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[3] = FornecedoresGrid.Columns[2].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[4] = FornecedoresGrid.Columns[3].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[5] = FornecedoresGrid.Columns[4].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[6] = FornecedoresGrid.Columns[5].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[7] = FornecedoresGrid.Columns[6].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[8] = FornecedoresGrid.Columns[7].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[9] = FornecedoresGrid.Columns[8].HeaderText.Replace("↑", "");
                        }

                        else if (SortedByOrder == "Descending")
                        {
                            ExcelApp.Cells[1] = FornecedoresGrid.Columns[0].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[2] = FornecedoresGrid.Columns[1].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[3] = FornecedoresGrid.Columns[2].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[4] = FornecedoresGrid.Columns[3].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[5] = FornecedoresGrid.Columns[4].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[6] = FornecedoresGrid.Columns[5].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[7] = FornecedoresGrid.Columns[6].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[8] = FornecedoresGrid.Columns[7].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[9] = FornecedoresGrid.Columns[8].HeaderText.Replace("↓", "");
                        }
                    }

                    for (int i = 0; i < FornecedoresGrid.Rows.Count; i++)
                    {
                        for (int j = 1; j < FornecedoresGrid.Columns.Count; j++)
                        {
                            ExcelApp.Cells[i + 2, 1] = FornecedoresGrid.Rows[i].Cells[0].Value.ToString();
                            ExcelApp.Cells[i + 2, 2] = FornecedoresGrid.Rows[i].Cells[1].Value.ToString();
                            ExcelApp.Cells[i + 2, 3] = FornecedoresGrid.Rows[i].Cells[2].Value.ToString();
                            ExcelApp.Cells[i + 2, 4] = FornecedoresGrid.Rows[i].Cells[3].Value.ToString();
                            ExcelApp.Cells[i + 2, 5] = FornecedoresGrid.Rows[i].Cells[4].Value.ToString();
                            ExcelApp.Cells[i + 2, 6] = FornecedoresGrid.Rows[i].Cells[5].Value.ToString();
                            ExcelApp.Cells[i + 2, 7] = FornecedoresGrid.Rows[i].Cells[6].Value.ToString();
                            ExcelApp.Cells[i + 2, 8] = FornecedoresGrid.Rows[i].Cells[8].Value.ToString();
                            ExcelApp.Cells[i + 2, 9] = FornecedoresGrid.Rows[i].Cells[9].Value.ToString();
                        }
                    }
                }

                else if (AllColumns.Checked)
                {
                    ExcelApp.Columns[1].ColumnWidth = 5;
                    ExcelApp.Columns[2].ColumnWidth = 30;
                    ExcelApp.Columns[3].ColumnWidth = 15;
                    ExcelApp.Columns[4].ColumnWidth = 20;
                    ExcelApp.Columns[5].ColumnWidth = 10;
                    ExcelApp.Columns[6].ColumnWidth = 10;
                    ExcelApp.Columns[7].ColumnWidth = 15;
                    ExcelApp.Columns[8].ColumnWidth = 20;
                    ExcelApp.Columns[9].ColumnWidth = 30;
                    ExcelApp.Columns[10].ColumnWidth = 15;
                    ExcelApp.Columns[11].ColumnWidth = 15;
                    ExcelApp.Columns[12].ColumnWidth = 10;
                    ExcelApp.Columns[13].ColumnWidth = 10;
                    ExcelApp.Columns[14].ColumnWidth = 15;
                    ExcelApp.Columns[15].ColumnWidth = 30;
                    ExcelApp.Columns[16].ColumnWidth = 30;
                    ExcelApp.Columns[17].ColumnWidth = 15;

                    for (int i = 1; i < FornecedoresGrid.Columns.Count; i++)
                    {
                        if (SortedByOrder == "Ascending")
                        {
                            ExcelApp.Cells[1] = FornecedoresGrid.Columns[0].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[2] = FornecedoresGrid.Columns[1].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[3] = FornecedoresGrid.Columns[2].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[4] = FornecedoresGrid.Columns[3].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[5] = FornecedoresGrid.Columns[4].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[6] = FornecedoresGrid.Columns[5].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[7] = FornecedoresGrid.Columns[6].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[8] = FornecedoresGrid.Columns[7].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[9] = FornecedoresGrid.Columns[8].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[10] = FornecedoresGrid.Columns[9].HeaderText;
                            ExcelApp.Cells[11] = FornecedoresGrid.Columns[10].HeaderText;
                            ExcelApp.Cells[12] = FornecedoresGrid.Columns[11].HeaderText;
                            ExcelApp.Cells[13] = FornecedoresGrid.Columns[12].HeaderText;
                            ExcelApp.Cells[14] = FornecedoresGrid.Columns[13].HeaderText;
                            ExcelApp.Cells[15] = FornecedoresGrid.Columns[14].HeaderText;
                            ExcelApp.Cells[16] = FornecedoresGrid.Columns[15].HeaderText;
                            ExcelApp.Cells[17] = FornecedoresGrid.Columns[16].HeaderText;
                        }

                        else if (SortedByOrder == "Descending")
                        {
                            ExcelApp.Cells[1] = FornecedoresGrid.Columns[0].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[2] = FornecedoresGrid.Columns[1].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[3] = FornecedoresGrid.Columns[2].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[4] = FornecedoresGrid.Columns[3].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[5] = FornecedoresGrid.Columns[4].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[6] = FornecedoresGrid.Columns[5].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[7] = FornecedoresGrid.Columns[6].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[8] = FornecedoresGrid.Columns[7].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[9] = FornecedoresGrid.Columns[8].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[10] = FornecedoresGrid.Columns[9].HeaderText;
                            ExcelApp.Cells[11] = FornecedoresGrid.Columns[10].HeaderText;
                            ExcelApp.Cells[12] = FornecedoresGrid.Columns[11].HeaderText;
                            ExcelApp.Cells[13] = FornecedoresGrid.Columns[12].HeaderText;
                            ExcelApp.Cells[14] = FornecedoresGrid.Columns[13].HeaderText;
                            ExcelApp.Cells[15] = FornecedoresGrid.Columns[14].HeaderText;
                            ExcelApp.Cells[16] = FornecedoresGrid.Columns[15].HeaderText;
                            ExcelApp.Cells[17] = FornecedoresGrid.Columns[16].HeaderText;
                        }
                    }

                    for (int i = 0; i < FornecedoresGrid.Rows.Count; i++)
                    {
                        for (int j = 1; j < FornecedoresGrid.Columns.Count - 2; j++)
                        {
                            ExcelApp.Cells[i + 2, 1] = FornecedoresGrid.Rows[i].Cells[0].Value.ToString();
                            ExcelApp.Cells[i + 2, 2] = FornecedoresGrid.Rows[i].Cells[1].Value.ToString();
                            ExcelApp.Cells[i + 2, 3] = FornecedoresGrid.Rows[i].Cells[2].Value.ToString();
                            ExcelApp.Cells[i + 2, 4] = FornecedoresGrid.Rows[i].Cells[3].Value.ToString();
                            ExcelApp.Cells[i + 2, 5] = FornecedoresGrid.Rows[i].Cells[4].Value.ToString();
                            ExcelApp.Cells[i + 2, 6] = FornecedoresGrid.Rows[i].Cells[5].Value.ToString();
                            ExcelApp.Cells[i + 2, 7] = FornecedoresGrid.Rows[i].Cells[6].Value.ToString();
                            ExcelApp.Cells[i + 2, 8] = FornecedoresGrid.Rows[i].Cells[7].Value.ToString();
                            ExcelApp.Cells[i + 2, 9] = FornecedoresGrid.Rows[i].Cells[8].Value.ToString();
                            ExcelApp.Cells[i + 2, 10] = FornecedoresGrid.Rows[i].Cells[9].Value.ToString();
                            ExcelApp.Cells[i + 2, 11] = FornecedoresGrid.Rows[i].Cells[10].Value.ToString();
                            ExcelApp.Cells[i + 2, 12] = FornecedoresGrid.Rows[i].Cells[11].Value.ToString();
                            ExcelApp.Cells[i + 2, 13] = FornecedoresGrid.Rows[i].Cells[12].Value.ToString();
                            ExcelApp.Cells[i + 2, 14] = FornecedoresGrid.Rows[i].Cells[13].Value.ToString();
                            ExcelApp.Cells[i + 2, 15] = FornecedoresGrid.Rows[i].Cells[14].Value.ToString();
                            ExcelApp.Cells[i + 2, 16] = FornecedoresGrid.Rows[i].Cells[15].Value.ToString();
                            ExcelApp.Cells[i + 2, 17] = FornecedoresGrid.Rows[i].Cells[16].Value.ToString();
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
            ExportExcel.FileName = "Planilha geral de fornecedores";
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
                    ExcelApp.Columns[1].ColumnWidth = 5;
                    ExcelApp.Columns[2].ColumnWidth = 30;
                    ExcelApp.Columns[3].ColumnWidth = 15;
                    ExcelApp.Columns[4].ColumnWidth = 20;
                    ExcelApp.Columns[5].ColumnWidth = 10;
                    ExcelApp.Columns[6].ColumnWidth = 30;
                    ExcelApp.Columns[7].ColumnWidth = 15;
                    ExcelApp.Columns[8].ColumnWidth = 20;
                    ExcelApp.Columns[9].ColumnWidth = 20;

                    for (int i = 1; i < FornecedoresGrid.Columns.Count; i++)
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
                            ExcelApp.Cells[8] = AllDataGrid.Columns[8].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[9] = AllDataGrid.Columns[9].HeaderText.Replace("↑", "");
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
                            ExcelApp.Cells[8] = AllDataGrid.Columns[8].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[8] = AllDataGrid.Columns[9].HeaderText.Replace("↓", "");
                        }
                    }

                    for (int i = 0; i < AllDataGrid.Rows.Count; i++)
                    {
                        for (int j = 1; j < AllDataGrid.Columns.Count; j++)
                        {
                            ExcelApp.Cells[i + 2, 1] = AllDataGrid.Rows[i].Cells[0].Value.ToString();
                            ExcelApp.Cells[i + 2, 2] = AllDataGrid.Rows[i].Cells[1].Value.ToString();
                            ExcelApp.Cells[i + 2, 3] = AllDataGrid.Rows[i].Cells[2].Value.ToString();
                            ExcelApp.Cells[i + 2, 4] = AllDataGrid.Rows[i].Cells[3].Value.ToString();
                            ExcelApp.Cells[i + 2, 5] = AllDataGrid.Rows[i].Cells[4].Value.ToString();
                            ExcelApp.Cells[i + 2, 6] = AllDataGrid.Rows[i].Cells[5].Value.ToString();
                            ExcelApp.Cells[i + 2, 7] = AllDataGrid.Rows[i].Cells[6].Value.ToString();
                            ExcelApp.Cells[i + 2, 8] = AllDataGrid.Rows[i].Cells[8].Value.ToString();
                            ExcelApp.Cells[i + 2, 9] = AllDataGrid.Rows[i].Cells[9].Value.ToString();
                        }
                    }
                }

                else if (AllColumns.Checked)
                {
                    ExcelApp.Columns[1].ColumnWidth = 5;
                    ExcelApp.Columns[2].ColumnWidth = 30;
                    ExcelApp.Columns[3].ColumnWidth = 15;
                    ExcelApp.Columns[4].ColumnWidth = 20;
                    ExcelApp.Columns[5].ColumnWidth = 10;
                    ExcelApp.Columns[6].ColumnWidth = 10;
                    ExcelApp.Columns[7].ColumnWidth = 15;
                    ExcelApp.Columns[8].ColumnWidth = 20;
                    ExcelApp.Columns[9].ColumnWidth = 30;
                    ExcelApp.Columns[10].ColumnWidth = 15;
                    ExcelApp.Columns[11].ColumnWidth = 15;
                    ExcelApp.Columns[12].ColumnWidth = 10;
                    ExcelApp.Columns[13].ColumnWidth = 10;
                    ExcelApp.Columns[14].ColumnWidth = 15;
                    ExcelApp.Columns[15].ColumnWidth = 30;
                    ExcelApp.Columns[16].ColumnWidth = 30;
                    ExcelApp.Columns[17].ColumnWidth = 15;

                    for (int i = 1; i < FornecedoresGrid.Columns.Count; i++)
                    {
                        if (SortedByOrder == "Ascending")
                        {
                            ExcelApp.Cells[1] = FornecedoresGrid.Columns[0].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[2] = FornecedoresGrid.Columns[1].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[3] = FornecedoresGrid.Columns[2].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[4] = FornecedoresGrid.Columns[3].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[5] = FornecedoresGrid.Columns[4].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[6] = FornecedoresGrid.Columns[5].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[7] = FornecedoresGrid.Columns[6].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[8] = FornecedoresGrid.Columns[7].HeaderText;
                            ExcelApp.Cells[9] = FornecedoresGrid.Columns[8].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[10] = FornecedoresGrid.Columns[9].HeaderText.Replace("↑", "");
                            ExcelApp.Cells[11] = FornecedoresGrid.Columns[10].HeaderText;
                            ExcelApp.Cells[12] = FornecedoresGrid.Columns[11].HeaderText;
                            ExcelApp.Cells[13] = FornecedoresGrid.Columns[12].HeaderText;
                            ExcelApp.Cells[14] = FornecedoresGrid.Columns[13].HeaderText;
                            ExcelApp.Cells[15] = FornecedoresGrid.Columns[14].HeaderText;
                            ExcelApp.Cells[16] = FornecedoresGrid.Columns[15].HeaderText;
                            ExcelApp.Cells[17] = FornecedoresGrid.Columns[16].HeaderText;
                        }

                        else if (SortedByOrder == "Descending")
                        {
                            ExcelApp.Cells[1] = FornecedoresGrid.Columns[0].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[2] = FornecedoresGrid.Columns[1].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[3] = FornecedoresGrid.Columns[2].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[4] = FornecedoresGrid.Columns[3].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[5] = FornecedoresGrid.Columns[4].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[6] = FornecedoresGrid.Columns[5].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[7] = FornecedoresGrid.Columns[6].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[8] = FornecedoresGrid.Columns[7].HeaderText;
                            ExcelApp.Cells[9] = FornecedoresGrid.Columns[8].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[10] = FornecedoresGrid.Columns[9].HeaderText.Replace("↓", "");
                            ExcelApp.Cells[11] = FornecedoresGrid.Columns[10].HeaderText;
                            ExcelApp.Cells[12] = FornecedoresGrid.Columns[11].HeaderText;
                            ExcelApp.Cells[13] = FornecedoresGrid.Columns[12].HeaderText;
                            ExcelApp.Cells[14] = FornecedoresGrid.Columns[13].HeaderText;
                            ExcelApp.Cells[15] = FornecedoresGrid.Columns[14].HeaderText;
                            ExcelApp.Cells[16] = FornecedoresGrid.Columns[15].HeaderText;
                            ExcelApp.Cells[17] = FornecedoresGrid.Columns[16].HeaderText;
                        }
                    }

                    for (int i = 0; i < FornecedoresGrid.Rows.Count; i++)
                    {
                        for (int j = 1; j < FornecedoresGrid.Columns.Count - 2; j++)
                        {
                            ExcelApp.Cells[i + 2, 1] = FornecedoresGrid.Rows[i].Cells[0].Value.ToString();
                            ExcelApp.Cells[i + 2, 2] = FornecedoresGrid.Rows[i].Cells[1].Value.ToString();
                            ExcelApp.Cells[i + 2, 3] = FornecedoresGrid.Rows[i].Cells[2].Value.ToString();
                            ExcelApp.Cells[i + 2, 4] = FornecedoresGrid.Rows[i].Cells[3].Value.ToString();
                            ExcelApp.Cells[i + 2, 5] = FornecedoresGrid.Rows[i].Cells[4].Value.ToString();
                            ExcelApp.Cells[i + 2, 6] = FornecedoresGrid.Rows[i].Cells[5].Value.ToString();
                            ExcelApp.Cells[i + 2, 7] = FornecedoresGrid.Rows[i].Cells[6].Value.ToString();
                            ExcelApp.Cells[i + 2, 8] = FornecedoresGrid.Rows[i].Cells[7].Value.ToString();
                            ExcelApp.Cells[i + 2, 9] = FornecedoresGrid.Rows[i].Cells[8].Value.ToString();
                            ExcelApp.Cells[i + 2, 10] = FornecedoresGrid.Rows[i].Cells[9].Value.ToString();
                            ExcelApp.Cells[i + 2, 11] = FornecedoresGrid.Rows[i].Cells[10].Value.ToString();
                            ExcelApp.Cells[i + 2, 12] = FornecedoresGrid.Rows[i].Cells[11].Value.ToString();
                            ExcelApp.Cells[i + 2, 13] = FornecedoresGrid.Rows[i].Cells[12].Value.ToString();
                            ExcelApp.Cells[i + 2, 14] = FornecedoresGrid.Rows[i].Cells[13].Value.ToString();
                            ExcelApp.Cells[i + 2, 15] = FornecedoresGrid.Rows[i].Cells[14].Value.ToString();
                            ExcelApp.Cells[i + 2, 16] = FornecedoresGrid.Rows[i].Cells[15].Value.ToString();
                            ExcelApp.Cells[i + 2, 17] = FornecedoresGrid.Rows[i].Cells[16].Value.ToString();
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
            if (FornecedoresGrid.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Relatório de fornecedores.pdf";
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
                                PdfPTable pdfTable = new PdfPTable(FornecedoresGrid.Columns.Count - 11);
                                pdfTable.DefaultCell.Padding = 3;
                                pdfTable.WidthPercentage = 100;
                                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                                foreach (DataGridViewColumn column in FornecedoresGrid.Columns)
                                {
                                    if (column.Index != 7 && column.Index != 10 && column.Index != 11 && column.Index != 12 && column.Index != 13 && column.Index != 14
                                        && column.Index != 15 && column.Index != 16 && column.Index != 17 && column.Index != 18 && column.Index != 19 && column.Index != 20
                                        && column.Index != 21 && column.Index != 22 && column.Index != 23 && column.Index != 24)
                                    {
                                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                        cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                                        cell.Padding = 6;
                                        pdfTable.AddCell(cell);
                                    }         
                                }

                                foreach (DataGridViewRow row in FornecedoresGrid.Rows)
                                {
                                    foreach (DataGridViewCell cell in row.Cells)
                                    {
                                        if (cell.ColumnIndex != 7 && cell.ColumnIndex != 10 && cell.ColumnIndex != 11 && cell.ColumnIndex != 12 && cell.ColumnIndex != 13 && cell.ColumnIndex != 14
                                        && cell.ColumnIndex != 15 && cell.ColumnIndex != 16 && cell.ColumnIndex != 17 && cell.ColumnIndex != 18 && cell.ColumnIndex != 19 && cell.ColumnIndex != 20
                                        && cell.ColumnIndex != 21 && cell.ColumnIndex != 22 && cell.ColumnIndex != 23 && cell.ColumnIndex != 24)
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
                                    var Nome = new Paragraph("                           CLÍNICA CAR                                                " +
                                        "Relatório de fornecedores", NomeParagraph);
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
                                            var Texto = new Paragraph("Tabela de Fornecedores - Hoje\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        if (FilterByDays == 8)
                                        {
                                            var Texto = new Paragraph("Tabela de Fornecedores - Últimos 7 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 31)
                                        {
                                            var Texto = new Paragraph("Tabela de Fornecedores - Últimos 30 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 365)
                                        {
                                            var Texto = new Paragraph("Tabela de Fornecedores - Últimos 365 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }
                                    }

                                    else
                                    {
                                        var Texto = new Paragraph("Tabela de Fornecedores\n\n", TextoParagraph);

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
                                PdfPTable pdfTable = new PdfPTable(FornecedoresGrid.Columns.Count - 3);
                                pdfTable.DefaultCell.Padding = 6;
                                pdfTable.WidthPercentage = 101.25F;
                                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                                foreach (DataGridViewColumn column in FornecedoresGrid.Columns)
                                {
                                    if (column.Index != 17 && column.Index != 18 && column.Index != 19 && column.Index != 20 && column.Index != 21
                                        && column.Index != 22 && column.Index != 23 && column.Index != 24)
                                    {
                                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                        cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                                        cell.Padding = 8;
                                        pdfTable.AddCell(cell);
                                    }
                                }

                                foreach (DataGridViewRow row in FornecedoresGrid.Rows)
                                {
                                    foreach (DataGridViewCell cell in row.Cells)
                                    {
                                        if (cell.ColumnIndex != 17 && cell.ColumnIndex != 18 && cell.ColumnIndex != 19 && cell.ColumnIndex != 20 && cell.ColumnIndex != 21
                                        && cell.ColumnIndex != 22 && cell.ColumnIndex != 23 && cell.ColumnIndex != 24)
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
                                    var Nome = new Paragraph("                           CLÍNICA CAR                                                " +
                                        "Relatório de fornecedores", NomeParagraph);
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
                                            var Texto = new Paragraph("Tabela de Fornecedores - Hoje\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        if (FilterByDays == 8)
                                        {
                                            var Texto = new Paragraph("Tabela de Fornecedores - Últimos 7 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 31)
                                        {
                                            var Texto = new Paragraph("Tabela de Fornecedores - Últimos 30 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 365)
                                        {
                                            var Texto = new Paragraph("Tabela de Fornecedores - Últimos 365 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }
                                    }

                                    else
                                    {
                                        var Texto = new Paragraph("Tabela de Fornecedores\n\n", TextoParagraph);

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
                sfd.FileName = "Relatório geral de fornecedores.pdf";
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
                                PdfPTable pdfTable = new PdfPTable(AllDataGrid.Columns.Count - 11);
                                pdfTable.DefaultCell.Padding = 3;
                                pdfTable.WidthPercentage = 100;
                                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                                foreach (DataGridViewColumn column in AllDataGrid.Columns)
                                {
                                    if (column.Index != 7 && column.Index != 10 && column.Index != 11 && column.Index != 12 && column.Index != 13 && column.Index != 14
                                        && column.Index != 15 && column.Index != 16 && column.Index != 17 && column.Index != 18 && column.Index != 19 && column.Index != 20
                                        && column.Index != 21 && column.Index != 22 && column.Index != 23)
                                    {
                                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                        cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                                        cell.Padding = 6;
                                        pdfTable.AddCell(cell);
                                    }
                                }

                                foreach (DataGridViewRow row in AllDataGrid.Rows)
                                {
                                    foreach (DataGridViewCell cell in row.Cells)
                                    {
                                        if (cell.ColumnIndex != 7 && cell.ColumnIndex != 10 && cell.ColumnIndex != 11 && cell.ColumnIndex != 12 && cell.ColumnIndex != 13 && cell.ColumnIndex != 14
                                        && cell.ColumnIndex != 15 && cell.ColumnIndex != 16 && cell.ColumnIndex != 17 && cell.ColumnIndex != 18 && cell.ColumnIndex != 19 && cell.ColumnIndex != 20
                                        && cell.ColumnIndex != 21 && cell.ColumnIndex != 22 && cell.ColumnIndex != 23)
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
                                    var Nome = new Paragraph("                           CLÍNICA CAR                                                " +
                                        "Relatório de fornecedores", NomeParagraph);
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
                                            var Texto = new Paragraph("Tabela de Fornecedores - Hoje\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        if (FilterByDays == 8)
                                        {
                                            var Texto = new Paragraph("Tabela de Fornecedores - Últimos 7 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 31)
                                        {
                                            var Texto = new Paragraph("Tabela de Fornecedores - Últimos 30 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 365)
                                        {
                                            var Texto = new Paragraph("Tabela de Fornecedores - Últimos 365 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }
                                    }

                                    else
                                    {
                                        var Texto = new Paragraph("Tabela de Fornecedores\n\n", TextoParagraph);

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
                                PdfPTable pdfTable = new PdfPTable(AllDataGrid.Columns.Count - 3);
                                pdfTable.DefaultCell.Padding = 6;
                                pdfTable.WidthPercentage = 101.25F;
                                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                                foreach (DataGridViewColumn column in AllDataGrid.Columns)
                                {
                                    if (column.Index != 17 && column.Index != 18 && column.Index != 19 && column.Index != 20 && column.Index != 21
                                        && column.Index != 22 && column.Index != 23)
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
                                        if (cell.ColumnIndex != 17 && cell.ColumnIndex != 18 && cell.ColumnIndex != 19 && cell.ColumnIndex != 20 && cell.ColumnIndex != 21
                                        && cell.ColumnIndex != 22 && cell.ColumnIndex != 23)
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
                                    var Nome = new Paragraph("                           CLÍNICA CAR                                                " +
                                        "Relatório de fornecedores", NomeParagraph);
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
                                            var Texto = new Paragraph("Tabela geral de Fornecedores - Hoje\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        if (FilterByDays == 8)
                                        {
                                            var Texto = new Paragraph("Tabela geral de Fornecedores - Últimos 7 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 31)
                                        {
                                            var Texto = new Paragraph("Tabela geral de Fornecedores - Últimos 30 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }

                                        else if (FilterByDays == 365)
                                        {
                                            var Texto = new Paragraph("Tabela geral de Fornecedores - Últimos 365 dias\n\n", TextoParagraph);

                                            Texto.Alignment = Element.ALIGN_CENTER;
                                            pdfDoc.Add(Texto);
                                        }
                                    }

                                    else
                                    {
                                        var Texto = new Paragraph("Tabela geral de Fornecedores\n\n", TextoParagraph);

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

        /* Informaçoes dos fornecedores */

        // Mostrar pagina de informaçoes e valor das informaçoes de cada fornecedor
        private void FornecedoresGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (fornecedoresData.Fornecedores.Count > 0)
            {
                if (FornecedoresGrid.CurrentCell.ColumnIndex == 18)
                {
                    HideFrames();
                    Search.Visible = false; FilterBtn.Visible = false; RemoverFiltros.Visible = false; ExportBtn.Visible = false;
                    PageItens.Visible = false; MostrarText.Visible = false; NovoFornecedor.Visible = false; EraseText.Visible = false;
                    MoreOptionsBtn.Visible = false; toolStripPaging.Visible = false;

                    Editar.Location = new Point(Editar.Location.X, 57);
                    Excluir.Location = new Point(Excluir.Location.X, 57);
                    Voltar.Location = new Point(Voltar.Location.X, 57);

                    IDInfo.Text = "ID: " + FornecedoresGrid.CurrentRow.Cells[0].Value.ToString();
                    IDInt.Text = FornecedoresGrid.CurrentRow.Cells[0].Value.ToString();
                    FornecedorName.Text = FornecedoresGrid.CurrentRow.Cells[1].Value.ToString();
                    TelefoneInfo.Text = FornecedoresGrid.CurrentRow.Cells[2].Value.ToString();
                    CpfInfo.Text = FornecedoresGrid.CurrentRow.Cells[3].Value.ToString();
                    PessoaInfo.Text = FornecedoresGrid.CurrentRow.Cells[4].Value.ToString();
                    InscEstadualInfo.Text = FornecedoresGrid.CurrentRow.Cells[5].Value.ToString();
                    EnderecoInfo.Text = FornecedoresGrid.CurrentRow.Cells[6].Value.ToString();
                    CidadeInfo.Text = FornecedoresGrid.CurrentRow.Cells[7].Value.ToString();
                    EntradaInfo.Text = FornecedoresGrid.CurrentRow.Cells[8].Value.ToString();
                    BairroInfo.Text = FornecedoresGrid.CurrentRow.Cells[9].Value.ToString();
                    CepInfo.Text = FornecedoresGrid.CurrentRow.Cells[10].Value.ToString();
                    EstadoInfo.Text = FornecedoresGrid.CurrentRow.Cells[11].Value.ToString();
                    ComplementoInfo.Text = FornecedoresGrid.CurrentRow.Cells[12].Value.ToString();
                    ObservacoesInfo.Text = FornecedoresGrid.CurrentRow.Cells[13].Value.ToString();
                    EmailInfo.Text = "       " + FornecedoresGrid.CurrentRow.Cells[14].Value.ToString();
                    UltimaCompraInfo.Text = "       " + FornecedoresGrid.CurrentRow.Cells[15].Value.ToString();
                    TransportadoraInfo.Text = FornecedoresGrid.CurrentRow.Cells[16].Value.ToString();

                    OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

                    con.Open();

                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM Estoque WHERE FORNECEDOR = '" + FornecedorName.Text + "'", con);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        HideNewFornecedorItens();

                        DataTable customrows = dt;

                        // Produtos data
                        estoqueBindingSource.DataSource = customrows;
                        estoqueTableAdapter.Fill(estoqueData.Estoque);

                        ForneceAlgumProduto = true;

                        Search.Visible = false; FilterBtn.Visible = false; RemoverFiltros.Visible = false; ExportBtn.Visible = false;
                        PageItens.Visible = false; MostrarText.Visible = false; NovoFornecedor.Visible = false; EraseText.Visible = false;
                        MoreOptionsBtn.Visible = false; toolStripPaging.Visible = false;

                        con.Close();
                    }
                    else
                    {
                        Search.Visible = false; FilterBtn.Visible = false; RemoverFiltros.Visible = false; ExportBtn.Visible = false;
                        PageItens.Visible = false; MostrarText.Visible = false; NovoFornecedor.Visible = false; EraseText.Visible = false;
                        MoreOptionsBtn.Visible = false; toolStripPaging.Visible = false;

                        ForneceAlgumProduto = false;
                    }

                    byte[] content = (byte[])FornecedoresGrid.CurrentRow.Cells[17].Value;
                    MemoryStream stream = new MemoryStream(content);
                    FornecedorPicture.Image = System.Drawing.Image.FromStream(stream);

                    if (TransportadoraInfo.Text == "true")
                        TransportadoraInfo.Text = "Sim";
                    else if (TransportadoraInfo.Text == "false")
                        TransportadoraInfo.Text = "Não";

                    if (EntradaInfo.Text.Length > 10)
                    {
                        EntradaInfo.Text = EntradaInfo.Text.Substring(0, 10);
                    }

                    if (TelefoneInfo.Text.Length == 15)
                    {
                        TipoDeNumeroInfo.Text = "Celular";
                    }

                    else if (TelefoneInfo.Text.Length == 9)
                    {
                        TipoDeNumeroInfo.Text = "Telefone";
                    }

                    if (ProdutosGrid.RowCount == 0)
                    {
                        Search.Visible = false; FilterBtn.Visible = false; RemoverFiltros.Visible = false; ExportBtn.Visible = false;
                        PageItens.Visible = false; MostrarText.Visible = false; NovoFornecedor.Visible = false; EraseText.Visible = false;
                        MoreOptionsBtn.Visible = false; toolStripPaging.Visible = false;

                        ProdutosGrid.Visible = false;
                        guna2Separator13.Visible = false;
                        NotFornece.Visible = true;
                        NotForneceDesc.Visible = true;

                        ForneceAlgumProduto = false;
                    }
                    else
                    {
                        Search.Visible = false; FilterBtn.Visible = false; RemoverFiltros.Visible = false; ExportBtn.Visible = false;
                        PageItens.Visible = false; MostrarText.Visible = false; NovoFornecedor.Visible = false; EraseText.Visible = false;
                        MoreOptionsBtn.Visible = false; toolStripPaging.Visible = false;

                        ProdutosGrid.Visible = true;
                        guna2Separator13.Visible = true;
                        NotFornece.Visible = false;
                        NotForneceDesc.Visible = false;

                        ForneceAlgumProduto = true;
                    }

                    FornecedorInfo.Location = new Point(FornecedorInfo.Location.X, 106);
                }
            }
        }

        // Mostrar pagina de informaçoes e valor das informaçoes de cada fornecedor so q com clique duplo
        private void FornecedoresGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (fornecedoresData.Fornecedores.Count > 0)
            {
                if (EnableDoubleClickInGrid)
                {
                    HideFrames();
                    Search.Visible = false; FilterBtn.Visible = false; RemoverFiltros.Visible = false; ExportBtn.Visible = false;
                    PageItens.Visible = false; MostrarText.Visible = false; NovoFornecedor.Visible = false; EraseText.Visible = false;
                    MoreOptionsBtn.Visible = false; toolStripPaging.Visible = false;

                    Editar.Location = new Point(Editar.Location.X, 57);
                    Excluir.Location = new Point(Excluir.Location.X, 57);
                    Voltar.Location = new Point(Voltar.Location.X, 57);

                    IDInfo.Text = "ID: " + FornecedoresGrid.CurrentRow.Cells[0].Value.ToString();
                    IDInt.Text = FornecedoresGrid.CurrentRow.Cells[0].Value.ToString();
                    FornecedorName.Text = FornecedoresGrid.CurrentRow.Cells[1].Value.ToString();
                    TelefoneInfo.Text = FornecedoresGrid.CurrentRow.Cells[2].Value.ToString();
                    CpfInfo.Text = FornecedoresGrid.CurrentRow.Cells[3].Value.ToString();
                    PessoaInfo.Text = FornecedoresGrid.CurrentRow.Cells[4].Value.ToString();
                    InscEstadualInfo.Text = FornecedoresGrid.CurrentRow.Cells[5].Value.ToString();
                    EnderecoInfo.Text = FornecedoresGrid.CurrentRow.Cells[6].Value.ToString();
                    CidadeInfo.Text = FornecedoresGrid.CurrentRow.Cells[7].Value.ToString();
                    EntradaInfo.Text = FornecedoresGrid.CurrentRow.Cells[8].Value.ToString();
                    BairroInfo.Text = FornecedoresGrid.CurrentRow.Cells[9].Value.ToString();
                    CepInfo.Text = FornecedoresGrid.CurrentRow.Cells[10].Value.ToString();
                    EstadoInfo.Text = FornecedoresGrid.CurrentRow.Cells[11].Value.ToString();
                    ComplementoInfo.Text = FornecedoresGrid.CurrentRow.Cells[12].Value.ToString();
                    ObservacoesInfo.Text = FornecedoresGrid.CurrentRow.Cells[13].Value.ToString();
                    EmailInfo.Text = "       " + FornecedoresGrid.CurrentRow.Cells[14].Value.ToString();
                    UltimaCompraInfo.Text = "       " + FornecedoresGrid.CurrentRow.Cells[15].Value.ToString();
                    TransportadoraInfo.Text = FornecedoresGrid.CurrentRow.Cells[16].Value.ToString();

                    OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\LocalDTB.mdb;Persist Security Info=True");

                    con.Open();

                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM Estoque WHERE FORNECEDOR = '" + FornecedorName.Text + "'", con);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        HideNewFornecedorItens();

                        DataTable customrows = dt;

                        // Produtos data
                        estoqueBindingSource.DataSource = customrows;
                        estoqueTableAdapter.Fill(estoqueData.Estoque);

                        ForneceAlgumProduto = true;

                        Search.Visible = false; FilterBtn.Visible = false; RemoverFiltros.Visible = false; ExportBtn.Visible = false;
                        PageItens.Visible = false; MostrarText.Visible = false; NovoFornecedor.Visible = false; EraseText.Visible = false;
                        MoreOptionsBtn.Visible = false; toolStripPaging.Visible = false;

                        con.Close();
                    }
                    else
                    {
                        Search.Visible = false; FilterBtn.Visible = false; RemoverFiltros.Visible = false; ExportBtn.Visible = false;
                        PageItens.Visible = false; MostrarText.Visible = false; NovoFornecedor.Visible = false; EraseText.Visible = false;
                        MoreOptionsBtn.Visible = false; toolStripPaging.Visible = false;

                        ForneceAlgumProduto = false;
                    }

                    byte[] content = (byte[])FornecedoresGrid.CurrentRow.Cells[17].Value;
                    MemoryStream stream = new MemoryStream(content);
                    FornecedorPicture.Image = System.Drawing.Image.FromStream(stream);

                    if (TransportadoraInfo.Text == "true")
                        TransportadoraInfo.Text = "Sim";
                    else if (TransportadoraInfo.Text == "false")
                        TransportadoraInfo.Text = "Não";

                    if (EntradaInfo.Text.Length > 10)
                    {
                        EntradaInfo.Text = EntradaInfo.Text.Substring(0, 10);
                    }

                    if (TelefoneInfo.Text.Length == 15)
                    {
                        TipoDeNumeroInfo.Text = "Celular";
                    }

                    else if (TelefoneInfo.Text.Length == 9)
                    {
                        TipoDeNumeroInfo.Text = "Telefone";
                    }

                    if (ProdutosGrid.RowCount == 0)
                    {
                        Search.Visible = false; FilterBtn.Visible = false; RemoverFiltros.Visible = false; ExportBtn.Visible = false;
                        PageItens.Visible = false; MostrarText.Visible = false; NovoFornecedor.Visible = false; EraseText.Visible = false;
                        MoreOptionsBtn.Visible = false; toolStripPaging.Visible = false;

                        ProdutosGrid.Visible = false;
                        guna2Separator13.Visible = false;
                        NotFornece.Visible = true;
                        NotForneceDesc.Visible = true;

                        ForneceAlgumProduto = false;
                    }
                    else
                    {
                        Search.Visible = false; FilterBtn.Visible = false; RemoverFiltros.Visible = false; ExportBtn.Visible = false;
                        PageItens.Visible = false; MostrarText.Visible = false; NovoFornecedor.Visible = false; EraseText.Visible = false;
                        MoreOptionsBtn.Visible = false; toolStripPaging.Visible = false;

                        ProdutosGrid.Visible = true;
                        guna2Separator13.Visible = true;
                        NotFornece.Visible = false;
                        NotForneceDesc.Visible = false;

                        ForneceAlgumProduto = true;
                    }

                    FornecedorInfo.Location = new Point(FornecedorInfo.Location.X, 106);
                }
            }
        }

        // Botoes
        private void DadosBtn_Click(object sender, EventArgs e) 
        {
            MovingBar.Size = new Size(130, 3);
            MovingBar.Location = new Point(10, 189);

            DadosBtn.ForeColor = ThemeManager.RedFontColor;
            EnderecoBtn.ForeColor = Color.FromArgb(180, 180, 180);
            ProdutosBtn.ForeColor = Color.FromArgb(180, 180, 180);

            guna2Separator13.Visible = false;

            DadosPanel.Visible = true;
            EnderecoPanel.Visible = false;
            ProdutosPanel.Visible = false;
        }

        private void EnderecoBtn_Click(object sender, EventArgs e)
        {
            MovingBar.Size = new Size(110, 3);
            MovingBar.Location = new Point(170, 189);
            
            DadosBtn.ForeColor = Color.FromArgb(180, 180, 180);
            EnderecoBtn.ForeColor = ThemeManager.RedFontColor;
            ProdutosBtn.ForeColor = Color.FromArgb(180, 180, 180);

            guna2Separator13.Visible = false;

            DadosPanel.Visible = false;
            EnderecoPanel.Visible = true;
            ProdutosPanel.Visible = false;
        }

        private void ProdutosBtn_Click(object sender, EventArgs e)
        {
            MovingBar.Size = new Size(170, 3);
            MovingBar.Location = new Point(315, 189);

            DadosBtn.ForeColor = Color.FromArgb(180, 180, 180);
            EnderecoBtn.ForeColor = Color.FromArgb(180, 180, 180);
            ProdutosBtn.ForeColor = ThemeManager.RedFontColor;

            if (ForneceAlgumProduto)
                guna2Separator13.Visible = true;

            DadosPanel.Visible = false;
            EnderecoPanel.Visible = false;
            ProdutosPanel.Visible = true;
        }

        // Editar
        private void Editar_Click(object sender, EventArgs e)
        {
            foreach (Form frm in fc)
            {
                if (frm.Name != "EditFornecedor")
                    EditFornecedorOpen = false;
                else
                    EditFornecedorOpen = true;
            }

            if (EditFornecedorOpen != true)
            {
                EditFornecedorOpen = true;

                int ID;
                byte[] Foto;

                string Nome, Telefone, TipoDeNumero, Cpf, InscEstadual, Pessoa, 
                    Transportadora, Endereco, Bairro, Cidade, Entrada, Cep, Estado, 
                    Complemento, Email, Observacoes, ForneceUmProduto;

                ID = Convert.ToInt32(IDInt.Text);
                Nome = FornecedorName.Text;
                Telefone = TelefoneInfo.Text;
                TipoDeNumero = TipoDeNumeroInfo.Text;
                Cpf = CpfInfo.Text;
                Pessoa = PessoaInfo.Text;
                InscEstadual = InscEstadualInfo.Text;
                Transportadora = TransportadoraInfo.Text;
                Endereco = EnderecoInfo.Text;
                Bairro = BairroInfo.Text;
                Cidade = CidadeInfo.Text;
                Entrada = EntradaInfo.Text;
                Cep = CepInfo.Text;
                Estado = EstadoInfo.Text;
                Complemento = ComplementoInfo.Text;
                Email = EmailInfo.Text;
                Observacoes = ObservacoesInfo.Text;
                ForneceUmProduto = Convert.ToString(ForneceAlgumProduto);

                byte[] ImageToByteArray(System.Drawing.Image imageIn)
                {
                    using (var ms = new MemoryStream())
                    {
                        imageIn.Save(ms, imageIn.RawFormat);
                        return ms.ToArray();
                    }
                }

                Foto = ImageToByteArray(FornecedorPicture.Image);

                ThreadStart ts = new ThreadStart(() => {
                    DarkBackground(new Frames.EditFornecedor(ID, Nome, Telefone, TipoDeNumero, Cpf, Pessoa, InscEstadual,
                    Transportadora, Entrada, Endereco, Bairro, Cidade, Cep, Estado, Complemento, Email, Observacoes, ForneceUmProduto, Foto));
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
                if (frm.Name != "DeleteSelected")
                    DeleteFornecedorOpen = false;
                else
                    DeleteFornecedorOpen = true;
            }

            if (DeleteFornecedorOpen != true)
            {
                DeleteFornecedorOpen = true;

                ID = Convert.ToInt32(IDInt.Text);

                Frames.DeleteSelected.DeleteSelectedFrame.TmplText.Text = "Excluir fornecedor";
                Frames.DeleteSelected.DeleteSelectedFrame.LblText.Text = "Você deseja mesmo excluir este fornecedor?";

                ThreadStart ts = new ThreadStart(() => {
                    DarkBackground(new Frames.DeleteSelected("Fornecedor", ID));
                });

                Thread t = new Thread(ts);

                t.SetApartmentState(ApartmentState.STA);

                t.Start();
            }
        }

        // Fechar pagina de informaçoes
        private void Voltar_Click(object sender, EventArgs e)
        {
            FornecedorInfo.Location = new Point(FornecedorInfo.Location.X, 11106);

            Search.Visible = true; FilterBtn.Visible = true; ExportBtn.Visible = true;
            PageItens.Visible = true; MostrarText.Visible = true; NovoFornecedor.Visible = true;
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

            FornecedoresGrid.GridColor = ThemeManager.SeparatorAndBorderColor;
            FornecedoresGrid.RowTemplate.Height = 44;
            fornecedoresBindingSource.DataSource = null;
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

            FornecedoresGrid.GridColor = ThemeManager.FormBackColor;
            FornecedoresGrid.RowTemplate.Height = 32;
            fornecedoresBindingSource.DataSource = null;
            ReloadPage();

            HideFrames();
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Opçoes de seleçao de fornecedores */

        // Exibir o tanto de fornecedores selecionados e mostrar as opçoes
        private void FornecedoresGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                AllIDs.Clear();

                foreach (DataGridViewRow dgv in FornecedoresGrid.SelectedRows)
                {
                    if (dgv.Selected)
                        if (FornecedoresGrid.SelectedRows.Count >= 2)
                        {
                            AllIDs.Add(Convert.ToInt32(dgv.Cells[0].Value));
                        }
                        else
                        AllIDs.Clear();
                }

                if (FornecedoresGrid.SelectedRows.Count >= 2)
                {
                    SelectedOptions.Visible = true;
                    ClientesSelecionados.Text = FornecedoresGrid.SelectedRows.Count.ToString() + " fornecedores selecionados";

                    if (FornecedoresGrid.SelectedRows.Count >= 10)
                    {
                        MiniSeparator1.Location = new Point(192, MiniSeparator1.Location.Y);
                        DeleteAllSelected.Location = new Point(208, DeleteAllSelected.Location.Y);
                    }

                    else
                    {
                        MiniSeparator1.Location = new Point(186, MiniSeparator1.Location.Y);
                        DeleteAllSelected.Location = new Point(203, DeleteAllSelected.Location.Y);
                    }
                }

                else
                    SelectedOptions.Visible = false;
            }
        }

        // Excluir todos os fornecedores selecionados
        private void DeleteAllSelected_Click(object sender, EventArgs e)
        {
            Frames.DeleteAllSelected DeleteAllForm = new Frames.DeleteAllSelected();

            Frames.DeleteAllSelected.DeleteAllFrame.TmplText.Text = "Excluir fornecedores";
            Frames.DeleteAllSelected.DeleteAllFrame.LblText.Text = "Você deseja mesmo excluir esses fornecedores?";

            ThreadStart ts = new ThreadStart(() => {
                DarkBackground(DeleteAllForm);
            });

            Thread t = new Thread(ts);

            t.SetApartmentState(ApartmentState.STA);

            t.Start();
        }
    }
}
