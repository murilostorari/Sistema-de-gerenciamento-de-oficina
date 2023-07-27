
namespace TCC
{
    partial class Main
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.panel2 = new System.Windows.Forms.Panel();
            this.HideBar = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MoreEstoqueItensBtn = new System.Windows.Forms.PictureBox();
            this.SaidaDeItem = new Guna.UI2.WinForms.Guna2Button();
            this.EntradaDeItem = new Guna.UI2.WinForms.Guna2Button();
            this.Sair = new Guna.UI2.WinForms.Guna2Button();
            this.Configuracoes = new Guna.UI2.WinForms.Guna2Button();
            this.BarraLateral = new System.Windows.Forms.PictureBox();
            this.Serviços = new Guna.UI2.WinForms.Guna2Button();
            this.Funcionarios = new Guna.UI2.WinForms.Guna2Button();
            this.Fornecedores = new Guna.UI2.WinForms.Guna2Button();
            this.Contratos = new Guna.UI2.WinForms.Guna2Button();
            this.Inicio = new Guna.UI2.WinForms.Guna2Button();
            this.Clientes = new Guna.UI2.WinForms.Guna2Button();
            this.LogoName = new System.Windows.Forms.Label();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.EstoqueSeparator = new Guna.UI2.WinForms.Guna2VSeparator();
            this.Separator2 = new Guna.UI2.WinForms.Guna2Separator();
            this.Estoque = new Guna.UI2.WinForms.Guna2Button();
            this.Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.BarraHor = new System.Windows.Forms.Panel();
            this.BarraVert = new System.Windows.Forms.Panel();
            this.AddForm = new System.Windows.Forms.Panel();
            this.ToolTip = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.ToolTip2 = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.VerifyTimer = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HideBar)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MoreEstoqueItensBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarraLateral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panel2.Controls.Add(this.HideBar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(190, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(858, 56);
            this.panel2.TabIndex = 3;
            // 
            // HideBar
            // 
            this.HideBar.BackColor = System.Drawing.Color.Transparent;
            this.HideBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HideBar.Image = global::TCC.Properties.Resources.hide_bar;
            this.HideBar.Location = new System.Drawing.Point(6, 15);
            this.HideBar.Margin = new System.Windows.Forms.Padding(0);
            this.HideBar.Name = "HideBar";
            this.HideBar.Size = new System.Drawing.Size(26, 26);
            this.HideBar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.HideBar.TabIndex = 2;
            this.HideBar.TabStop = false;
            this.ToolTip.SetToolTip(this.HideBar, "Expandir menu");
            this.ToolTip2.SetToolTip(this.HideBar, "Esconder menu");
            this.HideBar.Click += new System.EventHandler(this.Hide_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panel1.Controls.Add(this.MoreEstoqueItensBtn);
            this.panel1.Controls.Add(this.SaidaDeItem);
            this.panel1.Controls.Add(this.EntradaDeItem);
            this.panel1.Controls.Add(this.Sair);
            this.panel1.Controls.Add(this.Configuracoes);
            this.panel1.Controls.Add(this.BarraLateral);
            this.panel1.Controls.Add(this.Serviços);
            this.panel1.Controls.Add(this.Funcionarios);
            this.panel1.Controls.Add(this.Fornecedores);
            this.panel1.Controls.Add(this.Contratos);
            this.panel1.Controls.Add(this.Inicio);
            this.panel1.Controls.Add(this.Clientes);
            this.panel1.Controls.Add(this.LogoName);
            this.panel1.Controls.Add(this.Logo);
            this.panel1.Controls.Add(this.EstoqueSeparator);
            this.panel1.Controls.Add(this.Separator2);
            this.panel1.Controls.Add(this.Estoque);
            this.panel1.Controls.Add(this.Separator1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.MaximumSize = new System.Drawing.Size(190, 2000);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 701);
            this.panel1.TabIndex = 4;
            // 
            // MoreEstoqueItensBtn
            // 
            this.MoreEstoqueItensBtn.BackColor = System.Drawing.Color.Transparent;
            this.MoreEstoqueItensBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MoreEstoqueItensBtn.Image = ((System.Drawing.Image)(resources.GetObject("MoreEstoqueItensBtn.Image")));
            this.MoreEstoqueItensBtn.Location = new System.Drawing.Point(164, 159);
            this.MoreEstoqueItensBtn.Margin = new System.Windows.Forms.Padding(0);
            this.MoreEstoqueItensBtn.Name = "MoreEstoqueItensBtn";
            this.MoreEstoqueItensBtn.Size = new System.Drawing.Size(12, 12);
            this.MoreEstoqueItensBtn.TabIndex = 30;
            this.MoreEstoqueItensBtn.TabStop = false;
            this.ToolTip.SetToolTip(this.MoreEstoqueItensBtn, "Mostrar mais opções de estoque");
            this.ToolTip2.SetToolTip(this.MoreEstoqueItensBtn, "Mostrar mais opções de estoque");
            this.MoreEstoqueItensBtn.Click += new System.EventHandler(this.MoreEstoqueItensBtn_Click);
            // 
            // SaidaDeItem
            // 
            this.SaidaDeItem.Animated = true;
            this.SaidaDeItem.BackColor = System.Drawing.Color.Transparent;
            this.SaidaDeItem.BorderColor = System.Drawing.Color.Empty;
            this.SaidaDeItem.BorderRadius = 5;
            this.SaidaDeItem.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.SaidaDeItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaidaDeItem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.SaidaDeItem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.SaidaDeItem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.SaidaDeItem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.SaidaDeItem.FillColor = System.Drawing.Color.Empty;
            this.SaidaDeItem.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.SaidaDeItem.ForeColor = System.Drawing.Color.Black;
            this.SaidaDeItem.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.SaidaDeItem.Image = ((System.Drawing.Image)(resources.GetObject("SaidaDeItem.Image")));
            this.SaidaDeItem.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.SaidaDeItem.ImageOffset = new System.Drawing.Point(-6, 1);
            this.SaidaDeItem.Location = new System.Drawing.Point(203, 232);
            this.SaidaDeItem.Margin = new System.Windows.Forms.Padding(0);
            this.SaidaDeItem.Name = "SaidaDeItem";
            this.SaidaDeItem.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.SaidaDeItem.Size = new System.Drawing.Size(166, 28);
            this.SaidaDeItem.TabIndex = 23;
            this.SaidaDeItem.Text = "Saída de itens";
            this.SaidaDeItem.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.SaidaDeItem.TextOffset = new System.Drawing.Point(-2, 0);
            this.ToolTip.SetToolTip(this.SaidaDeItem, "Saída de itens do estoque");
            this.SaidaDeItem.Visible = false;
            this.SaidaDeItem.Click += new System.EventHandler(this.SaidaDeItem_Click);
            // 
            // EntradaDeItem
            // 
            this.EntradaDeItem.Animated = true;
            this.EntradaDeItem.BackColor = System.Drawing.Color.Transparent;
            this.EntradaDeItem.BorderColor = System.Drawing.Color.Empty;
            this.EntradaDeItem.BorderRadius = 5;
            this.EntradaDeItem.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.EntradaDeItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EntradaDeItem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.EntradaDeItem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.EntradaDeItem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.EntradaDeItem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.EntradaDeItem.FillColor = System.Drawing.Color.Empty;
            this.EntradaDeItem.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.EntradaDeItem.ForeColor = System.Drawing.Color.Black;
            this.EntradaDeItem.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.EntradaDeItem.Image = global::TCC.Properties.Resources.entrada_item_preto;
            this.EntradaDeItem.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.EntradaDeItem.ImageOffset = new System.Drawing.Point(-6, 1);
            this.EntradaDeItem.Location = new System.Drawing.Point(213, 190);
            this.EntradaDeItem.Margin = new System.Windows.Forms.Padding(0);
            this.EntradaDeItem.Name = "EntradaDeItem";
            this.EntradaDeItem.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.EntradaDeItem.Size = new System.Drawing.Size(166, 28);
            this.EntradaDeItem.TabIndex = 22;
            this.EntradaDeItem.Text = "Entrada de itens";
            this.EntradaDeItem.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.EntradaDeItem.TextOffset = new System.Drawing.Point(-2, 0);
            this.ToolTip.SetToolTip(this.EntradaDeItem, "Entrada de itens no estoque");
            this.EntradaDeItem.Visible = false;
            this.EntradaDeItem.Click += new System.EventHandler(this.EntradaDeItem_Click);
            // 
            // Sair
            // 
            this.Sair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Sair.Animated = true;
            this.Sair.BackColor = System.Drawing.Color.Transparent;
            this.Sair.BorderColor = System.Drawing.Color.Empty;
            this.Sair.BorderRadius = 5;
            this.Sair.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Sair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Sair.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Sair.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Sair.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Sair.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Sair.FillColor = System.Drawing.Color.Empty;
            this.Sair.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.Sair.ForeColor = System.Drawing.Color.Black;
            this.Sair.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.Sair.Image = global::TCC.Properties.Resources.logout;
            this.Sair.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Sair.ImageOffset = new System.Drawing.Point(-6, 1);
            this.Sair.Location = new System.Drawing.Point(13, 665);
            this.Sair.Margin = new System.Windows.Forms.Padding(0);
            this.Sair.Name = "Sair";
            this.Sair.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Sair.Size = new System.Drawing.Size(166, 28);
            this.Sair.TabIndex = 20;
            this.Sair.Text = "Sair";
            this.Sair.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Sair.TextOffset = new System.Drawing.Point(-5, 0);
            this.ToolTip.SetToolTip(this.Sair, "Sair");
            this.Sair.Click += new System.EventHandler(this.Sair_Click);
            // 
            // Configuracoes
            // 
            this.Configuracoes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Configuracoes.Animated = true;
            this.Configuracoes.BackColor = System.Drawing.Color.Transparent;
            this.Configuracoes.BorderColor = System.Drawing.Color.Empty;
            this.Configuracoes.BorderRadius = 5;
            this.Configuracoes.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Configuracoes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Configuracoes.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Configuracoes.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Configuracoes.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Configuracoes.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Configuracoes.FillColor = System.Drawing.Color.Empty;
            this.Configuracoes.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.Configuracoes.ForeColor = System.Drawing.Color.Black;
            this.Configuracoes.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.Configuracoes.Image = global::TCC.Properties.Resources.settings;
            this.Configuracoes.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Configuracoes.ImageOffset = new System.Drawing.Point(-6, 1);
            this.Configuracoes.ImageSize = new System.Drawing.Size(18, 18);
            this.Configuracoes.Location = new System.Drawing.Point(13, 623);
            this.Configuracoes.Margin = new System.Windows.Forms.Padding(0);
            this.Configuracoes.Name = "Configuracoes";
            this.Configuracoes.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Configuracoes.Size = new System.Drawing.Size(166, 28);
            this.Configuracoes.TabIndex = 19;
            this.Configuracoes.Text = "Configurações";
            this.Configuracoes.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Configuracoes.TextOffset = new System.Drawing.Point(-2, 0);
            this.ToolTip.SetToolTip(this.Configuracoes, "Configurações");
            this.Configuracoes.Click += new System.EventHandler(this.Configuracoes_Click);
            // 
            // BarraLateral
            // 
            this.BarraLateral.Image = ((System.Drawing.Image)(resources.GetObject("BarraLateral.Image")));
            this.BarraLateral.Location = new System.Drawing.Point(-1, 67);
            this.BarraLateral.Margin = new System.Windows.Forms.Padding(0);
            this.BarraLateral.Name = "BarraLateral";
            this.BarraLateral.Size = new System.Drawing.Size(3, 23);
            this.BarraLateral.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.BarraLateral.TabIndex = 18;
            this.BarraLateral.TabStop = false;
            // 
            // Serviços
            // 
            this.Serviços.Animated = true;
            this.Serviços.BackColor = System.Drawing.Color.Transparent;
            this.Serviços.BorderColor = System.Drawing.Color.Empty;
            this.Serviços.BorderRadius = 5;
            this.Serviços.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Serviços.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Serviços.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Serviços.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Serviços.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Serviços.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Serviços.FillColor = System.Drawing.Color.Empty;
            this.Serviços.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.Serviços.ForeColor = System.Drawing.Color.Black;
            this.Serviços.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.Serviços.Image = global::TCC.Properties.Resources.serviços;
            this.Serviços.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Serviços.ImageOffset = new System.Drawing.Point(-6, 1);
            this.Serviços.ImageSize = new System.Drawing.Size(18, 18);
            this.Serviços.Location = new System.Drawing.Point(13, 274);
            this.Serviços.Margin = new System.Windows.Forms.Padding(0);
            this.Serviços.Name = "Serviços";
            this.Serviços.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Serviços.Size = new System.Drawing.Size(166, 28);
            this.Serviços.TabIndex = 7;
            this.Serviços.Text = "Serviços";
            this.Serviços.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ToolTip.SetToolTip(this.Serviços, "Serviços");
            this.Serviços.Click += new System.EventHandler(this.Serviços_Click);
            // 
            // Funcionarios
            // 
            this.Funcionarios.Animated = true;
            this.Funcionarios.BackColor = System.Drawing.Color.Transparent;
            this.Funcionarios.BorderColor = System.Drawing.Color.Empty;
            this.Funcionarios.BorderRadius = 5;
            this.Funcionarios.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Funcionarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Funcionarios.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Funcionarios.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Funcionarios.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Funcionarios.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Funcionarios.FillColor = System.Drawing.Color.Empty;
            this.Funcionarios.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.Funcionarios.ForeColor = System.Drawing.Color.Black;
            this.Funcionarios.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.Funcionarios.Image = global::TCC.Properties.Resources.funcionarios;
            this.Funcionarios.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Funcionarios.ImageOffset = new System.Drawing.Point(-6, 1);
            this.Funcionarios.ImageSize = new System.Drawing.Size(18, 18);
            this.Funcionarios.Location = new System.Drawing.Point(13, 232);
            this.Funcionarios.Margin = new System.Windows.Forms.Padding(0);
            this.Funcionarios.Name = "Funcionarios";
            this.Funcionarios.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Funcionarios.Size = new System.Drawing.Size(166, 28);
            this.Funcionarios.TabIndex = 6;
            this.Funcionarios.Text = "Funcionários";
            this.Funcionarios.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ToolTip.SetToolTip(this.Funcionarios, "Funcionários");
            this.Funcionarios.Click += new System.EventHandler(this.Funcionarios_Click);
            // 
            // Fornecedores
            // 
            this.Fornecedores.Animated = true;
            this.Fornecedores.BackColor = System.Drawing.Color.Transparent;
            this.Fornecedores.BorderColor = System.Drawing.Color.Empty;
            this.Fornecedores.BorderRadius = 5;
            this.Fornecedores.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Fornecedores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Fornecedores.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Fornecedores.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Fornecedores.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Fornecedores.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Fornecedores.FillColor = System.Drawing.Color.Empty;
            this.Fornecedores.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.Fornecedores.ForeColor = System.Drawing.Color.Black;
            this.Fornecedores.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.Fornecedores.Image = global::TCC.Properties.Resources.delivery;
            this.Fornecedores.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Fornecedores.ImageOffset = new System.Drawing.Point(-6, 0);
            this.Fornecedores.ImageSize = new System.Drawing.Size(18, 18);
            this.Fornecedores.Location = new System.Drawing.Point(13, 190);
            this.Fornecedores.Margin = new System.Windows.Forms.Padding(0);
            this.Fornecedores.Name = "Fornecedores";
            this.Fornecedores.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Fornecedores.Size = new System.Drawing.Size(166, 28);
            this.Fornecedores.TabIndex = 5;
            this.Fornecedores.Text = "Fornecedores";
            this.Fornecedores.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ToolTip.SetToolTip(this.Fornecedores, "Fornecedores");
            this.Fornecedores.Click += new System.EventHandler(this.Fornecedores_Click);
            // 
            // Contratos
            // 
            this.Contratos.Animated = true;
            this.Contratos.BackColor = System.Drawing.Color.Transparent;
            this.Contratos.BorderColor = System.Drawing.Color.Empty;
            this.Contratos.BorderRadius = 5;
            this.Contratos.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Contratos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Contratos.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Contratos.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Contratos.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Contratos.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Contratos.FillColor = System.Drawing.Color.Empty;
            this.Contratos.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.Contratos.ForeColor = System.Drawing.Color.Black;
            this.Contratos.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.Contratos.Image = global::TCC.Properties.Resources.contratos;
            this.Contratos.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Contratos.ImageOffset = new System.Drawing.Point(-6, 1);
            this.Contratos.Location = new System.Drawing.Point(13, 190);
            this.Contratos.Margin = new System.Windows.Forms.Padding(0);
            this.Contratos.Name = "Contratos";
            this.Contratos.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Contratos.Size = new System.Drawing.Size(166, 28);
            this.Contratos.TabIndex = 4;
            this.Contratos.Text = "Contratos";
            this.Contratos.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Contratos.TextOffset = new System.Drawing.Point(-3, 0);
            this.ToolTip.SetToolTip(this.Contratos, "Contratos");
            this.Contratos.Visible = false;
            // 
            // Inicio
            // 
            this.Inicio.Animated = true;
            this.Inicio.BackColor = System.Drawing.Color.Transparent;
            this.Inicio.BorderColor = System.Drawing.Color.Empty;
            this.Inicio.BorderRadius = 5;
            this.Inicio.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Inicio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Inicio.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Inicio.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Inicio.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Inicio.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Inicio.FillColor = System.Drawing.Color.Empty;
            this.Inicio.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.Inicio.ForeColor = System.Drawing.Color.Black;
            this.Inicio.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.Inicio.Image = global::TCC.Properties.Resources.home;
            this.Inicio.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Inicio.ImageOffset = new System.Drawing.Point(-6, 1);
            this.Inicio.Location = new System.Drawing.Point(13, 64);
            this.Inicio.Margin = new System.Windows.Forms.Padding(0);
            this.Inicio.Name = "Inicio";
            this.Inicio.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Inicio.Size = new System.Drawing.Size(166, 28);
            this.Inicio.TabIndex = 1;
            this.Inicio.Text = "Início";
            this.Inicio.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Inicio.TextOffset = new System.Drawing.Point(-2, 0);
            this.ToolTip.SetToolTip(this.Inicio, "Início");
            this.Inicio.Click += new System.EventHandler(this.Inicio_Click);
            // 
            // Clientes
            // 
            this.Clientes.Animated = true;
            this.Clientes.BackColor = System.Drawing.Color.Transparent;
            this.Clientes.BorderColor = System.Drawing.Color.Empty;
            this.Clientes.BorderRadius = 5;
            this.Clientes.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Clientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Clientes.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Clientes.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Clientes.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Clientes.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Clientes.FillColor = System.Drawing.Color.Empty;
            this.Clientes.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.Clientes.ForeColor = System.Drawing.Color.Black;
            this.Clientes.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.Clientes.Image = global::TCC.Properties.Resources.clientes;
            this.Clientes.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Clientes.ImageOffset = new System.Drawing.Point(-6, 1);
            this.Clientes.Location = new System.Drawing.Point(13, 106);
            this.Clientes.Margin = new System.Windows.Forms.Padding(0);
            this.Clientes.Name = "Clientes";
            this.Clientes.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Clientes.Size = new System.Drawing.Size(166, 28);
            this.Clientes.TabIndex = 2;
            this.Clientes.Text = "Clientes";
            this.Clientes.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Clientes.TextOffset = new System.Drawing.Point(-3, 0);
            this.ToolTip.SetToolTip(this.Clientes, "Clientes");
            this.Clientes.Click += new System.EventHandler(this.Clientes_Click);
            // 
            // LogoName
            // 
            this.LogoName.AutoSize = true;
            this.LogoName.BackColor = System.Drawing.Color.Transparent;
            this.LogoName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LogoName.Font = new System.Drawing.Font("Segoe UI Semibold", 15.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogoName.ForeColor = System.Drawing.Color.Black;
            this.LogoName.Location = new System.Drawing.Point(52, 14);
            this.LogoName.Margin = new System.Windows.Forms.Padding(0);
            this.LogoName.Name = "LogoName";
            this.LogoName.Size = new System.Drawing.Size(137, 30);
            this.LogoName.TabIndex = 8;
            this.LogoName.Text = "CLÍNICA CAR";
            this.LogoName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Logo
            // 
            this.Logo.BackColor = System.Drawing.Color.Transparent;
            this.Logo.Image = global::TCC.Properties.Resources.Logo_250;
            this.Logo.Location = new System.Drawing.Point(5, 8);
            this.Logo.Margin = new System.Windows.Forms.Padding(0);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(44, 44);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Logo.TabIndex = 7;
            this.Logo.TabStop = false;
            // 
            // EstoqueSeparator
            // 
            this.EstoqueSeparator.BackColor = System.Drawing.Color.Transparent;
            this.EstoqueSeparator.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.EstoqueSeparator.FillThickness = 4;
            this.EstoqueSeparator.Location = new System.Drawing.Point(25, 176);
            this.EstoqueSeparator.Margin = new System.Windows.Forms.Padding(0);
            this.EstoqueSeparator.Name = "EstoqueSeparator";
            this.EstoqueSeparator.Size = new System.Drawing.Size(2, 70);
            this.EstoqueSeparator.TabIndex = 24;
            this.EstoqueSeparator.Visible = false;
            // 
            // Separator2
            // 
            this.Separator2.BackColor = System.Drawing.Color.Transparent;
            this.Separator2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Separator2.Location = new System.Drawing.Point(10, 262);
            this.Separator2.Margin = new System.Windows.Forms.Padding(0);
            this.Separator2.Name = "Separator2";
            this.Separator2.Size = new System.Drawing.Size(170, 10);
            this.Separator2.TabIndex = 29;
            this.Separator2.Visible = false;
            // 
            // Estoque
            // 
            this.Estoque.Animated = true;
            this.Estoque.BackColor = System.Drawing.Color.Transparent;
            this.Estoque.BorderColor = System.Drawing.Color.Empty;
            this.Estoque.BorderRadius = 5;
            this.Estoque.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Estoque.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Estoque.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Estoque.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Estoque.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Estoque.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Estoque.FillColor = System.Drawing.Color.Empty;
            this.Estoque.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.Estoque.ForeColor = System.Drawing.Color.Black;
            this.Estoque.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.Estoque.Image = global::TCC.Properties.Resources.estoque;
            this.Estoque.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Estoque.ImageOffset = new System.Drawing.Point(-6, 1);
            this.Estoque.Location = new System.Drawing.Point(13, 148);
            this.Estoque.Margin = new System.Windows.Forms.Padding(0);
            this.Estoque.Name = "Estoque";
            this.Estoque.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Estoque.Size = new System.Drawing.Size(166, 28);
            this.Estoque.TabIndex = 3;
            this.Estoque.Text = "Estoque";
            this.Estoque.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Estoque.TextOffset = new System.Drawing.Point(-2, 0);
            this.ToolTip.SetToolTip(this.Estoque, "Estoque");
            this.Estoque.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Estoque_MouseClick);
            // 
            // Separator1
            // 
            this.Separator1.BackColor = System.Drawing.Color.Transparent;
            this.Separator1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Separator1.Location = new System.Drawing.Point(10, 136);
            this.Separator1.Margin = new System.Windows.Forms.Padding(0);
            this.Separator1.Name = "Separator1";
            this.Separator1.Size = new System.Drawing.Size(170, 10);
            this.Separator1.TabIndex = 28;
            this.Separator1.Visible = false;
            // 
            // BarraHor
            // 
            this.BarraHor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BarraHor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.BarraHor.Location = new System.Drawing.Point(0, 55);
            this.BarraHor.Name = "BarraHor";
            this.BarraHor.Size = new System.Drawing.Size(1864, 1);
            this.BarraHor.TabIndex = 5;
            // 
            // BarraVert
            // 
            this.BarraVert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.BarraVert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.BarraVert.Location = new System.Drawing.Point(189, -8);
            this.BarraVert.Margin = new System.Windows.Forms.Padding(0);
            this.BarraVert.Name = "BarraVert";
            this.BarraVert.Size = new System.Drawing.Size(1, 710);
            this.BarraVert.TabIndex = 6;
            // 
            // AddForm
            // 
            this.AddForm.BackColor = System.Drawing.Color.White;
            this.AddForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AddForm.Location = new System.Drawing.Point(190, 56);
            this.AddForm.Margin = new System.Windows.Forms.Padding(0);
            this.AddForm.Name = "AddForm";
            this.AddForm.Size = new System.Drawing.Size(858, 645);
            this.AddForm.TabIndex = 5;
            // 
            // ToolTip
            // 
            this.ToolTip.Active = false;
            this.ToolTip.AllowLinksHandling = true;
            this.ToolTip.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.ToolTip.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.ToolTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.ToolTip.MaximumSize = new System.Drawing.Size(0, 0);
            this.ToolTip.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.ToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // ToolTip2
            // 
            this.ToolTip2.AllowLinksHandling = true;
            this.ToolTip2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.ToolTip2.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.ToolTip2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.ToolTip2.MaximumSize = new System.Drawing.Size(0, 0);
            this.ToolTip2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.ToolTip2.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // VerifyTimer
            // 
            this.VerifyTimer.Interval = 200;
            this.VerifyTimer.Tick += new System.EventHandler(this.VerifyTimer_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1048, 701);
            this.Controls.Add(this.BarraVert);
            this.Controls.Add(this.BarraHor);
            this.Controls.Add(this.AddForm);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Clínica Car";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HideBar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MoreEstoqueItensBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarraLateral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LogoName;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Panel BarraHor;
        private System.Windows.Forms.Panel BarraVert;
        private System.Windows.Forms.Panel AddForm;
        private Guna.UI2.WinForms.Guna2Button Clientes;
        private Guna.UI2.WinForms.Guna2Button Inicio;
        private Guna.UI2.WinForms.Guna2Button Estoque;
        private Guna.UI2.WinForms.Guna2Button Fornecedores;
        private Guna.UI2.WinForms.Guna2Button Funcionarios;
        private Guna.UI2.WinForms.Guna2Button Serviços;
        private System.Windows.Forms.PictureBox BarraLateral;
        private Guna.UI2.WinForms.Guna2HtmlToolTip ToolTip;
        private Guna.UI2.WinForms.Guna2HtmlToolTip ToolTip2;
        private System.Windows.Forms.PictureBox HideBar;
        private Guna.UI2.WinForms.Guna2Button Configuracoes;
        private Guna.UI2.WinForms.Guna2Button Sair;
        private Guna.UI2.WinForms.Guna2Button SaidaDeItem;
        private Guna.UI2.WinForms.Guna2Button EntradaDeItem;
        private Guna.UI2.WinForms.Guna2VSeparator EstoqueSeparator;
        private Guna.UI2.WinForms.Guna2Separator Separator2;
        private Guna.UI2.WinForms.Guna2Separator Separator1;
        private System.Windows.Forms.PictureBox MoreEstoqueItensBtn;
        private Guna.UI2.WinForms.Guna2Button Contratos;
        private System.Windows.Forms.Timer VerifyTimer;
    }
}

