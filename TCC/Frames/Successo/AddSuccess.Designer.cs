
namespace TCC.Frames
{
    partial class AddSuccess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddSuccess));
            this.Ok = new Guna.UI2.WinForms.Guna2Button();
            this.Texto = new System.Windows.Forms.Label();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.TimerAnim = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.Gif = new System.Windows.Forms.PictureBox();
            this.GifTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Gif)).BeginInit();
            this.SuspendLayout();
            // 
            // Ok
            // 
            this.Ok.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Ok.Animated = true;
            this.Ok.BackColor = System.Drawing.Color.Transparent;
            this.Ok.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(178)))), ((int)(((byte)(77)))));
            this.Ok.BorderRadius = 3;
            this.Ok.BorderThickness = 1;
            this.Ok.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(178)))), ((int)(((byte)(57)))));
            this.Ok.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(178)))), ((int)(((byte)(57)))));
            this.Ok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Ok.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Ok.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Ok.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Ok.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Ok.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(178)))), ((int)(((byte)(77)))));
            this.Ok.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(178)))), ((int)(((byte)(57)))));
            this.Ok.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.Ok.ForeColor = System.Drawing.Color.White;
            this.Ok.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(198)))), ((int)(((byte)(77)))));
            this.Ok.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(198)))), ((int)(((byte)(77)))));
            this.Ok.IndicateFocus = true;
            this.Ok.Location = new System.Drawing.Point(54, 250);
            this.Ok.Margin = new System.Windows.Forms.Padding(0);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(192, 38);
            this.Ok.TabIndex = 104;
            this.Ok.Text = "OK!";
            this.Ok.Click += new System.EventHandler(this.Ok_Click_1);
            // 
            // Texto
            // 
            this.Texto.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Texto.BackColor = System.Drawing.Color.Transparent;
            this.Texto.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.Texto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.Texto.Location = new System.Drawing.Point(49, 190);
            this.Texto.Margin = new System.Windows.Forms.Padding(0);
            this.Texto.Name = "Texto";
            this.Texto.Size = new System.Drawing.Size(200, 44);
            this.Texto.TabIndex = 105;
            this.Texto.Text = "Cliente adicionado com sucesso!";
            this.Texto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(50, 150);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 35);
            this.label1.TabIndex = 109;
            this.label1.Text = "Sucesso!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.Gif.TabIndex = 110;
            this.Gif.TabStop = false;
            // 
            // GifTimer
            // 
            this.GifTimer.Interval = 1;
            this.GifTimer.Tick += new System.EventHandler(this.GifTimer_Tick);
            // 
            // AddSuccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.Gif);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.Texto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "AddSuccess";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastrado com sucesso!";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Sucesso_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Gif)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button Ok;
        private System.Windows.Forms.Label Texto;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private System.Windows.Forms.Timer TimerAnim;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox Gif;
        private System.Windows.Forms.Timer GifTimer;
    }
}