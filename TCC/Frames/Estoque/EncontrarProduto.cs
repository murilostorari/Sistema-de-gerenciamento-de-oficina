using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCC.Frames.Estoque
{
    public partial class EncontrarProduto : Form
    {
        FormCollection fc = Application.OpenForms;

        NovaEntrada NewEntry = (NovaEntrada)Application.OpenForms["NovaEntrada"];
        NovaSaida SaidaForm = (NovaSaida)Application.OpenForms["NovaSaida"];

        private int CurrentPage = 0;
        int PagesCount = 0;
        int PageRows = 10;

        bool NovoProdutoOpen;

        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateFrames = Properties.Settings.Default.AnimarFrames;
        bool AnimateButtonsAndPanels = Properties.Settings.Default.AnimarBotoes;

        public EncontrarProduto()
        {
            InitializeComponent();
            SetColor();

            if (AnimateFrames)
                guna2BorderlessForm1.AnimateWindow = true;
            else
                guna2BorderlessForm1.AnimateWindow = false;
        }

        private void EncontrarProduto_Load(object sender, EventArgs e)
        {
            // Estoque data
            this.estoqueTableAdapter.Fill(this.estoqueData.Estoque);

            if (estoqueData.Estoque.Count == 0)
            {
                EstoqueGrid.Visible = false;
                Separator2.Visible = false;
                toolStripPaging.Visible = false;
                Search.Visible = false;
                MostrarText.Visible = false;
                PageItens.Visible = false;

                Illustration.Visible = true;
                Desc.Visible = true;
            }

            if (IsDarkModeEnabled)
                ToolStripDarkPanel.Visible = true;
            else
                ToolStripDarkPanel.Visible = false;

            if (AnimateButtonsAndPanels)
                Search.Animated = true;
            else
                Search.Animated = false;

            PagesCount = Convert.ToInt32(Math.Ceiling(estoqueData.Estoque.Count * 1.0 / PageRows));
            CurrentPage = 0;
            RefreshPagination();
            ReloadPage();

            System.Drawing.Rectangle ToolStripArea = toolStripPaging.Parent.ClientRectangle;
            toolStripPaging.Left = (ToolStripArea.Width - toolStripPaging.Width) / 2;

            System.Drawing.Rectangle DarkToolStripPanelArea = ToolStripDarkPanel.Parent.ClientRectangle;
            ToolStripDarkPanel.Left = (DarkToolStripPanelArea.Width - ToolStripDarkPanel.Width) / 2;

            toolStripPaging.Cursor = Cursors.Hand;
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Funcoes */

        // Delay
        async Task TaskDelay(int valor)
        {
            await Task.Delay(valor);
            //BackspacePressed = false;
        }

        private async void CloseFunction()
        {
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
                    if (frm.Name == "NovaEntrada")
                    {
                        if (NewEntry.Opacity < 1.0)
                            NewEntry.Opacity = 1.0;
                    }
                    else if (frm.Name == "NovaSaida")
                    {
                        if (SaidaForm.Opacity < 1.0)
                            SaidaForm.Opacity = 1.0;
                    }
                }

                Close();
            }
        }

        // Carregar dados e ajustar valores
        private void ReloadPage()
        {
            var query = from campos in estoqueData.Estoque
                        select new
                        {
                            campos.CODIGO,
                            campos.NUMEROFABRICANTE,
                            campos.PRODUTO,
                            campos.FORNECEDOR,
                        };

            estoqueBindingSource.DataSource = query.Skip(PageRows * CurrentPage).Take(PageRows).ToList();
            PagesCount = Convert.ToInt32(Math.Ceiling(estoqueData.Estoque.Count * 1.0 / PageRows));
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

                ReloadPage();
                RefreshPagination();
            }
            catch (Exception) { }
        }

        // Filtro de pesquisa
        private void SearchFilter()
        {
            DataView dv = estoqueData.Estoque.DefaultView;

            dv.RowFilter = string.Format("convert (CODIGO, 'System.String') LIKE '%" + Search.Text + "%' OR PRODUTO LIKE '%" + Search.Text + "%' OR NUMEROFABRICANTE LIKE '%" + Search.Text + "%' OR FORNECEDOR LIKE '%" + Search.Text + "%'");
            estoqueBindingSource.DataSource = dv;
        }

        // Cliente nao encontrado
        private void NotFinded()
        {
            if (EstoqueGrid.RowCount == 0)
            {
                NotFindDesc.Text = "Nenhum resultado que corresponda \n a sua pesquisa atual foi encontrado.";

                EstoqueGrid.Visible = false;
                Separator2.Visible = false;
                toolStripPaging.Visible = false;

                NotFind.Visible = true;

                NotFindDesc.Visible = true;
            }
            else
            {
                EstoqueGrid.Visible = true;
                Separator2.Visible = true;
                toolStripPaging.Visible = true;

                NotFind.Visible = false;
                NotFindDesc.Visible = false;
            }
        }

        private void TimerAnim_Tick(object sender, EventArgs e)
        {
            if (Opacity > 0.0)
            {
                Opacity -= 0.2;
            }
            else
                TimerAnim.Stop();
        }

        private void FormAnim_Tick(object sender, EventArgs e)
        {
            foreach (Form frm in fc)
            {
                if (frm.Name == "NovaEntrada")
                {
                    if (NewEntry.Opacity < 1.0)
                    {
                        NewEntry.Opacity += 0.2;
                    }
                    else
                    {
                        FormAnim.Stop();
                    }
                }
                else if (frm.Name == "NovaSaida")
                {
                    if (SaidaForm.Opacity < 1.0)
                    {
                        SaidaForm.Opacity += 0.2;
                    }
                    else
                    {
                        FormAnim.Stop();
                    }
                }
            }
        }

        // Ativar/desativar o dark mode
        private void SetColor()
        {
            this.BackColor = ThemeManager.FormBackColor;

            FrameName.BackColor = ThemeManager.FormBackColor;
            FrameName.ForeColor = ThemeManager.WhiteFontColor;

            Separator.FillColor = ThemeManager.SeparatorAndBorderColor;
            Separator2.FillColor = ThemeManager.SeparatorAndBorderColor;

            Minimize.IconColor = ThemeManager.CloseMinimizeIconColor;
            Minimize.HoverState.IconColor = ThemeManager.CloseMinimizeHoverIconColor;

            CloseBtn.IconColor = ThemeManager.CloseMinimizeIconColor;
            CloseBtn.HoverState.IconColor = ThemeManager.CloseMinimizeHoverIconColor;

            EstoqueGrid.BackgroundColor = ThemeManager.FormBackColor;
            EstoqueGrid.DefaultCellStyle.BackColor = ThemeManager.FormBackColor;
            EstoqueGrid.DefaultCellStyle.SelectionBackColor = ThemeManager.FormBackColor;
            EstoqueGrid.DefaultCellStyle.ForeColor = ThemeManager.GridForeColor;
            EstoqueGrid.DefaultCellStyle.SelectionForeColor = ThemeManager.FontColor;
            EstoqueGrid.GridColor = ThemeManager.SeparatorAndBorderColor;

            EstoqueGrid.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.WhiteFontColor;
            EstoqueGrid.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemeManager.WhiteFontColor;
            EstoqueGrid.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.FormBackColor;
            EstoqueGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemeManager.FormBackColor;

            EstoqueGrid.RowHeadersDefaultCellStyle.BackColor = ThemeManager.FormBackColor;

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

            Desc.ForeColor = ThemeManager.FontColor;
            NotFindDesc.ForeColor = ThemeManager.FontColor;

            MostrarText.BackColor = ThemeManager.FormBackColor;
            MostrarText.ForeColor = ThemeManager.FontColor;

            ToolTip.ForeColor = ThemeManager.GunaToolTipForeColor;
            ToolTip.BorderColor = ThemeManager.GunaToolTipBorderColor;
            ToolTip.BackColor = ThemeManager.GunaToolTipBackColor;
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Outros */

        private void Search_TextChanged(object sender, EventArgs e)
        {
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

                EstoqueGrid.Visible = true;
                Separator2.Visible = true;
                EraseText.Visible = false;
                toolStripPaging.Visible = true;

                ReloadPage();
                RefreshPagination();
            }
        }

        private void Search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
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

                    EstoqueGrid.Visible = true;
                    Separator2.Visible = true;
                    EraseText.Visible = false;

                    ReloadPage();
                    RefreshPagination();

                    toolStripPaging.Visible = true;
                }
            }
        }

        private void EraseText_Click(object sender, EventArgs e)
        {
            Search.Text = "";
            NotFind.Visible = false;
            NotFindDesc.Visible = false;

            ReloadPage();
            RefreshPagination();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            CloseFunction();
        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            foreach (Form frm in fc)
            {
                if (frm.Name == "NovaEntrada")
                {
                    frm.Opacity = 1.0d;
                }
                else if (frm.Name == "NovaSaida")
                {
                    frm.Opacity = 1.0d;
                }
            }
        }

        private void EstoqueGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (estoqueData.Estoque.Count > 0)
            {
                foreach (Form frm in fc)
                {
                    if (frm.Name == "NovaEntrada")
                    {
                        if (NovaEntrada.EntryFrame.NumberType == "Codigo")
                            NovaEntrada.EntryFrame.CodigoText.Text = EstoqueGrid.CurrentRow.Cells[0].Value.ToString();
                        else if (NovaEntrada.EntryFrame.NumberType == "NumeroFabricante")
                            NovaEntrada.EntryFrame.CodigoText.Text = EstoqueGrid.CurrentRow.Cells[2].Value.ToString();
                    }
                    else if (frm.Name == "NovaSaida")
                    {
                        if (NovaSaida.SaidaFrame.NumberType == "Codigo")
                            NovaSaida.SaidaFrame.CodigoText.Text = EstoqueGrid.CurrentRow.Cells[0].Value.ToString();
                        else if (NovaSaida.SaidaFrame.NumberType == "NumeroFabricante")
                            NovaSaida.SaidaFrame.CodigoText.Text = EstoqueGrid.CurrentRow.Cells[2].Value.ToString();
                    }
                }
            }

            CloseFunction();
        }

        private void PageItens_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageRows = int.Parse(PageItens.Text);

            if (CurrentPage > PagesCount - 1)
                CurrentPage = PagesCount - 1;

            PagesCount = Convert.ToInt32(Math.Ceiling(AllDataGrid.RowCount * 1.0 / PageRows));

            ReloadPage();
            RefreshPagination();
        }

        private void MostrarText_Click(object sender, EventArgs e)
        {
            if (PageItens.Enabled)
            {
                PageItens.DroppedDown = true;
                PageItens.Focus();
            }
        }
    }
}
