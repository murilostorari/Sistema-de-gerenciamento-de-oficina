
namespace TCC.Frames.Estoque
{
    partial class PecasUtilizadas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PecasUtilizadas));
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.TimerAnim = new System.Windows.Forms.Timer(this.components);
            this.ToolTip = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.FrameName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.Separator = new Guna.UI2.WinForms.Guna2Separator();
            this.Minimize = new Guna.UI2.WinForms.Guna2ControlBox();
            this.CloseBtn = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.Tecnico = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Cancelar = new Guna.UI2.WinForms.Guna2Button();
            this.Inserir = new Guna.UI2.WinForms.Guna2Button();
            this.ProdutoHint = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.TecnicoHint = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.Produto = new Guna.UI2.WinForms.Guna2TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.UsadaHint = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.Usada = new Guna.UI2.WinForms.Guna2TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Custo = new Guna.UI2.WinForms.Guna2TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ValorTotal = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Codigo = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FormAnim = new System.Windows.Forms.Timer(this.components);
            this.Disponivel = new Guna.UI2.WinForms.Guna2TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.NumeroFabricante = new Guna.UI2.WinForms.Guna2TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.NumeroDoServico = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.AnimateWindow = true;
            this.guna2BorderlessForm1.AnimationInterval = 100;
            this.guna2BorderlessForm1.BorderRadius = 6;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.HasFormShadow = false;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // TimerAnim
            // 
            this.TimerAnim.Interval = 1;
            this.TimerAnim.Tick += new System.EventHandler(this.TimerAnim_Tick);
            // 
            // ToolTip
            // 
            this.ToolTip.AllowLinksHandling = true;
            this.ToolTip.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.ToolTip.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.ToolTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.ToolTip.MaximumSize = new System.Drawing.Size(0, 0);
            this.ToolTip.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.ToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // FrameName
            // 
            this.FrameName.BackColor = System.Drawing.Color.Transparent;
            this.FrameName.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            this.FrameName.ForeColor = System.Drawing.Color.Black;
            this.FrameName.IsSelectionEnabled = false;
            this.FrameName.Location = new System.Drawing.Point(14, 4);
            this.FrameName.Margin = new System.Windows.Forms.Padding(0);
            this.FrameName.Name = "FrameName";
            this.FrameName.Size = new System.Drawing.Size(196, 39);
            this.FrameName.TabIndex = 256;
            this.FrameName.Text = "Adicionar peças";
            this.FrameName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Separator
            // 
            this.Separator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator.BackColor = System.Drawing.Color.Transparent;
            this.Separator.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Separator.Location = new System.Drawing.Point(11, 46);
            this.Separator.Margin = new System.Windows.Forms.Padding(0);
            this.Separator.Name = "Separator";
            this.Separator.Size = new System.Drawing.Size(578, 1);
            this.Separator.TabIndex = 257;
            this.Separator.UseTransparentBackground = true;
            // 
            // Minimize
            // 
            this.Minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Minimize.Animated = true;
            this.Minimize.BackColor = System.Drawing.Color.Transparent;
            this.Minimize.BorderColor = System.Drawing.Color.Empty;
            this.Minimize.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
            this.Minimize.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.Minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Minimize.CustomIconSize = 13F;
            this.Minimize.FillColor = System.Drawing.Color.Transparent;
            this.Minimize.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.Minimize.ForeColor = System.Drawing.Color.Black;
            this.Minimize.HoverState.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.Minimize.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.Minimize.Location = new System.Drawing.Point(519, 9);
            this.Minimize.Margin = new System.Windows.Forms.Padding(0);
            this.Minimize.Name = "Minimize";
            this.Minimize.Size = new System.Drawing.Size(32, 32);
            this.Minimize.TabIndex = 259;
            this.Minimize.TabStop = false;
            this.Minimize.UseTransparentBackground = true;
            // 
            // CloseBtn
            // 
            this.CloseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseBtn.Animated = true;
            this.CloseBtn.BackColor = System.Drawing.Color.Transparent;
            this.CloseBtn.BorderColor = System.Drawing.Color.Empty;
            this.CloseBtn.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
            this.CloseBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseBtn.CustomClick = true;
            this.CloseBtn.CustomIconSize = 15F;
            this.CloseBtn.FillColor = System.Drawing.Color.Transparent;
            this.CloseBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.CloseBtn.ForeColor = System.Drawing.Color.Black;
            this.CloseBtn.HoverState.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.CloseBtn.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.CloseBtn.Location = new System.Drawing.Point(559, 9);
            this.CloseBtn.Margin = new System.Windows.Forms.Padding(0);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(32, 32);
            this.CloseBtn.TabIndex = 258;
            this.CloseBtn.TabStop = false;
            this.CloseBtn.UseTransparentBackground = true;
            this.CloseBtn.Click += new System.EventHandler(this.Close_Click);
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel1.IsSelectionEnabled = false;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(2, 1);
            this.guna2HtmlLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(112, 27);
            this.guna2HtmlLabel1.TabIndex = 396;
            this.guna2HtmlLabel1.Text = "Mão de obra";
            this.guna2HtmlLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Tecnico
            // 
            this.Tecnico.BackColor = System.Drawing.Color.Transparent;
            this.Tecnico.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Tecnico.BorderRadius = 3;
            this.Tecnico.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Tecnico.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Tecnico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Tecnico.FocusedColor = System.Drawing.Color.Black;
            this.Tecnico.FocusedState.BorderColor = System.Drawing.Color.Black;
            this.Tecnico.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.Tecnico.ForeColor = System.Drawing.Color.Black;
            this.Tecnico.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.Tecnico.ItemHeight = 28;
            this.Tecnico.Items.AddRange(new object[] {
            "Não definido"});
            this.Tecnico.ItemsAppearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Tecnico.ItemsAppearance.ForeColor = System.Drawing.Color.Black;
            this.Tecnico.ItemsAppearance.SelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Tecnico.Location = new System.Drawing.Point(11, 423);
            this.Tecnico.Margin = new System.Windows.Forms.Padding(0);
            this.Tecnico.Name = "Tecnico";
            this.Tecnico.Size = new System.Drawing.Size(578, 34);
            this.Tecnico.Sorted = true;
            this.Tecnico.StartIndex = 0;
            this.Tecnico.TabIndex = 276;
            this.Tecnico.SelectedIndexChanged += new System.EventHandler(this.Tecnico_SelectedIndexChanged);
            this.Tecnico.SelectedValueChanged += new System.EventHandler(this.ChangedToTrue);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.label3.Location = new System.Drawing.Point(9, 402);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 17);
            this.label3.TabIndex = 277;
            this.label3.Text = "TÉCNICO RESPONSÁVEL";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Cancelar
            // 
            this.Cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Cancelar.Animated = true;
            this.Cancelar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.Cancelar.BorderRadius = 3;
            this.Cancelar.BorderThickness = 1;
            this.Cancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cancelar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Cancelar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Cancelar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Cancelar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Cancelar.FillColor = System.Drawing.Color.Transparent;
            this.Cancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.Cancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.Cancelar.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.Cancelar.Location = new System.Drawing.Point(9, 553);
            this.Cancelar.Margin = new System.Windows.Forms.Padding(0);
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.Cancelar.Size = new System.Drawing.Size(120, 38);
            this.Cancelar.TabIndex = 278;
            this.Cancelar.Text = "Cancelar";
            this.Cancelar.TextOffset = new System.Drawing.Point(0, -1);
            this.Cancelar.Click += new System.EventHandler(this.Cancelar1_Click);
            // 
            // Inserir
            // 
            this.Inserir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Inserir.Animated = true;
            this.Inserir.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.Inserir.BorderRadius = 3;
            this.Inserir.BorderThickness = 1;
            this.Inserir.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(68)))), ((int)(((byte)(30)))));
            this.Inserir.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(68)))), ((int)(((byte)(30)))));
            this.Inserir.CheckedState.ForeColor = System.Drawing.Color.White;
            this.Inserir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Inserir.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Inserir.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Inserir.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Inserir.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Inserir.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.Inserir.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.Inserir.ForeColor = System.Drawing.Color.White;
            this.Inserir.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Inserir.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Inserir.IndicateFocus = true;
            this.Inserir.Location = new System.Drawing.Point(471, 553);
            this.Inserir.Margin = new System.Windows.Forms.Padding(0);
            this.Inserir.Name = "Inserir";
            this.Inserir.Size = new System.Drawing.Size(120, 38);
            this.Inserir.TabIndex = 279;
            this.Inserir.Text = "Inserir peças";
            this.Inserir.TextOffset = new System.Drawing.Point(2, -1);
            this.Inserir.Click += new System.EventHandler(this.Inserir_Click);
            // 
            // ProdutoHint
            // 
            this.ProdutoHint.BackColor = System.Drawing.Color.Transparent;
            this.ProdutoHint.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProdutoHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.ProdutoHint.IsSelectionEnabled = false;
            this.ProdutoHint.Location = new System.Drawing.Point(11, 120);
            this.ProdutoHint.Margin = new System.Windows.Forms.Padding(0);
            this.ProdutoHint.Name = "ProdutoHint";
            this.ProdutoHint.Size = new System.Drawing.Size(139, 17);
            this.ProdutoHint.TabIndex = 289;
            this.ProdutoHint.TabStop = false;
            this.ProdutoHint.Text = "Insira o nome do produto";
            this.ProdutoHint.Visible = false;
            // 
            // TecnicoHint
            // 
            this.TecnicoHint.BackColor = System.Drawing.Color.Transparent;
            this.TecnicoHint.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TecnicoHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.TecnicoHint.IsSelectionEnabled = false;
            this.TecnicoHint.Location = new System.Drawing.Point(11, 460);
            this.TecnicoHint.Margin = new System.Windows.Forms.Padding(0);
            this.TecnicoHint.Name = "TecnicoHint";
            this.TecnicoHint.Size = new System.Drawing.Size(215, 17);
            this.TecnicoHint.TabIndex = 290;
            this.TecnicoHint.TabStop = false;
            this.TecnicoHint.Text = "Insira o técnico responsável pelo serviço";
            this.TecnicoHint.Visible = false;
            // 
            // Produto
            // 
            this.Produto.Animated = true;
            this.Produto.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Produto.BorderRadius = 3;
            this.Produto.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Produto.DefaultText = "";
            this.Produto.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Produto.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Produto.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Produto.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Produto.FocusedState.BorderColor = System.Drawing.Color.Black;
            this.Produto.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.Produto.ForeColor = System.Drawing.Color.Black;
            this.Produto.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.Produto.IconLeftSize = new System.Drawing.Size(0, 0);
            this.Produto.Location = new System.Drawing.Point(11, 83);
            this.Produto.Margin = new System.Windows.Forms.Padding(0);
            this.Produto.Name = "Produto";
            this.Produto.PasswordChar = '\0';
            this.Produto.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.Produto.PlaceholderText = "";
            this.Produto.SelectedText = "";
            this.Produto.Size = new System.Drawing.Size(578, 34);
            this.Produto.TabIndex = 294;
            this.Produto.TextChanged += new System.EventHandler(this.Produto_TextChanged);
            this.Produto.Leave += new System.EventHandler(this.Produto_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.label7.Location = new System.Drawing.Point(11, 62);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 17);
            this.label7.TabIndex = 295;
            this.label7.Text = "NOME DO PRODUTO";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UsadaHint
            // 
            this.UsadaHint.BackColor = System.Drawing.Color.Transparent;
            this.UsadaHint.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsadaHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.UsadaHint.IsSelectionEnabled = false;
            this.UsadaHint.Location = new System.Drawing.Point(11, 290);
            this.UsadaHint.Margin = new System.Windows.Forms.Padding(0);
            this.UsadaHint.Name = "UsadaHint";
            this.UsadaHint.Size = new System.Drawing.Size(138, 17);
            this.UsadaHint.TabIndex = 298;
            this.UsadaHint.TabStop = false;
            this.UsadaHint.Text = "Insira a quantidade usada";
            this.UsadaHint.Visible = false;
            // 
            // Usada
            // 
            this.Usada.Animated = true;
            this.Usada.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Usada.BorderRadius = 3;
            this.Usada.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Usada.DefaultText = "";
            this.Usada.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Usada.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Usada.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Usada.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Usada.FocusedState.BorderColor = System.Drawing.Color.Black;
            this.Usada.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.Usada.ForeColor = System.Drawing.Color.Black;
            this.Usada.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.Usada.IconLeftSize = new System.Drawing.Size(0, 0);
            this.Usada.Location = new System.Drawing.Point(11, 253);
            this.Usada.Margin = new System.Windows.Forms.Padding(0);
            this.Usada.Name = "Usada";
            this.Usada.PasswordChar = '\0';
            this.Usada.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.Usada.PlaceholderText = "";
            this.Usada.SelectedText = "";
            this.Usada.Size = new System.Drawing.Size(284, 34);
            this.Usada.TabIndex = 296;
            this.Usada.TextChanged += new System.EventHandler(this.Usada_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.label9.Location = new System.Drawing.Point(7, 232);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 17);
            this.label9.TabIndex = 297;
            this.label9.Text = "QNTD. USADA";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Custo
            // 
            this.Custo.Animated = true;
            this.Custo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Custo.BorderRadius = 3;
            this.Custo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Custo.DefaultText = "";
            this.Custo.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Custo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Custo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Custo.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Custo.FocusedState.BorderColor = System.Drawing.Color.Black;
            this.Custo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.Custo.ForeColor = System.Drawing.Color.Black;
            this.Custo.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.Custo.IconLeftSize = new System.Drawing.Size(0, 0);
            this.Custo.Location = new System.Drawing.Point(11, 338);
            this.Custo.Margin = new System.Windows.Forms.Padding(0);
            this.Custo.Name = "Custo";
            this.Custo.PasswordChar = '\0';
            this.Custo.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.Custo.PlaceholderText = "";
            this.Custo.ReadOnly = true;
            this.Custo.SelectedText = "";
            this.Custo.Size = new System.Drawing.Size(284, 34);
            this.Custo.TabIndex = 299;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.label4.Location = new System.Drawing.Point(9, 317);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 17);
            this.label4.TabIndex = 300;
            this.label4.Text = "CUSTO DO PRODUTO (R$)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ValorTotal
            // 
            this.ValorTotal.Animated = true;
            this.ValorTotal.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.ValorTotal.BorderRadius = 3;
            this.ValorTotal.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ValorTotal.DefaultText = "";
            this.ValorTotal.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.ValorTotal.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ValorTotal.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ValorTotal.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ValorTotal.FocusedState.BorderColor = System.Drawing.Color.Black;
            this.ValorTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.ValorTotal.ForeColor = System.Drawing.Color.Black;
            this.ValorTotal.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.ValorTotal.IconLeftSize = new System.Drawing.Size(0, 0);
            this.ValorTotal.Location = new System.Drawing.Point(307, 338);
            this.ValorTotal.Margin = new System.Windows.Forms.Padding(0);
            this.ValorTotal.Name = "ValorTotal";
            this.ValorTotal.PasswordChar = '\0';
            this.ValorTotal.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.ValorTotal.PlaceholderText = "";
            this.ValorTotal.ReadOnly = true;
            this.ValorTotal.SelectedText = "";
            this.ValorTotal.Size = new System.Drawing.Size(284, 34);
            this.ValorTotal.TabIndex = 302;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.label1.Location = new System.Drawing.Point(305, 317);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 17);
            this.label1.TabIndex = 301;
            this.label1.Text = "VALOR TOTAL (R$)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Codigo
            // 
            this.Codigo.Animated = true;
            this.Codigo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Codigo.BorderRadius = 3;
            this.Codigo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Codigo.DefaultText = "";
            this.Codigo.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Codigo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Codigo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Codigo.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Codigo.FocusedState.BorderColor = System.Drawing.Color.Black;
            this.Codigo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.Codigo.ForeColor = System.Drawing.Color.Black;
            this.Codigo.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.Codigo.IconLeftSize = new System.Drawing.Size(0, 0);
            this.Codigo.Location = new System.Drawing.Point(11, 168);
            this.Codigo.Margin = new System.Windows.Forms.Padding(0);
            this.Codigo.Name = "Codigo";
            this.Codigo.PasswordChar = '\0';
            this.Codigo.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.Codigo.PlaceholderText = "";
            this.Codigo.ReadOnly = true;
            this.Codigo.SelectedText = "";
            this.Codigo.Size = new System.Drawing.Size(284, 34);
            this.Codigo.TabIndex = 303;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.label2.Location = new System.Drawing.Point(9, 147);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 17);
            this.label2.TabIndex = 304;
            this.label2.Text = "CÓDIGO";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormAnim
            // 
            this.FormAnim.Interval = 1;
            this.FormAnim.Tick += new System.EventHandler(this.FormAnim_Tick);
            // 
            // Disponivel
            // 
            this.Disponivel.Animated = true;
            this.Disponivel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Disponivel.BorderRadius = 3;
            this.Disponivel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Disponivel.DefaultText = "";
            this.Disponivel.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Disponivel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Disponivel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Disponivel.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Disponivel.FocusedState.BorderColor = System.Drawing.Color.Black;
            this.Disponivel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.Disponivel.ForeColor = System.Drawing.Color.Black;
            this.Disponivel.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.Disponivel.IconLeftSize = new System.Drawing.Size(0, 0);
            this.Disponivel.Location = new System.Drawing.Point(307, 253);
            this.Disponivel.Margin = new System.Windows.Forms.Padding(0);
            this.Disponivel.Name = "Disponivel";
            this.Disponivel.PasswordChar = '\0';
            this.Disponivel.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.Disponivel.PlaceholderText = "";
            this.Disponivel.ReadOnly = true;
            this.Disponivel.SelectedText = "";
            this.Disponivel.Size = new System.Drawing.Size(284, 34);
            this.Disponivel.TabIndex = 305;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.label5.Location = new System.Drawing.Point(305, 232);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 17);
            this.label5.TabIndex = 306;
            this.label5.Text = "QNTD. DISPONÍVEL";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NumeroFabricante
            // 
            this.NumeroFabricante.Animated = true;
            this.NumeroFabricante.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.NumeroFabricante.BorderRadius = 3;
            this.NumeroFabricante.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.NumeroFabricante.DefaultText = "";
            this.NumeroFabricante.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.NumeroFabricante.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.NumeroFabricante.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.NumeroFabricante.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.NumeroFabricante.FocusedState.BorderColor = System.Drawing.Color.Black;
            this.NumeroFabricante.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.NumeroFabricante.ForeColor = System.Drawing.Color.Black;
            this.NumeroFabricante.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.NumeroFabricante.IconLeftSize = new System.Drawing.Size(0, 0);
            this.NumeroFabricante.Location = new System.Drawing.Point(307, 168);
            this.NumeroFabricante.Margin = new System.Windows.Forms.Padding(0);
            this.NumeroFabricante.Name = "NumeroFabricante";
            this.NumeroFabricante.PasswordChar = '\0';
            this.NumeroFabricante.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.NumeroFabricante.PlaceholderText = "";
            this.NumeroFabricante.ReadOnly = true;
            this.NumeroFabricante.SelectedText = "";
            this.NumeroFabricante.Size = new System.Drawing.Size(284, 34);
            this.NumeroFabricante.TabIndex = 307;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.label6.Location = new System.Drawing.Point(305, 147);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(167, 17);
            this.label6.TabIndex = 308;
            this.label6.Text = "NÚMERO DO FABRICANTE";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NumeroDoServico
            // 
            this.NumeroDoServico.AutoSize = true;
            this.NumeroDoServico.BackColor = System.Drawing.Color.White;
            this.NumeroDoServico.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.NumeroDoServico.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.NumeroDoServico.Location = new System.Drawing.Point(468, 24);
            this.NumeroDoServico.Margin = new System.Windows.Forms.Padding(0);
            this.NumeroDoServico.Name = "NumeroDoServico";
            this.NumeroDoServico.Size = new System.Drawing.Size(48, 17);
            this.NumeroDoServico.TabIndex = 309;
            this.NumeroDoServico.Text = "123456";
            this.NumeroDoServico.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.NumeroDoServico.Visible = false;
            // 
            // PecasUtilizadas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 600);
            this.Controls.Add(this.NumeroDoServico);
            this.Controls.Add(this.NumeroFabricante);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Disponivel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Codigo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ValorTotal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Custo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.UsadaHint);
            this.Controls.Add(this.Usada);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.Produto);
            this.Controls.Add(this.TecnicoHint);
            this.Controls.Add(this.ProdutoHint);
            this.Controls.Add(this.Inserir);
            this.Controls.Add(this.Cancelar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Tecnico);
            this.Controls.Add(this.Minimize);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.Separator);
            this.Controls.Add(this.FrameName);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(600, 600);
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "PecasUtilizadas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adicionar um novo produto ao serviço";
            this.Load += new System.EventHandler(this.NovoProduto_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private System.Windows.Forms.Timer TimerAnim;
        private Guna.UI2.WinForms.Guna2HtmlToolTip ToolTip;
        private Guna.UI2.WinForms.Guna2HtmlLabel FrameName;
        private Guna.UI2.WinForms.Guna2Separator Separator;
        private Guna.UI2.WinForms.Guna2ControlBox Minimize;
        private Guna.UI2.WinForms.Guna2ControlBox CloseBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nUMERODOSERVICODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dESCRICAODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIPODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iNICIODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fIMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qUANTIDADEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vALORDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tECNICODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2ComboBox Tecnico;
        private Guna.UI2.WinForms.Guna2Button Cancelar;
        private Guna.UI2.WinForms.Guna2Button Inserir;
        private Guna.UI2.WinForms.Guna2HtmlLabel ProdutoHint;
        private Guna.UI2.WinForms.Guna2HtmlLabel TecnicoHint;
        private Guna.UI2.WinForms.Guna2TextBox Produto;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2HtmlLabel UsadaHint;
        private Guna.UI2.WinForms.Guna2TextBox Usada;
        private System.Windows.Forms.Label label9;
        private Guna.UI2.WinForms.Guna2TextBox Custo;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2TextBox ValorTotal;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox Codigo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer FormAnim;
        private Guna.UI2.WinForms.Guna2TextBox Disponivel;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2TextBox NumeroFabricante;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label NumeroDoServico;
    }
}