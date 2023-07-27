using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace TCC
{
    public partial class Main : Form
    {
        FormCollection fc = Application.OpenForms;

        private Form ActiveForm;

        Thread nt;

        bool SidebarHided = Properties.Settings.Default.SidebarHided;
        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateButtons = Properties.Settings.Default.AnimarBotoes;

        string OpenedForm;

        bool LeaveFormOpen;

        List<Guna.UI2.WinForms.Guna2Button> GunaButtons;
        List<Guna.UI2.WinForms.Guna2Separator> GunaSeparators;

        public Main()
        {
            InitializeComponent();

            ThemeManager.ChangeMode();

            AddControlsToList();
            SetColor();

            FormShow(new Home());

            Console.WriteLine(Properties.Settings.Default.RememberPassword);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (SidebarHided)
            {
                panel1.Width = 55;
                BarraVert.Location = new Point(54, -8);

                foreach (Control control in panel1.Controls)
                {
                    if (control.Name != "Logo" & control.Name != "LogoName" & control.Name != "BarraLateral")
                    {
                        control.Size = new Size(28, 28);
                    }
                }

                ToolTip.Active = true;
                ToolTip2.Active = false;
                HideBar.Image = Properties.Resources.show_bar;
            }
            else
            {
                panel1.Width = 190;
                BarraVert.Location = new Point(189, -8);

                foreach (Control control in panel1.Controls)
                {
                    if (control.Name != "Logo" & control.Name != "LogoName" & control.Name != "BarraLateral")
                    {
                        control.Size = new Size(166, 28);
                    }
                }

                ToolTip.Active = false;
                ToolTip2.Active = true;
                HideBar.Image = Properties.Resources.hide_bar;
            }

            if (IsDarkModeEnabled)
            {
                Inicio.Image = Properties.Resources.home_branco;
                Clientes.Image = Properties.Resources.clientes_branco;
                Estoque.Image = Properties.Resources.estoque_branco;
                EntradaDeItem.Image = Properties.Resources.entrada_item_branco;
                SaidaDeItem.Image = Properties.Resources.saida_item_branco;
                Contratos.Image = Properties.Resources.contratos_branco;
                Fornecedores.Image = Properties.Resources.delivery_branco;
                Funcionarios.Image = Properties.Resources.funcionarios_branco;
                Serviços.Image = Properties.Resources.serviços_branco;
                Configuracoes.Image = Properties.Resources.settings_branco;
                Sair.Image = Properties.Resources.logout_branco;
            }
            else
            {
                Inicio.Image = Properties.Resources.home;
                Clientes.Image = Properties.Resources.clientes;
                Estoque.Image = Properties.Resources.estoque;
                EntradaDeItem.Image = Properties.Resources.entrada_item_preto;
                SaidaDeItem.Image = Properties.Resources.saida_item_preto;
                Contratos.Image = Properties.Resources.contratos;
                Fornecedores.Image = Properties.Resources.delivery;
                Funcionarios.Image = Properties.Resources.funcionarios;
                Serviços.Image = Properties.Resources.serviços;
                Configuracoes.Image = Properties.Resources.settings;
                Sair.Image = Properties.Resources.logout;
            }

            Inicio.Image = Properties.Resources.home_red;
            Inicio.ForeColor = ThemeManager.RedFontColor;

            if (AnimateButtons)
            {
                foreach(Guna.UI2.WinForms.Guna2Button ct in GunaButtons)
                {
                    ct.Animated = true;
                }
            }
            else
            {
                foreach (Guna.UI2.WinForms.Guna2Button ct in GunaButtons)
                {
                    ct.Animated = false;
                }
            }

            VerifyTimer.Start();
        }

        // Salvar configuraçoes do usuario qnd fechar o programa
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.ShowPopups = Properties.Settings.Default.ShowPopups;
            Properties.Settings.Default.AnimarBotoes = Properties.Settings.Default.AnimarBotoes;
            Properties.Settings.Default.DarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
            Properties.Settings.Default.SidebarHided = Properties.Settings.Default.SidebarHided;
            Properties.Settings.Default.DoubleClickInGridEnabled = Properties.Settings.Default.DoubleClickInGridEnabled;
            Properties.Settings.Default.AutoCompleteCurrencyValues = Properties.Settings.Default.AutoCompleteCurrencyValues;
            Properties.Settings.Default.RememberPassword = Properties.Settings.Default.RememberPassword;
            Properties.Settings.Default.ItensPorPagina = Properties.Settings.Default.ItensPorPagina;
            Properties.Settings.Default.TipoDeLista = Properties.Settings.Default.TipoDeLista;
            Properties.Settings.Default.Save();

            Console.WriteLine(Properties.Settings.Default.ItensPorPagina);

            Application.Exit();
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Funcoes */

        // Delay
        async Task TaskDelay(int valor)
        {
            await Task.Delay(valor);
        }

        // Fechar form aberto
        private void ActveFormClose()
        {
            if (ActiveForm != null)
                ActiveForm.Close();
        }

        // Abrir form qnd clicar em algum botao especifico
        private void ActiveButton(Guna2Button Btn)
        {
            foreach (Control ct in panel1.Controls)
            {
                if (ct.Name != "LogoName")
                {
                    if (IsDarkModeEnabled != true)
                        ct.ForeColor = Color.Black;
                    else
                        ct.ForeColor = ThemeManager.FontColor;
                }

                if (IsDarkModeEnabled)
                    Btn.ForeColor = Color.FromArgb(255, 33, 0);
                else
                    Btn.ForeColor = Color.FromArgb(255, 3, 0);

                BarraLateral.Location = new Point(-1, Btn.Location.Y + 4);
            }
        }

        // Abrir form relacionado ao botao clicado
        private void FormShow(Form FormName)
        {
            if (OpenedForm != FormName.Name)
            {
                ActveFormClose();
                ActiveForm = FormName;
                FormName.TopLevel = false;
                FormName.FormBorderStyle = FormBorderStyle.None;
                FormName.Dock = DockStyle.Fill;
                AddForm.Controls.Add(FormName);
                FormName.BringToFront();
                FormName.Show();
            }

            if (ActiveForm == FormName)
                OpenedForm = FormName.Name;
        }

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

        // Adicionar itens a lista pra poder usar o dark/light mode
        private void AddControlsToList()
        {
            GunaButtons = new List<Guna.UI2.WinForms.Guna2Button>();
            GunaSeparators = new List<Guna.UI2.WinForms.Guna2Separator>();

            Guna.UI2.WinForms.Guna2Button[] Buttons = new Guna.UI2.WinForms.Guna2Button[11]
            {
                Inicio, Clientes, Estoque, EntradaDeItem, SaidaDeItem, Contratos, Fornecedores, Funcionarios, Serviços, Configuracoes, Sair
            };

            Guna.UI2.WinForms.Guna2Separator[] Separators = new Guna.UI2.WinForms.Guna2Separator[2]
            {
                Separator1, Separator2
            };

            GunaButtons.AddRange(Buttons);
            GunaSeparators.AddRange(Separators);
        }

        // Ativar/desativar o dark mode
        private void SetColor()
        {
            panel1.BackColor = ThemeManager.AppBordersColor;
            panel2.BackColor = ThemeManager.AppBordersColor;

            BarraHor.BackColor = ThemeManager.AppBordersBarColor;
            BarraVert.BackColor = ThemeManager.AppBordersBarColor;

            LogoName.ForeColor = ThemeManager.WhiteFontColor;

            EstoqueSeparator.FillColor = ThemeManager.AppBordersBarColor;

            ToolTip.ForeColor = ThemeManager.GunaToolTipForeColor;
            ToolTip.BorderColor = ThemeManager.GunaToolTipBorderColor;
            ToolTip.BackColor = ThemeManager.GunaToolTipBackColor;

            ToolTip2.ForeColor = ThemeManager.GunaToolTipForeColor;
            ToolTip2.BorderColor = ThemeManager.GunaToolTipBorderColor;
            ToolTip2.BackColor = ThemeManager.GunaToolTipBackColor;

            AddForm.BackColor = ThemeManager.FormBackColor;

            // Botoes normais
            foreach (Guna.UI2.WinForms.Guna2Button ct in GunaButtons)
            {
                ct.ForeColor = ThemeManager.FontColor;
                ct.FillColor = ThemeManager.StartButtonFillColor;
                ct.PressedColor = ThemeManager.StartButtonPressedColor;
                ct.HoverState.FillColor = ThemeManager.StartButtonHoverStateColor;
                ct.CheckedState.FillColor = ThemeManager.StartButtonCheckedStateColor;
            }

            // Separators
            foreach (Guna.UI2.WinForms.Guna2Separator ct in GunaSeparators)
            {
                ct.FillColor = ThemeManager.AppBordersBarColor;
            }
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Outros */

        private void Inicio_Click(object sender, EventArgs e)
        {        
            FormShow(new Home());
            ActiveButton(Inicio);

            if (IsDarkModeEnabled)
            {
                Inicio.Image = Properties.Resources.home_red; Clientes.Image = Properties.Resources.clientes_branco; Estoque.Image = Properties.Resources.estoque_branco; EntradaDeItem.Image = Properties.Resources.entrada_item_branco; SaidaDeItem.Image = Properties.Resources.saida_item_branco;
                Contratos.Image = Properties.Resources.contratos_branco; Fornecedores.Image = Properties.Resources.delivery_branco; Funcionarios.Image = Properties.Resources.funcionarios_branco;
                Serviços.Image = Properties.Resources.serviços_branco; Configuracoes.Image = Properties.Resources.settings_branco; Sair.Image = Properties.Resources.logout_branco;

            }
            else
            {
                Inicio.Image = Properties.Resources.home_red; Clientes.Image = Properties.Resources.clientes; Estoque.Image = Properties.Resources.estoque; EntradaDeItem.Image = Properties.Resources.entrada_item_preto; SaidaDeItem.Image = Properties.Resources.saida_item_preto;
                Contratos.Image = Properties.Resources.contratos; Fornecedores.Image = Properties.Resources.delivery; Funcionarios.Image = Properties.Resources.funcionarios;
                Serviços.Image = Properties.Resources.serviços; Configuracoes.Image = Properties.Resources.settings; Sair.Image = Properties.Resources.logout;
            }
        }

        private void Clientes_Click(object sender, EventArgs e)
        {
            FormShow(new Clientes());
            ActiveButton(Clientes);

            if (IsDarkModeEnabled)
            {
                Inicio.Image = Properties.Resources.home_branco; Clientes.Image = Properties.Resources.clientes_red; Estoque.Image = Properties.Resources.estoque_branco; EntradaDeItem.Image = Properties.Resources.entrada_item_branco; SaidaDeItem.Image = Properties.Resources.saida_item_branco;
                Contratos.Image = Properties.Resources.contratos_branco; Fornecedores.Image = Properties.Resources.delivery_branco; Funcionarios.Image = Properties.Resources.funcionarios_branco;
                Serviços.Image = Properties.Resources.serviços_branco; Configuracoes.Image = Properties.Resources.settings_branco; Sair.Image = Properties.Resources.logout_branco;

            }

            else
            {
                Inicio.Image = Properties.Resources.home; Clientes.Image = Properties.Resources.clientes_red; Estoque.Image = Properties.Resources.estoque; EntradaDeItem.Image = Properties.Resources.entrada_item_preto; SaidaDeItem.Image = Properties.Resources.saida_item_preto;
                Contratos.Image = Properties.Resources.contratos; Fornecedores.Image = Properties.Resources.delivery; Funcionarios.Image = Properties.Resources.funcionarios;
                Serviços.Image = Properties.Resources.serviços; Configuracoes.Image = Properties.Resources.settings; Sair.Image = Properties.Resources.logout;
            }
        }

        private void Estoque_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FormShow(new TCC.Principais.Estoque());
                ActiveButton(Estoque);

                if (IsDarkModeEnabled)
                {
                    Inicio.Image = Properties.Resources.home_branco; Clientes.Image = Properties.Resources.clientes_branco; Estoque.Image = Properties.Resources.estoque_red; EntradaDeItem.Image = Properties.Resources.entrada_item_branco; SaidaDeItem.Image = Properties.Resources.saida_item_branco;
                    Contratos.Image = Properties.Resources.contratos_branco; Fornecedores.Image = Properties.Resources.delivery_branco; Funcionarios.Image = Properties.Resources.funcionarios_branco;
                    Serviços.Image = Properties.Resources.serviços_branco; Configuracoes.Image = Properties.Resources.settings_branco; Sair.Image = Properties.Resources.logout_branco;

                }

                else
                {
                    Inicio.Image = Properties.Resources.home; Clientes.Image = Properties.Resources.clientes; Estoque.Image = Properties.Resources.estoque_red; EntradaDeItem.Image = Properties.Resources.entrada_item_preto; SaidaDeItem.Image = Properties.Resources.saida_item_preto;
                    Contratos.Image = Properties.Resources.contratos; Fornecedores.Image = Properties.Resources.delivery; Funcionarios.Image = Properties.Resources.funcionarios;
                    Serviços.Image = Properties.Resources.serviços; Configuracoes.Image = Properties.Resources.settings; Sair.Image = Properties.Resources.logout;
                }
            }

            else if (e.Button == MouseButtons.Right)
            {
                if (SidebarHided)
                {
                    if (SaidaDeItem.Visible != true && SaidaDeItem.Visible != true)
                    {
                        EntradaDeItem.Visible = true;
                        SaidaDeItem.Visible = true;

                        EntradaDeItem.Location = new Point(13, EntradaDeItem.Location.Y);
                        SaidaDeItem.Location = new Point(13, SaidaDeItem.Location.Y);

                        Contratos.Location = new Point(Contratos.Location.X, 274);
                        Fornecedores.Location = new Point(Fornecedores.Location.X, 274);
                        Funcionarios.Location = new Point(Funcionarios.Location.X, 316);
                        Serviços.Location = new Point(Serviços.Location.X, 358);

                        EstoqueSeparator.Size = new Size(EstoqueSeparator.Size.Width, 78);

                        EstoqueSeparator.Location = new Point(Estoque.Location.X, EstoqueSeparator.Location.Y + 2);

                        Separator1.Location = new Point(Estoque.Location.X, Estoque.Location.Y - 20);
                        Separator2.Location = new Point(Contratos.Location.X, Contratos.Location.Y - 20);

                        Separator1.Visible = true;
                        Separator2.Visible = true;

                        EstoqueSeparator.Visible = true;
                    }
                    else
                    {
                        EntradaDeItem.Visible = false;
                        SaidaDeItem.Visible = false;

                        EntradaDeItem.Location = new Point(103, EntradaDeItem.Location.Y);
                        SaidaDeItem.Location = new Point(103, SaidaDeItem.Location.Y);

                        Contratos.Location = new Point(Contratos.Location.X, 190);
                        Fornecedores.Location = new Point(Fornecedores.Location.X, 190);
                        Funcionarios.Location = new Point(Funcionarios.Location.X, 232);
                        Serviços.Location = new Point(Serviços.Location.X, 274);

                        EstoqueSeparator.Size = new Size(EstoqueSeparator.Size.Width, 78);

                        EstoqueSeparator.Location = new Point(Estoque.Location.X, EstoqueSeparator.Location.Y - 1);

                        Separator1.Location = new Point(Separator1.Location.X, Separator1.Location.Y + 20);
                        Separator2.Location = new Point(Separator2.Location.X, Separator2.Location.Y + 16);

                        Separator1.Visible = false;
                        Separator2.Visible = false;

                        EstoqueSeparator.Visible = false;
                    }
                }
                else
                {
                    EstoqueSeparator.Visible = false;
                }
            }
        }

        private void EntradaDeItem_Click(object sender, EventArgs e)
        {
            FormShow(new TCC.Principais.EntradaDeItens());
            ActiveButton(EntradaDeItem);

            if (IsDarkModeEnabled)
            {
                Inicio.Image = Properties.Resources.home_branco; Clientes.Image = Properties.Resources.clientes_branco; Estoque.Image = Properties.Resources.estoque_branco; EntradaDeItem.Image = Properties.Resources.entrada_item_red; SaidaDeItem.Image = Properties.Resources.saida_item_branco;
                Contratos.Image = Properties.Resources.contratos_branco; Fornecedores.Image = Properties.Resources.delivery_branco; Funcionarios.Image = Properties.Resources.funcionarios_branco;
                Serviços.Image = Properties.Resources.serviços_branco; Configuracoes.Image = Properties.Resources.settings_branco; Sair.Image = Properties.Resources.logout_branco;

            }

            else
            {
                Inicio.Image = Properties.Resources.home; Clientes.Image = Properties.Resources.clientes; Estoque.Image = Properties.Resources.estoque; EntradaDeItem.Image = Properties.Resources.entrada_item_red; SaidaDeItem.Image = Properties.Resources.saida_item_preto;
                Contratos.Image = Properties.Resources.contratos; Fornecedores.Image = Properties.Resources.delivery; Funcionarios.Image = Properties.Resources.funcionarios;
                Serviços.Image = Properties.Resources.serviços; Configuracoes.Image = Properties.Resources.settings; Sair.Image = Properties.Resources.logout;
            }
        }

        private void SaidaDeItem_Click(object sender, EventArgs e)
        {
            FormShow(new TCC.Principais.SaidaDeItens());
            ActiveButton(SaidaDeItem);

            if (IsDarkModeEnabled)
            {
                Inicio.Image = Properties.Resources.home_branco; Clientes.Image = Properties.Resources.clientes_branco; Estoque.Image = Properties.Resources.estoque_branco; EntradaDeItem.Image = Properties.Resources.entrada_item_branco; SaidaDeItem.Image = Properties.Resources.saida_item_red;
                Contratos.Image = Properties.Resources.contratos_branco; Fornecedores.Image = Properties.Resources.delivery_branco; Funcionarios.Image = Properties.Resources.funcionarios_branco;
                Serviços.Image = Properties.Resources.serviços_branco; Configuracoes.Image = Properties.Resources.settings_branco; Sair.Image = Properties.Resources.logout_branco;

            }

            else
            {
                Inicio.Image = Properties.Resources.home; Clientes.Image = Properties.Resources.clientes; Estoque.Image = Properties.Resources.estoque_red; EntradaDeItem.Image = Properties.Resources.entrada_item_preto; SaidaDeItem.Image = Properties.Resources.saida_item_red;
                Contratos.Image = Properties.Resources.contratos; Fornecedores.Image = Properties.Resources.delivery; Funcionarios.Image = Properties.Resources.funcionarios;
                Serviços.Image = Properties.Resources.serviços; Configuracoes.Image = Properties.Resources.settings; Sair.Image = Properties.Resources.logout;
            }
        }

        private void Fornecedores_Click(object sender, EventArgs e)
        {
            FormShow(new Fornecedores());
            ActiveButton(Fornecedores);

            if (IsDarkModeEnabled)
            {
                Inicio.Image = Properties.Resources.home_branco; Clientes.Image = Properties.Resources.clientes_branco; Estoque.Image = Properties.Resources.estoque_branco; EntradaDeItem.Image = Properties.Resources.entrada_item_branco; SaidaDeItem.Image = Properties.Resources.saida_item_branco;
                Contratos.Image = Properties.Resources.contratos_branco; Fornecedores.Image = Properties.Resources.delivery_red; Funcionarios.Image = Properties.Resources.funcionarios_branco;
                Serviços.Image = Properties.Resources.serviços_branco; Configuracoes.Image = Properties.Resources.settings_branco; Sair.Image = Properties.Resources.logout_branco;

            }

            else
            {
                Inicio.Image = Properties.Resources.home; Clientes.Image = Properties.Resources.clientes; Estoque.Image = Properties.Resources.estoque; EntradaDeItem.Image = Properties.Resources.entrada_item_preto; SaidaDeItem.Image = Properties.Resources.saida_item_preto;
                Contratos.Image = Properties.Resources.contratos; Fornecedores.Image = Properties.Resources.delivery_red; Funcionarios.Image = Properties.Resources.funcionarios;
                Serviços.Image = Properties.Resources.serviços; Configuracoes.Image = Properties.Resources.settings; Sair.Image = Properties.Resources.logout;
            }
        }

        private void Funcionarios_Click(object sender, EventArgs e)
        {
            FormShow(new Funcionarios());
            ActiveButton(Funcionarios);

            if (IsDarkModeEnabled)
            {
                Inicio.Image = Properties.Resources.home_branco; Clientes.Image = Properties.Resources.clientes_branco; Estoque.Image = Properties.Resources.estoque_branco; EntradaDeItem.Image = Properties.Resources.entrada_item_branco; SaidaDeItem.Image = Properties.Resources.saida_item_branco;
                Contratos.Image = Properties.Resources.contratos_branco; Fornecedores.Image = Properties.Resources.delivery_branco; Funcionarios.Image = Properties.Resources.funcionarios_red;
                Serviços.Image = Properties.Resources.serviços_branco; Configuracoes.Image = Properties.Resources.settings_branco; Sair.Image = Properties.Resources.logout_branco;

            }

            else
            {
                Inicio.Image = Properties.Resources.home; Clientes.Image = Properties.Resources.clientes; Estoque.Image = Properties.Resources.estoque; EntradaDeItem.Image = Properties.Resources.entrada_item_preto; SaidaDeItem.Image = Properties.Resources.saida_item_preto;
                Contratos.Image = Properties.Resources.contratos; Fornecedores.Image = Properties.Resources.delivery; Funcionarios.Image = Properties.Resources.funcionarios_red;
                Serviços.Image = Properties.Resources.serviços; Configuracoes.Image = Properties.Resources.settings; Sair.Image = Properties.Resources.logout;
            }
        }

        private void Serviços_Click(object sender, EventArgs e)
        {
            FormShow(new Principais.Servicos());
            ActiveButton(Serviços);

            if (IsDarkModeEnabled)
            {
                Inicio.Image = Properties.Resources.home_branco; Clientes.Image = Properties.Resources.clientes_branco; Estoque.Image = Properties.Resources.estoque_branco; EntradaDeItem.Image = Properties.Resources.entrada_item_branco; SaidaDeItem.Image = Properties.Resources.saida_item_branco;
                Contratos.Image = Properties.Resources.contratos_branco; Fornecedores.Image = Properties.Resources.delivery_branco; Funcionarios.Image = Properties.Resources.funcionarios_branco;
                Serviços.Image = Properties.Resources.serviços_red; Configuracoes.Image = Properties.Resources.settings_branco; Sair.Image = Properties.Resources.logout_branco;

            }

            else
            {
                Inicio.Image = Properties.Resources.home; Clientes.Image = Properties.Resources.clientes; Estoque.Image = Properties.Resources.estoque; EntradaDeItem.Image = Properties.Resources.entrada_item_preto; SaidaDeItem.Image = Properties.Resources.saida_item_preto;
                Contratos.Image = Properties.Resources.contratos; Fornecedores.Image = Properties.Resources.delivery; Funcionarios.Image = Properties.Resources.funcionarios;
                Serviços.Image = Properties.Resources.serviços_red; Configuracoes.Image = Properties.Resources.settings; Sair.Image = Properties.Resources.logout;
            }
        }

        private void Configuracoes_Click(object sender, EventArgs e)
        {
            FormShow(new Principais.Configuraçoes());
            ActiveButton(Configuracoes);

            if (IsDarkModeEnabled)
            {
                Inicio.Image = Properties.Resources.home_branco; Clientes.Image = Properties.Resources.clientes_branco; Estoque.Image = Properties.Resources.estoque_branco; EntradaDeItem.Image = Properties.Resources.entrada_item_branco; SaidaDeItem.Image = Properties.Resources.saida_item_branco;
                Contratos.Image = Properties.Resources.contratos_branco; Fornecedores.Image = Properties.Resources.delivery_branco; Funcionarios.Image = Properties.Resources.funcionarios_branco;
                Serviços.Image = Properties.Resources.serviços_branco; Configuracoes.Image = Properties.Resources.settings_red; Sair.Image = Properties.Resources.logout_branco;

            }

            else
            {
                Inicio.Image = Properties.Resources.home; Clientes.Image = Properties.Resources.clientes; Estoque.Image = Properties.Resources.estoque; EntradaDeItem.Image = Properties.Resources.entrada_item_preto; SaidaDeItem.Image = Properties.Resources.saida_item_preto;
                Contratos.Image = Properties.Resources.contratos; Fornecedores.Image = Properties.Resources.delivery; Funcionarios.Image = Properties.Resources.funcionarios;
                Serviços.Image = Properties.Resources.serviços; Configuracoes.Image = Properties.Resources.settings_red; Sair.Image = Properties.Resources.logout;
            }
        }

        // Expandir/esconder barra lateral
        private void Hide_Click(object sender, EventArgs e)
        {
            if (panel1.Width == 190)
            {
                panel1.Width = 56;
                BarraVert.Location = new Point(55, -8);

                foreach (Control control in panel1.Controls)
                {
                    if (control.Name != "Logo" & control.Name != "LogoName" & control.Name != "BarraLateral")
                    {
                        control.Size = new Size(28, 28);
                    }
                }

                ToolTip.Active = true;
                ToolTip2.Active = false;
                HideBar.Image = Properties.Resources.show_bar;

                SidebarHided = true;

                if (EntradaDeItem.Visible && SaidaDeItem.Visible)
                {

                    EstoqueSeparator.Size = new Size(EstoqueSeparator.Size.Width, 58);
                    EstoqueSeparator.Location = new Point(Estoque.Location.X, EstoqueSeparator.Location.Y + 2);
                    EstoqueSeparator.Visible = true;
                }

                Properties.Settings.Default.SidebarHided = SidebarHided;
            }
            else
            {
                panel1.Width = 190;
                BarraVert.Location = new Point(189, -8);

                foreach (Control control in panel1.Controls)
                {
                    if (control.Name != "Logo" & control.Name != "LogoName" & control.Name != "BarraLateral")
                    {
                        control.Size = new Size(162, 28);
                    }
                }

                ToolTip.Active = false;
                ToolTip2.Active = true;
                HideBar.Image = Properties.Resources.hide_bar;

                SidebarHided = false;

                EstoqueSeparator.Size = new Size(EstoqueSeparator.Size.Width, 58);
                EstoqueSeparator.Location = new Point(Estoque.Location.X, EstoqueSeparator.Location.Y - 2);
                EstoqueSeparator.Visible = false;

                Properties.Settings.Default.SidebarHided = SidebarHided;
            }
        }

        private void MoreEstoqueItensBtn_Click(object sender, EventArgs e)
        {
            if (SaidaDeItem.Visible != true && SaidaDeItem.Visible != true)
            {
                EntradaDeItem.Visible = true;
                SaidaDeItem.Visible = true;

                EntradaDeItem.Location = new Point(13, EntradaDeItem.Location.Y);
                SaidaDeItem.Location = new Point(13, SaidaDeItem.Location.Y);

                Contratos.Location = new Point(Contratos.Location.X, 274);
                Fornecedores.Location = new Point(Fornecedores.Location.X, 316);
                Funcionarios.Location = new Point(Funcionarios.Location.X, 360);
                Serviços.Location = new Point(Serviços.Location.X, 402);

                EstoqueSeparator.Size = new Size(EstoqueSeparator.Size.Width, 70);

                EstoqueSeparator.Location = new Point(Estoque.Location.X, EstoqueSeparator.Location.Y + 2);

                Separator1.Location = new Point(Estoque.Location.X, Estoque.Location.Y - 20);
                Separator2.Location = new Point(Contratos.Location.X, Contratos.Location.Y - 20);

                Separator1.Visible = true;
                Separator2.Visible = true;

                EstoqueSeparator.Visible = false;
            }
            else
            {
                EntradaDeItem.Visible = false;
                SaidaDeItem.Visible = false;

                EntradaDeItem.Location = new Point(103, EntradaDeItem.Location.Y);
                SaidaDeItem.Location = new Point(103, SaidaDeItem.Location.Y);

                Contratos.Location = new Point(Contratos.Location.X, 190);
                Fornecedores.Location = new Point(Fornecedores.Location.X, 232);
                Funcionarios.Location = new Point(Funcionarios.Location.X, 274);
                Serviços.Location = new Point(Serviços.Location.X, 316);

                EstoqueSeparator.Size = new Size(EstoqueSeparator.Size.Width, 70);

                EstoqueSeparator.Location = new Point(Estoque.Location.X, EstoqueSeparator.Location.Y - 1);

                Separator1.Location = new Point(Separator1.Location.X, Separator1.Location.Y + 20);
                Separator2.Location = new Point(Separator2.Location.X, Separator2.Location.Y + 16);

                Separator1.Visible = false;
                Separator2.Visible = false;

                EstoqueSeparator.Visible = false;
            }
        }

        private void Sair_Click(object sender, EventArgs e)
        {
            foreach (Form frm in fc)
            {
                if (frm.Name != "LeaveCofirm")
                    LeaveFormOpen = false;
                else
                    LeaveFormOpen = true;
            }

            if (LeaveFormOpen != true)
            {
                LeaveFormOpen = true;

                ThreadStart ts = new ThreadStart(() => {
                    DarkBackground(new Frames.LeaveConfirm());
                });

                Thread t = new Thread(ts);

                t.SetApartmentState(ApartmentState.STA);

                t.Start();
            }
        }

        private async void VerifyTimer_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.CanShowLoginForm)
            {
                Properties.Settings.Default.CanShowLoginForm = false;

                await TaskDelay(100);

                Properties.Settings.Default.RememberPassword = false;

                Login LoginForm = new Login();

                LoginForm.Show();

                Hide();
            }
        }
    }
}
