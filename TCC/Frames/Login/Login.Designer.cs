
namespace TCC
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.HidePassword = new System.Windows.Forms.PictureBox();
            this.Senha = new Guna.UI2.WinForms.Guna2TextBox();
            this.Usuario = new Guna.UI2.WinForms.Guna2TextBox();
            this.LoginBtn = new Guna.UI2.WinForms.Guna2Button();
            this.ToolTip = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.Remember = new Guna.UI2.WinForms.Guna2CustomCheckBox();
            this.ToolTip2 = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.label1 = new System.Windows.Forms.Label();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.HidePassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // HidePassword
            // 
            this.HidePassword.AccessibleDescription = "";
            this.HidePassword.AccessibleName = "";
            this.HidePassword.BackColor = System.Drawing.Color.Transparent;
            this.HidePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HidePassword.Image = global::TCC.Properties.Resources.show_pass;
            this.HidePassword.Location = new System.Drawing.Point(242, 215);
            this.HidePassword.Margin = new System.Windows.Forms.Padding(0);
            this.HidePassword.Name = "HidePassword";
            this.HidePassword.Size = new System.Drawing.Size(20, 20);
            this.HidePassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.HidePassword.TabIndex = 99;
            this.HidePassword.TabStop = false;
            this.HidePassword.Tag = "";
            this.ToolTip.SetToolTip(this.HidePassword, "Mostrar senha");
            this.ToolTip2.SetToolTip(this.HidePassword, "Esconder senha");
            this.HidePassword.Click += new System.EventHandler(this.Hide_Click);
            // 
            // Senha
            // 
            this.Senha.Animated = true;
            this.Senha.BackColor = System.Drawing.Color.Transparent;
            this.Senha.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Senha.BorderRadius = 3;
            this.Senha.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Senha.DefaultText = "";
            this.Senha.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Senha.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Senha.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Senha.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Senha.FocusedState.BorderColor = System.Drawing.Color.Black;
            this.Senha.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.Senha.ForeColor = System.Drawing.Color.Black;
            this.Senha.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.Senha.IconLeft = global::TCC.Properties.Resources.cadeado;
            this.Senha.IconLeftOffset = new System.Drawing.Point(3, 0);
            this.Senha.Location = new System.Drawing.Point(12, 206);
            this.Senha.Margin = new System.Windows.Forms.Padding(0);
            this.Senha.MaxLength = 6;
            this.Senha.Multiline = true;
            this.Senha.Name = "Senha";
            this.Senha.PasswordChar = '●';
            this.Senha.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.Senha.PlaceholderText = "Senha";
            this.Senha.SelectedText = "";
            this.Senha.Size = new System.Drawing.Size(260, 38);
            this.Senha.TabIndex = 98;
            this.Senha.TextOffset = new System.Drawing.Point(2, 0);
            this.Senha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Senha_KeyPress);
            // 
            // Usuario
            // 
            this.Usuario.Animated = true;
            this.Usuario.BackColor = System.Drawing.Color.Transparent;
            this.Usuario.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Usuario.BorderRadius = 3;
            this.Usuario.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Usuario.DefaultText = "";
            this.Usuario.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Usuario.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Usuario.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Usuario.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Usuario.FocusedState.BorderColor = System.Drawing.Color.Black;
            this.Usuario.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.Usuario.ForeColor = System.Drawing.Color.Black;
            this.Usuario.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.Usuario.IconLeft = global::TCC.Properties.Resources.person;
            this.Usuario.IconLeftOffset = new System.Drawing.Point(3, 0);
            this.Usuario.Location = new System.Drawing.Point(12, 156);
            this.Usuario.Margin = new System.Windows.Forms.Padding(0);
            this.Usuario.MaxLength = 16;
            this.Usuario.Multiline = true;
            this.Usuario.Name = "Usuario";
            this.Usuario.PasswordChar = '\0';
            this.Usuario.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.Usuario.PlaceholderText = "Usuário";
            this.Usuario.SelectedText = "";
            this.Usuario.Size = new System.Drawing.Size(260, 38);
            this.Usuario.TabIndex = 97;
            this.Usuario.TextOffset = new System.Drawing.Point(2, 0);
            this.Usuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Usuario_KeyPress);
            // 
            // LoginBtn
            // 
            this.LoginBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoginBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.LoginBtn.BorderRadius = 3;
            this.LoginBtn.BorderThickness = 1;
            this.LoginBtn.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.LoginBtn.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.LoginBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoginBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.LoginBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.LoginBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.LoginBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.LoginBtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.LoginBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.LoginBtn.ForeColor = System.Drawing.Color.White;
            this.LoginBtn.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.LoginBtn.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.LoginBtn.Location = new System.Drawing.Point(12, 259);
            this.LoginBtn.Margin = new System.Windows.Forms.Padding(0);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(260, 38);
            this.LoginBtn.TabIndex = 243;
            this.LoginBtn.Text = "Entrar";
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click_1);
            // 
            // ToolTip
            // 
            this.ToolTip.AllowLinksHandling = true;
            this.ToolTip.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.ToolTip.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.ToolTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.ToolTip.MaximumSize = new System.Drawing.Size(0, 0);
            this.ToolTip.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.ToolTip.TitleFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // Remember
            // 
            this.Remember.Checked = true;
            this.Remember.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.Remember.CheckedState.BorderRadius = 2;
            this.Remember.CheckedState.BorderThickness = 0;
            this.Remember.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.Remember.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Remember.Location = new System.Drawing.Point(12, 264);
            this.Remember.Margin = new System.Windows.Forms.Padding(0);
            this.Remember.Name = "Remember";
            this.Remember.Size = new System.Drawing.Size(15, 15);
            this.Remember.TabIndex = 244;
            this.Remember.Text = "guna2CustomCheckBox1";
            this.ToolTip.SetToolTip(this.Remember, "Lembrar senha");
            this.ToolTip2.SetToolTip(this.Remember, "Lembrar senha");
            this.Remember.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Remember.UncheckedState.BorderRadius = 2;
            this.Remember.UncheckedState.BorderThickness = 0;
            this.Remember.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Remember.Visible = false;
            this.Remember.CheckedChanged += new System.EventHandler(this.Remember_CheckedChanged);
            // 
            // ToolTip2
            // 
            this.ToolTip2.AllowLinksHandling = true;
            this.ToolTip2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.ToolTip2.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.ToolTip2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.ToolTip2.MaximumSize = new System.Drawing.Size(0, 0);
            this.ToolTip2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.ToolTip2.TitleFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolTip2.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(32, 262);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 245;
            this.label1.Text = "Lembrar senha";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // Logo
            // 
            this.Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Logo.ErrorImage = ((System.Drawing.Image)(resources.GetObject("Logo.ErrorImage")));
            this.Logo.Image = ((System.Drawing.Image)(resources.GetObject("Logo.Image")));
            this.Logo.InitialImage = ((System.Drawing.Image)(resources.GetObject("Logo.InitialImage")));
            this.Logo.Location = new System.Drawing.Point(92, 10);
            this.Logo.Margin = new System.Windows.Forms.Padding(0);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(100, 100);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Logo.TabIndex = 246;
            this.Logo.TabStop = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(2, 110);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(280, 28);
            this.label3.TabIndex = 247;
            this.label3.Text = "LOGIN";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(284, 306);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Remember);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.HidePassword);
            this.Controls.Add(this.Senha);
            this.Controls.Add(this.Usuario);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 345);
            this.MinimumSize = new System.Drawing.Size(300, 345);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clínica Car - Login";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_FormClosed);
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.HidePassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2TextBox Usuario;
        private Guna.UI2.WinForms.Guna2TextBox Senha;
        private System.Windows.Forms.PictureBox HidePassword;
        private Guna.UI2.WinForms.Guna2Button LoginBtn;
        private Guna.UI2.WinForms.Guna2HtmlToolTip ToolTip;
        private Guna.UI2.WinForms.Guna2HtmlToolTip ToolTip2;
        private Guna.UI2.WinForms.Guna2CustomCheckBox Remember;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Label label3;
    }
}