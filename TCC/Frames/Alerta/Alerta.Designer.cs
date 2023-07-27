
namespace TCC.Frames
{
    partial class Alerta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Alerta));
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.TimerAnim = new System.Windows.Forms.Timer(this.components);
            this.FormAnim = new System.Windows.Forms.Timer(this.components);
            this.Gif = new System.Windows.Forms.PictureBox();
            this.Texto = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Ok = new Guna.UI2.WinForms.Guna2Button();
            this.GifTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Gif)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.AnimateWindow = true;
            this.guna2BorderlessForm1.AnimationInterval = 70;
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
            // FormAnim
            // 
            this.FormAnim.Interval = 1;
            this.FormAnim.Tick += new System.EventHandler(this.FormAnim_Tick);
            // 
            // Gif
            // 
            this.Gif.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Gif.BackColor = System.Drawing.Color.Transparent;
            this.Gif.Image = global::TCC.Properties.Resources.alerta___icon;
            this.Gif.Location = new System.Drawing.Point(10, 30);
            this.Gif.Margin = new System.Windows.Forms.Padding(0);
            this.Gif.Name = "Gif";
            this.Gif.Size = new System.Drawing.Size(280, 75);
            this.Gif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Gif.TabIndex = 116;
            this.Gif.TabStop = false;
            // 
            // Texto
            // 
            this.Texto.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Texto.BackColor = System.Drawing.Color.Transparent;
            this.Texto.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.Texto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.Texto.Location = new System.Drawing.Point(10, 180);
            this.Texto.Margin = new System.Windows.Forms.Padding(0);
            this.Texto.Name = "Texto";
            this.Texto.Size = new System.Drawing.Size(280, 44);
            this.Texto.TabIndex = 118;
            this.Texto.Text = "Produto não encontrado no sistema. Deseja cadastrar um novo?";
            this.Texto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(50, 130);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 35);
            this.label1.TabIndex = 119;
            this.label1.Text = "Atenção!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Ok
            // 
            this.Ok.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Ok.Animated = true;
            this.Ok.BackColor = System.Drawing.Color.Transparent;
            this.Ok.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(178)))), ((int)(((byte)(77)))));
            this.Ok.BorderRadius = 3;
            this.Ok.BorderThickness = 1;
            this.Ok.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.Ok.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.Ok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Ok.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Ok.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Ok.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Ok.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Ok.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(178)))), ((int)(((byte)(77)))));
            this.Ok.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.Ok.ForeColor = System.Drawing.Color.White;
            this.Ok.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.Ok.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.Ok.Location = new System.Drawing.Point(54, 250);
            this.Ok.Margin = new System.Windows.Forms.Padding(0);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(192, 38);
            this.Ok.TabIndex = 120;
            this.Ok.Text = "OK";
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // GifTimer
            // 
            this.GifTimer.Interval = 1;
            this.GifTimer.Tick += new System.EventHandler(this.GifTimer_Tick);
            // 
            // Alerta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Texto);
            this.Controls.Add(this.Gif);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "Alerta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Atenção!";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Alerta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Gif)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private System.Windows.Forms.Timer TimerAnim;
        private System.Windows.Forms.Timer FormAnim;
        private System.Windows.Forms.PictureBox Gif;
        private Guna.UI2.WinForms.Guna2Button Ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Texto;
        private System.Windows.Forms.Timer GifTimer;
    }
}