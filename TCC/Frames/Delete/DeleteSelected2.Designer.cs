
namespace TCC.Frames
{
    partial class DeleteSelected2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteSelected2));
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.TimerAnim = new System.Windows.Forms.Timer(this.components);
            this.Texto = new System.Windows.Forms.Label();
            this.Cancelar = new Guna.UI2.WinForms.Guna2Button();
            this.Ok = new Guna.UI2.WinForms.Guna2Button();
            this.ItemInfoText = new System.Windows.Forms.Label();
            this.TemplateText = new System.Windows.Forms.Label();
            this.Gif = new System.Windows.Forms.PictureBox();
            this.GifTimer = new System.Windows.Forms.Timer(this.components);
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
            // Texto
            // 
            this.Texto.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Texto.BackColor = System.Drawing.Color.Transparent;
            this.Texto.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.Texto.ForeColor = System.Drawing.Color.DimGray;
            this.Texto.Location = new System.Drawing.Point(49, 190);
            this.Texto.Margin = new System.Windows.Forms.Padding(0);
            this.Texto.Name = "Texto";
            this.Texto.Size = new System.Drawing.Size(200, 44);
            this.Texto.TabIndex = 110;
            this.Texto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.Cancelar.FillColor = System.Drawing.Color.Transparent;
            this.Cancelar.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Cancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.Cancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.Cancelar.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Cancelar.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Cancelar.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Cancelar.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Cancelar.ImageOffset = new System.Drawing.Point(-4, 0);
            this.Cancelar.ImageSize = new System.Drawing.Size(16, 16);
            this.Cancelar.Location = new System.Drawing.Point(21, 250);
            this.Cancelar.Margin = new System.Windows.Forms.Padding(0);
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Cancelar.Size = new System.Drawing.Size(120, 38);
            this.Cancelar.TabIndex = 370;
            this.Cancelar.Text = "Não";
            this.Cancelar.TextOffset = new System.Drawing.Point(0, -1);
            this.Cancelar.UseTransparentBackground = true;
            this.Cancelar.Click += new System.EventHandler(this.Cancelar_Click);
            // 
            // Ok
            // 
            this.Ok.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Ok.Animated = true;
            this.Ok.BackColor = System.Drawing.Color.Transparent;
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
            this.Ok.Location = new System.Drawing.Point(158, 250);
            this.Ok.Margin = new System.Windows.Forms.Padding(0);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(120, 38);
            this.Ok.TabIndex = 369;
            this.Ok.Text = "Excluir";
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // ItemInfoText
            // 
            this.ItemInfoText.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.ItemInfoText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.ItemInfoText.Location = new System.Drawing.Point(10, 150);
            this.ItemInfoText.Margin = new System.Windows.Forms.Padding(0);
            this.ItemInfoText.Name = "ItemInfoText";
            this.ItemInfoText.Size = new System.Drawing.Size(280, 18);
            this.ItemInfoText.TabIndex = 391;
            this.ItemInfoText.Text = "126";
            this.ItemInfoText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ItemInfoText.Visible = false;
            // 
            // TemplateText
            // 
            this.TemplateText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TemplateText.BackColor = System.Drawing.Color.Transparent;
            this.TemplateText.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.TemplateText.ForeColor = System.Drawing.Color.Black;
            this.TemplateText.Location = new System.Drawing.Point(30, 150);
            this.TemplateText.Margin = new System.Windows.Forms.Padding(0);
            this.TemplateText.Name = "TemplateText";
            this.TemplateText.Size = new System.Drawing.Size(240, 35);
            this.TemplateText.TabIndex = 392;
            this.TemplateText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Gif
            // 
            this.Gif.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Gif.BackColor = System.Drawing.Color.Transparent;
            this.Gif.Location = new System.Drawing.Point(10, 10);
            this.Gif.Margin = new System.Windows.Forms.Padding(0);
            this.Gif.Name = "Gif";
            this.Gif.Size = new System.Drawing.Size(280, 130);
            this.Gif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Gif.TabIndex = 112;
            this.Gif.TabStop = false;
            // 
            // GifTimer
            // 
            this.GifTimer.Interval = 1;
            this.GifTimer.Tick += new System.EventHandler(this.GifTimer_Tick);
            // 
            // DeleteSelected2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.TemplateText);
            this.Controls.Add(this.ItemInfoText);
            this.Controls.Add(this.Cancelar);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.Texto);
            this.Controls.Add(this.Gif);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "DeleteSelected2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Excluir cliente";
            this.Load += new System.EventHandler(this.Delete_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Gif)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private System.Windows.Forms.Timer TimerAnim;
        private System.Windows.Forms.Label Texto;
        private System.Windows.Forms.PictureBox Gif;
        private Guna.UI2.WinForms.Guna2Button Cancelar;
        private Guna.UI2.WinForms.Guna2Button Ok;
        private System.Windows.Forms.Label ItemInfoText;
        private System.Windows.Forms.Label TemplateText;
        private System.Windows.Forms.Timer GifTimer;
    }
}