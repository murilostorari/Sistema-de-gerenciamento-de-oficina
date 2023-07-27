
namespace TCC.Frames
{
    partial class RestartApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestartApp));
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.Ok1 = new Guna.UI2.WinForms.Guna2Button();
            this.Texto = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            // Ok1
            // 
            this.Ok1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Ok1.Animated = true;
            this.Ok1.BackColor = System.Drawing.Color.Transparent;
            this.Ok1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(178)))), ((int)(((byte)(77)))));
            this.Ok1.BorderRadius = 3;
            this.Ok1.BorderThickness = 1;
            this.Ok1.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(178)))), ((int)(((byte)(57)))));
            this.Ok1.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(178)))), ((int)(((byte)(57)))));
            this.Ok1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Ok1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Ok1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Ok1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Ok1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Ok1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(178)))), ((int)(((byte)(77)))));
            this.Ok1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.Ok1.ForeColor = System.Drawing.Color.White;
            this.Ok1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(198)))), ((int)(((byte)(77)))));
            this.Ok1.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(198)))), ((int)(((byte)(77)))));
            this.Ok1.Location = new System.Drawing.Point(54, 250);
            this.Ok1.Margin = new System.Windows.Forms.Padding(0);
            this.Ok1.Name = "Ok1";
            this.Ok1.Size = new System.Drawing.Size(192, 38);
            this.Ok1.TabIndex = 113;
            this.Ok1.Text = "OK!";
            this.Ok1.Click += new System.EventHandler(this.Ok1_Click);
            // 
            // Texto
            // 
            this.Texto.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Texto.BackColor = System.Drawing.Color.Transparent;
            this.Texto.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.Texto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.Texto.Location = new System.Drawing.Point(10, 160);
            this.Texto.Margin = new System.Windows.Forms.Padding(0);
            this.Texto.Name = "Texto";
            this.Texto.Size = new System.Drawing.Size(280, 70);
            this.Texto.TabIndex = 116;
            this.Texto.Text = "Alterações salvas com sucesso! O aplicativo será reiniciado para aplicar as alter" +
    "ações feitas.";
            this.Texto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(50, 115);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 35);
            this.label1.TabIndex = 114;
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
            this.Gif.Size = new System.Drawing.Size(280, 100);
            this.Gif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Gif.TabIndex = 112;
            this.Gif.TabStop = false;
            // 
            // GifTimer
            // 
            this.GifTimer.Interval = 1;
            this.GifTimer.Tick += new System.EventHandler(this.GifTimer_Tick);
            // 
            // RestartApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Texto);
            this.Controls.Add(this.Ok1);
            this.Controls.Add(this.Gif);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "RestartApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reinicar aplicativo";
            this.Load += new System.EventHandler(this.RestartApp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Gif)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2Button Ok1;
        private System.Windows.Forms.PictureBox Gif;
        private System.Windows.Forms.Label Texto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer GifTimer;
    }
}