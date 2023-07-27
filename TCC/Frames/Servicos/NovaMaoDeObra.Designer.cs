
namespace TCC.Frames.Estoque
{
    partial class NovaMaoDeObra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NovaMaoDeObra));
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.TimerAnim = new System.Windows.Forms.Timer(this.components);
            this.ToolTip = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.FrameName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.Separator = new Guna.UI2.WinForms.Guna2Separator();
            this.Minimize = new Guna.UI2.WinForms.Guna2ControlBox();
            this.CloseBtn = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.Servico = new Guna.UI2.WinForms.Guna2TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.FormaDeCobranca = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Tecnico = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Cancelar = new Guna.UI2.WinForms.Guna2Button();
            this.Inserir = new Guna.UI2.WinForms.Guna2Button();
            this.DataInicio = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label33 = new System.Windows.Forms.Label();
            this.ValorPorHoraOuServico = new Guna.UI2.WinForms.Guna2TextBox();
            this.DataFim = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.QuantidadeHoras = new Guna.UI2.WinForms.Guna2TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ValorTotal = new Guna.UI2.WinForms.Guna2TextBox();
            this.ServicoHint = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.TecnicoHint = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.ValorPorHoraHint = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.QuantidadeDeHorasHint = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.FormAnim = new System.Windows.Forms.Timer(this.components);
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
            this.FrameName.Size = new System.Drawing.Size(213, 39);
            this.FrameName.TabIndex = 256;
            this.FrameName.Text = "Adicionar serviço";
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
            // Servico
            // 
            this.Servico.Animated = true;
            this.Servico.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Servico.BorderRadius = 3;
            this.Servico.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Servico.DefaultText = "";
            this.Servico.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Servico.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Servico.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Servico.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Servico.FocusedState.BorderColor = System.Drawing.Color.Black;
            this.Servico.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.Servico.ForeColor = System.Drawing.Color.Black;
            this.Servico.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.Servico.IconLeftSize = new System.Drawing.Size(0, 0);
            this.Servico.Location = new System.Drawing.Point(11, 83);
            this.Servico.Margin = new System.Windows.Forms.Padding(0);
            this.Servico.Multiline = true;
            this.Servico.Name = "Servico";
            this.Servico.PasswordChar = '\0';
            this.Servico.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.Servico.PlaceholderText = "";
            this.Servico.SelectedText = "";
            this.Servico.Size = new System.Drawing.Size(578, 56);
            this.Servico.TabIndex = 270;
            this.Servico.TextOffset = new System.Drawing.Point(-4, -4);
            this.Servico.TextChanged += new System.EventHandler(this.DescricaoServico_TextChanged);
            this.Servico.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ChangedToTrueKeyPress);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BackColor = System.Drawing.Color.White;
            this.label32.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.label32.Location = new System.Drawing.Point(9, 62);
            this.label32.Margin = new System.Windows.Forms.Padding(0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(137, 17);
            this.label32.TabIndex = 271;
            this.label32.Text = "SERVIÇO EXECUTADO";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormaDeCobranca
            // 
            this.FormaDeCobranca.BackColor = System.Drawing.Color.Transparent;
            this.FormaDeCobranca.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.FormaDeCobranca.BorderRadius = 3;
            this.FormaDeCobranca.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormaDeCobranca.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.FormaDeCobranca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FormaDeCobranca.FocusedColor = System.Drawing.Color.Black;
            this.FormaDeCobranca.FocusedState.BorderColor = System.Drawing.Color.Black;
            this.FormaDeCobranca.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.FormaDeCobranca.ForeColor = System.Drawing.Color.Black;
            this.FormaDeCobranca.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.FormaDeCobranca.ItemHeight = 28;
            this.FormaDeCobranca.Items.AddRange(new object[] {
            "Por hora",
            "Por serviço"});
            this.FormaDeCobranca.ItemsAppearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormaDeCobranca.ItemsAppearance.ForeColor = System.Drawing.Color.Black;
            this.FormaDeCobranca.ItemsAppearance.SelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.FormaDeCobranca.Location = new System.Drawing.Point(307, 277);
            this.FormaDeCobranca.Margin = new System.Windows.Forms.Padding(0);
            this.FormaDeCobranca.Name = "FormaDeCobranca";
            this.FormaDeCobranca.Size = new System.Drawing.Size(284, 34);
            this.FormaDeCobranca.Sorted = true;
            this.FormaDeCobranca.StartIndex = 0;
            this.FormaDeCobranca.TabIndex = 272;
            this.FormaDeCobranca.SelectedIndexChanged += new System.EventHandler(this.FormaDeCobranca_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.label1.Location = new System.Drawing.Point(305, 256);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 17);
            this.label1.TabIndex = 273;
            this.label1.Text = "FORMA DE COBRANÇA";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.label2.Location = new System.Drawing.Point(9, 256);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 17);
            this.label2.TabIndex = 275;
            this.label2.Text = "VALOR COBRADO POR HORA";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.Tecnico.Location = new System.Drawing.Point(11, 192);
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
            this.label3.Location = new System.Drawing.Point(9, 171);
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
            this.Inserir.Text = "Inserir serviço";
            this.Inserir.TextOffset = new System.Drawing.Point(2, -1);
            this.Inserir.Click += new System.EventHandler(this.Inserir_Click);
            // 
            // DataInicio
            // 
            this.DataInicio.Animated = true;
            this.DataInicio.BackColor = System.Drawing.Color.Transparent;
            this.DataInicio.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.DataInicio.BorderRadius = 3;
            this.DataInicio.BorderThickness = 1;
            this.DataInicio.Checked = true;
            this.DataInicio.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.DataInicio.CheckedState.FillColor = System.Drawing.Color.White;
            this.DataInicio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DataInicio.CustomFormat = "dd/MM/yyyy hh:mm";
            this.DataInicio.FillColor = System.Drawing.Color.White;
            this.DataInicio.Font = new System.Drawing.Font("Segoe UI", 10.15F);
            this.DataInicio.ForeColor = System.Drawing.Color.Black;
            this.DataInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DataInicio.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.DataInicio.Location = new System.Drawing.Point(10, 362);
            this.DataInicio.Margin = new System.Windows.Forms.Padding(0);
            this.DataInicio.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.DataInicio.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DataInicio.Name = "DataInicio";
            this.DataInicio.Size = new System.Drawing.Size(284, 34);
            this.DataInicio.TabIndex = 281;
            this.DataInicio.Value = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.BackColor = System.Drawing.Color.White;
            this.label33.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label33.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.label33.Location = new System.Drawing.Point(8, 341);
            this.label33.Margin = new System.Windows.Forms.Padding(0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(104, 17);
            this.label33.TabIndex = 280;
            this.label33.Text = "DATA DE INÍCIO";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ValorPorHoraOuServico
            // 
            this.ValorPorHoraOuServico.Animated = true;
            this.ValorPorHoraOuServico.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.ValorPorHoraOuServico.BorderRadius = 3;
            this.ValorPorHoraOuServico.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ValorPorHoraOuServico.DefaultText = "";
            this.ValorPorHoraOuServico.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.ValorPorHoraOuServico.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ValorPorHoraOuServico.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ValorPorHoraOuServico.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ValorPorHoraOuServico.FocusedState.BorderColor = System.Drawing.Color.Black;
            this.ValorPorHoraOuServico.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.ValorPorHoraOuServico.ForeColor = System.Drawing.Color.Black;
            this.ValorPorHoraOuServico.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.ValorPorHoraOuServico.IconLeftSize = new System.Drawing.Size(0, 0);
            this.ValorPorHoraOuServico.Location = new System.Drawing.Point(9, 277);
            this.ValorPorHoraOuServico.Margin = new System.Windows.Forms.Padding(0);
            this.ValorPorHoraOuServico.Name = "ValorPorHoraOuServico";
            this.ValorPorHoraOuServico.PasswordChar = '\0';
            this.ValorPorHoraOuServico.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.ValorPorHoraOuServico.PlaceholderText = "";
            this.ValorPorHoraOuServico.SelectedText = "";
            this.ValorPorHoraOuServico.Size = new System.Drawing.Size(284, 34);
            this.ValorPorHoraOuServico.TabIndex = 282;
            this.ValorPorHoraOuServico.TextChanged += new System.EventHandler(this.ValorPorHoraOuServico_TextChanged);
            this.ValorPorHoraOuServico.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextKeyDown);
            this.ValorPorHoraOuServico.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextKeyPress);
            this.ValorPorHoraOuServico.Leave += new System.EventHandler(this.ValorPorHoraOuServico_Leave);
            // 
            // DataFim
            // 
            this.DataFim.Animated = true;
            this.DataFim.BackColor = System.Drawing.Color.Transparent;
            this.DataFim.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.DataFim.BorderRadius = 3;
            this.DataFim.BorderThickness = 1;
            this.DataFim.Checked = true;
            this.DataFim.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.DataFim.CheckedState.FillColor = System.Drawing.Color.White;
            this.DataFim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DataFim.CustomFormat = "dd/MM/yyyy hh:mm";
            this.DataFim.FillColor = System.Drawing.Color.White;
            this.DataFim.Font = new System.Drawing.Font("Segoe UI", 10.15F);
            this.DataFim.ForeColor = System.Drawing.Color.Black;
            this.DataFim.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DataFim.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.DataFim.Location = new System.Drawing.Point(307, 362);
            this.DataFim.Margin = new System.Windows.Forms.Padding(0);
            this.DataFim.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.DataFim.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DataFim.Name = "DataFim";
            this.DataFim.Size = new System.Drawing.Size(284, 34);
            this.DataFim.TabIndex = 284;
            this.DataFim.Value = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            this.DataFim.ValueChanged += new System.EventHandler(this.DataFim_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.label4.Location = new System.Drawing.Point(305, 341);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 17);
            this.label4.TabIndex = 283;
            this.label4.Text = "DATA DE TÉRMINO";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // QuantidadeHoras
            // 
            this.QuantidadeHoras.Animated = true;
            this.QuantidadeHoras.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.QuantidadeHoras.BorderRadius = 3;
            this.QuantidadeHoras.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.QuantidadeHoras.DefaultText = "";
            this.QuantidadeHoras.FocusedState.BorderColor = System.Drawing.Color.Black;
            this.QuantidadeHoras.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.QuantidadeHoras.ForeColor = System.Drawing.Color.Black;
            this.QuantidadeHoras.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.QuantidadeHoras.IconLeftSize = new System.Drawing.Size(0, 0);
            this.QuantidadeHoras.Location = new System.Drawing.Point(9, 447);
            this.QuantidadeHoras.Margin = new System.Windows.Forms.Padding(0);
            this.QuantidadeHoras.Name = "QuantidadeHoras";
            this.QuantidadeHoras.PasswordChar = '\0';
            this.QuantidadeHoras.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.QuantidadeHoras.PlaceholderText = "";
            this.QuantidadeHoras.SelectedText = "";
            this.QuantidadeHoras.Size = new System.Drawing.Size(284, 34);
            this.QuantidadeHoras.TabIndex = 285;
            this.QuantidadeHoras.TextChanged += new System.EventHandler(this.QuantidadeHoras_TextChanged);
            this.QuantidadeHoras.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ChangedToTrueKeyPress);
            this.QuantidadeHoras.Leave += new System.EventHandler(this.QuantidadeHoras_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.label5.Location = new System.Drawing.Point(9, 426);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 17);
            this.label5.TabIndex = 286;
            this.label5.Text = "QUANTIDADE DE HORAS";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.label6.Location = new System.Drawing.Point(305, 426);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 17);
            this.label6.TabIndex = 288;
            this.label6.Text = "VALOR TOTAL";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.ValorTotal.Location = new System.Drawing.Point(305, 447);
            this.ValorTotal.Margin = new System.Windows.Forms.Padding(0);
            this.ValorTotal.Name = "ValorTotal";
            this.ValorTotal.PasswordChar = '\0';
            this.ValorTotal.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.ValorTotal.PlaceholderText = "";
            this.ValorTotal.SelectedText = "";
            this.ValorTotal.Size = new System.Drawing.Size(284, 34);
            this.ValorTotal.TabIndex = 287;
            // 
            // ServicoHint
            // 
            this.ServicoHint.BackColor = System.Drawing.Color.Transparent;
            this.ServicoHint.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServicoHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.ServicoHint.IsSelectionEnabled = false;
            this.ServicoHint.Location = new System.Drawing.Point(11, 142);
            this.ServicoHint.Margin = new System.Windows.Forms.Padding(0);
            this.ServicoHint.Name = "ServicoHint";
            this.ServicoHint.Size = new System.Drawing.Size(140, 17);
            this.ServicoHint.TabIndex = 289;
            this.ServicoHint.TabStop = false;
            this.ServicoHint.Text = "Insira o serviço executado";
            this.ServicoHint.Visible = false;
            // 
            // TecnicoHint
            // 
            this.TecnicoHint.BackColor = System.Drawing.Color.Transparent;
            this.TecnicoHint.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TecnicoHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.TecnicoHint.IsSelectionEnabled = false;
            this.TecnicoHint.Location = new System.Drawing.Point(11, 229);
            this.TecnicoHint.Margin = new System.Windows.Forms.Padding(0);
            this.TecnicoHint.Name = "TecnicoHint";
            this.TecnicoHint.Size = new System.Drawing.Size(215, 17);
            this.TecnicoHint.TabIndex = 290;
            this.TecnicoHint.TabStop = false;
            this.TecnicoHint.Text = "Insira o técnico responsável pelo serviço";
            this.TecnicoHint.Visible = false;
            // 
            // ValorPorHoraHint
            // 
            this.ValorPorHoraHint.BackColor = System.Drawing.Color.Transparent;
            this.ValorPorHoraHint.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValorPorHoraHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.ValorPorHoraHint.IsSelectionEnabled = false;
            this.ValorPorHoraHint.Location = new System.Drawing.Point(9, 314);
            this.ValorPorHoraHint.Margin = new System.Windows.Forms.Padding(0);
            this.ValorPorHoraHint.Name = "ValorPorHoraHint";
            this.ValorPorHoraHint.Size = new System.Drawing.Size(223, 17);
            this.ValorPorHoraHint.TabIndex = 291;
            this.ValorPorHoraHint.TabStop = false;
            this.ValorPorHoraHint.Text = "Insira o valor cobrado por hora do serviço";
            this.ValorPorHoraHint.Visible = false;
            // 
            // QuantidadeDeHorasHint
            // 
            this.QuantidadeDeHorasHint.BackColor = System.Drawing.Color.Transparent;
            this.QuantidadeDeHorasHint.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantidadeDeHorasHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.QuantidadeDeHorasHint.IsSelectionEnabled = false;
            this.QuantidadeDeHorasHint.Location = new System.Drawing.Point(9, 484);
            this.QuantidadeDeHorasHint.Margin = new System.Windows.Forms.Padding(0);
            this.QuantidadeDeHorasHint.Name = "QuantidadeDeHorasHint";
            this.QuantidadeDeHorasHint.Size = new System.Drawing.Size(223, 17);
            this.QuantidadeDeHorasHint.TabIndex = 292;
            this.QuantidadeDeHorasHint.TabStop = false;
            this.QuantidadeDeHorasHint.Text = "Insira o valor cobrado por hora do serviço";
            this.QuantidadeDeHorasHint.Visible = false;
            // 
            // FormAnim
            // 
            this.FormAnim.Interval = 1;
            this.FormAnim.Tick += new System.EventHandler(this.FormAnim_Tick);
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
            this.NumeroDoServico.TabIndex = 293;
            this.NumeroDoServico.Text = "123456";
            this.NumeroDoServico.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.NumeroDoServico.Visible = false;
            // 
            // NovaMaoDeObra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 600);
            this.Controls.Add(this.NumeroDoServico);
            this.Controls.Add(this.QuantidadeDeHorasHint);
            this.Controls.Add(this.ValorPorHoraHint);
            this.Controls.Add(this.TecnicoHint);
            this.Controls.Add(this.ServicoHint);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ValorTotal);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.QuantidadeHoras);
            this.Controls.Add(this.DataFim);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ValorPorHoraOuServico);
            this.Controls.Add(this.DataInicio);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.Inserir);
            this.Controls.Add(this.Cancelar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Tecnico);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FormaDeCobranca);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.Servico);
            this.Controls.Add(this.Minimize);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.Separator);
            this.Controls.Add(this.FrameName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(600, 600);
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "NovaMaoDeObra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adicionar novo serviço";
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
        private Guna.UI2.WinForms.Guna2TextBox Servico;
        private System.Windows.Forms.Label label32;
        private Guna.UI2.WinForms.Guna2ComboBox FormaDeCobranca;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2ComboBox Tecnico;
        private Guna.UI2.WinForms.Guna2Button Cancelar;
        private Guna.UI2.WinForms.Guna2Button Inserir;
        private Guna.UI2.WinForms.Guna2DateTimePicker DataInicio;
        private System.Windows.Forms.Label label33;
        private Guna.UI2.WinForms.Guna2TextBox ValorPorHoraOuServico;
        private Guna.UI2.WinForms.Guna2DateTimePicker DataFim;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2TextBox QuantidadeHoras;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2TextBox ValorTotal;
        private Guna.UI2.WinForms.Guna2HtmlLabel ServicoHint;
        private Guna.UI2.WinForms.Guna2HtmlLabel ValorPorHoraHint;
        private Guna.UI2.WinForms.Guna2HtmlLabel TecnicoHint;
        private Guna.UI2.WinForms.Guna2HtmlLabel QuantidadeDeHorasHint;
        private System.Windows.Forms.Timer FormAnim;
        private System.Windows.Forms.Label NumeroDoServico;
    }
}