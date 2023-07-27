
namespace TCC.Frames
{
    partial class DeleteConfirmation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteConfirmation));
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.TimerAnim = new System.Windows.Forms.Timer(this.components);
            this.TextoCima = new System.Windows.Forms.Label();
            this.Ok = new Guna.UI2.WinForms.Guna2Button();
            this.Cancelar = new Guna.UI2.WinForms.Guna2Button();
            this.label6 = new System.Windows.Forms.Label();
            this.PasswordHint = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.Texto = new System.Windows.Forms.Label();
            this.Password = new Guna.UI2.WinForms.Guna2TextBox();
            this.Gif = new System.Windows.Forms.PictureBox();
            this.GifTimer = new System.Windows.Forms.Timer(this.components);
            this.GifTimer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Gif)).BeginInit();
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
            // TextoCima
            // 
            this.TextoCima.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TextoCima.BackColor = System.Drawing.Color.Transparent;
            this.TextoCima.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.TextoCima.ForeColor = System.Drawing.Color.Black;
            this.TextoCima.Location = new System.Drawing.Point(55, 115);
            this.TextoCima.Margin = new System.Windows.Forms.Padding(0);
            this.TextoCima.Name = "TextoCima";
            this.TextoCima.Size = new System.Drawing.Size(270, 35);
            this.TextoCima.TabIndex = 375;
            this.TextoCima.Text = "Confirmar exclusão";
            this.TextoCima.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Ok
            // 
            this.Ok.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Ok.Animated = true;
            this.Ok.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.Ok.BorderRadius = 3;
            this.Ok.BorderThickness = 1;
            this.Ok.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.Ok.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.Ok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Ok.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Ok.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Ok.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Ok.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Ok.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.Ok.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.Ok.ForeColor = System.Drawing.Color.White;
            this.Ok.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.Ok.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.Ok.Location = new System.Drawing.Point(199, 330);
            this.Ok.Margin = new System.Windows.Forms.Padding(0);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(150, 38);
            this.Ok.TabIndex = 389;
            this.Ok.Text = "Excluir";
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // Cancelar
            // 
            this.Cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancelar.Animated = true;
            this.Cancelar.BackColor = System.Drawing.Color.Transparent;
            this.Cancelar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.Cancelar.BorderRadius = 3;
            this.Cancelar.BorderThickness = 1;
            this.Cancelar.CheckedState.BorderColor = System.Drawing.Color.Black;
            this.Cancelar.CheckedState.CustomBorderColor = System.Drawing.Color.Black;
            this.Cancelar.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Cancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cancelar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Cancelar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Cancelar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Cancelar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Cancelar.FillColor = System.Drawing.Color.Empty;
            this.Cancelar.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Cancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.Cancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.Cancelar.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Cancelar.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Cancelar.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Cancelar.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Cancelar.ImageOffset = new System.Drawing.Point(-4, 0);
            this.Cancelar.ImageSize = new System.Drawing.Size(16, 16);
            this.Cancelar.Location = new System.Drawing.Point(27, 330);
            this.Cancelar.Margin = new System.Windows.Forms.Padding(0);
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Cancelar.Size = new System.Drawing.Size(150, 38);
            this.Cancelar.TabIndex = 390;
            this.Cancelar.Text = "Cancelar";
            this.Cancelar.TextOffset = new System.Drawing.Point(0, -1);
            this.Cancelar.UseTransparentBackground = true;
            this.Cancelar.Click += new System.EventHandler(this.Cancelar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.label6.Location = new System.Drawing.Point(26, 230);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 17);
            this.label6.TabIndex = 391;
            this.label6.Text = "SENHA";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PasswordHint
            // 
            this.PasswordHint.BackColor = System.Drawing.Color.Transparent;
            this.PasswordHint.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.PasswordHint.IsSelectionEnabled = false;
            this.PasswordHint.Location = new System.Drawing.Point(29, 287);
            this.PasswordHint.Margin = new System.Windows.Forms.Padding(0);
            this.PasswordHint.Name = "PasswordHint";
            this.PasswordHint.Size = new System.Drawing.Size(86, 17);
            this.PasswordHint.TabIndex = 393;
            this.PasswordHint.TabStop = false;
            this.PasswordHint.Text = "Senha incorreta";
            this.PasswordHint.Visible = false;
            // 
            // Texto
            // 
            this.Texto.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Texto.BackColor = System.Drawing.Color.Transparent;
            this.Texto.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.Texto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.Texto.Location = new System.Drawing.Point(29, 160);
            this.Texto.Margin = new System.Windows.Forms.Padding(0);
            this.Texto.Name = "Texto";
            this.Texto.Size = new System.Drawing.Size(322, 44);
            this.Texto.TabIndex = 395;
            this.Texto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Password
            // 
            this.Password.Animated = true;
            this.Password.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Password.BorderRadius = 3;
            this.Password.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Password.DefaultText = "";
            this.Password.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Password.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Password.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Password.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Password.FocusedState.BorderColor = System.Drawing.Color.Black;
            this.Password.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Password.ForeColor = System.Drawing.Color.Black;
            this.Password.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.Password.IconLeftSize = new System.Drawing.Size(0, 0);
            this.Password.IconRightSize = new System.Drawing.Size(18, 18);
            this.Password.Location = new System.Drawing.Point(29, 250);
            this.Password.Margin = new System.Windows.Forms.Padding(0);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '●';
            this.Password.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.Password.PlaceholderText = "";
            this.Password.SelectedText = "";
            this.Password.Size = new System.Drawing.Size(322, 34);
            this.Password.TabIndex = 396;
            this.Password.TextChanged += new System.EventHandler(this.Password_TextChanged);
            this.Password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Password_KeyPress);
            // 
            // Gif
            // 
            this.Gif.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Gif.BackColor = System.Drawing.Color.Transparent;
            this.Gif.Location = new System.Drawing.Point(50, 20);
            this.Gif.Margin = new System.Windows.Forms.Padding(0);
            this.Gif.Name = "Gif";
            this.Gif.Size = new System.Drawing.Size(280, 90);
            this.Gif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Gif.TabIndex = 382;
            this.Gif.TabStop = false;
            // 
            // GifTimer
            // 
            this.GifTimer.Interval = 20;
            this.GifTimer.Tick += new System.EventHandler(this.GifTimer_Tick);
            // 
            // GifTimer2
            // 
            this.GifTimer2.Interval = 1;
            this.GifTimer2.Tick += new System.EventHandler(this.GifTimer2_Tick);
            // 
            // DeleteConfirmation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 380);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Texto);
            this.Controls.Add(this.PasswordHint);
            this.Controls.Add(this.TextoCima);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Cancelar);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.Gif);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(380, 380);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(380, 380);
            this.Name = "DeleteConfirmation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confimar exclusão de clientes";
            this.Load += new System.EventHandler(this.DeleteConfirmation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Gif)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private System.Windows.Forms.Timer TimerAnim;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2Button Cancelar;
        private Guna.UI2.WinForms.Guna2Button Ok;
        private System.Windows.Forms.PictureBox Gif;
        private System.Windows.Forms.Label TextoCima;
        private Guna.UI2.WinForms.Guna2HtmlLabel PasswordHint;
        private System.Windows.Forms.Label Texto;
        private Guna.UI2.WinForms.Guna2TextBox Password;
        private System.Windows.Forms.Timer GifTimer;
        private System.Windows.Forms.Timer GifTimer2;
    }
}